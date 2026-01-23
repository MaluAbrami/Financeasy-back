using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financeasy.Infra.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bank_account",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    bank = table.Column<string>(type: "longtext", nullable: false),
                    balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bank_account", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "card_invoice",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    card_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    closing_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    due_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    is_paid = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_invoice", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    type = table.Column<string>(type: "longtext", nullable: false),
                    recurrence_type = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_category", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    email = table.Column<string>(type: "longtext", nullable: false),
                    password = table.Column<string>(type: "longtext", nullable: false),
                    profile_photo = table.Column<string>(type: "longtext", nullable: true),
                    alert_limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "card",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    bank_account_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    category_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false),
                    credit_limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    available_limit = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    closing_day = table.Column<int>(type: "int", nullable: false),
                    due_day = table.Column<int>(type: "int", nullable: false),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted_at = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card", x => x.id);
                    table.ForeignKey(
                        name: "FK_card_bank_account_bank_account_id",
                        column: x => x.bank_account_id,
                        principalTable: "bank_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "transaction",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    payment_method = table.Column<string>(type: "longtext", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    bank_account_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    category_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_transaction", x => x.id);
                    table.ForeignKey(
                        name: "FK_transaction_bank_account_bank_account_id",
                        column: x => x.bank_account_id,
                        principalTable: "bank_account",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_transaction_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "card_purchase",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    user_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    card_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    category_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    total_amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    installments = table.Column<int>(type: "int", nullable: false),
                    purchase_date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    description = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_purchase", x => x.id);
                    table.ForeignKey(
                        name: "FK_card_purchase_card_card_id",
                        column: x => x.card_id,
                        principalTable: "card",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_card_purchase_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "card_installment",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "char(36)", nullable: false),
                    card_purchase_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    card_invoice_id = table.Column<Guid>(type: "char(36)", nullable: false),
                    category_name = table.Column<string>(type: "longtext", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    total_installments = table.Column<int>(type: "int", nullable: false),
                    amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    paid = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_card_installment", x => x.id);
                    table.ForeignKey(
                        name: "FK_card_installment_card_invoice_card_invoice_id",
                        column: x => x.card_invoice_id,
                        principalTable: "card_invoice",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_card_installment_card_purchase_card_purchase_id",
                        column: x => x.card_purchase_id,
                        principalTable: "card_purchase",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_card_bank_account_id_name",
                table: "card",
                columns: new[] { "bank_account_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_card_installment_card_invoice_id",
                table: "card_installment",
                column: "card_invoice_id");

            migrationBuilder.CreateIndex(
                name: "IX_card_installment_card_purchase_id",
                table: "card_installment",
                column: "card_purchase_id");

            migrationBuilder.CreateIndex(
                name: "IX_card_purchase_card_id",
                table: "card_purchase",
                column: "card_id");

            migrationBuilder.CreateIndex(
                name: "IX_card_purchase_category_id",
                table: "card_purchase",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_category_user_id_name",
                table: "category",
                columns: new[] { "user_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_transaction_bank_account_id",
                table: "transaction",
                column: "bank_account_id");

            migrationBuilder.CreateIndex(
                name: "IX_transaction_category_id",
                table: "transaction",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "card_installment");

            migrationBuilder.DropTable(
                name: "transaction");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "card_invoice");

            migrationBuilder.DropTable(
                name: "card_purchase");

            migrationBuilder.DropTable(
                name: "card");

            migrationBuilder.DropTable(
                name: "category");

            migrationBuilder.DropTable(
                name: "bank_account");
        }
    }
}
