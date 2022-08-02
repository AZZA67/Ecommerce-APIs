using Lab.DTO;
using Lab.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Lab.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration _configuration;
        EntityContext context;
        private readonly SignInManager<ApplicationUser> signinmanager;
        private readonly IHttpContextAccessor httpContextAccessor;

        public AccountController(IHttpContextAccessor httpContextAccessor,SignInManager<ApplicationUser> signinmanager,UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, EntityContext context)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            this.context = context;
            this.signinmanager = signinmanager;
        }
        //List<Claim> Claims = new List<Claim>();
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                //HttpContext.User =  SignInManager(user);
                //this.Claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                //this.Claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                //this.Claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
               
              await  userManager.AddClaimAsync(user, new Claim(ClaimTypes.NameIdentifier, user.Id));
                await userManager.AddClaimAsync(user, new Claim(ClaimTypes.Name, user.UserName));
                await userManager.AddClaimAsync(user,new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
                var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }


              

                var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecrtKey"]));

                var token = new JwtSecurityToken(
                    issuer: _configuration["JWT:ValidIssuer"],
                    audience: _configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );
                //getuser( authClaims);

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    authClaims = authClaims[2].Value

                }) ;
            }
            return Unauthorized();
        }
        //private async Task<ApplicationUser> GetCurrentUserAsync() => userManager.GetUserAsync(HttpContext.User);
        [HttpGet]
        [Route("getuser")]
        public  IActionResult getuser()
        {
            //var email = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(c => c.Type == "jti")?.Value;
            //var userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
             string LoggedInUser = User.Identity.Name;
            return Ok(LoggedInUser);

          
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, "User already exist.");

            ApplicationUser user = new ApplicationUser()
            {
                SecurityStamp = Guid.NewGuid().ToString(),
                NationalID = model.NationalID,
                MobileNo = model.Mobileno,
                Address = model.Address,
                Email = model.Email,
                UserName = model.Username,
            

            };
          
            var result = await userManager.CreateAsync(user, model.Password);
            if (!await roleManager.RoleExistsAsync(User_Roles.User))
                await roleManager.CreateAsync(new IdentityRole(User_Roles.User));
            if (await roleManager.RoleExistsAsync(User_Roles.User))
            {
                
                    await userManager.AddToRoleAsync(user, User_Roles.User);
                
            }
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again." );

            return Ok( "User created successfully!" );
        }

        [HttpPost]
        [Route("register-admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, "User already exist.");

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest("bad request");
            if (!result.Succeeded)
                return BadRequest("bad request");

            if (!await roleManager.RoleExistsAsync(User_Roles.Admin))
                await roleManager.CreateAsync(new IdentityRole(User_Roles.Admin));
            if (!await roleManager.RoleExistsAsync(User_Roles.User))
                await roleManager.CreateAsync(new IdentityRole(User_Roles.User));

            if (await roleManager.RoleExistsAsync(User_Roles.Admin))
            {
                await userManager.AddToRoleAsync(user, User_Roles.Admin);
            }

            return Ok( "User created successfully!" );
        }
        [HttpPost]
        [Route("register-Seller")]
        public async Task<IActionResult> RegisterSeller([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, "User already exist.");

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, "User creation failed! Please check user details and try again.");

            if (!await roleManager.RoleExistsAsync(User_Roles.Seller))
                await roleManager.CreateAsync(new IdentityRole(User_Roles.Seller));
            if (await roleManager.RoleExistsAsync(User_Roles.Seller))
            {
                await userManager.AddToRoleAsync(user, User_Roles.Seller);
            }

            return Ok("User created successfully!");
        }
        //var testb = User.Identity.GetUserId();




        // [NonAction]
        //public async Task<JwtSecurityToken> GenerateToke(ApplicationUser userModel)
        //{
        //    var claims = new List<Claim>();
        //    claims.Add(new Claim("intakeNo", "42"));//Custom
        //    claims.Add(new Claim(ClaimTypes.Name, userModel.UserName));
        //    claims.Add(new Claim(ClaimTypes.NameIdentifier, userModel.Id));
        //    var roles = await userManager.GetRolesAsync(userModel);
        //    foreach (var role in roles)
        //    {
        //        claims.Add(new Claim(ClaimTypes.Role, role));
        //    }
        //    //Jti "Identifier Token
        //    claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
        //    //---------------------------------(: Token :)--------------------------------------
        //    var key =
        //        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecrtKey"]));
        //    var mytoken = new JwtSecurityToken(
        //        audience: _configuration["JWT:ValidAudience"],
        //        issuer: _configuration["JWT:ValidIssuer"],
        //        expires: DateTime.Now.AddHours(1),
        //        claims: claims,
        //        signingCredentials:
        //               new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        //        );
        //    return mytoken;
        //}
    }
}

