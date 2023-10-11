namespace Bnan.Ui.ViewModels.MAS
{
    public class CrMasSysProcedureVM
    {
        public string CrMasSysProceduresCode { get; set; } = null!;
        public string? CrMasSysProceduresClassification { get; set; }
        public bool? CrMasSysProceduresSubjectAlert { get; set; }
        public string? CrMasSysProceduresArName { get; set; }
        public string? CrMasSysProceduresEnName { get; set; }
        public long? CrMasSysProceduresDaysAlertAboutExpire { get; set; }
        public long? CrMasSysProceduresKmAlertAboutExpire { get; set; }
        public string? CrMasSysProceduresStatus { get; set; }
        public string? CrMasSysProceduresReasons { get; set; }
    }
}
