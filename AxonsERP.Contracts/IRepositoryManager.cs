namespace AxonsERP.Contracts
{
    public interface IRepositoryManager
    {
        IApplicationCtrlRepository ApplicationCtrl { get; }
        ITaxRateControlRepository TaxRateControl { get; }
        IGeneralDescRepository GeneralDescRepository { get; }
        IBillCollectionDateRepository BillCollectionDateRepository { get; }
        ICVDescRepository CVDescRepository { get; }
        ICreditControlRepository CreditControlRepository { get; }
        IConvertToGLRepository ConvertToGLRepository { get; }
        ICompanyAccChartRepository CompanyAccChartRepository { get; }
        ITRNDescRepository TRNDescRepository { get; }
        ICompanyRepository CompanyRepository { get; }

        void Commit();
    }
}
