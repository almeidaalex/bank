using Microsoft.EntityFrameworkCore.Migrations;

namespace Bank.Infra.Migrations
{
  public partial class Initial_Seed : Migration
  {
    protected override void Up(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData("Owner",
         new[] { "Id", "Title" },
         new object[] { 1, "Alex A." });

      migrationBuilder.InsertData("Owner",
          new[] { "Id", "Title" },
          new object[] { 2, "Grace N." });

      migrationBuilder.InsertData("Accounts",
          new[] { "No", "OwnerId", "Balance" },
          new object[] { 1001, 1, 10000 });

      migrationBuilder.InsertData("Accounts",
          new[] { "No", "OwnerId", "Balance" },
          new object[] { 1002, 2, 400 });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
      migrationBuilder.DeleteData("Owner",
          new[] { "Id", "Title" },
          new object[] { 1, "Alex A." });

      migrationBuilder.DeleteData("Owner",
          new[] { "Id", "Title" },
          new object[] { 2, "Grace N." });

    }
  }
}