using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class NewStructureEntitiesAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contacts_Types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    email = table.Column<string>(type: "character varying", nullable: true),
                    phone = table.Column<string>(type: "character varying", nullable: true),
                    cell = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts_Types", x => x.contact_type_id);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    country_name = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.country_id);
                });

            migrationBuilder.CreateTable(
                name: "LOV_type_master",
                columns: table => new
                {
                    reference_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(type: "character varying", nullable: true),
                    value = table.Column<string>(type: "character varying", nullable: true),
                    LOV_type = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LOV_type_master", x => x.reference_id);
                });

            migrationBuilder.CreateTable(
                name: "Organizations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_name = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizations", x => x.organization_id);
                });

            migrationBuilder.CreateTable(
                name: "User_Types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_types_name = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Types", x => x.user_type_id);
                });

            migrationBuilder.CreateTable(
                name: "jurisdictions",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    jurisdiction_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    jurisdiction_name = table.Column<string>(type: "character varying", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jurisdictions", x => x.jurisdiction_id);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    contact_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    contact_type_id = table.Column<int>(type: "integer", nullable: false),
                    contact_value = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.contact_id);
                    table.ForeignKey(
                        name: "FK_Contacts_Contacts_Types_contact_type_id",
                        column: x => x.contact_type_id,
                        principalTable: "Contacts_Types",
                        principalColumn: "contact_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    state_name = table.Column<string>(type: "character varying", nullable: true),
                    state_code = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.state_id);
                    table.ForeignKey(
                        name: "FK_States_Countries_country_id",
                        column: x => x.country_id,
                        principalTable: "Countries",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    users_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_firstname = table.Column<string>(type: "character varying", nullable: true),
                    user_lasttname = table.Column<string>(type: "character varying", nullable: true),
                    designation = table.Column<string>(type: "character varying", nullable: true),
                    user_type_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.users_id);
                    table.ForeignKey(
                        name: "FK_Users_User_Types_user_type_id",
                        column: x => x.user_type_id,
                        principalTable: "User_Types",
                        principalColumn: "user_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Juridictions_organization_association",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    juridictions_organization_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    jurisdiction_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Juridictions_organization_association", x => x.juridictions_organization_id);
                    table.ForeignKey(
                        name: "FK_Juridictions_organization_association_Organizations_organiz~",
                        column: x => x.organization_id,
                        principalTable: "Organizations",
                        principalColumn: "organization_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Juridictions_organization_association_jurisdictions_jurisdi~",
                        column: x => x.jurisdiction_id,
                        principalTable: "jurisdictions",
                        principalColumn: "jurisdiction_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "counties",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    county_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    county_name = table.Column<string>(type: "character varying", nullable: true),
                    county_code = table.Column<int>(type: "integer", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_counties", x => x.county_id);
                    table.ForeignKey(
                        name: "FK_counties_States_state_id",
                        column: x => x.state_id,
                        principalTable: "States",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Facilities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    facility_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    facility_name = table.Column<string>(type: "character varying", nullable: true),
                    juridictions_organization_id = table.Column<int>(type: "integer", nullable: false),
                    administered_at_location = table.Column<string>(type: "character varying", nullable: true),
                    sending_organization = table.Column<string>(type: "character varying", nullable: true),
                    responsible_organization = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Facilities", x => x.facility_id);
                    table.ForeignKey(
                        name: "FK_Facilities_Juridictions_organization_association_juridictio~",
                        column: x => x.juridictions_organization_id,
                        principalTable: "Juridictions_organization_association",
                        principalColumn: "juridictions_organization_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    line1 = table.Column<string>(type: "character varying", nullable: true),
                    line2 = table.Column<string>(type: "character varying", nullable: true),
                    Suite = table.Column<string>(type: "character varying", nullable: true),
                    county_id = table.Column<int>(type: "integer", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.address_id);
                    table.ForeignKey(
                        name: "FK_Addresses_Countries_country_id",
                        column: x => x.country_id,
                        principalTable: "Countries",
                        principalColumn: "country_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_States_state_id",
                        column: x => x.state_id,
                        principalTable: "States",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_counties_county_id",
                        column: x => x.county_id,
                        principalTable: "counties",
                        principalColumn: "county_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city_name = table.Column<string>(type: "character varying", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: true),
                    county_id = table.Column<int>(type: "integer", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    created_by = table.Column<string>(type: "character varying", nullable: true),
                    updated_by = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.city_id);
                    table.ForeignKey(
                        name: "FK_Cities_States_state_id",
                        column: x => x.state_id,
                        principalTable: "States",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cities_counties_county_id",
                        column: x => x.county_id,
                        principalTable: "counties",
                        principalColumn: "county_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_country_id",
                table: "Addresses",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_county_id",
                table: "Addresses",
                column: "county_id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_state_id",
                table: "Addresses",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_county_id",
                table: "Cities",
                column: "county_id");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_state_id",
                table: "Cities",
                column: "state_id");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_contact_type_id",
                table: "Contacts",
                column: "contact_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_juridictions_organization_id",
                table: "Facilities",
                column: "juridictions_organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_Juridictions_organization_association_jurisdiction_id",
                table: "Juridictions_organization_association",
                column: "jurisdiction_id");

            migrationBuilder.CreateIndex(
                name: "IX_Juridictions_organization_association_organization_id",
                table: "Juridictions_organization_association",
                column: "organization_id");

            migrationBuilder.CreateIndex(
                name: "IX_States_country_id",
                table: "States",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_Users_user_type_id",
                table: "Users",
                column: "user_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_counties_state_id",
                table: "counties",
                column: "state_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Facilities");

            migrationBuilder.DropTable(
                name: "LOV_type_master");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "counties");

            migrationBuilder.DropTable(
                name: "Contacts_Types");

            migrationBuilder.DropTable(
                name: "Juridictions_organization_association");

            migrationBuilder.DropTable(
                name: "User_Types");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "Organizations");

            migrationBuilder.DropTable(
                name: "jurisdictions");

            migrationBuilder.DropTable(
                name: "Countries");
        }
    }
}
