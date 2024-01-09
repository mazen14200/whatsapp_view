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
        public virtual DbSet<CrCasAccountReceipt> CrCasAccountReceipts { get; set; } = null!;
        public virtual DbSet<CrCasAccountSalesPoint> CrCasAccountSalesPoints { get; set; } = null!;
        public virtual DbSet<CrCasBeneficiary> CrCasBeneficiaries { get; set; } = null!;
        public virtual DbSet<CrCasBranchDocument> CrCasBranchDocuments { get; set; } = null!;
        public virtual DbSet<CrCasBranchInformation> CrCasBranchInformations { get; set; } = null!;
        public virtual DbSet<CrCasBranchPost> CrCasBranchPosts { get; set; } = null!;
        public virtual DbSet<CrCasCarAdvantage> CrCasCarAdvantages { get; set; } = null!;
        public virtual DbSet<CrCasCarDocumentsMaintenance> CrCasCarDocumentsMaintenances { get; set; } = null!;
        public virtual DbSet<CrCasCarInformation> CrCasCarInformations { get; set; } = null!;
        public virtual DbSet<CrCasLessorClassification> CrCasLessorClassifications { get; set; } = null!;
        public virtual DbSet<CrCasLessorMechanism> CrCasLessorMechanisms { get; set; } = null!;
        public virtual DbSet<CrCasLessorMembership> CrCasLessorMemberships { get; set; } = null!;
        public virtual DbSet<CrCasOwner> CrCasOwners { get; set; } = null!;
        public virtual DbSet<CrCasPriceCarAdditional> CrCasPriceCarAdditionals { get; set; } = null!;
        public virtual DbSet<CrCasPriceCarAdvantage> CrCasPriceCarAdvantages { get; set; } = null!;
        public virtual DbSet<CrCasPriceCarBasic> CrCasPriceCarBasics { get; set; } = null!;
        public virtual DbSet<CrCasPriceCarOption> CrCasPriceCarOptions { get; set; } = null!;
        public virtual DbSet<CrCasRenterContractAdditional> CrCasRenterContractAdditionals { get; set; } = null!;
        public virtual DbSet<CrCasRenterContractAdvantage> CrCasRenterContractAdvantages { get; set; } = null!;
        public virtual DbSet<CrCasRenterContractAlert> CrCasRenterContractAlerts { get; set; } = null!;
        public virtual DbSet<CrCasRenterContractAuthorization> CrCasRenterContractAuthorizations { get; set; } = null!;
        public virtual DbSet<CrCasRenterContractBasic> CrCasRenterContractBasics { get; set; } = null!;
        public virtual DbSet<CrCasRenterContractCarCheckup> CrCasRenterContractCarCheckups { get; set; } = null!;
        public virtual DbSet<CrCasRenterContractChoice> CrCasRenterContractChoices { get; set; } = null!;
        public virtual DbSet<CrCasRenterContractStatistic> CrCasRenterContractStatistics { get; set; } = null!;
        public virtual DbSet<CrCasRenterLessor> CrCasRenterLessors { get; set; } = null!;
        public virtual DbSet<CrCasRenterPrivateDriverInformation> CrCasRenterPrivateDriverInformations { get; set; } = null!;
        public virtual DbSet<CrCasSysAdministrativeProcedure> CrCasSysAdministrativeProcedures { get; set; } = null!;
        public virtual DbSet<CrElmEmployer> CrElmEmployers { get; set; } = null!;
        public virtual DbSet<CrElmLicense> CrElmLicenses { get; set; } = null!;
        public virtual DbSet<CrElmPersonal> CrElmPersonals { get; set; } = null!;
        public virtual DbSet<CrElmPost> CrElmPosts { get; set; } = null!;
        public virtual DbSet<CrMasContractCompany> CrMasContractCompanies { get; set; } = null!;
        public virtual DbSet<CrMasContractCompanyDetailed> CrMasContractCompanyDetaileds { get; set; } = null!;
        public virtual DbSet<CrMasLessorImage> CrMasLessorImages { get; set; } = null!;
        public virtual DbSet<CrMasLessorInformation> CrMasLessorInformations { get; set; } = null!;
        public virtual DbSet<CrMasRenterInformation> CrMasRenterInformations { get; set; } = null!;
        public virtual DbSet<CrMasRenterPost> CrMasRenterPosts { get; set; } = null!;
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
        public virtual DbSet<CrMasSysProbabilityMembership> CrMasSysProbabilityMemberships { get; set; } = null!;
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
                optionsBuilder.UseSqlServer("Server=.;Database=BnanKSA;Trusted_Connection=True;");
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

            modelBuilder.Entity<CrCasAccountReceipt>(entity =>
            {
                entity.HasKey(e => e.CrCasAccountReceiptNo);

                entity.ToTable("CR_Cas_Account_Receipt");

                entity.HasIndex(e => e.CrCasAccountReceiptAccount, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_Account");

                entity.HasIndex(e => e.CrCasAccountReceiptBank, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_Bank");

                entity.HasIndex(e => e.CrCasAccountReceiptCar, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_Car");

                entity.HasIndex(e => new { e.CrCasAccountReceiptLessorCode, e.CrCasAccountReceiptBranchCode }, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_Lessor_Code_CR_Cas_Account_Receipt_Branch_Code");

                entity.HasIndex(e => e.CrCasAccountReceiptPaymentMethod, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_Payment_Method");

                entity.HasIndex(e => e.CrCasAccountReceiptReferenceType, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_Reference_Type");

                entity.HasIndex(e => e.CrCasAccountReceiptRenterId, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_Renter_Id");

                entity.HasIndex(e => e.CrCasAccountReceiptSalesPoint, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_SalesPoint");

                entity.HasIndex(e => e.CrCasAccountReceiptUser, "IX_CR_Cas_Account_Receipt_CR_Cas_Account_Receipt_User");

                entity.Property(e => e.CrCasAccountReceiptNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptAccount)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Account")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptArPdfFile)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Account_Receipt_Ar_PDF_File");

                entity.Property(e => e.CrCasAccountReceiptBank)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Bank")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptBranchCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Branch_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptCar)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Account_Receipt_Car");

                entity.Property(e => e.CrCasAccountReceiptDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Account_Receipt_Date");

                entity.Property(e => e.CrCasAccountReceiptEnPdfFile)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Account_Receipt_En_PDF_File");

                entity.Property(e => e.CrCasAccountReceiptIsPassing)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Is_Passing")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptLessorCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Lessor_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptPassingDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Account_Receipt_Passing_Date");

                entity.Property(e => e.CrCasAccountReceiptPassingReference)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Passing_Reference")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptPassingUser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Passing_User");

                entity.Property(e => e.CrCasAccountReceiptPayment)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Account_Receipt_Payment");

                entity.Property(e => e.CrCasAccountReceiptPaymentMethod)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Payment_Method")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Account_Receipt_Reasons");

                entity.Property(e => e.CrCasAccountReceiptReceipt)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Account_Receipt_Receipt");

                entity.Property(e => e.CrCasAccountReceiptReferenceNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Reference_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptReferenceType)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Reference_Type")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptRenterId)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Account_Receipt_Renter_Id");

                entity.Property(e => e.CrCasAccountReceiptRenterPreviousBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Account_Receipt_Renter_Previous_Balance");

                entity.Property(e => e.CrCasAccountReceiptSalesPoint)
                    .HasMaxLength(11)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_SalesPoint")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptSalesPointPreviousBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Account_Receipt_SalesPoint_Previous_Balance");

                entity.Property(e => e.CrCasAccountReceiptType)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Type")
                    .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceiptUser)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_User");

                entity.Property(e => e.CrCasAccountReceiptUserPreviousBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Account_Receipt_User_Previous_Balance");

                entity.Property(e => e.CrCasAccountReceiptYear)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Account_Receipt_Year")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasAccountReceiptAccountNavigation)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => d.CrCasAccountReceiptAccount)
                    .HasConstraintName("fk_CR_Cas_Account_Receipt_CR_Cas_Account_Bank");

                entity.HasOne(d => d.CrCasAccountReceiptBankNavigation)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => d.CrCasAccountReceiptBank)
                    .HasConstraintName("fk_CR_Cas_Account_Receipt_CR_Mas_Sup_Account_Bank");

                entity.HasOne(d => d.CrCasAccountReceiptCarNavigation)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => d.CrCasAccountReceiptCar)
                    .HasConstraintName("fk_CR_Cas_Account_Receipt_CR_Cas_Car_Information");

                entity.HasOne(d => d.CrCasAccountReceiptPaymentMethodNavigation)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => d.CrCasAccountReceiptPaymentMethod)
                    .HasConstraintName("fk_CR_Cas_Account_Receipt_CR_Mas_Sup_Account_Payment_Method");

                entity.HasOne(d => d.CrCasAccountReceiptReferenceTypeNavigation)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => d.CrCasAccountReceiptReferenceType)
                    .HasConstraintName("fk_CR_Cas_Account_Receipt_CR_Mas_Sup_Account_Reference");

                entity.HasOne(d => d.CrCasAccountReceiptRenter)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => d.CrCasAccountReceiptRenterId)
                    .HasConstraintName("fk_CR_Cas_Account_Receipt_CR_Mas_Renter_Information");

                entity.HasOne(d => d.CrCasAccountReceiptSalesPointNavigation)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => d.CrCasAccountReceiptSalesPoint)
                    .HasConstraintName("fk_CR_Cas_Account_Receipt_CR_Cas_Account_SalesPoint");

                entity.HasOne(d => d.CrCasAccountReceiptUserNavigation)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => d.CrCasAccountReceiptUser)
                    .HasConstraintName("fk_CR_Cas_Account_Receipt_CR_Mas_User_Information");

                entity.HasOne(d => d.CrCasAccountReceiptNavigation)
                    .WithMany(p => p.CrCasAccountReceipts)
                    .HasForeignKey(d => new { d.CrCasAccountReceiptLessorCode, d.CrCasAccountReceiptBranchCode })
                    .HasConstraintName("fk_lessor");
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

                entity.HasIndex(e => new { e.CrCasBranchInformationCode, e.CrCasBranchInformationLessor }, "uq_CR_Cas_Branch_Information")
                    .IsUnique();

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

            modelBuilder.Entity<CrCasCarAdvantage>(entity =>
            {
                entity.HasKey(e => new { e.CrCasCarAdvantagesSerialNo, e.CrCasCarAdvantagesCode });

                entity.ToTable("CR_Cas_Car_Advantages");

                entity.HasIndex(e => e.CrCasCarAdvantagesBrand, "IX_CR_Cas_Car_Advantages_CR_Cas_Car_Advantages_Brand");

                entity.HasIndex(e => e.CrCasCarAdvantagesCategory, "IX_CR_Cas_Car_Advantages_CR_Cas_Car_Advantages_Category");

                entity.HasIndex(e => e.CrCasCarAdvantagesCode, "IX_CR_Cas_Car_Advantages_CR_Cas_Car_Advantages_Code");

                entity.HasIndex(e => e.CrCasCarAdvantagesLessor, "IX_CR_Cas_Car_Advantages_CR_Cas_Car_Advantages_Lessor");

                entity.HasIndex(e => e.CrCasCarAdvantagesModel, "IX_CR_Cas_Car_Advantages_CR_Cas_Car_Advantages_Model");

                entity.Property(e => e.CrCasCarAdvantagesSerialNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Car_Advantages_Serial_No");

                entity.Property(e => e.CrCasCarAdvantagesCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Advantages_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarAdvantagesBrand)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Advantages_Brand")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarAdvantagesCarYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Advantages_Car_Year")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarAdvantagesCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Advantages_Category")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarAdvantagesLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Advantages_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarAdvantagesModel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Advantages_Model")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarAdvantagesStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Advantages_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasCarAdvantagesBrandNavigation)
                    .WithMany(p => p.CrCasCarAdvantages)
                    .HasForeignKey(d => d.CrCasCarAdvantagesBrand)
                    .HasConstraintName("FK_CR_Cas_Car_Advantages_CR_Mas_Sup_Car_Brand");

                entity.HasOne(d => d.CrCasCarAdvantagesCategoryNavigation)
                    .WithMany(p => p.CrCasCarAdvantages)
                    .HasForeignKey(d => d.CrCasCarAdvantagesCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Car_Advantages_CR_Mas_Sup_Car_Category");

                entity.HasOne(d => d.CrCasCarAdvantagesCodeNavigation)
                    .WithMany(p => p.CrCasCarAdvantages)
                    .HasForeignKey(d => d.CrCasCarAdvantagesCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Car_Advantages_CR_Mas_Sup_Car_Advantages");

                entity.HasOne(d => d.CrCasCarAdvantagesLessorNavigation)
                    .WithMany(p => p.CrCasCarAdvantages)
                    .HasForeignKey(d => d.CrCasCarAdvantagesLessor)
                    .HasConstraintName("FK_CR_Cas_Car_Advantages_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrCasCarAdvantagesModelNavigation)
                    .WithMany(p => p.CrCasCarAdvantages)
                    .HasForeignKey(d => d.CrCasCarAdvantagesModel)
                    .HasConstraintName("FK_CR_Cas_Car_Advantages_CR_Mas_Sup_Car_Model");

                entity.HasOne(d => d.CrCasCarAdvantagesSerialNoNavigation)
                    .WithMany(p => p.CrCasCarAdvantages)
                    .HasForeignKey(d => d.CrCasCarAdvantagesSerialNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Car_Advantages_CR_Cas_Car_Information");
            });

            modelBuilder.Entity<CrCasCarDocumentsMaintenance>(entity =>
            {
                entity.HasKey(e => new { e.CrCasCarDocumentsMaintenanceSerailNo, e.CrCasCarDocumentsMaintenanceProcedures });

                entity.ToTable("CR_Cas_Car_Documents_Maintenance");

                entity.HasIndex(e => new { e.CrCasCarDocumentsMaintenanceLessor, e.CrCasCarDocumentsMaintenanceBranch }, "IX_CR_Cas_Car_Documents_Maintenance_CR_Cas_Car_Documents_Maintenance_Lessor_CR_Cas_Car_Documents_Maintenance_Branch");

                entity.HasIndex(e => e.CrCasCarDocumentsMaintenanceProcedures, "IX_CR_Cas_Car_Documents_Maintenance_CR_Cas_Car_Documents_Maintenance_Procedures");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceSerailNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Serail_No");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceProcedures)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Procedures")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarDocumentsMaintenanceBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarDocumentsMaintenanceCarStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Car_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarDocumentsMaintenanceConsumptionKm).HasColumnName("CR_Cas_Car_Documents_Maintenance_Consumption_KM");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceCurrentMeter).HasColumnName("CR_Cas_Car_Documents_Maintenance_Current_Meter");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Date");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceDateAboutToFinish)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Date_About_To_Finish");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceEndDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_End_Date");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Image");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceIsActivation).HasColumnName("CR_Cas_Car_Documents_Maintenance_IS_Activation");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceKmAboutToFinish).HasColumnName("CR_Cas_Car_Documents_Maintenance_KM_About_To_Finish");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceKmEndsAt).HasColumnName("CR_Cas_Car_Documents_Maintenance_KM_Ends_At");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarDocumentsMaintenanceNo)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_No");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceProceduresClassification)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Procedures_Classification")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarDocumentsMaintenanceReadKm).HasColumnName("CR_Cas_Car_Documents_Maintenance_Read_KM");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Reasons");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceStartDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Start_Date");

                entity.Property(e => e.CrCasCarDocumentsMaintenanceStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Documents_Maintenance_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasCarDocumentsMaintenanceProceduresNavigation)
                    .WithMany(p => p.CrCasCarDocumentsMaintenances)
                    .HasForeignKey(d => d.CrCasCarDocumentsMaintenanceProcedures)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Car_Documents_Maintenance_CR_Mas_Sys_Procedures");

                entity.HasOne(d => d.CrCasCarDocumentsMaintenanceSerailNoNavigation)
                    .WithMany(p => p.CrCasCarDocumentsMaintenances)
                    .HasForeignKey(d => d.CrCasCarDocumentsMaintenanceSerailNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Car_Documents_Maintenance_CR_Cas_Car_Information");

                entity.HasOne(d => d.CrCasCarDocumentsMaintenanceNavigation)
                    .WithMany(p => p.CrCasCarDocumentsMaintenances)
                    .HasForeignKey(d => new { d.CrCasCarDocumentsMaintenanceLessor, d.CrCasCarDocumentsMaintenanceBranch })
                    .HasConstraintName("FK_CR_Cas_Car_Documents_Maintenance_CR_Cas_Branch_Information");
            });

            modelBuilder.Entity<CrCasCarInformation>(entity =>
            {
                entity.HasKey(e => e.CrCasCarInformationSerailNo)
                    .HasName("PK_CR_Cas_Car_Information_1");

                entity.ToTable("CR_Cas_Car_Information");

                entity.HasIndex(e => new { e.CrCasCarInformationBeneficiary, e.CrCasCarInformationLessor }, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Beneficiary_CR_Cas_Car_Information_Lessor");

                entity.HasIndex(e => e.CrCasCarInformationBrand, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Brand");

                entity.HasIndex(e => e.CrCasCarInformationCvt, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_CVT");

                entity.HasIndex(e => e.CrCasCarInformationCategory, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Category");

                entity.HasIndex(e => e.CrCasCarInformationCity, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_City");

                entity.HasIndex(e => e.CrCasCarInformationDistribution, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Distribution");

                entity.HasIndex(e => e.CrCasCarInformationFloorColor, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Floor_Color");

                entity.HasIndex(e => e.CrCasCarInformationFuel, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Fuel");

                entity.HasIndex(e => new { e.CrCasCarInformationLessor, e.CrCasCarInformationBranch }, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Lessor_CR_Cas_Car_Information_Branch");

                entity.HasIndex(e => e.CrCasCarInformationMainColor, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Main_Color");

                entity.HasIndex(e => e.CrCasCarInformationModel, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Model");

                entity.HasIndex(e => new { e.CrCasCarInformationOwner, e.CrCasCarInformationLessor }, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Owner_CR_Cas_Car_Information_Lessor");

                entity.HasIndex(e => e.CrCasCarInformationRegion, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Region");

                entity.HasIndex(e => e.CrCasCarInformationRegistration, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Registration");

                entity.HasIndex(e => e.CrCasCarInformationSeatColor, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Seat_Color");

                entity.HasIndex(e => e.CrCasCarInformationSecondaryColor, "IX_CR_Cas_Car_Information_CR_Cas_Car_Information_Secondary_Color");

                entity.Property(e => e.CrCasCarInformationSerailNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Car_Information_Serail_No");

                entity.Property(e => e.CrCasCarInformationBeneficiary)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Beneficiary");

                entity.Property(e => e.CrCasCarInformationBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationBranchStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Branch_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationBrand)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Brand")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Category")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationCity)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_City")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationConcatenateArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Car_Information_Concatenate_Ar_Name");

                entity.Property(e => e.CrCasCarInformationConcatenateEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Car_Information_Concatenate_En_Name");

                entity.Property(e => e.CrCasCarInformationConractCount).HasColumnName("CR_Cas_Car_Information_Conract_Count");

                entity.Property(e => e.CrCasCarInformationConractDaysNo).HasColumnName("CR_Cas_Car_Information_Conract_Days_No");

                entity.Property(e => e.CrCasCarInformationCurrentMeter).HasColumnName("CR_Cas_Car_Information_Current_Meter");

                entity.Property(e => e.CrCasCarInformationCustomsNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Car_Information_Customs_No");

                entity.Property(e => e.CrCasCarInformationCvt)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_CVT")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationDistribution)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Distribution")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationDocumentationStatus).HasColumnName("CR_Cas_Car_Information_Documentation_Status");

                entity.Property(e => e.CrCasCarInformationFloorColor)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Floor_Color")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationForSaleStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_ForSale_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationFuel)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Fuel")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationJoinedFleetDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Car_Information_Joined_Fleet_Date");

                entity.Property(e => e.CrCasCarInformationLastContractDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Car_Information_Last_Contract_Date");

                entity.Property(e => e.CrCasCarInformationLastPictures)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Car_Information_Last_Pictures");

                entity.Property(e => e.CrCasCarInformationLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationLocation)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Location")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationMainColor)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Main_Color")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationMaintenanceStatus).HasColumnName("CR_Cas_Car_Information_Maintenance_Status");

                entity.Property(e => e.CrCasCarInformationModel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Model")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationOfferValueSale)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Car_Information_Offer_Value_Sale");

                entity.Property(e => e.CrCasCarInformationOfferedSaleDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Car_Information_offered_Sale_Date");

                entity.Property(e => e.CrCasCarInformationOwner)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Owner");

                entity.Property(e => e.CrCasCarInformationOwnerStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Owner_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationPlateArNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Car_Information_Plate_Ar_No");

                entity.Property(e => e.CrCasCarInformationPlateEnNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Car_Information_Plate_En_No");

                entity.Property(e => e.CrCasCarInformationPriceNo)
                    .HasMaxLength(22)
                    .HasColumnName("CR_Cas_Car_Information_Price_No");

                entity.Property(e => e.CrCasCarInformationPriceStatus).HasColumnName("CR_Cas_Car_Information_Price_Status");

                entity.Property(e => e.CrCasCarInformationReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Car_Information_Reasons");

                entity.Property(e => e.CrCasCarInformationRegion)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Region")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationRegistration)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Registration")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationSeatColor)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Seat_Color")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationSecondaryColor)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Secondary_Color")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationSoldDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Car_Information_Sold_Date");

                entity.Property(e => e.CrCasCarInformationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasCarInformationStructureNo)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Cas_Car_Information_Structure_No");

                entity.Property(e => e.CrCasCarInformationYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Car_Information_Year")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasCarInformationBrandNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationBrand)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Brand");

                entity.HasOne(d => d.CrCasCarInformationCategoryNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationCategory)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Category");

                entity.HasOne(d => d.CrCasCarInformationCityNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationCity)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Post_City");

                entity.HasOne(d => d.CrCasCarInformationCvtNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationCvt)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_CVT");

                entity.HasOne(d => d.CrCasCarInformationDistributionNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationDistribution)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Distribution");

                entity.HasOne(d => d.CrCasCarInformationFloorColorNavigation)
                    .WithMany(p => p.CrCasCarInformationCrCasCarInformationFloorColorNavigations)
                    .HasForeignKey(d => d.CrCasCarInformationFloorColor)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Floor_Color");

                entity.HasOne(d => d.CrCasCarInformationFuelNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationFuel)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Fuel");

                entity.HasOne(d => d.CrCasCarInformationMainColorNavigation)
                    .WithMany(p => p.CrCasCarInformationCrCasCarInformationMainColorNavigations)
                    .HasForeignKey(d => d.CrCasCarInformationMainColor)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Color");

                entity.HasOne(d => d.CrCasCarInformationModelNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationModel)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Model");

                entity.HasOne(d => d.CrCasCarInformationRegionNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationRegion)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Post_Regions");

                entity.HasOne(d => d.CrCasCarInformationRegistrationNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => d.CrCasCarInformationRegistration)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Registration");

                entity.HasOne(d => d.CrCasCarInformationSeatColorNavigation)
                    .WithMany(p => p.CrCasCarInformationCrCasCarInformationSeatColorNavigations)
                    .HasForeignKey(d => d.CrCasCarInformationSeatColor)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Seat_Color");

                entity.HasOne(d => d.CrCasCarInformationSecondaryColorNavigation)
                    .WithMany(p => p.CrCasCarInformationCrCasCarInformationSecondaryColorNavigations)
                    .HasForeignKey(d => d.CrCasCarInformationSecondaryColor)
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Mas_Sup_Car_Secondary_Color");

                entity.HasOne(d => d.CrCasCarInformationNavigation)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => new { d.CrCasCarInformationBeneficiary, d.CrCasCarInformationLessor })
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Cas_Beneficiary");

                entity.HasOne(d => d.CrCasCarInformation1)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => new { d.CrCasCarInformationLessor, d.CrCasCarInformationBranch })
                    .HasConstraintName("FK_CR_Cas_Car_Information_Branch_lessor");

                entity.HasOne(d => d.CrCasCarInformation2)
                    .WithMany(p => p.CrCasCarInformations)
                    .HasForeignKey(d => new { d.CrCasCarInformationOwner, d.CrCasCarInformationLessor })
                    .HasConstraintName("FK_CR_Cas_Car_Information_CR_Cas_Owners");
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

                entity.Property(e => e.CrCasLessorMembershipConditionsPicture)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Lessor_Membership_Conditions_Picture");

                entity.HasOne(d => d.CrCasLessorMembershipConditionsNavigation)
                    .WithMany(p => p.CrCasLessorMemberships)
                    .HasForeignKey(d => d.CrCasLessorMembershipConditions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("CR_Cas_Lessor_Membership_CR_Mas_Sup_Renter_Membership");

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

                entity.Property(e => e.CrCasOwnersCountryKey)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Cas_Owners_Country_Key");

                entity.Property(e => e.CrCasOwnersEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Owners_En_Name");

                entity.Property(e => e.CrCasOwnersMobile)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Owners_Mobile");

                entity.Property(e => e.CrCasOwnersReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Owners_Reasons");

                entity.Property(e => e.CrCasOwnersSector)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Owners_Sector")
                    .IsFixedLength();

                entity.Property(e => e.CrCasOwnersSendContractByEmail).HasColumnName("CR_Cas_Owners_Send_Contract_By_Email");

                entity.Property(e => e.CrCasOwnersSendContractByWhatsUp).HasColumnName("CR_Cas_Owners_Send_Contract_By_WhatsUp");

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

            modelBuilder.Entity<CrCasPriceCarAdditional>(entity =>
            {
                entity.HasKey(e => new { e.CrCasPriceCarAdditionalNo, e.CrCasPriceCarAdditionalCode });

                entity.ToTable("CR_Cas_Price_Car_Additional");

                entity.HasIndex(e => e.CrCasPriceCarAdditionalCode, "IX_CR_Cas_Price_Car_Additional_CR_Cas_Price_Car_Additional_Code");

                entity.Property(e => e.CrCasPriceCarAdditionalNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Additional_No");

                entity.Property(e => e.CrCasPriceCarAdditionalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Additional_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarAdditionalValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Additional_Value");

                entity.HasOne(d => d.CrCasPriceCarAdditionalCodeNavigation)
                    .WithMany(p => p.CrCasPriceCarAdditionals)
                    .HasForeignKey(d => d.CrCasPriceCarAdditionalCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Additional_CR_Mas_Sup_Contract_Additional");

                entity.HasOne(d => d.CrCasPriceCarAdditionalNoNavigation)
                    .WithMany(p => p.CrCasPriceCarAdditionals)
                    .HasForeignKey(d => d.CrCasPriceCarAdditionalNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Additional_CR_Cas_Price_Car_Basic");
            });

            modelBuilder.Entity<CrCasPriceCarAdvantage>(entity =>
            {
                entity.HasKey(e => new { e.CrCasPriceCarAdvantagesNo, e.CrCasPriceCarAdvantagesCode });

                entity.ToTable("CR_Cas_Price_Car_Advantages");

                entity.HasIndex(e => e.CrCasPriceCarAdvantagesCode, "IX_CR_Cas_Price_Car_Advantages_CR_Cas_Price_Car_Advantages_Code");

                entity.Property(e => e.CrCasPriceCarAdvantagesNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Advantages_No");

                entity.Property(e => e.CrCasPriceCarAdvantagesCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Advantages_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarAdvantagesValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Advantages_Value");

                entity.HasOne(d => d.CrCasPriceCarAdvantagesCodeNavigation)
                    .WithMany(p => p.CrCasPriceCarAdvantages)
                    .HasForeignKey(d => d.CrCasPriceCarAdvantagesCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Advantages_CR_Mas_Sup_Car_Advantages");

                entity.HasOne(d => d.CrCasPriceCarAdvantagesNoNavigation)
                    .WithMany(p => p.CrCasPriceCarAdvantages)
                    .HasForeignKey(d => d.CrCasPriceCarAdvantagesNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Advantages_CR_Cas_Price_Car_Basic");
            });

            modelBuilder.Entity<CrCasPriceCarBasic>(entity =>
            {
                entity.HasKey(e => e.CrCasPriceCarBasicNo);

                entity.ToTable("CR_Cas_Price_Car_Basic");

                entity.HasIndex(e => e.CrCasPriceCarBasicBrandCode, "IX_CR_Cas_Price_Car_Basic_CR_Cas_Price_Car_Basic_Brand_Code");

                entity.HasIndex(e => e.CrCasPriceCarBasicCategoryCode, "IX_CR_Cas_Price_Car_Basic_CR_Cas_Price_Car_Basic_Category_Code");

                entity.HasIndex(e => e.CrCasPriceCarBasicDistributionCode, "IX_CR_Cas_Price_Car_Basic_CR_Cas_Price_Car_Basic_Distribution_Code");

                entity.HasIndex(e => e.CrCasPriceCarBasicLessorCode, "IX_CR_Cas_Price_Car_Basic_CR_Cas_Price_Car_Basic_Lessor_Code");

                entity.HasIndex(e => e.CrCasPriceCarBasicModelCode, "IX_CR_Cas_Price_Car_Basic_CR_Cas_Price_Car_Basic_Model_Code");

                entity.Property(e => e.CrCasPriceCarBasicNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_No");

                entity.Property(e => e.CrCasCarPriceBasicInFeesTamm)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Car_Price_Basic_In_Fees_Tamm");

                entity.Property(e => e.CrCasCarPriceBasicInFeesTga)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Car_Price_Basic_In_Fees_TGA");

                entity.Property(e => e.CrCasCarPriceBasicOutFeesTamm)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Car_Price_Basic_Out_Fees_Tamm");

                entity.Property(e => e.CrCasCarPriceBasicOutFeesTga)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Car_Price_Basic_Out_Fees_TGA");

                entity.Property(e => e.CrCasPriceCarBasicAdditionalDriverValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Additional_Driver_Value");

                entity.Property(e => e.CrCasPriceCarBasicAdditionalKmValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Additional_KM_Value");

                entity.Property(e => e.CrCasPriceCarBasicAlertHour).HasColumnName("CR_Cas_Price_Car_Basic_Alert_Hour");

                entity.Property(e => e.CrCasPriceCarBasicBrandCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Brand_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicCancelHour).HasColumnName("CR_Cas_Price_Car_Basic_Cancel_Hour");

                entity.Property(e => e.CrCasPriceCarBasicCarYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Car_Year")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicCategoryCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Category_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicCompensationAccident)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Compensation_Accident");

                entity.Property(e => e.CrCasPriceCarBasicCompensationDrowning)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Compensation_Drowning");

                entity.Property(e => e.CrCasPriceCarBasicCompensationFire)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Compensation_Fire");

                entity.Property(e => e.CrCasPriceCarBasicCompensationTheft)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Compensation_Theft");

                entity.Property(e => e.CrCasPriceCarBasicDailyRent)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Daily_Rent");

                entity.Property(e => e.CrCasPriceCarBasicDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Date");

                entity.Property(e => e.CrCasPriceCarBasicDateAboutToFinish)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Date_About_To_Finish");

                entity.Property(e => e.CrCasPriceCarBasicDistributionCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Distribution_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicEndDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Price_Car_Basic_End_Date");

                entity.Property(e => e.CrCasPriceCarBasicExtraHourValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Extra_Hour_Value");

                entity.Property(e => e.CrCasPriceCarBasicFreeAdditionalHours).HasColumnName("CR_Cas_Price_Car_Basic_Free_Additional_Hours");

                entity.Property(e => e.CrCasPriceCarBasicHourMax).HasColumnName("CR_Cas_Price_Car_Basic_Hour_Max");

                entity.Property(e => e.CrCasPriceCarBasicIsAdditionalDriver).HasColumnName("CR_Cas_Price_Car_Basic_Is_Additional_Driver");

                entity.Property(e => e.CrCasPriceCarBasicLessorCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Lessor_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicMaxAge).HasColumnName("CR_Cas_Price_Car_Basic_Max_Age");

                entity.Property(e => e.CrCasPriceCarBasicMinAge).HasColumnName("CR_Cas_Price_Car_Basic_Min_Age");

                entity.Property(e => e.CrCasPriceCarBasicModelCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Model_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicMonthlyDay).HasColumnName("CR_Cas_Price_Car_Basic_Monthly_Day");

                entity.Property(e => e.CrCasPriceCarBasicMonthlyRent)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Monthly_Rent");

                entity.Property(e => e.CrCasPriceCarBasicNoDailyFreeKm).HasColumnName("CR_Cas_Price_Car_Basic_No_Daily_Free_KM");

                entity.Property(e => e.CrCasPriceCarBasicPrivateDriverValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Private_Driver_Value");

                entity.Property(e => e.CrCasPriceCarBasicReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Reasons");

                entity.Property(e => e.CrCasPriceCarBasicRentalTaxRate)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Rental_Tax_Rate");

                entity.Property(e => e.CrCasPriceCarBasicRequireFinancialCredit).HasColumnName("CR_Cas_Price_Car_Basic_Require_Financial_Credit");

                entity.Property(e => e.CrCasPriceCarBasicStartDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Start_Date");

                entity.Property(e => e.CrCasPriceCarBasicStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicType)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Type")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicWeeklyDay).HasColumnName("CR_Cas_Price_Car_Basic_Weekly_Day");

                entity.Property(e => e.CrCasPriceCarBasicWeeklyRent)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Weekly_Rent");

                entity.Property(e => e.CrCasPriceCarBasicYear)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Basic_Year")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarBasicYearlyDay).HasColumnName("CR_Cas_Price_Car_Basic_Yearly_Day");

                entity.Property(e => e.CrCasPriceCarBasicYearlyRent)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Basic_Yearly_Rent");

                entity.HasOne(d => d.CrCasPriceCarBasicBrandCodeNavigation)
                    .WithMany(p => p.CrCasPriceCarBasics)
                    .HasForeignKey(d => d.CrCasPriceCarBasicBrandCode)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Basic_CR_Mas_Sup_Car_Brand");

                entity.HasOne(d => d.CrCasPriceCarBasicCategoryCodeNavigation)
                    .WithMany(p => p.CrCasPriceCarBasics)
                    .HasForeignKey(d => d.CrCasPriceCarBasicCategoryCode)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Basic_CR_Mas_Sup_Car_Category");

                entity.HasOne(d => d.CrCasPriceCarBasicDistributionCodeNavigation)
                    .WithMany(p => p.CrCasPriceCarBasics)
                    .HasForeignKey(d => d.CrCasPriceCarBasicDistributionCode)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Basic_CR_Mas_Sup_Car_Distribution");

                entity.HasOne(d => d.CrCasPriceCarBasicLessorCodeNavigation)
                    .WithMany(p => p.CrCasPriceCarBasics)
                    .HasForeignKey(d => d.CrCasPriceCarBasicLessorCode)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Basic_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrCasPriceCarBasicModelCodeNavigation)
                    .WithMany(p => p.CrCasPriceCarBasics)
                    .HasForeignKey(d => d.CrCasPriceCarBasicModelCode)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Basic_CR_Mas_Sup_Car_Model");
            });

            modelBuilder.Entity<CrCasPriceCarOption>(entity =>
            {
                entity.HasKey(e => new { e.CrCasPriceCarOptionsNo, e.CrCasPriceCarOptionsCode });

                entity.ToTable("CR_Cas_Price_Car_Options");

                entity.HasIndex(e => e.CrCasPriceCarOptionsCode, "IX_CR_Cas_Price_Car_Options_CR_Cas_Price_Car_Options_Code");

                entity.Property(e => e.CrCasPriceCarOptionsNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Options_No");

                entity.Property(e => e.CrCasPriceCarOptionsCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Price_Car_Options_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasPriceCarOptionsValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Price_Car_Options_Value");

                entity.HasOne(d => d.CrCasPriceCarOptionsCodeNavigation)
                    .WithMany(p => p.CrCasPriceCarOptions)
                    .HasForeignKey(d => d.CrCasPriceCarOptionsCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Options_CR_Mas_Sup_Contract_Options");

                entity.HasOne(d => d.CrCasPriceCarOptionsNoNavigation)
                    .WithMany(p => p.CrCasPriceCarOptions)
                    .HasForeignKey(d => d.CrCasPriceCarOptionsNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Price_Car_Options_CR_Cas_Price_Car_Basic");
            });

            modelBuilder.Entity<CrCasRenterContractAdditional>(entity =>
            {
                entity.HasKey(e => new { e.CrCasRenterContractAdditionalNo, e.CrCasRenterContractAdditionalCode });

                entity.ToTable("CR_Cas_Renter_Contract_Additional");

                entity.HasIndex(e => e.CrCasRenterContractAdditionalCode, "IX_CR_Cas_Renter_Contract_Additional_CR_Cas_Renter_Contract_Additional_Code");

                entity.Property(e => e.CrCasRenterContractAdditionalNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Additional_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAdditionalCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Additional_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasContractAdditionalValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Contract_Additional_Value");

                entity.HasOne(d => d.CrCasRenterContractAdditionalCodeNavigation)
                    .WithMany(p => p.CrCasRenterContractAdditionals)
                    .HasForeignKey(d => d.CrCasRenterContractAdditionalCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Additional_CR_Mas_Sup_Contract_Additional");

                entity.HasOne(d => d.CrCasRenterContractAdditionalNoNavigation)
                    .WithMany(p => p.CrCasRenterContractAdditionals)
                    .HasForeignKey(d => d.CrCasRenterContractAdditionalNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Additional_CR_Cas_Renter_Contract_Basic");
            });

            modelBuilder.Entity<CrCasRenterContractAdvantage>(entity =>
            {
                entity.HasKey(e => new { e.CrCasRenterContractAdvantagesNo, e.CrCasRenterContractAdvantagesCode });

                entity.ToTable("CR_Cas_Renter_Contract_Advantages");

                entity.HasIndex(e => e.CrCasRenterContractAdvantagesCode, "IX_CR_Cas_Renter_Contract_Advantages_CR_Cas_Renter_Contract_Advantages_Code");

                entity.Property(e => e.CrCasRenterContractAdvantagesNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Advantages_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAdvantagesCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Advantages_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasContractAdvantagesValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Contract_Advantages_Value");

                entity.HasOne(d => d.CrCasRenterContractAdvantagesCodeNavigation)
                    .WithMany(p => p.CrCasRenterContractAdvantages)
                    .HasForeignKey(d => d.CrCasRenterContractAdvantagesCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Advantages_CR_Mas_Sup_Car_Advantages");

                entity.HasOne(d => d.CrCasRenterContractAdvantagesNoNavigation)
                    .WithMany(p => p.CrCasRenterContractAdvantages)
                    .HasForeignKey(d => d.CrCasRenterContractAdvantagesNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Advantages_CR_Cas_Renter_Contract_Basic");
            });

            modelBuilder.Entity<CrCasRenterContractAlert>(entity =>
            {
                entity.HasKey(e => e.CrCasRenterContractAlertNo);

                entity.ToTable("CR_Cas_Renter_Contract_Alert");

                entity.HasIndex(e => new { e.CrCasRenterContractAlertBranch, e.CrCasRenterContractAlertLessor }, "IX_CR_Cas_Renter_Contract_Alert_CR_Cas_Renter_Contract_Alert_Branch_CR_Cas_Renter_Contract_Alert_Lessor");

                entity.Property(e => e.CrCasRenterContractAlertNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAlertBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAlertContractActiviteStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_Contract_Activite_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAlertContractStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_Contract_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAlertDayDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_Day_Date");

                entity.Property(e => e.CrCasRenterContractAlertDays).HasColumnName("CR_Cas_Renter_Contract_Alert_Days");

                entity.Property(e => e.CrCasRenterContractAlertEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_End_Date");

                entity.Property(e => e.CrCasRenterContractAlertHour).HasColumnName("CR_Cas_Renter_Contract_Alert_Hour");

                entity.Property(e => e.CrCasRenterContractAlertHourDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_Hour_Date");

                entity.Property(e => e.CrCasRenterContractAlertLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAlertStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAlertStatusMsg)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Alert_Status_Msg");

                entity.HasOne(d => d.CrCasRenterContractAlertNoNavigation)
                    .WithOne(p => p.CrCasRenterContractAlert)
                    .HasForeignKey<CrCasRenterContractAlert>(d => d.CrCasRenterContractAlertNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Alert_CR_Cas_Renter_Contract_Basic");

                entity.HasOne(d => d.CrCasRenterContractAlertNavigation)
                    .WithMany(p => p.CrCasRenterContractAlerts)
                    .HasPrincipalKey(p => new { p.CrCasBranchInformationCode, p.CrCasBranchInformationLessor })
                    .HasForeignKey(d => new { d.CrCasRenterContractAlertBranch, d.CrCasRenterContractAlertLessor })
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Alert_CR_Cas_Branch_Information");
            });

            modelBuilder.Entity<CrCasRenterContractAuthorization>(entity =>
            {
                entity.HasKey(e => e.CrCasRenterContractAuthorizationContractNo);

                entity.ToTable("CR_Cas_Renter_Contract_Authorization");

                entity.HasIndex(e => e.CrCasRenterContractAuthorizationLessor, "IX_CR_Cas_Renter_Contract_Authorization_CR_Cas_Renter_Contract_Authorization_Lessor");

                entity.Property(e => e.CrCasRenterContractAuthorizationContractNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Authorization_Contract_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAuthorizationAction).HasColumnName("CR_Cas_Renter_Contract_Authorization_Action");

                entity.Property(e => e.CrCasRenterContractAuthorizationDaysNo).HasColumnName("CR_Cas_Renter_Contract_Authorization_Days_No");

                entity.Property(e => e.CrCasRenterContractAuthorizationEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Authorization_End_Date");

                entity.Property(e => e.CrCasRenterContractAuthorizationLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Authorization_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAuthorizationNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Authorization_No");

                entity.Property(e => e.CrCasRenterContractAuthorizationStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Authorization_Start_Date");

                entity.Property(e => e.CrCasRenterContractAuthorizationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Authorization_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractAuthorizationType).HasColumnName("CR_Cas_Renter_Contract_Authorization_Type");

                entity.Property(e => e.CrCasRenterContractAuthorizationValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Authorization_Value");

                entity.HasOne(d => d.CrCasRenterContractAuthorizationContractNoNavigation)
                    .WithOne(p => p.CrCasRenterContractAuthorization)
                    .HasForeignKey<CrCasRenterContractAuthorization>(d => d.CrCasRenterContractAuthorizationContractNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Authorization_CR_Cas_Renter_Contract_Basic");

                entity.HasOne(d => d.CrCasRenterContractAuthorizationLessorNavigation)
                    .WithMany(p => p.CrCasRenterContractAuthorizations)
                    .HasForeignKey(d => d.CrCasRenterContractAuthorizationLessor)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Authorization_CR_Mas_Lessor_Information");
            });

            modelBuilder.Entity<CrCasRenterContractBasic>(entity =>
            {
                entity.HasKey(e => e.CrCasRenterContractBasicNo);

                entity.ToTable("CR_Cas_Renter_Contract_Basic");

                entity.HasIndex(e => new { e.CrCasRenterContractBasicAdditionalDriverId, e.CrCasRenterContractBasicLessor }, "IX_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Contract_Basic_Additional_Driver_Id_CR_Cas_Renter_Contract_Basic_Lessor");

                entity.HasIndex(e => new { e.CrCasRenterContractBasicBranch, e.CrCasRenterContractBasicLessor }, "IX_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Contract_Basic_Branch_CR_Cas_Renter_Contract_Basic_Lessor");

                entity.HasIndex(e => e.CrCasRenterContractBasicCarSerailNo, "IX_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Contract_Basic_Car_Serail_No");

                entity.HasIndex(e => new { e.CrCasRenterContractBasicDriverId, e.CrCasRenterContractBasicLessor }, "IX_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Contract_Basic_Driver_Id_CR_Cas_Renter_Contract_Basic_Lessor");

                entity.HasIndex(e => new { e.CrCasRenterContractBasicPrivateDriverId, e.CrCasRenterContractBasicLessor }, "IX_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Contract_Basic_Private_Driver_Id_CR_Cas_Renter_Contract_Basic_Lessor");

                entity.HasIndex(e => new { e.CrCasRenterContractBasicRenterId, e.CrCasRenterContractBasicLessor }, "IX_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Contract_Basic_Renter_Id_CR_Cas_Renter_Contract_Basic_Lessor");

                entity.HasIndex(e => e.CrCasRenterContractBasicSector, "IX_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Contract_Basic_Sector");

                entity.Property(e => e.CrCasRenterContractBasicNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractBasicAdditionalDriverId)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Additional_Driver_Id");

                entity.Property(e => e.CrCasRenterContractBasicAdditionalDriverValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Additional_Driver_Value");

                entity.Property(e => e.CrCasRenterContractBasicAdditionalValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Additional_Value");

                entity.Property(e => e.CrCasRenterContractBasicAllowCanceled)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Allow_Canceled");

                entity.Property(e => e.CrCasRenterContractBasicAmountPaidAdvance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Amount_Paid_Advance");

                entity.Property(e => e.CrCasRenterContractBasicAmountRequired)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Amount_Required");

                entity.Property(e => e.CrCasRenterContractBasicArPdfFile)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Ar_PDF_File");

                entity.Property(e => e.CrCasRenterContractBasicArTga)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Ar_TGA");

                entity.Property(e => e.CrCasRenterContractBasicAuthorizationValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Authorization_Value");

                entity.Property(e => e.CrCasRenterContractBasicBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractBasicCarSerailNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Car_Serail_No");

                entity.Property(e => e.CrCasRenterContractBasicCopy).HasColumnName("CR_Cas_Renter_Contract_Basic_Copy");

                entity.Property(e => e.CrCasRenterContractBasicCurrentReadingMeter).HasColumnName("CR_Cas_Renter_Contract_Basic_Current_Reading_Meter");

                entity.Property(e => e.CrCasRenterContractBasicDailyFreeKm).HasColumnName("CR_Cas_Renter_Contract_Basic_Daily_Free_KM");

                entity.Property(e => e.CrCasRenterContractBasicDailyFreeKmUser).HasColumnName("CR_Cas_Renter_Contract_Basic_Daily_Free_KM_User");

                entity.Property(e => e.CrCasRenterContractBasicDailyRent)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Daily_Rent");

                entity.Property(e => e.CrCasRenterContractBasicDriverId)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Driver_Id");

                entity.Property(e => e.CrCasRenterContractBasicEnPdfFile)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_En_PDF_File");

                entity.Property(e => e.CrCasRenterContractBasicEnTga)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_En_TGA");

                entity.Property(e => e.CrCasRenterContractBasicExpectedDiscountValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Discount_Value");

                entity.Property(e => e.CrCasRenterContractBasicExpectedEndDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_End_Date");

                entity.Property(e => e.CrCasRenterContractBasicExpectedOptionsValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Options_Value");

                entity.Property(e => e.CrCasRenterContractBasicExpectedPrivateDriverValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Private_Driver_Value");

                entity.Property(e => e.CrCasRenterContractBasicExpectedRentValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Rent_Value");

                entity.Property(e => e.CrCasRenterContractBasicExpectedRentalDays).HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Rental_Days");

                entity.Property(e => e.CrCasRenterContractBasicExpectedStartDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Start_Date");

                entity.Property(e => e.CrCasRenterContractBasicExpectedTaxValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Tax_Value");

                entity.Property(e => e.CrCasRenterContractBasicExpectedTotal)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Total");

                entity.Property(e => e.CrCasRenterContractBasicExpectedValueAfterDiscount)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Value_After_Discount");

                entity.Property(e => e.CrCasRenterContractBasicExpectedValueBeforDiscount)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Expected_Value_Befor_Discount");

                entity.Property(e => e.CrCasRenterContractBasicFreeHours).HasColumnName("CR_Cas_Renter_Contract_Basic_Free_Hours");

                entity.Property(e => e.CrCasRenterContractBasicHourMax).HasColumnName("CR_Cas_Renter_Contract_Basic_Hour_Max");

                entity.Property(e => e.CrCasRenterContractBasicHourValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Hour_Value");

                entity.Property(e => e.CrCasRenterContractBasicIssuedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Issued_Date");

                entity.Property(e => e.CrCasRenterContractBasicKmValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_KM_Value");

                entity.Property(e => e.CrCasRenterContractBasicLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractBasicMonthlyRent)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Monthly_Rent");

                entity.Property(e => e.CrCasRenterContractBasicPreviousBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Previous_Balance");

                entity.Property(e => e.CrCasRenterContractBasicPrivateDriverId)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Private_Driver_Id");

                entity.Property(e => e.CrCasRenterContractBasicPrivateDriverValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Private_Driver_Value");

                entity.Property(e => e.CrCasRenterContractBasicProcedures)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Procedures")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractBasicReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Reasons");

                entity.Property(e => e.CrCasRenterContractBasicRenterId)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Renter_Id");

                entity.Property(e => e.CrCasRenterContractBasicSector)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Sector")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractBasicStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractBasicTaxRate)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Tax_Rate");

                entity.Property(e => e.CrCasRenterContractBasicTotalDailyFreeKm).HasColumnName("CR_Cas_Renter_Contract_Basic_Total_Daily_Free_KM");

                entity.Property(e => e.CrCasRenterContractBasicTotalFreeHours).HasColumnName("CR_Cas_Renter_Contract_Basic_Total_Free_Hours");

                entity.Property(e => e.CrCasRenterContractBasicUserDiscountRate)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_User_Discount_Rate");

                entity.Property(e => e.CrCasRenterContractBasicUserFreeHours).HasColumnName("CR_Cas_Renter_Contract_Basic_User_Free_Hours");

                entity.Property(e => e.CrCasRenterContractBasicUserInsert)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_User_Insert");

                entity.Property(e => e.CrCasRenterContractBasicWeeklyRent)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Weekly_Rent");

                entity.Property(e => e.CrCasRenterContractBasicYear)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Year")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractBasicYearlyRent)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Basic_Yearly_Rent");

                entity.Property(e => e.CrCasRenterContractOffersReference)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Offers_Reference");

                entity.Property(e => e.CrCasRenterContractPriceReference)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Price_Reference");

                entity.Property(e => e.CrCasRenterContractUserReference)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_User_Reference");

                entity.HasOne(d => d.CrCasRenterContractBasicCarSerailNoNavigation)
                    .WithMany(p => p.CrCasRenterContractBasics)
                    .HasForeignKey(d => d.CrCasRenterContractBasicCarSerailNo)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Basic_CR_Cas_Car_Information");

                entity.HasOne(d => d.CrCasRenterContractBasicSectorNavigation)
                    .WithMany(p => p.CrCasRenterContractBasics)
                    .HasForeignKey(d => d.CrCasRenterContractBasicSector)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Basic_CR_Mas_Sup_Renter_Sector");

                entity.HasOne(d => d.CrCasRenterContractBasicNavigation)
                    .WithMany(p => p.CrCasRenterContractBasicCrCasRenterContractBasicNavigations)
                    .HasForeignKey(d => new { d.CrCasRenterContractBasicAdditionalDriverId, d.CrCasRenterContractBasicLessor })
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Lessor_Add_Driver");

                entity.HasOne(d => d.CrCasRenterContractBasic1)
                    .WithMany(p => p.CrCasRenterContractBasics)
                    .HasPrincipalKey(p => new { p.CrCasBranchInformationCode, p.CrCasBranchInformationLessor })
                    .HasForeignKey(d => new { d.CrCasRenterContractBasicBranch, d.CrCasRenterContractBasicLessor })
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Basic_CR_Cas_Branch_Information");

                entity.HasOne(d => d.CrCasRenterContractBasic2)
                    .WithMany(p => p.CrCasRenterContractBasicCrCasRenterContractBasic2s)
                    .HasForeignKey(d => new { d.CrCasRenterContractBasicDriverId, d.CrCasRenterContractBasicLessor })
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Lessor_Driver");

                entity.HasOne(d => d.CrCasRenterContractBasic3)
                    .WithMany(p => p.CrCasRenterContractBasics)
                    .HasForeignKey(d => new { d.CrCasRenterContractBasicPrivateDriverId, d.CrCasRenterContractBasicLessor })
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Lessor_Private_Driver");

                entity.HasOne(d => d.CrCasRenterContractBasic4)
                    .WithMany(p => p.CrCasRenterContractBasicCrCasRenterContractBasic4s)
                    .HasForeignKey(d => new { d.CrCasRenterContractBasicRenterId, d.CrCasRenterContractBasicLessor })
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Basic_CR_Cas_Renter_Lessor");
            });

            modelBuilder.Entity<CrCasRenterContractCarCheckup>(entity =>
            {
                entity.HasKey(e => new { e.CrCasRenterContractCarCheckupNo, e.CrCasRenterContractCarCheckupCode, e.CrCasRenterContractCarCheckupType });

                entity.ToTable("CR_Cas_Renter_Contract_Car_Checkup");

                entity.HasIndex(e => e.CrCasRenterContractCarCheckupCode, "IX_CR_Cas_Renter_Contract_Car_Checkup_CR_Cas_Renter_Contract_Car_Checkup_Code");

                entity.Property(e => e.CrCasRenterContractCarCheckupNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Car_Checkup_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractCarCheckupCode)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Car_Checkup_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractCarCheckupType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Car_Checkup_Type")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractCarCheckupReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Contract_Car_Checkup_Reasons");


                entity.HasOne(d => d.CrCasRenterContractCarCheckupCodeNavigation)
                    .WithMany(p => p.CrCasRenterContractCarCheckups)
                    .HasForeignKey(d => d.CrCasRenterContractCarCheckupCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Car_Checkup_CR_Mas_Sup_Contract_Car_Checkup");

                entity.HasOne(d => d.CrCasRenterContractCarCheckupNoNavigation)
                    .WithMany(p => p.CrCasRenterContractCarCheckups)
                    .HasForeignKey(d => d.CrCasRenterContractCarCheckupNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Car_Checkup_CR_Cas_Renter_Contract_Basic");
            });

            modelBuilder.Entity<CrCasRenterContractChoice>(entity =>
            {
                entity.HasKey(e => new { e.CrCasRenterContractChoiceNo, e.CrCasRenterContractChoiceCode });

                entity.ToTable("CR_Cas_Renter_Contract_Choice");

                entity.HasIndex(e => e.CrCasRenterContractChoiceCode, "IX_CR_Cas_Renter_Contract_Choice_CR_Cas_Renter_Contract_Choice_Code");

                entity.Property(e => e.CrCasRenterContractChoiceNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Choice_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractChoiceCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Choice_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasContractChoiceValue)
                    .HasColumnType("decimal(7, 2)")
                    .HasColumnName("CR_Cas_Contract_Choice_Value");

                entity.HasOne(d => d.CrCasRenterContractChoiceCodeNavigation)
                    .WithMany(p => p.CrCasRenterContractChoices)
                    .HasForeignKey(d => d.CrCasRenterContractChoiceCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Choice_CR_Mas_Sup_Contract_Options");

                entity.HasOne(d => d.CrCasRenterContractChoiceNoNavigation)
                    .WithMany(p => p.CrCasRenterContractChoices)
                    .HasForeignKey(d => d.CrCasRenterContractChoiceNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Choice_CR_Cas_Renter_Contract_Basic");
            });

            modelBuilder.Entity<CrCasRenterContractStatistic>(entity =>
            {
                entity.HasKey(e => e.CrCasRenterContractStatisticsNo);

                entity.ToTable("CR_Cas_Renter_Contract_Statistics");

                entity.HasIndex(e => new { e.CrCasRenterContractStatisticsBranch, e.CrCasRenterContractStatisticsLessor }, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Branch_CR_Cas_Renter_Contract_Statistics_Lessor");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsBranchCity, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Branch_City");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsBranchRegions, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Branch_Regions");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsBrand, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Brand");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsCarSerialNo, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Car_Serial_No");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsCategory, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Category");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsGender, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Gender");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsJobs, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Jobs");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsMembership, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Membership");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsModel, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Model");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsNationalities, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Nationalities");

                entity.HasIndex(e => new { e.CrCasRenterContractStatisticsRenter, e.CrCasRenterContractStatisticsLessor }, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Renter_CR_Cas_Renter_Contract_Statistics_Lessor");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsRenterCity, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Renter_City");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsRenterRegions, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_Renter_Regions");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsUserClose, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_User_Close");

                entity.HasIndex(e => e.CrCasRenterContractStatisticsUserOpen, "IX_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Statistics_User_Open");

                entity.Property(e => e.CrCasRenterContractStatisticsNo)
                    .HasMaxLength(22)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisicsDays).HasColumnName("CR_Cas_Renter_Contract_Statisics_Days");

                entity.Property(e => e.CrCasRenterContractStatisticsAdditionsHourValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Additions_Hour_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsAdditionsKmValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Additions_KM_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsAdditionsValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Additions_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsAgeNo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Age_No")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsAuthorizationValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Authorization_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsBnanValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Bnan_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsBranch)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Branch")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsBranchCity)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Branch_City")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsBranchRegions)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Branch_Regions")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsBrand)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Brand")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsCarSerialNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Car_Serial_No");

                entity.Property(e => e.CrCasRenterContractStatisticsCarYear)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Car_Year")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Category")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsCompensationValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Compensation_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsContractAfterValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Contract_After_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsContractValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Contract_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Date");

                entity.Property(e => e.CrCasRenterContractStatisticsDayClose)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Day_Close")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsDayCount)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Day_Count")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsDayCreate)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Day_Create")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsDiscountValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Discount_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsExpensesValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Expenses_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsGender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Gender")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsGmonthCreate)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_GMonth_Create")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsHmonthCreate)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_HMonth_Create")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsJobs)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Jobs")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsKm)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_KM")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsLessor)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Lessor")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsMembership)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Membership")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsModel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Model")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsNationalities)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Nationalities")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsOptionsValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Options_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsRentValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Rent_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsRenter)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Renter");

                entity.Property(e => e.CrCasRenterContractStatisticsRenterCity)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Renter_City")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsRenterRegions)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Renter_Regions")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsTaxValue)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Tax_Value");

                entity.Property(e => e.CrCasRenterContractStatisticsTimeClose)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Time_Close")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsTimeCreate)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Time_Create")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterContractStatisticsUserClose)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_User_Close");

                entity.Property(e => e.CrCasRenterContractStatisticsUserOpen)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_User_Open");

                entity.Property(e => e.CrCasRenterContractStatisticsValueNo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Contract_Statistics_Value_No")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasRenterContractStatisticsBranchCityNavigation)
                    .WithMany(p => p.CrCasRenterContractStatisticCrCasRenterContractStatisticsBranchCityNavigations)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsBranchCity)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Post_City_Brn");

                entity.HasOne(d => d.CrCasRenterContractStatisticsBranchRegionsNavigation)
                    .WithMany(p => p.CrCasRenterContractStatisticCrCasRenterContractStatisticsBranchRegionsNavigations)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsBranchRegions)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Post_Regions_Brn");

                entity.HasOne(d => d.CrCasRenterContractStatisticsBrandNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsBrand)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Car_Brand");

                entity.HasOne(d => d.CrCasRenterContractStatisticsCarSerialNoNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsCarSerialNo)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Cas_Car_Information");

                entity.HasOne(d => d.CrCasRenterContractStatisticsCategoryNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsCategory)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Car_Category");

                entity.HasOne(d => d.CrCasRenterContractStatisticsGenderNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsGender)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Renter_Gender");

                entity.HasOne(d => d.CrCasRenterContractStatisticsJobsNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsJobs)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Renter_Professions");

                entity.HasOne(d => d.CrCasRenterContractStatisticsMembershipNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsMembership)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Renter_Membership");

                entity.HasOne(d => d.CrCasRenterContractStatisticsModelNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsModel)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Car_Model");

                entity.HasOne(d => d.CrCasRenterContractStatisticsNationalitiesNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsNationalities)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Renter_Nationalities");

                entity.HasOne(d => d.CrCasRenterContractStatisticsNoNavigation)
                    .WithOne(p => p.CrCasRenterContractStatistic)
                    .HasForeignKey<CrCasRenterContractStatistic>(d => d.CrCasRenterContractStatisticsNo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Contract_Basic");

                entity.HasOne(d => d.CrCasRenterContractStatisticsRenterCityNavigation)
                    .WithMany(p => p.CrCasRenterContractStatisticCrCasRenterContractStatisticsRenterCityNavigations)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsRenterCity)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Post_City_Renter");

                entity.HasOne(d => d.CrCasRenterContractStatisticsRenterRegionsNavigation)
                    .WithMany(p => p.CrCasRenterContractStatisticCrCasRenterContractStatisticsRenterRegionsNavigations)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsRenterRegions)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_Sup_Post_Regions_Renter");

                entity.HasOne(d => d.CrCasRenterContractStatisticsUserCloseNavigation)
                    .WithMany(p => p.CrCasRenterContractStatisticCrCasRenterContractStatisticsUserCloseNavigations)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsUserClose)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_User_Information_Close");

                entity.HasOne(d => d.CrCasRenterContractStatisticsUserOpenNavigation)
                    .WithMany(p => p.CrCasRenterContractStatisticCrCasRenterContractStatisticsUserOpenNavigations)
                    .HasForeignKey(d => d.CrCasRenterContractStatisticsUserOpen)
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Mas_User_Information_Open");

                entity.HasOne(d => d.CrCasRenterContractStatistics)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasPrincipalKey(p => new { p.CrCasBranchInformationCode, p.CrCasBranchInformationLessor })
                    .HasForeignKey(d => new { d.CrCasRenterContractStatisticsBranch, d.CrCasRenterContractStatisticsLessor })
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Cas_Branch_Information");

                entity.HasOne(d => d.CrCasRenterContractStatisticsNavigation)
                    .WithMany(p => p.CrCasRenterContractStatistics)
                    .HasForeignKey(d => new { d.CrCasRenterContractStatisticsRenter, d.CrCasRenterContractStatisticsLessor })
                    .HasConstraintName("fk_CR_Cas_Renter_Contract_Statistics_CR_Cas_Renter_Lessor");
            });

            modelBuilder.Entity<CrCasRenterLessor>(entity =>
            {
                entity.HasKey(e => new { e.CrCasRenterLessorId, e.CrCasRenterLessorCode });

                entity.ToTable("CR_Cas_Renter_Lessor");

                entity.HasIndex(e => e.CrCasRenterLessorCode, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_Code");

                entity.HasIndex(e => e.CrCasRenterLessorIdtrype, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_IDTRype");

                entity.HasIndex(e => e.CrCasRenterLessorMembership, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_Membership");

                entity.HasIndex(e => e.CrCasRenterLessorSector, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_Sector");

                entity.HasIndex(e => e.CrCasRenterLessorStatisticsCity, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_Statistics_City");

                entity.HasIndex(e => e.CrCasRenterLessorStatisticsGender, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_Statistics_Gender");

                entity.HasIndex(e => e.CrCasRenterLessorStatisticsJobs, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_Statistics_Jobs");

                entity.HasIndex(e => e.CrCasRenterLessorStatisticsNationalities, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_Statistics_Nationalities");

                entity.HasIndex(e => e.CrCasRenterLessorStatisticsRegions, "IX_CR_Cas_Renter_Lessor_CR_Cas_Renter_Lessor_Statistics_Regions");

                entity.HasIndex(e => new { e.CrCasRenterLessorId, e.CrCasRenterLessorCode }, "uq_CR_Cas_Renter_Lessor")
                    .IsUnique();

                entity.Property(e => e.CrCasRenterLessorId)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Cas_Renter_Lessor_Id");

                entity.Property(e => e.CrCasRenterLessorCode)
                    .HasMaxLength(4)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorBalance)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Lessor_Balance");

                entity.Property(e => e.CrCasRenterLessorContractCount).HasColumnName("CR_Cas_Renter_Lessor_Contract_Count");

                entity.Property(e => e.CrCasRenterLessorContractDays).HasColumnName("CR_Cas_Renter_Lessor_Contract_Days");

                entity.Property(e => e.CrCasRenterLessorContractExtension).HasColumnName("CR_Cas_Renter_Lessor_Contract_Extension");

                entity.Property(e => e.CrCasRenterLessorContractKm).HasColumnName("CR_Cas_Renter_Lessor_Contract_KM");

                entity.Property(e => e.CrCasRenterLessorContractTradedAmount)
                    .HasColumnType("decimal(13, 2)")
                    .HasColumnName("CR_Cas_Renter_Lessor_Contract_Traded_Amount");

                entity.Property(e => e.CrCasRenterLessorCopyId).HasColumnName("CR_Cas_Renter_Lessor_CopyID");

                entity.Property(e => e.CrCasRenterLessorDateFirstInteraction)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Lessor_Date_First_Interaction");

                entity.Property(e => e.CrCasRenterLessorDateLastContractual)
                    .HasColumnType("date")
                    .HasColumnName("CR_Cas_Renter_Lessor_Date_Last_contractual");

                entity.Property(e => e.CrCasRenterLessorDealingMechanism)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Dealing_Mechanism")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorEvaluationNumber).HasColumnName("CR_Cas_Renter_Lessor_Evaluation_Number");

                entity.Property(e => e.CrCasRenterLessorEvaluationTotal).HasColumnName("CR_Cas_Renter_Lessor_Evaluation_Total");

                entity.Property(e => e.CrCasRenterLessorEvaluationValue)
                    .HasColumnType("decimal(5, 2)")
                    .HasColumnName("CR_Cas_Renter_Lessor_Evaluation_Value");

                entity.Property(e => e.CrCasRenterLessorIdtrype)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_IDTRype")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorMembership)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Membership")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Cas_Renter_Lessor_Reasons");

                entity.Property(e => e.CrCasRenterLessorSector)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Sector")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorStatisticsAge)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Statistics_Age")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorStatisticsCity)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Statistics_City")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorStatisticsGender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Statistics_Gender")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorStatisticsJobs)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Statistics_Jobs")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorStatisticsNationalities)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Statistics_Nationalities")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorStatisticsRegions)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Statistics_Regions")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorStatisticsTraded)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Statistics_Traded")
                    .IsFixedLength();

                entity.Property(e => e.CrCasRenterLessorStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Lessor_Status")
                    .IsFixedLength();

                entity.HasOne(d => d.CrCasRenterLessorCodeNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Lessor_Information");

                entity.HasOne(d => d.CrCasRenterLessorNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Renter_Information");

                entity.HasOne(d => d.CrCasRenterLessorIdtrypeNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorIdtrype)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Sup_Renter_IDType");

                entity.HasOne(d => d.CrCasRenterLessorMembershipNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorMembership)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Sup_Renter_Membership");

                entity.HasOne(d => d.CrCasRenterLessorSectorNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorSector)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Sup_Renter_Sector");

                entity.HasOne(d => d.CrCasRenterLessorStatisticsCityNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorStatisticsCity)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Sup_Post_City");

                entity.HasOne(d => d.CrCasRenterLessorStatisticsGenderNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorStatisticsGender)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Sup_Renter_Gender");

                entity.HasOne(d => d.CrCasRenterLessorStatisticsJobsNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorStatisticsJobs)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Sup_Renter_Professions");

                entity.HasOne(d => d.CrCasRenterLessorStatisticsNationalitiesNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorStatisticsNationalities)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Sup_Renter_Nationalities");

                entity.HasOne(d => d.CrCasRenterLessorStatisticsRegionsNavigation)
                    .WithMany(p => p.CrCasRenterLessors)
                    .HasForeignKey(d => d.CrCasRenterLessorStatisticsRegions)
                    .HasConstraintName("FK_CR_Cas_Renter_Lessor_CR_Mas_Sup_Post_Regions");
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

                entity.Property(e => e.CrCasRenterPrivateDriverInformationKeyMobile)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Cas_Renter_Private_Driver_Information_Key_Mobile");

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

            modelBuilder.Entity<CrElmEmployer>(entity =>
            {
                entity.HasKey(e => e.CrElmEmployerCode);

                entity.ToTable("CR_Elm_Employer");

                entity.Property(e => e.CrElmEmployerCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Elm_Employer_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrElmEmployerArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Elm_Employer_Ar_Name");

                entity.Property(e => e.CrElmEmployerEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Elm_Employer_En_Name");
            });

            modelBuilder.Entity<CrElmLicense>(entity =>
            {
                entity.HasKey(e => e.CrElmLicensePersonId);

                entity.ToTable("CR_Elm_License");

                entity.Property(e => e.CrElmLicensePersonId)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_License_Person_Id");

                entity.Property(e => e.CrElmLicenseArName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_License_Ar_Name");

                entity.Property(e => e.CrElmLicenseEnName)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_License_En_Name");

                entity.Property(e => e.CrElmLicenseExpiryDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Elm_License_Expiry_Date");

                entity.Property(e => e.CrElmLicenseIssuedDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Elm_License_Issued_Date");

                entity.Property(e => e.CrElmLicenseNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_License_No");
            });

            modelBuilder.Entity<CrElmPersonal>(entity =>
            {
                entity.HasKey(e => e.CrElmPersonalCode);

                entity.ToTable("CR_Elm_Personal");

                entity.Property(e => e.CrElmPersonalCode)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_Code");

                entity.Property(e => e.CrElmPersonalArGender)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_Ar_Gender");

                entity.Property(e => e.CrElmPersonalArName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Elm_Personal_Ar_Name");

                entity.Property(e => e.CrElmPersonalArNationality)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_Ar_Nationality");

                entity.Property(e => e.CrElmPersonalArProfessions)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_Ar_Professions");

                entity.Property(e => e.CrElmPersonalBirthDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Elm_Personal_Birth_Date");

                entity.Property(e => e.CrElmPersonalCountryKey)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Elm_Personal_Country_Key");

                entity.Property(e => e.CrElmPersonalEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Elm_Personal_Email");

                entity.Property(e => e.CrElmPersonalEnGender)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_En_Gender");

                entity.Property(e => e.CrElmPersonalEnName)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Elm_Personal_En_Name");

                entity.Property(e => e.CrElmPersonalEnNationality)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_En_Nationality");

                entity.Property(e => e.CrElmPersonalEnProfessions)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_En_Professions");

                entity.Property(e => e.CrElmPersonalExpiryIdDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Elm_Personal_Expiry_Id_Date");

                entity.Property(e => e.CrElmPersonalIdCopy).HasColumnName("CR_Elm_Personal_Id_Copy");

                entity.Property(e => e.CrElmPersonalIssuedIdDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Elm_Personal_Issued_Id_Date");

                entity.Property(e => e.CrElmPersonalIssuedPlace)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_Issued_Place");

                entity.Property(e => e.CrElmPersonalMobile)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_Mobile");

                entity.Property(e => e.CrElmPersonalSector)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Elm_Personal_Sector");
            });

            modelBuilder.Entity<CrElmPost>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CR_Elm_Post");

                entity.Property(e => e.CrElmPostAdditionalNo).HasColumnName("CR_Elm_Post_Additional_No");

                entity.Property(e => e.CrElmPostBuildingNo).HasColumnName("CR_Elm_Post_Building_No");

                entity.Property(e => e.CrElmPostCityArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Elm_Post_City_Ar_Name");

                entity.Property(e => e.CrElmPostCityEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Elm_Post_City_En_Name");

                entity.Property(e => e.CrElmPostCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Elm_Post_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrElmPostDistrictArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Elm_Post_District_Ar_Name");

                entity.Property(e => e.CrElmPostDistrictEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Elm_Post_District_En_Name");

                entity.Property(e => e.CrElmPostRegionsArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Elm_Post_Regions_Ar_Name");

                entity.Property(e => e.CrElmPostRegionsEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Elm_Post_Regions_En_Name");

                entity.Property(e => e.CrElmPostStreetArName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Elm_Post_Street_Ar_Name");

                entity.Property(e => e.CrElmPostStreetEnName)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Elm_Post_Street_En_Name");

                entity.Property(e => e.CrElmPostUnitNo)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Elm_Post_Unit_No");

                entity.Property(e => e.CrElmPostZipCode).HasColumnName("CR_Elm_Post_Zip_Code");
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
                    .HasMaxLength(22)
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

            modelBuilder.Entity<CrMasRenterInformation>(entity =>
            {
                entity.ToTable("CR_Mas_Renter_Information");

                entity.HasIndex(e => e.CrMasRenterInformationBank, "IX_CR_Mas_Renter_Information_CR_Mas_Renter_Information_Bank");

                entity.HasIndex(e => e.CrMasRenterInformationDrivingLicenseType, "IX_CR_Mas_Renter_Information_CR_Mas_Renter_Information_Driving_License_Type");

                entity.HasIndex(e => e.CrMasRenterInformationEmployer, "IX_CR_Mas_Renter_Information_CR_Mas_Renter_Information_Employer");

                entity.HasIndex(e => e.CrMasRenterInformationGender, "IX_CR_Mas_Renter_Information_CR_Mas_Renter_Information_Gender");

                entity.HasIndex(e => e.CrMasRenterInformationIdtype, "IX_CR_Mas_Renter_Information_CR_Mas_Renter_Information_IDType");

                entity.HasIndex(e => e.CrMasRenterInformationNationality, "IX_CR_Mas_Renter_Information_CR_Mas_Renter_Information_Nationality");

                entity.HasIndex(e => e.CrMasRenterInformationProfession, "IX_CR_Mas_Renter_Information_CR_Mas_Renter_Information_Profession");

                entity.HasIndex(e => e.CrMasRenterInformationSector, "IX_CR_Mas_Renter_Information_CR_Mas_Renter_Information_Sector");

                entity.Property(e => e.CrMasRenterInformationId)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Renter_Information_Id");

                entity.Property(e => e.CrMasRenterInformationArName)
                    .HasMaxLength(110)
                    .HasColumnName("CR_Mas_Renter_Information_Ar_Name");

                entity.Property(e => e.CrMasRenterInformationBank)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Bank")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationBirthDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Information_BirthDate");

                entity.Property(e => e.CrMasRenterInformationCommunicationLanguage)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Communication_Language")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationCopyId).HasColumnName("CR_Mas_Renter_Information_CopyID");

                entity.Property(e => e.CrMasRenterInformationCountreyKey)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Countrey_Key");

                entity.Property(e => e.CrMasRenterInformationDrivingLicenseDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Information_Driving_License_Date");

                entity.Property(e => e.CrMasRenterInformationDrivingLicenseNo)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Renter_Information_Driving_License_No");

                entity.Property(e => e.CrMasRenterInformationDrivingLicenseType)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Driving_License_Type")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationEmail)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Renter_Information_Email");

                entity.Property(e => e.CrMasRenterInformationEmployer)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Employer")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationEnName)
                    .HasMaxLength(110)
                    .HasColumnName("CR_Mas_Renter_Information_En_Name");

                entity.Property(e => e.CrMasRenterInformationExpiryDrivingLicenseDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Information_Expiry_Driving_License_Date");

                entity.Property(e => e.CrMasRenterInformationExpiryIdDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Information_Expiry_Id_Date");

                entity.Property(e => e.CrMasRenterInformationGender)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Gender")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationIban)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Iban");

                entity.Property(e => e.CrMasRenterInformationIdtype)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_IDType")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationIssueIdDate)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Information_Issue_Id_Date");

                entity.Property(e => e.CrMasRenterInformationIssuePlace)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Renter_Information_Issue_Place");

                entity.Property(e => e.CrMasRenterInformationMobile)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Mobile");

                entity.Property(e => e.CrMasRenterInformationNationality)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Nationality")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationProfession)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Profession")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Renter_Information_Reasons");

                entity.Property(e => e.CrMasRenterInformationRenterIdImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Renter_Information_Renter_Id_Image");

                entity.Property(e => e.CrMasRenterInformationRenterLicenseImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Renter_Information_Renter_License_Image");

                entity.Property(e => e.CrMasRenterInformationSector)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Sector")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationSignature)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Renter_Information_Signature");

                entity.Property(e => e.CrMasRenterInformationStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterInformationTaxNo)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Information_Tax_No");

                entity.Property(e => e.CrMasRenterInformationUpDateLicenseData)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Information_UpDate_License_Data");

                entity.Property(e => e.CrMasRenterInformationUpDatePersonalData)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Information_UpDate_Personal_Data");

                entity.Property(e => e.CrMasRenterInformationUpDateWorkplaceData)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Information_UpDate_Workplace_Data");

                entity.HasOne(d => d.CrMasRenterInformationBankNavigation)
                    .WithMany(p => p.CrMasRenterInformations)
                    .HasForeignKey(d => d.CrMasRenterInformationBank)
                    .HasConstraintName("FK_CR_Mas_Renter_Information_CR_Mas_Sup_Account_Bank");

                entity.HasOne(d => d.CrMasRenterInformationDrivingLicenseTypeNavigation)
                    .WithMany(p => p.CrMasRenterInformations)
                    .HasForeignKey(d => d.CrMasRenterInformationDrivingLicenseType)
                    .HasConstraintName("FK_CR_Mas_Renter_Information_CR_Mas_Sup_Renter_Driving_License");

                entity.HasOne(d => d.CrMasRenterInformationEmployerNavigation)
                    .WithMany(p => p.CrMasRenterInformations)
                    .HasForeignKey(d => d.CrMasRenterInformationEmployer)
                    .HasConstraintName("FK_CR_Mas_Renter_Information_CR_Mas_Sup_Renter_Employer");

                entity.HasOne(d => d.CrMasRenterInformationGenderNavigation)
                    .WithMany(p => p.CrMasRenterInformations)
                    .HasForeignKey(d => d.CrMasRenterInformationGender)
                    .HasConstraintName("FK_CR_Mas_Renter_Information_CR_Mas_Sup_Renter_Gender");

                entity.HasOne(d => d.CrMasRenterInformationIdtypeNavigation)
                    .WithMany(p => p.CrMasRenterInformations)
                    .HasForeignKey(d => d.CrMasRenterInformationIdtype)
                    .HasConstraintName("FK_CR_Mas_Renter_Information_CR_Mas_Sup_Renter_IDType");

                entity.HasOne(d => d.CrMasRenterInformationNationalityNavigation)
                    .WithMany(p => p.CrMasRenterInformations)
                    .HasForeignKey(d => d.CrMasRenterInformationNationality)
                    .HasConstraintName("FK_CR_Mas_Renter_Information_CR_Mas_Sup_Renter_Nationalities");

                entity.HasOne(d => d.CrMasRenterInformationProfessionNavigation)
                    .WithMany(p => p.CrMasRenterInformations)
                    .HasForeignKey(d => d.CrMasRenterInformationProfession)
                    .HasConstraintName("FK_CR_Mas_Renter_Information_CR_Mas_Sup_Renter_Professions");

                entity.HasOne(d => d.CrMasRenterInformationSectorNavigation)
                    .WithMany(p => p.CrMasRenterInformations)
                    .HasForeignKey(d => d.CrMasRenterInformationSector)
                    .HasConstraintName("FK_CR_Mas_Renter_Information_CR_Mas_Sup_Renter_Sector");
            });

            modelBuilder.Entity<CrMasRenterPost>(entity =>
            {
                entity.HasKey(e => e.CrMasRenterPostCode);

                entity.ToTable("CR_Mas_Renter_Post");

                entity.HasIndex(e => e.CrMasRenterPostCity, "IX_CR_Mas_Renter_Post_CR_Mas_Renter_Post_City");

                entity.HasIndex(e => e.CrMasRenterPostRegions, "IX_CR_Mas_Renter_Post_CR_Mas_Renter_Post_Regions");

                entity.Property(e => e.CrMasRenterPostCode)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Renter_Post_Code");

                entity.Property(e => e.CrMasRenterPostAdditionalNumbers)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Mas_Renter_Post_Additional_Numbers");

                entity.Property(e => e.CrMasRenterPostArConcatenate)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Mas_Renter_Post_Ar_Concatenate");

                entity.Property(e => e.CrMasRenterPostArDistrict)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Renter_Post_Ar_District");

                entity.Property(e => e.CrMasRenterPostArMailManual)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Mas_Renter_Post_Ar_Mail_Manual");

                entity.Property(e => e.CrMasRenterPostArShortConcatenate)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Mas_Renter_Post_Ar_Short_Concatenate");

                entity.Property(e => e.CrMasRenterPostArStreet)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Renter_Post_Ar_Street");

                entity.Property(e => e.CrMasRenterPostBuilding)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Mas_Renter_Post_Building");

                entity.Property(e => e.CrMasRenterPostCity)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Post_City")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterPostEnConcatenate)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Mas_Renter_Post_En_Concatenate");

                entity.Property(e => e.CrMasRenterPostEnDistrict)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Renter_Post_En_District");

                entity.Property(e => e.CrMasRenterPostEnMailManual)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Mas_Renter_Post_En_Mail_Manual");

                entity.Property(e => e.CrMasRenterPostEnShortConcatenate)
                    .HasMaxLength(200)
                    .HasColumnName("CR_Mas_Renter_Post_En_Short_Concatenate");

                entity.Property(e => e.CrMasRenterPostEnStreet)
                    .HasMaxLength(50)
                    .HasColumnName("CR_Mas_Renter_Post_En_Street");

                entity.Property(e => e.CrMasRenterPostReasons)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Renter_Post_Reasons");

                entity.Property(e => e.CrMasRenterPostRegions)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Post_Regions")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterPostShortCode)
                    .HasMaxLength(8)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Post_Short_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterPostStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Renter_Post_Status")
                    .IsFixedLength();

                entity.Property(e => e.CrMasRenterPostUnitNo)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Renter_Post_Unit_No");

                entity.Property(e => e.CrMasRenterPostUpDatePost)
                    .HasColumnType("date")
                    .HasColumnName("CR_Mas_Renter_Post_UpDate_Post");

                entity.Property(e => e.CrMasRenterPostZipCode)
                    .HasMaxLength(10)
                    .HasColumnName("CR_Mas_Renter_Post_Zip_Code");

                entity.HasOne(d => d.CrMasRenterPostCityNavigation)
                    .WithMany(p => p.CrMasRenterPosts)
                    .HasForeignKey(d => d.CrMasRenterPostCity)
                    .HasConstraintName("FK_CR_Mas_Renter_Post_CR_Mas_Sup_Post_City");

                entity.HasOne(d => d.CrMasRenterPostCodeNavigation)
                    .WithOne(p => p.CrMasRenterPost)
                    .HasForeignKey<CrMasRenterPost>(d => d.CrMasRenterPostCode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CR_Mas_Renter_Post_CR_Mas_Renter_Information");

                entity.HasOne(d => d.CrMasRenterPostRegionsNavigation)
                    .WithMany(p => p.CrMasRenterPosts)
                    .HasForeignKey(d => d.CrMasRenterPostRegions)
                    .HasConstraintName("FK_CR_Mas_Renter_Post_CR_Mas_Sup_Post_Regions");
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
                    .HasMaxLength(40)
                    .HasColumnName("CR_Mas_Sup_Account_Receipt_Reference_Ar_Name");

                entity.Property(e => e.CrMasSupAccountReceiptReferenceEnName)
                    .HasMaxLength(40)
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

                entity.Property(e => e.CrMasSupCarAdvantagesImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Advantages_Image");

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

                entity.Property(e => e.CrMasSupCarBrandImage)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sup_Car_Brand_Image");

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
                    .HasMaxLength(40)
                    .HasColumnName("CR_Mas_Sup_Car_Fuel_Ar_Name");

                entity.Property(e => e.CrMasSupCarFuelEnName)
                    .HasMaxLength(40)
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

                entity.Property(e => e.CrMasSupPostCityLatitude)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("CR_Mas_Sup_Post_City_Latitude");

                entity.Property(e => e.CrMasSupPostCityLocation).HasColumnName("CR_Mas_Sup_Post_City_Location");

                entity.Property(e => e.CrMasSupPostCityLongitude)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("CR_Mas_Sup_Post_City_Longitude");

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
                    .HasConstraintName("fk_CR_Mas_Sup_Post_City_CR_Mas_Sup_Post_Regions");
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

                entity.Property(e => e.CrMasSupPostRegionsLatitude)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("CR_Mas_Sup_Post_Regions_Latitude");

                entity.Property(e => e.CrMasSupPostRegionsLocation).HasColumnName("CR_Mas_Sup_Post_Regions_Location");

                entity.Property(e => e.CrMasSupPostRegionsLongitude)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("CR_Mas_Sup_Post_Regions_Longitude");

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
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Evaluations_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysEvaluationsArDescription)
                    .HasMaxLength(20)
                    .HasColumnName("CR_Mas_Sys_Evaluations_Ar_Description");

                entity.Property(e => e.CrMasSysEvaluationsClassification)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Evaluations_Classification")
                    .IsFixedLength();

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

            modelBuilder.Entity<CrMasSysProbabilityMembership>(entity =>
            {
                entity.HasKey(e => e.CrMasSysProbabilityMembershipCode);

                entity.ToTable("CR_Mas_Sys_Probability_Membership");

                entity.Property(e => e.CrMasSysProbabilityMembershipCode)
                    .HasMaxLength(5)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Probability_Membership_Code")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysProbabilityMembershipGroup)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_Sys_Probability_Membership_Group")
                    .IsFixedLength();

                entity.Property(e => e.CrMasSysProbabilityMembershipStetment)
                    .HasMaxLength(100)
                    .HasColumnName("CR_Mas_Sys_Probability_Membership_Stetment");
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

                entity.Property(e => e.CrMasUserBranchValidityBranchRecStatus)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("CR_Mas_User_Branch_Validity_Branch_Rec_Status")
                    .IsFixedLength();

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

                entity.HasIndex(e => e.CrMasUserContractValidityAdmin, "IX_CR_Mas_User_Contract_Validity_CR_Mas_User_Contract_Validity_Admin");

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

                entity.Property(e => e.CrMasUserContractValidityCreate).HasColumnName("CR_Mas_User_Contract_Validity_Create");

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
