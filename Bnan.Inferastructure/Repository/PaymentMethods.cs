using Bnan.Core.Interfaces;
using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class PaymentMethods:IPaymentMethods
    {
        IUnitOfWork _unitOfWork;
        public PaymentMethods(IUnitOfWork unitOfWork) 
        { 
        _unitOfWork = unitOfWork;
        }

        public async Task<List<CrMasSupAccountPaymentMethod?>> GetAllPaymentsByStatusAsync()
        {
            var methods = await _unitOfWork.CrMasSupAccountPaymentMethod.GetAllAsync();
            return (List<CrMasSupAccountPaymentMethod?>)methods;

        }

    }
}
