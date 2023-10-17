using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenric<CrMasSysMainTask> CrMasSysMainTasks { get; }
        public IGenric<CrMasSysSubTask> CrMasSysSubTasks { get; }
        public IGenric<CrMasUserLogin> CrMasUserLogins { get; }
        public IGenric<CrMasSysSystem> CrMasSysSystems { get; }
        public IGenric<CrMasLessorInformation> CrMasLessorInformation { get; }
        public IGenric<CrMasUserMainValidation> CrMasUserMainValidations { get; }
        public IGenric<CrMasUserSubValidation> CrMasUserSubValidations { get; }
        public IGenric<CrMasUserProceduresValidation> CrMasUserProceduresValidations { get; }
        public IGenric<CrMasSysProcedure> CrMasSysProcedure { get; }
        public IGenric<CrMasSysProceduresTask> CrMasSysProceduresTasks { get; }
        public IGenric<CrMasSysCallingKey> CrMasSysCallingKeys { get; }
        public IGenric<CrMasLessorImage> CrMasLessorImage { get; }
        public IGenric<CrCasOwner> CrCasOwner { get; }
        public IGenric<CrCasBeneficiary> CrCasBeneficiary { get; }
        public IGenric<CrCasLessorMembership> CrCasLessorMembership { get; }
        public IGenric<CrMasSupRenterMembership> CrMasSupRenterMembership { get; }
        public IGenric<CrCasLessorMechanism> CrCasLessorMechanism { get; }
        public IGenric<CrMasContractCompany> CrMasContractCompany { get; }
        public IGenric<CrCasBranchInformation> CrCasBranchInformation { get; }
        public IGenric<CrCasBranchDocument> CrCasBranchDocument { get; }
        public IGenric<CrMasSupPostCity> CrMasSupPostCity { get; }
        public IGenric<CrCasBranchPost> CrCasBranchPost { get; }
        public IGenric<CrCasAccountBank> CrCasAccountBank { get; }
        public IGenric<CrCasAccountSalesPoint> CrCasAccountSalesPoint { get; }
        public IGenric<CrMasSupAccountBank> CrMasSupAccountBanks { get; }
        public IGenric<CrCasLessorClassification> CrCasLessorClassification { get; }
        public IGenric<CrMasSysMainTask> CrMasSysMainTask { get; }
        public IGenric<CrMasSysSubTask> CrMasSysSubTask { get; }
        public IGenric<CrMasContractCompanyDetailed> CrMasContractCompanyDetailed { get; }
        public IGenric<CrMasSupCarDistribution> CrMasSupCarDistribution { get; }
        public IGenric<CrMasSupAccountPaymentMethod> CrMasSupAccountPaymentMethod { get; }
        public IGenric<CrMasSupAccountReference> CrMasSupAccountReference { get; }
        public IGenric<CrMasSupCarCategory> CrMasSupCarCategory { get; }
        public IGenric<CrMasSupCarModel> CrMasSupCarModel { get; }
        public IGenric<CrMasUserInformation> CrMasUserInformation { get; }
        public IGenric<CrMasSupContractAdditional> CrMasSupContractAdditional { get; }
        public IGenric<CrMasSupContractOption> CrMasSupContractOption { get; }
        public IGenric<CrMasSupContractCarCheckup> CrMasSupContractCarCheckup { get; }
        public IGenric<CrMasUserBranchValidity> CrMasUserBranchValidity { get; }

        public IGenric<CrMasSupPostRegion> CrMasSupPostRegion { get; }

        public IGenric<CrCasRenterPrivateDriverInformation> CrCasRenterPrivateDriverInformation { get; }
        public IGenric<CrMasSupRenterDrivingLicense> CrMasSupRenterDrivingLicense { get; }

        public IGenric<CrMasSupRenterIdtype> CrMasSupRenterIdtype { get; }

        public IGenric<CrMasSupRenterGender> CrMasSupRenterGender { get; }

        public IGenric<CrMasSupRenterNationality> CrMasSupRenterNationality { get; }
        public IGenric<CrMasUserContractValidity> CrMasUserContractValidity { get; }
        public IGenric<CrCasSysAdministrativeProcedure> CrCasSysAdministrativeProcedure { get; }
        public IGenric<CrCasCarInformation> CrCasCarInformation { get; }
        public IGenric<CrMasSupCarRegistration> CrMasSupCarRegistration { get; }
        public IGenric<CrMasSupCarFuel> CrMasSupCarFuel { get; }
        public IGenric<CrMasSupCarCvt> CrMasSupCarCvt { get; }
        public IGenric<CrMasSupCarColor> CrMasSupCarColor { get; }




        int Complete();
        Task<int> CompleteAsync();
    }
}
