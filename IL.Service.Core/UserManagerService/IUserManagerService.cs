using IL.DTO.Core.UserDTO;
using IM.Data.Core;
using System.Collections.Generic;

namespace IL.Service.Core.UserManagerService
{
    public interface IUserManagerService
    {
        public int CreateUser(User obj);
        public List<UserResponseDTO> GetUsers();
        public bool DeleteUser(int userid);
        public bool InActivateUser(int userid);
        public bool ChangePassowrd(int userid, string oldPassowrd, string password);
        public bool ResetPassword(int userid, string password);
        public bool ValidateUser(int userid);
        public bool CheckUniqueUserName(string username);
        public bool SaveUserStoreMapping(List<int> outlets, int userId);
    }
}
