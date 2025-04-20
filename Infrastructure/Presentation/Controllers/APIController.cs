using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public abstract class APIController:ControllerBase
    {

    }
}
