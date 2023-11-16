using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class tbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "countries",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    country_id = table.Column<int>(type: "integer", nullable: false),
                    country_name = table.Column<string>(type: "character varying", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_countries", x => x.id);
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
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jurisdictions", x => x.jurisdiction_id);
                });

            migrationBuilder.CreateTable(
                name: "organizations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    organization_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_name = table.Column<string>(type: "character varying", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_organizations", x => x.organization_id);
                });

            migrationBuilder.CreateTable(
                name: "user_types",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_type_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_types_name = table.Column<string>(type: "character varying", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_types", x => x.user_type_id);
                });

            migrationBuilder.CreateTable(
                name: "states",
                columns: table => new
                {
                    state_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    state_name = table.Column<string>(type: "character varying", nullable: true),
                    state_code = table.Column<int>(type: "integer", nullable: false),
                    zipcode = table.Column<int>(type: "integer", nullable: false),
                    fk_country_id = table.Column<int>(type: "integer", nullable: false),
                    countryid = table.Column<Guid>(type: "uuid", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_states", x => x.state_id);
                    table.ForeignKey(
                        name: "FK_states_countries_countryid",
                        column: x => x.countryid,
                        principalTable: "countries",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "jurd_org_ass",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    jurd_org_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    organization_id = table.Column<int>(type: "integer", nullable: false),
                    jurisdiction_id = table.Column<int>(type: "integer", nullable: false),
                    organizationsorganization_id = table.Column<int>(type: "integer", nullable: false),
                    jurisdictionsjurisdiction_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_jurd_org_ass", x => x.jurd_org_id);
                    table.ForeignKey(
                        name: "FK_jurd_org_ass_jurisdictions_jurisdictionsjurisdiction_id",
                        column: x => x.jurisdictionsjurisdiction_id,
                        principalTable: "jurisdictions",
                        principalColumn: "jurisdiction_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_jurd_org_ass_organizations_organizationsorganization_id",
                        column: x => x.organizationsorganization_id,
                        principalTable: "organizations",
                        principalColumn: "organization_id",
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
                    statesstate_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_counties", x => x.county_id);
                    table.ForeignKey(
                        name: "FK_counties_states_statesstate_id",
                        column: x => x.statesstate_id,
                        principalTable: "states",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "facilities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    facility_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    facility_name = table.Column<string>(type: "character varying", nullable: true),
                    jur_ord_id = table.Column<int>(type: "integer", nullable: false),
                    jurd_org_assjurd_org_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_facilities", x => x.facility_id);
                    table.ForeignKey(
                        name: "FK_facilities_jurd_org_ass_jurd_org_assjurd_org_id",
                        column: x => x.jurd_org_assjurd_org_id,
                        principalTable: "jurd_org_ass",
                        principalColumn: "jurd_org_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cities",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    city_name = table.Column<string>(type: "character varying", nullable: true),
                    zipcode = table.Column<int>(type: "integer", nullable: true),
                    county_id = table.Column<int>(type: "integer", nullable: false),
                    state_id = table.Column<int>(type: "integer", nullable: false),
                    statesstate_id = table.Column<int>(type: "integer", nullable: false),
                    Countiescounty_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cities", x => x.city_id);
                    table.ForeignKey(
                        name: "FK_cities_counties_Countiescounty_id",
                        column: x => x.Countiescounty_id,
                        principalTable: "counties",
                        principalColumn: "county_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cities_states_statesstate_id",
                        column: x => x.statesstate_id,
                        principalTable: "states",
                        principalColumn: "state_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    username = table.Column<string>(type: "character varying", nullable: false),
                    password = table.Column<string>(type: "character varying", nullable: false),
                    county_id = table.Column<int>(type: "integer", nullable: false),
                    city_id = table.Column<int>(type: "integer", nullable: false),
                    facility_id = table.Column<int>(type: "integer", nullable: false),
                    user_type_id = table.Column<int>(type: "integer", nullable: false),
                    user_typesuser_type_id = table.Column<int>(type: "integer", nullable: false),
                    countiescounty_id = table.Column<int>(type: "integer", nullable: false),
                    citiescity_id = table.Column<int>(type: "integer", nullable: false),
                    facilitiesfacility_id = table.Column<int>(type: "integer", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "character varying", nullable: true),
                    updatedby = table.Column<string>(type: "character varying", nullable: true),
                    isdelete = table.Column<bool>(type: "boolean", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_users_cities_citiescity_id",
                        column: x => x.citiescity_id,
                        principalTable: "cities",
                        principalColumn: "city_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_counties_countiescounty_id",
                        column: x => x.countiescounty_id,
                        principalTable: "counties",
                        principalColumn: "county_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_facilities_facilitiesfacility_id",
                        column: x => x.facilitiesfacility_id,
                        principalTable: "facilities",
                        principalColumn: "facility_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users_user_types_user_typesuser_type_id",
                        column: x => x.user_typesuser_type_id,
                        principalTable: "user_types",
                        principalColumn: "user_type_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cities_Countiescounty_id",
                table: "cities",
                column: "Countiescounty_id");

            migrationBuilder.CreateIndex(
                name: "IX_cities_statesstate_id",
                table: "cities",
                column: "statesstate_id");

            migrationBuilder.CreateIndex(
                name: "IX_counties_statesstate_id",
                table: "counties",
                column: "statesstate_id");

            migrationBuilder.CreateIndex(
                name: "IX_facilities_jurd_org_assjurd_org_id",
                table: "facilities",
                column: "jurd_org_assjurd_org_id");

            migrationBuilder.CreateIndex(
                name: "IX_jurd_org_ass_jurisdictionsjurisdiction_id",
                table: "jurd_org_ass",
                column: "jurisdictionsjurisdiction_id");

            migrationBuilder.CreateIndex(
                name: "IX_jurd_org_ass_organizationsorganization_id",
                table: "jurd_org_ass",
                column: "organizationsorganization_id");

            migrationBuilder.CreateIndex(
                name: "IX_states_countryid",
                table: "states",
                column: "countryid");

            migrationBuilder.CreateIndex(
                name: "IX_users_citiescity_id",
                table: "users",
                column: "citiescity_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_countiescounty_id",
                table: "users",
                column: "countiescounty_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_facilitiesfacility_id",
                table: "users",
                column: "facilitiesfacility_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_user_typesuser_type_id",
                table: "users",
                column: "user_typesuser_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "cities");

            migrationBuilder.DropTable(
                name: "facilities");

            migrationBuilder.DropTable(
                name: "user_types");

            migrationBuilder.DropTable(
                name: "counties");

            migrationBuilder.DropTable(
                name: "jurd_org_ass");

            migrationBuilder.DropTable(
                name: "states");

            migrationBuilder.DropTable(
                name: "jurisdictions");

            migrationBuilder.DropTable(
                name: "organizations");

            migrationBuilder.DropTable(
                name: "countries");
        }
    }
}
