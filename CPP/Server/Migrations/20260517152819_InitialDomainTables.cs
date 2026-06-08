using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CP.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialDomainTables : Migration
    {
        /// <inheritdoc />
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            // Создание таблиц для доменных сущностей
            // Создание таблицы для водителей
            migrationBuilder.CreateTable(
                name: "drivers",
                schema: "public",
                columns: table => new
                {
                    driver_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    license_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    license_issue_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    license_expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    vehicle_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers", x => x.driver_id);
                    table.ForeignKey(
                        name: "FK_drivers_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            // Создание таблицы для транспортных средств
            migrationBuilder.CreateTable(
                name: "vehicles",
                schema: "public",
                columns: table => new
                {
                    vehicle_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    license_plate = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    vin = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    vehicle_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    fuel_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    mileage = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: false, defaultValue: 0m),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValue: "Available"),
                    insurance = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    InsuranceExpiryDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    driver_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.vehicle_id);
                    table.ForeignKey(
                        name: "FK_vehicles_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "public",
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.SetNull);
                });

            // Создание таблицы для заявок на поездку
            migrationBuilder.CreateTable(
                name: "trip_requests",
                schema: "public",
                columns: table => new
                {
                    request_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<string>(type: "text", nullable: true),
                    vehicle_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    trip_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    vehicle_id = table.Column<int>(type: "integer", nullable: true),
                    driver_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValue: "Pending"),
                    rejection_reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_requests", x => x.request_id);
                    table.ForeignKey(
                        name: "FK_trip_requests_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trip_requests_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "public",
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_trip_requests_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "public",
                        principalTable: "vehicles",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.SetNull);
                });

            // Создание таблицы для поездок
            migrationBuilder.CreateTable(
                name: "trips",
                schema: "public",
                columns: table => new
                {
                    trip_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    request_id = table.Column<int>(type: "integer", nullable: false),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    trip_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    start_odometer = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
                    end_odometer = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValue: "Scheduled")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.trip_id);
                    table.ForeignKey(
                        name: "FK_trips_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "public",
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_trips_trip_requests_request_id",
                        column: x => x.request_id,
                        principalSchema: "public",
                        principalTable: "trip_requests",
                        principalColumn: "request_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trips_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "public",
                        principalTable: "vehicles",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Restrict);
                });

            // Индексы для доменных таблиц
            migrationBuilder.CreateIndex(
                name: "IX_drivers_user_id",
                schema: "public",
                table: "drivers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_drivers_vehicle_id",
                schema: "public",
                table: "drivers",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_trip_requests_driver_id",
                schema: "public",
                table: "trip_requests",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_trip_requests_user_id",
                schema: "public",
                table: "trip_requests",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_trip_requests_vehicle_id",
                schema: "public",
                table: "trip_requests",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_driver_id",
                schema: "public",
                table: "trips",
                column: "driver_id");

            migrationBuilder.CreateIndex(
                name: "IX_trips_request_id",
                schema: "public",
                table: "trips",
                column: "request_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_trips_vehicle_id",
                schema: "public",
                table: "trips",
                column: "vehicle_id");

            migrationBuilder.CreateIndex(
                name: "IX_vehicles_driver_id",
                schema: "public",
                table: "vehicles",
                column: "driver_id");

            // Внешний ключ для drivers → vehicles
            migrationBuilder.AddForeignKey(
                name: "FK_drivers_vehicles_vehicle_id",
                schema: "public",
                table: "drivers",
                column: "vehicle_id",
                principalSchema: "public",
                principalTable: "vehicles",
                principalColumn: "vehicle_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        // Удаление таблиц для доменных сущностей
       protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "trips",
                schema: "public");

            migrationBuilder.DropTable(
                name: "trip_requests",
                schema: "public");

            migrationBuilder.DropTable(
                name: "vehicles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "drivers",
                schema: "public");
        }
    }
}
