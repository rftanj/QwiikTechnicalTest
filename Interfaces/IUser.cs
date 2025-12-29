using QwiikTechnicalTest.Models.DTO.User;

namespace QwiikTechnicalTest.Interfaces
{
    public interface IUser
    {
        Task<bool> SignInUser(UserLoginDTO dto);
    }
}
