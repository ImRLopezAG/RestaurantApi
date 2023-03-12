using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Restaurant.Infrastructure.Persistence.Migrations {
  public partial class Initial : Migration {
    protected override void Up(MigrationBuilder migrationBuilder) {
      migrationBuilder.CreateTable(
          name: "Tables",
          columns: table => new {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Capacity = table.Column<int>(type: "int", nullable: false),
            Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
            StatusId = table.Column<int>(type: "int", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Tables", x => x.Id);
          });

      migrationBuilder.CreateTable(
          name: "Orders",
          columns: table => new {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            TableId = table.Column<int>(type: "int", nullable: false),
            StatusId = table.Column<int>(type: "int", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_Orders", x => x.Id);
            table.ForeignKey(
                      name: "FK_Orders_Tables_TableId",
                      column: x => x.TableId,
                      principalTable: "Tables",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "Plates",
          columns: table => new {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            Price = table.Column<double>(type: "float", nullable: false),
            Capacity = table.Column<int>(type: "int", nullable: false),
            CategoryId = table.Column<int>(type: "int", nullable: false),
            OrderId = table.Column<int>(type: "int", nullable: true),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Plates", x => x.Id);
            table.ForeignKey(
                      name: "FK_Plates_Orders_OrderId",
                      column: x => x.OrderId,
                      principalTable: "Orders",
                      principalColumn: "Id");
          });

      migrationBuilder.CreateTable(
          name: "Ingredients",
          columns: table => new {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            PlateId = table.Column<int>(type: "int", nullable: true),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
            Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
          },
          constraints: table => {
            table.PrimaryKey("PK_Ingredients", x => x.Id);
            table.ForeignKey(
                      name: "FK_Ingredients_Plates_PlateId",
                      column: x => x.PlateId,
                      principalTable: "Plates",
                      principalColumn: "Id");
          });

      migrationBuilder.CreateTable(
          name: "OrderPlates",
          columns: table => new {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            PlateId = table.Column<int>(type: "int", nullable: false),
            OrderId = table.Column<int>(type: "int", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_OrderPlates", x => x.Id);
            table.ForeignKey(
                      name: "FK_OrderPlates_Orders_OrderId",
                      column: x => x.OrderId,
                      principalTable: "Orders",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_OrderPlates_Plates_PlateId",
                      column: x => x.PlateId,
                      principalTable: "Plates",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateTable(
          name: "PlateIngredients",
          columns: table => new {
            Id = table.Column<int>(type: "int", nullable: false)
                  .Annotation("SqlServer:Identity", "1, 1"),
            PlateId = table.Column<int>(type: "int", nullable: false),
            IngredientId = table.Column<int>(type: "int", nullable: false),
            CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
            LastModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
          },
          constraints: table => {
            table.PrimaryKey("PK_PlateIngredients", x => x.Id);
            table.ForeignKey(
                      name: "FK_PlateIngredients_Ingredients_IngredientId",
                      column: x => x.IngredientId,
                      principalTable: "Ingredients",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
            table.ForeignKey(
                      name: "FK_PlateIngredients_Plates_PlateId",
                      column: x => x.PlateId,
                      principalTable: "Plates",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Cascade);
          });

      migrationBuilder.CreateIndex(
          name: "IX_Ingredients_PlateId",
          table: "Ingredients",
          column: "PlateId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderPlates_OrderId",
          table: "OrderPlates",
          column: "OrderId");

      migrationBuilder.CreateIndex(
          name: "IX_OrderPlates_PlateId",
          table: "OrderPlates",
          column: "PlateId");

      migrationBuilder.CreateIndex(
          name: "IX_Orders_TableId",
          table: "Orders",
          column: "TableId");

      migrationBuilder.CreateIndex(
          name: "IX_PlateIngredients_IngredientId",
          table: "PlateIngredients",
          column: "IngredientId");

      migrationBuilder.CreateIndex(
          name: "IX_PlateIngredients_PlateId",
          table: "PlateIngredients",
          column: "PlateId");

      migrationBuilder.CreateIndex(
          name: "IX_Plates_OrderId",
          table: "Plates",
          column: "OrderId");
    }

    protected override void Down(MigrationBuilder migrationBuilder) {
      migrationBuilder.DropTable(
          name: "OrderPlates");

      migrationBuilder.DropTable(
          name: "PlateIngredients");

      migrationBuilder.DropTable(
          name: "Ingredients");

      migrationBuilder.DropTable(
          name: "Plates");

      migrationBuilder.DropTable(
          name: "Orders");

      migrationBuilder.DropTable(
          name: "Tables");
    }
  }
}
