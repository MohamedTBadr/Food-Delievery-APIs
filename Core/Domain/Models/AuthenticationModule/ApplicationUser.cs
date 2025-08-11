using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models.AuthenticationModule;
    public class ApplicationUser:IdentityUser
    {


        public string DisplayName { get; set; } = default!;

    //FirsttName,LastName,Citt,street,Country
    public Address Address { get; set; }    


    }




