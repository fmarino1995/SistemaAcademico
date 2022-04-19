namespace SistemaAcademicoApplication.Interfaces
{
    public interface ICurrentUserService
    {
        Task<string> GetUserName();
    }
}
