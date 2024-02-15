using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Bnan.Inferastructure.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class CompnayContract : ICompnayContract
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompnayContract(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCompanyContract(string lessorCode)
        {
            var sysProcedures = _unitOfWork.CrMasSysProcedure.FindAll(l => l.CrMasSysProceduresStatus == "A" && l.CrMasSysProceduresClassification == "11");
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(lessorCode);
            if (lessor != null)
            {
                foreach (var item in sysProcedures)
                {
                    string Year = int.Parse(DateTime.Now.ToString("yy")).ToString();
                    string sector = "3";
                    string procdureType = item.CrMasSysProceduresCode.ToString();
                    string lessorCodeForEx = lessor.CrMasLessorInformationCode;
                    string Branch = "100";
                    string? id = _unitOfWork.CrMasContractCompany.GetAll().LastOrDefault(l => l.CrMasContractCompanyLessor == lessor.CrMasLessorInformationCode)?.CrMasContractCompanyNo.Split("-")[3];

                    CrMasContractCompany contractCompany = new CrMasContractCompany
                    {
                        CrMasContractCompanyNo = IncrementString.IncrementStringExtension(Year, sector, procdureType, lessorCodeForEx, Branch, id),
                        CrMasContractCompanyStatus = "N",
                        CrMasContractCompanyYear = int.Parse(DateTime.Now.ToString("yy")).ToString(),
                        CrMasContractCompanyProceduresClassification = "11",
                        CrMasContractCompanySector = sector,
                        CrMasContractCompanyProcedures = procdureType,
                        CrMasContractCompanyLessor = lessorCodeForEx,
                        CrMasContractCompanyTaxRate = 0,
                        CrMasContractCompanyAnnualFees = 0,
                        CrMasContractCompanyDiscountRate = 0,
                    };

                    await _unitOfWork.CrMasContractCompany.AddAsync(contractCompany);
                }
            }

            return true;
        }

        public async Task<bool> AddCompanyContractCas(string lessorCode, string proceduresCode)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(lessorCode);
            if (lessor != null)
            {
                string Year = int.Parse(DateTime.Now.ToString("yy")).ToString();
                string sector = "3";
                string procdureType = proceduresCode;
                string lessorCodeForEx = lessor.CrMasLessorInformationCode;
                string Branch = "100";
                string? id = _unitOfWork.CrMasContractCompany.GetAll().LastOrDefault(l => l.CrMasContractCompanyLessor == lessor.CrMasLessorInformationCode && l.CrMasContractCompanyProcedures == proceduresCode)?.CrMasContractCompanyNo.Split("-")[3];

                CrMasContractCompany contractCompany = new CrMasContractCompany
                {
                    CrMasContractCompanyNo = IncrementString.IncrementStringExtension(Year, sector, procdureType, lessorCodeForEx, Branch, id),
                    CrMasContractCompanyStatus = "N",
                    CrMasContractCompanyYear = int.Parse(DateTime.Now.ToString("yy")).ToString(),
                    CrMasContractCompanyProceduresClassification = "11",
                    CrMasContractCompanySector = sector,
                    CrMasContractCompanyProcedures = procdureType,
                    CrMasContractCompanyLessor = lessorCodeForEx,
                };

                await _unitOfWork.CrMasContractCompany.AddAsync(contractCompany);

            }

            return true;
        }



        public async Task<bool> UpdateCompanyContract(string CompanyContractCode, string Date, string StartDate, string EndDate, string ContractCompanyAnnualFees,
            string ContractCompanyTaxRate, string ContractCompanyDiscountRate, string Activiation)
        {
            var companyContract = await _unitOfWork.CrMasContractCompany.FindAsync(x => x.CrMasContractCompanyNo == CompanyContractCode);
            var mecanizm = await _unitOfWork.CrCasLessorMechanism.FindAsync(x => x.CrCasLessorMechanismProcedures == "112");
            double aboutExp = (double)mecanizm.CrCasLessorMechanismDaysAlertAboutExpire;
            companyContract.CrMasContractCompanyDate = DateTime.Parse(Date).Date;
            companyContract.CrMasContractCompanyStartDate = DateTime.Parse(StartDate).Date;
            companyContract.CrMasContractCompanyEndDate = DateTime.Parse(EndDate).Date;
            companyContract.CrMasContractCompanyAnnualFees = decimal.Parse(ContractCompanyAnnualFees, CultureInfo.InvariantCulture);
            companyContract.CrMasContractCompanyDiscountRate = decimal.Parse(ContractCompanyDiscountRate, CultureInfo.InvariantCulture);
            companyContract.CrMasContractCompanyAboutToExpire = DateTime.Parse(EndDate).AddDays(-aboutExp).Date;
            companyContract.CrMasContractCompanyStatus = "A";
            companyContract.CrMasContractCompanyActivation = Activiation;
            companyContract.CrMasContractCompanyTaxRate = decimal.Parse(ContractCompanyTaxRate, CultureInfo.InvariantCulture);
            companyContract.CrMasContractCompanyNumber = companyContract.CrMasContractCompanyNo;
            _unitOfWork.CrMasContractCompany.Update(companyContract);
            return true;
        }
        public async Task<bool> UpdateCompanyContractCas(CrMasContractCompany model)
        {
            var companyContract = _unitOfWork.CrMasContractCompany.Find(x => x.CrMasContractCompanyNo == model.CrMasContractCompanyNo);
            var mecanizm = _unitOfWork.CrCasLessorMechanism.Find(x => x.CrCasLessorMechanismProcedures == "112");
            double aboutExp = (double)mecanizm.CrCasLessorMechanismDaysAlertAboutExpire;
            companyContract.CrMasContractCompanyDate = model.CrMasContractCompanyDate;
            companyContract.CrMasContractCompanyStartDate = model.CrMasContractCompanyStartDate?.Date;
            companyContract.CrMasContractCompanyEndDate = model.CrMasContractCompanyEndDate?.Date;
            companyContract.CrMasContractCompanyAboutToExpire = model.CrMasContractCompanyEndDate?.AddDays(-aboutExp).Date;
            companyContract.CrMasContractCompanyUserPassword = model.CrMasContractCompanyUserPassword;
            companyContract.CrMasContractCompanyUserId = model.CrMasContractCompanyUserPassword;
            companyContract.CrMasContractCompanyNumber = model.CrMasContractCompanyNumber;
            companyContract.CrMasContractCompanyReasons = model.CrMasContractCompanyReasons;
            companyContract.CrMasContractCompanyImage = model.CrMasContractCompanyImage;
            companyContract.CrMasContractCompanyStatus = "A";
            _unitOfWork.CrMasContractCompany.Update(companyContract);
            await _unitOfWork.CompleteAsync();

            return true;
        }

        public async Task<bool> AddCompanyContractDetailed(string CompanyContractCode, string From, string To, string Value, int serial)
        {
            var contractCount = 0;
            CrMasContractCompanyDetailed crMasContractCompanyDetailed = new CrMasContractCompanyDetailed();
            crMasContractCompanyDetailed.CrMasContractCompanyDetailedNo = CompanyContractCode;
            // to get last record to auto increament
            var contractDetailedNo = _unitOfWork.CrMasContractCompanyDetailed.FindAll(x => x.CrMasContractCompanyDetailedNo == CompanyContractCode).Count();
            if (contractDetailedNo == 0)
            {
                contractCount = serial;
            }
            else
            {
                var lastContract = _unitOfWork.CrMasContractCompanyDetailed.GetAll().OrderByDescending(x => x.CrMasContractCompanyDetailedSer).FirstOrDefault(x => x.CrMasContractCompanyDetailedNo != null);
                contractCount = lastContract != null ? lastContract.CrMasContractCompanyDetailedSer + serial : 1;
            }
            crMasContractCompanyDetailed.CrMasContractCompanyDetailedSer = contractCount;
            crMasContractCompanyDetailed.CrMasContractCompanyDetailedFromPrice = decimal.Parse(From, CultureInfo.InvariantCulture);
            crMasContractCompanyDetailed.CrMasContractCompanyDetailedToPrice = decimal.Parse(To, CultureInfo.InvariantCulture);
            crMasContractCompanyDetailed.CrMasContractCompanyDetailedValue = decimal.Parse(Value, CultureInfo.InvariantCulture);
            await _unitOfWork.CrMasContractCompanyDetailed.AddAsync(crMasContractCompanyDetailed);

            return true;
        }

        public async Task<bool> AddCompanyContractAfterDelete(string lessorCode)
        {
            var sysProcedures = _unitOfWork.CrMasSysProcedure.Find(l => l.CrMasSysProceduresStatus == "A" && l.CrMasSysProceduresClassification == "11" && l.CrMasSysProceduresCode == "112");
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(lessorCode);
            if (lessor != null)
            {
                string Year = int.Parse(DateTime.Now.ToString("yy")).ToString();
                string sector = "3";
                string procdureType = sysProcedures.CrMasSysProceduresCode.ToString();
                string lessorCodeForEx = lessor.CrMasLessorInformationCode;
                string Branch = "100";
                string? id = _unitOfWork.CrMasContractCompany.GetAll().LastOrDefault(l => l.CrMasContractCompanyLessor == lessor.CrMasLessorInformationCode)?.CrMasContractCompanyNo.Split("-")[3];

                CrMasContractCompany contractCompany = new CrMasContractCompany
                {
                    CrMasContractCompanyNo = IncrementString.IncrementStringExtension(Year, sector, procdureType, lessorCodeForEx, Branch, id),
                    CrMasContractCompanyStatus = "N",
                    CrMasContractCompanyYear = int.Parse(DateTime.Now.ToString("yy")).ToString(),
                    CrMasContractCompanyProceduresClassification = "11",
                    CrMasContractCompanySector = sector,
                    CrMasContractCompanyProcedures = procdureType,
                    CrMasContractCompanyLessor = lessorCodeForEx,
                    CrMasContractCompanyTaxRate = 0,
                    CrMasContractCompanyAnnualFees = 0,
                    CrMasContractCompanyDiscountRate = 0,
                };

                await _unitOfWork.CrMasContractCompany.AddAsync(contractCompany);
            }
            return true;
        }
    }
}
