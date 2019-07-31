﻿// <auto-generated />
using System;
using ASample.NetCore.Auths.DbConexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ASample.NetCore.Auths.Migrations
{
    [DbContext(typeof(ASampleIdentityDbContext))]
    partial class ASampleIdentityDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.ASampleUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
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

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TGroup", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<string>("GroupName")
                        .HasMaxLength(50);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<Guid>("ParentId");

                    b.HasKey("Id");

                    b.ToTable("TGroup");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TGroupRoleRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GroupId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("TGroupRoleRelation");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TLog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<int>("OpType");

                    b.Property<string>("OpUser")
                        .HasMaxLength(500);

                    b.HasKey("Id");

                    b.ToTable("TLog");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TOrganization", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<string>("OrgName")
                        .HasMaxLength(50);

                    b.Property<Guid>("ParentId");

                    b.HasKey("Id");

                    b.ToTable("TOrganization");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TOrganizationRoleRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("OrgId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("TOrganizationRoleRelation");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TRight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<Guid>("ParentId");

                    b.Property<string>("RightName")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("TRight");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<string>("Description")
                        .HasMaxLength(500);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<Guid>("ParentId");

                    b.Property<string>("RoleName")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("TRole");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TRoleRightRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("RightId");

                    b.Property<int>("RightType");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.ToTable("TRoleRightRelation");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<DateTime>("CreateTime");

                    b.Property<DateTime?>("DeleteTime");

                    b.Property<string>("Email")
                        .HasMaxLength(20);

                    b.Property<bool>("IsDeleted");

                    b.Property<DateTime>("LastLoginTime");

                    b.Property<string>("LoginName")
                        .HasMaxLength(20);

                    b.Property<DateTime>("LoginTime");

                    b.Property<DateTime?>("ModifyTime");

                    b.Property<Guid>("OrgId");

                    b.Property<string>("Password");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(12);

                    b.Property<string>("UserName")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("TUser");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TUserGroupRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("GroupId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("TUserGroupRelation");
                });

            modelBuilder.Entity("ASample.NetCore.Auths.Domains.TUserRoleRelation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("RoleId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.ToTable("TUserRoleRelation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
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
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("ASample.NetCore.Auths.Domains.ASampleUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("ASample.NetCore.Auths.Domains.ASampleUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ASample.NetCore.Auths.Domains.ASampleUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("ASample.NetCore.Auths.Domains.ASampleUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
