using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class MasCarFuel : IMasCarFuel
    {
        public IUnitOfWork _unitOfWork;

        public MasCarFuel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<List<string>> GetAllCarFuelsCount()
        {
            List<List<string>> Counts_ids = new List<List<string>>();
            IEnumerable<CrMasSupCarFuel?> Brands = _unitOfWork.CrMasSupCarFuel.GetAll();
            if (Brands != null)
            {
                foreach (var item in Brands)
                {
                    List<string> Counts = new List<string>();
                    int x = _unitOfWork.CrCasCarInformation.Count(l => l.CrCasCarInformationFuel == item.CrMasSupCarFuelCode);
                    if (x != null)
                    {
                        Counts.Add(item.CrMasSupCarFuelCode);
                        Counts.Add(x.ToString());
                        Counts_ids.Add(Counts);
                    }
                }
            }

            return (Counts_ids);
        }

        public int GetOneCarFuelCount(string id)
        {
            int x = 0;
            x = _unitOfWork.CrCasCarInformation.Count(l => l.CrCasCarInformationFuel == id);

            return x;
        }
    }
}
