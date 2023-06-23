using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AxonsERP.Entities.ConfigurationModels
{
    public interface IUserProvider
    {
        int UserId { get; set; }
        int BusinessUnitId { get; set; }
        List<int> RoleIds { get; set; }
        int LanguageId { get; set; }
    }

    public class UserProvider : IUserProvider
    {
        public int UserId { get; set; }
        public int BusinessUnitId { get; set; }
        public List<int> RoleIds { get; set; } = new List<int>();
        public int LanguageId { get; set; }
    }
}
