using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsClubApi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMembershipPlanDetails_MembershipPlans_PlanId",
                table: "UserMembershipPlanDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMembershipPlanDetails_MembershipPlans_PlanId",
                table: "UserMembershipPlanDetails",
                column: "PlanId",
                principalTable: "MembershipPlans",
                principalColumn: "PlanId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMembershipPlanDetails_MembershipPlans_PlanId",
                table: "UserMembershipPlanDetails");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMembershipPlanDetails_MembershipPlans_PlanId",
                table: "UserMembershipPlanDetails",
                column: "PlanId",
                principalTable: "MembershipPlans",
                principalColumn: "PlanId");
        }
    }
}
