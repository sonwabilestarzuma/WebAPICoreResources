using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using WebApiCoreResources.Models;

namespace WebApiCoreResources.Data
{
    public class CampIdentityInitializer
    {
        private UserManager<CampUser> _userMgr;
        private RoleManager<IdentityRole> _roleMgr;

        public CampIdentityInitializer(UserManager<CampUser> userMgr, RoleManager<IdentityRole> roleMgr)
        {
            _userMgr = userMgr;
            _roleMgr = roleMgr;
        }

        public async Task Seed()
        {
            var user = await _userMgr.FindByNameAsync("sonwabilestarzuma");
            
            // Add User
            if(user == null)
            {
                if(!(await _roleMgr.RoleExistsAsync("Admin")))
                {
                    var role = new IdentityRole("Admin");
                 //   role.Claims.Add(new IdentityRoleClaim<string>() { ClaimType = "IsAdmin", ClaimValue = "True" });
                  
                    await _roleMgr.CreateAsync(role);
                }
                user = new CampUser()
                {
                    UserName = "sonwabilestarzuma",
                    FirstName = "Star",
                    LastName = "Zuma",
                    Email = "sonwabilestarzuma@gmail.com"
                };

                var userResult = await _userMgr.CreateAsync(user, "P@ssWord!");
                var roleResult = await _userMgr.AddToRoleAsync(user, "Admin");
                var ClaimResult = await _userMgr.AddClaimAsync(user, new Claim("SuperUser", "True"));

                if (!userResult.Succeeded || !roleResult.Succeeded || !ClaimResult.Succeeded)
                {
                    throw new InvalidOperationException("Failed to build user and roles");
                }
            }

        }
    }
}
