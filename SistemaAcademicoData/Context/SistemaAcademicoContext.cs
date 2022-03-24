using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SistemaAcademicoData.Context
{
    public class SistemaAcademicoContext : IdentityDbContext
    {
        public SistemaAcademicoContext(DbContextOptions<SistemaAcademicoContext> options)
            : base(options)
        {
        }
    }
}