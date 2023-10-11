using System;
using System.Collections.Generic;

namespace Bnan.Core.Models
{
    public partial class CrCasBranchDocument
    {
        public string CrCasBranchDocumentsLessor { get; set; } = null!;
        public string CrCasBranchDocumentsBranch { get; set; } = null!;
        public string CrCasBranchDocumentsProcedures { get; set; } = null!;
        public string? CrCasBranchDocumentsProceduresClassification { get; set; }
        public bool? CrCasBranchDocumentsActivation { get; set; }
        public string? CrCasBranchDocumentsNo { get; set; }
        public DateTime? CrCasBranchDocumentsDate { get; set; }
        public DateTime? CrCasBranchDocumentsStartDate { get; set; }
        public DateTime? CrCasBranchDocumentsEndDate { get; set; }
        public DateTime? CrCasBranchDocumentsDateAboutToFinish { get; set; }
        public string? CrCasBranchDocumentsImage { get; set; }
        public string? CrCasBranchDocumentsBranchStatus { get; set; }
        public string? CrCasBranchDocumentsStatus { get; set; }
        public string? CrCasBranchDocumentsReasons { get; set; }

        public virtual CrCasBranchInformation CrCasBranchDocuments { get; set; } = null!;
        public virtual CrMasLessorInformation CrCasBranchDocumentsLessorNavigation { get; set; } = null!;
        public virtual CrMasSysProcedure CrCasBranchDocumentsProceduresNavigation { get; set; } = null!;
    }
}
