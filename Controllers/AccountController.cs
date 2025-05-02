using InventoryTracker.Data;
using InventoryTracker.Models;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using InventoryTracker.DTOs.AccountDTOs;

namespace InventoryTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IConfiguration configure;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configure)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configure = configure;
        }

        #region Register
        [HttpPost("Register")]
        public async Task<ActionResult<GeneralResponse>> Register(RegisterDTO registerFromReq)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser? UserEmailFromDb = await userManager.FindByEmailAsync(registerFromReq.Email);
                if (UserEmailFromDb != null)
                {
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "Email address is already taken"
                    };
                }


                ApplicationUser user = new ApplicationUser()
                {
                    UserName = registerFromReq.UserName,
                    Email = registerFromReq.Email,
                    PhoneNumber = registerFromReq.PhoneNumber
                };


                if (userManager.Users.Count() == 0)
                {
                    ApplicationRole GuestRole = new ApplicationRole()
                    {
                        Name = "User"
                    };
                    ApplicationRole AdminRole = new ApplicationRole()
                    {
                        Name = "Admin"
                    };
                    IdentityResult GuestRoleResult = await roleManager.CreateAsync(GuestRole);
                    IdentityResult AdminRoleResult = await roleManager.CreateAsync(AdminRole);

                    if (AdminRoleResult.Succeeded && GuestRoleResult.Succeeded)
                    {

                        var res = await CreateUser(user, registerFromReq.Password, "Admin");
                        if (res.IsPass)
                        {
                            return new GeneralResponse()
                            {
                                IsPass = true,
                                Data = "Account Create Success"
                            };
                        }
                        else
                        {
                            return new GeneralResponse()
                            {
                                IsPass = false,
                                Data = ModelState
                            };
                        }

                    }
                    else
                    {
                        foreach (var error in GuestRoleResult.Errors)
                        {
                            ModelState.AddModelError("newErrors", error.Description);

                        }
                        foreach (var error in AdminRoleResult.Errors)
                        {
                            ModelState.AddModelError("newErrors", error.Description);

                        }
                    }
                }
                else
                {
                    var res = await CreateUser(user, registerFromReq.Password, "User");
                    if (res.IsPass)
                    {
                        return new GeneralResponse()
                        {
                            IsPass = true,
                            Data = "Account Create Success"
                        };
                    }
                    else
                    {
                        return new GeneralResponse()
                        {
                            IsPass = false,
                            Data = ModelState
                        };
                    }
                }
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };
        }


        private async Task<GeneralResponse> CreateUser(ApplicationUser user, string password, string role)
        {
            IdentityResult UserCreatedResult = await userManager.CreateAsync(user, password);
            if (UserCreatedResult.Succeeded)
            {
                IdentityResult AddRoleToUserResult = await userManager.AddToRoleAsync(user, role);
                if (AddRoleToUserResult.Succeeded)
                {
                    return new GeneralResponse()
                    {
                        IsPass = true,
                        Data = "Account Create Success"
                    };

                }
                foreach (var error in AddRoleToUserResult.Errors)
                {
                    ModelState.AddModelError("newErrors", error.Description);

                }
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = ModelState
                };

            }
            else
            {
                foreach (var error in UserCreatedResult.Errors)
                {
                    ModelState.AddModelError("newErrors", error.Description);

                }
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = ModelState
                };
            }
        }
        #endregion


        #region Login
        [HttpPost("login")]
        public async Task<ActionResult<GeneralResponse>> Login(LoginDTO loginFromRequest)
        {
            if (ModelState.IsValid)
            {
                if (string.IsNullOrEmpty(loginFromRequest.UserName) && string.IsNullOrEmpty(loginFromRequest.Email))
                {
                    return new GeneralResponse()
                    {
                        IsPass = false,
                        Data = "The Username/Email field is required"
                    };
                }

                ApplicationUser? user = await userManager.FindByNameAsync(loginFromRequest.UserName ?? "");

                if (user == null)
                {
                    user = await userManager.FindByEmailAsync(loginFromRequest.Email);
                }

                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, loginFromRequest.Password);
                    if (found)
                    {
                        var userRoles = await userManager.GetRolesAsync(user);

                        List<Claim> claims = new List<Claim>();

                        claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                        claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                        if (userRoles != null)
                        {
                            foreach (string role in userRoles)
                            {
                                claims.Add(new Claim(ClaimTypes.Role, role));
                            }
                        }

                        SymmetricSecurityKey signinkey = new(Encoding.UTF8.GetBytes(configure["JWT:Key"]));


                        SigningCredentials signingCredentials =
                            new SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken token = new JwtSecurityToken(
                            issuer: configure["JWT:Iss"],
                            audience: configure["JWT:Aud"],
                            expires: DateTime.Now.AddDays(15),
                            claims: claims,
                            signingCredentials: signingCredentials
                            );

                        return new GeneralResponse()
                        {
                            IsPass = true,
                            Data = new
                            {
                                expired = token.ValidTo,
                                token = new JwtSecurityTokenHandler().WriteToken(token)
                            }
                        };
                    }

                }
                ModelState.AddModelError("newError", "Invalid Account");
                return new GeneralResponse()
                {
                    IsPass = false,
                    Data = ModelState
                };
            }
            return new GeneralResponse()
            {
                IsPass = false,
                Data = ModelState
            };

        }
        #endregion


        #region Get Profile
        [HttpGet("Profile/{UserName:regex(^[[A-Za-z0-9]]+$)}")]
        [Authorize]
        public async Task<ActionResult<GeneralResponse>> GetUserInfo(string UserName)
        {
            //if i want user to open his profile only

            //var currentUser = await userManager.GetUserAsync(User);
            //if (currentUser == null || currentUser.UserName != UserName)
            //{
            //    return new GeneralResponse()
            //    {
            //        IsPass = false,
            //        Data = "Unauthorized access or user mismatch"
            //    };
            //}

            ApplicationUser? user = await userManager.FindByNameAsync(UserName ?? "");

            if (user != null)
            {
                return new GeneralResponse()
                {
                    IsPass = true,
                    Data = new GetAccountDTO
                    {
                        UserName = user.UserName,
                        Email = user.Email,
                        PhoneNumber = user.PhoneNumber,

                    }
                };
            }

            return new GeneralResponse()
            {
                IsPass = false,
                Data = "No user found"
            };

        }

        #endregion

    }
}

