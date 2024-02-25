using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class RenterIdType : IRenterIdType
    {
        public IUnitOfWork _unitOfWork;
        public RenterIdType(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<List<string>> GetAllRenterIdTypesCount()
        {
            List<List<string>> Counts_ids = new List<List<string>>();
            IEnumerable<CrMasSupRenterIdtype?> Brands = _unitOfWork.CrMasSupRenterIdtype.GetAll();
            if (Brands != null)
            {
                foreach (var item in Brands)
                {
                    List<string> Counts = new List<string>();
                    int x = _unitOfWork.CrCasRenterPrivateDriverInformation.Count(l => l.CrCasRenterPrivateDriverInformationIdtrype == item.CrMasSupRenterIdtypeCode);
                    if (x != null)
                    {
                        Counts.Add(item.CrMasSupRenterIdtypeCode);
                        Counts.Add(x.ToString());
                        Counts_ids.Add(Counts);
                    }
                }
            }

            return (Counts_ids);
        }

        public int GetOneRenterIdTypeCount(string id)
        {
            int x = 0;
            x = _unitOfWork.CrCasRenterPrivateDriverInformation.Count(l => l.CrCasRenterPrivateDriverInformationIdtrype == id);

            return x;
        }
    }
}
