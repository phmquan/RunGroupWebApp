using Microsoft.EntityFrameworkCore;
using RunGroupWebApp.Data;
using RunGroupWebApp.Interfaces;
using RunGroupWebApp.Models;

namespace RunGroupWebApp.Repository
{
    public class RaceRepository : IRaceRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IRaceRepository raceRepository;

        public RaceRepository(ApplicationDbContext _context)
        {
            this._context = _context;
            this.raceRepository = raceRepository;
        }
        public bool Add(Race race)
        {
            _context.Add(race);
            return Save();
        }

        public bool Delete(Race race)
        {
            _context.Remove(race);
            return Save();
        }

        public async Task<IEnumerable<Race>> GetAll()
        {
            return await _context.Races.ToListAsync();
        }

        public async Task<IEnumerable<Race>> GetAllRacesByCity(string city)
        {
            return await _context.Races.Where(i=>i.Address.City == city).ToListAsync();
        }

        public Task<Race> GetByIdAsync(int id)
        {
            return _context.Races.Include(i=>i.Address).FirstOrDefaultAsync(i=>i.Id==id);
        }

        public bool Save()
        {
            var save=_context.SaveChanges();
            return save > 0? true: false; 
        }

        public bool Update(Race race)
        {
            _context.Update(race);
            return Save();
        }
    }
}
