using Microsoft.EntityFrameworkCore.Migrations;

namespace PluginSleuth.Migrations
{
    public partial class Data4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Versions",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 255);

            migrationBuilder.AddColumn<bool>(
                name: "Hidden",
                table: "UserVersions",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Keywords",
                table: "Plugins",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d04b3e3c-b81b-4de9-b742-45e425f6e1cd", "AQAAAAEAACcQAAAAELmviWxtlclFXfyRvZoFuW01Nqe5Aw1jjhO3iaa33i6rvU0i4lQEVEAWOqSeuld/GA==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "179394af-cefd-4bd4-86c1-a7c4e9cf7184", "AQAAAAEAACcQAAAAECqrwSjGwDb7H9kWry86/nh83qu4+fU3gqqKrNHFrlisqLcxD4l4aqzy9XM0b17+Aw==" });

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 1,
                column: "Keywords",
                value: "");

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 2,
                column: "Keywords",
                value: "");

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 3,
                column: "Keywords",
                value: "");

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 4,
                column: "Keywords",
                value: "");

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 5,
                column: "Keywords",
                value: "");

            migrationBuilder.UpdateData(
                table: "Plugins",
                keyColumn: "PluginId",
                keyValue: 6,
                column: "Keywords",
                value: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Hidden",
                table: "UserVersions");

            migrationBuilder.DropColumn(
                name: "Keywords",
                table: "Plugins");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Versions",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 50);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "15400c7a-2e73-4efa-97f6-f954bdc9ac7e", "AQAAAAEAACcQAAAAEEb7HplSm2YDRnMpfSJ0SMwDH4Vqa/vaGSiZ7UFZFnJgIUIXp23Qymcl/47Ewjda8w==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "7438404d-56e4-43e7-997a-ee9e05c2024b", "AQAAAAEAACcQAAAAEK33r3zy68yw8A3m3tZvpYP0Fb3yq9YFE2MSTRlVFMkDVpVs6AcP+WkO2P2Sfz7Ikg==" });
        }
    }
}
