using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Services.Services
{
    public class UserChannelService : IUserChannelService
    {
        private readonly AppDB _dbContext;
        private readonly List<int> _channel = new();

        public UserChannelService(AppDB dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int[]> GetUserChannelAsync(int userId)
        {
            try
            {
                _channel.Add(userId);

                var records = await _dbContext.Tbl_Users.Where(x => x.ParentId == userId).Select(y => y.UserId).ToListAsync();
                
                foreach (var user in records) 
                {
                    await GetUserChannelAsync(user);
                }
                
                return _channel.ToArray();
            }
            catch (Exception)
            {
                return Array.Empty<int>();
            }
        }
    }
}
