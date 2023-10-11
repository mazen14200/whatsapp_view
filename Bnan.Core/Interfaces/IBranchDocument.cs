using Bnan.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnan.Core.Interfaces
{
    public interface IBranchDocument
    {
        Task<bool> AddBranchDocumentDefault(string lessorCode);
        Task<bool> AddBranchDocument(string lessorCode, string branchCode);
        Task<bool> UpdateBranchDocument(CrCasBranchDocument CrCasBranchDocument);
        Task<CrCasBranchDocument> GetBranchDocument(string DocumentsLessor, string DocumentsBranch, string DocumentsProcedures);
    }
}
