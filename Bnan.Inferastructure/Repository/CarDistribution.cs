using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class CarDistribution : ICarDistribution
    {
        public IUnitOfWork _unitOfWork;

        public CarDistribution(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddCarDisribtion(CrMasSupCarDistribution crMasSupCarDistribution)
        {
           
            var model = await _unitOfWork.CrMasSupCarModel.GetByIdAsync(crMasSupCarDistribution.CrMasSupCarDistributionModel);
            var Category = await _unitOfWork.CrMasSupCarCategory.GetByIdAsync(crMasSupCarDistribution.CrMasSupCarDistributionCategory);
            var CarDistributionArConcat = $"{model.CrMasSupCarModelArConcatenateName} - {Category.CrMasSupCarCategoryArName} - {crMasSupCarDistribution.CrMasSupCarDistributionYear}";
            var CarDistributionEnConcat = $"{model.CrMasSupCarModelConcatenateEnName} - {Category.CrMasSupCarCategoryEnName} - {crMasSupCarDistribution.CrMasSupCarDistributionYear}";

            CrMasSupCarDistribution NewcrMasSupCarDistribution = new()
            {
                CrMasSupCarDistributionCode = crMasSupCarDistribution.CrMasSupCarDistributionCode,
                CrMasSupCarDistributionBrand = model.CrMasSupCarModelBrand,
                CrMasSupCarDistributionModel = crMasSupCarDistribution.CrMasSupCarDistributionModel,
                CrMasSupCarDistributionCategory = crMasSupCarDistribution.CrMasSupCarDistributionCategory,
                CrMasSupCarDistributionYear = crMasSupCarDistribution.CrMasSupCarDistributionYear,
                CrMasSupCarDistributionDoor = crMasSupCarDistribution.CrMasSupCarDistributionDoor,
                CrMasSupCarDistributionBagBags = crMasSupCarDistribution.CrMasSupCarDistributionBagBags,
                CrMasSupCarDistributionSmallBags = crMasSupCarDistribution.CrMasSupCarDistributionSmallBags,
                CrMasSupCarDistributionPassengers = crMasSupCarDistribution.CrMasSupCarDistributionPassengers,
                CrMasSupCarDistributionConcatenateArName = CarDistributionArConcat,
                CrMasSupCarDistributionConcatenateEnName = CarDistributionEnConcat,
                CrMasSupCarDistributionImage = crMasSupCarDistribution.CrMasSupCarDistributionImage,
                CrMasSupCarDistributionStatus = "A"

            };
            await _unitOfWork.CrMasSupCarDistribution.AddAsync(NewcrMasSupCarDistribution);
            _unitOfWork.Complete();
            return true;
        }

        public async Task<bool> editCarDisribtion(CrMasSupCarDistribution crMasSupCarDistribution)
        {
            var model = await _unitOfWork.CrMasSupCarModel.GetByIdAsync(crMasSupCarDistribution.CrMasSupCarDistributionModel);
            var Category = await _unitOfWork.CrMasSupCarCategory.GetByIdAsync(crMasSupCarDistribution.CrMasSupCarDistributionCategory);
            var CarDistributionArConcat = $"{model.CrMasSupCarModelArConcatenateName} - {Category.CrMasSupCarCategoryArName} - {crMasSupCarDistribution.CrMasSupCarDistributionYear}";
            var CarDistributionEnConcat = $"{model.CrMasSupCarModelConcatenateEnName} - {Category.CrMasSupCarCategoryEnName} - {crMasSupCarDistribution.CrMasSupCarDistributionYear}";
            crMasSupCarDistribution.CrMasSupCarDistributionBrand = model.CrMasSupCarModelBrand;
            crMasSupCarDistribution.CrMasSupCarDistributionConcatenateArName = CarDistributionArConcat;
            crMasSupCarDistribution.CrMasSupCarDistributionConcatenateEnName = CarDistributionEnConcat;
            _unitOfWork.CrMasSupCarDistribution.Update(crMasSupCarDistribution);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
