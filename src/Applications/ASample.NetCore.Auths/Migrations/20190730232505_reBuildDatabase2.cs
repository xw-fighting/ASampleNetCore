﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ASample.NetCore.Auths.Migrations
{
    public partial class reBuildDatabase2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TGroup",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    GroupName = table.Column<string>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TGroupRoleRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TGroupRoleRelation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    OpType = table.Column<int>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    OpUser = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TLog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TOrganization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    OrgName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOrganization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TOrganizationRoleRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    OrgId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TOrganizationRoleRelation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TRight",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    RightName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRight", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TRole",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    ParentId = table.Column<Guid>(nullable: false),
                    RoleName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TRoleRightRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false),
                    RightId = table.Column<Guid>(nullable: false),
                    RightType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TRoleRightRelation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUser",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleteTime = table.Column<DateTime>(nullable: true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ModifyTime = table.Column<DateTime>(nullable: true),
                    OrgId = table.Column<Guid>(nullable: false),
                    LoginName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    LoginTime = table.Column<DateTime>(nullable: false),
                    LastLoginTime = table.Column<DateTime>(nullable: false),
                    Count = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUserGroupRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    GroupId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserGroupRelation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TUserRoleRelation",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TUserRoleRelation", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TGroup");

            migrationBuilder.DropTable(
                name: "TGroupRoleRelation");

            migrationBuilder.DropTable(
                name: "TLog");

            migrationBuilder.DropTable(
                name: "TOrganization");

            migrationBuilder.DropTable(
                name: "TOrganizationRoleRelation");

            migrationBuilder.DropTable(
                name: "TRight");

            migrationBuilder.DropTable(
                name: "TRole");

            migrationBuilder.DropTable(
                name: "TRoleRightRelation");

            migrationBuilder.DropTable(
                name: "TUser");

            migrationBuilder.DropTable(
                name: "TUserGroupRelation");

            migrationBuilder.DropTable(
                name: "TUserRoleRelation");
        }
    }
}
