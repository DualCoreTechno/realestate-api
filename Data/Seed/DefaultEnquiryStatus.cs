using Data.Entity;
using Data.Services;

namespace Data.Seed
{
    public static class DefaultEnquiryStatus
    {
        public static List<Tbl_EnquiryStatus> EnquiryStatusList()
        {
            return new List<Tbl_EnquiryStatus>
            {
                new() 
                { 
                    Id = 1,
                    Name = "Open", 
                    IsActive = true,
                    CreatedOn = DateConverterForData.GetSystemDateTime()
                },
                new()
                {
                    Id = 2,
                    Name = "Close",
                    IsActive = true,
                    CreatedOn = DateConverterForData.GetSystemDateTime()
                },
                new()
                {
                    Id = 3,
                    Name = "Hold",
                    IsActive = true,
                    CreatedOn = DateConverterForData.GetSystemDateTime()
                }
            };
        }
    }
}