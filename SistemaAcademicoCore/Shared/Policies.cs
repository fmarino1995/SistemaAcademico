using Microsoft.AspNetCore.Authorization;

namespace SistemaAcademicoCore.Shared
{
    public static class Policies
    {
        public const string IsAdmin = "IsAdmin";
        public const string IsProfessor = "IsProfessor";
        public const string IsAluno = "IsAluno";

        public static AuthorizationPolicy IsAdminPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole("Administrativo")
                                                   .Build();
        }

        public static AuthorizationPolicy IsProfessorPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole("Professor")
                                                   .Build();
        }

        public static AuthorizationPolicy IsAlunoPolicy()
        {
            return new AuthorizationPolicyBuilder().RequireAuthenticatedUser()
                                                   .RequireRole("Aluno")
                                                   .Build();
        }
    }
}
