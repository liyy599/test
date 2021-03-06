﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ZC.Customer.Repository;

namespace ZC.Customer.Repository.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20200313074519_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ZC.Customer.Model.Models.CompanyInfo", b =>
                {
                    b.Property<long>("Id");

                    b.Property<int>("AccountPeriod");

                    b.Property<string>("AffiliatedCompany")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("AlterationCompany")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("Area");

                    b.Property<int>("ColdChain");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("CreatorUserName")
                        .HasMaxLength(50);

                    b.Property<long>("CustomerLevel");

                    b.Property<long>("CustomerType");

                    b.Property<long>("DirectorId");

                    b.Property<long>("DirectorName");

                    b.Property<int>("Express");

                    b.Property<string>("ExtraPacking")
                        .IsRequired();

                    b.Property<int>("Invoice");

                    b.Property<bool>("IsDeleted");

                    b.Property<bool>("IsSeal");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LastModifierUserName")
                        .HasMaxLength(50);

                    b.Property<int>("LinkNum");

                    b.Property<long>("Province");

                    b.Property<int>("QT");

                    b.Property<string>("Remark")
                        .IsRequired()
                        .HasMaxLength(200);

                    b.Property<long>("SalesmanId");

                    b.Property<long>("SalesmanName");

                    b.Property<string>("SpecialProduct")
                        .IsRequired();

                    b.Property<int>("Template");

                    b.HasKey("Id");

                    b.ToTable("CompanyInfo");
                });

            modelBuilder.Entity("ZC.Customer.Model.Models.ProductInfo", b =>
                {
                    b.Property<long>("Id");

                    b.Property<string>("Abbreviation")
                        .HasMaxLength(50);

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreationTime");

                    b.Property<long?>("CreatorUserId");

                    b.Property<string>("CreatorUserName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("LastModificationTime");

                    b.Property<long?>("LastModifierUserId");

                    b.Property<string>("LastModifierUserName")
                        .HasMaxLength(50);

                    b.Property<string>("Milliliter")
                        .HasMaxLength(50);

                    b.Property<string>("Model")
                        .HasMaxLength(50);

                    b.Property<string>("Pack")
                        .HasMaxLength(50);

                    b.Property<decimal>("Price");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("RegisterNo")
                        .HasMaxLength(50);

                    b.Property<string>("Rise");

                    b.Property<string>("Specs")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("ProductInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
