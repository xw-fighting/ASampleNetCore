﻿// <auto-generated />
using System;
using ASample.NetCore.SqlServerWebSite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASample.NetCore.SqlServerWebSite.Migrations
{
    [DbContext(typeof(ASampleDbContext))]
    partial class ASampleDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASample.NetCore.SqlServerWebSite.Domain.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Age");

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ASample.NetCore.SqlServerWebSite.Domain.UserInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("Birthday");

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("Name");

                    b.Property<string>("Tel");

                    b.HasKey("Id");

                    b.ToTable("UserInfo");
                });
#pragma warning restore 612, 618
        }
    }
}
