namespace Million.Domain.Services.Utilities
{
    using Million.Domain.Entities.Response;

    public static class Helper
    {
        public static GeneralResponse ManageResponse(object data = null, bool status = true)
        {
            return new GeneralResponse { isSuccess = status, result = data };
        }
    }
}