using AxonsERP.Entities.DataTransferObjects;
using AxonsERP.Entities.Models;
using AxonsERP.Entities.RequestFeatures;

namespace AxonsERP.Contracts 
{
    public interface IConvertToGLRepository
    {
        IEnumerable<ConvertToGL> GetListConvertToGL();
        ConvertToGL GetSingleConvertToGL(ConvertToGLForGetSingle convertToGLForGetSingle);
        IEnumerable<ConvertToGL> SearchConvertToGL(ConvertToGLParameters parameters);
        void CreateConvertToGL(ConvertToGLForCreate convertToGLForCreate);
        void UpdateConvertToGL(ConvertToGLForUpdate convertToGLForUpdate);
    }
}