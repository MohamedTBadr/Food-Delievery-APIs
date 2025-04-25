using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.Authentication;

namespace ServicesAbstractions
{
    public interface IAuthenticationService
    {

        //login
        Task<UserResponse>  LoginAsync(LoginRequest request);



        //register

        Task<UserResponse> RegisterAsync(RegisterRequest request);




        Task<bool> CheckEmailAsync(string email);



        Task<AddressDTO> GetUserAddressAsync(string email);
        Task<AddressDTO> UpdateUserAddressAsync(AddressDTO Address,string email);


        Task<UserResponse> GetUserByEmail(string email);

    }
}
