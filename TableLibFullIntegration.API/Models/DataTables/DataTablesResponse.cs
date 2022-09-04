using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TableLibFullIntegration.API.Models.DataTables
{
    public class DataTablesResponse
    {
        /// <summary>
        /// Table's data
        /// </summary>
        /// <value></value>
        public IEnumerable<object>? data { get; set; }
        /// <summary>
        /// Integer value to sync requests along the session
        /// </summary>
        /// <value></value>
        public int draw { get; set; }
        /// <summary>
        /// Count of records of the dataset
        /// </summary>
        /// <value></value>
        public int recordsTotal { get; set; }
        /// <summary>
        /// Count of filtered records of the dataset
        /// </summary>
        /// <value></value>
        public int recordsFiltered { get; set; }
    }
}