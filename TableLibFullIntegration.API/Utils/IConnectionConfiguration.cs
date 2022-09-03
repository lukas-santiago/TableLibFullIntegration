using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableLibFullIntegration.API.Utils
{
    public interface IConnectionConfiguration
    {
        public string ConnectionString { get; }
    }
}