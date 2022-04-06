using Microsoft.AspNetCore.Identity;

namespace Domain.ValidationEntities
{
    public class BrazilianIdentityErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError LoginAlreadyAssociated() => new IdentityError
        {
            Code = nameof(LoginAlreadyAssociated),
            Description = "Email já associado a outra conta."
        };

        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError { Code = nameof(DuplicateUserName), Description = $"'{userName}', já está associado a outra conta" };
        }
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError { Code = nameof(DuplicateEmail), Description = $"'{email}', já está associado a outra conta" };
        }
    }
}
