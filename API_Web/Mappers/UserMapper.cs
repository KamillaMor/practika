using API_Web.DTO;
using API_Web.Models;

namespace API_Web.Mappers
{
    public class UserMapper
    {
        public static UserDTO ConvertToDTO(UserList user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                Password = user.Password,
                Login = user.Login
            };
        }
        public static UserList ConvertToModel(UserDTO user)
        {
            return new UserList
            {
                UserId = user.UserId,
                Surname = user.Surname,
                Name = user.Name,
                Patronymic = user.Patronymic,
                Password = user.Password,
                Login = user.Login
            };
        }
    }
}
