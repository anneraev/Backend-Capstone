using Microsoft.EntityFrameworkCore.Migrations;

namespace PluginSleuth.Migrations
{
    public partial class Data2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "About",
                table: "Plugins",
                maxLength: 255,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "c7f0fb1c-5e93-4f2d-91a1-e2d94bfef9e2", "AQAAAAEAACcQAAAAEDuPIwRoRjRoLTMoVkPhaHAhOAJYmnyKxtUzbbIQGfV2xEVkenMl1tqYCU8jZpp9jQ==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "b6311601-0257-4217-9beb-a3e1105beedd", "AQAAAAEAACcQAAAAEFyKALdkrnW6ToY8WgkN6OM3xwCXRrtMRtgL/cgkh8S8ceAc898KA3T2KDXGfyJArQ==" });

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 1,
                columns: new[] { "About", "CommercialUse" },
                values: new object[] { "Controlled Turn Battle features turn-based battles where battlers act as soon as they have a command and actions affect turn order", 3 });

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 2,
                columns: new[] { "About", "CommercialUse" },
                values: new object[] { "Modifiable menu plugin for flexible UI development.", 2 });

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 3,
                columns: new[] { "About", "CommercialUse" },
                values: new object[] { "Modifiable menu plugin for flexible UI development.", 1 });

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 4,
                columns: new[] { "About", "CommercialUse" },
                values: new object[] { "Allows the developer to set up skills with an area-of-effect target.", 1 });

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 5,
                column: "About",
                value: "Start menu plugin for Game Maker");

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 6,
                column: "About",
                value: "This plugin is meant to be unlisted, and can only be viewed by its author or an admin.");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "About",
                table: "Plugins");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f9ff561f-b7f8-4c05-83cc-fbbe93d2578d", "AQAAAAEAACcQAAAAEPOtRKmmQDTjufFxhaYOGKmzSvmz7idUBEM4u8sK9veiPPolYRGn5NpI0BJbFamtGA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "8207de12-45b7-40bc-bc17-457612ab2748", "AQAAAAEAACcQAAAAEHZNTNmRfJwYbviBav32WvB9EXBl/P1Q6pgoXkPFXe9WwaJ2nIvu7pGTxEN23ntFcg==" });

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 1,
                column: "CommercialUse",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 2,
                column: "CommercialUse",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 3,
                column: "CommercialUse",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 4,
                column: "CommercialUse",
                value: 0);
        }
    }
}
