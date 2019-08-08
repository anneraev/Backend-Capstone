using Microsoft.EntityFrameworkCore.Migrations;

namespace PluginSleuth.Migrations
{
    public partial class Data6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "326ee973-cb8e-4006-8fc1-9f94e4e94b19", "AQAAAAEAACcQAAAAEBwcHQPMA7WhtAroojBkvs6DXkKvFXZZGa1aqsvgWaxAJzIUGZRAoIqXqvGhsji7+g==" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "10000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "f3b9ca3f-d04d-48da-b6db-b68a95125f04", "AQAAAAEAACcQAAAAECidgK134GhAmg6+CmA+O1JqboPX2jOM0cLMt9NBpS5Q6QixL/UUBKyYKExS1+1NQg==" });

            migrationBuilder.UpdateData(
                table: "Engines",
                keyColumn: "EngineId",
                keyValue: 2,
                column: "Link",
                value: "https://www.yoyogames.com/gamemaker");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: 2,
                column: "Link",
                value: null);
        }
    }
}
