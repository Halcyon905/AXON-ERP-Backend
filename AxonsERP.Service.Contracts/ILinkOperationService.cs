using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface ILinkOperationService
    {
        IEnumerable<LinkOperation> GetListLinkOperation(string mainColumn);
        IEnumerable<LinkOperation> SearchLinkOperation(LinkOperationParameters parameters);
    }
}