using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Raspador.Models;

namespace Raspador.Services
{
    public interface IStockService
    {
        Task<List<Stock>> GetStocksAsync(int id);
    }
}