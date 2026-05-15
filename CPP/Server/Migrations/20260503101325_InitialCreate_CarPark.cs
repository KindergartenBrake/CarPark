using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CP.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate_CarPark : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    password_hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true"),
                    role = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
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
                    Идентификатор_водителя = table.Column<int>(type: "integer", nullable: true),
                    Имя = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Фамилия = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Отчество = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Номер_прав = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Дата_выдачи_прав = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Дата_окончания_прав = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Дата_рождения = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Телефон = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Состояние = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "v_trip_requests",
                schema: "public",
                columns: table => new
                {
                    Идентификатор_заявки = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_пользователя = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_водителя = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_ТС = table.Column<int>(type: "integer", nullable: true),
                    Дата_создания = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
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
                    Идентификатор_поездки = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_заявки = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_ТС = table.Column<int>(type: "integer", nullable: true),
                    Дата_поездки = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Время_начала = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Время_окончания = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Пробег_в_начале = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
                    Пробег_в_конце = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
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
                    Идентификаторпользователя = table.Column<int>(name: "Идентификатор пользователя", type: "integer", nullable: true),
                    Имя_пользователя = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Хэш_пароля = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Состояние = table.Column<bool>(type: "boolean", nullable: true),
                    Роль = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "v_vehicles",
                schema: "public",
                columns: table => new
                {
                    Идентификатор_ТС = table.Column<int>(type: "integer", nullable: true),
                    Идентификатор_водителя = table.Column<int>(type: "integer", nullable: true),
                    Госномер = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: true),
                    VIN_номер = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: true),
                    Марка = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Модель = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Год_выпуска = table.Column<int>(type: "integer", nullable: true),
                    Пробег = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
                    Тип_топлива = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Тип_ТС = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Состояние = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Страховка = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "public",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "public",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "public",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "public",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "drivers",
                schema: "public",
                columns: table => new
                {
                    driver_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    vehicle_id = table.Column<int>(type: "integer", nullable: true),
                    first_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    middle_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    license_number = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    license_issue_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    license_expiry_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    birth_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: true, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_drivers", x => x.driver_id);
                    table.ForeignKey(
                        name: "FK_drivers_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id");
                });

            migrationBuilder.CreateTable(
                name: "vehicles",
                schema: "public",
                columns: table => new
                {
                    vehicle_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    driver_id = table.Column<int>(type: "integer", nullable: true),
                    license_plate = table.Column<string>(type: "character varying(12)", maxLength: 12, nullable: false),
                    vin = table.Column<string>(type: "character varying(17)", maxLength: 17, nullable: false),
                    brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    year = table.Column<int>(type: "integer", nullable: false),
                    mileage = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true, defaultValueSql: "0"),
                    fuel_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    vehicle_type = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    insurance = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vehicles", x => x.vehicle_id);
                    table.ForeignKey(
                        name: "FK_vehicles_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "public",
                        principalTable: "drivers",
                        principalColumn: "driver_id");
                });

            migrationBuilder.CreateTable(
                name: "trip_requests",
                schema: "public",
                columns: table => new
                {
                    request_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    driver_id = table.Column<int>(type: "integer", nullable: false),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP"),
                    description = table.Column<string>(type: "text", nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValueSql: "'pending'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trip_requests", x => x.request_id);
                    table.ForeignKey(
                        name: "FK_trip_requests_drivers_driver_id",
                        column: x => x.driver_id,
                        principalSchema: "public",
                        principalTable: "drivers",
                        principalColumn: "driver_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trip_requests_users_user_id",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_trip_requests_vehicles_vehicle_id",
                        column: x => x.vehicle_id,
                        principalSchema: "public",
                        principalTable: "vehicles",
                        principalColumn: "vehicle_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "trips",
                schema: "public",
                columns: table => new
                {
                    trip_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    request_id = table.Column<int>(type: "integer", nullable: false),
                    vehicle_id = table.Column<int>(type: "integer", nullable: false),
                    trip_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    start_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    end_time = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    start_odometer = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: false),
                    end_odometer = table.Column<decimal>(type: "numeric(12,1)", precision: 12, scale: 1, nullable: true),
                    status = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValueSql: "'planned'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_trips", x => x.trip_id);
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
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "public",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "public",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "public",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "public",
                table: "AspNetUserRoles",
                column: "RoleId");

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
                name: "IX_trips_request_id",
                schema: "public",
                table: "trips",
                column: "request_id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_drivers_vehicles_vehicle_id",
                schema: "public",
                table: "drivers",
                column: "vehicle_id",
                principalSchema: "public",
                principalTable: "vehicles",
                principalColumn: "vehicle_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drivers_users_user_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropForeignKey(
                name: "FK_drivers_vehicles_vehicle_id",
                schema: "public",
                table: "drivers");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "trips",
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

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "public");

            migrationBuilder.DropTable(
                name: "trip_requests",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
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
