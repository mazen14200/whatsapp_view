﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface ICustody
    {
        Task<bool> UpdateAccountReceipt(string ReceiptNo,string ReferenceNo, string Reasons);

    }
}