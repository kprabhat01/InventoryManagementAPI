using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IL.DTO.Core.UserDTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        public int RoleId { get; set; }
        [Required]
        public string Passcode { get; set; }
        public string CreatedBy { get; set; } = "System";
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public List<int> Outlet { get; set; }
    }
    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserId { get; set; }
        public int RoleId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreateDate { get; set; }
    }
    public class CredentialDTO
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Passcode { get; set; }
    }

    public class ChangeCredential
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string oldPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
    }
    public class AdminCrendital
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
