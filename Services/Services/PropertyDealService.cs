using Data.Data;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Services.Settings;
using ViewModels.Enums;
using ViewModels.ViewModels;

namespace Services.Services
{
    public class PropertyDealService : IPropertyDealService
    {
        private readonly AppDB _dbContext;
        private readonly IUserChannelService _userChannelService;

        public PropertyDealService(AppDB dbContext, IUserChannelService userChannelService)
        {
            _dbContext = dbContext;
            _userChannelService = userChannelService;
        }

        public async Task<ResponseBaseModel> GetPageLoadDataAsync(int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    List<Tbl_Users> userList;

                    if (userRole == RoleEnum.Admin.ToString())
                    {
                        userList = await _dbContext.Tbl_Users.ToListAsync();
                    }
                    else
                    {
                        int[] userChannel = await _userChannelService.GetUserChannelAsync(userId);
                        userList = await _dbContext.Tbl_Users.Where(x => userChannel.Contains(x.UserId)).OrderBy(x => x.FirstName).ToListAsync();
                    }

                    var propertyTypeList = await _dbContext.Tbl_PropertyType.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive,
                    }).OrderBy(x => x.Text).ToListAsync();

                    var sourceList = await _dbContext.Tbl_Source.Select(record => new DropDownCommonResponse
                    {
                        Id = record.Id,
                        Text = record.Name,
                        IsActive = record.IsActive,
                    }).OrderBy(x => x.Text).ToListAsync();

                    List<Tbl_BhkOffice> bhkOfficeList = await _dbContext.Tbl_BhkOffice.OrderBy(x => x.Name).Include(x => x.Segment).ThenInclude(x => x.PropertyType).ToListAsync();

                    PropertyDealPageLoadModel response = new()
                    {
                        PropertyTypeList = propertyTypeList,

                        UserList = userList.Select(record => new DropDownCommonResponse
                        {
                            Id = record.UserId,
                            Text = $"{record.FirstName} {record.LastName}",
                            IsActive = record.IsActive,
                        }).ToList(),

                        SourceList = sourceList,

                        BhkOfficeList = bhkOfficeList.Select(record => new DropDownCommonResponse
                        {
                            Id = record.Id,
                            Text = $"{record.Name} - {record.Segment.Name} - {record.Segment.PropertyType.Name}",
                            IsActive = record.IsActive,
                        }).ToList()
                    };

