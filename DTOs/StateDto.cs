public class StateDto
{
    public decimal StateId { get; set; }
    public string StateName { get; set; } = null!;
    public decimal? CountryId { get; set; }
    public CountryDto? Country { get; set; }
    public ICollection<CityDto> Cities { get; set; } = new List<CityDto>();
}

