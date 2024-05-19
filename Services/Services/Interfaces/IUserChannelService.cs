namespace Services.Services
{
    public interface IUserChannelService : IService
    {
        Task<int[]> GetUserChannelAsync(int userId);
    }
}
