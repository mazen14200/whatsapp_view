using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class MasCarRegistration : IMasCarRegistration
    {
        public IUnitOfWork _unitOfWork;

        public MasCarRegistration(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<List<string>> GetAllCarRegistrationsCount()
        {
            List<List<string>> Counts_ids = new List<List<string>>();
            IEnumerable<CrMasSupCarRegistration?> Brands = _unitOfWork.CrMasSupCarRegistration.GetAll();
            if (Brands != null)
            {
                foreach (var item in Brands)
                {
                    List<string> Counts = new List<string>();
                    int x = _unitOfWork.CrCasCarInformation.Count(l => l.CrCasCarInformationRegistration == item.CrMasSupCarRegistrationCode);
                    if (x != null)
                    {
                        Counts.Add(item.CrMasSupCarRegistrationCode);
                        Counts.Add(x.ToString());
                        Counts_ids.Add(Counts);
                    }
                }
            }

            return (Counts_ids);
        }

        public int GetOneCarRegistrationCount(string id)
        {
            int x = 0;
            x = _unitOfWork.CrCasCarInformation.Count(l => l.CrCasCarInformationRegistration == id);

            return x;
        }
    }
}
