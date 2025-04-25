using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Exceptions;
using Domain.Models.AuthenticationModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ServicesAbstractions;
using Shared.Authentication;

namespace Services
{
    public class AuthenticationService(UserManager<ApplicationUser> UserManager,IOptions<JWTOptions> options,IMapper mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        =>(await UserManager.FindByEmailAsync(email))!=null;  

        public async Task<AddressDTO> GetUserAddressAsync(string email)
        {
            var User = await UserManager.Users.Include(u => u.Address).FirstOrDefaultAsync(e => e.Email == email)
                ?? throw new UserNotFoundException(email);

            if(User.Address is not null)
            {
                return mapper.Map<AddressDTO>(User.Address);
            }

            throw new AddressNotFoundException(User.UserName);
                
        }

        public async Task<UserResponse> GetUserByEmail(string email)
        {


            var User = await UserManager.FindByEmailAsync(email)
                ?? throw new UserNotFoundException(email);


            return new(email,User.DisplayName,User.PasswordHash,await GenerateTokenAsync(User));
        }
        public async Task<AddressDTO> UpdateUserAddressAsync(AddressDTO Address, string email)
        {
            var User = await UserManager.Users.Include(u => u.Address).FirstOrDefaultAsync(e => e.Email == email)
                            ?? throw new UserNotFoundException(email);


            if (User.Address is not null) //Update
            {
                User.Address.FirstName = Address.FirstName;
                User.Address.LastName = Address.LastName;
                User.Address.City = Address.City;
                User.Address.Country = Address.Country;
                User.Address.Street = Address.Street;
            }
            else//Create
            {
                User.Address= mapper.Map<Address>(Address);

            }
            await UserManager.UpdateAsync(User);

            return mapper.Map<AddressDTO>(User.Address);
        }

        public async Task<UserResponse> LoginAsync(LoginRequest request)
        {
//check If User Exist
var User= await UserManager.FindByEmailAsync(request.Email)??throw new UserNotFoundException(request.Email);
//
        var Isvalid=await UserManager.CheckPasswordAsync(User,request.Password);
            if (Isvalid) return new(request.Email,request.Password,request.DisplayName,"JWT");

            throw new UnauthorizedException();
//

        }

        public async Task<UserResponse> RegisterAsync(RegisterRequest request)
        {
            var User = new ApplicationUser
            {
                Email = request.Email,
                DisplayName = request.DisplayName,
                PhoneNumber = request.PhoneNumber,
                UserName = request.UserName

            };

            var Result = await UserManager.CreateAsync(User, request.Password);
            if (Result.Succeeded) return new(request.Email, request.Password, request.DisplayName,await GenerateTokenAsync(User));
            var errors=Result.Errors.Select(e=>e.Description).ToList();
            throw new BadRequestException(errors);



        }


        private  async Task<string> GenerateTokenAsync(ApplicationUser user)
        {
            var jwt = options.Value;
                var Claims=new List<Claim>()
                { 
                new(ClaimTypes.Email,user.Email!),
                new(ClaimTypes.Name,user.UserName!),
                
                };
                var Roles=await UserManager.GetRolesAsync(user);
            foreach (var item in Roles)
            {
                Claims.Add(new(ClaimTypes.Role, item));

            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));


            var cred=new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer:jwt.Issuer,
                audience : jwt.Audience,
                claims:Claims,
                expires:DateTime.UtcNow.AddDays(jwt.DurationDays),
                signingCredentials:cred
                );
            var TokenHandler = new JwtSecurityTokenHandler();
            return TokenHandler.WriteToken(token);
           
        }

    }
}
