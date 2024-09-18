using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsClubApi.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureUserMembershipPlanDetailRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                });

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    CityId = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CountryId = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateId = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.CityId);
                });

            migrationBuilder.CreateTable(
                name: "CorporateUsers",
                columns: table => new
                {
                    CorporateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorporateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonDetail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactPersonMobileNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactPersonEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailIdentifier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CorporateUsers", x => x.CorporateId);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Isocode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.CountryId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "States",
                columns: table => new
                {
                    StateId = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_States", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    SupplierId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SupplierType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SupplierContact = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "UserCategory",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategory", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "MembershipPlans",
                columns: table => new
                {
                    PlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CorporateId = table.Column<int>(type: "int", nullable: true),
                    CorporateShare = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlans", x => x.PlanId);
                    table.ForeignKey(
                        name: "FK_MembershipPlans_CorporateUsers_CorporateId",
                        column: x => x.CorporateId,
                        principalTable: "CorporateUsers",
                        principalColumn: "CorporateId");
                });

            migrationBuilder.CreateTable(
                name: "SupplierActivitiesDetails",
                columns: table => new
                {
                    SupplierActivityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierId = table.Column<int>(type: "int", nullable: true),
                    ActivityId = table.Column<int>(type: "int", nullable: true),
                    ActivitySchedule = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierActivitiesDetails", x => x.SupplierActivityId);
                    table.ForeignKey(
                        name: "FK_SupplierActivitiesDetails_Activities_ActivityId",
                        column: x => x.ActivityId,
                        principalTable: "Activities",
                        principalColumn: "ActivityId");
                    table.ForeignKey(
                        name: "FK_SupplierActivitiesDetails_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "SupplierId");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dob = table.Column<DateOnly>(type: "date", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Postalcode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    GoogleLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePic = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNotification = table.Column<bool>(type: "bit", nullable: true),
                    EmailNotification = table.Column<bool>(type: "bit", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ActivationLevel = table.Column<int>(type: "int", nullable: true),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: true),
                    CreatedBy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifiedBy = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    LastModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<int>(type: "int", nullable: true),
                    EmailVerified = table.Column<int>(type: "int", nullable: true),
                    MobileVerified = table.Column<int>(type: "int", nullable: true),
                    IsVerified = table.Column<bool>(type: "bit", nullable: true),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    CityId = table.Column<int>(type: "int", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    UserCategoryId = table.Column<int>(type: "int", nullable: true),
                    PasswordReset = table.Column<bool>(type: "bit", nullable: true),
                    DataVerified = table.Column<bool>(type: "bit", nullable: true),
                    DataVerificationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EmailVerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileVerificationCode = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_UserCategory_UserCategoryId",
                        column: x => x.UserCategoryId,
                        principalTable: "UserCategory",
                        principalColumn: "CategoryId");
                });

            migrationBuilder.CreateTable(
                name: "MembershipPlanAttribute",
                columns: table => new
                {
                    AttributeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    Attributename = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Attributedetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipPlanAttribute", x => x.AttributeId);
                    table.ForeignKey(
                        name: "FK_MembershipPlanAttribute_MembershipPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipPlans",
                        principalColumn: "PlanId");
                });

            migrationBuilder.CreateTable(
                name: "UserConsentDetails",
                columns: table => new
                {
                    ConsentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    ConsentType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsentStatus = table.Column<bool>(type: "bit", nullable: true),
                    ConsentDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserConsentDetails", x => x.ConsentId);
                    table.ForeignKey(
                        name: "FK_UserConsentDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserMembershipPlanDetails",
                columns: table => new
                {
                    UserPlanId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: true),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: true),
                    PlanId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMembershipPlanDetails", x => x.UserPlanId);
                    table.ForeignKey(
                        name: "FK_UserMembershipPlanDetails_MembershipPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipPlans",
                        principalColumn: "PlanId");
                    table.ForeignKey(
                        name: "FK_UserMembershipPlanDetails_MembershipPlans_PlanId1",
                        column: x => x.PlanId1,
                        principalTable: "MembershipPlans",
                        principalColumn: "PlanId");
                    table.ForeignKey(
                        name: "FK_UserMembershipPlanDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserNotificationHistories",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    NotificationMessage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NotificationDate = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotificationHistories", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_UserNotificationHistories_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserPaymentDetails",
                columns: table => new
                {
                    PaymentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    PlanId = table.Column<int>(type: "int", nullable: true),
                    PaymentDate = table.Column<DateOnly>(type: "date", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPaymentDetails", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_UserPaymentDetails_MembershipPlans_PlanId",
                        column: x => x.PlanId,
                        principalTable: "MembershipPlans",
                        principalColumn: "PlanId");
                    table.ForeignKey(
                        name: "FK_UserPaymentDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserRolesDetails",
                columns: table => new
                {
                    UserRoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRolesDetails", x => x.UserRoleId);
                    table.ForeignKey(
                        name: "FK_UserRolesDetails_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_UserRolesDetails_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPlanAttribute_PlanId",
                table: "MembershipPlanAttribute",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_MembershipPlans_CorporateId",
                table: "MembershipPlans",
                column: "CorporateId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierActivitiesDetails_ActivityId",
                table: "SupplierActivitiesDetails",
                column: "ActivityId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierActivitiesDetails_SupplierId",
                table: "SupplierActivitiesDetails",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_UserConsentDetails_UserId",
                table: "UserConsentDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipPlanDetails_PlanId",
                table: "UserMembershipPlanDetails",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipPlanDetails_PlanId1",
                table: "UserMembershipPlanDetails",
                column: "PlanId1");

            migrationBuilder.CreateIndex(
                name: "IX_UserMembershipPlanDetails_UserId",
                table: "UserMembershipPlanDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserNotificationHistories_UserId",
                table: "UserNotificationHistories",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentDetails_PlanId",
                table: "UserPaymentDetails",
                column: "PlanId");

            migrationBuilder.CreateIndex(
                name: "IX_UserPaymentDetails_UserId",
                table: "UserPaymentDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolesDetails_RoleId",
                table: "UserRolesDetails",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRolesDetails_UserId",
                table: "UserRolesDetails",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true,
                filter: "[Email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Mobile",
                table: "Users",
                column: "Mobile",
                unique: true,
                filter: "[Mobile] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UserCategoryId",
                table: "Users",
                column: "UserCategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "MembershipPlanAttribute");

            migrationBuilder.DropTable(
                name: "States");

            migrationBuilder.DropTable(
                name: "SupplierActivitiesDetails");

            migrationBuilder.DropTable(
                name: "UserConsentDetails");

            migrationBuilder.DropTable(
                name: "UserMembershipPlanDetails");

            migrationBuilder.DropTable(
                name: "UserNotificationHistories");

            migrationBuilder.DropTable(
                name: "UserPaymentDetails");

            migrationBuilder.DropTable(
                name: "UserRolesDetails");

            migrationBuilder.DropTable(
                name: "Activities");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "MembershipPlans");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "CorporateUsers");

            migrationBuilder.DropTable(
                name: "UserCategory");
        }
    }
}
