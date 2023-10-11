using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class LessorImage : ILessorImage
    {
        private IUnitOfWork _unitOfWork;

        public LessorImage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddLessorImage(string LessorCode)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            var lessorImg = new CrMasLessorImage
            {
                CrMasLessorImageCode = lessor.CrMasLessorInformationCode,
            };
            await _unitOfWork.CrMasLessorImage.AddAsync(lessorImg);
            return true;
        }
    }
}
