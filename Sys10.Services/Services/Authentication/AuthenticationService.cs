using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Services.Objects;

namespace Sys10.Services.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICryptService _cryptService;
        private readonly IAuthenticationTokenService _authenticationTokenService;

        public AuthenticationService(IUnitOfWork unitOfWork, ICryptService cryptService,
            IAuthenticationTokenService authenticationTokenService)
        {
            _unitOfWork = unitOfWork;
            _cryptService = cryptService;
            _authenticationTokenService = authenticationTokenService;
        }

        public Result Authenticate(string name, string password)
        {
            var user = _unitOfWork.RepositoryBase.FirstOrDefault<User>(u => u.Name == name);
            if (user == null)
                return new Result()
                {
                    Status = false,
                    Message = "Usuário não encontrado."
                };

            var passwordValidation = _cryptService.IsValid(password, user?.Password);
            if(!passwordValidation)
                return new Result()
                {
                    Status = false,
                    Message = "Senha inválida!"
                };

            if(!user.Status)
                return new Result()
                {
                    Status = false,
                    Message = "Usuário desativado."
                };

            var authenticationToken = _authenticationTokenService.CreateAuthenticationToken(user);
            return new Result()
            {
                Status = true,
                Message = authenticationToken
            };
        }
    }
}
