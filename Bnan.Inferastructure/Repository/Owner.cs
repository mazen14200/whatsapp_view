using Bnan.Core.Extensions;
using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class Owner : IOwner
    {
        private IUnitOfWork _unitOfWork;

        public Owner(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddOwner(string LessorCode)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            var lessorOwner = new CrCasOwner
            {
                CrCasOwnersCode = lessor.CrMasLessorInformationGovernmentNo,
                CrCasOwnersLessorCode = lessor.CrMasLessorInformationCode,
                CrCasOwnersSector = "2",
                CrCasOwnersArName = lessor.CrMasLessorInformationArLongName,
                CrCasOwnersEnName = lessor.CrMasLessorInformationEnLongName,
                CrCasOwnersStatus = Status.Acive
            };
            await _unitOfWork.CrCasOwner.AddAsync(lessorOwner);
            return true;
        }

        public async Task<bool> AddOwnerInCas(CrCasOwner model)
        {

            CrCasOwner crCasOwner = new CrCasOwner()
            {
                CrCasOwnersCode=model.CrCasOwnersCode,
                CrCasOwnersCommercialNo=model.CrCasOwnersCommercialNo,
                CrCasOwnersLessorCode = model.CrCasOwnersLessorCode,
                CrCasOwnersArName = model.CrCasOwnersArName,
                CrCasOwnersEnName=model.CrCasOwnersEnName,
                CrCasOwnersStatus=Status.Acive,
                CrCasOwnersSector = "2",
                CrCasOwnersReasons= model.CrCasOwnersReasons
            };
            await _unitOfWork.CrCasOwner.AddAsync(crCasOwner);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> UpdateOwnerInCas(CrCasOwner model)
        {

            var crCasOwner = await _unitOfWork.CrCasOwner.FindAsync(x => x.CrCasOwnersCode == model.CrCasOwnersCode);
            crCasOwner.CrCasOwnersCommercialNo = model.CrCasOwnersCommercialNo;
            crCasOwner.CrCasOwnersArName = model.CrCasOwnersArName;
            crCasOwner.CrCasOwnersEnName = model.CrCasOwnersEnName;
            crCasOwner.CrCasOwnersReasons = model.CrCasOwnersReasons;
            
            _unitOfWork.CrCasOwner.Update(crCasOwner);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
