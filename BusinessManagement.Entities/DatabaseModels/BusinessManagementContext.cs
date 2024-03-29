﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessManagement.Entities.DatabaseModels
{
    public partial class BusinessManagementContext : DbContext
    {
        public BusinessManagementContext()
        {
        }

        public BusinessManagementContext(DbContextOptions<BusinessManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<AccountGroup> AccountGroups { get; set; }
        public virtual DbSet<AccountOperation> AccountOperations { get; set; }
        public virtual DbSet<AccountOperationDetail> AccountOperationDetails { get; set; }
        public virtual DbSet<AccountOperationType> AccountOperationTypes { get; set; }
        public virtual DbSet<AccountType> AccountTypes { get; set; }
        public virtual DbSet<Apartment> Apartments { get; set; }
        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<Branch> Branches { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<Cash> Cashes { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Currency> Currencies { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeeType> EmployeeTypes { get; set; }
        public virtual DbSet<Flat> Flats { get; set; }
        public virtual DbSet<FullAddress> FullAddresses { get; set; }
        public virtual DbSet<HouseOwner> HouseOwners { get; set; }
        public virtual DbSet<Manager> Managers { get; set; }
        public virtual DbSet<OperationClaim> OperationClaims { get; set; }
        public virtual DbSet<Section> Sections { get; set; }
        public virtual DbSet<SectionGroup> SectionGroups { get; set; }
        public virtual DbSet<SystemUser> SystemUsers { get; set; }
        public virtual DbSet<SystemUserClaim> SystemUserClaims { get; set; }
        public virtual DbSet<Tenant> Tenants { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=work\\sqlserver;Database=BusinessManagement;User Id=sa;Password=Candiltos96.;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("Account");

                entity.Property(e => e.AccountCode)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Balance).HasColumnType("money");

                entity.Property(e => e.CreditBalance).HasColumnType("money");

                entity.Property(e => e.DebitBalance).HasColumnType("money");

                entity.Property(e => e.Limit).HasColumnType("money");

                entity.HasOne(d => d.AccountGroup)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__Account__4316F928");

                entity.HasOne(d => d.AccountType)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.AccountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__Account__440B1D61");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__BranchI__4222D4EF");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Accounts)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Account__Busines__412EB0B6");
            });

            modelBuilder.Entity<AccountGroup>(entity =>
            {
                entity.ToTable("AccountGroup");

                entity.Property(e => e.AccountGroupCode)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.AccountGroupName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AccountOperation>(entity =>
            {
                entity.ToTable("AccountOperation");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.HasOne(d => d.AccountOperationType)
                    .WithMany(p => p.AccountOperations)
                    .HasForeignKey(d => d.AccountOperationTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Accou__08B54D69");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AccountOperations)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Branc__07C12930");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.AccountOperations)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Busin__06CD04F7");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AccountOperations)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Emplo__09A971A2");
            });

            modelBuilder.Entity<AccountOperationDetail>(entity =>
            {
                entity.ToTable("AccountOperationDetail");

                entity.Property(e => e.CreditBalance).HasColumnType("money");

                entity.Property(e => e.DebitBalance).HasColumnType("money");

                entity.Property(e => e.DocumentCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ExchangeRate).HasColumnType("money");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.AccountOperationDetails)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Accou__0F624AF8");

                entity.HasOne(d => d.AccountOperation)
                    .WithMany(p => p.AccountOperationDetails)
                    .HasForeignKey(d => d.AccountOperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Accou__0E6E26BF");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.AccountOperationDetails)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Branc__0D7A0286");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.AccountOperationDetails)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Busin__0C85DE4D");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.AccountOperationDetails)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__AccountOp__Curre__10566F31");
            });

            modelBuilder.Entity<AccountOperationType>(entity =>
            {
                entity.ToTable("AccountOperationType");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.ToTable("AccountType");

                entity.Property(e => e.AccountTypeName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Apartment>(entity =>
            {
                entity.ToTable("Apartment");

                entity.Property(e => e.ApartmentCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ApartmentName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__Branc__797309D9");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__Busin__787EE5A0");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__Manag__7A672E12");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Apartments)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Apartment__Secti__778AC167");
            });

            modelBuilder.Entity<Bank>(entity =>
            {
                entity.ToTable("Bank");

                entity.Property(e => e.BankAccountCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BankBranchCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BankBranchName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.BankCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.BankName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Iban)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.OfficerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Banks)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bank__AccountId__4E88ABD4");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Banks)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bank__BranchId__4D94879B");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Banks)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bank__BusinessId__4CA06362");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Banks)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bank__CurrencyId__5070F446");

                entity.HasOne(d => d.FullAddress)
                    .WithMany(p => p.Banks)
                    .HasForeignKey(d => d.FullAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bank__FullAddres__4F7CD00D");
            });

            modelBuilder.Entity<Branch>(entity =>
            {
                entity.ToTable("Branch");

                entity.Property(e => e.BranchCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.BranchName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Branch__Business__37A5467C");

                entity.HasOne(d => d.FullAddress)
                    .WithMany(p => p.Branches)
                    .HasForeignKey(d => d.FullAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Branch__FullAddr__38996AB5");
            });

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");

                entity.Property(e => e.BusinessCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.BusinessName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.HasOne(d => d.OwnerSystemUser)
                    .WithMany(p => p.Businesses)
                    .HasForeignKey(d => d.OwnerSystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Business__OwnerS__34C8D9D1");
            });

            modelBuilder.Entity<Cash>(entity =>
            {
                entity.ToTable("Cash");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Cashes)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cash__AccountId__48CFD27E");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Cashes)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cash__BranchId__47DBAE45");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Cashes)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cash__BusinessId__46E78A0C");

                entity.HasOne(d => d.Currency)
                    .WithMany(p => p.Cashes)
                    .HasForeignKey(d => d.CurrencyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cash__CurrencyId__49C3F6B7");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.ToTable("City");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Currency>(entity =>
            {
                entity.ToTable("Currency");

                entity.Property(e => e.CurrencyId).ValueGeneratedOnAdd();

                entity.Property(e => e.CurrencyName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CurrencySymbol)
                    .IsRequired()
                    .HasMaxLength(5);
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.AvatarUrl)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.NameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxOffice).HasMaxLength(100);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__Accoun__60A75C0F");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__Branch__5FB337D6");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Customer__Busine__5EBF139D");
            });

            modelBuilder.Entity<District>(entity =>
            {
                entity.ToTable("District");

                entity.Property(e => e.DistrictName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Districts)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__District__CityId__2E1BDC42");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.AvatarUrl)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.NameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.QuitDate).HasColumnType("date");

                entity.Property(e => e.StartDate).HasColumnType("date");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Accoun__5AEE82B9");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Branch__59FA5E80");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Busine__59063A47");

                entity.HasOne(d => d.EmployeeType)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.EmployeeTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employee__Employ__5BE2A6F2");
            });

            modelBuilder.Entity<EmployeeType>(entity =>
            {
                entity.ToTable("EmployeeType");

                entity.Property(e => e.EmployeeTypeName)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Flat>(entity =>
            {
                entity.ToTable("Flat");

                entity.Property(e => e.FlatCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Apartment)
                    .WithMany(p => p.Flats)
                    .HasForeignKey(d => d.ApartmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Flat__ApartmentI__7E37BEF6");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Flats)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Flat__BranchId__00200768");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Flats)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Flat__BusinessId__7F2BE32F");

                entity.HasOne(d => d.HouseOwner)
                    .WithMany(p => p.Flats)
                    .HasForeignKey(d => d.HouseOwnerId)
                    .HasConstraintName("FK__Flat__HouseOwner__01142BA1");

                entity.HasOne(d => d.Section)
                    .WithMany(p => p.Flats)
                    .HasForeignKey(d => d.SectionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Flat__SectionId__7D439ABD");

                entity.HasOne(d => d.Tenant)
                    .WithMany(p => p.Flats)
                    .HasForeignKey(d => d.TenantId)
                    .HasConstraintName("FK__Flat__TenantId__02084FDA");
            });

            modelBuilder.Entity<FullAddress>(entity =>
            {
                entity.ToTable("FullAddress");

                entity.Property(e => e.AddressText)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.AddressTitle)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.FullAddresses)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FullAddre__CityI__30F848ED");

                entity.HasOne(d => d.District)
                    .WithMany(p => p.FullAddresses)
                    .HasForeignKey(d => d.DistrictId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__FullAddre__Distr__31EC6D26");
            });

            modelBuilder.Entity<HouseOwner>(entity =>
            {
                entity.ToTable("HouseOwner");

                entity.Property(e => e.AvatarUrl)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.NameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxOffice).HasMaxLength(100);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.HouseOwners)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HouseOwne__Accou__656C112C");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.HouseOwners)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HouseOwne__Branc__6477ECF3");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.HouseOwners)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__HouseOwne__Busin__6383C8BA");
            });

            modelBuilder.Entity<Manager>(entity =>
            {
                entity.ToTable("Manager");

                entity.Property(e => e.AvatarUrl)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.NameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxOffice).HasMaxLength(100);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Manager__BranchI__5441852A");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Managers)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Manager__Busines__534D60F1");
            });

            modelBuilder.Entity<OperationClaim>(entity =>
            {
                entity.ToTable("OperationClaim");

                entity.Property(e => e.OperationClaimName)
                    .IsRequired()
                    .HasMaxLength(500);
            });

            modelBuilder.Entity<Section>(entity =>
            {
                entity.ToTable("Section");

                entity.Property(e => e.SectionCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.SectionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__BranchI__72C60C4A");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__Busines__71D1E811");

                entity.HasOne(d => d.FullAddress)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.FullAddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__FullAdd__74AE54BC");

                entity.HasOne(d => d.Manager)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.ManagerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__Manager__73BA3083");

                entity.HasOne(d => d.SectionGroup)
                    .WithMany(p => p.Sections)
                    .HasForeignKey(d => d.SectionGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Section__Section__70DDC3D8");
            });

            modelBuilder.Entity<SectionGroup>(entity =>
            {
                entity.ToTable("SectionGroup");

                entity.Property(e => e.SectionGroupName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.SectionGroups)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SectionGr__Branc__6E01572D");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.SectionGroups)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SectionGr__Busin__6D0D32F4");
            });

            modelBuilder.Entity<SystemUser>(entity =>
            {
                entity.ToTable("SystemUser");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.PasswordSalt)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<SystemUserClaim>(entity =>
            {
                entity.ToTable("SystemUserClaim");

                entity.HasOne(d => d.OperationClaim)
                    .WithMany(p => p.SystemUserClaims)
                    .HasForeignKey(d => d.OperationClaimId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SystemUse__Opera__29572725");

                entity.HasOne(d => d.SystemUser)
                    .WithMany(p => p.SystemUserClaims)
                    .HasForeignKey(d => d.SystemUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SystemUse__Syste__286302EC");
            });

            modelBuilder.Entity<Tenant>(entity =>
            {
                entity.ToTable("Tenant");

                entity.Property(e => e.AvatarUrl)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.DateOfBirth).HasColumnType("date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.NameSurname)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Notes)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TaxOffice).HasMaxLength(100);

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Tenants)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tenant__AccountI__6A30C649");

                entity.HasOne(d => d.Branch)
                    .WithMany(p => p.Tenants)
                    .HasForeignKey(d => d.BranchId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tenant__BranchId__693CA210");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.Tenants)
                    .HasForeignKey(d => d.BusinessId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Tenant__Business__68487DD7");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
