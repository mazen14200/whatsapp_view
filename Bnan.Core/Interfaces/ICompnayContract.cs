using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ICompnayContract
    {
        Task<bool> AddCompanyContract(string lessorCode);
        Task<bool> AddCompanyContractAfterDelete(string lessorCode);
        Task<bool> AddCompanyContractCas(string lessorCode, string proceduresCode);
        Task<bool> UpdateCompanyContract(string CompanyContractCode, string Date, string StartDate, string EndDate, string ContractCompanyAnnualFees,
            string ContractCompanyTaxRate, string ContractCompanyDiscountRate, string Activiation);
        Task<bool> UpdateCompanyContractCas(CrMasContractCompany model);

        Task<bool> AddCompanyContractDetailed(string CompanyContractCode, string From, string To, string Value,int serial);
    }
}
