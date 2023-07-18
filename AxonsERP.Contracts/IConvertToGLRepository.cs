using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Contracts 
{
    public interface IConvertToGLRepository
    {
        IEnumerable<ConvertToGL> GetListConvertToGL();
        ConvertToGL GetSingleConvertToGL(ConvertToGLForGetSingle convertToGLForGetSingle);
    }
}