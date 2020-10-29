﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using manahil.Models;

namespace manahil.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20201016053011_updateDeposit")]
    partial class updateDeposit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("manahil.Models.ApprovalProjects", b =>
                {
                    b.Property<int>("ApprovalProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("ApprovalDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("ApprovedMoney")
                        .HasColumnType("float");

                    b.Property<string>("AuditStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CertificateStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("CurrencyRate")
                        .HasColumnType("float");

                    b.Property<int?>("DonorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndtDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("ExpenseAmount")
                        .HasColumnType("float");

                    b.Property<string>("MonumentalNo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectImplementAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProjectName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SerialNo")
                        .HasColumnType("int");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("TotalAcceptedMoney")
                        .HasColumnType("float");

                    b.HasKey("ApprovalProjectId");

                    b.HasIndex("DonorId");

                    b.ToTable("ApprovalProjects");
                });

            modelBuilder.Entity("manahil.Models.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("manahil.Models.City", b =>
                {
                    b.Property<int>("CityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("CityId");

                    b.HasIndex("CountryId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("manahil.Models.Contractor", b =>
                {
                    b.Property<int>("ContractorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("PrintEmail")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.HasKey("ContractorId");

                    b.HasIndex("CityId");

                    b.ToTable("Contractors");
                });

            modelBuilder.Entity("manahil.Models.Country", b =>
                {
                    b.Property<int>("CountryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("CountryId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("manahil.Models.DepositAccount", b =>
                {
                    b.Property<int>("DepositAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Balance")
                        .HasColumnType("float");

                    b.Property<double>("DepositAmount")
                        .HasColumnType("float");

                    b.Property<string>("DepositType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Donor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TransactionDate")
                        .HasColumnType("datetime2");

                    b.HasKey("DepositAccountId");

                    b.ToTable("DepositAccounts");
                });

            modelBuilder.Entity("manahil.Models.Designation", b =>
                {
                    b.Property<int>("DesignationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("DesignationId");

                    b.ToTable("Designations");
                });

            modelBuilder.Entity("manahil.Models.Donor", b =>
                {
                    b.Property<int>("DonorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("CountryId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<string>("OwnerName")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.HasKey("DonorId");

                    b.HasIndex("CountryId");

                    b.ToTable("Donors");
                });

            modelBuilder.Entity("manahil.Models.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<int?>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Contact")
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<int?>("DesignationId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<bool>("Status")
                        .HasColumnType("bit");

                    b.HasKey("EmployeeId");

                    b.HasIndex("CityId");

                    b.HasIndex("DesignationId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("manahil.Models.ManageDrive", b =>
                {
                    b.Property<int>("ManageDriveId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.Property<string>("DriveLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("ManageDriveId");

                    b.HasIndex("DonorId");

                    b.ToTable("ManageDrives");
                });

            modelBuilder.Entity("manahil.Models.Payment", b =>
                {
                    b.Property<int>("PaymentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ContractorId")
                        .HasColumnType("int");

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.Property<string>("Invoice")
                        .HasColumnType("nvarchar(500)")
                        .HasMaxLength(500);

                    b.Property<int>("PaidProjects")
                        .HasColumnType("int");

                    b.Property<DateTime>("PaymentDate")
                        .HasColumnType("datetime2")
                        .HasMaxLength(100);

                    b.HasKey("PaymentId");

                    b.HasIndex("ContractorId");

                    b.HasIndex("DonorId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("manahil.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("CategoryId")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<DateTime?>("CompletedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ContractorId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DistributionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DonorId")
                        .HasColumnType("int");

                    b.Property<string>("DonorSerial")
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.Property<int?>("EmployeeId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("GetDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ManahilSerial")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PaymentStatus")
                        .HasColumnType("bit");

                    b.Property<string>("TamidNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ContractorId");

                    b.HasIndex("DonorId");

                    b.HasIndex("EmployeeId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("manahil.Models.Thana", b =>
                {
                    b.Property<int>("ThanaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(200)")
                        .HasMaxLength(200);

                    b.HasKey("ThanaId");

                    b.HasIndex("CityId");

                    b.ToTable("Thanas");
                });

            modelBuilder.Entity("manahil.Models.TransferAccount", b =>
                {
                    b.Property<int>("TransferAccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AgainstProject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApprovalStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ContractorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DepositAccountId")
                        .HasColumnType("int");

                    b.Property<string>("FC1orFD7")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("TransferAmount")
                        .HasColumnType("float");

                    b.Property<DateTime>("TransferDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TransferAccountId");

                    b.HasIndex("DepositAccountId");

                    b.ToTable("TransferAccounts");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("manahil.Models.ApprovalProjects", b =>
                {
                    b.HasOne("manahil.Models.Donor", "Donor")
                        .WithMany()
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("manahil.Models.City", b =>
                {
                    b.HasOne("manahil.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("manahil.Models.Contractor", b =>
                {
                    b.HasOne("manahil.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("manahil.Models.Donor", b =>
                {
                    b.HasOne("manahil.Models.Country", "Country")
                        .WithMany()
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("manahil.Models.Employee", b =>
                {
                    b.HasOne("manahil.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("manahil.Models.Designation", "Designation")
                        .WithMany()
                        .HasForeignKey("DesignationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("manahil.Models.ManageDrive", b =>
                {
                    b.HasOne("manahil.Models.Donor", "Donor")
                        .WithMany()
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("manahil.Models.Payment", b =>
                {
                    b.HasOne("manahil.Models.Contractor", "Contractor")
                        .WithMany()
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("manahil.Models.Donor", "Donor")
                        .WithMany()
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("manahil.Models.Project", b =>
                {
                    b.HasOne("manahil.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("manahil.Models.Contractor", "Contractor")
                        .WithMany("Projects")
                        .HasForeignKey("ContractorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("manahil.Models.Donor", "Donor")
                        .WithMany()
                        .HasForeignKey("DonorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("manahil.Models.Employee", "Employee")
                        .WithMany("Projects")
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("manahil.Models.Thana", b =>
                {
                    b.HasOne("manahil.Models.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("manahil.Models.TransferAccount", b =>
                {
                    b.HasOne("manahil.Models.DepositAccount", "DepositAccount")
                        .WithMany()
                        .HasForeignKey("DepositAccountId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
