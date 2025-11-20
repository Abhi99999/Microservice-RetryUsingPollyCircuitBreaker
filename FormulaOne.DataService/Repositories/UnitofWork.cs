using FormulaOne.DataService.Data;
using FormulaOne.DataService.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormulaOne.DataService.Repositories
{
    public class UnitofWork : IUnitofWork,IDisposable
    {
        private readonly AppDbContext _context;
        public IDriverRepository Drivers { get;}

        public IAchievmentsRepository Achivements { get;}

        public UnitofWork(AppDbContext context,ILoggerFactory loggerFactory)
        {
            _context = context;
            var logger = loggerFactory.CreateLogger("logs");
            Drivers = new DriverRepository(_context,logger);
            Achivements = new AchievmentsRepository(_context,logger);
        }
        public async Task<bool> CompleteAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
