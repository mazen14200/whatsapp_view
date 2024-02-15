using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class MasCarAdvantage : IMasCarAdvantage
    {
        public IUnitOfWork _unitOfWork;

        public MasCarAdvantage(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<List<string>> GetAllCarAdvantagesCount()
        {
            List<List<string>> Counts_ids = new List<List<string>>();
            IEnumerable<CrMasSupCarAdvantage?> Brands = _unitOfWork.CrMasSupCarAdvantage.GetAll();
            if (Brands != null)
            {
                foreach (var item in Brands)
                {
                    List<string> Counts = new List<string>();
                    int x = _unitOfWork.CrCasCarAdvantage.Count(l => l.CrCasCarAdvantagesCode == item.CrMasSupCarAdvantagesCode);
                    if (x != null)
                    {
                        Counts.Add(item.CrMasSupCarAdvantagesCode);
                        Counts.Add(x.ToString());
                        Counts_ids.Add(Counts);
                    }
                }
            }

            return (Counts_ids);
        }

        public int GetOneCarAdvantageCount(string id)
        {
            int x = 0;
            x = _unitOfWork.CrCasCarAdvantage.Count(l => l.CrCasCarAdvantagesCode == id);

            return x;
        }
    }
  }
