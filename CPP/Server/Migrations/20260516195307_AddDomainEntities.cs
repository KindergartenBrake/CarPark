using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CP.Server.Migrations
{
    /// <inheritdoc />
    public partial class AddDomainEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_users_user_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_drivers_vehicles_vehicle_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_requests_drivers_driver_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_requests_users_user_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_requests_vehicles_vehicle_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_vehicles_vehicle_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_drivers_driver_id",
                schema: "public",
                table: "vehicles");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "v_drivers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "v_trip_requests",
                schema: "public");

            migrationBuilder.DropTable(
                name: "v_trips",
                schema: "public");

            migrationBuilder.DropTable(
                name: "v_users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "v_vehicles",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_trips_request_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trip_requests_user_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropColumn(
                name: "user_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "public",
                table: "vehicles",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "Available",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "mileage",
                schema: "public",
                table: "vehicles",
                type: "numeric(12,1)",
                precision: 12,
                scale: 1,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,1)",
                oldPrecision: 12,
                oldScale: 1,
                oldNullable: true,
                oldDefaultValueSql: "0");

            migrationBuilder.AddColumn<DateTime>(
                name: "insurance_expiry_date",
                schema: "public",
                table: "vehicles",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "public",
                table: "trips",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "Scheduled",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValueSql: "'planned'::character varying");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_time",
                schema: "public",
                table: "trips",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<decimal>(
                name: "start_odometer",
                schema: "public",
                table: "trips",
                type: "numeric(12,1)",
                precision: 12,
                scale: 1,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,1)",
                oldPrecision: 12,
                oldScale: 1);

            migrationBuilder.AddColumn<int>(
                name: "driver_id",
                schema: "public",
                table: "trips",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "vehicle_id",
                schema: "public",
                table: "trip_requests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "public",
                table: "trip_requests",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                defaultValue: "Pending",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValueSql: "'pending'::character varying");

            migrationBuilder.AlterColumn<int>(
                name: "driver_id",
                schema: "public",
                table: "trip_requests",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "public",
                table: "trip_requests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true,
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<string>(
                name: "created_by_user_id",
                schema: "public",
                table: "trip_requests",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "end_time",
                schema: "public",
                table: "trip_requests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "rejection_reason",
                schema: "public",
                table: "trip_requests",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "start_time",
                schema: "public",
                table: "trip_requests",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "trip_date",
                schema: "public",
                table: "trip_requests",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "vehicle_type",
                schema: "public",
                table: "trip_requests",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "user_id",
                schema: "public",
                table: "drivers",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                schema: "public",
                table: "drivers",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true,
                oldDefaultValueSql: "true");

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
                name: "IX_trip_requests_created_by_user_id",
                schema: "public",
                table: "trip_requests",
                column: "created_by_user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_AspNetUsers_user_id",
                schema: "public",
                table: "drivers",
                column: "user_id",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_vehicles_vehicle_id",
                schema: "public",
                table: "drivers",
                column: "vehicle_id",
                principalSchema: "public",
                principalTable: "vehicles",
                principalColumn: "vehicle_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_requests_AspNetUsers_created_by_user_id",
                schema: "public",
                table: "trip_requests",
                column: "created_by_user_id",
                principalSchema: "public",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_requests_drivers_driver_id",
                schema: "public",
                table: "trip_requests",
                column: "driver_id",
                principalSchema: "public",
                principalTable: "drivers",
                principalColumn: "driver_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_requests_vehicles_vehicle_id",
                schema: "public",
                table: "trip_requests",
                column: "vehicle_id",
                principalSchema: "public",
                principalTable: "vehicles",
                principalColumn: "vehicle_id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_drivers_driver_id",
                schema: "public",
                table: "trips",
                column: "driver_id",
                principalSchema: "public",
                principalTable: "drivers",
                principalColumn: "driver_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_vehicles_vehicle_id",
                schema: "public",
                table: "trips",
                column: "vehicle_id",
                principalSchema: "public",
                principalTable: "vehicles",
                principalColumn: "vehicle_id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_drivers_driver_id",
                schema: "public",
                table: "vehicles",
                column: "driver_id",
                principalSchema: "public",
                principalTable: "drivers",
                principalColumn: "driver_id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_AspNetUsers_user_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_drivers_vehicles_vehicle_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_requests_AspNetUsers_created_by_user_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_requests_drivers_driver_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_trip_requests_vehicles_vehicle_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_drivers_driver_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_trips_vehicles_vehicle_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropForeignKey(
                name: "FK_vehicles_drivers_driver_id",
                schema: "public",
                table: "vehicles");

            migrationBuilder.DropIndex(
                name: "IX_trips_driver_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trips_request_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropIndex(
                name: "IX_trip_requests_created_by_user_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropColumn(
                name: "insurance_expiry_date",
                schema: "public",
                table: "vehicles");

            migrationBuilder.DropColumn(
                name: "driver_id",
                schema: "public",
                table: "trips");

            migrationBuilder.DropColumn(
                name: "created_by_user_id",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropColumn(
                name: "end_time",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropColumn(
                name: "rejection_reason",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropColumn(
                name: "start_time",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropColumn(
                name: "trip_date",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.DropColumn(
                name: "vehicle_type",
                schema: "public",
                table: "trip_requests");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "public",
                table: "vehicles",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "Available");

            migrationBuilder.AlterColumn<decimal>(
                name: "mileage",
                schema: "public",
                table: "vehicles",
                type: "numeric(12,1)",
                precision: 12,
                scale: 1,
                nullable: true,
                defaultValueSql: "0",
                oldClrType: typeof(decimal),
                oldType: "numeric(12,1)",
                oldPrecision: 12,
                oldScale: 1,
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "public",
                table: "trips",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                defaultValueSql: "'planned'::character varying",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "Scheduled");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_time",
                schema: "public",
                table: "trips",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "start_odometer",
                schema: "public",
                table: "trips",
                type: "numeric(12,1)",
                precision: 12,
                scale: 1,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,1)",
                oldPrecision: 12,
                oldScale: 1,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "vehicle_id",
                schema: "public",
                table: "trip_requests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                schema: "public",
                table: "trip_requests",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true,
                defaultValueSql: "'pending'::character varying",
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20,
                oldNullable: true,
                oldDefaultValue: "Pending");

            migrationBuilder.AlterColumn<int>(
                name: "driver_id",
                schema: "public",
                table: "trip_requests",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "created_at",
                schema: "public",
                table: "trip_requests",
                type: "timestamp with time zone",
                nullable: true,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "user_id",
                schema: "public",
                table: "trip_requests",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "user_id",
                schema: "public",
                table: "drivers",
                type: "integer",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "is_active",
                schema: "public",
                table: "drivers",
                type: "boolean",
                nullable: true,
                defaultValueSql: "true",
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "v_drivers",
                schema: "public",
                columns: table => new
                {
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Дата_выдачи_прав = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Дата_окончания_прав = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Дата_рождения = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Идентификатор_водителя = table.Column<int>(type: "integer", nullable: true),
                    Имя = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Номер_прав = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Отчество = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Состояние = table.Column<bool>(type: "boolean", nullable: true),
                    Телефон = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Фамилия = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "v_trip_requests",
                schema: "public",
                columns: table => new
                {
                    Дата_создания = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Идентификатор_водителя = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_заявки = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_пользователя = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_ТС = table.Column<int>(type: "integer", nullable: true),
                    Описание = table.Column<string>(type: "text", nullable: true),
                    Статус = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "v_trips",
                schema: "public",
                columns: table => new
                {
                    Время_начала = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Время_окончания = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Дата_поездки = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Идентификатор_заявки = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_поездки = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_ТС = table.Column<int>(type: "integer", nullable: true),
                    Пробег_в_конце = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
                    Пробег_в_начале = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
                    Статус = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "v_users",
                schema: "public",
                columns: table => new
                {
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Идентификаторпользователя = table.Column<int>(name: "Идентификатор пользователя", type: "integer", nullable: true),
                    Имя_пользователя = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Роль = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Состояние = table.Column<bool>(type: "boolean", nullable: true),
                    Хэш_пароля = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "v_vehicles",
                schema: "public",
                columns: table => new
                {
                    VIN_номер = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: true),
                    Год_выпуска = table.Column<int>(type: "integer", nullable: true),
                    Госномер = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    Идентификатор_водителя = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_ТС = table.Column<int>(type: "integer", nullable: true),
                    Марка = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Модель = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Пробег = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
                    Состояние = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Страховка = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Тип_топлива = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Тип_ТС = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_trips_request_id",
                schema: "public",
                table: "trips",
                column: "request_id");

            migrationBuilder.CreateIndex(
                name: "IX_trip_requests_user_id",
                schema: "public",
                table: "trip_requests",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_users_user_id",
                schema: "public",
                table: "drivers",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_vehicles_vehicle_id",
                schema: "public",
                table: "drivers",
                column: "vehicle_id",
                principalSchema: "public",
                principalTable: "vehicles",
                principalColumn: "vehicle_id");

            migrationBuilder.AddForeignKey(
                name: "FK_trip_requests_drivers_driver_id",
                schema: "public",
                table: "trip_requests",
                column: "driver_id",
                principalSchema: "public",
                principalTable: "drivers",
                principalColumn: "driver_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_requests_users_user_id",
                schema: "public",
                table: "trip_requests",
                column: "user_id",
                principalSchema: "public",
                principalTable: "users",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trip_requests_vehicles_vehicle_id",
                schema: "public",
                table: "trip_requests",
                column: "vehicle_id",
                principalSchema: "public",
                principalTable: "vehicles",
                principalColumn: "vehicle_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_trips_vehicles_vehicle_id",
                schema: "public",
                table: "trips",
                column: "vehicle_id",
                principalSchema: "public",
                principalTable: "vehicles",
                principalColumn: "vehicle_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_vehicles_drivers_driver_id",
                schema: "public",
                table: "vehicles",
                column: "driver_id",
                principalSchema: "public",
                principalTable: "drivers",
                principalColumn: "driver_id");
        }
    }
}
