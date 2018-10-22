using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Raspador.Data;
using Raspador.Models;
using Raspador.Services;

namespace Raspador.Controllers
{
    [Authorize]
    public class StockController : Controller
    {
//        private readonly ApplicationDbContext _context;
        private readonly IStockService _stockService;
        private readonly UserManager<IdentityUser> _userManager;

//        public StockController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

        public StockController(IStockService stockService, UserManager<IdentityUser> userManager)
        {
            _stockService = stockService;
            _userManager = userManager;
        }

        // GET: Stock
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            
            var stocks = await _stockService.GetStocksAsync(currentUser);

            var model = new StockViewModel()
            {
                Stocks = stocks
            };

            return View(model);


//            return View(await _context.Stocks.ToListAsync());
        }

//        // GET: Stock/Details/5
//        public async Task<IActionResult> Details(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var stock = await _context.Stocks
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (stock == null)
//            {
//                return NotFound();
//            }
//
//            return View(stock);
//        }
//
//        // GET: Stock/Create
//        public IActionResult Create()
//        {
//            return View();
//        }
//
//        // POST: Stock/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,Symbol,LastPrice,PriceChange,PricePercentChange,Shares,CostBasis,MarketValue,DayGainChange,DayGainPercentChange,TotalGainChange,TotalGainPercentChange,NumOfLots,Notes,Date")] Stock stock)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(stock);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(stock);
//        }
//
//        // GET: Stock/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var stock = await _context.Stocks.FindAsync(id);
//            if (stock == null)
//            {
//                return NotFound();
//            }
//            return View(stock);
//        }
//
//        // POST: Stock/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,Symbol,LastPrice,PriceChange,PricePercentChange,Shares,CostBasis,MarketValue,DayGainChange,DayGainPercentChange,TotalGainChange,TotalGainPercentChange,NumOfLots,Notes,Date")] Stock stock)
//        {
//            if (id != stock.Id)
//            {
//                return NotFound();
//            }
//
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(stock);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!StockExists(stock.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }
//            return View(stock);
//        }
//
//        // GET: Stock/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var stock = await _context.Stocks
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (stock == null)
//            {
//                return NotFound();
//            }
//
//            return View(stock);
//        }
//
//        // POST: Stock/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var stock = await _context.Stocks.FindAsync(id);
//            _context.Stocks.Remove(stock);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//
//        private bool StockExists(int id)
//        {
//            return _context.Stocks.Any(e => e.Id == id);
//        }
    }
}
