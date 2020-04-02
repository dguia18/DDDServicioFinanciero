using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimientoFinanciero_CuentaBancaria_CuentaBancariaId",
                table: "MovimientoFinanciero");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CuentaBancaria",
                table: "CuentaBancaria");

            migrationBuilder.RenameTable(
                name: "CuentaBancaria",
                newName: "ServicioFinanciero");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorRetiro",
                table: "MovimientoFinanciero",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "ValorConsignacion",
                table: "MovimientoFinanciero",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<decimal>(
                name: "Saldo",
                table: "ServicioFinanciero",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AddColumn<int>(
                name: "DiasDeTermino",
                table: "ServicioFinanciero",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "TieneConsignacion",
                table: "ServicioFinanciero",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "CupoDeSobregiro",
                table: "ServicioFinanciero",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "tieneConsignaciones",
                table: "ServicioFinanciero",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ciudad",
                table: "ServicioFinanciero",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServicioFinanciero",
                table: "ServicioFinanciero",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientoFinanciero_ServicioFinanciero_CuentaBancariaId",
                table: "MovimientoFinanciero",
                column: "CuentaBancariaId",
                principalTable: "ServicioFinanciero",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimientoFinanciero_ServicioFinanciero_CuentaBancariaId",
                table: "MovimientoFinanciero");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServicioFinanciero",
                table: "ServicioFinanciero");

            migrationBuilder.DropColumn(
                name: "DiasDeTermino",
                table: "ServicioFinanciero");

            migrationBuilder.DropColumn(
                name: "TieneConsignacion",
                table: "ServicioFinanciero");

            migrationBuilder.DropColumn(
                name: "CupoDeSobregiro",
                table: "ServicioFinanciero");

            migrationBuilder.DropColumn(
                name: "tieneConsignaciones",
                table: "ServicioFinanciero");

            migrationBuilder.DropColumn(
                name: "Ciudad",
                table: "ServicioFinanciero");

            migrationBuilder.RenameTable(
                name: "ServicioFinanciero",
                newName: "CuentaBancaria");

            migrationBuilder.AlterColumn<double>(
                name: "ValorRetiro",
                table: "MovimientoFinanciero",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "ValorConsignacion",
                table: "MovimientoFinanciero",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AlterColumn<double>(
                name: "Saldo",
                table: "CuentaBancaria",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CuentaBancaria",
                table: "CuentaBancaria",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimientoFinanciero_CuentaBancaria_CuentaBancariaId",
                table: "MovimientoFinanciero",
                column: "CuentaBancariaId",
                principalTable: "CuentaBancaria",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
