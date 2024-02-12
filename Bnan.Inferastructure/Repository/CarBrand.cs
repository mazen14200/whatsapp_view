using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class CarBrand : ICarBrand
    {
        public IUnitOfWork _unitOfWork;

        public CarBrand(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<List<CrMasSupCarBrand?>> GetAllCarBrandsByStatusAsync()
        {
            var Brands = await _unitOfWork.CrMasSupCarBrand.GetAllAsync();
            return (List<CrMasSupCarBrand?>)Brands;
        }
        public List<List<string>> GetAllCarBrandsCount()
        {
            List<List<string>> Counts_ids = new List<List<string>>();
            IEnumerable<CrMasSupCarBrand?> Brands = _unitOfWork.CrMasSupCarBrand.GetAll();
            if (Brands != null)
            {
                foreach (var item in Brands)
                {
                    List<string> Counts = new List<string>();
                    int x = _unitOfWork.CrCasCarInformation.Count(l=> l.CrCasCarInformationBrand == item.CrMasSupCarBrandCode);
                    if (x != null)
                    {
                        Counts.Add(item.CrMasSupCarBrandCode);
                        Counts.Add(x.ToString());
                        Counts_ids.Add(Counts);
                    }
                }
            }

            return (Counts_ids);
        }

        public  int GetOneBrandCount(string id)
        {
           int x = 0;
           x = _unitOfWork.CrCasCarInformation.Count(l => l.CrCasCarInformationBrand == id);

            return x;
        }




    }
}
