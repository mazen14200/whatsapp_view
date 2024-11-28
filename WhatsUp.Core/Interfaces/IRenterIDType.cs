﻿using WhatsUp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhatsUp.Core.Interfaces
{
    public interface IRenterIdType
    {
        List<List<string>> GetAllRenterIdTypesCount();

        int GetOneRenterIdTypeCount(string id);

    }
}
