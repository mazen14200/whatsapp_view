using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class PostCity:IPostCity
    {
        public IUnitOfWork _unitOfWork;

        public PostCity(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CrMasSupPostCity?>> GetAllCityByStatusAsync()
        {
            var City = await _unitOfWork.CrMasSupPostCity.GetAllAsync();
            return (List<CrMasSupPostCity?>)City;

        }
    }
}
