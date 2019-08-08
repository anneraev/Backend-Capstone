using Microsoft.EntityFrameworkCore.Migrations;

namespace PluginSleuth.Migrations
{
    public partial class Data5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "9a5d9e9c-1517-4b88-8946-565479abe651", "AQAAAAEAACcQAAAAEGyhiMNEIpT5pz3cvG7rJfR1Gs6X2cM2/dXT5DJIeJ4j6/9aZyffhS7eLfV6ugAaDg==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "3405df69-9aa3-4aed-a0b0-4cc2b4bce103", "AQAAAAEAACcQAAAAEMGg+PtyVfGJ5lWUwulCKea04/1JMoAw5H2qiCbzSIlJUKNUH2loE1CWCBIKXPqZ2A==" });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "EngineId",
                keyValue: 1,
                columns: new[] { "BannerPath", "Link" },
                values: new object[] { "https://d289qh4hsbjjw7.cloudfront.net/rpgmaker-20130522223546811/files/program-logo-rpg-maker-mv.png", "http://www.rpgmakerweb.com/products/programs/rpg-maker-mv/" });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "EngineId",
                keyValue: 2,
                column: "BannerPath",
                value: "https://proxy.duckduckgo.com/iu/?u=http%3A%2F%2Fcdn03.androidauthority.net%2Fwp-content%2Fuploads%2F2015%2F09%2FGameMaker-Studio-Logo.jpg&f=1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                table: "Engines",
                keyColumn: "EngineId",
                keyValue: 1,
                columns: new[] { "BannerPath", "Link" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "EngineId",
                keyValue: 2,
                column: "BannerPath",
                value: null);
        }
    }
}
