using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServicesAbstractions;
using Shared.Authentication;

namespace Presentation.Controllers
{
    public class AuthenticationController(IServiceManager serviceManager):APIController
    {


        //Login (Email,Password)=> UserResponse(string JWT,string Email,string DisplayName)

        [HttpPost("login")]
        public async Task<ActionResult<UserResponse>> Login(LoginRequest request) => Ok(await serviceManager.AuthenticationService.LoginAsync(request));


        //Register (RegisterRequest =>(string Name,Username,Password,DisplayName)) =>UserResponse



        [HttpPost("Register")]
        public async Task<ActionResult<UserResponse>> Register(RegisterRequest Request)=>Ok(await serviceManager.AuthenticationService.RegisterAsync(Request));




        //checkMail(Email)=>bool
        [Authorize]
        [HttpGet("CheckEmail")]
        public async Task<ActionResult<bool>> CheckEmail(string email)
       =>Ok(await serviceManager.AuthenticationService.CheckEmailAsync(email));

        //[Authorize]
        //GetCurrrentUserAddress() &&User&&Update

        [Authorize]
        [HttpGet("Address")]
        public async Task<ActionResult<bool>> GetAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthenticationService.GetUserAddressAsync(email!));
        }
        
        [Authorize]

        [HttpGet("Address")]
        public async Task<ActionResult<bool>> UpdateAddress(AddressDTO addressDTO)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return Ok(await serviceManager.AuthenticationService.UpdateUserAddressAsync(addressDTO,email));
        }


        [Authorize]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);

            return Ok(await serviceManager.AuthenticationService.GetUserByEmail(email));
        }
    }
}
