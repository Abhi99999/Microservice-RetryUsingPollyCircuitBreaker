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
    public class DriverRepository : GenericRepositories<Driver>, IDriverRepository
    {
        public DriverRepository(AppDbContext context, ILogger logger) : base(context, logger) { }

        

        public override async Task<IEnumerable<Driver>> All()
        {
            try
            {
                return await _context.Drivers.Where(x => x.Status == 1).AsNoTracking().AsSplitQuery().OrderBy(x => x.AddedDate).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all drivers");
                return Enumerable.Empty<Driver>();
            }
        }

        public override async Task<bool> Delete(Guid id)
        {
            try
            {
                Driver? result = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == id);
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
                _logger.LogError(ex, "Error deleting driver");
                return false;
            }
        }

        public override async Task<bool> Update(Driver entity)
        {
            try
            {
                Driver? result = await _context.Drivers.FirstOrDefaultAsync(x => x.Id == entity.Id);
                
                if (result == null)
                    return false;

                result.UpdatedDate = DateTime.UtcNow;
                result.FirstName = entity.FirstName;
                result.LastName = entity.LastName;
                result.DriverNumber = entity.DriverNumber;
                result.DOB = entity.DOB;
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating driver");
                return false;
            }
        }

    }
}
