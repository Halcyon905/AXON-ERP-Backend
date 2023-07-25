using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Contracts 
{
    public interface ILinkOperationRepository
    {
        IEnumerable<LinkOperation> GetListLinkOperation(string mainColumn);
        IEnumerable<LinkOperation> SearchLinkOperation(LinkOperationParameters parameters);
    }
}