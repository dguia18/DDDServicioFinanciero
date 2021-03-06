﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServicioFinanciero",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(nullable: true),
                    Numero = table.Column<string>(nullable: true),
                    Saldo = table.Column<double>(nullable: false),
                    Ciudad = table.Column<string>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    DiasDeTermino = table.Column<int>(nullable: true),
                    CupoDeSobregiro = table.Column<double>(nullable: true),
                    CupoPreAprobado = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServicioFinanciero", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimientosFinancieros",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuentaBancariaId = table.Column<int>(nullable: true),
                    ValorRetiro = table.Column<double>(nullable: false),
                    ValorConsignacion = table.Column<double>(nullable: false),
                    FechaMovimiento = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimientosFinancieros", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimientosFinancieros_ServicioFinanciero_CuentaBancariaId",
                        column: x => x.CuentaBancariaId,
                        principalTable: "ServicioFinanciero",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovimientosFinancieros_CuentaBancariaId",
                table: "MovimientosFinancieros",
                column: "CuentaBancariaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovimientosFinancieros");

            migrationBuilder.DropTable(
                name: "ServicioFinanciero");
        }
    }
}
