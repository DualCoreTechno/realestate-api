using Data.Entity;
using Data.Seed;
using Data.Services;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.ComponentModel;
using ViewModels.Common;
using ViewModels.Enums;

namespace Data.Data
{
    public class AppDB : IdentityDbContext<Tbl_Users>
    {
        readonly int _userId;

        public AppDB(DbContextOptions<AppDB> options, IUserTokenService userTokenService) : base(options)
        {
            if (userTokenService != null)
            {
                _userId = userTokenService.GetUserId();
            }
        }

        #region Tables
        public DbSet<Tbl_Users> Tbl_Users { get; set; }
        public DbSet<Tbl_MinMax> Tbl_MinMax { get; set; }
        public DbSet<Tbl_Activity> Tbl_Activity { get; set; }
        public DbSet<Tbl_Area> Tbl_Area { get; set; }
        public DbSet<Tbl_Buildings> Tbl_Buildings { get; set; }
        public DbSet<Tbl_DraftReason> Tbl_DraftReason { get; set; }
        public DbSet<Tbl_EnquiryStatus> Tbl_EnquiryStatus { get; set; }
        public DbSet<Tbl_FurnitureStatus> Tbl_FurnitureStatus { get; set; }
        public DbSet<Tbl_PropertyType> Tbl_PropertyType { get; set; }
        public DbSet<Tbl_Measurement> Tbl_Measurement { get; set; }
        public DbSet<Tbl_Nonuse> Tbl_Nonuse { get; set; }
        public DbSet<Tbl_Price> Tbl_Price { get; set; }
        public DbSet<Tbl_Purpose> Tbl_Purpose { get; set; }
        public DbSet<Tbl_Role> Tbl_Role { get; set; }
        public DbSet<Tbl_Source> Tbl_Source { get; set; }
        public DbSet<Tbl_BhkOffice> Tbl_BhkOffice { get; set; }
        public DbSet<Tbl_Enquiry> Tbl_Enquiry { get; set; }
        public DbSet<Tbl_EnquiryRemarks> Tbl_EnquiryRemarks { get; set; }
        public DbSet<Tbl_ActivityParent> Tbl_ActivityParent { get; set; }
        public DbSet<Tbl_ActivityChild> Tbl_ActivityChild { get; set; }
        public DbSet<Tbl_Budget> Tbl_Budget { get; set; }
        public DbSet<Tbl_Property> Tbl_Property { get; set; }
        public DbSet<Tbl_PropertyDeal> Tbl_PropertyDeal { get; set; }
        public DbSet<Tbl_PropertyDealPayment> Tbl_PropertyDealPayment { get; set; }
        public DbSet<Tbl_PropertyStatus> Tbl_PropertyStatus { get; set; }
        public DbSet<Tbl_Segment> Tbl_Segment { get; set; }
        public DbSet<Tbl_Role_Permission> Tbl_Role_Permission { get; set; }
        public DbSet<Tbl_LogData> Tbl_LogData { get; set; }

