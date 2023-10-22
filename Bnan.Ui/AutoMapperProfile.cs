using AutoMapper;
using Bnan.Core.Models;
using Bnan.Ui.ViewModels;
using Bnan.Ui.ViewModels.CAS;
using Bnan.Ui.ViewModels.Identitiy;
using Bnan.Ui.ViewModels.MAS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure
{
    public class AutoMapperProfile : Profile
    {

        public AutoMapperProfile()
        {
            CreateMap<CrMasLessorInformationVM, CrMasLessorInformation>();
            CreateMap<CrMasLessorInformation, CrMasLessorInformationVM>().ForMember(x => x.CrMasLessorInformationGovernmentNo, opt => opt.MapFrom(y => y.CrMasLessorInformationGovernmentNo.Trim()))
                                                                         .ForMember(x => x.CrMasLessorInformationTaxNo, opt => opt.MapFrom(y => y.CrMasLessorInformationTaxNo.Trim()))
                                                                         .ForMember(x => x.CrMasLessorInformationCommunicationMobile, opt => opt.MapFrom(y => y.CrMasLessorInformationCommunicationMobile.Trim()))
                                                                         .ForMember(x => x.CrMasLessorInformationCallFree, opt => opt.MapFrom(y => y.CrMasLessorInformationCallFree.Trim()))
                                                                         .ForMember(x => x.CrMasLessorInformationCommunicationMobile, opt => opt.MapFrom(y => y.CrMasLessorInformationCommunicationMobile.Trim()))
                                                                         .ForMember(x => x.CrMasLessorInformationTwiter, opt => opt.MapFrom(y => y.CrMasLessorInformationTwiter.Trim()));
            CreateMap<RegisterViewModel, CrMasUserInformation>();
            CreateMap<CrMasUserInformation, RegisterViewModel>();

            CreateMap<CrMasSysProcedure, CrMasSysProcedureVM>();
            CreateMap<CrMasSysProcedureVM, CrMasSysProcedure>();

            CreateMap<CrMasLessorImage, LessorImagesVM>();
            CreateMap<LessorImagesVM, CrMasLessorImage>();

            CreateMap<BranchPostVM, CrCasBranchPost>();
            CreateMap<CrCasBranchPost, BranchPostVM>();
            CreateMap<CrMasContractCompany, ContractCompanyVM>();
            CreateMap<ContractCompanyVM, CrMasContractCompany>();
            CreateMap<BankVM, CrMasSupAccountBank>();
            CreateMap<CrMasSupAccountBank, BankVM>();
            CreateMap<PaymentMethodsVM,CrMasSupAccountPaymentMethod >();
            CreateMap<CrMasSupAccountPaymentMethod, PaymentMethodsVM>();

            CreateMap<AccountRefrenceVM, CrMasSupAccountReference>();
            CreateMap<CrMasSupAccountReference, AccountRefrenceVM>();

            CreateMap<ContractAdditionalVM, CrMasSupContractAdditional>();
            CreateMap<CrMasSupContractAdditional, ContractAdditionalVM>();

            CreateMap<ContractOptionsVM, CrMasSupContractOption>();
            CreateMap<CrMasSupContractOption, ContractOptionsVM>();

            CreateMap<PostCityVM, CrMasSupPostCity>();
            CreateMap<CrMasSupPostCity, PostCityVM>();

            CreateMap<CrMasSupCarDistributionVM, CrMasSupCarDistribution>();
            CreateMap<CrMasSupCarDistribution, CrMasSupCarDistributionVM>();

            CreateMap<CarCheckupVM, CrMasSupContractCarCheckup>();
            CreateMap<CrMasSupContractCarCheckup, CarCheckupVM>();

            CreateMap<PostRegionVM, CrMasSupPostRegion>();
            CreateMap<CrMasSupPostRegion, PostRegionVM>();

            CreateMap<AccountBankVM, CrCasAccountBank>();
            CreateMap<CrCasAccountBank, AccountBankVM>();

            CreateMap<RenterDriverVM, CrCasRenterPrivateDriverInformation>();
            CreateMap<CrCasRenterPrivateDriverInformation, RenterDriverVM>();

            CreateMap<CrCasBranchInformation, BranchVM>();
            CreateMap<BranchVM, CrCasBranchInformation>();

            CreateMap<BranchPost1VM, CrCasBranchPost>();
            CreateMap<CrCasBranchPost, BranchPost1VM>();
            CreateMap<ContractValiditionsVM, CrMasUserContractValidity>();
            CreateMap<CrMasUserContractValidity, ContractValiditionsVM>();

            CreateMap<CrCasBranchPost, BranchPost1VM>();  
            
            CreateMap<BranchDocumentVM, CrCasBranchDocument>();
            CreateMap<CrCasBranchDocument, BranchDocumentVM>();
            CreateMap<CrCasAccountSalesPoint, SalesPointsVM>();
            CreateMap<SalesPointsVM, CrCasAccountSalesPoint>();

            CreateMap<CrCasLessorMechanism, MechanismVM>();
            CreateMap<MechanismVM, CrCasLessorMechanism>();
            CreateMap<CrCasOwner, OwnersVM>();
            CreateMap<OwnersVM, CrCasOwner>();
            CreateMap<AdminstritiveProceduresVM, CrCasSysAdministrativeProcedure>();
            CreateMap<CrCasSysAdministrativeProcedure, AdminstritiveProceduresVM>();
            CreateMap<CarsInforamtionVM, CrCasCarInformation>();
            CreateMap<CrCasCarInformation, CarsInforamtionVM>();
            CreateMap<DocumentsMaintainceCarVM, CrCasCarDocumentsMaintenance>();
            CreateMap<CrCasCarDocumentsMaintenance, DocumentsMaintainceCarVM>();

        }

    }
}
