using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Raspador.Data;
using Raspador.Models;

namespace Raspador.Services
{
    public class StockService : IStockService
    {
        private readonly ApplicationDbContext _context;

        public StockService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetStocksAsync(int id)
        {
            var snapshotId = id;
            
            var stocks = await _context.Stocks
                    .Where(x => x.SnapshotInfo.SnapshotId == snapshotId)
                    .AsNoTracking()
                    .ToListAsync();
            return stocks;
        }
    }
}