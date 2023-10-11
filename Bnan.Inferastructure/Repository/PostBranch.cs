using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class PostBranch : IPostBranch
    {
        private readonly IUnitOfWork _unitOfWork;
        public PostBranch(IUnitOfWork unitOfWork, BnanKSAContext context)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddPostBranch(CrCasBranchPost CrCasBranchPost, CrMasSupPostCity City)
        {

            var BranchPost = new CrCasBranchPost
            {
                CrCasBranchPostLessor = CrCasBranchPost.CrCasBranchPostLessor,
                CrCasBranchPostBranch = CrCasBranchPost.CrCasBranchPostBranch,
                CrCasBranchPostRegions = City.CrMasSupPostCityRegionsCode,
                CrCasBranchPostCity = City.CrMasSupPostCityCode,
                CrCasBranchPostArDistrict = CrCasBranchPost.CrCasBranchPostArDistrict,
                CrCasBranchPostEnDistrict = CrCasBranchPost.CrCasBranchPostEnDistrict,
                CrCasBranchPostArStreet = CrCasBranchPost.CrCasBranchPostArStreet,
                CrCasBranchPostEnStreet = CrCasBranchPost.CrCasBranchPostEnStreet,
                CrCasBranchPostBuilding = CrCasBranchPost.CrCasBranchPostBuilding,
                CrCasBranchPostUnitNo = CrCasBranchPost.CrCasBranchPostUnitNo,
                CrCasBranchPostZipCode = CrCasBranchPost.CrCasBranchPostZipCode,
                CrCasBranchPostAdditionalNumbers = CrCasBranchPost.CrCasBranchPostAdditionalNumbers,
                CrCasBranchPostArConcatenate = $"{City.CrMasSupPostCityConcatenateArName} - {CrCasBranchPost.CrCasBranchPostArDistrict} - {CrCasBranchPost.CrCasBranchPostArStreet} - {CrCasBranchPost.CrCasBranchPostBuilding} - {CrCasBranchPost.CrCasBranchPostUnitNo} - {CrCasBranchPost.CrCasBranchPostZipCode} - {CrCasBranchPost.CrCasBranchPostAdditionalNumbers}",
                CrCasBranchPostEnConcatenate = $"{City.CrMasSupPostCityConcatenateEnName} - {CrCasBranchPost.CrCasBranchPostEnDistrict} - {CrCasBranchPost.CrCasBranchPostEnStreet} - {CrCasBranchPost.CrCasBranchPostBuilding} - {CrCasBranchPost.CrCasBranchPostUnitNo} - {CrCasBranchPost.CrCasBranchPostZipCode} - {CrCasBranchPost.CrCasBranchPostAdditionalNumbers}",
                CrCasBranchPostArShortConcatenate = $"{City.CrMasSupPostCityConcatenateArName} - {CrCasBranchPost.CrCasBranchPostArDistrict} - {CrCasBranchPost.CrCasBranchPostArStreet}",
                CrCasBranchPostEnShortConcatenate = $"{City.CrMasSupPostCityConcatenateEnName} - {CrCasBranchPost.CrCasBranchPostEnDistrict} - {CrCasBranchPost.CrCasBranchPostEnStreet}",
                 CrCasBranchPostUpDateMail = DateTime.Now,
                CrCasBranchPostStatus = "A"

            };
            await _unitOfWork.CrCasBranchPost.AddAsync(BranchPost);
            return true;
        }

        public async Task<bool> AddPostBranchDefault(string LessorCode, CrCasBranchPost crCasBranchPost, CrMasSupPostCity City)
        {
            var lessor = await _unitOfWork.CrMasLessorInformation.GetByIdAsync(LessorCode);
            var BranchPost = new CrCasBranchPost
            {
                CrCasBranchPostLessor = lessor.CrMasLessorInformationCode,
                CrCasBranchPostBranch = "100",
                CrCasBranchPostRegions = City.CrMasSupPostCityRegionsCode,
                CrCasBranchPostCity = City.CrMasSupPostCityCode,
                CrCasBranchPostArDistrict = crCasBranchPost.CrCasBranchPostArDistrict,
                CrCasBranchPostEnDistrict = crCasBranchPost.CrCasBranchPostEnDistrict,
                CrCasBranchPostArStreet = crCasBranchPost.CrCasBranchPostArStreet,
                CrCasBranchPostEnStreet = crCasBranchPost.CrCasBranchPostEnStreet,
                CrCasBranchPostBuilding = crCasBranchPost.CrCasBranchPostBuilding,
                CrCasBranchPostUnitNo = crCasBranchPost.CrCasBranchPostUnitNo,
                CrCasBranchPostZipCode = crCasBranchPost.CrCasBranchPostZipCode,
                CrCasBranchPostAdditionalNumbers = crCasBranchPost.CrCasBranchPostAdditionalNumbers,
                CrCasBranchPostArConcatenate = $"{City.CrMasSupPostCityConcatenateArName} - {crCasBranchPost.CrCasBranchPostArDistrict} - {crCasBranchPost.CrCasBranchPostArStreet} - {crCasBranchPost.CrCasBranchPostBuilding} - {crCasBranchPost.CrCasBranchPostUnitNo} - {crCasBranchPost.CrCasBranchPostZipCode} - {crCasBranchPost.CrCasBranchPostAdditionalNumbers}",
                CrCasBranchPostEnConcatenate = $"{City.CrMasSupPostCityConcatenateEnName} - {crCasBranchPost.CrCasBranchPostEnDistrict} - {crCasBranchPost.CrCasBranchPostEnStreet} - {crCasBranchPost.CrCasBranchPostBuilding} - {crCasBranchPost.CrCasBranchPostUnitNo} - {crCasBranchPost.CrCasBranchPostZipCode} - {crCasBranchPost.CrCasBranchPostAdditionalNumbers}",
                CrCasBranchPostArShortConcatenate = $"{City.CrMasSupPostCityConcatenateArName} - {crCasBranchPost.CrCasBranchPostArDistrict} - {crCasBranchPost.CrCasBranchPostArStreet}",
                CrCasBranchPostEnShortConcatenate = $"{City.CrMasSupPostCityConcatenateEnName} - {crCasBranchPost.CrCasBranchPostEnDistrict} - {crCasBranchPost.CrCasBranchPostEnStreet}",
                CrCasBranchPostUpDateMail = DateTime.Now,
                CrCasBranchPostStatus = "A"

            };
            await _unitOfWork.CrCasBranchPost.AddAsync(BranchPost);
            return true;
        }

        public List<CrCasBranchPost>GetAllByLessor(string LessorCode)
        {
            var branches =  _unitOfWork.CrCasBranchPost.FindAll(l=>l.CrCasBranchPostLessor == LessorCode, (new[] { "CrCasBranchPostCityNavigation", "CrCasBranchPostNavigation" })).ToList();
            return branches;
        }

       
    }
}
