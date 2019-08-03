using Microsoft.EntityFrameworkCore.Migrations;

namespace PluginSleuth.Migrations
{
    public partial class Data3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
