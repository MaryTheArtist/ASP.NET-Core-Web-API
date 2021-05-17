using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ProductDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Brand", "Name", "Price", "Quantity" },
                values: new object[] { "1", "Samsung", "Galaxy A70", 800.0, 5 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Brand", "Name", "Price", "Quantity" },
                values: new object[] { "2", "Samsung", "Galaxy A50", 600.0, 10 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "2");
        }
    }
}
