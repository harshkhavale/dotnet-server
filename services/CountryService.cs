using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface ICountryService
{
    Task<IEnumerable<Countries>> GetAllCountriesAsync();
    Task<Countries?> GetCountryByIdAsync(int id);
    Task<Countries> CreateCountryAsync(Countries country);
    Task<Countries?> UpdateCountryAsync(int id, Countries country);
    Task<bool> DeleteCountryAsync(int id);
}

public class CountryService : ICountryService
{
    private readonly SportsClubContext _context;

    public CountryService(SportsClubContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Countries>> GetAllCountriesAsync()
    {
        return await _context.Countries
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Countries?> GetCountryByIdAsync(int id)
    {
        return await _context.Countries
            .AsNoTracking()
            .SingleOrDefaultAsync(c => c.CountryId == id);
    }

    public async Task<Countries> CreateCountryAsync(Countries country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();
        return country;
    }

    public async Task<Countries?> UpdateCountryAsync(int id, Countries country)
    {
        var existingCountry = await _context.Countries.FindAsync(id);
        if (existingCountry == null) return null;

        existingCountry.CountryName = country.CountryName;
        existingCountry.Isocode = country.Isocode;

        _context.Entry(existingCountry).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
            return existingCountry;
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CountryExistsAsync(id))
                return null;
            else
                throw;
        }
    }

    public async Task<bool> DeleteCountryAsync(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null) return false;

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();
        return true;
    }

    private async Task<bool> CountryExistsAsync(int id)
    {
        return await _context.Countries.AnyAsync(e => e.CountryId == id);
    }
}
