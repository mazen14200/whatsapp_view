﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bnan.Inferastructure.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CR_Cas_Lessor_Classification",
                columns: table => new
                {
                    CR_Cas_Lessor_Classification_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    CR_Cas_Lessor_Classification_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Cas_Lessor_Classification_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Lessor_Classification_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Lessor_Classification_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Lessor_Classification", x => x.CR_Cas_Lessor_Classification_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Account_Bank",
                columns: table => new
                {
                    CR_Mas_Sup_Account_Bank_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Account_Bank_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Account_Bank_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Account_Bank_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Account_Bank_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Account_Bank", x => x.CR_Mas_Sup_Account_Bank_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Account_Payment_Method",
                columns: table => new
                {
                    CR_Mas_Sup_Account_Payment_Method_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Account_Payment_Method_Classification = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Account_Payment_Method_Ar_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sup_Account_Payment_Method_En_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sup_Account_Payment_Method_Accept_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Account_Payment_Method_Reject_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Account_Payment_Method_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Account_Payment_Method_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Account_Payment_Method", x => x.CR_Mas_Sup_Account_Payment_Method_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Account_Reference",
                columns: table => new
                {
                    CR_Mas_Sup_Account_Receipt_Reference_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Account_Receipt_Reference_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Account_Receipt_Reference_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Account_Payment_Method_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Account_Payment_Method_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Account_Reference", x => x.CR_Mas_Sup_Account_Receipt_Reference_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Advantages",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Advantages_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Car_Advantages_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Advantages_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Advantages_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_Advantages_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Advantages", x => x.CR_Mas_Sup_Car_Advantages_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Brand",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Brand_Code = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Mas_Sup_Car_Brand_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Brand_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Brand_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_Brand_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Brand", x => x.CR_Mas_Sup_Car_Brand_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Color",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Color_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Car_Color_Ar_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sup_Car_Color_En_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sup_Car_Color_Counter = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Car_Color_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Car_Color_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_Color_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Color", x => x.CR_Mas_Sup_Car_Color_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_CVT",
                columns: table => new
                {
                    CR_Mas_Sup_Car_CVT_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Car_CVT_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_CVT_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_CVT_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Car_CVT_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_CVT_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_CVT", x => x.CR_Mas_Sup_Car_CVT_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Fuel",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Fuel_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Car_Fuel_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Fuel_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Fuel_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Car_Fuel_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_Fuel_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Fuel", x => x.CR_Mas_Sup_Car_Fuel_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Registration",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Registration_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Car_Registration_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Registration_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Registration_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_Registration_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Registration", x => x.CR_Mas_Sup_Car_Registration_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Contract_Car_Checkup",
                columns: table => new
                {
                    CR_Mas_Sup_Contract_Car_Checkup_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Contract_Car_Checkup_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Contract_Car_Checkup_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Contract_Car_Checkup_Accept_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Car_Checkup_Reject_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Car_Checkup_Block_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Car_Checkup_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Contract_Car_Checkup_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Contract_Car_Checkup", x => x.CR_Mas_Sup_Contract_Car_Checkup_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Membership_Choice",
                columns: table => new
                {
                    CR_Mas_Sup_Membership_Choice_Code = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: false),
                    CR_Mas_Sup_Membership_Choice_Group = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Membership_Choice_Stetment = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Membership_Choice", x => x.CR_Mas_Sup_Membership_Choice_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Post_Regions",
                columns: table => new
                {
                    CR_Mas_Sup_Post_Regions_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sup_Post_Regions_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Post_Regions_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Post_Regions_Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Post_Regions_Longitude = table.Column<decimal>(type: "numeric(12,8)", nullable: true),
                    CR_Mas_Sup_Post_Regions_Latitude = table.Column<decimal>(type: "numeric(12,8)", nullable: true),
                    CR_Mas_Sup_Post_Regions_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Post_Regions_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Post_Regions", x => x.CR_Mas_Sup_Post_Regions_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_Driving_License",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_Driving_License_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    CR_Mas_Sup_Renter_Driving_License_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_Driving_License_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_Driving_License_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_Driving_License_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_Driving_License", x => x.CR_Mas_Sup_Renter_Driving_License_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_IDType",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_IDType_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    CR_Mas_Sup_Renter_IDType_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_IDType_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_IDType_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_IDType_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_IDType", x => x.CR_Mas_Sup_Renter_IDType_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_Sector",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_Sector_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    CR_Mas_Sup_Renter_Sector_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_Sector_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_Sector_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_Sector_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_Sector", x => x.CR_Mas_Sup_Renter_Sector_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Calling_Keys",
                columns: table => new
                {
                    CR_Mas_Sys_Calling_Keys_Code = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Mas_Sys_Calling_Keys_Ar_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sys_Calling_Keys_En_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sys_Calling_Keys_No = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    CR_Mas_Sys_Calling_Keys_Count = table.Column<long>(type: "bigint", nullable: true),
                    CR_Mas_Sys_Calling_Keys_Flag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sys_Calling_Keys_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Calling_Keys_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Calling_Keys", x => x.CR_Mas_Sys_Calling_Keys_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Evaluation",
                columns: table => new
                {
                    CR_Mas_Sys_Evaluations_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    CR_Mas_Sys_Evaluations_Ar_Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sys_Evaluations_En_Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sys_Service_Evaluations_Value = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sys_Evaluations_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sys_Evaluations_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Evaluations_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Evaluation", x => x.CR_Mas_Sys_Evaluations_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Evaluation_Types",
                columns: table => new
                {
                    CR_Mas_Sys_Evaluation_Types_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sys_Evaluation_Types_Kind = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Evaluation_Types_Ar_Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sys_Evaluation_Types_En_Description = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sys_Service_Evaluation_Types_Value = table.Column<long>(type: "bigint", nullable: true),
                    CR_Mas_Sys_Evaluation_Types_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sys_Evaluation_Types_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Evaluation_Types_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Evaluation_Types", x => x.CR_Mas_Sys_Evaluation_Types_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Group",
                columns: table => new
                {
                    CR_Mas_Sys_Group_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: false),
                    CR_Mas_Sys_Group_Classified = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Group_Independent = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Group_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sys_Group_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sys_Group_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Group_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Group", x => x.CR_Mas_Sys_Group_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Procedures",
                columns: table => new
                {
                    CR_Mas_Sys_Procedures_Code = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Mas_Sys_Procedures_Classification = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sys_Procedures_Subject_Alert = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Procedures_Ar_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sys_Procedures_En_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sys_Procedures_Days_Alert_About_Expire = table.Column<long>(type: "bigint", nullable: true),
                    CR_Mas_Sys_Procedures_KM_Alert_About_Expire = table.Column<long>(type: "bigint", nullable: true),
                    CR_Mas_Sys_Procedures_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Procedures_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Procedures", x => x.CR_Mas_Sys_Procedures_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Status",
                columns: table => new
                {
                    CR_Mas_Sys_Status_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    CR_Mas_Sys_Status_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sys_Status_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sys_Status_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Status_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Status", x => x.CR_Mas_Sys_Status_Code);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_System",
                columns: table => new
                {
                    CR_Mas_Sys_System_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false),
                    CR_Mas_Sys_System_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sys_System_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sys_System_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_System_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_System", x => x.CR_Mas_Sys_System_Code);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Lessor_Information",
                columns: table => new
                {
                    CR_Mas_Lessor_Information_Code = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Mas_Lessor_Information_Ar_Long_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_Ar_Short_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Lessor_Information_En_Long_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_En_Short_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Lessor_Information_Classification = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Lessor_Information_Location = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_Government_No = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: true),
                    CR_Mas_Lessor_Information_Tax_No = table.Column<string>(type: "char(30)", unicode: false, fixedLength: true, maxLength: 30, nullable: true),
                    CR_Mas_Lessor_Information_Director_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Lessor_Information_Director_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Lessor_Information_Communication_Mobile_Key = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CR_Mas_Lessor_Information_Communication_Mobile = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: true),
                    CR_Mas_Lessor_Information_Call_Free_Key = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CR_Mas_Lessor_Information_Call_Free = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: true),
                    CR_Mas_Lessor_Information_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_Twiter = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_FaceBook = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_Instagram = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_Account = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_Cont_Whatsapp_Key = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CR_Mas_Lessor_Information_Cont_Whatsapp = table.Column<string>(type: "char(20)", unicode: false, fixedLength: true, maxLength: 20, nullable: true),
                    CR_Mas_Lessor_Information_Cont_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Information_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Lessor_Information_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Lessor_Information", x => x.CR_Mas_Lessor_Information_Code);
                    table.ForeignKey(
                        name: "CR_Mas_Lessor_Information_CR_Cas_Lessor_Classification",
                        column: x => x.CR_Mas_Lessor_Information_Classification,
                        principalTable: "CR_Cas_Lessor_Classification",
                        principalColumn: "CR_Cas_Lessor_Classification_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Category",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Category_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Car_Category_Group = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Car_Category_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Car_Category_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Car_Category_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_Category_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Category", x => x.CR_Mas_Sup_Car_Category_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Car_Category_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Car_Category_Group,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Model",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Model_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Car_Model_Group = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Car_Model_Brand = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Mas_Sup_Car_Model_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Model_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Car_Model_Ar_Concatenate_Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CR_Mas_Sup_Car_Model_Concatenate_En_Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CR_Mas_Sup_Car_Model_Counter = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Car_Model_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_Model_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Model", x => x.CR_Mas_Sup_Car_Model_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Car_Model_CR_Mas_Sup_Car_Brand",
                        column: x => x.CR_Mas_Sup_Car_Model_Brand,
                        principalTable: "CR_Mas_Sup_Car_Brand",
                        principalColumn: "CR_Mas_Sup_Car_Brand_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Car_Model_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Car_Model_Group,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Year",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Year_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Car_Year_Group = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Car_Year_No = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Year", x => x.CR_Mas_Sup_Car_Year_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Car_Year_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Car_Year_Group,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Contract_Additional",
                columns: table => new
                {
                    CR_Mas_Sup_Contract_Additional_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Contract_Additional_Group = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Contract_Additional_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Contract_Additional_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Contract_Additional_Accept_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Additional_Reject_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Additional_Block_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Additional_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Contract_Additional_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Contract_Additional", x => x.CR_Mas_Sup_Contract_Additional_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Contract_Additional_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Contract_Additional_Group,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Contract_Options",
                columns: table => new
                {
                    CR_Mas_Sup_Contract_Options_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Contract_Options_Group = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Contract_Options_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Contract_Options_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sup_Contract_Options_Accept_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Options_Reject_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Options_Block_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Contract_Options_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Contract_Options_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Contract_Options", x => x.CR_Mas_Sup_Contract_Options_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Contract_Options_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Contract_Options_Group,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Post_City",
                columns: table => new
                {
                    CR_Mas_Sup_Post_City_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Post_City_Group_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Post_City_Regions_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Post_City_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Post_City_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Post_City_Concatenate_Ar_Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CR_Mas_Sup_Post_City_Concatenate_En_Name = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: true),
                    CR_Mas_Sup_Post_City_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CR_Mas_Sup_Post_City_Longitude = table.Column<decimal>(type: "numeric(12,8)", nullable: true),
                    CR_Mas_Sup_Post_City_Latitude = table.Column<decimal>(type: "numeric(12,8)", nullable: true),
                    CR_Mas_Sup_Post_City_Counter = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Post_City_Regions_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Post_City_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Post_City_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Post_City", x => x.CR_Mas_Sup_Post_City_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Post_City_CR_Mas_Sup_Post_Regions",
                        column: x => x.CR_Mas_Sup_Post_City_Regions_Code,
                        principalTable: "CR_Mas_Sup_Post_Regions",
                        principalColumn: "CR_Mas_Sup_Post_Regions_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Post_City_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Post_City_Group_Code,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_Age",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_Age_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Renter_Age_Group_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Renter_Age_No = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_Age", x => x.CR_Mas_Sup_Renter_Age_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Renter_Age_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Renter_Age_Group_Code,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_Employer",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_Employer_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Renter_Employer_Group_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Renter_Employer_Sector_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_Employer_Ar_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Renter_Employer_En_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Renter_Employer_Counter = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Renter_Employer_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_Employer_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_Employer", x => x.CR_Mas_Sup_Renter_Employer_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Renter_Employer_CR_Mas_Sup_Renter_Sector",
                        column: x => x.CR_Mas_Sup_Renter_Employer_Sector_Code,
                        principalTable: "CR_Mas_Sup_Renter_Sector",
                        principalColumn: "CR_Mas_Sup_Renter_Sector_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Renter_Employer_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Renter_Employer_Group_Code,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_Gender",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_Gender_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Renter_Gender_Group_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Renter_Gender_Ar_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sup_Renter_Gender_En_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sup_Renter_Gender_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_Gender_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_Gender", x => x.CR_Mas_Sup_Renter_Gender_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Renter_Gender_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Renter_Gender_Group_Code,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_Membership",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_Membership_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Renter_Membership_Group_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Renter_Membership_Ar_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sup_Renter_Membership_En_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sup_Renter_Membership_Accept_Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Renter_Membership_Reject_Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Renter_Membership_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_Membership_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_Membership", x => x.CR_Mas_Sup_Renter_Membership_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Renter_Membership_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Renter_Membership_Group_Code,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_Nationalities",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_Nationalities_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Renter_Nationalities_Group_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Renter_Nationalities_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_Nationalities_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_Nationalities_Flag = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Renter_Nationalities_Counter = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Renter_Nationalities_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_Nationalities_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_Nationalities", x => x.CR_Mas_Sup_Renter_Nationalities_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Renter_Nationalities_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Renter_Nationalities_Group_Code,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Renter_Professions",
                columns: table => new
                {
                    CR_Mas_Sup_Renter_Professions_Code = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Renter_Professions_Group_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sup_Renter_Professions_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_Professions_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Mas_Sup_Renter_Professions_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Renter_Professions_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Renter_Professions", x => x.CR_Mas_Sup_Renter_Professions_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Renter_Professions_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Sup_Renter_Professions_Group_Code,
                        principalTable: "CR_Mas_Sys_Group",
                        principalColumn: "CR_Mas_Sys_Group_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Main_Tasks",
                columns: table => new
                {
                    CR_Mas_Sys_Main_Tasks_Code = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Mas_Sys_Main_Tasks_System = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Main_Tasks_Ar_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sys_Main_Tasks_En_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_Sys_Main_Tasks_Concatenate_Ar_Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CR_Mas_Sys_Main_Tasks_Concatenate_En_Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CR_Mas_Sys_Main_Tasks_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Main_Tasks_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Main_Tasks", x => x.CR_Mas_Sys_Main_Tasks_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sys_Main_Tasks_CR_Mas_Sys_System",
                        column: x => x.CR_Mas_Sys_Main_Tasks_System,
                        principalTable: "CR_Mas_Sys_System",
                        principalColumn: "CR_Mas_Sys_System_Code");
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Account_Bank",
                columns: table => new
                {
                    CR_Cas_Account_Bank_Code = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: false),
                    CR_Cas_Account_Bank_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Cas_Account_Bank_No = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Cas_Account_Bank_Serail = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Cas_Account_Bank_IBAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Account_Bank_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Account_Bank_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Account_Bank_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Account_Bank_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Account_Bank", x => x.CR_Cas_Account_Bank_Code);
                    table.ForeignKey(
                        name: "FK_CR_Cas_Account_Bank_CR_Mas_Lessor_Information",
                        column: x => x.CR_Cas_Account_Bank_Lessor,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                    table.ForeignKey(
                        name: "FK_CR_Cas_Account_Bank_CR_Mas_Sup_Account_Bank",
                        column: x => x.CR_Cas_Account_Bank_No,
                        principalTable: "CR_Mas_Sup_Account_Bank",
                        principalColumn: "CR_Mas_Sup_Account_Bank_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Beneficiary",
                columns: table => new
                {
                    CR_Cas_Beneficiary_Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CR_Cas_Beneficiary_Lessor_Code = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Cas_Beneficiary_Commercial_No = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CR_Cas_Beneficiary_Sector = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Beneficiary_Ar_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Cas_Beneficiary_En_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Cas_Beneficiary_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Beneficiary_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Beneficiary", x => new { x.CR_Cas_Beneficiary_Code, x.CR_Cas_Beneficiary_Lessor_Code });
                    table.ForeignKey(
                        name: "CR_Cas_Beneficiary_CR_Mas_Lessor_Information",
                        column: x => x.CR_Cas_Beneficiary_Lessor_Code,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                    table.ForeignKey(
                        name: "CR_Cas_Beneficiary_CR_Mas_Sup_Renter_Sector",
                        column: x => x.CR_Cas_Beneficiary_Sector,
                        principalTable: "CR_Mas_Sup_Renter_Sector",
                        principalColumn: "CR_Mas_Sup_Renter_Sector_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Branch_Information",
                columns: table => new
                {
                    CR_Cas_Branch_Information_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Cas_Branch_Information_Code = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Cas_Branch_Information_Ar_TGA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Cas_Branch_Information_En_TGA = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Cas_Branch_Information_Ar_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Cas_Branch_Information_Ar_Short_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Cas_Branch_Information_En_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Cas_Branch_Information_En_Short_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Cas_Branch_Information_Government_No = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Cas_Branch_Information_Tax_No = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Cas_Branch_Information_Director_Ar_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Cas_Branch_Information_Director_En_Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    CR_Cas_Branch_Information_Telephone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Cas_Branch_Information_Mobile = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Cas_Branch_Information_Director_Signature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Cas_Branch_Information_Total_Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Cas_Branch_Information_Reserved_Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Cas_Branch_Information_Available_Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Cas_Branch_Information_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Branch_Information_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Branch_Information", x => new { x.CR_Cas_Branch_Information_Lessor, x.CR_Cas_Branch_Information_Code });
                    table.ForeignKey(
                        name: "CR_Cas_Branch_Information_CR_Mas_Lessor_Information",
                        column: x => x.CR_Cas_Branch_Information_Lessor,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Lessor_Mechanism",
                columns: table => new
                {
                    CR_Cas_Lessor_Mechanism_Code = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Cas_Lessor_Mechanism_Procedures = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Cas_Lessor_Mechanism_Procedures_Classification = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Cas_Lessor_Mechanism_Activate = table.Column<bool>(type: "bit", nullable: true),
                    CR_Cas_Lessor_Mechanism_Days_Alert_About_Expire = table.Column<int>(type: "int", nullable: true),
                    CR_Cas_Lessor_Mechanism_KM_Alert_About_Expire = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Lessor_Mechanism", x => new { x.CR_Cas_Lessor_Mechanism_Code, x.CR_Cas_Lessor_Mechanism_Procedures });
                    table.ForeignKey(
                        name: "CR_Cas_Lessor_Mechanism_CR_Mas_Lessor_Information",
                        column: x => x.CR_Cas_Lessor_Mechanism_Code,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                    table.ForeignKey(
                        name: "CR_Cas_Lessor_Mechanism_CR_Mas_Sys_Procedures",
                        column: x => x.CR_Cas_Lessor_Mechanism_Procedures,
                        principalTable: "CR_Mas_Sys_Procedures",
                        principalColumn: "CR_Mas_Sys_Procedures_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Owners",
                columns: table => new
                {
                    CR_Cas_Owners_Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    CR_Cas_Owners_Lessor_Code = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Cas_Owners_Commercial_No = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CR_Cas_Owners_Sector = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Owners_Ar_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Cas_Owners_En_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Cas_Owners_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Owners_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Owners", x => new { x.CR_Cas_Owners_Code, x.CR_Cas_Owners_Lessor_Code });
                    table.ForeignKey(
                        name: "CR_Cas_Owners_CR_Mas_Lessor_Information",
                        column: x => x.CR_Cas_Owners_Lessor_Code,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                    table.ForeignKey(
                        name: "CR_Cas_Owners_CR_Mas_Sup_Renter_Sector",
                        column: x => x.CR_Cas_Owners_Sector,
                        principalTable: "CR_Mas_Sup_Renter_Sector",
                        principalColumn: "CR_Mas_Sup_Renter_Sector_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Contract_Company",
                columns: table => new
                {
                    CR_Mas_Contract_Company_No = table.Column<string>(type: "char(22)", unicode: false, fixedLength: true, maxLength: 22, nullable: false),
                    CR_Mas_Contract_Company_Year = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Contract_Company_Sector = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Contract_Company_Procedures = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Mas_Contract_Company_Procedures_Classification = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Contract_Company_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Mas_Contract_Company_Number = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CR_Mas_Contract_Company_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_Contract_Company_Start_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_Contract_Company_End_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_Contract_Company_About_To_Expire = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_Contract_Company_Activation = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Contract_Company_Annual_Fees = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_Contract_Company_Discount_Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    CR_Mas_Contract_Company_Tax_Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    CR_Mas_Contract_Company_User_Id = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CR_Mas_Contract_Company_User_Password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CR_Mas_Contract_Company_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Contract_Company_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Contract_Company_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Contract_Company", x => x.CR_Mas_Contract_Company_No);
                    table.ForeignKey(
                        name: "Fk_CR_Mas_Contract_Company",
                        column: x => x.CR_Mas_Contract_Company_Sector,
                        principalTable: "CR_Mas_Sup_Renter_Sector",
                        principalColumn: "CR_Mas_Sup_Renter_Sector_Code");
                    table.ForeignKey(
                        name: "Fk_CR_Mas_Contract_Company_CR_Mas_Lessor_Information",
                        column: x => x.CR_Mas_Contract_Company_Lessor,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                    table.ForeignKey(
                        name: "Fk_CR_Mas_Contract_Company_CR_Mas_Sys_Procedures",
                        column: x => x.CR_Mas_Contract_Company_Procedures,
                        principalTable: "CR_Mas_Sys_Procedures",
                        principalColumn: "CR_Mas_Sys_Procedures_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Lessor_Image",
                columns: table => new
                {
                    CR_Mas_Lessor_Image_Code = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Mas_Lessor_Image_Logo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Logo_Print = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Stamp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Stamp_Outside_City = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Stamp_Outside_Country = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Stamp_Full_Amount_Paid = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Signature_Director = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Create_Contract_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Create_Contract_WhatUp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Tomorrow_Contract_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Tomorrow_Contract_WhatUp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Hour_Contract_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Hour_Contract_WhatUp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_End_Contract_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_End_Contract_WhatUp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Close_Contract_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Close_Contract_WhatUp = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Cont_Ar_Conditions_1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Cont_En_Conditions_1 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Cont_Ar_Conditions_2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Cont_En_Conditions_2 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Cont_Ar_Conditions_3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Lessor_Image_Cont_En_Conditions_3 = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Lessor_Image", x => x.CR_Mas_Lessor_Image_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Lessor_Image_CR_Mas_Sys_Group",
                        column: x => x.CR_Mas_Lessor_Image_Code,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_User_Information",
                columns: table => new
                {
                    CR_Mas_User_Information_Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CR_Mas_User_Information_PassWord = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CR_Mas_User_Information_Remind_Me = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_User_Information_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Mas_User_Information_Default_Branch = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Mas_User_Information_Default_Language = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_User_Information_Authorization_Bnan = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Information_Authorization_Admin = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Information_Authorization_Branch = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Information_Authorization_Owner = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Information_Authorization_FoolwUp = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Information_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_User_Information_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_User_Information_Tasks_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_User_Information_Tasks_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_User_Information_Reserved_Balance = table.Column<decimal>(type: "decimal(13,3)", nullable: true),
                    CR_Mas_User_Information_Total_Balance = table.Column<decimal>(type: "decimal(13,3)", nullable: true, defaultValueSql: "((0))"),
                    CR_Mas_User_Information_Available_Balance = table.Column<decimal>(type: "decimal(13,3)", nullable: true),
                    CR_Mas_User_Information_Credit_Limit = table.Column<decimal>(type: "decimal(13,3)", nullable: true),
                    CR_Mas_User_Information_Calling_Key = table.Column<string>(type: "varchar(5)", unicode: false, maxLength: 5, nullable: true),
                    CR_Mas_User_Information_Mobile_No = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CR_Mas_User_Information_Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_User_Information_ChangePassWord_Last_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_User_Information_Entry_Last_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_User_Information_Entry_Last_Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    CR_Mas_User_Information_Exit_Last_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_User_Information_Exit_Last_Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    CR_Mas_User_Information_Exit_Timer = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_User_Information_Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_User_Information_Signature = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_User_Information_Operation_Status = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Information_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_User_Information_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_User_Information", x => x.CR_Mas_User_Information_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Information_CR_Mas_Lessor_Information",
                        column: x => x.CR_Mas_User_Information_Lessor,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sup_Car_Distribution",
                columns: table => new
                {
                    CR_Mas_Sup_Car_Distribution_Brand = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Mas_Sup_Car_Distribution_Model = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Car_Distribution_Category = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Mas_Sup_Car_Distribution_Year = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Mas_Sup_Car_Distribution_Door = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Car_Distribution_Bag_Bags = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Car_Distribution_Small_Bags = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Car_Distribution_Passengers = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Car_Distribution_Count = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_Sup_Car_Distribution_Concatenate_Ar_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Car_Distribution_Concatenate_En_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Car_Distribution_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sup_Car_Distribution_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sup_Car_Distribution_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sup_Car_Distribution", x => new { x.CR_Mas_Sup_Car_Distribution_Brand, x.CR_Mas_Sup_Car_Distribution_Model, x.CR_Mas_Sup_Car_Distribution_Category, x.CR_Mas_Sup_Car_Distribution_Year });
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Brand",
                        column: x => x.CR_Mas_Sup_Car_Distribution_Brand,
                        principalTable: "CR_Mas_Sup_Car_Brand",
                        principalColumn: "CR_Mas_Sup_Car_Brand_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Category",
                        column: x => x.CR_Mas_Sup_Car_Distribution_Category,
                        principalTable: "CR_Mas_Sup_Car_Category",
                        principalColumn: "CR_Mas_Sup_Car_Category_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Model",
                        column: x => x.CR_Mas_Sup_Car_Distribution_Model,
                        principalTable: "CR_Mas_Sup_Car_Model",
                        principalColumn: "CR_Mas_Sup_Car_Model_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Lessor_Membership",
                columns: table => new
                {
                    CR_Cas_Lessor_Membership_Conditions = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    CR_Cas_Lessor_Membership_Conditions_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Cas_Lessor_Membership_Conditions_Activate = table.Column<bool>(type: "bit", nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_Amount = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_Link_2 = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_KM = table.Column<int>(type: "int", nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_Link_1 = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_Contract_No = table.Column<int>(type: "int", nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_is_Correcte = table.Column<bool>(type: "bit", nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_Choice = table.Column<string>(type: "char(5)", unicode: false, fixedLength: true, maxLength: 5, nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_No = table.Column<int>(type: "int", nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_Group = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Lessor_Membership_Conditions_Picture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Lessor_Membership", x => new { x.CR_Cas_Lessor_Membership_Conditions, x.CR_Cas_Lessor_Membership_Conditions_Lessor });
                    table.ForeignKey(
                        name: "CR_Cas_Lessor_Membership_CR_Mas_Sup_Membership_Choice",
                        column: x => x.CR_Cas_Lessor_Membership_Conditions_Choice,
                        principalTable: "CR_Mas_Sup_Membership_Choice",
                        principalColumn: "CR_Mas_Sup_Membership_Choice_Code");
                    table.ForeignKey(
                        name: "CR_Cas_Lessor_Membership_CR_Mas_Sup_Renter_Membership",
                        column: x => x.CR_Cas_Lessor_Membership_Conditions,
                        principalTable: "CR_Mas_Sup_Renter_Membership",
                        principalColumn: "CR_Mas_Sup_Renter_Membership_Code");
                    table.ForeignKey(
                        name: "CR_Mas_Lessor_Information_CR_Mas_Sup_Renter_Membership",
                        column: x => x.CR_Cas_Lessor_Membership_Conditions_Lessor,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Procedures_Tasks",
                columns: table => new
                {
                    CR_Mas_Sys_Procedures_Tasks_Sub_Tasks = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: false),
                    CR_Mas_Sys_Procedures_Tasks_System = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Main_Tasks = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Insert_Available = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Insert_Ar_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Insert_En_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UpDate_Available = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UpDate_Ar_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UpDate_En_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Hold_Available = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Hold_Ar_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Hold_En_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UnHold_Available = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UnHold_Ar_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UnHold_En_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Delete_Available = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Delete_Ar_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_Delete_En_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UnDelete_Available = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UnDelete_Ar_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    CR_Mas_Sys_Procedures_Tasks_UnDelete_En_Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Procedures_Tasks", x => x.CR_Mas_Sys_Procedures_Tasks_Sub_Tasks);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sys_Procedures_Tasks_CR_Mas_Sys_Main_Tasks",
                        column: x => x.CR_Mas_Sys_Procedures_Tasks_Main_Tasks,
                        principalTable: "CR_Mas_Sys_Main_Tasks",
                        principalColumn: "CR_Mas_Sys_Main_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sys_Procedures_Tasks_CR_Mas_Sys_System",
                        column: x => x.CR_Mas_Sys_Procedures_Tasks_System,
                        principalTable: "CR_Mas_Sys_System",
                        principalColumn: "CR_Mas_Sys_System_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Sub_Tasks",
                columns: table => new
                {
                    CR_Mas_Sys_Sub_Tasks_Code = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: false),
                    CR_Mas_Sys_Sub_Tasks_System_Code = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Sub_Tasks_Main_Code = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Mas_Sys_Sub_Tasks_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sys_Sub_Tasks_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Mas_Sys_Sub_Tasks_Procedures_Expanded = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_Sys_Sub_Tasks_Concatenate_Ar_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sys_Sub_Tasks_Concatenate_En_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_Sys_Sub_Tasks_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_Sys_Sub_Tasks_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Sub_Tasks", x => x.CR_Mas_Sys_Sub_Tasks_Code);
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sys_Sub_Tasks_CR_Mas_Sys_Main_Tasks",
                        column: x => x.CR_Mas_Sys_Sub_Tasks_Main_Code,
                        principalTable: "CR_Mas_Sys_Main_Tasks",
                        principalColumn: "CR_Mas_Sys_Main_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_Sys_Sub_Tasks_CR_Mas_Sys_System",
                        column: x => x.CR_Mas_Sys_Sub_Tasks_System_Code,
                        principalTable: "CR_Mas_Sys_System",
                        principalColumn: "CR_Mas_Sys_System_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Account_SalesPoint",
                columns: table => new
                {
                    CR_Cas_Account_SalesPoint_Code = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    CR_Cas_Account_SalesPoint_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Cas_Account_SalesPoint_Brn = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Cas_Account_SalesPoint_Bank = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Cas_Account_SalesPoint_Serial = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Cas_Account_SalesPoint_Account_Bank = table.Column<string>(type: "char(8)", unicode: false, fixedLength: true, maxLength: 8, nullable: true),
                    CR_Cas_Account_SalesPoint_No = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Account_SalesPoint_Ar_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Account_SalesPoint_En_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Account_SalesPoint_Total_Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Cas_Account_SalesPoint_Total_Reserved = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Cas_Account_SalesPoint_Total_Available = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Cas_Account_SalesPoint_Branch_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Account_SalesPoint_Bank_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Account_SalesPoint_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Account_SalesPoint_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Account_SalesPoint", x => x.CR_Cas_Account_SalesPoint_Code);
                    table.ForeignKey(
                        name: "FK_CR_Cas_Account_SalesPoint_CR_Cas_Account_Bank",
                        column: x => x.CR_Cas_Account_SalesPoint_Account_Bank,
                        principalTable: "CR_Cas_Account_Bank",
                        principalColumn: "CR_Cas_Account_Bank_Code");
                    table.ForeignKey(
                        name: "FK_CR_Cas_Account_SalesPoint_CR_Cas_Account_SalesPoint",
                        column: x => x.CR_Cas_Account_SalesPoint_Bank,
                        principalTable: "CR_Mas_Sup_Account_Bank",
                        principalColumn: "CR_Mas_Sup_Account_Bank_Code");
                    table.ForeignKey(
                        name: "FK_CR_Cas_Account_SalesPoint_CR_Cas_Branch_Information_branch_Lessor",
                        columns: x => new { x.CR_Cas_Account_SalesPoint_Lessor, x.CR_Cas_Account_SalesPoint_Brn },
                        principalTable: "CR_Cas_Branch_Information",
                        principalColumns: new[] { "CR_Cas_Branch_Information_Lessor", "CR_Cas_Branch_Information_Code" });
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Branch_Documents",
                columns: table => new
                {
                    CR_Cas_Branch_Documents_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Cas_Branch_Documents_Branch = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Cas_Branch_Documents_Procedures = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Cas_Branch_Documents_Procedures_Classification = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Cas_Branch_Documents_Activation = table.Column<bool>(type: "bit", nullable: true),
                    CR_Cas_Branch_Documents_No = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Branch_Documents_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Cas_Branch_Documents_Start_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Cas_Branch_Documents_End_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Cas_Branch_Documents_Date_About_To_Finish = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Cas_Branch_Documents_Image = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Cas_Branch_Documents_Branch_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Branch_Documents_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Cas_Branch_Documents_Reasons = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Branch_Documents", x => new { x.CR_Cas_Branch_Documents_Lessor, x.CR_Cas_Branch_Documents_Branch, x.CR_Cas_Branch_Documents_Procedures });
                    table.ForeignKey(
                        name: "CR_Cas_Branch_Documents_CR_Mas_Lessor_Branch_Information_Code",
                        columns: x => new { x.CR_Cas_Branch_Documents_Lessor, x.CR_Cas_Branch_Documents_Branch },
                        principalTable: "CR_Cas_Branch_Information",
                        principalColumns: new[] { "CR_Cas_Branch_Information_Lessor", "CR_Cas_Branch_Information_Code" });
                    table.ForeignKey(
                        name: "CR_Cas_Branch_Documents_CR_Mas_Lessor_Information_Code",
                        column: x => x.CR_Cas_Branch_Documents_Lessor,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                    table.ForeignKey(
                        name: "CR_Cas_Branch_Documents_CR_Mas_Sys_Procedures",
                        column: x => x.CR_Cas_Branch_Documents_Procedures,
                        principalTable: "CR_Mas_Sys_Procedures",
                        principalColumn: "CR_Mas_Sys_Procedures_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Cas_Branch_Post",
                columns: table => new
                {
                    CR_Cas_Branch_Post_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: false),
                    CR_Cas_Branch_Post_Branch = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Cas_Branch_Post_Short_Code = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CR_Cas_Branch_Post_Regions = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Cas_Branch_Post_City = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: true),
                    CR_Cas_Branch_Post_Ar_District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Branch_Post_En_District = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Branch_Post_Ar_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Branch_Post_En_Street = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CR_Cas_Branch_Post_Building = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CR_Cas_Branch_Post_Unit_No = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Cas_Branch_Post_Zip_Code = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CR_Cas_Branch_Post_Additional_Numbers = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CR_Cas_Branch_Post_Ar_Concatenate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CR_Cas_Branch_Post_En_Concatenate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CR_Cas_Branch_Post_Ar_Short_Concatenate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CR_Cas_Branch_Post_En_Short_Concatenate = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CR_Cas_Branch_Post_Ar_Mail_Manual = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CR_Cas_Branch_Post_En_Mail_Manual = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CR_Cas_Branch_Post_UpDate_Mail = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Cas_Branch_Post_Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CR_Cas_Branch_Post_Longitude = table.Column<decimal>(type: "numeric(12,8)", nullable: true),
                    CR_Cas_Branch_Post_Latitude = table.Column<decimal>(type: "numeric(12,8)", nullable: true),
                    CR_Cas_Branch_Post_Status = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    CR_Cas_Branch_Post_Reasons = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Cas_Branch_Post", x => new { x.CR_Cas_Branch_Post_Lessor, x.CR_Cas_Branch_Post_Branch });
                    table.ForeignKey(
                        name: "CR_Cas_Branch_Post_CR_Mas_Lessor_Branch_Information_Code",
                        columns: x => new { x.CR_Cas_Branch_Post_Lessor, x.CR_Cas_Branch_Post_Branch },
                        principalTable: "CR_Cas_Branch_Information",
                        principalColumns: new[] { "CR_Cas_Branch_Information_Lessor", "CR_Cas_Branch_Information_Code" });
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Contract_Company_Detailed",
                columns: table => new
                {
                    CR_Mas_Contract_Company_Detailed_No = table.Column<string>(type: "char(22)", unicode: false, fixedLength: true, maxLength: 22, nullable: false),
                    CR_Mas_Contract_Company_Detailed_Ser = table.Column<int>(type: "int", nullable: false),
                    CR_Mas_Contract_Company_Detailed_From_Price = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_Contract_Company_Detailed_To_Price = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_Contract_Company_Detailed_Value = table.Column<decimal>(type: "decimal(13,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Contract_Company_Detailed", x => new { x.CR_Mas_Contract_Company_Detailed_No, x.CR_Mas_Contract_Company_Detailed_Ser });
                    table.ForeignKey(
                        name: "CR_Mas_Contract_Company_Detailed_CR_Mas_Contract_Company",
                        column: x => x.CR_Mas_Contract_Company_Detailed_No,
                        principalTable: "CR_Mas_Contract_Company",
                        principalColumn: "CR_Mas_Contract_Company_No");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_Sys_Service_Evaluation",
                columns: table => new
                {
                    CR_Mas_Sys_Service_Evaluation_Contract_No = table.Column<string>(type: "varchar(22)", unicode: false, maxLength: 22, nullable: false),
                    CR_Mas_Sys_Service_Evaluation_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Mas_Sys_Service_Evaluation_Renter = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CR_Mas_Sys_Service_Evaluation_User = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CR_Mas_Sys_Service_Evaluation_Code = table.Column<string>(type: "char(2)", unicode: false, fixedLength: true, maxLength: 2, nullable: true),
                    CR_Mas_Sys_Service_Evaluation_Value = table.Column<long>(type: "bigint", nullable: true),
                    CR_Mas_Sys_Service_Evaluation_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_Sys_Service_Evaluation_Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    CR_Mas_Sys_Service_Evaluation_Reasons = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_Sys_Service_Evaluation", x => x.CR_Mas_Sys_Service_Evaluation_Contract_No);
                    table.ForeignKey(
                        name: "CR_Mas_Sys_Service_Evaluation_CR_Mas_Lessor_Information",
                        column: x => x.CR_Mas_Sys_Service_Evaluation_Lessor,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                    table.ForeignKey(
                        name: "CR_Mas_Sys_Service_Evaluation_CR_Mas_Sys_Evaluation_Types",
                        column: x => x.CR_Mas_Sys_Service_Evaluation_Code,
                        principalTable: "CR_Mas_Sys_Evaluation_Types",
                        principalColumn: "CR_Mas_Sys_Evaluation_Types_Code");
                    table.ForeignKey(
                        name: "CR_Mas_Sys_Service_Evaluation_CR_Mas_User_Information",
                        column: x => x.CR_Mas_Sys_Service_Evaluation_User,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_User_Branch_Validity",
                columns: table => new
                {
                    CR_Mas_User_Branch_Validity_Id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CR_Mas_User_Branch_Validity_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Mas_User_Branch_Validity_Branch = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_Cash_Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_Cash_Reserved = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_Cash_Available = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_SalesPoint_Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_SalesPoint_Reserved = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_SalesPoint_Available = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_Transfer_Balance = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_Transfer_Reserved = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_Transfer_Available = table.Column<decimal>(type: "decimal(13,2)", nullable: true),
                    CR_Mas_User_Branch_Validity_Branch_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_User_Branch_Validity", x => x.CR_Mas_User_Branch_Validity_Id);
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Branch_Validity_CR_Mas_User_Information",
                        column: x => x.CR_Mas_User_Branch_Validity_Id,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_User_Contract_Validity",
                columns: table => new
                {
                    CR_Mas_User_Contract_Validity_User_Id = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CR_Mas_User_Contract_Validity_Admin = table.Column<string>(type: "char(22)", unicode: false, fixedLength: true, maxLength: 22, nullable: true),
                    CR_Mas_User_Contract_Validity_Register = table.Column<bool>(type: "bit", nullable: false),
                    CR_Mas_User_Contract_Validity_Chamber = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Transfer_Permission = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Licence_Municipale = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Company_Address = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Traffic_License = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Insurance = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Operating_Card = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Chkec_Up = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Id = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Driving_License = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Renter_Address = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Employer = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Age = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Tires = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Oil = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Maintenance = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Fbrake = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Bbrake = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Discount_Rate = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    CR_Mas_User_Contract_Validity_Km = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_User_Contract_Validity_Hour = table.Column<int>(type: "int", nullable: true),
                    CR_Mas_User_Contract_Validity_Less_Contract_Value = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Cancel = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_Extension = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Contract_Validity_End = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_User_Contract_Validity", x => x.CR_Mas_User_Contract_Validity_User_Id);
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Contract_Validity_CR_Mas_User_Information",
                        column: x => x.CR_Mas_User_Contract_Validity_User_Id,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_User_Main_Validation",
                columns: table => new
                {
                    CR_Mas_User_Main_Validation_User = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CR_Mas_User_Main_Validation_Main_Tasks = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: false),
                    CR_Mas_User_Main_Validation_Main_System = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_User_Main_Validation_Authorization = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__CR_Mas_Main_Validitaion", x => new { x.CR_Mas_User_Main_Validation_User, x.CR_Mas_User_Main_Validation_Main_Tasks });
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Main_Validation_CR_Mas_Sys_Main_Tasks",
                        column: x => x.CR_Mas_User_Main_Validation_Main_Tasks,
                        principalTable: "CR_Mas_Sys_Main_Tasks",
                        principalColumn: "CR_Mas_Sys_Main_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Main_Validation_CR_Mas_Sys_System",
                        column: x => x.CR_Mas_User_Main_Validation_Main_System,
                        principalTable: "CR_Mas_Sys_System",
                        principalColumn: "CR_Mas_Sys_System_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Main_Validation_CR_Mas_User_Information",
                        column: x => x.CR_Mas_User_Main_Validation_User,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_User_Message",
                columns: table => new
                {
                    CR_Mas_User_Message_No = table.Column<int>(type: "int", nullable: false),
                    CR_Mas_User_Message_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Mas_User_Message_User_Sender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CR_Mas_User_Message_User_Receiver = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CR_Mas_User_Message_Date_Was_Sent = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_User_Message_Time_Was_Sent = table.Column<TimeSpan>(type: "time", nullable: true),
                    CR_Mas_User_Message_Date_Was_Received = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_User_Message_Time_Was_Received = table.Column<TimeSpan>(type: "time", nullable: true),
                    CR_Mas_User_Message_Content = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_User_Message_Status = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_User_Message", x => x.CR_Mas_User_Message_No);
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Message_CR_Mas_User_Information_User_Sender",
                        column: x => x.CR_Mas_User_Message_User_Sender,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Message_CR_Mas_User_Message_User_Receiver",
                        column: x => x.CR_Mas_User_Message_User_Receiver,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "varchar(10)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_CR_Mas_User_Information_UserId",
                        column: x => x.UserId,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "varchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogin_CR_Mas_User_Information_UserId",
                        column: x => x.UserId,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(10)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_CR_Mas_User_Information_UserId",
                        column: x => x.UserId,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "varchar(10)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_CR_Mas_User_Information_UserId",
                        column: x => x.UserId,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_User_Login",
                columns: table => new
                {
                    CR_Mas_User_Login_No = table.Column<int>(type: "int", nullable: false),
                    CR_Mas_User_Login_Entry_Date = table.Column<DateTime>(type: "date", nullable: true),
                    CR_Mas_User_Login_Entry_Time = table.Column<TimeSpan>(type: "time", nullable: true),
                    CR_Mas_User_Login_User = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    CR_Mas_User_Login_Lessor = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Mas_User_Login_Branch = table.Column<string>(type: "char(4)", unicode: false, fixedLength: true, maxLength: 4, nullable: true),
                    CR_Mas_User_Login_System = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_User_Login_Main_Task = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Mas_User_Login_Sub_Task = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: true),
                    CR_Mas_User_Login_Ar_Operation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_User_Login_En_Operation = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CR_Mas_User_Login_Sub_Computer_Type = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_User_Login_Sub_Computer_Code = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CR_Mas_User_Login_Concatenate_Operation_Ar_Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CR_Mas_User_Login_Concatenate_Operation_En_Description = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_User_Login", x => x.CR_Mas_User_Login_No);
                    table.ForeignKey(
                        name: "CR_Mas_User_Login_CR_Mas_Lessor_Information",
                        column: x => x.CR_Mas_User_Login_Lessor,
                        principalTable: "CR_Mas_Lessor_Information",
                        principalColumn: "CR_Mas_Lessor_Information_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Login_CR_Mas_Sys_Main_Tasks",
                        column: x => x.CR_Mas_User_Login_Main_Task,
                        principalTable: "CR_Mas_Sys_Main_Tasks",
                        principalColumn: "CR_Mas_Sys_Main_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Login_CR_Mas_Sys_Sub_Tasks",
                        column: x => x.CR_Mas_User_Login_Sub_Task,
                        principalTable: "CR_Mas_Sys_Sub_Tasks",
                        principalColumn: "CR_Mas_Sys_Sub_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Login_CR_Mas_Sys_System",
                        column: x => x.CR_Mas_User_Login_System,
                        principalTable: "CR_Mas_Sys_System",
                        principalColumn: "CR_Mas_Sys_System_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Login_CR_Mas_User_Information",
                        column: x => x.CR_Mas_User_Login_User,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_User_Procedures_Validation",
                columns: table => new
                {
                    CR_Mas_User_Procedures_Validation_Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CR_Mas_User_Procedures_Validation_Sub_Tasks = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: false),
                    CR_Mas_User_Procedures_Validation_System = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_User_Procedures_Validation_Main_Task = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Mas_User_Procedures_Validation_Insert_Authorization = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Procedures_Validation_UpDate_Authorization = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Procedures_Validation_Hold_Authorization = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Procedures_Validation_UnHold_Authorization = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Procedures_Validation_Delete_Authorization = table.Column<bool>(type: "bit", nullable: true),
                    CR_Mas_User_Procedures_Validation_UnDelete_Authorization = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_User_Procedures_Validation", x => new { x.CR_Mas_User_Procedures_Validation_Code, x.CR_Mas_User_Procedures_Validation_Sub_Tasks });
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Procedures_Validation_CR_Mas_Sys_Main_Tasks",
                        column: x => x.CR_Mas_User_Procedures_Validation_Main_Task,
                        principalTable: "CR_Mas_Sys_Main_Tasks",
                        principalColumn: "CR_Mas_Sys_Main_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Procedures_Validation_CR_Mas_Sys_Sub_Tasks",
                        column: x => x.CR_Mas_User_Procedures_Validation_Sub_Tasks,
                        principalTable: "CR_Mas_Sys_Sub_Tasks",
                        principalColumn: "CR_Mas_Sys_Sub_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Procedures_Validation_CR_Mas_Sys_System",
                        column: x => x.CR_Mas_User_Procedures_Validation_System,
                        principalTable: "CR_Mas_Sys_System",
                        principalColumn: "CR_Mas_Sys_System_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Procedures_Validation_CR_Mas_User_Information",
                        column: x => x.CR_Mas_User_Procedures_Validation_Code,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                });

            migrationBuilder.CreateTable(
                name: "CR_Mas_User_Sub_Validation",
                columns: table => new
                {
                    CR_Mas_User_Sub_Validation_User = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    CR_Mas_User_Sub_Validation_Sub_Tasks = table.Column<string>(type: "char(7)", unicode: false, fixedLength: true, maxLength: 7, nullable: false),
                    CR_Mas_User_Sub_Validation_System = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: true),
                    CR_Mas_User_Sub_Validation_Main = table.Column<string>(type: "char(3)", unicode: false, fixedLength: true, maxLength: 3, nullable: true),
                    CR_Mas_User_Sub_Validation_Authorization = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CR_Mas_User_Sub_Validation", x => new { x.CR_Mas_User_Sub_Validation_User, x.CR_Mas_User_Sub_Validation_Sub_Tasks });
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Sub_Validation_CR_Mas_Sys_Main_Tasks",
                        column: x => x.CR_Mas_User_Sub_Validation_Main,
                        principalTable: "CR_Mas_Sys_Main_Tasks",
                        principalColumn: "CR_Mas_Sys_Main_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Sub_Validation_CR_Mas_Sys_Sub_Tasks",
                        column: x => x.CR_Mas_User_Sub_Validation_Sub_Tasks,
                        principalTable: "CR_Mas_Sys_Sub_Tasks",
                        principalColumn: "CR_Mas_Sys_Sub_Tasks_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Sub_Validation_CR_Mas_Sys_System",
                        column: x => x.CR_Mas_User_Sub_Validation_System,
                        principalTable: "CR_Mas_Sys_System",
                        principalColumn: "CR_Mas_Sys_System_Code");
                    table.ForeignKey(
                        name: "FK_CR_Mas_User_Sub_Validation_CR_Mas_User_Information",
                        column: x => x.CR_Mas_User_Sub_Validation_User,
                        principalTable: "CR_Mas_User_Information",
                        principalColumn: "CR_Mas_User_Information_Code");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Account_Bank_CR_Cas_Account_Bank_Lessor",
                table: "CR_Cas_Account_Bank",
                column: "CR_Cas_Account_Bank_Lessor");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Account_Bank_CR_Cas_Account_Bank_No",
                table: "CR_Cas_Account_Bank",
                column: "CR_Cas_Account_Bank_No");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Account_SalesPoint_CR_Cas_Account_SalesPoint_Account_Bank",
                table: "CR_Cas_Account_SalesPoint",
                column: "CR_Cas_Account_SalesPoint_Account_Bank");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Account_SalesPoint_CR_Cas_Account_SalesPoint_Bank",
                table: "CR_Cas_Account_SalesPoint",
                column: "CR_Cas_Account_SalesPoint_Bank");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Account_SalesPoint_CR_Cas_Account_SalesPoint_Lessor_CR_Cas_Account_SalesPoint_Brn",
                table: "CR_Cas_Account_SalesPoint",
                columns: new[] { "CR_Cas_Account_SalesPoint_Lessor", "CR_Cas_Account_SalesPoint_Brn" });

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Beneficiary_CR_Cas_Beneficiary_Lessor_Code",
                table: "CR_Cas_Beneficiary",
                column: "CR_Cas_Beneficiary_Lessor_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Beneficiary_CR_Cas_Beneficiary_Sector",
                table: "CR_Cas_Beneficiary",
                column: "CR_Cas_Beneficiary_Sector");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Branch_Documents_CR_Cas_Branch_Documents_Procedures",
                table: "CR_Cas_Branch_Documents",
                column: "CR_Cas_Branch_Documents_Procedures");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Lessor_Mechanism_CR_Cas_Lessor_Mechanism_Procedures",
                table: "CR_Cas_Lessor_Mechanism",
                column: "CR_Cas_Lessor_Mechanism_Procedures");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Lessor_Membership_CR_Cas_Lessor_Membership_Conditions_Choice",
                table: "CR_Cas_Lessor_Membership",
                column: "CR_Cas_Lessor_Membership_Conditions_Choice");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Lessor_Membership_CR_Cas_Lessor_Membership_Conditions_Lessor",
                table: "CR_Cas_Lessor_Membership",
                column: "CR_Cas_Lessor_Membership_Conditions_Lessor");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Owners_CR_Cas_Owners_Lessor_Code",
                table: "CR_Cas_Owners",
                column: "CR_Cas_Owners_Lessor_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Cas_Owners_CR_Cas_Owners_Sector",
                table: "CR_Cas_Owners",
                column: "CR_Cas_Owners_Sector");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Contract_Company_CR_Mas_Contract_Company_Lessor",
                table: "CR_Mas_Contract_Company",
                column: "CR_Mas_Contract_Company_Lessor");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Contract_Company_CR_Mas_Contract_Company_Procedures",
                table: "CR_Mas_Contract_Company",
                column: "CR_Mas_Contract_Company_Procedures");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Contract_Company_CR_Mas_Contract_Company_Sector",
                table: "CR_Mas_Contract_Company",
                column: "CR_Mas_Contract_Company_Sector");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Lessor_Information_CR_Mas_Lessor_Information_Classification",
                table: "CR_Mas_Lessor_Information",
                column: "CR_Mas_Lessor_Information_Classification");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Car_Category_CR_Mas_Sup_Car_Category_Group",
                table: "CR_Mas_Sup_Car_Category",
                column: "CR_Mas_Sup_Car_Category_Group");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Distribution_Category",
                table: "CR_Mas_Sup_Car_Distribution",
                column: "CR_Mas_Sup_Car_Distribution_Category");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Car_Distribution_CR_Mas_Sup_Car_Distribution_Model",
                table: "CR_Mas_Sup_Car_Distribution",
                column: "CR_Mas_Sup_Car_Distribution_Model");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Car_Model_CR_Mas_Sup_Car_Model_Brand",
                table: "CR_Mas_Sup_Car_Model",
                column: "CR_Mas_Sup_Car_Model_Brand");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Car_Model_CR_Mas_Sup_Car_Model_Group",
                table: "CR_Mas_Sup_Car_Model",
                column: "CR_Mas_Sup_Car_Model_Group");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Car_Year_CR_Mas_Sup_Car_Year_Group",
                table: "CR_Mas_Sup_Car_Year",
                column: "CR_Mas_Sup_Car_Year_Group");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Contract_Additional_CR_Mas_Sup_Contract_Additional_Group",
                table: "CR_Mas_Sup_Contract_Additional",
                column: "CR_Mas_Sup_Contract_Additional_Group");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Contract_Options_CR_Mas_Sup_Contract_Options_Group",
                table: "CR_Mas_Sup_Contract_Options",
                column: "CR_Mas_Sup_Contract_Options_Group");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Post_City_CR_Mas_Sup_Post_City_Group_Code",
                table: "CR_Mas_Sup_Post_City",
                column: "CR_Mas_Sup_Post_City_Group_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Post_City_CR_Mas_Sup_Post_City_Regions_Code",
                table: "CR_Mas_Sup_Post_City",
                column: "CR_Mas_Sup_Post_City_Regions_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Renter_Age_CR_Mas_Sup_Renter_Age_Group_Code",
                table: "CR_Mas_Sup_Renter_Age",
                column: "CR_Mas_Sup_Renter_Age_Group_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Renter_Employer_CR_Mas_Sup_Renter_Employer_Group_Code",
                table: "CR_Mas_Sup_Renter_Employer",
                column: "CR_Mas_Sup_Renter_Employer_Group_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Renter_Employer_CR_Mas_Sup_Renter_Employer_Sector_Code",
                table: "CR_Mas_Sup_Renter_Employer",
                column: "CR_Mas_Sup_Renter_Employer_Sector_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Renter_Gender_CR_Mas_Sup_Renter_Gender_Group_Code",
                table: "CR_Mas_Sup_Renter_Gender",
                column: "CR_Mas_Sup_Renter_Gender_Group_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Renter_Membership_CR_Mas_Sup_Renter_Membership_Group_Code",
                table: "CR_Mas_Sup_Renter_Membership",
                column: "CR_Mas_Sup_Renter_Membership_Group_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Renter_Nationalities_CR_Mas_Sup_Renter_Nationalities_Group_Code",
                table: "CR_Mas_Sup_Renter_Nationalities",
                column: "CR_Mas_Sup_Renter_Nationalities_Group_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sup_Renter_Professions_CR_Mas_Sup_Renter_Professions_Group_Code",
                table: "CR_Mas_Sup_Renter_Professions",
                column: "CR_Mas_Sup_Renter_Professions_Group_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sys_Main_Tasks_CR_Mas_Sys_Main_Tasks_System",
                table: "CR_Mas_Sys_Main_Tasks",
                column: "CR_Mas_Sys_Main_Tasks_System");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sys_Procedures_Tasks_CR_Mas_Sys_Procedures_Tasks_Main_Tasks",
                table: "CR_Mas_Sys_Procedures_Tasks",
                column: "CR_Mas_Sys_Procedures_Tasks_Main_Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sys_Procedures_Tasks_CR_Mas_Sys_Procedures_Tasks_System",
                table: "CR_Mas_Sys_Procedures_Tasks",
                column: "CR_Mas_Sys_Procedures_Tasks_System");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sys_Service_Evaluation_CR_Mas_Sys_Service_Evaluation_Code",
                table: "CR_Mas_Sys_Service_Evaluation",
                column: "CR_Mas_Sys_Service_Evaluation_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sys_Service_Evaluation_CR_Mas_Sys_Service_Evaluation_Lessor",
                table: "CR_Mas_Sys_Service_Evaluation",
                column: "CR_Mas_Sys_Service_Evaluation_Lessor");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sys_Service_Evaluation_CR_Mas_Sys_Service_Evaluation_User",
                table: "CR_Mas_Sys_Service_Evaluation",
                column: "CR_Mas_Sys_Service_Evaluation_User");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sys_Sub_Tasks_CR_Mas_Sys_Sub_Tasks_Main_Code",
                table: "CR_Mas_Sys_Sub_Tasks",
                column: "CR_Mas_Sys_Sub_Tasks_Main_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_Sys_Sub_Tasks_CR_Mas_Sys_Sub_Tasks_System_Code",
                table: "CR_Mas_Sys_Sub_Tasks",
                column: "CR_Mas_Sys_Sub_Tasks_System_Code");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Information_CR_Mas_User_Information_Lessor",
                table: "CR_Mas_User_Information",
                column: "CR_Mas_User_Information_Lessor",
                unique: true,
                filter: "([NormalizedUserName] IS NOT NULL)");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "CR_Mas_User_Information",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Login_CR_Mas_User_Login_Lessor",
                table: "CR_Mas_User_Login",
                column: "CR_Mas_User_Login_Lessor");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Login_CR_Mas_User_Login_Main_Task",
                table: "CR_Mas_User_Login",
                column: "CR_Mas_User_Login_Main_Task");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Login_CR_Mas_User_Login_Sub_Task",
                table: "CR_Mas_User_Login",
                column: "CR_Mas_User_Login_Sub_Task");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Login_CR_Mas_User_Login_System",
                table: "CR_Mas_User_Login",
                column: "CR_Mas_User_Login_System");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Login_CR_Mas_User_Login_User",
                table: "CR_Mas_User_Login",
                column: "CR_Mas_User_Login_User");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Main_Validation_CR_Mas_User_Main_Validation_Main_System",
                table: "CR_Mas_User_Main_Validation",
                column: "CR_Mas_User_Main_Validation_Main_System");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Main_Validation_CR_Mas_User_Main_Validation_Main_Tasks",
                table: "CR_Mas_User_Main_Validation",
                column: "CR_Mas_User_Main_Validation_Main_Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Message_CR_Mas_User_Message_User_Receiver",
                table: "CR_Mas_User_Message",
                column: "CR_Mas_User_Message_User_Receiver");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Message_CR_Mas_User_Message_User_Sender",
                table: "CR_Mas_User_Message",
                column: "CR_Mas_User_Message_User_Sender");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Procedures_Validation_CR_Mas_User_Procedures_Validation_Main_Task",
                table: "CR_Mas_User_Procedures_Validation",
                column: "CR_Mas_User_Procedures_Validation_Main_Task");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Procedures_Validation_CR_Mas_User_Procedures_Validation_Sub_Tasks",
                table: "CR_Mas_User_Procedures_Validation",
                column: "CR_Mas_User_Procedures_Validation_Sub_Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Procedures_Validation_CR_Mas_User_Procedures_Validation_System",
                table: "CR_Mas_User_Procedures_Validation",
                column: "CR_Mas_User_Procedures_Validation_System");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Sub_Validation_CR_Mas_User_Sub_Validation_Main",
                table: "CR_Mas_User_Sub_Validation",
                column: "CR_Mas_User_Sub_Validation_Main");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Sub_Validation_CR_Mas_User_Sub_Validation_Sub_Tasks",
                table: "CR_Mas_User_Sub_Validation",
                column: "CR_Mas_User_Sub_Validation_Sub_Tasks");

            migrationBuilder.CreateIndex(
                name: "IX_CR_Mas_User_Sub_Validation_CR_Mas_User_Sub_Validation_System",
                table: "CR_Mas_User_Sub_Validation",
                column: "CR_Mas_User_Sub_Validation_System");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "Roles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogin_UserId",
                table: "UserLogin",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CR_Cas_Account_SalesPoint");

            migrationBuilder.DropTable(
                name: "CR_Cas_Beneficiary");

            migrationBuilder.DropTable(
                name: "CR_Cas_Branch_Documents");

            migrationBuilder.DropTable(
                name: "CR_Cas_Branch_Post");

            migrationBuilder.DropTable(
                name: "CR_Cas_Lessor_Mechanism");

            migrationBuilder.DropTable(
                name: "CR_Cas_Lessor_Membership");

            migrationBuilder.DropTable(
                name: "CR_Cas_Owners");

            migrationBuilder.DropTable(
                name: "CR_Mas_Contract_Company_Detailed");

            migrationBuilder.DropTable(
                name: "CR_Mas_Lessor_Image");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Account_Payment_Method");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Account_Reference");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Advantages");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Color");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_CVT");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Distribution");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Fuel");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Registration");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Year");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Contract_Additional");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Contract_Car_Checkup");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Contract_Options");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Post_City");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_Age");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_Driving_License");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_Employer");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_Gender");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_IDType");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_Nationalities");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_Professions");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Calling_Keys");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Evaluation");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Procedures_Tasks");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Service_Evaluation");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Status");

            migrationBuilder.DropTable(
                name: "CR_Mas_User_Branch_Validity");

            migrationBuilder.DropTable(
                name: "CR_Mas_User_Contract_Validity");

            migrationBuilder.DropTable(
                name: "CR_Mas_User_Login");

            migrationBuilder.DropTable(
                name: "CR_Mas_User_Main_Validation");

            migrationBuilder.DropTable(
                name: "CR_Mas_User_Message");

            migrationBuilder.DropTable(
                name: "CR_Mas_User_Procedures_Validation");

            migrationBuilder.DropTable(
                name: "CR_Mas_User_Sub_Validation");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "CR_Cas_Account_Bank");

            migrationBuilder.DropTable(
                name: "CR_Cas_Branch_Information");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Membership_Choice");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_Membership");

            migrationBuilder.DropTable(
                name: "CR_Mas_Contract_Company");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Category");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Model");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Post_Regions");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Evaluation_Types");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Sub_Tasks");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "CR_Mas_User_Information");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Account_Bank");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Renter_Sector");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Procedures");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sup_Car_Brand");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Group");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_Main_Tasks");

            migrationBuilder.DropTable(
                name: "CR_Mas_Lessor_Information");

            migrationBuilder.DropTable(
                name: "CR_Mas_Sys_System");

            migrationBuilder.DropTable(
                name: "CR_Cas_Lessor_Classification");
        }
    }
}
