using Bnan.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class FinancialTransactionOfEmployee : IFinancialTransactionOfEmployee
    {
        public IUnitOfWork _unitOfWork;

        public FinancialTransactionOfEmployee(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
