using TangerinesAuction.Core.Abstractions;
using TangerinesAuction.Core.Abstractions.Repositories;
using TangerinesAuction.Core.Abstractions.Services;
using TangerinesAuction.Core.Exceptions;
using TangerinesAuction.Core.Models;

namespace TangerinesAuction.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsersRepository _usersRepository;

        private readonly IPasswordVerifier _passwordVerifier;
        private readonly IJwtProvider _jwtProvider;

        public AuthService(IUsersRepository usersRepository, IPasswordVerifier passwordVerifier, IJwtProvider jwtProvider)
        {
            _usersRepository = usersRepository;
            _passwordVerifier = passwordVerifier;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _usersRepository.GetByEmailAsync(email);
            if (user == null || !_passwordVerifier.Verify(password, user.PasswordHash))
            {
                throw new UnauthorizedAccessException("Неправильный email или пароль");
            }

            var token = _jwtProvider.GenerateToken(user);
            return token;
        }

        public async Task<User> RegisterAsync(string email, string passwordHash)
        {
            var existingUser = await _usersRepository.GetByEmailAsync(email);
            if (existingUser != null)
            {
                throw new UserException("Юзер с таким email уже существует");
            }

            var user = new User(email, passwordHash);
            await _usersRepository.AddAsync(user);
            return user;
        }
    }
}