        #endregion Tables

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(decimal) || property.ClrType == typeof(decimal?))
                    {
                        property.SetColumnType("decimal(18,2)"); // Adjust precision and scale as needed
                    }
                }
            }

            modelBuilder.Entity<Tbl_PropertyDeal>()
            .HasOne(p => p.PropertySource)
            .WithMany()
            .HasForeignKey(p => p.PropertySourceId)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbl_PropertyDeal>()
                .HasOne(p => p.BuyerSource)
                .WithMany()
                .HasForeignKey(p => p.BuyerSourceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbl_Segment>()
              .HasMany(s => s.TblBhkOffice)
              .WithOne(ad => ad.Segment)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbl_BhkOffice>()
            .HasMany(s => s.TblBudget)
            .WithOne(ad => ad.BhkOffice)
            .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbl_PropertyType>()
                      .HasMany(s => s.TblSegment)
                      .WithOne(ad => ad.PropertyType)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbl_ActivityParent>()
                      .HasMany(s => s.ActivityChild)
                      .WithOne(ad => ad.ActivityParent)
                      .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tbl_Role>()
                     .HasMany(s => s.TblRolePermission)
                     .WithOne(ad => ad.Role)
                     .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

        public override int SaveChanges()
        {
            ManageLogs();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ManageLogs();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public void ManageLogs()
        {
            var logEntries = new List<Tbl_LogData>();

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted));

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("CreatedBy").IsModified = false;
                    entry.Property("CreatedOn").IsModified = false;

                    ((BaseEntity)entry.Entity).UpdatedBy = _userId;
                    ((BaseEntity)entry.Entity).UpdatedOn = DateConverterForData.GetSystemDateTime();
                }

                if (entry.State == EntityState.Added)
                {
                    ((BaseEntity)entry.Entity).CreatedBy = _userId;
                    ((BaseEntity)entry.Entity).CreatedOn = DateConverterForData.GetSystemDateTime();
                }

                if (TableNameArray.MasterTableName.Any(name => entry.CurrentValues.EntityType.Name.Replace("Data.Entity.", "").Equals(name)))
                {
                    var nameColumnValue = entry.CurrentValues["Name"];
                    logEntries.Add(GetLogObject($"Entry [{nameColumnValue}] {entry.State}", LogTypeEnum.MasterLog, entry.CurrentValues.EntityType.Name));
                }
                else if (TableNameArray.TransactionTableName.Any(name => entry.CurrentValues.EntityType.Name.Replace("Data.Entity.", "").Equals(name)))
                {
                    //int idColumnValue = Convert.ToInt32(entry.CurrentValues["Id"]);
                    logEntries.Add(GetLogObject($"Entry {entry.State}", LogTypeEnum.TransactionLog, entry.CurrentValues.EntityType.Name));
                }
                else
                {
                    switch (entry.CurrentValues.EntityType.Name.Replace("Data.Entity.", "").ToUpper())
                    {
                        case "TBL_USERS":
                            {
                                var userNameColumnValue = entry.CurrentValues["UserName"];
                                logEntries.Add(GetLogObject($"Entry [{userNameColumnValue}] {entry.State}", LogTypeEnum.MasterLog, entry.CurrentValues.EntityType.Name));
                                break;
                            }

                        case "TBL_MINMAX":
                            {
                                var minTitleColumnValue = entry.CurrentValues["MinTitle"];
                                var maxTitleColumnValue = entry.CurrentValues["MaxTitle"];
                                
                                logEntries.Add(GetLogObject($"Entry Min Title [{minTitleColumnValue}] And Max Title [{maxTitleColumnValue}] {entry.State}", LogTypeEnum.MasterLog, entry.CurrentValues.EntityType.Name));
                                break;
                            }

                        case "TBL_ENQUIRYREMARKS":
                            {
                                //var enquiryIdColumnValue = entry.CurrentValues["EnquiryId"];
                                logEntries.Add(GetLogObject($"New Remark Added For Enquiry", LogTypeEnum.TransactionLog, entry.CurrentValues.EntityType.Name));
                                break;
                            }

                        //case "TBL_ROLE_PERMISSION":
                        //    {
                        //        //var roleNameColumnValue = entry.CurrentValues["RoleId"];
                        //        logEntries.Add(GetLogObject($"Permission Updated", LogTypeEnum.MasterLog, entry.CurrentValues.EntityType.Name));
                        //        break;
                        //    }
                    }
                }
            }

            foreach (var logEntry in logEntries)
            {
                Tbl_LogData.Add(logEntry);
            }
        }

        public Tbl_LogData GetLogObject(string description, LogTypeEnum logTypeEnum, string tableName)
        {
            return new Tbl_LogData
            {
                Description = description,
                Module = logTypeEnum,
                PageName = TableNameArray.GetAliasForTable(tableName.Replace("Data.Entity.", "")),
                IpAddress = "",
                CreatedBy = _userId,
                CreatedOn = DateConverterForData.GetSystemDateTime()
            };
        }
    }
}
