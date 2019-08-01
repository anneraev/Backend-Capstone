using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PluginSleuth.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    AvatarPath = table.Column<string>(nullable: true),
                    WebSite = table.Column<string>(nullable: true),
                    Github = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Engines",
                columns: table => new
                {
                    EngineId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Language = table.Column<string>(nullable: true),
                    About = table.Column<string>(maxLength: 255, nullable: false),
                    Link = table.Column<string>(nullable: true),
                    BannerPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engines", x => x.EngineId);
                });

            migrationBuilder.CreateTable(
                name: "PluginTypes",
                columns: table => new
                {
                    PluginTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginTypes", x => x.PluginTypeId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(maxLength: 128, nullable: false),
                    Name = table.Column<string>(maxLength: 128, nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plugins",
                columns: table => new
                {
                    PluginId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    EngineId = table.Column<int>(nullable: false),
                    PluginTypeId = table.Column<int>(nullable: false),
                    CommercialUse = table.Column<int>(nullable: false),
                    Free = table.Column<bool>(nullable: false),
                    Webpage = table.Column<string>(nullable: true),
                    IsListed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plugins", x => x.PluginId);
                    table.ForeignKey(
                        name: "FK_Plugins_Engines_EngineId",
                        column: x => x.EngineId,
                        principalTable: "Engines",
                        principalColumn: "EngineId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plugins_PluginTypes_PluginTypeId",
                        column: x => x.PluginTypeId,
                        principalTable: "PluginTypes",
                        principalColumn: "PluginTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Plugins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Versions",
                columns: table => new
                {
                    VersionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 255, nullable: false),
                    ReadMe = table.Column<string>(nullable: true),
                    PluginId = table.Column<int>(nullable: false),
                    DownloadLink = table.Column<string>(nullable: true),
                    Iteration = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versions", x => x.VersionId);
                    table.ForeignKey(
                        name: "FK_Versions_Plugins_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugins",
                        principalColumn: "PluginId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserVersions",
                columns: table => new
                {
                    UserVersionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<string>(nullable: false),
                    VersionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserVersions", x => x.UserVersionId);
                    table.ForeignKey(
                        name: "FK_UserVersions_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserVersions_Versions_VersionId",
                        column: x => x.VersionId,
                        principalTable: "Versions",
                        principalColumn: "VersionId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarPath", "ConcurrencyStamp", "Email", "EmailConfirmed", "Github", "IsAdmin", "LockoutEnabled", "LockoutEnd", "Name", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "WebSite" },
                values: new object[,]
                {
                    { "00000000-ffff-ffff-ffff-ffffffffffff", 0, null, "f9ff561f-b7f8-4c05-83cc-fbbe93d2578d", "admin@admin.com", true, null, true, false, null, "admin", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEPOtRKmmQDTjufFxhaYOGKmzSvmz7idUBEM4u8sK9veiPPolYRGn5NpI0BJbFamtGA==", null, false, "7f434309-a4d9-48e9-9ebb-8803db794577", false, "admin@admin.com", null },
                    { "10000000-ffff-ffff-ffff-ffffffffffff", 0, null, "8207de12-45b7-40bc-bc17-457612ab2748", "notadmin@notadmin.com", true, null, false, false, null, "notadmin", "NOTADMIN@NOTADMIN.COM", "NOTADMIN@NOTADMIN.COM", "AQAAAAEAACcQAAAAEHZNTNmRfJwYbviBav32WvB9EXBl/P1Q6pgoXkPFXe9WwaJ2nIvu7pGTxEN23ntFcg==", null, false, "8f434309-a4d9-48e9-9ebb-8803db794577", false, "notadmin@notadmin.com", null }
                });

            migrationBuilder.InsertData(
                table: "Engines",
                columns: new[] { "EngineId", "About", "BannerPath", "Language", "Link", "Title" },
                values: new object[,]
                {
                    { 1, "With tools for managing database, a javascript code base, and the ability to deploy to Mac, PC, and Mobile! With RPG Maker MV, almost anyone (even a child) can make a game!", null, null, null, "RPG Maker MV" },
                    { 2, "Game Maker makes games!", null, null, null, "Game Maker" }
                });

            migrationBuilder.InsertData(
                table: "PluginTypes",
                columns: new[] { "PluginTypeId", "Name" },
                values: new object[,]
                {
                    { 1, "Battle System" },
                    { 2, "Menu" }
                });

            migrationBuilder.InsertData(
                table: "Plugins",
                columns: new[] { "PluginId", "CommercialUse", "EngineId", "Free", "IsListed", "PluginTypeId", "Title", "UserId", "Webpage" },
                values: new object[,]
                {
                    { 1, 0, 1, false, true, 1, "YEP Battle System - CTB", "00000000-ffff-ffff-ffff-ffffffffffff", null },
                    { 4, 0, 1, false, true, 1, "Mog Area Target", "10000000-ffff-ffff-ffff-ffffffffffff", null },
                    { 2, 0, 1, false, true, 2, "Mog Status Menu", "10000000-ffff-ffff-ffff-ffffffffffff", null },
                    { 3, 0, 1, false, true, 2, "Mog Save Menu", "10000000-ffff-ffff-ffff-ffffffffffff", null },
                    { 5, 0, 2, false, true, 2, "Game Maker Start Menu", "00000000-ffff-ffff-ffff-ffffffffffff", null },
                    { 6, 0, 2, false, false, 2, "Anne's Unlisted Plugin", "10000000-ffff-ffff-ffff-ffffffffffff", null }
                });

            migrationBuilder.InsertData(
                table: "Versions",
                columns: new[] { "VersionId", "DownloadLink", "Iteration", "Name", "PluginId", "ReadMe" },
                values: new object[,]
                {
                    { 1, null, 1, "1.1", 1, null },
                    { 2, null, 2, "1.55", 1, null },
                    { 6, null, 1, "42", 4, null },
                    { 3, null, 1, "0.2Beta", 2, null },
                    { 4, null, 1, "Beta", 3, null },
                    { 5, null, 2, "Final", 3, null },
                    { 7, null, 0, "Unlisted", 5, null }
                });

            migrationBuilder.InsertData(
                table: "UserVersions",
                columns: new[] { "UserVersionId", "UserId", "VersionId" },
                values: new object[,]
                {
                    { 1, "00000000-ffff-ffff-ffff-ffffffffffff", 1 },
                    { 2, "00000000-ffff-ffff-ffff-ffffffffffff", 2 },
                    { 3, "10000000-ffff-ffff-ffff-ffffffffffff", 4 },
                    { 4, "00000000-ffff-ffff-ffff-ffffffffffff", 5 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Plugins_EngineId",
                table: "Plugins",
                column: "EngineId");

            migrationBuilder.CreateIndex(
                name: "IX_Plugins_PluginTypeId",
                table: "Plugins",
                column: "PluginTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plugins_UserId",
                table: "Plugins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVersions_UserId",
                table: "UserVersions",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserVersions_VersionId",
                table: "UserVersions",
                column: "VersionId");

            migrationBuilder.CreateIndex(
                name: "IX_Versions_PluginId",
                table: "Versions",
                column: "PluginId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "UserVersions");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Versions");

            migrationBuilder.DropTable(
                name: "Plugins");

            migrationBuilder.DropTable(
                name: "Engines");

            migrationBuilder.DropTable(
                name: "PluginTypes");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
