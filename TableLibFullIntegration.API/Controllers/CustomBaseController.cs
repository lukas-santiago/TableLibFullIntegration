using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TableLibFullIntegration.API.Controllers
{
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        internal readonly DbConnection connection;
        public CustomBaseController(DbConnection connection)
        {
            this.connection = connection;
        }
    }
}