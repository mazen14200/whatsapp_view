using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Bnan.Core.Models
{
    public partial class BnanKSAContext : IdentityDbContext<CrMasUserInformation>
    {
        public BnanKSAContext()
        {
        }

        public BnanKSAContext(DbContextOptions<BnanKSAContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CrCasAccountBank> CrCasAccountBanks { get; set; } = null!;
        public virtual DbSet<CrCasAccountSalesPoint> CrCasAccountSalesPoints { get; set; } = null!;
        public virtual DbSet<CrCasBeneficiary> CrCasBeneficiaries { get; set; } = null!;
        public virtual DbSet<CrCasBranchDocument> CrCasBranchDocuments { get; set; } = null!;
        public virtual DbSet<CrCasBranchInformation> CrCasBranchInformations { get; set; } = null!;
        public virtual DbSet<CrCasBranchPost> CrCasBranchPosts { get; set; } = null!;
        public virtual DbSet<CrCasLessorClassification> CrCasLessorClassifications { get; set; } = null!;
        public virtual DbSet<CrCasLessorMechanism> CrCasLessorMechanisms { get; set; } = null!;
        public virtual DbSet<CrCasLessorMembership> CrCasLessorMemberships { get; set; } = null!;
        public virtual DbSet<CrCasOwner> CrCasOwners { get; set; } = null!;
        public virtual DbSet<CrCasRenterPrivateDriverInformation> CrCasRenterPrivateDriverInformations { get; set; } = null!;
        public virtual DbSet<CrCasSysAdministrativeProcedure> CrCasSysAdministrativeProcedures { get; set; } = null!;
        public virtual DbSet<CrMasContractCompany> CrMasContractCompanies { get; set; } = null!;
        public virtual DbSet<CrMasContractCompanyDetailed> CrMasContractCompanyDetaileds { get; set; } = null!;
        public virtual DbSet<CrMasLessorImage> CrMasLessorImages { get; set; } = null!;
        public virtual DbSet<CrMasLessorInformation> CrMasLessorInformations { get; set; } = null!;
        public virtual DbSet<CrMasSupAccountBank> CrMasSupAccountBanks { get; set; } = null!;
        public virtual DbSet<CrMasSupAccountPaymentMethod> CrMasSupAccountPaymentMethods { get; set; } = null!;
        public virtual DbSet<CrMasSupAccountReference> CrMasSupAccountReferences { get; set; } = null!;
        public virtual DbSet<CrMasSupCarAdvantage> CrMasSupCarAdvantages { get; set; } = null!;
        public virtual DbSet<CrMasSupCarBrand> CrMasSupCarBrands { get; set; } = null!;
        public virtual DbSet<CrMasSupCarCategory> CrMasSupCarCategories { get; set; } = null!;
        public virtual DbSet<CrMasSupCarColor> CrMasSupCarColors { get; set; } = null!;
        public virtual DbSet<CrMasSupCarCvt> CrMasSupCarCvts { get; set; } = null!;
        public virtual DbSet<CrMasSupCarDistribution> CrMasSupCarDistributions { get; set; } = null!;
        public virtual DbSet<CrMasSupCarFuel> CrMasSupCarFuels { get; set; } = null!;
        public virtual DbSet<CrMasSupCarModel> CrMasSupCarModels { get; set; } = null!;
        public virtual DbSet<CrMasSupCarRegistration> CrMasSupCarRegistrations { get; set; } = null!;
        public virtual DbSet<CrMasSupCarYear> CrMasSupCarYears { get; set; } = null!;
        public virtual DbSet<CrMasSupContractAdditional> CrMasSupContractAdditionals { get; set; } = null!;
        public virtual DbSet<CrMasSupContractCarCheckup> CrMasSupContractCarCheckups { get; set; } = null!;
        public virtual DbSet<CrMasSupContractOption> CrMasSupContractOptions { get; set; } = null!;
        public virtual DbSet<CrMasSupMembershipChoice> CrMasSupMembershipChoices { get; set; } = null!;
        public virtual DbSet<CrMasSupPostCity> CrMasSupPostCities { get; set; } = null!;
        public virtual DbSet<CrMasSupPostRegion> CrMasSupPostRegions { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterAge> CrMasSupRenterAges { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterDrivingLicense> CrMasSupRenterDrivingLicenses { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterEmployer> CrMasSupRenterEmployers { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterGender> CrMasSupRenterGenders { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterIdtype> CrMasSupRenterIdtypes { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterMembership> CrMasSupRenterMemberships { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterNationality> CrMasSupRenterNationalities { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterProfession> CrMasSupRenterProfessions { get; set; } = null!;
        public virtual DbSet<CrMasSupRenterSector> CrMasSupRenterSectors { get; set; } = null!;
        public virtual DbSet<CrMasSysCallingKey> CrMasSysCallingKeys { get; set; } = null!;
        public virtual DbSet<CrMasSysEvaluation> CrMasSysEvaluations { get; set; } = null!;
        public virtual DbSet<CrMasSysGroup> CrMasSysGroups { get; set; } = null!;
        public virtual DbSet<CrMasSysMainTask> CrMasSysMainTasks { get; set; } = null!;
        public virtual DbSet<CrMasSysProcedure> CrMasSysProcedures { get; set; } = null!;
        public virtual DbSet<CrMasSysProceduresTask> CrMasSysProceduresTasks { get; set; } = null!;
        public virtual DbSet<CrMasSysStatus> CrMasSysStatuses { get; set; } = null!;
        public virtual DbSet<CrMasSysSubTask> CrMasSysSubTasks { get; set; } = null!;
        public virtual DbSet<CrMasSysSystem> CrMasSysSystems { get; set; } = null!;
        public virtual DbSet<CrMasUserBranchValidity> CrMasUserBranchValidities { get; set; } = null!;
        public virtual DbSet<CrMasUserContractValidity> CrMasUserContractValidities { get; set; } = null!;
        public virtual DbSet<CrMasUserInformation> CrMasUserInformations { get; set; } = null!;
        public virtual DbSet<CrMasUserLogin> CrMasUserLogins { get; set; } = null!;
        public virtual DbSet<CrMasUserMainValidation> CrMasUserMainValidations { get; set; } = null!;
        public virtual DbSet<CrMasUserMessage> CrMasUserMessages { get; set; } = null!;
        public virtual DbSet<CrMasUserProceduresValidation> CrMasUserProceduresValidations { get; set; } = null!;
        public virtual DbSet<CrMasUserSubValidation> CrMasUserSubValidations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=BnanKSA;Trusted_Connection=True;TrustServerCertificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UserTokens");
            modelBuilder.Entity<CrMasUserInformation>().Ignore(x => x.Email).Ignore(x => x.EmailConfirmed)
                .Ignore(x => x.NormalizedEmail).Ignore(x => x.PhoneNumber).Ignore(x => x.PhoneNumberConfirmed);
            modelBuilder.Entity<CrCasAccountBank>(entity =>
            {
                entity.HasKey(e => e.CrCasAccountBankCode);

                entity.ToTable("CR_Cas_Account_Bank");

                entity.HasIndex(e => e.CrCasAccountBankLessor, "IX_CR_Cas_Account_Bank_CR_Cas_Account_Bank_Lessor");

                entity.HasIndex(e => e.CrCasAccountBankNo, "IX_CR_Cas_Account_Bank_CR_Cas_Account_Bank_No");

                entity.Property(e => e.CrCasAccountBankCode)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Bank_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountBankArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Account_Bank_Ar_Name");

                entity.Property(e => e.CrCasAccountBankEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Account_Bank_En_Name");

                entity.Property(e => e.CrCasAccountBankIban)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Account_Bank_IBAN");

                entity.Property(e => e.CrCasAccountBankLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Bank_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountBankNo)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Bank_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountBankReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Account_Bank_Reasons");

                entity.Property(e => e.CrCasAccountBankSerail)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Bank_Serail")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountBankStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Bank_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasAccountBankLessorNavigation)
                    .WithMany(p => p.CrCasAccountBanks)
                    .HasForeignKey(d => d.CrCasAccountBankLessor)
                    .HasConstraintName("FK_CR_Cas_Account_Bank_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrCasAccountBankNoNavigation)
                    .WithMany(p => p.CrCasAccountBanks)
                    .HasForeignKey(d => d.CrCasAccountBankNo)
                    .HasConstraintName("FK_CR_Cas_Account_Bank_CR_Mas_Sup_Account_Bank");
            });

            modelBuilder.Entity<CrCasAccountSalesPoint>(entity =>
            {
                entity.HasKey(e => e.CrCasAccountSalesPointCode);

                entity.ToTable("CR_Cas_Account_SalesPoint");

                entity.HasIndex(e => e.CrCasAccountSalesPointAccountBank, "IX_CR_Cas_Account_SalesPoint_CR_Cas_Account_SalesPoint_Account_Bank");

                entity.HasIndex(e => e.CrCasAccountSalesPointBank, "IX_CR_Cas_Account_SalesPoint_CR_Cas_Account_SalesPoint_Bank");

                entity.HasIndex(e => new { e.CrCasAccountSalesPointLessor, e.CrCasAccountSalesPointBrn }, "IX_CR_Cas_Account_SalesPoint_CR_Cas_Account_SalesPoint_Lessor_CR_Cas_Account_SalesPoint_Brn");

                entity.Property(e => e.CrCasAccountSalesPointCode)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointAccountBank)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Account_Bank")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Ar_Name");

                entity.Property(e => e.CrCasAccountSalesPointBank)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Bank")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointBankStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Bank_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointBranchStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Branch_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointBrn)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Brn")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Account_SalesPoint_En_Name");

                entity.Property(e => e.CrCasAccountSalesPointLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointNo)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Account_SalesPoint_No");

                entity.Property(e => e.CrCasAccountSalesPointReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Reasons");

                entity.Property(e => e.CrCasAccountSalesPointSerial)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Serial")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_SalesPoint_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountSalesPointTotalAvailable)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Account_SalesPoint_Total_Available");

                entity.Property(e => e.CrCasAccountSalesPointTotalBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Account_SalesPoint_Total_Balance");

                entity.Property(e => e.CrCasAccountSalesPointTotalReserved)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Account_SalesPoint_Total_Reserved");

                entity.HasOne(d => d.CrCasAccountSalesPointAccountBankNavigation)
                    .WithMany(p => p.CrCasAccountSalesPoints)
                    .HasForeignKey(d => d.CrCasAccountSalesPointAccountBank)
                    .HasConstraintName("FK_CR_Cas_Account_SalesPoint_CR_Cas_Account_Bank");

                entity.HasOne(d => d.CrCasAccountSalesPointBankNavigation)
                    .WithMany(p => p.CrCasAccountSalesPoints)
                    .HasForeignKey(d => d.CrCasAccountSalesPointBank)
                    .HasConstraintName("FK_CR_Cas_Account_SalesPoint_CR_Cas_Account_SalesPoint");

                entity.HasOne(d => d.CrCasAccountSalesPointNavigation)
                    .WithMany(p => p.CrCasAccountSalesPoints)
                    .HasForeignKey(d => new { d.CrCasAccountSalesPointLessor, d.CrCasAccountSalesPointBrn })
                    .HasConstraintName("FK_CR_Cas_Account_SalesPoint_CR_Cas_Branch_Information_branch_Lessor");
            });

            modelBuilder.Entity<CrCasBeneficiary>(entity =>
            {
                entity.HasKey(e => new { e.CrCasBeneficiaryCode, e.CrCasBeneficiaryLessorCode });

                entity.ToTable("CR_Cas_Beneficiary");

                entity.HasIndex(e => e.CrCasBeneficiaryLessorCode, "IX_CR_Cas_Beneficiary_CR_Cas_Beneficiary_Lessor_Code");

                entity.HasIndex(e => e.CrCasBeneficiarySector, "IX_CR_Cas_Beneficiary_CR_Cas_Beneficiary_Sector");

                entity.Property(e => e.CrCasBeneficiaryCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Beneficiary_Code");

                entity.Property(e => e.CrCasBeneficiaryLessorCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Beneficiary_Lessor_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBeneficiaryArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Beneficiary_Ar_Name");

                entity.Property(e => e.CrCasBeneficiaryCommercialNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Beneficiary_Commercial_No");

                entity.Property(e => e.CrCasBeneficiaryEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Beneficiary_En_Name");

                entity.Property(e => e.CrCasBeneficiaryReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Beneficiary_Reasons");

                entity.Property(e => e.CrCasBeneficiarySector)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Beneficiary_Sector")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBeneficiaryStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Beneficiary_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasBeneficiaryLessorCodeNavigation)
                    .WithMany(p => p.CrCasBeneficiaries)
                    .HasForeignKey(d => d.CrCasBeneficiaryLessorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Beneficiary_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrCasBeneficiarySectorNavigation)
                    .WithMany(p => p.CrCasBeneficiaries)
                    .HasForeignKey(d => d.CrCasBeneficiarySector)
                    .HasConstraintName("CR_Cas_Beneficiary_CR_Mas_Sup_Renter_Sector");
            });

            modelBuilder.Entity<CrCasBranchDocument>(entity =>
            {
                entity.HasKey(e => new { e.CrCasBranchDocumentsLessor, e.CrCasBranchDocumentsBranch, e.CrCasBranchDocumentsProcedures });

                entity.ToTable("CR_Cas_Branch_Documents");

                entity.HasIndex(e => e.CrCasBranchDocumentsProcedures, "IX_CR_Cas_Branch_Documents_CR_Cas_Branch_Documents_Procedures");

                entity.Property(e => e.CrCasBranchDocumentsLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Documents_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchDocumentsBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Documents_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchDocumentsProcedures)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Documents_Procedures")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchDocumentsActivation).HasColumnName("CR_Cas_Branch_Documents_Activation");

                entity.Property(e => e.CrCasBranchDocumentsBranchStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Documents_Branch_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchDocumentsDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Branch_Documents_Date");

                entity.Property(e => e.CrCasBranchDocumentsDateAboutToFinish)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Branch_Documents_Date_About_To_Finish");

                entity.Property(e => e.CrCasBranchDocumentsEndDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Branch_Documents_End_Date");

                entity.Property(e => e.CrCasBranchDocumentsImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Branch_Documents_Image");

                entity.Property(e => e.CrCasBranchDocumentsNo)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Branch_Documents_No");

                entity.Property(e => e.CrCasBranchDocumentsProceduresClassification)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Documents_Procedures_Classification")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchDocumentsReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Branch_Documents_Reasons");

                entity.Property(e => e.CrCasBranchDocumentsStartDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Branch_Documents_Start_Date");

                entity.Property(e => e.CrCasBranchDocumentsStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Documents_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasBranchDocumentsLessorNavigation)
                    .WithMany(p => p.CrCasBranchDocuments)
                    .HasForeignKey(d => d.CrCasBranchDocumentsLessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Branch_Documents_CR_Mas_Lessor_Information_Code");

                entity.HasOne(d => d.CrCasBranchDocumentsProceduresNavigation)
                    .WithMany(p => p.CrCasBranchDocuments)
                    .HasForeignKey(d => d.CrCasBranchDocumentsProcedures)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Branch_Documents_CR_Mas_Sys_Procedures");

                entity.HasOne(d => d.CrCasBranchDocuments)
                    .WithMany(p => p.CrCasBranchDocuments)
                    .HasForeignKey(d => new { d.CrCasBranchDocumentsLessor, d.CrCasBranchDocumentsBranch })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Branch_Documents_CR_Mas_Lessor_Branch_Information_Code");
            });

            modelBuilder.Entity<CrCasBranchInformation>(entity =>
            {
                entity.HasKey(e => new { e.CrCasBranchInformationLessor, e.CrCasBranchInformationCode });

                entity.ToTable("CR_Cas_Branch_Information");

                entity.Property(e => e.CrCasBranchInformationLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Information_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchInformationCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Information_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchInformationArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Branch_Information_Ar_Name");

                entity.Property(e => e.CrCasBranchInformationArShortName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Branch_Information_Ar_Short_Name");

                entity.Property(e => e.CrCasBranchInformationArTga)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Branch_Information_Ar_TGA");

                entity.Property(e => e.CrCasBranchInformationAvailableBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Branch_Information_Available_Balance");

                entity.Property(e => e.CrCasBranchInformationDirectorArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Cas_Branch_Information_Director_Ar_Name");

                entity.Property(e => e.CrCasBranchInformationDirectorEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Cas_Branch_Information_Director_En_Name");

                entity.Property(e => e.CrCasBranchInformationDirectorSignature)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Branch_Information_Director_Signature");

                entity.Property(e => e.CrCasBranchInformationEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Branch_Information_En_Name");

                entity.Property(e => e.CrCasBranchInformationEnShortName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Branch_Information_En_Short_Name");

                entity.Property(e => e.CrCasBranchInformationEnTga)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Branch_Information_En_TGA");

                entity.Property(e => e.CrCasBranchInformationGovernmentNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Branch_Information_Government_No");

                entity.Property(e => e.CrCasBranchInformationMobile)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Branch_Information_Mobile");

                entity.Property(e => e.CrCasBranchInformationReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Branch_Information_Reasons");

                entity.Property(e => e.CrCasBranchInformationReservedBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Branch_Information_Reserved_Balance");

                entity.Property(e => e.CrCasBranchInformationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Information_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchInformationTaxNo)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Cas_Branch_Information_Tax_No");

                entity.Property(e => e.CrCasBranchInformationTelephone)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Branch_Information_Telephone");

                entity.Property(e => e.CrCasBranchInformationTotalBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Branch_Information_Total_Balance");

                entity.Property(e => e.CrMasBranchInformationMobileKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Branch_Information_Mobile_Key");

                entity.Property(e => e.CrMasBranchInformationTeleKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Branch_Information_Tele_Key");

                entity.HasOne(d => d.CrCasBranchInformationLessorNavigation)
                    .WithMany(p => p.CrCasBranchInformations)
                    .HasForeignKey(d => d.CrCasBranchInformationLessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Branch_Information_CR_Mas_Lessor_Information");
            });

            modelBuilder.Entity<CrCasBranchPost>(entity =>
            {
                entity.HasKey(e => new { e.CrCasBranchPostLessor, e.CrCasBranchPostBranch });

                entity.ToTable("CR_Cas_Branch_Post");

                entity.HasIndex(e => e.CrCasBranchPostCity, "IX_CR_Cas_Branch_Post_CR_Cas_Branch_Post_City");

                entity.HasIndex(e => e.CrCasBranchPostRegions, "IX_CR_Cas_Branch_Post_CR_Cas_Branch_Post_Regions");

                entity.Property(e => e.CrCasBranchPostLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Post_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchPostBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Post_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchPostAdditionalNumbers)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Cas_Branch_Post_Additional_Numbers");

                entity.Property(e => e.CrCasBranchPostArConcatenate)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Cas_Branch_Post_Ar_Concatenate");

                entity.Property(e => e.CrCasBranchPostArDistrict)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Branch_Post_Ar_District");

                entity.Property(e => e.CrCasBranchPostArMailManual)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Cas_Branch_Post_Ar_Mail_Manual");

                entity.Property(e => e.CrCasBranchPostArShortConcatenate)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Cas_Branch_Post_Ar_Short_Concatenate");

                entity.Property(e => e.CrCasBranchPostArStreet)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Branch_Post_Ar_Street");

                entity.Property(e => e.CrCasBranchPostBuilding)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Cas_Branch_Post_Building");

                entity.Property(e => e.CrCasBranchPostCity)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Post_City")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchPostEnConcatenate)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Cas_Branch_Post_En_Concatenate");

                entity.Property(e => e.CrCasBranchPostEnDistrict)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Branch_Post_En_District");

                entity.Property(e => e.CrCasBranchPostEnMailManual)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Cas_Branch_Post_En_Mail_Manual");

                entity.Property(e => e.CrCasBranchPostEnShortConcatenate)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Cas_Branch_Post_En_Short_Concatenate");

                entity.Property(e => e.CrCasBranchPostEnStreet)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Branch_Post_En_Street");

                entity.Property(e => e.CrCasBranchPostLatitude)
                    .HasColumnType("numeric(12, 8)")
                    .HasColumnName("CR_Cas_Branch_Post_Latitude");

                entity.Property(e => e.CrCasBranchPostLocation).HasColumnName("CR_Cas_Branch_Post_Location");

                entity.Property(e => e.CrCasBranchPostLongitude)
                    .HasColumnType("numeric(12, 8)")
                    .HasColumnName("CR_Cas_Branch_Post_Longitude");

                entity.Property(e => e.CrCasBranchPostReasons)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Cas_Branch_Post_Reasons")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchPostRegions)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Post_Regions")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchPostShortCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Branch_Post_Short_Code");

                entity.Property(e => e.CrCasBranchPostStatus)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Cas_Branch_Post_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasBranchPostUnitNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Branch_Post_Unit_No");

                entity.Property(e => e.CrCasBranchPostUpDateMail)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Branch_Post_UpDate_Mail");

                entity.Property(e => e.CrCasBranchPostZipCode)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Cas_Branch_Post_Zip_Code");

                entity.HasOne(d => d.CrCasBranchPostCityNavigation)
                    .WithMany(p => p.CrCasBranchPosts)
                    .HasForeignKey(d => d.CrCasBranchPostCity)
                    .HasConstraintName("FK_CR_Cas_Branch_Post_CR_Mas_Sup_Post_City");

                entity.HasOne(d => d.CrCasBranchPostRegionsNavigation)
                    .WithMany(p => p.CrCasBranchPosts)
                    .HasForeignKey(d => d.CrCasBranchPostRegions)
                    .HasConstraintName("FK_CR_Cas_Branch_Post_CR_Mas_Sup_Post_Regions");

                entity.HasOne(d => d.CrCasBranchPostNavigation)
                    .WithOne(p => p.CrCasBranchPost)
                    .HasForeignKey<CrCasBranchPost>(d => new { d.CrCasBranchPostLessor, d.CrCasBranchPostBranch })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Branch_Post_CR_Mas_Lessor_Branch_Information_Code");
            });

            modelBuilder.Entity<CrCasLessorClassification>(entity =>
            {
                entity.HasKey(e => e.CrCasLessorClassificationCode);

                entity.ToTable("CR_Cas_Lessor_Classification");

                entity.Property(e => e.CrCasLessorClassificationCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Classification_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorClassificationArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Cas_Lessor_Classification_Ar_Name");

                entity.Property(e => e.CrCasLessorClassificationEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Cas_Lessor_Classification_En_Name");

                entity.Property(e => e.CrMasLessorClassificationReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Classification_Reasons");

                entity.Property(e => e.CrMasLessorClassificationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Classification_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrCasLessorMechanism>(entity =>
            {
                entity.HasKey(e => new { e.CrCasLessorMechanismCode, e.CrCasLessorMechanismProcedures });

                entity.ToTable("CR_Cas_Lessor_Mechanism");

                entity.HasIndex(e => e.CrCasLessorMechanismProcedures, "IX_CR_Cas_Lessor_Mechanism_CR_Cas_Lessor_Mechanism_Procedures");

                entity.Property(e => e.CrCasLessorMechanismCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Mechanism_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorMechanismProcedures)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Mechanism_Procedures")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorMechanismActivate).HasColumnName("CR_Cas_Lessor_Mechanism_Activate");

                entity.Property(e => e.CrCasLessorMechanismDaysAlertAboutExpire).HasColumnName("CR_Cas_Lessor_Mechanism_Days_Alert_About_Expire");

                entity.Property(e => e.CrCasLessorMechanismKmAlertAboutExpire).HasColumnName("CR_Cas_Lessor_Mechanism_KM_Alert_About_Expire");

                entity.Property(e => e.CrCasLessorMechanismProceduresClassification)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Mechanism_Procedures_Classification")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasLessorMechanismCodeNavigation)
                    .WithMany(p => p.CrCasLessorMechanisms)
                    .HasForeignKey(d => d.CrCasLessorMechanismCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Lessor_Mechanism_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrCasLessorMechanismProceduresNavigation)
                    .WithMany(p => p.CrCasLessorMechanisms)
                    .HasForeignKey(d => d.CrCasLessorMechanismProcedures)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Lessor_Mechanism_CR_Mas_Sys_Procedures");
            });

            modelBuilder.Entity<CrCasLessorMembership>(entity =>
            {
                entity.HasKey(e => new { e.CrCasLessorMembershipConditions, e.CrCasLessorMembershipConditionsLessor });

                entity.ToTable("CR_Cas_Lessor_Membership");

                entity.HasIndex(e => e.CrCasLessorMembershipConditionsChoice, "IX_CR_Cas_Lessor_Membership_CR_Cas_Lessor_Membership_Conditions_Choice");

                entity.HasIndex(e => e.CrCasLessorMembershipConditionsLessor, "IX_CR_Cas_Lessor_Membership_CR_Cas_Lessor_Membership_Conditions_Lessor");

                entity.Property(e => e.CrCasLessorMembershipConditions)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorMembershipConditionsLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorMembershipConditionsActivate).HasColumnName("CR_Cas_Lessor_Membership_Conditions_Activate");

                entity.Property(e => e.CrCasLessorMembershipConditionsAmount)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions_Amount");

                entity.Property(e => e.CrCasLessorMembershipConditionsChoice)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions_Choice")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorMembershipConditionsContractNo).HasColumnName("CR_Cas_Lessor_Membership_Conditions_Contract_No");

                entity.Property(e => e.CrCasLessorMembershipConditionsGroup)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions_Group")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorMembershipConditionsIsCorrecte).HasColumnName("CR_Cas_Lessor_Membership_Conditions_is_Correcte");

                entity.Property(e => e.CrCasLessorMembershipConditionsKm).HasColumnName("CR_Cas_Lessor_Membership_Conditions_KM");

                entity.Property(e => e.CrCasLessorMembershipConditionsLink1)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions_Link_1")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorMembershipConditionsLink2)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions_Link_2")
                    .IsFixedLength();

                entity.Property(e => e.CrCasLessorMembershipConditionsNo).HasColumnName("CR_Cas_Lessor_Membership_Conditions_No");

                entity.Property(e => e.CrCasLessorMembershipConditionsPicture)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions_Picture");

                entity.HasOne(d => d.CrCasLessorMembershipConditionsNavigation)
                    .WithMany(p => p.CrCasLessorMemberships)
                    .HasForeignKey(d => d.CrCasLessorMembershipConditions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Lessor_Membership_CR_Mas_Sup_Renter_Membership");

                entity.HasOne(d => d.CrCasLessorMembershipConditionsChoiceNavigation)
                    .WithMany(p => p.CrCasLessorMemberships)
                    .HasForeignKey(d => d.CrCasLessorMembershipConditionsChoice)
                    .HasConstraintName("CR_Cas_Lessor_Membership_CR_Mas_Sup_Membership_Choice");

                entity.HasOne(d => d.CrCasLessorMembershipConditionsLessorNavigation)
                    .WithMany(p => p.CrCasLessorMemberships)
                    .HasForeignKey(d => d.CrCasLessorMembershipConditionsLessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Mas_Lessor_Information_CR_Mas_Sup_Renter_Membership");
            });

            modelBuilder.Entity<CrCasOwner>(entity =>
            {
                entity.HasKey(e => new { e.CrCasOwnersCode, e.CrCasOwnersLessorCode });

                entity.ToTable("CR_Cas_Owners");

                entity.HasIndex(e => e.CrCasOwnersLessorCode, "IX_CR_Cas_Owners_CR_Cas_Owners_Lessor_Code");

                entity.HasIndex(e => e.CrCasOwnersSector, "IX_CR_Cas_Owners_CR_Cas_Owners_Sector");

                entity.Property(e => e.CrCasOwnersCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Owners_Code");

                entity.Property(e => e.CrCasOwnersLessorCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Owners_Lessor_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasOwnersArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Owners_Ar_Name");

                entity.Property(e => e.CrCasOwnersCommercialNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Owners_Commercial_No");

                entity.Property(e => e.CrCasOwnersEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Owners_En_Name");

                entity.Property(e => e.CrCasOwnersReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Owners_Reasons");

                entity.Property(e => e.CrCasOwnersSector)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Owners_Sector")
                    .IsFixedLength();

                entity.Property(e => e.CrCasOwnersStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Owners_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasOwnersLessorCodeNavigation)
                    .WithMany(p => p.CrCasOwners)
                    .HasForeignKey(d => d.CrCasOwnersLessorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Owners_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrCasOwnersSectorNavigation)
                    .WithMany(p => p.CrCasOwners)
                    .HasForeignKey(d => d.CrCasOwnersSector)
                    .HasConstraintName("CR_Cas_Owners_CR_Mas_Sup_Renter_Sector");
            });

            modelBuilder.Entity<CrCasRenterPrivateDriverInformation>(entity =>
            {
                entity.HasKey(e => new { e.CrCasRenterPrivateDriverInformationId, e.CrCasRenterPrivateDriverInformationLessor })
                    .HasName("PK_CR_Cas_Renter_Private _Driver_Information");

                entity.ToTable("CR_Cas_Renter_Private_Driver_Information");

                entity.HasIndex(e => e.CrCasRenterPrivateDriverInformationGender, "IX_CR_Cas_Renter_Private_Driver_Information_CR_Cas_Renter_Private_Driver_Information_Gender");

                entity.HasIndex(e => e.CrCasRenterPrivateDriverInformationIdtrype, "IX_CR_Cas_Renter_Private_Driver_Information_CR_Cas_Renter_Private_Driver_Information_IDTRype");

                entity.HasIndex(e => e.CrCasRenterPrivateDriverInformationLessor, "IX_CR_Cas_Renter_Private_Driver_Information_CR_Cas_Renter_Private_Driver_Information_Lessor");

                entity.HasIndex(e => e.CrCasRenterPrivateDriverInformationLicenseType, "IX_CR_Cas_Renter_Private_Driver_Information_CR_Cas_Renter_Private_Driver_Information_License_Type");

                entity.HasIndex(e => e.CrCasRenterPrivateDriverInformationNationality, "IX_CR_Cas_Renter_Private_Driver_Information_CR_Cas_Renter_Private_Driver_Information_Nationality");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Id");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterPrivateDriverInformationArName)
                    .HasMaxLength(110)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Ar_Name");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationBirthDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_BirthDate");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationContractCount).HasColumnName("CR_Cas_Renter_Private_Driver_Information_Contract_Count");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationDaysCount).HasColumnName("CR_Cas_Renter_Private_Driver_Information_Days_Count");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationEmail)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Email");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationEnName)
                    .HasMaxLength(110)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_En_Name");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationEvaluationTotal)
                    .HasColumnType("decimal(9, 2)")
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Evaluation_Total");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationEvaluationValue)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Evaluation_Value");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationExpiryIdDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Expiry_Id_Date");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationGender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Gender")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterPrivateDriverInformationIdImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Id_Image");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationIdtrype)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_IDTRype")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterPrivateDriverInformationIssueIdDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Issue_Id_Date");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationLastContract)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Last_Contract");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationLicenseDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_License_Date");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationLicenseExpiry)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_License_Expiry");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationLicenseImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_License_Image");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationLicenseNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_License_No");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationLicenseType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_License_Type")
                    .IsFixedLength();
                entity.Property(e => e.CrCasRenterPrivateDriverInformationKeyMobile)
                   .HasMaxLength(10)
                   .IsUnicode(false)
                   .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Key_Mobile");
                entity.Property(e => e.CrCasRenterPrivateDriverInformationMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Mobile");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationNationality)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Nationality")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterPrivateDriverInformationReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Reasons");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationSignature)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Signature");

                entity.Property(e => e.CrCasRenterPrivateDriverInformationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterPrivateDriverInformationTraveledDistance).HasColumnName("CR_Cas_Renter_Private_Driver_Information_Traveled_Distance");

                entity.HasOne(d => d.CrCasRenterPrivateDriverInformationGenderNavigation)
                    .WithMany(p => p.CrCasRenterPrivateDriverInformations)
                    .HasForeignKey(d => d.CrCasRenterPrivateDriverInformationGender)
                    .HasConstraintName("fk_CR_Cas_Renter_Private_CR_Mas_Sup_Renter_Gender");

                entity.HasOne(d => d.CrCasRenterPrivateDriverInformationIdtrypeNavigation)
                    .WithMany(p => p.CrCasRenterPrivateDriverInformations)
                    .HasForeignKey(d => d.CrCasRenterPrivateDriverInformationIdtrype)
                    .HasConstraintName("fk_CR_Cas_Renter_Private_CR_Mas_Sup_Renter_IDType");

                entity.HasOne(d => d.CrCasRenterPrivateDriverInformationLessorNavigation)
                    .WithMany(p => p.CrCasRenterPrivateDriverInformations)
                    .HasForeignKey(d => d.CrCasRenterPrivateDriverInformationLessor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Private_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrCasRenterPrivateDriverInformationLicenseTypeNavigation)
                    .WithMany(p => p.CrCasRenterPrivateDriverInformations)
                    .HasForeignKey(d => d.CrCasRenterPrivateDriverInformationLicenseType)
                    .HasConstraintName("fk_CR_Cas_Renter_Private_CR_Mas_Sup_Renter_Driving_License");

                entity.HasOne(d => d.CrCasRenterPrivateDriverInformationNationalityNavigation)
                    .WithMany(p => p.CrCasRenterPrivateDriverInformations)
                    .HasForeignKey(d => d.CrCasRenterPrivateDriverInformationNationality)
                    .HasConstraintName("fk_CR_Cas_Renter_Private_CR_Mas_Sup_Renter_Nationalities");
            });

            modelBuilder.Entity<CrCasSysAdministrativeProcedure>(entity =>
            {
                entity.HasKey(e => e.CrCasSysAdministrativeProceduresNo);

                entity.ToTable("CR_Cas_Sys_Administrative_Procedures");

                entity.HasIndex(e => e.CrCasSysAdministrativeProceduresCode, "IX_CR_Cas_Sys_Administrative_Procedures_CR_Cas_Sys_Administrative_Procedures_Code");

                entity.HasIndex(e => new { e.CrCasSysAdministrativeProceduresLessor, e.CrCasSysAdministrativeProceduresBranch }, "IX_CR_Cas_Sys_Administrative_Procedures_CR_Cas_Sys_Administrative_Procedures_Lessor_CR_Cas_Sys_Administrative_Procedures_Branch");

                entity.HasIndex(e => e.CrCasSysAdministrativeProceduresSector, "IX_CR_Cas_Sys_Administrative_Procedures_CR_Cas_Sys_Administrative_Procedures_Sector");

                entity.HasIndex(e => e.CrCasSysAdministrativeProceduresUserInsert, "IX_CR_Cas_Sys_Administrative_Procedures_CR_Cas_Sys_Administrative_Procedures_User_Insert");

                entity.Property(e => e.CrCasSysAdministrativeProceduresNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresArDescription)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Ar_Description");

                entity.Property(e => e.CrCasSysAdministrativeProceduresBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresCarFrom)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Car_From");

                entity.Property(e => e.CrCasSysAdministrativeProceduresCarTo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Car_To");

                entity.Property(e => e.CrCasSysAdministrativeProceduresClassification)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Classification")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresCreditor)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Creditor");

                entity.Property(e => e.CrCasSysAdministrativeProceduresDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Date");

                entity.Property(e => e.CrCasSysAdministrativeProceduresDebit)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Debit");

                entity.Property(e => e.CrCasSysAdministrativeProceduresDocDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Doc_Date");

                entity.Property(e => e.CrCasSysAdministrativeProceduresDocEndDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Doc_End_Date");

                entity.Property(e => e.CrCasSysAdministrativeProceduresDocNo)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Doc_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresDocStartDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Doc_Start_Date");

                entity.Property(e => e.CrCasSysAdministrativeProceduresEnDescription)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_En_Description");

                entity.Property(e => e.CrCasSysAdministrativeProceduresLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Reasons");

                entity.Property(e => e.CrCasSysAdministrativeProceduresSector)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Sector")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresTargeted)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Targeted")
                    .IsFixedLength();

                entity.Property(e => e.CrCasSysAdministrativeProceduresTime).HasColumnName("CR_Cas_Sys_Administrative_Procedures_Time");

                entity.Property(e => e.CrCasSysAdministrativeProceduresUserInsert)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_User_Insert");

                entity.Property(e => e.CrCasSysAdministrativeProceduresYear)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Sys_Administrative_Procedures_Year")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasSysAdministrativeProceduresCodeNavigation)
                    .WithMany(p => p.CrCasSysAdministrativeProcedures)
                    .HasForeignKey(d => d.CrCasSysAdministrativeProceduresCode)
                    .HasConstraintName("fk_CR_Cas_Sys_Administrative_Procedures_CR_Mas_Sys_Procedures");

                entity.HasOne(d => d.CrCasSysAdministrativeProceduresSectorNavigation)
                    .WithMany(p => p.CrCasSysAdministrativeProcedures)
                    .HasForeignKey(d => d.CrCasSysAdministrativeProceduresSector)
                    .HasConstraintName("fk_CR_Cas_Sys_Administrative_Procedures_CR_Mas_Sup_Renter_Sector");

                entity.HasOne(d => d.CrCasSysAdministrativeProceduresUserInsertNavigation)
                    .WithMany(p => p.CrCasSysAdministrativeProcedures)
                    .HasForeignKey(d => d.CrCasSysAdministrativeProceduresUserInsert)
                    .HasConstraintName("fk_CR_Cas_Sys_Administrative_Procedures_CR_Mas_User_Information");

                entity.HasOne(d => d.CrCasSysAdministrativeProcedures)
                    .WithMany(p => p.CrCasSysAdministrativeProcedures)
                    .HasForeignKey(d => new { d.CrCasSysAdministrativeProceduresLessor, d.CrCasSysAdministrativeProceduresBranch })
                    .HasConstraintName("fk_CR_Cas_Sys_Administrative_Procedures_CR_Cas_Branch_Information");
            });

            modelBuilder.Entity<CrMasContractCompany>(entity =>
            {
                entity.HasKey(e => e.CrMasContractCompanyNo);

                entity.ToTable("CR_Mas_Contract_Company");

                entity.HasIndex(e => e.CrMasContractCompanyLessor, "IX_CR_Mas_Contract_Company_CR_Mas_Contract_Company_Lessor");

                entity.HasIndex(e => e.CrMasContractCompanyProcedures, "IX_CR_Mas_Contract_Company_CR_Mas_Contract_Company_Procedures");

                entity.HasIndex(e => e.CrMasContractCompanySector, "IX_CR_Mas_Contract_Company_CR_Mas_Contract_Company_Sector");

                entity.Property(e => e.CrMasContractCompanyNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_No")
                    .IsFixedLength();

                entity.Property(e => e.CrMasContractCompanyAboutToExpire)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Contract_Company_About_To_Expire");

                entity.Property(e => e.CrMasContractCompanyActivation)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Activation")
                    .IsFixedLength();

                entity.Property(e => e.CrMasContractCompanyAnnualFees)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_Contract_Company_Annual_Fees");

                entity.Property(e => e.CrMasContractCompanyDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Contract_Company_Date");

                entity.Property(e => e.CrMasContractCompanyDiscountRate)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CR_Mas_Contract_Company_Discount_Rate");

                entity.Property(e => e.CrMasContractCompanyEndDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Contract_Company_End_Date");

                entity.Property(e => e.CrMasContractCompanyImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Contract_Company_Image");

                entity.Property(e => e.CrMasContractCompanyLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrMasContractCompanyNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Number");

                entity.Property(e => e.CrMasContractCompanyProcedures)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Procedures")
                    .IsFixedLength();

                entity.Property(e => e.CrMasContractCompanyProceduresClassification)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Procedures_Classification")
                    .IsFixedLength();

                entity.Property(e => e.CrMasContractCompanyReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Contract_Company_Reasons");

                entity.Property(e => e.CrMasContractCompanySector)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Sector")
                    .IsFixedLength();

                entity.Property(e => e.CrMasContractCompanyStartDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Contract_Company_Start_Date");

                entity.Property(e => e.CrMasContractCompanyStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasContractCompanyTaxRate)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CR_Mas_Contract_Company_Tax_Rate");

                entity.Property(e => e.CrMasContractCompanyUserId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_User_Id");

                entity.Property(e => e.CrMasContractCompanyUserPassword)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_User_Password");

                entity.Property(e => e.CrMasContractCompanyYear)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Year")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasContractCompanyLessorNavigation)
                    .WithMany(p => p.CrMasContractCompanies)
                    .HasForeignKey(d => d.CrMasContractCompanyLessor)
                    .HasConstraintName("Fk_CR_Mas_Contract_Company_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrMasContractCompanyProceduresNavigation)
                    .WithMany(p => p.CrMasContractCompanies)
                    .HasForeignKey(d => d.CrMasContractCompanyProcedures)
                    .HasConstraintName("Fk_CR_Mas_Contract_Company_CR_Mas_Sys_Procedures");

                entity.HasOne(d => d.CrMasContractCompanySectorNavigation)
                    .WithMany(p => p.CrMasContractCompanies)
                    .HasForeignKey(d => d.CrMasContractCompanySector)
                    .HasConstraintName("Fk_CR_Mas_Contract_Company");
            });

            modelBuilder.Entity<CrMasContractCompanyDetailed>(entity =>
            {
                entity.HasKey(e => new { e.CrMasContractCompanyDetailedNo, e.CrMasContractCompanyDetailedSer });

                entity.ToTable("CR_Mas_Contract_Company_Detailed");

                entity.Property(e => e.CrMasContractCompanyDetailedNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Contract_Company_Detailed_No")
                    .IsFixedLength();

                entity.Property(e => e.CrMasContractCompanyDetailedSer).HasColumnName("CR_Mas_Contract_Company_Detailed_Ser");

                entity.Property(e => e.CrMasContractCompanyDetailedFromPrice)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_Contract_Company_Detailed_From_Price");

                entity.Property(e => e.CrMasContractCompanyDetailedToPrice)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_Contract_Company_Detailed_To_Price");

                entity.Property(e => e.CrMasContractCompanyDetailedValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_Contract_Company_Detailed_Value");

                entity.HasOne(d => d.CrMasContractCompanyDetailedNoNavigation)
                    .WithMany(p => p.CrMasContractCompanyDetaileds)
                    .HasForeignKey(d => d.CrMasContractCompanyDetailedNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Mas_Contract_Company_Detailed_CR_Mas_Contract_Company");
            });

            modelBuilder.Entity<CrMasLessorImage>(entity =>
            {
                entity.HasKey(e => e.CrMasLessorImageCode);

                entity.ToTable("CR_Mas_Lessor_Image");

                entity.Property(e => e.CrMasLessorImageCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Image_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorImageCloseContractEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Close_Contract_Email");

                entity.Property(e => e.CrMasLessorImageCloseContractWhatUp)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Close_Contract_WhatUp");

                entity.Property(e => e.CrMasLessorImageContArConditions1)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Cont_Ar_Conditions_1");

                entity.Property(e => e.CrMasLessorImageContArConditions2)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Cont_Ar_Conditions_2");

                entity.Property(e => e.CrMasLessorImageContArConditions3)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Cont_Ar_Conditions_3");

                entity.Property(e => e.CrMasLessorImageContEnConditions1)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Cont_En_Conditions_1");

                entity.Property(e => e.CrMasLessorImageContEnConditions2)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Cont_En_Conditions_2");

                entity.Property(e => e.CrMasLessorImageContEnConditions3)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Cont_En_Conditions_3");

                entity.Property(e => e.CrMasLessorImageCreateContractEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Create_Contract_Email");

                entity.Property(e => e.CrMasLessorImageCreateContractWhatUp)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Create_Contract_WhatUp");

                entity.Property(e => e.CrMasLessorImageEndContractEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_End_Contract_Email");

                entity.Property(e => e.CrMasLessorImageEndContractWhatUp)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_End_Contract_WhatUp");

                entity.Property(e => e.CrMasLessorImageHourContractEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Hour_Contract_Email");

                entity.Property(e => e.CrMasLessorImageHourContractWhatUp)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Hour_Contract_WhatUp");

                entity.Property(e => e.CrMasLessorImageLogo)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Logo");

                entity.Property(e => e.CrMasLessorImageLogoPrint)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Logo_Print");

                entity.Property(e => e.CrMasLessorImageSignatureDirector)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Signature_Director");

                entity.Property(e => e.CrMasLessorImageStamp)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Stamp");

                entity.Property(e => e.CrMasLessorImageStampFullAmountPaid)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Stamp_Full_Amount_Paid");

                entity.Property(e => e.CrMasLessorImageStampOutsideCity)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Stamp_Outside_City");

                entity.Property(e => e.CrMasLessorImageStampOutsideCountry)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Stamp_Outside_Country");

                entity.Property(e => e.CrMasLessorImageTomorrowContractEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Tomorrow_Contract_Email");

                entity.Property(e => e.CrMasLessorImageTomorrowContractWhatUp)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Image_Tomorrow_Contract_WhatUp");

                entity.HasOne(d => d.CrMasLessorImageCodeNavigation)
                    .WithOne(p => p.CrMasLessorImage)
                    .HasForeignKey<CrMasLessorImage>(d => d.CrMasLessorImageCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_Lessor_Image_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasLessorInformation>(entity =>
            {
                entity.HasKey(e => e.CrMasLessorInformationCode);

                entity.ToTable("CR_Mas_Lessor_Information");

                entity.HasIndex(e => e.CrMasLessorInformationClassification, "IX_CR_Mas_Lessor_Information_CR_Mas_Lessor_Information_Classification");

                entity.Property(e => e.CrMasLessorInformationCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorInformationAccount)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_Account");

                entity.Property(e => e.CrMasLessorInformationArLongName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_Ar_Long_Name");

                entity.Property(e => e.CrMasLessorInformationArShortName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Lessor_Information_Ar_Short_Name");

                entity.Property(e => e.CrMasLessorInformationCallFree)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Call_Free")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorInformationCallFreeKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Call_Free_Key");

                entity.Property(e => e.CrMasLessorInformationClassification)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Classification")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorInformationCommunicationMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Communication_Mobile")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorInformationCommunicationMobileKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Communication_Mobile_Key");

                entity.Property(e => e.CrMasLessorInformationContEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_Cont_Email");

                entity.Property(e => e.CrMasLessorInformationContWhatsapp)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Cont_Whatsapp")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorInformationContWhatsappKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Cont_Whatsapp_Key");

                entity.Property(e => e.CrMasLessorInformationDirectorArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Lessor_Information_Director_Ar_Name");

                entity.Property(e => e.CrMasLessorInformationDirectorEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Lessor_Information_Director_En_Name");

                entity.Property(e => e.CrMasLessorInformationEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_Email");

                entity.Property(e => e.CrMasLessorInformationEnLongName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_En_Long_Name");

                entity.Property(e => e.CrMasLessorInformationEnShortName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Lessor_Information_En_Short_Name");

                entity.Property(e => e.CrMasLessorInformationFaceBook)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_FaceBook");

                entity.Property(e => e.CrMasLessorInformationGovernmentNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Government_No")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorInformationInstagram)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_Instagram");

                entity.Property(e => e.CrMasLessorInformationLocation)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_Location");

                entity.Property(e => e.CrMasLessorInformationReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_Reasons");

                entity.Property(e => e.CrMasLessorInformationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorInformationTaxNo)
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Lessor_Information_Tax_No")
                    .IsFixedLength();

                entity.Property(e => e.CrMasLessorInformationTwiter)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Lessor_Information_Twiter");

                entity.HasOne(d => d.CrMasLessorInformationClassificationNavigation)
                    .WithMany(p => p.CrMasLessorInformations)
                    .HasForeignKey(d => d.CrMasLessorInformationClassification)
                    .HasConstraintName("CR_Mas_Lessor_Information_CR_Cas_Lessor_Classification");
            });

            modelBuilder.Entity<CrMasSupAccountBank>(entity =>
            {
                entity.HasKey(e => e.CrMasSupAccountBankCode);

                entity.ToTable("CR_Mas_Sup_Account_Bank");

                entity.Property(e => e.CrMasSupAccountBankCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Account_Bank_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupAccountBankArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Account_Bank_Ar_Name");

                entity.Property(e => e.CrMasSupAccountBankEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Account_Bank_En_Name");

                entity.Property(e => e.CrMasSupAccountBankReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Account_Bank_Reasons");

                entity.Property(e => e.CrMasSupAccountBankStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Account_Bank_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupAccountPaymentMethod>(entity =>
            {
                entity.HasKey(e => e.CrMasSupAccountPaymentMethodCode);

                entity.ToTable("CR_Mas_Sup_Account_Payment_Method");

                entity.Property(e => e.CrMasSupAccountPaymentMethodCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupAccountPaymentMethodAcceptImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Accept_Image");

                entity.Property(e => e.CrMasSupAccountPaymentMethodArName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Ar_Name");

                entity.Property(e => e.CrMasSupAccountPaymentMethodClassification)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Classification")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupAccountPaymentMethodEnName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_En_Name");

                entity.Property(e => e.CrMasSupAccountPaymentMethodReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Reasons");

                entity.Property(e => e.CrMasSupAccountPaymentMethodRejectImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Reject_Image");

                entity.Property(e => e.CrMasSupAccountPaymentMethodStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupAccountReference>(entity =>
            {
                entity.HasKey(e => e.CrMasSupAccountReceiptReferenceCode);

                entity.ToTable("CR_Mas_Sup_Account_Reference");

                entity.Property(e => e.CrMasSupAccountReceiptReferenceCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Account_Receipt_Reference_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupAccountPaymentMethodReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Reasons");

                entity.Property(e => e.CrMasSupAccountPaymentMethodStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Account_Payment_Method_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupAccountReceiptReferenceArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Account_Receipt_Reference_Ar_Name");

                entity.Property(e => e.CrMasSupAccountReceiptReferenceEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Account_Receipt_Reference_En_Name");
            });

            modelBuilder.Entity<CrMasSupCarAdvantage>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarAdvantagesCode);

                entity.ToTable("CR_Mas_Sup_Car_Advantages");

                entity.Property(e => e.CrMasSupCarAdvantagesCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Advantages_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarAdvantagesArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Advantages_Ar_Name");

                entity.Property(e => e.CrMasSupCarAdvantagesEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Advantages_En_Name");

                entity.Property(e => e.CrMasSupCarAdvantagesReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Advantages_Reasons");

                entity.Property(e => e.CrMasSupCarAdvantagesStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Advantages_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupCarBrand>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarBrandCode);

                entity.ToTable("CR_Mas_Sup_Car_Brand");

                entity.Property(e => e.CrMasSupCarBrandCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Brand_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarBrandArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Brand_Ar_Name");

                entity.Property(e => e.CrMasSupCarBrandEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Brand_En_Name");

                entity.Property(e => e.CrMasSupCarBrandReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Brand_Reasons");

                entity.Property(e => e.CrMasSupCarBrandStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Brand_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupCarCategory>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarCategoryCode);

                entity.ToTable("CR_Mas_Sup_Car_Category");

                entity.HasIndex(e => e.CrMasSupCarCategoryGroup, "IX_CR_Mas_Sup_Car_Category_CR_Mas_Sup_Car_Category_Group");

                entity.Property(e => e.CrMasSupCarCategoryCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Category_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarCategoryArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Car_Category_Ar_Name");

                entity.Property(e => e.CrMasSupCarCategoryEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Car_Category_En_Name");

                entity.Property(e => e.CrMasSupCarCategoryGroup)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Category_Group")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarCategoryReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Category_Reasons");

                entity.Property(e => e.CrMasSupCarCategoryStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Category_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupCarCategoryGroupNavigation)
                    .WithMany(p => p.CrMasSupCarCategories)
                    .HasForeignKey(d => d.CrMasSupCarCategoryGroup)
                    .HasConstraintName("FK_CR_Mas_Sup_Car_Category_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupCarColor>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarColorCode);

                entity.ToTable("CR_Mas_Sup_Car_Color");

                entity.Property(e => e.CrMasSupCarColorCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Color_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarColorArName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sup_Car_Color_Ar_Name");

                entity.Property(e => e.CrMasSupCarColorCounter).HasColumnName("CR_Mas_Sup_Car_Color_Counter");

                entity.Property(e => e.CrMasSupCarColorEnName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sup_Car_Color_En_Name");

                entity.Property(e => e.CrMasSupCarColorImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Color_Image");

                entity.Property(e => e.CrMasSupCarColorReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Color_Reasons");

                entity.Property(e => e.CrMasSupCarColorStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Color_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupCarCvt>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarCvtCode);

                entity.ToTable("CR_Mas_Sup_Car_CVT");

                entity.Property(e => e.CrMasSupCarCvtCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_CVT_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarCvtArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_CVT_Ar_Name");

                entity.Property(e => e.CrMasSupCarCvtEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_CVT_En_Name");

                entity.Property(e => e.CrMasSupCarCvtImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_CVT_Image");

                entity.Property(e => e.CrMasSupCarCvtReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_CVT_Reasons");

                entity.Property(e => e.CrMasSupCarCvtStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_CVT_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupCarDistribution>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarDistributionCode)
                    .HasName("PK_CR_Mas_Sup_Car_Distribution_1");

                entity.ToTable("CR_Mas_Sup_Car_Distribution");

                entity.HasIndex(e => e.CrMasSupCarDistributionBrand, "IX_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Distribution_Brand");

                entity.HasIndex(e => e.CrMasSupCarDistributionCategory, "IX_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Distribution_Category");

                entity.HasIndex(e => e.CrMasSupCarDistributionModel, "IX_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Distribution_Model");

                entity.Property(e => e.CrMasSupCarDistributionCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarDistributionBagBags).HasColumnName("CR_Mas_Sup_Car_Distribution_Bag_Bags");

                entity.Property(e => e.CrMasSupCarDistributionBrand)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Brand")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarDistributionCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Category")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarDistributionConcatenateArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Concatenate_Ar_Name");

                entity.Property(e => e.CrMasSupCarDistributionConcatenateEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Concatenate_En_Name");

                entity.Property(e => e.CrMasSupCarDistributionCount).HasColumnName("CR_Mas_Sup_Car_Distribution_Count");

                entity.Property(e => e.CrMasSupCarDistributionDoor).HasColumnName("CR_Mas_Sup_Car_Distribution_Door");

                entity.Property(e => e.CrMasSupCarDistributionImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Image");

                entity.Property(e => e.CrMasSupCarDistributionModel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Model")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarDistributionPassengers).HasColumnName("CR_Mas_Sup_Car_Distribution_Passengers");

                entity.Property(e => e.CrMasSupCarDistributionReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Reasons");

                entity.Property(e => e.CrMasSupCarDistributionSmallBags).HasColumnName("CR_Mas_Sup_Car_Distribution_Small_Bags");

                entity.Property(e => e.CrMasSupCarDistributionStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarDistributionYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Distribution_Year")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupCarDistributionBrandNavigation)
                    .WithMany(p => p.CrMasSupCarDistributions)
                    .HasForeignKey(d => d.CrMasSupCarDistributionBrand)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Brand");

                entity.HasOne(d => d.CrMasSupCarDistributionCategoryNavigation)
                    .WithMany(p => p.CrMasSupCarDistributions)
                    .HasForeignKey(d => d.CrMasSupCarDistributionCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Category");

                entity.HasOne(d => d.CrMasSupCarDistributionModelNavigation)
                    .WithMany(p => p.CrMasSupCarDistributions)
                    .HasForeignKey(d => d.CrMasSupCarDistributionModel)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Model");
            });

            modelBuilder.Entity<CrMasSupCarFuel>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarFuelCode);

                entity.ToTable("CR_Mas_Sup_Car_Fuel");

                entity.Property(e => e.CrMasSupCarFuelCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Fuel_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarFuelArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Fuel_Ar_Name");

                entity.Property(e => e.CrMasSupCarFuelEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Fuel_En_Name");

                entity.Property(e => e.CrMasSupCarFuelImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Fuel_Image");

                entity.Property(e => e.CrMasSupCarFuelReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Fuel_Reasons");

                entity.Property(e => e.CrMasSupCarFuelStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Fuel_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupCarModel>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarModelCode);

                entity.ToTable("CR_Mas_Sup_Car_Model");

                entity.HasIndex(e => e.CrMasSupCarModelBrand, "IX_CR_Mas_Sup_Car_Model_CR_Mas_Sup_Car_Model_Brand");

                entity.HasIndex(e => e.CrMasSupCarModelGroup, "IX_CR_Mas_Sup_Car_Model_CR_Mas_Sup_Car_Model_Group");

                entity.Property(e => e.CrMasSupCarModelCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Model_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarModelArConcatenateName)
                    .HasMaxLength(60)
                    .HasColumnName("CR_Mas_Sup_Car_Model_Ar_Concatenate_Name");

                entity.Property(e => e.CrMasSupCarModelArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Model_Ar_Name");

                entity.Property(e => e.CrMasSupCarModelBrand)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Model_Brand")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarModelConcatenateEnName)
                    .HasMaxLength(60)
                    .HasColumnName("CR_Mas_Sup_Car_Model_Concatenate_En_Name");

                entity.Property(e => e.CrMasSupCarModelCounter).HasColumnName("CR_Mas_Sup_Car_Model_Counter");

                entity.Property(e => e.CrMasSupCarModelEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Model_En_Name");

                entity.Property(e => e.CrMasSupCarModelGroup)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Model_Group")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarModelReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Model_Reasons");

                entity.Property(e => e.CrMasSupCarModelStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Model_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupCarModelBrandNavigation)
                    .WithMany(p => p.CrMasSupCarModels)
                    .HasForeignKey(d => d.CrMasSupCarModelBrand)
                    .HasConstraintName("FK_CR_Mas_Sup_Car_Model_CR_Mas_Sup_Car_Brand");

                entity.HasOne(d => d.CrMasSupCarModelGroupNavigation)
                    .WithMany(p => p.CrMasSupCarModels)
                    .HasForeignKey(d => d.CrMasSupCarModelGroup)
                    .HasConstraintName("FK_CR_Mas_Sup_Car_Model_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupCarRegistration>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarRegistrationCode);

                entity.ToTable("CR_Mas_Sup_Car_Registration");

                entity.Property(e => e.CrMasSupCarRegistrationCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Registration_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarRegistrationArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Registration_Ar_Name");

                entity.Property(e => e.CrMasSupCarRegistrationEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Car_Registration_En_Name");

                entity.Property(e => e.CrMasSupCarRegistrationReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Registration_Reasons");

                entity.Property(e => e.CrMasSupCarRegistrationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Registration_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupCarYear>(entity =>
            {
                entity.HasKey(e => e.CrMasSupCarYearCode);

                entity.ToTable("CR_Mas_Sup_Car_Year");

                entity.HasIndex(e => e.CrMasSupCarYearGroup, "IX_CR_Mas_Sup_Car_Year_CR_Mas_Sup_Car_Year_Group");

                entity.Property(e => e.CrMasSupCarYearCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Year_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarYearGroup)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Year_Group")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupCarYearNo)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Car_Year_No")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupCarYearGroupNavigation)
                    .WithMany(p => p.CrMasSupCarYears)
                    .HasForeignKey(d => d.CrMasSupCarYearGroup)
                    .HasConstraintName("FK_CR_Mas_Sup_Car_Year_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupContractAdditional>(entity =>
            {
                entity.HasKey(e => e.CrMasSupContractAdditionalCode);

                entity.ToTable("CR_Mas_Sup_Contract_Additional");

                entity.HasIndex(e => e.CrMasSupContractAdditionalGroup, "IX_CR_Mas_Sup_Contract_Additional_CR_Mas_Sup_Contract_Additional_Group");

                entity.Property(e => e.CrMasSupContractAdditionalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupContractAdditionalAcceptImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_Accept_Image");

                entity.Property(e => e.CrMasSupContractAdditionalArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_Ar_Name");

                entity.Property(e => e.CrMasSupContractAdditionalBlockImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_Block_Image");

                entity.Property(e => e.CrMasSupContractAdditionalEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_En_Name");

                entity.Property(e => e.CrMasSupContractAdditionalGroup)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_Group")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupContractAdditionalReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_Reasons");

                entity.Property(e => e.CrMasSupContractAdditionalRejectImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_Reject_Image");

                entity.Property(e => e.CrMasSupContractAdditionalStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Contract_Additional_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupContractAdditionalGroupNavigation)
                    .WithMany(p => p.CrMasSupContractAdditionals)
                    .HasForeignKey(d => d.CrMasSupContractAdditionalGroup)
                    .HasConstraintName("FK_CR_Mas_Sup_Contract_Additional_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupContractCarCheckup>(entity =>
            {
                entity.HasKey(e => e.CrMasSupContractCarCheckupCode);

                entity.ToTable("CR_Mas_Sup_Contract_Car_Checkup");

                entity.Property(e => e.CrMasSupContractCarCheckupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Contract_Car_Checkup_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupContractCarCheckupAcceptImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Car_Checkup_Accept_Image");

                entity.Property(e => e.CrMasSupContractCarCheckupArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Contract_Car_Checkup_Ar_Name");

                entity.Property(e => e.CrMasSupContractCarCheckupBlockImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Car_Checkup_Block_Image");

                entity.Property(e => e.CrMasSupContractCarCheckupEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Contract_Car_Checkup_En_Name");

                entity.Property(e => e.CrMasSupContractCarCheckupReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Car_Checkup_Reasons");

                entity.Property(e => e.CrMasSupContractCarCheckupRejectImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Car_Checkup_Reject_Image");

                entity.Property(e => e.CrMasSupContractCarCheckupStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Contract_Car_Checkup_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupContractOption>(entity =>
            {
                entity.HasKey(e => e.CrMasSupContractOptionsCode);

                entity.ToTable("CR_Mas_Sup_Contract_Options");

                entity.HasIndex(e => e.CrMasSupContractOptionsGroup, "IX_CR_Mas_Sup_Contract_Options_CR_Mas_Sup_Contract_Options_Group");

                entity.Property(e => e.CrMasSupContractOptionsCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupContractOptionsAcceptImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_Accept_Image");

                entity.Property(e => e.CrMasSupContractOptionsArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_Ar_Name");

                entity.Property(e => e.CrMasSupContractOptionsBlockImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_Block_Image");

                entity.Property(e => e.CrMasSupContractOptionsEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_En_Name");

                entity.Property(e => e.CrMasSupContractOptionsGroup)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_Group")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupContractOptionsReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_Reasons");

                entity.Property(e => e.CrMasSupContractOptionsRejectImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_Reject_Image");

                entity.Property(e => e.CrMasSupContractOptionsStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Contract_Options_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupContractOptionsGroupNavigation)
                    .WithMany(p => p.CrMasSupContractOptions)
                    .HasForeignKey(d => d.CrMasSupContractOptionsGroup)
                    .HasConstraintName("FK_CR_Mas_Sup_Contract_Options_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupMembershipChoice>(entity =>
            {
                entity.HasKey(e => e.CrMasSupMembershipChoiceCode);

                entity.ToTable("CR_Mas_Sup_Membership_Choice");

                entity.Property(e => e.CrMasSupMembershipChoiceCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Membership_Choice_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupMembershipChoiceGroup)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Membership_Choice_Group")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupMembershipChoiceStetment)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Membership_Choice_Stetment");
            });

            modelBuilder.Entity<CrMasSupPostCity>(entity =>
            {
                entity.HasKey(e => e.CrMasSupPostCityCode);

                entity.ToTable("CR_Mas_Sup_Post_City");

                entity.HasIndex(e => e.CrMasSupPostCityGroupCode, "IX_CR_Mas_Sup_Post_City_CR_Mas_Sup_Post_City_Group_Code");

                entity.HasIndex(e => e.CrMasSupPostCityRegionsCode, "IX_CR_Mas_Sup_Post_City_CR_Mas_Sup_Post_City_Regions_Code");

                entity.Property(e => e.CrMasSupPostCityCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Post_City_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupPostCityArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Post_City_Ar_Name");

                entity.Property(e => e.CrMasSupPostCityConcatenateArName)
                    .HasMaxLength(60)
                    .HasColumnName("CR_Mas_Sup_Post_City_Concatenate_Ar_Name");

                entity.Property(e => e.CrMasSupPostCityConcatenateEnName)
                    .HasMaxLength(60)
                    .HasColumnName("CR_Mas_Sup_Post_City_Concatenate_En_Name");

                entity.Property(e => e.CrMasSupPostCityCounter).HasColumnName("CR_Mas_Sup_Post_City_Counter");

                entity.Property(e => e.CrMasSupPostCityEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Post_City_En_Name");

                entity.Property(e => e.CrMasSupPostCityGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Post_City_Group_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupPostCityLatitude).HasColumnName("CR_Mas_Sup_Post_City_Latitude");

                entity.Property(e => e.CrMasSupPostCityLocation).HasColumnName("CR_Mas_Sup_Post_City_Location");

                entity.Property(e => e.CrMasSupPostCityLongitude).HasColumnName("CR_Mas_Sup_Post_City_Longitude");

                entity.Property(e => e.CrMasSupPostCityReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Post_City_Reasons");

                entity.Property(e => e.CrMasSupPostCityRegionsCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Post_City_Regions_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupPostCityRegionsStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Post_City_Regions_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupPostCityStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Post_City_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupPostCityGroupCodeNavigation)
                    .WithMany(p => p.CrMasSupPostCities)
                    .HasForeignKey(d => d.CrMasSupPostCityGroupCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Post_City_CR_Mas_Sys_Group");

                entity.HasOne(d => d.CrMasSupPostCityRegionsCodeNavigation)
                    .WithMany(p => p.CrMasSupPostCities)
                    .HasForeignKey(d => d.CrMasSupPostCityRegionsCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Post_City_CR_Mas_Sup_Post_Regions");
            });

            modelBuilder.Entity<CrMasSupPostRegion>(entity =>
            {
                entity.HasKey(e => e.CrMasSupPostRegionsCode);

                entity.ToTable("CR_Mas_Sup_Post_Regions");

                entity.Property(e => e.CrMasSupPostRegionsCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Post_Regions_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupPostRegionsArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Post_Regions_Ar_Name");

                entity.Property(e => e.CrMasSupPostRegionsEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Post_Regions_En_Name");

                entity.Property(e => e.CrMasSupPostRegionsLatitude).HasColumnName("CR_Mas_Sup_Post_Regions_Latitude");

                entity.Property(e => e.CrMasSupPostRegionsLocation)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Post_Regions_Location");

                entity.Property(e => e.CrMasSupPostRegionsLongitude).HasColumnName("CR_Mas_Sup_Post_Regions_Longitude");

                entity.Property(e => e.CrMasSupPostRegionsReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Post_Regions_Reasons");

                entity.Property(e => e.CrMasSupPostRegionsStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Post_Regions_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupRenterAge>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterAgeCode);

                entity.ToTable("CR_Mas_Sup_Renter_Age");

                entity.HasIndex(e => e.CrMasSupRenterAgeGroupCode, "IX_CR_Mas_Sup_Renter_Age_CR_Mas_Sup_Renter_Age_Group_Code");

                entity.Property(e => e.CrMasSupRenterAgeCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Age_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterAgeGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Age_Group_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterAgeNo).HasColumnName("CR_Mas_Sup_Renter_Age_No");

                entity.HasOne(d => d.CrMasSupRenterAgeGroupCodeNavigation)
                    .WithMany(p => p.CrMasSupRenterAges)
                    .HasForeignKey(d => d.CrMasSupRenterAgeGroupCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Renter_Age_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupRenterDrivingLicense>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterDrivingLicenseCode);

                entity.ToTable("CR_Mas_Sup_Renter_Driving_License");

                entity.Property(e => e.CrMasSupRenterDrivingLicenseCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Driving_License_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterDrivingLicenseArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_Driving_License_Ar_Name");

                entity.Property(e => e.CrMasSupRenterDrivingLicenseEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_Driving_License_En_Name");

                entity.Property(e => e.CrMasSupRenterDrivingLicenseReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Driving_License_Reasons");

                entity.Property(e => e.CrMasSupRenterDrivingLicenseStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Driving_License_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupRenterEmployer>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterEmployerCode);

                entity.ToTable("CR_Mas_Sup_Renter_Employer");

                entity.HasIndex(e => e.CrMasSupRenterEmployerGroupCode, "IX_CR_Mas_Sup_Renter_Employer_CR_Mas_Sup_Renter_Employer_Group_Code");

                entity.HasIndex(e => e.CrMasSupRenterEmployerSectorCode, "IX_CR_Mas_Sup_Renter_Employer_CR_Mas_Sup_Renter_Employer_Sector_Code");

                entity.Property(e => e.CrMasSupRenterEmployerCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Employer_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterEmployerArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Employer_Ar_Name");

                entity.Property(e => e.CrMasSupRenterEmployerCounter).HasColumnName("CR_Mas_Sup_Renter_Employer_Counter");

                entity.Property(e => e.CrMasSupRenterEmployerEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Employer_En_Name");

                entity.Property(e => e.CrMasSupRenterEmployerGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Employer_Group_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterEmployerReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Employer_Reasons");

                entity.Property(e => e.CrMasSupRenterEmployerSectorCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Employer_Sector_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterEmployerStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Employer_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupRenterEmployerGroupCodeNavigation)
                    .WithMany(p => p.CrMasSupRenterEmployers)
                    .HasForeignKey(d => d.CrMasSupRenterEmployerGroupCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Renter_Employer_CR_Mas_Sys_Group");

                entity.HasOne(d => d.CrMasSupRenterEmployerSectorCodeNavigation)
                    .WithMany(p => p.CrMasSupRenterEmployers)
                    .HasForeignKey(d => d.CrMasSupRenterEmployerSectorCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Renter_Employer_CR_Mas_Sup_Renter_Sector");
            });

            modelBuilder.Entity<CrMasSupRenterGender>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterGenderCode);

                entity.ToTable("CR_Mas_Sup_Renter_Gender");

                entity.HasIndex(e => e.CrMasSupRenterGenderGroupCode, "IX_CR_Mas_Sup_Renter_Gender_CR_Mas_Sup_Renter_Gender_Group_Code");

                entity.Property(e => e.CrMasSupRenterGenderCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Gender_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterGenderArName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sup_Renter_Gender_Ar_Name");

                entity.Property(e => e.CrMasSupRenterGenderEnName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sup_Renter_Gender_En_Name");

                entity.Property(e => e.CrMasSupRenterGenderGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Gender_Group_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterGenderReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Gender_Reasons");

                entity.Property(e => e.CrMasSupRenterGenderStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Gender_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupRenterGenderGroupCodeNavigation)
                    .WithMany(p => p.CrMasSupRenterGenders)
                    .HasForeignKey(d => d.CrMasSupRenterGenderGroupCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Renter_Gender_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupRenterIdtype>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterIdtypeCode);

                entity.ToTable("CR_Mas_Sup_Renter_IDType");

                entity.Property(e => e.CrMasSupRenterIdtypeCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_IDType_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterIdtypeArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_IDType_Ar_Name");

                entity.Property(e => e.CrMasSupRenterIdtypeEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_IDType_En_Name");

                entity.Property(e => e.CrMasSupRenterIdtypeReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_IDType_Reasons");

                entity.Property(e => e.CrMasSupRenterIdtypeStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_IDType_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSupRenterMembership>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterMembershipCode);

                entity.ToTable("CR_Mas_Sup_Renter_Membership");

                entity.HasIndex(e => e.CrMasSupRenterMembershipGroupCode, "IX_CR_Mas_Sup_Renter_Membership_CR_Mas_Sup_Renter_Membership_Group_Code");

                entity.Property(e => e.CrMasSupRenterMembershipCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Membership_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterMembershipAcceptPicture)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Membership_Accept_Picture");

                entity.Property(e => e.CrMasSupRenterMembershipArName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sup_Renter_Membership_Ar_Name");

                entity.Property(e => e.CrMasSupRenterMembershipEnName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sup_Renter_Membership_En_Name");

                entity.Property(e => e.CrMasSupRenterMembershipGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Membership_Group_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterMembershipReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Membership_Reasons");

                entity.Property(e => e.CrMasSupRenterMembershipRejectPicture)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Membership_Reject_Picture");

                entity.Property(e => e.CrMasSupRenterMembershipStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Membership_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupRenterMembershipGroupCodeNavigation)
                    .WithMany(p => p.CrMasSupRenterMemberships)
                    .HasForeignKey(d => d.CrMasSupRenterMembershipGroupCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Renter_Membership_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupRenterNationality>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterNationalitiesCode);

                entity.ToTable("CR_Mas_Sup_Renter_Nationalities");

                entity.HasIndex(e => e.CrMasSupRenterNationalitiesGroupCode, "IX_CR_Mas_Sup_Renter_Nationalities_CR_Mas_Sup_Renter_Nationalities_Group_Code");

                entity.Property(e => e.CrMasSupRenterNationalitiesCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Nationalities_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterNationalitiesArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_Nationalities_Ar_Name");

                entity.Property(e => e.CrMasSupRenterNationalitiesCounter).HasColumnName("CR_Mas_Sup_Renter_Nationalities_Counter");

                entity.Property(e => e.CrMasSupRenterNationalitiesEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_Nationalities_En_Name");

                entity.Property(e => e.CrMasSupRenterNationalitiesFlag)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Nationalities_Flag");

                entity.Property(e => e.CrMasSupRenterNationalitiesGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Nationalities_Group_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterNationalitiesReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Nationalities_Reasons");

                entity.Property(e => e.CrMasSupRenterNationalitiesStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Nationalities_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupRenterNationalitiesGroupCodeNavigation)
                    .WithMany(p => p.CrMasSupRenterNationalities)
                    .HasForeignKey(d => d.CrMasSupRenterNationalitiesGroupCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Renter_Nationalities_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupRenterProfession>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterProfessionsCode);

                entity.ToTable("CR_Mas_Sup_Renter_Professions");

                entity.HasIndex(e => e.CrMasSupRenterProfessionsGroupCode, "IX_CR_Mas_Sup_Renter_Professions_CR_Mas_Sup_Renter_Professions_Group_Code");

                entity.Property(e => e.CrMasSupRenterProfessionsCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Professions_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterProfessionsArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_Professions_Ar_Name");

                entity.Property(e => e.CrMasSupRenterProfessionsEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_Professions_En_Name");

                entity.Property(e => e.CrMasSupRenterProfessionsGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Professions_Group_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterProfessionsReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Professions_Reasons");

                entity.Property(e => e.CrMasSupRenterProfessionsStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Professions_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSupRenterProfessionsGroupCodeNavigation)
                    .WithMany(p => p.CrMasSupRenterProfessions)
                    .HasForeignKey(d => d.CrMasSupRenterProfessionsGroupCode)
                    .HasConstraintName("FK_CR_Mas_Sup_Renter_Professions_CR_Mas_Sys_Group");
            });

            modelBuilder.Entity<CrMasSupRenterSector>(entity =>
            {
                entity.HasKey(e => e.CrMasSupRenterSectorCode);

                entity.ToTable("CR_Mas_Sup_Renter_Sector");

                entity.Property(e => e.CrMasSupRenterSectorCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Sector_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSupRenterSectorArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_Sector_Ar_Name");

                entity.Property(e => e.CrMasSupRenterSectorEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sup_Renter_Sector_En_Name");

                entity.Property(e => e.CrMasSupRenterSectorReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Renter_Sector_Reasons");

                entity.Property(e => e.CrMasSupRenterSectorStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sup_Renter_Sector_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSysCallingKey>(entity =>
            {
                entity.HasKey(e => e.CrMasSysCallingKeysCode);

                entity.ToTable("CR_Mas_Sys_Calling_Keys");

                entity.Property(e => e.CrMasSysCallingKeysCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Calling_Keys_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysCallingKeysArName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sys_Calling_Keys_Ar_Name");

                entity.Property(e => e.CrMasSysCallingKeysCount).HasColumnName("CR_Mas_Sys_Calling_Keys_Count");

                entity.Property(e => e.CrMasSysCallingKeysEnName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sys_Calling_Keys_En_Name");

                entity.Property(e => e.CrMasSysCallingKeysFlag)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Calling_Keys_Flag");

                entity.Property(e => e.CrMasSysCallingKeysNo)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Mas_Sys_Calling_Keys_No")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysCallingKeysReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Calling_Keys_Reasons");

                entity.Property(e => e.CrMasSysCallingKeysStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Calling_Keys_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSysEvaluation>(entity =>
            {
                entity.HasKey(e => e.CrMasSysEvaluationsCode);

                entity.ToTable("CR_Mas_Sys_Evaluation");

                entity.Property(e => e.CrMasSysEvaluationsCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Evaluations_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysEvaluationsArDescription)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sys_Evaluations_Ar_Description");

                entity.Property(e => e.CrMasSysEvaluationsEnDescription)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sys_Evaluations_En_Description");

                entity.Property(e => e.CrMasSysEvaluationsImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Evaluations_Image");

                entity.Property(e => e.CrMasSysEvaluationsReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Evaluations_Reasons");

                entity.Property(e => e.CrMasSysEvaluationsStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Evaluations_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysServiceEvaluationsValue).HasColumnName("CR_Mas_Sys_Service_Evaluations_Value");
            });

            modelBuilder.Entity<CrMasSysGroup>(entity =>
            {
                entity.HasKey(e => e.CrMasSysGroupCode);

                entity.ToTable("CR_Mas_Sys_Group");

                entity.Property(e => e.CrMasSysGroupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Group_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysGroupArName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sys_Group_Ar_Name");

                entity.Property(e => e.CrMasSysGroupClassified).HasColumnName("CR_Mas_Sys_Group_Classified");

                entity.Property(e => e.CrMasSysGroupEnName)
                    .HasMaxLength(30)
                    .HasColumnName("CR_Mas_Sys_Group_En_Name");

                entity.Property(e => e.CrMasSysGroupIndependent).HasColumnName("CR_Mas_Sys_Group_Independent");

                entity.Property(e => e.CrMasSysGroupReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Group_Reasons");

                entity.Property(e => e.CrMasSysGroupStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Group_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSysMainTask>(entity =>
            {
                entity.HasKey(e => e.CrMasSysMainTasksCode);

                entity.ToTable("CR_Mas_Sys_Main_Tasks");

                entity.HasIndex(e => e.CrMasSysMainTasksSystem, "IX_CR_Mas_Sys_Main_Tasks_CR_Mas_Sys_Main_Tasks_System");

                entity.Property(e => e.CrMasSysMainTasksCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Main_Tasks_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysMainTasksArName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sys_Main_Tasks_Ar_Name");

                entity.Property(e => e.CrMasSysMainTasksConcatenateArName)
                    .HasMaxLength(40)
                    .HasColumnName("CR_Mas_Sys_Main_Tasks_Concatenate_Ar_Name");

                entity.Property(e => e.CrMasSysMainTasksConcatenateEnName)
                    .HasMaxLength(40)
                    .HasColumnName("CR_Mas_Sys_Main_Tasks_Concatenate_En_Name");

                entity.Property(e => e.CrMasSysMainTasksEnName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sys_Main_Tasks_En_Name");

                entity.Property(e => e.CrMasSysMainTasksReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Main_Tasks_Reasons");

                entity.Property(e => e.CrMasSysMainTasksStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Main_Tasks_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysMainTasksSystem)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Main_Tasks_System")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSysMainTasksSystemNavigation)
                    .WithMany(p => p.CrMasSysMainTasks)
                    .HasForeignKey(d => d.CrMasSysMainTasksSystem)
                    .HasConstraintName("FK_CR_Mas_Sys_Main_Tasks_CR_Mas_Sys_System");
            });

            modelBuilder.Entity<CrMasSysProcedure>(entity =>
            {
                entity.HasKey(e => e.CrMasSysProceduresCode);

                entity.ToTable("CR_Mas_Sys_Procedures");

                entity.Property(e => e.CrMasSysProceduresCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Procedures_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysProceduresArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Procedures_Ar_Name");

                entity.Property(e => e.CrMasSysProceduresClassification)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Procedures_Classification")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysProceduresDaysAlertAboutExpire).HasColumnName("CR_Mas_Sys_Procedures_Days_Alert_About_Expire");

                entity.Property(e => e.CrMasSysProceduresEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Procedures_En_Name");

                entity.Property(e => e.CrMasSysProceduresKmAlertAboutExpire).HasColumnName("CR_Mas_Sys_Procedures_KM_Alert_About_Expire");

                entity.Property(e => e.CrMasSysProceduresReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Procedures_Reasons");

                entity.Property(e => e.CrMasSysProceduresStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Procedures_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysProceduresSubjectAlert).HasColumnName("CR_Mas_Sys_Procedures_Subject_Alert");
            });

            modelBuilder.Entity<CrMasSysProceduresTask>(entity =>
            {
                entity.HasKey(e => e.CrMasSysProceduresTasksSubTasks);

                entity.ToTable("CR_Mas_Sys_Procedures_Tasks");

                entity.HasIndex(e => e.CrMasSysProceduresTasksMainTasks, "IX_CR_Mas_Sys_Procedures_Tasks_CR_Mas_Sys_Procedures_Tasks_Main_Tasks");

                entity.HasIndex(e => e.CrMasSysProceduresTasksSystem, "IX_CR_Mas_Sys_Procedures_Tasks_CR_Mas_Sys_Procedures_Tasks_System");

                entity.Property(e => e.CrMasSysProceduresTasksSubTasks)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_Sub_Tasks")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysProceduresTasksDeleteArName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_Delete_Ar_Name");

                entity.Property(e => e.CrMasSysProceduresTasksDeleteAvailable).HasColumnName("CR_Mas_Sys_Procedures_Tasks_Delete_Available");

                entity.Property(e => e.CrMasSysProceduresTasksDeleteEnName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_Delete_En_Name");

                entity.Property(e => e.CrMasSysProceduresTasksHoldArName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_Hold_Ar_Name");

                entity.Property(e => e.CrMasSysProceduresTasksHoldAvailable).HasColumnName("CR_Mas_Sys_Procedures_Tasks_Hold_Available");

                entity.Property(e => e.CrMasSysProceduresTasksHoldEnName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_Hold_En_Name");

                entity.Property(e => e.CrMasSysProceduresTasksInsertArName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_Insert_Ar_Name");

                entity.Property(e => e.CrMasSysProceduresTasksInsertAvailable).HasColumnName("CR_Mas_Sys_Procedures_Tasks_Insert_Available");

                entity.Property(e => e.CrMasSysProceduresTasksInsertEnName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_Insert_En_Name");

                entity.Property(e => e.CrMasSysProceduresTasksMainTasks)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_Main_Tasks")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysProceduresTasksSystem)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_System")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysProceduresTasksUnDeleteArName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_UnDelete_Ar_Name");

                entity.Property(e => e.CrMasSysProceduresTasksUnDeleteAvailable).HasColumnName("CR_Mas_Sys_Procedures_Tasks_UnDelete_Available");

                entity.Property(e => e.CrMasSysProceduresTasksUnDeleteEnName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_UnDelete_En_Name");

                entity.Property(e => e.CrMasSysProceduresTasksUnHoldArName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_UnHold_Ar_Name");

                entity.Property(e => e.CrMasSysProceduresTasksUnHoldAvailable).HasColumnName("CR_Mas_Sys_Procedures_Tasks_UnHold_Available");

                entity.Property(e => e.CrMasSysProceduresTasksUnHoldEnName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_UnHold_En_Name");

                entity.Property(e => e.CrMasSysProceduresTasksUpDateArName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_UpDate_Ar_Name");

                entity.Property(e => e.CrMasSysProceduresTasksUpDateAvailable).HasColumnName("CR_Mas_Sys_Procedures_Tasks_UpDate_Available");

                entity.Property(e => e.CrMasSysProceduresTasksUpDateEnName)
                    .HasMaxLength(25)
                    .HasColumnName("CR_Mas_Sys_Procedures_Tasks_UpDate_En_Name");

                entity.HasOne(d => d.CrMasSysProceduresTasksMainTasksNavigation)
                    .WithMany(p => p.CrMasSysProceduresTasks)
                    .HasForeignKey(d => d.CrMasSysProceduresTasksMainTasks)
                    .HasConstraintName("FK_CR_Mas_Sys_Procedures_Tasks_CR_Mas_Sys_Main_Tasks");

                entity.HasOne(d => d.CrMasSysProceduresTasksSystemNavigation)
                    .WithMany(p => p.CrMasSysProceduresTasks)
                    .HasForeignKey(d => d.CrMasSysProceduresTasksSystem)
                    .HasConstraintName("FK_CR_Mas_Sys_Procedures_Tasks_CR_Mas_Sys_System");
            });

            modelBuilder.Entity<CrMasSysStatus>(entity =>
            {
                entity.HasKey(e => e.CrMasSysStatusCode);

                entity.ToTable("CR_Mas_Sys_Status");

                entity.Property(e => e.CrMasSysStatusCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Status_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysStatusArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sys_Status_Ar_Name");

                entity.Property(e => e.CrMasSysStatusEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sys_Status_En_Name");

                entity.Property(e => e.CrMasSysStatusReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Status_Reasons");

                entity.Property(e => e.CrMasSysStatusStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Status_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasSysSubTask>(entity =>
            {
                entity.HasKey(e => e.CrMasSysSubTasksCode);

                entity.ToTable("CR_Mas_Sys_Sub_Tasks");

                entity.HasIndex(e => e.CrMasSysSubTasksMainCode, "IX_CR_Mas_Sys_Sub_Tasks_CR_Mas_Sys_Sub_Tasks_Main_Code");

                entity.HasIndex(e => e.CrMasSysSubTasksSystemCode, "IX_CR_Mas_Sys_Sub_Tasks_CR_Mas_Sys_Sub_Tasks_System_Code");

                entity.Property(e => e.CrMasSysSubTasksCode)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysSubTasksArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_Ar_Name");

                entity.Property(e => e.CrMasSysSubTasksConcatenateArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_Concatenate_Ar_Name");

                entity.Property(e => e.CrMasSysSubTasksConcatenateEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_Concatenate_En_Name");

                entity.Property(e => e.CrMasSysSubTasksEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_En_Name");

                entity.Property(e => e.CrMasSysSubTasksMainCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_Main_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysSubTasksProceduresExpanded).HasColumnName("CR_Mas_Sys_Sub_Tasks_Procedures_Expanded");

                entity.Property(e => e.CrMasSysSubTasksReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_Reasons");

                entity.Property(e => e.CrMasSysSubTasksStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysSubTasksSystemCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Sub_Tasks_System_Code")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasSysSubTasksMainCodeNavigation)
                    .WithMany(p => p.CrMasSysSubTasks)
                    .HasForeignKey(d => d.CrMasSysSubTasksMainCode)
                    .HasConstraintName("FK_CR_Mas_Sys_Sub_Tasks_CR_Mas_Sys_Main_Tasks");

                entity.HasOne(d => d.CrMasSysSubTasksSystemCodeNavigation)
                    .WithMany(p => p.CrMasSysSubTasks)
                    .HasForeignKey(d => d.CrMasSysSubTasksSystemCode)
                    .HasConstraintName("FK_CR_Mas_Sys_Sub_Tasks_CR_Mas_Sys_System");
            });

            modelBuilder.Entity<CrMasSysSystem>(entity =>
            {
                entity.HasKey(e => e.CrMasSysSystemCode);

                entity.ToTable("CR_Mas_Sys_System");

                entity.Property(e => e.CrMasSysSystemCode)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_System_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysSystemArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sys_System_Ar_Name");

                entity.Property(e => e.CrMasSysSystemEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Sys_System_En_Name");

                entity.Property(e => e.CrMasSysSystemReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_System_Reasons");

                entity.Property(e => e.CrMasSysSystemStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_System_Status")
                    .IsFixedLength();
            });

            modelBuilder.Entity<CrMasUserBranchValidity>(entity =>
            {
                entity.HasKey(e => new { e.CrMasUserBranchValidityId, e.CrMasUserBranchValidityLessor, e.CrMasUserBranchValidityBranch })
                    .HasName("PK_CR_Mas_User_Branch_Validity_1");

                entity.ToTable("CR_Mas_User_Branch_Validity");

                entity.HasIndex(e => new { e.CrMasUserBranchValidityLessor, e.CrMasUserBranchValidityBranch }, "IX_CR_Mas_User_Branch_Validity_CR_Mas_User_Branch_Validity_Lessor_CR_Mas_User_Branch_Validity_Branch");

                entity.Property(e => e.CrMasUserBranchValidityId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Branch_Validity_Id");

                entity.Property(e => e.CrMasUserBranchValidityLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Branch_Validity_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserBranchValidityBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserBranchValidityBranchCashAvailable)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_Cash_Available");

                entity.Property(e => e.CrMasUserBranchValidityBranchCashBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_Cash_Balance");

                entity.Property(e => e.CrMasUserBranchValidityBranchCashReserved)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_Cash_Reserved");

                entity.Property(e => e.CrMasUserBranchValidityBranchSalesPointAvailable)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_SalesPoint_Available");

                entity.Property(e => e.CrMasUserBranchValidityBranchSalesPointBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_SalesPoint_Balance");

                entity.Property(e => e.CrMasUserBranchValidityBranchSalesPointReserved)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_SalesPoint_Reserved");

                entity.Property(e => e.CrMasUserBranchValidityBranchStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserBranchValidityBranchTransferAvailable)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_Transfer_Available");

                entity.Property(e => e.CrMasUserBranchValidityBranchTransferBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_Transfer_Balance");

                entity.Property(e => e.CrMasUserBranchValidityBranchTransferReserved)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_Transfer_Reserved");

                entity.HasOne(d => d.CrMasUserBranchValidityNavigation)
                    .WithMany(p => p.CrMasUserBranchValidities)
                    .HasForeignKey(d => d.CrMasUserBranchValidityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_User_Branch_Validity_CR_Mas_User_Information");

                entity.HasOne(d => d.CrMasUserBranchValidity1)
                    .WithMany(p => p.CrMasUserBranchValidities)
                    .HasForeignKey(d => new { d.CrMasUserBranchValidityLessor, d.CrMasUserBranchValidityBranch })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Mas_User_Branch_Validity_CR_Cas_Branch_Information");
            });

            modelBuilder.Entity<CrMasUserContractValidity>(entity =>
            {
                entity.HasKey(e => e.CrMasUserContractValidityUserId);

                entity.ToTable("CR_Mas_User_Contract_Validity");

                entity.Property(e => e.CrMasUserContractValidityUserId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Contract_Validity_User_Id");

                entity.Property(e => e.CrMasUserContractValidityAdmin)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Contract_Validity_Admin")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserContractValidityAge).HasColumnName("CR_Mas_User_Contract_Validity_Age");

                entity.Property(e => e.CrMasUserContractValidityBbrake).HasColumnName("CR_Mas_User_Contract_Validity_Bbrake");

                entity.Property(e => e.CrMasUserContractValidityCancel).HasColumnName("CR_Mas_User_Contract_Validity_Cancel");

                entity.Property(e => e.CrMasUserContractValidityChamber).HasColumnName("CR_Mas_User_Contract_Validity_Chamber");

                entity.Property(e => e.CrMasUserContractValidityChkecUp).HasColumnName("CR_Mas_User_Contract_Validity_Chkec_Up");

                entity.Property(e => e.CrMasUserContractValidityCompanyAddress).HasColumnName("CR_Mas_User_Contract_Validity_Company_Address");

                entity.Property(e => e.CrMasUserContractValidityDiscountRate)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CR_Mas_User_Contract_Validity_Discount_Rate");

                entity.Property(e => e.CrMasUserContractValidityDrivingLicense).HasColumnName("CR_Mas_User_Contract_Validity_Driving_License");

                entity.Property(e => e.CrMasUserContractValidityEmployer).HasColumnName("CR_Mas_User_Contract_Validity_Employer");

                entity.Property(e => e.CrMasUserContractValidityEnd).HasColumnName("CR_Mas_User_Contract_Validity_End");

                entity.Property(e => e.CrMasUserContractValidityExtension).HasColumnName("CR_Mas_User_Contract_Validity_Extension");

                entity.Property(e => e.CrMasUserContractValidityFbrake).HasColumnName("CR_Mas_User_Contract_Validity_Fbrake");

                entity.Property(e => e.CrMasUserContractValidityHour).HasColumnName("CR_Mas_User_Contract_Validity_Hour");

                entity.Property(e => e.CrMasUserContractValidityId).HasColumnName("CR_Mas_User_Contract_Validity_Id");

                entity.Property(e => e.CrMasUserContractValidityInsurance).HasColumnName("CR_Mas_User_Contract_Validity_Insurance");

                entity.Property(e => e.CrMasUserContractValidityKm).HasColumnName("CR_Mas_User_Contract_Validity_Km");

                entity.Property(e => e.CrMasUserContractValidityLessContractValue).HasColumnName("CR_Mas_User_Contract_Validity_Less_Contract_Value");

                entity.Property(e => e.CrMasUserContractValidityLicenceMunicipale).HasColumnName("CR_Mas_User_Contract_Validity_Licence_Municipale");

                entity.Property(e => e.CrMasUserContractValidityMaintenance).HasColumnName("CR_Mas_User_Contract_Validity_Maintenance");

                entity.Property(e => e.CrMasUserContractValidityOil).HasColumnName("CR_Mas_User_Contract_Validity_Oil");

                entity.Property(e => e.CrMasUserContractValidityOperatingCard).HasColumnName("CR_Mas_User_Contract_Validity_Operating_Card");

                entity.Property(e => e.CrMasUserContractValidityRegister).HasColumnName("CR_Mas_User_Contract_Validity_Register");

                entity.Property(e => e.CrMasUserContractValidityRenterAddress).HasColumnName("CR_Mas_User_Contract_Validity_Renter_Address");

                entity.Property(e => e.CrMasUserContractValidityTires).HasColumnName("CR_Mas_User_Contract_Validity_Tires");

                entity.Property(e => e.CrMasUserContractValidityTrafficLicense).HasColumnName("CR_Mas_User_Contract_Validity_Traffic_License");

                entity.Property(e => e.CrMasUserContractValidityTransferPermission).HasColumnName("CR_Mas_User_Contract_Validity_Transfer_Permission");

                entity.HasOne(d => d.CrMasUserContractValidityAdminNavigation)
                    .WithMany(p => p.CrMasUserContractValidities)
                    .HasForeignKey(d => d.CrMasUserContractValidityAdmin)
                    .HasConstraintName("fk_CR_Mas_User_Contract_Validity_CR_Cas_Sys_Administrative_Procedures");

                entity.HasOne(d => d.CrMasUserContractValidityUser)
                    .WithOne(p => p.CrMasUserContractValidity)
                    .HasForeignKey<CrMasUserContractValidity>(d => d.CrMasUserContractValidityUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_User_Contract_Validity_CR_Mas_User_Information");
            });

            modelBuilder.Entity<CrMasUserInformation>(entity =>
            {
                entity.HasKey(e => e.CrMasUserInformationCode);

                entity.ToTable("CR_Mas_User_Information");

                entity.HasIndex(e => e.CrMasUserInformationLessor, "IX_CR_Mas_User_Information_CR_Mas_User_Information_Lessor");

                entity.Property(e => e.CrMasUserInformationCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Information_Code");

                entity.Property(e => e.CrMasUserInformationArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_User_Information_Ar_Name");

                entity.Property(e => e.CrMasUserInformationAuthorizationAdmin).HasColumnName("CR_Mas_User_Information_Authorization_Admin");

                entity.Property(e => e.CrMasUserInformationAuthorizationBnan).HasColumnName("CR_Mas_User_Information_Authorization_Bnan");

                entity.Property(e => e.CrMasUserInformationAuthorizationBranch).HasColumnName("CR_Mas_User_Information_Authorization_Branch");

                entity.Property(e => e.CrMasUserInformationAuthorizationFoolwUp).HasColumnName("CR_Mas_User_Information_Authorization_FoolwUp");

                entity.Property(e => e.CrMasUserInformationAuthorizationOwner).HasColumnName("CR_Mas_User_Information_Authorization_Owner");

                entity.Property(e => e.CrMasUserInformationAvailableBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Information_Available_Balance");

                entity.Property(e => e.CrMasUserInformationCallingKey)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Information_Calling_Key");

                entity.Property(e => e.CrMasUserInformationChangePassWordLastDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_User_Information_ChangePassWord_Last_Date");

                entity.Property(e => e.CrMasUserInformationCreditLimit)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Information_Credit_Limit");

                entity.Property(e => e.CrMasUserInformationDefaultBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Information_Default_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserInformationDefaultLanguage)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Information_Default_Language")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserInformationEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_User_Information_Email");

                entity.Property(e => e.CrMasUserInformationEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_User_Information_En_Name");

                entity.Property(e => e.CrMasUserInformationEntryLastDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_User_Information_Entry_Last_Date");

                entity.Property(e => e.CrMasUserInformationEntryLastTime).HasColumnName("CR_Mas_User_Information_Entry_Last_Time");

                entity.Property(e => e.CrMasUserInformationExitLastDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_User_Information_Exit_Last_Date");

                entity.Property(e => e.CrMasUserInformationExitLastTime).HasColumnName("CR_Mas_User_Information_Exit_Last_Time");

                entity.Property(e => e.CrMasUserInformationExitTimer).HasColumnName("CR_Mas_User_Information_Exit_Timer");

                entity.Property(e => e.CrMasUserInformationLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Information_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserInformationMobileNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Information_Mobile_No");

                entity.Property(e => e.CrMasUserInformationOperationStatus).HasColumnName("CR_Mas_User_Information_Operation_Status");

                entity.Property(e => e.CrMasUserInformationPassWord)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Information_PassWord");

                entity.Property(e => e.CrMasUserInformationPicture)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_User_Information_Picture");

                entity.Property(e => e.CrMasUserInformationReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_User_Information_Reasons");

                entity.Property(e => e.CrMasUserInformationRemindMe)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_User_Information_Remind_Me");

                entity.Property(e => e.CrMasUserInformationReservedBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Information_Reserved_Balance");

                entity.Property(e => e.CrMasUserInformationSignature)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_User_Information_Signature");

                entity.Property(e => e.CrMasUserInformationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Information_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserInformationTasksArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_User_Information_Tasks_Ar_Name");

                entity.Property(e => e.CrMasUserInformationTasksEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_User_Information_Tasks_En_Name");

                entity.Property(e => e.CrMasUserInformationTotalBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Mas_User_Information_Total_Balance")
                    .HasDefaultValueSql("((0))");
                entity.HasOne(d => d.CrMasUserInformationLessorNavigation)
                    .WithMany(p => p.CrMasUserInformations)
                    .HasForeignKey(d => d.CrMasUserInformationLessor)
                    .HasConstraintName("FK_CR_Mas_User_Information_CR_Mas_Lessor_Information");
            });

            modelBuilder.Entity<CrMasUserLogin>(entity =>
            {
                entity.HasKey(e => e.CrMasUserLoginNo);

                entity.ToTable("CR_Mas_User_Login");

                entity.HasIndex(e => e.CrMasUserLoginLessor, "IX_CR_Mas_User_Login_CR_Mas_User_Login_Lessor");

                entity.HasIndex(e => e.CrMasUserLoginMainTask, "IX_CR_Mas_User_Login_CR_Mas_User_Login_Main_Task");

                entity.HasIndex(e => e.CrMasUserLoginSubTask, "IX_CR_Mas_User_Login_CR_Mas_User_Login_Sub_Task");

                entity.HasIndex(e => e.CrMasUserLoginSystem, "IX_CR_Mas_User_Login_CR_Mas_User_Login_System");

                entity.HasIndex(e => e.CrMasUserLoginUser, "IX_CR_Mas_User_Login_CR_Mas_User_Login_User");

                entity.Property(e => e.CrMasUserLoginNo)
                    .ValueGeneratedNever()
                    .HasColumnName("CR_Mas_User_Login_No");

                entity.Property(e => e.CrMasUserLoginArOperation)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_User_Login_Ar_Operation");

                entity.Property(e => e.CrMasUserLoginBranch)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Login_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserLoginConcatenateOperationArDescription)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Mas_User_Login_Concatenate_Operation_Ar_Description");

                entity.Property(e => e.CrMasUserLoginConcatenateOperationEnDescription)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Mas_User_Login_Concatenate_Operation_En_Description");

                entity.Property(e => e.CrMasUserLoginEnOperation)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_User_Login_En_Operation");

                entity.Property(e => e.CrMasUserLoginEntryDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_User_Login_Entry_Date");

                entity.Property(e => e.CrMasUserLoginEntryTime).HasColumnName("CR_Mas_User_Login_Entry_Time");

                entity.Property(e => e.CrMasUserLoginLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Login_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserLoginMainTask)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Login_Main_Task")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserLoginSubComputerCode)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_User_Login_Sub_Computer_Code");

                entity.Property(e => e.CrMasUserLoginSubComputerType)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_User_Login_Sub_Computer_Type");

                entity.Property(e => e.CrMasUserLoginSubTask)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Login_Sub_Task")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserLoginSystem)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Login_System")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserLoginUser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Login_User");

                entity.HasOne(d => d.CrMasUserLoginLessorNavigation)
                    .WithMany(p => p.CrMasUserLogins)
                    .HasForeignKey(d => d.CrMasUserLoginLessor)
                    .HasConstraintName("CR_Mas_User_Login_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrMasUserLoginMainTaskNavigation)
                    .WithMany(p => p.CrMasUserLogins)
                    .HasForeignKey(d => d.CrMasUserLoginMainTask)
                    .HasConstraintName("FK_CR_Mas_User_Login_CR_Mas_Sys_Main_Tasks");

                entity.HasOne(d => d.CrMasUserLoginSubTaskNavigation)
                    .WithMany(p => p.CrMasUserLogins)
                    .HasForeignKey(d => d.CrMasUserLoginSubTask)
                    .HasConstraintName("FK_CR_Mas_User_Login_CR_Mas_Sys_Sub_Tasks");

                entity.HasOne(d => d.CrMasUserLoginSystemNavigation)
                    .WithMany(p => p.CrMasUserLogins)
                    .HasForeignKey(d => d.CrMasUserLoginSystem)
                    .HasConstraintName("FK_CR_Mas_User_Login_CR_Mas_Sys_System");

                entity.HasOne(d => d.CrMasUserLoginUserNavigation)
                    .WithMany(p => p.CrMasUserLogins)
                    .HasForeignKey(d => d.CrMasUserLoginUser)
                    .HasConstraintName("FK_CR_Mas_User_Login_CR_Mas_User_Information");
            });

            modelBuilder.Entity<CrMasUserMainValidation>(entity =>
            {
                entity.HasKey(e => new { e.CrMasUserMainValidationUser, e.CrMasUserMainValidationMainTasks })
                    .HasName("PK__CR_Mas_Main_Validitaion");

                entity.ToTable("CR_Mas_User_Main_Validation");

                entity.HasIndex(e => e.CrMasUserMainValidationMainSystem, "IX_CR_Mas_User_Main_Validation_CR_Mas_User_Main_Validation_Main_System");

                entity.HasIndex(e => e.CrMasUserMainValidationMainTasks, "IX_CR_Mas_User_Main_Validation_CR_Mas_User_Main_Validation_Main_Tasks");

                entity.Property(e => e.CrMasUserMainValidationUser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Main_Validation_User");

                entity.Property(e => e.CrMasUserMainValidationMainTasks)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Main_Validation_Main_Tasks")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserMainValidationAuthorization).HasColumnName("CR_Mas_User_Main_Validation_Authorization");

                entity.Property(e => e.CrMasUserMainValidationMainSystem)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Main_Validation_Main_System")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasUserMainValidationMainSystemNavigation)
                    .WithMany(p => p.CrMasUserMainValidations)
                    .HasForeignKey(d => d.CrMasUserMainValidationMainSystem)
                    .HasConstraintName("FK_CR_Mas_User_Main_Validation_CR_Mas_Sys_System");

                entity.HasOne(d => d.CrMasUserMainValidationMainTasksNavigation)
                    .WithMany(p => p.CrMasUserMainValidations)
                    .HasForeignKey(d => d.CrMasUserMainValidationMainTasks)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_User_Main_Validation_CR_Mas_Sys_Main_Tasks");

                entity.HasOne(d => d.CrMasUserMainValidationUserNavigation)
                    .WithMany(p => p.CrMasUserMainValidations)
                    .HasForeignKey(d => d.CrMasUserMainValidationUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_User_Main_Validation_CR_Mas_User_Information");
            });

            modelBuilder.Entity<CrMasUserMessage>(entity =>
            {
                entity.HasKey(e => e.CrMasUserMessageNo);

                entity.ToTable("CR_Mas_User_Message");

                entity.HasIndex(e => e.CrMasUserMessageUserReceiver, "IX_CR_Mas_User_Message_CR_Mas_User_Message_User_Receiver");

                entity.HasIndex(e => e.CrMasUserMessageUserSender, "IX_CR_Mas_User_Message_CR_Mas_User_Message_User_Sender");

                entity.Property(e => e.CrMasUserMessageNo)
                    .ValueGeneratedNever()
                    .HasColumnName("CR_Mas_User_Message_No");

                entity.Property(e => e.CrMasUserMessageContent)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_User_Message_Content");

                entity.Property(e => e.CrMasUserMessageDateWasReceived)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_User_Message_Date_Was_Received");

                entity.Property(e => e.CrMasUserMessageDateWasSent)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_User_Message_Date_Was_Sent");

                entity.Property(e => e.CrMasUserMessageLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Message_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserMessageStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Message_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserMessageTimeWasReceived).HasColumnName("CR_Mas_User_Message_Time_Was_Received");

                entity.Property(e => e.CrMasUserMessageTimeWasSent).HasColumnName("CR_Mas_User_Message_Time_Was_Sent");

                entity.Property(e => e.CrMasUserMessageUserReceiver)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Message_User_Receiver");

                entity.Property(e => e.CrMasUserMessageUserSender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Message_User_Sender");

                entity.HasOne(d => d.CrMasUserMessageUserReceiverNavigation)
                    .WithMany(p => p.CrMasUserMessageCrMasUserMessageUserReceiverNavigations)
                    .HasForeignKey(d => d.CrMasUserMessageUserReceiver)
                    .HasConstraintName("FK_CR_Mas_User_Message_CR_Mas_User_Message_User_Receiver");

                entity.HasOne(d => d.CrMasUserMessageUserSenderNavigation)
                    .WithMany(p => p.CrMasUserMessageCrMasUserMessageUserSenderNavigations)
                    .HasForeignKey(d => d.CrMasUserMessageUserSender)
                    .HasConstraintName("FK_CR_Mas_User_Message_CR_Mas_User_Information_User_Sender");
            });

            modelBuilder.Entity<CrMasUserProceduresValidation>(entity =>
            {
                entity.HasKey(e => new { e.CrMasUserProceduresValidationCode, e.CrMasUserProceduresValidationSubTasks });

                entity.ToTable("CR_Mas_User_Procedures_Validation");

                entity.HasIndex(e => e.CrMasUserProceduresValidationMainTask, "IX_CR_Mas_User_Procedures_Validation_CR_Mas_User_Procedures_Validation_Main_Task");

                entity.HasIndex(e => e.CrMasUserProceduresValidationSubTasks, "IX_CR_Mas_User_Procedures_Validation_CR_Mas_User_Procedures_Validation_Sub_Tasks");

                entity.HasIndex(e => e.CrMasUserProceduresValidationSystem, "IX_CR_Mas_User_Procedures_Validation_CR_Mas_User_Procedures_Validation_System");

                entity.Property(e => e.CrMasUserProceduresValidationCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Procedures_Validation_Code");

                entity.Property(e => e.CrMasUserProceduresValidationSubTasks)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Procedures_Validation_Sub_Tasks")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserProceduresValidationDeleteAuthorization).HasColumnName("CR_Mas_User_Procedures_Validation_Delete_Authorization");

                entity.Property(e => e.CrMasUserProceduresValidationHoldAuthorization).HasColumnName("CR_Mas_User_Procedures_Validation_Hold_Authorization");

                entity.Property(e => e.CrMasUserProceduresValidationInsertAuthorization).HasColumnName("CR_Mas_User_Procedures_Validation_Insert_Authorization");

                entity.Property(e => e.CrMasUserProceduresValidationMainTask)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Procedures_Validation_Main_Task")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserProceduresValidationSystem)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Procedures_Validation_System")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserProceduresValidationUnDeleteAuthorization).HasColumnName("CR_Mas_User_Procedures_Validation_UnDelete_Authorization");

                entity.Property(e => e.CrMasUserProceduresValidationUnHoldAuthorization).HasColumnName("CR_Mas_User_Procedures_Validation_UnHold_Authorization");

                entity.Property(e => e.CrMasUserProceduresValidationUpDateAuthorization).HasColumnName("CR_Mas_User_Procedures_Validation_UpDate_Authorization");

                entity.HasOne(d => d.CrMasUserProceduresValidationCodeNavigation)
                    .WithMany(p => p.CrMasUserProceduresValidations)
                    .HasForeignKey(d => d.CrMasUserProceduresValidationCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_User_Procedures_Validation_CR_Mas_User_Information");

                entity.HasOne(d => d.CrMasUserProceduresValidationMainTaskNavigation)
                    .WithMany(p => p.CrMasUserProceduresValidations)
                    .HasForeignKey(d => d.CrMasUserProceduresValidationMainTask)
                    .HasConstraintName("FK_CR_Mas_User_Procedures_Validation_CR_Mas_Sys_Main_Tasks");

                entity.HasOne(d => d.CrMasUserProceduresValidationSubTasksNavigation)
                    .WithMany(p => p.CrMasUserProceduresValidations)
                    .HasForeignKey(d => d.CrMasUserProceduresValidationSubTasks)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_User_Procedures_Validation_CR_Mas_Sys_Sub_Tasks");

                entity.HasOne(d => d.CrMasUserProceduresValidationSystemNavigation)
                    .WithMany(p => p.CrMasUserProceduresValidations)
                    .HasForeignKey(d => d.CrMasUserProceduresValidationSystem)
                    .HasConstraintName("FK_CR_Mas_User_Procedures_Validation_CR_Mas_Sys_System");
            });

            modelBuilder.Entity<CrMasUserSubValidation>(entity =>
            {
                entity.HasKey(e => new { e.CrMasUserSubValidationUser, e.CrMasUserSubValidationSubTasks });

                entity.ToTable("CR_Mas_User_Sub_Validation");

                entity.HasIndex(e => e.CrMasUserSubValidationMain, "IX_CR_Mas_User_Sub_Validation_CR_Mas_User_Sub_Validation_Main");

                entity.HasIndex(e => e.CrMasUserSubValidationSubTasks, "IX_CR_Mas_User_Sub_Validation_CR_Mas_User_Sub_Validation_Sub_Tasks");

                entity.HasIndex(e => e.CrMasUserSubValidationSystem, "IX_CR_Mas_User_Sub_Validation_CR_Mas_User_Sub_Validation_System");

                entity.Property(e => e.CrMasUserSubValidationUser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Sub_Validation_User");

                entity.Property(e => e.CrMasUserSubValidationSubTasks)
                    .HasMaxLength(7)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Sub_Validation_Sub_Tasks")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserSubValidationAuthorization).HasColumnName("CR_Mas_User_Sub_Validation_Authorization");

                entity.Property(e => e.CrMasUserSubValidationMain)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Sub_Validation_Main")
                    .IsFixedLength();

                entity.Property(e => e.CrMasUserSubValidationSystem)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Sub_Validation_System")
                    .IsFixedLength();

                entity.HasOne(d => d.CrMasUserSubValidationMainNavigation)
                    .WithMany(p => p.CrMasUserSubValidations)
                    .HasForeignKey(d => d.CrMasUserSubValidationMain)
                    .HasConstraintName("FK_CR_Mas_User_Sub_Validation_CR_Mas_Sys_Main_Tasks");

                entity.HasOne(d => d.CrMasUserSubValidationSubTasksNavigation)
                    .WithMany(p => p.CrMasUserSubValidations)
                    .HasForeignKey(d => d.CrMasUserSubValidationSubTasks)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_User_Sub_Validation_CR_Mas_Sys_Sub_Tasks");

                entity.HasOne(d => d.CrMasUserSubValidationSystemNavigation)
                    .WithMany(p => p.CrMasUserSubValidations)
                    .HasForeignKey(d => d.CrMasUserSubValidationSystem)
                    .HasConstraintName("FK_CR_Mas_User_Sub_Validation_CR_Mas_Sys_System");

                entity.HasOne(d => d.CrMasUserSubValidationUserNavigation)
                    .WithMany(p => p.CrMasUserSubValidations)
                    .HasForeignKey(d => d.CrMasUserSubValidationUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_User_Sub_Validation_CR_Mas_User_Information");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
