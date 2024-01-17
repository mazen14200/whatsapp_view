using Bnan.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Inferastructure.Repository
{
    public class Custody:ICustody
    {
        private IUnitOfWork _unitOfWork;

        public Custody(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> UpdateAccountReceipt(string ReceiptNo,string ReferenceNo,string Reasons)
        {
            var receipt = await _unitOfWork.CrCasAccountReceipt.FindAsync(x=>x.CrCasAccountReceiptNo== ReceiptNo);
            if (receipt!=null)
            {
                receipt.CrCasAccountReceiptPassingDate = DateTime.Now.Date;
                receipt.CrCasAccountReceiptIsPassing = "2";
                receipt.CrCasAccountReceiptPassingReference = ReferenceNo;
                receipt.CrCasAccountReceiptReasons = Reasons;
                if (_unitOfWork.CrCasAccountReceipt.Update(receipt)!=null) return true;
            }
            return false;
        }
    }
}
