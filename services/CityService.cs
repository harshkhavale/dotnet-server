using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICityService
{
    Task<IEnumerable<Cities>> GetAllCitiesAsync();
    Task<Cities?> GetCityByIdAsync(decimal id);
    Task<Cities> CreateCityAsync(Cities city);
    Task<Cities?> UpdateCityAsync(decimal id, Cities city);
    Task<bool> DeleteCityAsync(decimal id);
}

public class CityService : ICityService
{
    private readonly SportsClubContext _context;

    public CityService(SportsClubContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cities>> GetAllCitiesAsync()
    {
        return await _context.Cities

            .ToListAsync();
    }

    public async Task<Cities?> GetCityByIdAsync(decimal id)
    {
        return await _context.Cities

            .SingleOrDefaultAsync(c => c.CityId == id);
    }

  public async Task<Cities> CreateCityAsync(Cities city)
{
    city.CityId = default; 
    _context.Cities.Add(city);
    await _context.SaveChangesAsync();
    return city;
}



    public async Task<Cities?> UpdateCityAsync(decimal id, Cities city)
    {
        var existingCity = await _context.Cities.FindAsync(id);
        if (existingCity == null) return null;

        existingCity.CityName = city.CityName;
        existingCity.StateId = city.StateId;
        existingCity.CountryId = city.CountryId;

        await _context.SaveChangesAsync();
        return existingCity;
    }

    public async Task<bool> DeleteCityAsync(decimal id)
    {
        var city = await _context.Cities.FindAsync(id);
        if (city == null) return false;

        _context.Cities.Remove(city);
        await _context.SaveChangesAsync();
        return true;
    }
}
