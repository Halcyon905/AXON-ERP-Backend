using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Service.Contracts 
{
    public interface IConvertToGLService
    {
        IEnumerable<ConvertToGL> GetListConvertToGL();
        ConvertToGL GetSingleConvertToGL(ConvertToGLForGetSingle convertToGLForGetSingle);
        IEnumerable<ConvertToGL> SearchConvertToGL(ConvertToGLParameters parameters);
        ConvertToGLForGetSingle CreateConvertToGL(ConvertToGLForCreate convertToGLForCreate);
        void UpdateConvertToGL(ConvertToGLForUpdate convertToGLForUpdate);
    }
}