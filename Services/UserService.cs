using Microsoft.Extensions.Configuration;
using QwiikTechnicalTest.Interfaces;
using QwiikTechnicalTest.Models.DTO.User;
using QwiikTechnicalTest.Repositories;
using QwiikTechnicalTest.Utilities;

namespace QwiikTechnicalTest.Services
{
    public class UserService: IUser
    {

        private readonly UserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly string _pepper;
        private readonly string _iteration;
        public UserService(UserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _pepper = configuration.GetSection("Security:Pepper").Value ?? "";
            _iteration = configuration.GetSection("Security:Iteration").Value ?? "";
        }

        public async Task<bool> SignInUser(UserLoginDTO dto)
        {
            var user = await _userRepository.GetUserAdmin(dto.email);
            if (user is null)
                return false;
            var hashResult = Hasher.ComputeHash(dto.password, user.Salt, _pepper, Convert.ToInt32(_iteration));

            if (hashResult == user.PasswordHash)
                return true;

            return false;
        }
    }
}
