using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using FormulaOne.Entities.DbSet;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repositories
{
    public class AchievmentsRepository : GenericRepositories<Achivement>, IAchievmentsRepository
    {
        public AchievmentsRepository(AppDbContext context, ILogger logger) : base(context, logger) { }

        public async Task<Achivement?> GetAchivementsByDriverId(Guid driverId)
        {
            try
            {
                return (await _context.Achivement.FirstOrDefaultAsync(a => a.DriverID == driverId && a.Status == 1));
                    
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving achivements for driver with ID {DriverId}", driverId);
                return new ();
            }
        }

        public override async Task<IEnumerable<Achivement>> All()
        {
            try
            {
                return await _context.Achivement.Where(x => x.Status == 1).AsNoTracking().AsSplitQuery().OrderBy(x => x.AddedDate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all Achivements");
                return Enumerable.Empty<Achivement>();
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                Achivement? result = await _context.Achivement.FirstOrDefaultAsync(x => x.Id == id);
                if (result != null)
                {
                    return false;
                }

                result!.Status = 0;
                result.UpdatedDate = DateTime.UtcNow;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting Achivements");
                return false;
            }
        }

        public override async Task<bool> Update(Achivement entity)
        {
            try
            {
                Achivement? result = await _context.Achivement.FirstOrDefaultAsync(x => x.Id == entity.Id);

                if (result == null)
                    return false;

                result.UpdatedDate = DateTime.UtcNow;
                result.FastestLap = entity.FastestLap;
                result.PolePosition = entity.PolePosition;
                result.RaceWins = entity.RaceWins;
                result.WorldChampionship = entity.WorldChampionship;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Achivements");
                return false;
            }
        }
    }
}
