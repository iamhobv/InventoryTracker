using InventoryTracker.Data;
using System.IdentityModel.Tokens.Jwt;
using InventoryTracker.DTOs.RoleDTOs;
using InventoryTracker.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using InventoryTracker.Services;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RoleController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        [HttpPost("AddNewRole")]
        public async Task<ActionResult<GeneralResponse>> AddNewRole(AddNewRoleDTO NewRoleName)
        {
            ApplicationRole NewRole = new ApplicationRole() { Name = NewRoleName.Name };

            if (ModelState.IsValid)
            {
                IdentityResult roleResult = await roleManager.CreateAsync(NewRole);
                if (roleResult.Succeeded)
                {
                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "New role added"
                    };


                }
                else
                {
                    foreach (var item in roleResult.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }


                }
            }


            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
        }

        [HttpGet("getUsersAndRoles")]
        public ActionResult<GeneralResponse> GetAllUserandRoles()
        {
            var AllUsers = userManager.Users.ToList().ProjectEnumrableTo<ApplicationUser, UsernameDTO>();
            var AllRoles = roleManager.Roles.ToList().ProjectEnumrableTo<ApplicationRole, RoleNameDTO>();


            return new GeneralResponse()
            {
                IsPass = true,
                Data = new
                {
                    Users = AllUsers,
                    Roles = AllRoles
                }
            };
        }

        [HttpPost("AssignRole")]
        public async Task<ActionResult<GeneralResponse>> AssignRole(AssignRoleDTO AssignRoleFrom)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser UserIdExists = await userManager.FindByNameAsync(AssignRoleFrom.UserName);
                if (UserIdExists != null)
                {
                    bool RoleExists = await roleManager.RoleExistsAsync(AssignRoleFrom.RoleName);
                    if (RoleExists)
                    {
                        var AddRole = await userManager.AddToRoleAsync(UserIdExists, AssignRoleFrom.RoleName);
                        if (AddRole.Succeeded)
                        {
                            return new GeneralResponse()
                            {
                                IsPass = true,
                                Data = "Role added to user successfully"
                            };
                        }
                        else
                        {
                            foreach (var error in AddRole.Errors)
                            {
                                ModelState.AddModelError("", error.Description);
                            }

                        }

                    }
                    else
                    {
                        ModelState.AddModelError("", "Role is not exists");

                    }

                }
                else
                {
                    ModelState.AddModelError("", "User is not exists");

                }
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };

        }


    }
}
