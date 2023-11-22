﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Models;

public partial class SigmaproIisContext : DbContext
{
    public SigmaproIisContext()
    {
    }

    public SigmaproIisContext(DbContextOptions<SigmaproIisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<BusinessConfiguration> BusinessConfigurations { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<County> Counties { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Juridiction> Juridictions { get; set; }

    public virtual DbSet<LovMaster> LovMasters { get; set; }

    public virtual DbSet<Organization> Organizations { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<TestAble> TestAbles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserType> UserTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=sigmaprodb.postgres.database.azure.com,5432;Database=sigmapro_iis;Username=sigmaprodb_user;Password=Rules@23$$11;TrustServerCertificate=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Addresses_pkey");

            entity.HasIndex(e => e.CountyId, "fki_fk_countyids");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AddressId)
                .HasColumnType("character varying")
                .HasColumnName("address_id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountyId).HasColumnName("county_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Line1)
                .HasColumnType("character varying")
                .HasColumnName("line1");
            entity.Property(e => e.Line2)
                .HasColumnType("character varying")
                .HasColumnName("line2");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.Suite)
                .HasColumnType("character varying")
                .HasColumnName("suite");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("fk_countryids");

            entity.HasOne(d => d.County).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.CountyId)
                .HasConstraintName("fk_countyids");

            entity.HasOne(d => d.State).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateids");
        });

        modelBuilder.Entity<BusinessConfiguration>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Business_Configuration_pkey");

            entity.ToTable("Business_Configuration");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BusinessId)
                .HasColumnType("character varying")
                .HasColumnName("Business_id");
            entity.Property(e => e.BusinessName)
                .HasColumnType("character varying")
                .HasColumnName("Business name");
            entity.Property(e => e.EmailId)
                .HasColumnType("character varying")
                .HasColumnName("email id");
            entity.Property(e => e.FaciltyIdStart)
                .HasColumnType("character varying")
                .HasColumnName("facilty_id_start");
            entity.Property(e => e.FaciltyIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("facilty_id_suffix");
            entity.Property(e => e.JurisdictionIdStart)
                .HasColumnType("character varying")
                .HasColumnName("Jurisdiction_id_start");
            entity.Property(e => e.JusrisidictionIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("Jusrisidiction_id_suffix");
            entity.Property(e => e.OrganizationIdStart)
                .HasColumnType("character varying")
                .HasColumnName("organization_id_start");
            entity.Property(e => e.OrganizationIdSuffix)
                .HasColumnType("character varying")
                .HasColumnName("organization_id_suffix");
            entity.Property(e => e.Password)
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Cities_pkey");

            entity.HasIndex(e => e.CountyId, "fki_fk_countyid");

            entity.HasIndex(e => e.StateId, "fki_fk_stateid");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CityId).HasColumnName("city_id");
            entity.Property(e => e.CityName)
                .HasColumnType("character varying")
                .HasColumnName("city_name");
            entity.Property(e => e.CountyId).HasColumnName("county_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.Zipcode).HasColumnName("zipcode");

            entity.HasOne(d => d.County).WithMany(p => p.Cities)
                .HasForeignKey(d => d.CountyId)
                .HasConstraintName("fk_countyid");

            entity.HasOne(d => d.State).WithMany(p => p.Cities)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateid");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Contacts_pkey");

            entity.HasIndex(e => e.ContactTypeId, "fki_FK_contact_type_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ContactId)
                .HasColumnType("character varying")
                .HasColumnName("contact_id");
            entity.Property(e => e.ContactTypeId).HasColumnName("contact_type_id");
            entity.Property(e => e.ContactValue)
                .HasColumnType("character varying")
                .HasColumnName("contact_value");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.ContactType).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.ContactTypeId)
                .HasConstraintName("FK_contact_type_id");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Countries_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CountryName)
                .HasColumnType("character varying")
                .HasColumnName("country_name");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<County>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Counties_pkey");

            entity.HasIndex(e => e.StateId, "fki_fk_stateids");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CountyCode)
                .HasColumnType("character varying")
                .HasColumnName("county_code");
            entity.Property(e => e.CountyId).HasColumnName("county_id");
            entity.Property(e => e.CountyName)
                .HasColumnType("character varying")
                .HasColumnName("county_name");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.UpdatedBy).HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.State).WithMany(p => p.Counties)
                .HasForeignKey(d => d.StateId)
                .HasConstraintName("fk_stateids");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Facilities_pkey");

            entity.HasIndex(e => e.OrganizationsId, "fki_organizations_id");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AdministeredAtLocation)
                .HasColumnType("character varying")
                .HasColumnName("administered_at_location");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.FacilityId)
                .HasColumnType("character varying")
                .HasColumnName("facility_id");
            entity.Property(e => e.FacilityName)
                .HasColumnType("character varying")
                .HasColumnName("facility_name");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.OrganizationsId).HasColumnName("organizations_id");
            entity.Property(e => e.ResponsibleOrganization)
                .HasColumnType("character varying")
                .HasColumnName("responsible_organization");
            entity.Property(e => e.SendingOrganization)
                .HasColumnType("character varying")
                .HasColumnName("sending_organization");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Organizations).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.OrganizationsId)
                .HasConstraintName("organizations_id");
        });

        modelBuilder.Entity<Juridiction>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Juridictions_pkey");

            entity.HasIndex(e => e.BusinessId, "fki_fk_businessids");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BusinessId).HasColumnName("business_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.JuridictionId)
                .HasColumnType("character varying")
                .HasColumnName("juridiction_id");
            entity.Property(e => e.JuridictionName)
                .HasColumnType("character varying")
                .HasColumnName("juridiction_name");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.Zipcode).HasColumnName("zipcode");

            entity.HasOne(d => d.Business).WithMany(p => p.Juridictions)
                .HasForeignKey(d => d.BusinessId)
                .HasConstraintName("fk_businessids");
        });

        modelBuilder.Entity<LovMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("LOV_master_pkey");

            entity.ToTable("LOV_master");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.Key)
                .HasColumnType("character varying")
                .HasColumnName("key");
            entity.Property(e => e.LovType)
                .HasColumnType("character varying")
                .HasColumnName("LOV_type");
            entity.Property(e => e.ReferenceId)
                .HasColumnType("character varying")
                .HasColumnName("reference_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.Value)
                .HasColumnType("character varying")
                .HasColumnName("value");
        });

        modelBuilder.Entity<Organization>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Organizations_pkey");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.JuridictionId)
                .HasColumnType("character varying")
                .HasColumnName("juridiction_id");
            entity.Property(e => e.OrganizationName)
                .HasColumnType("character varying")
                .HasColumnName("organization_name");
            entity.Property(e => e.OrganizationsId)
                .HasColumnType("character varying")
                .HasColumnName("organizations_id");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("States_pkey");

            entity.HasIndex(e => e.CountryId, "fki_fk_countryids");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.StateCode)
                .HasColumnType("character varying")
                .HasColumnName("state_code");
            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.StateName)
                .HasColumnType("character varying")
                .HasColumnName("state_name");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");

            entity.HasOne(d => d.Country).WithMany(p => p.States)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("fk_countryids");
        });

        modelBuilder.Entity<TestAble>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("test_able_pkey");

            entity.ToTable("test_able");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Firstname)
                .HasColumnType("character varying")
                .HasColumnName("firstname");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Users_pkey");

            entity.HasIndex(e => e.UserTypeId, "fki_fk_user_type_id");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Designation)
                .HasColumnType("character varying")
                .HasColumnName("designation");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UserFirstname)
                .HasColumnType("character varying")
                .HasColumnName("user_firstname");
            entity.Property(e => e.UserLasttname)
                .HasColumnType("character varying")
                .HasColumnName("user_lasttname");
            entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");
            entity.Property(e => e.UsersId).HasColumnName("users_id");

            entity.HasOne(d => d.UserType).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserTypeId)
                .HasConstraintName("fk_user_type_id");
        });

        modelBuilder.Entity<UserType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_Types_pkey");

            entity.ToTable("User_Types");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasColumnType("character varying")
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate).HasColumnName("created_date");
            entity.Property(e => e.Isdelete).HasColumnName("isdelete");
            entity.Property(e => e.UpdatedBy)
                .HasColumnType("character varying")
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedDate).HasColumnName("updated_date");
            entity.Property(e => e.UserTypeId)
                .HasColumnType("character varying")
                .HasColumnName("user_type_id");
            entity.Property(e => e.UserTypesName)
                .HasColumnType("character varying")
                .HasColumnName("user_types_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
