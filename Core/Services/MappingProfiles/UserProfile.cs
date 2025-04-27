using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Models.AuthenticationModule;
using Shared.Authentication;

namespace Services.MappingProfiles
{
    public class UserProfile:Profile
    {

        public UserProfile()
        {
            CreateMap<AddressDTO, Address>().ReverseMap();
        }

    }
}
