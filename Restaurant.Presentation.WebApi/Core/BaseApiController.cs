using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Restaurant.Presentation.WebApi.Core;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]/[action]")]
public abstract class BaseApiController : ControllerBase
{
  
}
