using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace aspDatabase.Areas.Identity.Data;

// Add profile data for application users by adding properties to the aspDatabaseUser class
public class aspDatabaseUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
}

