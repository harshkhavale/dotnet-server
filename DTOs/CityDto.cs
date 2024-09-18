public class CityDto
{
    public decimal? CityId { get; set; }
    public string CityName { get; set; } = null!;
    public decimal? StateId { get; set; }
    public StateDto? State { get; set; }
}