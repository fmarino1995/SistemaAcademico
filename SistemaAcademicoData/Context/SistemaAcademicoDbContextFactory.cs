using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoData.Context
{
    public class SistemaAcademicoDbContextFactory : DesignTimeDbContextFactoryBase<SistemaAcademicoContext>
    {
        protected override SistemaAcademicoContext CreateNewInstance(DbContextOptions<SistemaAcademicoContext> options)
        {
            return new SistemaAcademicoContext(options);
        }
    }
}
