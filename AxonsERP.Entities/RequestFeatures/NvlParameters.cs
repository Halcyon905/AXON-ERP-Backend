using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AxonsERP.Entities.RequestFeatures
{
    public class NvlParameters
    {
        // NVL(NvlColumn, NvlDefaultValue) = NvlCheckValue
        public string? NvlColumn { get; set; }
        public string? NvlDefaultValue { get; set; }
        public string? NvlCheckValue { get; set; }
    }
}