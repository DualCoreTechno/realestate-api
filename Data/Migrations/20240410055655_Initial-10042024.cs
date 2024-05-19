using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    public partial class Initial10042024 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecurityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtpSendTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DateOfJoin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    IsPresent = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ProfileImage = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastLoginIp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Activity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsParent = table.Column<bool>(type: "bit", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ParentActivityId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Activity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ActivityParent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ActivityParent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Area",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Area", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Buildings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Buildings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_DraftReason",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_DraftReason", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_EnquiryRemarks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EnquiryId = table.Column<int>(type: "int", nullable: false),
                    ActivityChildId = table.Column<int>(type: "int", nullable: true),
                    Nfd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EnquiryStatusId = table.Column<int>(type: "int", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_EnquiryRemarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_EnquiryStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_EnquiryStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_FurnitureStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_FurnitureStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_LogData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Module = table.Column<int>(type: "int", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IpAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_LogData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Measurement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Measurement", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_MinMax",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    For = table.Column<int>(type: "int", nullable: false),
                    MinValue = table.Column<int>(type: "int", nullable: false),
                    MaxValue = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_MinMax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Nonuse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Nonuse", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Price",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Price", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_PropertyDealPayment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyDealId = table.Column<int>(type: "int", nullable: false),
                    PaymentOption = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PropertyDealPayment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_PropertyStatus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PropertyStatus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_PropertyType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PropertyType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Purpose",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Purpose", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Role",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Source",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Source", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_ActivityChild",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ActivityParentId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_ActivityChild", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_ActivityChild_Tbl_ActivityParent_ActivityParentId",
                        column: x => x.ActivityParentId,
                        principalTable: "Tbl_ActivityParent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Segment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    PropertyTypeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Segment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Segment_Tbl_PropertyType_PropertyTypeId",
                        column: x => x.PropertyTypeId,
                        principalTable: "Tbl_PropertyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Role_Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Role_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Role_Permission_Tbl_Role_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Tbl_Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_BhkOffice",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    SegmentId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_BhkOffice", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_BhkOffice_Tbl_Segment_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Tbl_Segment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Budget",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    From = table.Column<int>(type: "int", nullable: false),
                    To = table.Column<int>(type: "int", nullable: false),
                    For = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    BhkOfficeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Budget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Budget_Tbl_BhkOffice_BhkOfficeId",
                        column: x => x.BhkOfficeId,
                        principalTable: "Tbl_BhkOffice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Property",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    PropertyFor = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Block = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SuperBuiltupArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CarpetArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    BuiltupArea = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    FurnitureStatusId = table.Column<int>(type: "int", nullable: true),
                    Parking = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Comission = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AvailableFrom = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BhkOfficeId = table.Column<int>(type: "int", nullable: true),
                    BuildingId = table.Column<int>(type: "int", nullable: true),
                    AreaId = table.Column<int>(type: "int", nullable: true),
                    MeasurementId = table.Column<int>(type: "int", nullable: true),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    PropertyStatusId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Property", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Property_Tbl_Area_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Tbl_Area",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Property_Tbl_BhkOffice_BhkOfficeId",
                        column: x => x.BhkOfficeId,
                        principalTable: "Tbl_BhkOffice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Property_Tbl_Buildings_BuildingId",
                        column: x => x.BuildingId,
                        principalTable: "Tbl_Buildings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Property_Tbl_Measurement_MeasurementId",
                        column: x => x.MeasurementId,
                        principalTable: "Tbl_Measurement",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Property_Tbl_PropertyStatus_PropertyStatusId",
                        column: x => x.PropertyStatusId,
                        principalTable: "Tbl_PropertyStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Property_Tbl_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Tbl_Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_PropertyDeal",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    DealTypeId = table.Column<int>(type: "int", nullable: false),
                    DealDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PossessionDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlatOfficeNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SquareFeet = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DealAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OwnerBrokerage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ClientBrokerage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PropertySourceId = table.Column<int>(type: "int", nullable: false),
                    BuyerSourceId = table.Column<int>(type: "int", nullable: false),
                    BhkOfficeId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_PropertyDeal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_PropertyDeal_Tbl_BhkOffice_BhkOfficeId",
                        column: x => x.BhkOfficeId,
                        principalTable: "Tbl_BhkOffice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_PropertyDeal_Tbl_Source_BuyerSourceId",
                        column: x => x.BuyerSourceId,
                        principalTable: "Tbl_Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_PropertyDeal_Tbl_Source_PropertySourceId",
                        column: x => x.PropertySourceId,
                        principalTable: "Tbl_Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tbl_Enquiry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NameOfClient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnquiryFor = table.Column<int>(type: "int", nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastRemark = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AssignTo = table.Column<int>(type: "int", nullable: false),
                    AssignBy = table.Column<int>(type: "int", nullable: false),
                    Nfd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AreaId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsClosed = table.Column<bool>(type: "bit", nullable: true),
                    Mobile1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mobile3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SourceId = table.Column<int>(type: "int", nullable: true),
                    BhkOfficeId = table.Column<int>(type: "int", nullable: true),
                    EnquiryStatusId = table.Column<int>(type: "int", nullable: true),
                    BudgetId = table.Column<int>(type: "int", nullable: true),
                    NonuseId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedBy = table.Column<int>(type: "int", nullable: true),
                    UpdatedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tbl_Enquiry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tbl_Enquiry_Tbl_BhkOffice_BhkOfficeId",
                        column: x => x.BhkOfficeId,
                        principalTable: "Tbl_BhkOffice",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Enquiry_Tbl_Budget_BudgetId",
                        column: x => x.BudgetId,
                        principalTable: "Tbl_Budget",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Enquiry_Tbl_EnquiryStatus_EnquiryStatusId",
                        column: x => x.EnquiryStatusId,
                        principalTable: "Tbl_EnquiryStatus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Enquiry_Tbl_Nonuse_NonuseId",
                        column: x => x.NonuseId,
                        principalTable: "Tbl_Nonuse",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tbl_Enquiry_Tbl_Source_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Tbl_Source",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Tbl_EnquiryStatus",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsActive", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4473), true, "Open", null, null },
                    { 2, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4476), true, "Close", null, null },
                    { 3, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4477), true, "Hold", null, null }
                });

            migrationBuilder.InsertData(
                table: "Tbl_Role",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "IsActive", "Name", "UpdatedBy", "UpdatedOn" },
                values: new object[] { 1, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4227), true, "Admin", null, null });

            migrationBuilder.InsertData(
                table: "Tbl_Role_Permission",
                columns: new[] { "Id", "CreatedBy", "CreatedOn", "MenuId", "RoleId", "UpdatedBy", "UpdatedOn" },
                values: new object[,]
                {
                    { 1, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4356), 1, 1, null, null },
                    { 2, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4358), 2, 1, null, null },
                    { 3, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4359), 3, 1, null, null },
                    { 4, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4360), 4, 1, null, null },
                    { 5, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4361), 5, 1, null, null },
                    { 6, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4363), 6, 1, null, null },
                    { 7, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4364), 7, 1, null, null },
                    { 8, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4364), 8, 1, null, null },
                    { 9, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4365), 10, 1, null, null },
                    { 10, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4366), 11, 1, null, null },
                    { 11, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4367), 12, 1, null, null },
                    { 12, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4368), 13, 1, null, null },
                    { 13, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4368), 16, 1, null, null },
                    { 14, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4369), 18, 1, null, null },
                    { 15, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4369), 20, 1, null, null },
                    { 16, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4370), 21, 1, null, null },
                    { 17, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4370), 22, 1, null, null },
                    { 18, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4372), 23, 1, null, null },
                    { 19, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4372), 24, 1, null, null },
                    { 20, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4373), 25, 1, null, null },
                    { 21, 0, new DateTime(2024, 4, 10, 11, 26, 55, 418, DateTimeKind.Local).AddTicks(4373), 26, 1, null, null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_ActivityChild_ActivityParentId",
                table: "Tbl_ActivityChild",
                column: "ActivityParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_BhkOffice_SegmentId",
                table: "Tbl_BhkOffice",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Budget_BhkOfficeId",
                table: "Tbl_Budget",
                column: "BhkOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Enquiry_BhkOfficeId",
                table: "Tbl_Enquiry",
                column: "BhkOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Enquiry_BudgetId",
                table: "Tbl_Enquiry",
                column: "BudgetId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Enquiry_EnquiryStatusId",
                table: "Tbl_Enquiry",
                column: "EnquiryStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Enquiry_NonuseId",
                table: "Tbl_Enquiry",
                column: "NonuseId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Enquiry_SourceId",
                table: "Tbl_Enquiry",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Property_AreaId",
                table: "Tbl_Property",
                column: "AreaId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Property_BhkOfficeId",
                table: "Tbl_Property",
                column: "BhkOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Property_BuildingId",
                table: "Tbl_Property",
                column: "BuildingId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Property_MeasurementId",
                table: "Tbl_Property",
                column: "MeasurementId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Property_PropertyStatusId",
                table: "Tbl_Property",
                column: "PropertyStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Property_SourceId",
                table: "Tbl_Property",
                column: "SourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PropertyDeal_BhkOfficeId",
                table: "Tbl_PropertyDeal",
                column: "BhkOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PropertyDeal_BuyerSourceId",
                table: "Tbl_PropertyDeal",
                column: "BuyerSourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_PropertyDeal_PropertySourceId",
                table: "Tbl_PropertyDeal",
                column: "PropertySourceId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Role_Permission_RoleId",
                table: "Tbl_Role_Permission",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Tbl_Segment_PropertyTypeId",
                table: "Tbl_Segment",
                column: "PropertyTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Tbl_Activity");

            migrationBuilder.DropTable(
                name: "Tbl_ActivityChild");

            migrationBuilder.DropTable(
                name: "Tbl_DraftReason");

            migrationBuilder.DropTable(
                name: "Tbl_Enquiry");

            migrationBuilder.DropTable(
                name: "Tbl_EnquiryRemarks");

            migrationBuilder.DropTable(
                name: "Tbl_FurnitureStatus");

            migrationBuilder.DropTable(
                name: "Tbl_LogData");

            migrationBuilder.DropTable(
                name: "Tbl_MinMax");

            migrationBuilder.DropTable(
                name: "Tbl_Price");

            migrationBuilder.DropTable(
                name: "Tbl_Property");

            migrationBuilder.DropTable(
                name: "Tbl_PropertyDeal");

            migrationBuilder.DropTable(
                name: "Tbl_PropertyDealPayment");

            migrationBuilder.DropTable(
                name: "Tbl_Purpose");

            migrationBuilder.DropTable(
                name: "Tbl_Role_Permission");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Tbl_ActivityParent");

            migrationBuilder.DropTable(
                name: "Tbl_Budget");

            migrationBuilder.DropTable(
                name: "Tbl_EnquiryStatus");

            migrationBuilder.DropTable(
                name: "Tbl_Nonuse");

            migrationBuilder.DropTable(
                name: "Tbl_Area");

            migrationBuilder.DropTable(
                name: "Tbl_Buildings");

            migrationBuilder.DropTable(
                name: "Tbl_Measurement");

            migrationBuilder.DropTable(
                name: "Tbl_PropertyStatus");

            migrationBuilder.DropTable(
                name: "Tbl_Source");

            migrationBuilder.DropTable(
                name: "Tbl_Role");

            migrationBuilder.DropTable(
                name: "Tbl_BhkOffice");

            migrationBuilder.DropTable(
                name: "Tbl_Segment");

            migrationBuilder.DropTable(
                name: "Tbl_PropertyType");
        }
    }
}
