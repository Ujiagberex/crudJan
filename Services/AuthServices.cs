using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using WebApiClass.DTO;
using WebApiClass.IServices;
using WebApiClass.Model;
using static WebApiClass.DTO.Responses;

namespace WebApiClass.Services
{
    public class AuthServices : IAuth
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole>  roleManager;
        private readonly IConfiguration configuration;
        private readonly IMapper mapper;

        public AuthServices(IConfiguration configuration, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            this.configuration = configuration;
            this.roleManager = roleManager;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<GeneralResponse> CreateUser(RegisterDTO registerDTO)
        {
            if(registerDTO == null)
            {
                return new GeneralResponse(false, "Cannot be null/empty");
            }

            //Mapping
            var newUser = new ApplicationUser()
            {
                fullname = registerDTO.FullName,
                Email = registerDTO.EmailAddress,
                PasswordHash = registerDTO.Password,
                UserName = registerDTO.EmailAddress
            };
            //var newUser = mapper.Map<ApplicationUser>(registerDTO);

            var user = await userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
            {
                return new GeneralResponse(false, "This Email already exists"); 
            }

            var createUser = await userManager.CreateAsync(newUser!, registerDTO.Password);
            if (!createUser.Succeeded)
            {
                return new GeneralResponse(false, "An error occured");
            }

            var checkAdmin = await roleManager.FindByNameAsync("Admin");
            if(checkAdmin == null)
            {
                await roleManager.CreateAsync(new IdentityRole() {Name = "Admin"});
                await userManager.AddToRoleAsync(newUser, "Admin");
                return new GeneralResponse(true, "Admin account created");
            }
            else
            {
                var checkUser = await roleManager.FindByNameAsync("User");
                if(checkUser == null)
                {
                    await roleManager.CreateAsync(new IdentityRole() { Name = "User" });
                    await userManager.AddToRoleAsync(newUser, "User");
                    return new GeneralResponse(true, "User account successfully created");
                }
            }

            return new GeneralResponse(false, "Could not create an account");

        }

        public async Task<LogInResponse> LogInUser(LogInDTO logInDTO)
        {
            if (logInDTO == null)
            {
                return new LogInResponse(false, null, "This model cannot be empty");     
            }

            var getUser = await userManager.FindByEmailAsync(logInDTO.EmailAddress);
            if (getUser == null)
            {
                return new LogInResponse(false, null, "This email wasn't found");
            }

            bool isPasswordConfirmed = await userManager.CheckPasswordAsync(getUser, logInDTO.Password);
            if (!isPasswordConfirmed)
            {
                return new LogInResponse(false, null, "Wrong password");
            }

            var getUserRole = await userManager.GetRolesAsync(getUser);
            var userSession = new UserSession(getUser.Id, getUser.fullname, getUser.Email, getUserRole.First());
            string token = GenerateToken(userSession);

            return new LogInResponse(true, token, "Login Successful");

        }

        private string GenerateToken(UserSession user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var userClaims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.id),
                new Claim(ClaimTypes.Name, user.fullname),
                new Claim(ClaimTypes.Email, user.email),
                new Claim(ClaimTypes.Role, user.role)          
            };

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:Issuer"],
                audience: configuration["JWT:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(14),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
