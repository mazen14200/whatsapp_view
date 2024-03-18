using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class CasRenterLessorInformation :IRenterLessorInformation
    {
        public IUnitOfWork _unitOfWork;

        public CasRenterLessorInformation(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public  List<List<string>> AdressSplit(List<CrMasRenterPost> RenterLessorInformationAllA)
        {
            List<List<string>> ConcateAdress_short = new List<List<string>>();
            foreach (var item in RenterLessorInformationAllA)
            {
                var ar = item.CrMasRenterPostArShortConcatenate.ToString();
                string[] values = ar.Split('-');
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim();
                }
                var en = item.CrMasRenterPostEnShortConcatenate.ToString();
                string[] values2 = en.Split('-');
                for (int i = 0; i < values2.Length; i++)
                {
                    values2[i] = values2[i].Trim();
                }
                if (values.Length > 1 && values2.Length > 1)
                {
                    if (values2[0].Length < 4 && values2.Length > 2)
                    {
                        ConcateAdress_short.Add(new List<string> { item.CrMasRenterPostCode, values[0] + " - " + values[1], values2[0] + "-" + values2[1] + " - " + values2[2] });
                    }
                    else
                    {
                        ConcateAdress_short.Add(new List<string> { item.CrMasRenterPostCode, values[0] + " - " + values[1], values2[0] + " - " + values2[1] });

                    }
                }


            }
            if (ConcateAdress_short == null)
            {
                return null;

            }
            else
            {
                return ConcateAdress_short;

            }
        }

    }
}