                    return new ResponseBaseModel(200, response);
                }
                else
                {
                    return new ResponseBaseModel(404, new EnquiryPageLoadModel(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> GetAllPropertyDealAsync(int userId, int roleId)
        {
            try
            {
                var userRole = await _dbContext.Tbl_Role.Where(x => x.Id == roleId).Select(x => x.Name).FirstOrDefaultAsync();

                if (userRole != null)
                {
                    int[]? userChannel = null;

                    if (userRole != RoleEnum.Admin.ToString())
                        userChannel = await _userChannelService.GetUserChannelAsync(userId);

                    IQueryable<PropertyDealModel> query =
                    (
                        from PropertyDeal in _dbContext.Tbl_PropertyDeal
                        where (userChannel == null || userChannel.Contains(PropertyDeal.UserId))
                        
                        join Users in _dbContext.Tbl_Users on PropertyDeal.UserId equals Users.UserId into UsersTemp
                        from Users in UsersTemp.DefaultIfEmpty()
                        
                        orderby PropertyDeal.CreatedOn descending

                        select new PropertyDealModel
                        {
                            Id = PropertyDeal.Id,
                            DealTypeId = PropertyDeal.DealTypeId,
                            UserId = PropertyDeal.UserId,
                            EmployeeName = $"{Users.FirstName} {Users.LastName}",
                            DealDate = DateConverter.ToStringDateTime(PropertyDeal.DealDate),
                            PossessionDate = DateConverter.ToStringDateTime(PropertyDeal.PossessionDate),
                            PropertyName = PropertyDeal.PropertyName,
                            FlatOfficeNo = PropertyDeal.FlatOfficeNo,
                            OwnerName = PropertyDeal.OwnerName,
                            OwnerContactNo = PropertyDeal.OwnerContactNo,
                            PropertySourceId = PropertyDeal.PropertySourceId,
                            BuyerName = PropertyDeal.BuyerName,
                            BuyerContactNo = PropertyDeal.BuyerContactNo,
                            BuyerSourceId = PropertyDeal.BuyerSourceId,
                            BhkOfficeId = PropertyDeal.BhkOfficeId,
                            SquareFeet = PropertyDeal.SquareFeet,
                            DealAmount = PropertyDeal.DealAmount,
                            OwnerBrokerage = PropertyDeal.OwnerBrokerage,
                            ClientBrokerage = PropertyDeal.ClientBrokerage,
                            Remark = PropertyDeal.Remark,
                            Payments = _dbContext.Tbl_PropertyDealPayment.Where(x => x.PropertyDealId == PropertyDeal.Id).
                                        Select(x => new PropertyDealPaymentModel
                                        {
                                            PaymentOption = x.PaymentOption,
                                            Amount = x.Amount,
                                            Date = x.Date,
                                            Remark = x.Remark
                                        }).OrderBy(x => x.Date).ToList(),
                        }
                    ).AsQueryable();

                    int totalRecords = query.Count();

                    List<PropertyDealModel> result = await query.ToListAsync();

                    if (result.Count > 0)
                        return new ResponseBaseModel(200, result, totalRecords);
                    else
                        return new ResponseBaseModel(404, new List<PropertyDealModel>(), "No record(s) found.");
                }
                else
                {
                    return new ResponseBaseModel(404, new List<PropertyDealModel>(), "Role not found.");
                }
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> AddOrUpdatePropertyDealAsync(PropertyDealModel request)
        {
            try
            {
                Tbl_PropertyDeal record = new()
                {
                    Id = request.Id,
                    DealTypeId = request.DealTypeId,
                    UserId = request.UserId,
                    DealDate = DateConverter.ToDateTime(request.DealDate),
                    PossessionDate = DateConverter.ToDateTime(request.PossessionDate),
                    PropertyName = request.PropertyName,
                    FlatOfficeNo = request.FlatOfficeNo,
                    OwnerName = request.OwnerName,
                    OwnerContactNo = request.OwnerContactNo,
                    PropertySourceId = request.PropertySourceId,
                    BuyerName = request.BuyerName,
                    BuyerContactNo = request.BuyerContactNo,
                    BuyerSourceId = request.BuyerSourceId,
                    BhkOfficeId = request.BhkOfficeId,
                    SquareFeet = request.SquareFeet,
                    DealAmount = request.DealAmount,
                    OwnerBrokerage = request.OwnerBrokerage,
                    ClientBrokerage = request.ClientBrokerage,
                    Remark = request.Remark,
                };

                if (request.Id != 0)
                {
                    _dbContext.Tbl_PropertyDeal.Update(record);
                    await _dbContext.SaveChangesAsync();
                }
                else
                {
                    await _dbContext.Tbl_PropertyDeal.AddAsync(record);
                    await _dbContext.SaveChangesAsync();
                }

                List<Tbl_PropertyDealPayment>? dealPayment = await _dbContext.Tbl_PropertyDealPayment.Where(x => x.PropertyDealId == record.Id).ToListAsync();

                if (dealPayment != null)
                    _dbContext.Tbl_PropertyDealPayment.RemoveRange(dealPayment);

                if (request.Payments != null)
                {
                    List<Tbl_PropertyDealPayment> payments = request.Payments.Select(rec => new Tbl_PropertyDealPayment
                    {
                        PropertyDealId = record.Id,
                        PaymentOption = rec.PaymentOption,
                        Amount = rec.Amount,
                        Date = rec.Date,
                        Remark = rec.Remark
                    }).ToList();

                    await _dbContext.Tbl_PropertyDealPayment.AddRangeAsync(payments);
                }

                await _dbContext.SaveChangesAsync();

                if (request.Id != 0)
                {
                    return new ResponseBaseModel(200, "Property deal updated successfully.");
                }

                return new ResponseBaseModel(200, "Property deal added successfully.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }

        public async Task<ResponseBaseModel> DeletePropertyDealAsync(int id)
        {
            try
            {
                Tbl_PropertyDeal? record = await _dbContext.Tbl_PropertyDeal.FirstOrDefaultAsync(x => x.Id == id);

                if (record != null)
                {
                    List<Tbl_PropertyDealPayment>? dealPayment = await _dbContext.Tbl_PropertyDealPayment.Where(x => x.PropertyDealId == record.Id).ToListAsync();

                    if (dealPayment != null)
                        _dbContext.Tbl_PropertyDealPayment.RemoveRange(dealPayment);

                    _dbContext.Tbl_PropertyDeal.Remove(record);
                }
                else
                {
                    return new ResponseBaseModel(404, "Record not found.");
                }

                await _dbContext.SaveChangesAsync();

                return new ResponseBaseModel(200, "Property deal deleted successfully.");
            }
            catch (DbUpdateException)
            {
                return new ResponseBaseModel(500, "Unable to delete the Property deal. It is connected with other entities.");
            }
            catch (Exception)
            {
                return new ResponseBaseModel(500, "Something went wrong, please try again.");
            }
        }
    }
}
