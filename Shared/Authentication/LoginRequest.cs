﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Authentication
{
    public record LoginRequest([EmailAddress]string Email,string Password, string DisplayName, string Token);
    
}
