using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Tenant.Migrations
{
    public partial class databasedata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Multitenants",
                columns: table => new
                {
                    TenantId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedBy = table.Column<int>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    UpdatedBy = table.Column<int>(nullable: true),
                    UpdatedDate = table.Column<DateTime>(nullable: true),
                    EffectiveFrom = table.Column<DateTime>(nullable: true),
                    EffectiveTo = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    Status = table.Column<int>(nullable: true),
                    Attribute1 = table.Column<string>(nullable: true),
                    Attribute2 = table.Column<string>(nullable: true),
                    Attribute3 = table.Column<string>(nullable: true),
                    Attribute4 = table.Column<string>(nullable: true),
                    Attribute5 = table.Column<string>(nullable: true),
                    TenantUrl = table.Column<string>(maxLength: 100, nullable: true),
                    TenantConnectionString = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Multitenants", x => x.TenantId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Multitenants");
        }
    }
}
