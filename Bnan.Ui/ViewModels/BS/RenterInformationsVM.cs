namespace Bnan.Ui.ViewModels.BS
{
    public class RenterInformationsVM
    {
        // Personal
        public string? RenterID { get; set; }
        public string? PersonalArName { get; set; }
        public string? PersonalEnName { get; set; }
        public string? PersonalArNationality { get; set; }
        public string? PersonalEnNationality { get; set; }
        public string? PersonalArGender { get; set; }
        public string? PersonalEnGender { get; set; }
        public string? PersonalArProfessions { get; set; }
        public string? PersonalEnProfessions { get; set; }
        public string? PersonalEmail { get; set; }
        public string? MobileNumber { get; set; }
        public string? KeyCountry { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? ExpiryIdDate { get; set; }
        public decimal? Balance { get; set; }
        public decimal? AvailableBalance { get; set; }
        public decimal? ReservedBalance { get; set; }

        //Licence
        public string? LicenseCode { get; set; }
        public string? LicenseArName { get; set; }
        public string? LicenseEnName { get; set; }
        public DateTime? LicenseIssuedDate { get; set; }
        public DateTime? LicenseExpiryDate { get; set; }
        //Employeer
        public string? EmployerCode { get; set; }
        public string? EmployerArName { get; set; }
        public string? EmployerEnName { get; set; }
        //Post
        public string? PostArNameConcenate { get; set; }
        public string? PostEnNameConcenate { get; set; }
        //Cas Renter
        public DateTime? LastVisit { get; set; }
        public DateTime? LastContract { get; set; }
        public int? ContractCount { get; set; }
        public int? RentalDays { get; set; }
        public int? KMCut { get; set; }
        public decimal? AmountsTraded { get; set; }
        public decimal? Evaluation { get; set; }
        public string? ArDealingMechanism { get; set; }
        public string? EnDealingMechanism { get; set; }
        public string? ArMembership { get; set; }
        public string? EnMembership { get; set; }
        public int? CountContracts { get; set; }


    }
}
