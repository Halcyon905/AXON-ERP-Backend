using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxonsERP.Entities.RequestFeatures
{
    public class MultiJoin
    {
        public string columnForJoin { get; set; } = string.Empty;
        public string columnForSplitOn { get; set; } = string.Empty;
        public string queryTableJoin { get; set; } = string.Empty;
        public string propNameJoin { get; set; } = string.Empty;
        public string columnForDefaultWhere { get; set; } = string.Empty;
        public List<Type>? types { get; set; }
    }
}
