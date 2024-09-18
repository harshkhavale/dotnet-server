
public class MembershipPlanDto
{
    public int PlanId { get; set; }
    public string PlanName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public DateTime ModifiedDateTime { get; set; }
    public string? CreatedBy { get; set; }
    public string? ModifiedBy { get; set; }
    public int? CorporateId { get; set; }
    public decimal? CorporateShare { get; set; }


    public List<MembershipPlanAttributeDto> MembershipPlanAttributes { get; set; }
}