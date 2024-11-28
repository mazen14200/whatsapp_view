using System;
using System.Collections.Generic;
using Bnan.Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WhatsUp.Core.Models
{
    //public partial class WhatsUpContext : IdentityDbContext<CrMasUserInformation>
    public partial class WhatsUpContext : DbContext

    {
        public WhatsUpContext()
        {
        }

        public WhatsUpContext(DbContextOptions<WhatsUpContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Company> Company { get; set; } = null!;

        public virtual DbSet<Connect> Connect { get; set; } = null!;
        public virtual DbSet<Renter> Renter { get; set; } = null!;
        public virtual DbSet<Message> Message { get; set; } = null!;
        public virtual DbSet<CrCasAccountReceipt> CrCasAccountReceipt { get; set; } = null!;
        public virtual DbSet<CrMasSupPostRegion> CrMasSupPostRegion { get; set; } = null!;
        public virtual DbSet<CrMasSupPostRegion_x> CrMasSupPostRegion_x { get; set; } = null!;


        //public virtual DbSet<AccountWhatsUp> AccountWhatsUp { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=.;Database=WhatsUp;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("Server=.;Database=whats_db;Trusted_Connection=True;");
                //optionsBuilder.UseSqlServer("Server=localhost,14333;Database=whats_db;User Id=bnanwhats;Password=Maz@123456en;Trusted_Connection=True;TrustServerCertificate=True;");
                optionsBuilder.UseSqlServer("Server=localhost,14333;Database=whats_db;User Id=bnanwhats;Password=Maz@123456en;Trusted_Connection=True;TrustServerCertificate=True;");
                
                //62.84.187.79
            }
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<AccountWhatsUp>(entity =>
            //{
            //    //entity.HasKey(e => e.CrMasSysNumberTextCode);
            //    entity.HasKey(e => e.AccountWhatsUpCompanyId);

            //    entity.ToTable("Account_WhatsUp");

            //    entity.Property(e => e.AccountWhatsUpCompanyId)
            //    .HasMaxLength(4)
            //    .IsUnicode(false)
            //    .HasColumnName("Account_WhatsUp_Company_Id");

            //    entity.Property(e => e.AccountWhatsUpName)
            //    .HasMaxLength(200)
            //    .HasColumnName("Account_WhatsUp_Name");

            //    entity.Property(e => e.AccountWhatsUpUserName)
            //    .HasMaxLength(50)
            //    .HasColumnName("Account_WhatsUp_UserName");

            //    entity.Property(e => e.AccountWhatsUpPassword)
            //    .HasMaxLength(50)
            //    .HasColumnName("Account_WhatsUp_Password");

            //    entity.Property(e => e.AccountWhatsUpFullPhoneNumber)
            //    .HasMaxLength(25)
            //    .IsUnicode(false)
            //    .HasColumnName("Account_WhatsUp_FullPhoneNumber");

            //    entity.Property(e => e.AccountWhatsUpApiToken)
            //    .HasMaxLength(300)
            //    .IsUnicode(false)
            //    .HasColumnName("Account_WhatsUp_ApiToken");

            //    entity.Property(e => e.AccountWhatsUpImage)
            //    .HasMaxLength(150)
            //    .IsUnicode(false)
            //    .HasColumnName("Account_WhatsUp_Image");

            //    entity.Property(e => e.AccountWhatsUpStatus)
            //    .HasMaxLength(1)
            //    .IsUnicode(false)
            //    .IsFixedLength()
            //    .HasColumnName("Account_WhatsUp_Status");

            //});


            modelBuilder.Entity<Company>(entity =>
            {
                //entity.HasKey(e => e.CrMasSysNumberTextCode);
                entity.HasKey(e => e.companyId);

                entity.ToTable("company");

                entity.Property(e => e.companyId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("company_id")
                .IsFixedLength();

                entity.Property(e => e.companyName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("company_name");

                entity.Property(e => e.companyStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("company_status");

            });

            modelBuilder.Entity<Connect>(entity =>
            {
                //entity.HasKey(e => e.connectId);

                entity.ToTable("connect");

                entity.Property(e => e.connectId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("connect_id")
                .IsFixedLength();

                entity.Property(e => e.connectSerial)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("connect_serial");

                entity.Property(e => e.connectName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("connect_name");

                entity.Property(e => e.connectMobile)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("connect_mobile");

                entity.Property(e => e.connectDeviceType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("connect_deviceType");

                entity.Property(e => e.connectIsBussenis)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("connect_isBussenis");

                entity.Property(e => e.connect_Login_Date_time)
                .HasColumnType("datetime")
                .HasColumnName("connect_Login_Datetime");

                entity.Property(e => e.connect_LogOut_Date_time)
                .HasColumnType("datetime")
                .HasColumnName("connect_LogOut_Datetime");


                entity.Property(e => e.connectStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("connect_status");



            });


            modelBuilder.Entity<Renter>(entity =>
            {
                //entity.HasKey(e => e.CrMasSysNumberTextCode);
                entity.HasKey(e => e.RenterId);

                entity.ToTable("Renter");

                entity.Property(e => e.RenterId)
                .HasMaxLength(20)
                .HasColumnName("Renter_Id");

                entity.Property(e => e.RenterPersonEnName)
                .HasMaxLength(50)
                .HasColumnName("Renter_Person_EnName");

                entity.Property(e => e.RenterPersonArName)
                .HasMaxLength(50)
                .HasColumnName("Renter_Person_ArName");

                entity.Property(e => e.RentercountryKey)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Renter_country_Key");

                entity.Property(e => e.RenterPhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Renter_PhoneNumber");

                entity.Property(e => e.RenterStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Renter_Status");

            });


            modelBuilder.Entity<Message>(entity =>
            {
                //entity.HasKey(e => e.CrMasSysNumberTextCode);
                entity.HasKey(e => e.MessageId);

                entity.ToTable("Message");

                entity.Property(e => e.MessageId)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Message_Id");

                entity.Property(e => e.MessageConnectId)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("Message_connect_Id")
                .IsFixedLength();

                entity.Property(e => e.MessageRenterId)
                .HasMaxLength(20)
                .HasColumnName("Message_Renter_Id");

                entity.Property(e => e.Message_Sent_DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Message_Sent_DateTime");

                entity.Property(e => e.Message_Received_DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Message_Received_DateTime");

                entity.Property(e => e.Message_Read_DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Message_Read_DateTime");

                entity.Property(e => e.Message_Deleted_DateTime)
                .HasColumnType("datetime")
                .HasColumnName("Message_Deleted_DateTime");



                entity.Property(e => e.MessageType)
                .HasMaxLength(2)
                .IsUnicode(false)
                .HasColumnName("Message_Type");


                entity.Property(e => e.MessagePhoneNumberFull)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Message_PhoneNumberFull");

                entity.Property(e => e.MessageText)
                .HasMaxLength(500)
                .HasColumnName("Message_Text");

                entity.Property(e => e.MessageStatus)
                .HasMaxLength(1)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("Message_Status");

            });

            modelBuilder.Entity<CrCasAccountReceipt>(entity =>
            {
                //entity.HasKey(e => e.CrMasSysNumberTextCode);
                entity.HasKey(e => e.CrCasAccountReceipt_No);

                entity.ToTable("CR_Cas_Account_Receipt");

                entity.Property(e => e.CrCasAccountReceipt_No)
                .HasMaxLength(22)
                .HasColumnName("CR_Cas_Account_Receipt_No");

                entity.Property(e => e.CrCasAccountReceipt_LessorCode)
                .HasMaxLength(4)
                .IsUnicode(false)
                .HasColumnName("CR_Cas_Account_Receipt_Lessor_Code")
                .IsFixedLength();

                entity.Property(e => e.CrCasAccountReceipt_DateTime)
                .HasColumnType("datetime")
                .HasColumnName("CR_Cas_Account_Receipt_Date");

                entity.Property(e => e.CrCasAccountReceipt_Reference_No)
                .HasMaxLength(22)
                .HasColumnName("CR_Cas_Account_Receipt_Reference_No");


                entity.Property(e => e.CrCasAccountReceipt_Payment)
                .HasColumnType("decimal(13, 2)")
                .HasColumnName("CR_Cas_Account_Receipt_Payment");

                entity.Property(e => e.CrCasAccountReceipt_Receipt)
                .HasColumnType("decimal(13, 2)")
                .HasColumnName("CR_Cas_Account_Receipt_Receipt");

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

            modelBuilder.Entity<CrMasSupPostRegion_x>(entity =>
            {
                entity.HasKey(e => e.CrMasSupPostRegionsCode);

                entity.ToTable("CR_Mas_Sup_Post_Regions_x");

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



            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
