namespace SistemaAcademicoApplication.Interfaces
{
    public interface IChecksumService
    {
        string GetMD5Checksum(string fileName);
    }
}
