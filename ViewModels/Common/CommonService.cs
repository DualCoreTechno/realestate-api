namespace ViewModels.Common
{
    public static class CommonService
    {
        public static string GenerateNNumberRandomString(int length)
        {
            string randomString = string.Empty;
            Random random = new();
            
            for (int i = 0; i < length; i++)
            {
                randomString += random.Next(0, 9).ToString();
            }

            return randomString;
        }
    }
}
