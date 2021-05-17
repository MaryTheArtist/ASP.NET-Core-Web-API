using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class ProductDataSeed2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "1",
                column: "Weight",
                value: 200.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "2",
                column: "Weight",
                value: 190.0);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductID", "Brand", "Name", "Price", "Quantity", "Weight" },
                values: new object[,]
                {
                    { "3", "Samsung", "Galaxy A5", 600.0, 9, 190.0 },
                    { "4", "Samsung", "Galaxy S7 ", 700.0, 10, 205.0 },
                    { "5", "Samsung", "Note 10", 1000.0, 5, 180.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "3");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "4");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "5");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "1",
                column: "Weight",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "ProductID",
                keyValue: "2",
                column: "Weight",
                value: 0.0);
        }
    }
}
