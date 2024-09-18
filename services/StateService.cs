using Microsoft.EntityFrameworkCore;
using SportsClubApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
public interface IStateService
{
    Task<IEnumerable<States>> GetAllStatesAsync();
    Task<States?> GetStateByIdAsync(decimal id);
    Task<States> CreateStateAsync(States state);
    Task<States?> UpdateStateAsync(decimal id, States state);
    Task<bool> DeleteStateAsync(decimal id);
}
public class StateService : IStateService
{
    private readonly SportsClubContext _context;

    public StateService(SportsClubContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<States>> GetAllStatesAsync()
    {
        return await _context.States.ToListAsync();
    }

    public async Task<States?> GetStateByIdAsync(decimal id)
    {
        return await _context.States.SingleOrDefaultAsync(s => s.StateId == id);
    }

    public async Task<States> CreateStateAsync(States state)
    {
        _context.States.Add(state);
        await _context.SaveChangesAsync();
        return state;
    }

    public async Task<States?> UpdateStateAsync(decimal id, States state)
    {
        var existingState = await _context.States.FindAsync(id);
        if (existingState == null) return null;

        existingState.StateName = state.StateName;
        existingState.CountryId = state.CountryId;

        await _context.SaveChangesAsync();
        return existingState;
    }

    public async Task<bool> DeleteStateAsync(decimal id)
    {
        var state = await _context.States.FindAsync(id);
        if (state == null) return false;

        _context.States.Remove(state);
        await _context.SaveChangesAsync();
        return true;
    }
}
