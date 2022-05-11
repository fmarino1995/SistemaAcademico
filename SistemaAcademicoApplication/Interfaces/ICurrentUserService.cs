namespace SistemaAcademicoApplication.Interfaces
{
    public interface ICurrentUserService
    {
        Task<string> GetUserNameAsync();
        Task<string> GetUserName();
    }
}
