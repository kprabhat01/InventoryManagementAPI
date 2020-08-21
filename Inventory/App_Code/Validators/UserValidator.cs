using IL.Service.Core.UserManagerService;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Inventory.App_Code.Validators
{
    public class UserValidator : ValidationAttribute
    {
        private readonly IUserManagerService _userManager;
        public UserValidator(IUserManagerService userManager)
        {
            this._userManager = userManager;
        }
        public override bool IsValid(object value)
        {
            int userId = (int)value;
            return this._userManager.ValidateUser(userId);
        }
    }
}