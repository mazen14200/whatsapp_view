using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IOwner
    {
        Task<bool> AddOwner(string LessorCode);
        Task<bool> AddOwnerInCas(CrCasOwner model);

        Task<bool> UpdateOwnerInCas(CrCasOwner model);

    }
}
