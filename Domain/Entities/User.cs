using Microsoft.AspNetCore.Identity;
using System;

namespace Domain
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
