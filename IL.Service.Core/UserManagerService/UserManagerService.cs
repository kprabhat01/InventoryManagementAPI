using AutoMapper;
using IL.DTO.Core.Result;
using IL.DTO.Core.UserDTO;
using IM.Data.Core;
using System.Collections.Generic;
using System.Linq;

namespace IL.Service.Core.UserManagerService
{
    public class UserManagerService : IUserManagerService
    {

        private readonly IMapper _mapper;

        public UserManagerService(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public int CreateUser(User obj)
        {
            using (var entities = new db_InventoryEntities())
            {
                entities.Users.Add(obj);
                entities.SaveChanges();
                return obj.id;
            }
        }
        public List<UserResponseDTO> GetUsers()
        {
            using (var entities = new db_InventoryEntities())
            {
                return entities.Users.Where(p => p.deleteflag == false).AsEnumerable().Select(opt => this._mapper.Map<UserResponseDTO>(opt)).ToList();
            }
        }
        public bool DeleteUser(int userid)
        {
            using (var entities = new db_InventoryEntities())
            {
                var user = entities.Users.SingleOrDefault(p => p.id == userid);
                if (user == null) return false;
                user.deleteflag = true;
                entities.SaveChanges();
                return true;
            }
        }
        public bool InActivateUser(int userid)
        {
            using (var entities = new db_InventoryEntities())
            {
                var user = entities.Users.SingleOrDefault(p => p.id == userid);
                user.isActive = false;
                entities.SaveChanges();
                return true;
            }
        }
        public bool ChangePassowrd(int userid, string oldPassowrd, string password)
        {
            using (var entities = new db_InventoryEntities())
            {
                var user = entities.Users.SingleOrDefault(p => p.id == userid && p.deleteflag == false && p.passcode == oldPassowrd);
                if (user == null) return false;
                user.passcode = password;
                entities.SaveChanges();
                return true;
            }
        }
        public bool ResetPassword(int userid, string password)
        {

            using (var entities = new db_InventoryEntities())
            {
                var user = entities.Users.SingleOrDefault(p => p.id == userid && p.deleteflag == false);
                if (user == null) return false;
                user.passcode = password;
                entities.SaveChanges();
                return true;
            }

        }
        public bool ValidateUser(int userid)
        {
            using (var entities = new db_InventoryEntities())
            {
                return entities.Users.Any(p => p.id == userid && p.deleteflag == false);
            }
        }
        public bool CheckUniqueUserName(string username)
        {
            using (var entities = new db_InventoryEntities())
            {
                return entities.Users.Any(p => p.userId == username);
            }
        }

        public bool SaveUserStoreMapping(List<int> outlets, int userId)
        {
            using (var entities = new db_InventoryEntities())
            {
                List<OutletMapping> lst = new List<OutletMapping>();
                foreach (int outlet in outlets)
                {
                    lst.Add(new OutletMapping
                    {
                        userId = userId,
                        outletId = outlet
                    });
                }
                entities.OutletMappings.AddRange(lst);
                entities.SaveChanges();
                return true;
            }
        }

    }
}
