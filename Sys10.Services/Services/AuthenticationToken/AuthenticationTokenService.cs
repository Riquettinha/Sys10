using Sys10.Data.Helper;
using Sys10.Data.Models;
using Sys10.Data.UnitOfWork;
using Sys10.Services.Objects;
using System;
using System.Linq;

namespace Sys10.Services.Services
{
    public class AuthenticationTokenService : IAuthenticationTokenService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AuthenticationTokenService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public string CreateAuthenticationToken(User user)
        {
            var random = new Random();
            var token = random.Next(100000, 999999);
            user.AuthenticationToken = token.ToString();
            user.AuthenticationTokenExpiration = DateTime.Now.Brasilia().AddMonths(1);

            _unitOfWork.RepositoryBase.Edit(user);
            _unitOfWork.Commit();

            return user.AuthenticationToken;
        }

        public Result ValidateToken(string name, string token)
        {
            var userTokenData = _unitOfWork.RepositoryBase
                .Get<User>(u => u.Name == name)
                .Select(u => new { u.AuthenticationToken, u.AuthenticationTokenExpiration })
                .FirstOrDefault();

            if (userTokenData.AuthenticationTokenExpiration < DateTime.Now.Brasilia())
                return new Result()
                {
                    Status = false,
                    Message = "Token expirado. Favor autenticar novamente para gerar novo token."
                };

            if (userTokenData.AuthenticationToken != token)
                return new Result()
                {
                    Status = false,
                    Message = "Token inválido para usuário."
                };

            return new Result()
            {
                Status = true
            };
        }
    }
}
