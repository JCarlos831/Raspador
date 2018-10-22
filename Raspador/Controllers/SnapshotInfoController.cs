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
    public class SnapshotInfoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISnapshotInfoService _snapshotInfoService;
        private readonly UserManager<IdentityUser> _userManager;

//        public SnapshotInfoController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

        public SnapshotInfoController(ISnapshotInfoService snapshotInfoService, UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _snapshotInfoService = snapshotInfoService;
            _userManager = userManager;
        }

        // GET: SnapshotInfo
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();
            
            var snapshots = await _snapshotInfoService.GetSnapshotAsync(currentUser);

            var model = new SnapshotInfoViewModel()
            {
                Snapshots = snapshots
            };

            return View(model);
            
//            return View(await _context.Snapshots.ToListAsync());
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddSnapshot(SnapshotInfo newSnapshotInfo)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null) return Challenge();

            var successful = await _snapshotInfoService.AddSnapshotAsync(newSnapshotInfo, currentUser);

            if (!successful)
            {
                return BadRequest("Could not add snapshot");
            }

            return RedirectToAction("Index");
        }

//        // GET: SnapshotInfo/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var snapshotInfo = await _context.Snapshots
                .SingleOrDefaultAsync(m => m.Id == id);

            var stocks = from s in _context.Stocks
                    .Where(x => x.SnapshotInfo.Id == id)
                select s;

            if (snapshotInfo == null)
            {
                return NotFound();
            }

            snapshotInfo.Stocks = await stocks.AsNoTracking().ToListAsync();

            return View(snapshotInfo);
//        }
//
//        // GET: SnapshotInfo/Create
//        public IActionResult Create()
//        {
//            return View();
//        }
//
//        // POST: SnapshotInfo/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Create([Bind("Id,NetWorth,DayGainChange,DayGainPercent,TotalGain,TotalGainPercent,Date")] SnapshotInfo snapshotInfo)
//        {
//            if (ModelState.IsValid)
//            {
//                _context.Add(snapshotInfo);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }
//            return View(snapshotInfo);
//        }
//
//        // GET: SnapshotInfo/Edit/5
//        public async Task<IActionResult> Edit(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var snapshotInfo = await _context.Snapshots.FindAsync(id);
//            if (snapshotInfo == null)
//            {
//                return NotFound();
//            }
//            return View(snapshotInfo);
//        }
//
//        // POST: SnapshotInfo/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> Edit(int id, [Bind("Id,NetWorth,DayGainChange,DayGainPercent,TotalGain,TotalGainPercent,Date")] SnapshotInfo snapshotInfo)
//        {
//            if (id != snapshotInfo.Id)
//            {
//                return NotFound();
//            }
//
//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    _context.Update(snapshotInfo);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!SnapshotInfoExists(snapshotInfo.Id))
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
//            return View(snapshotInfo);
//        }
//
//        // GET: SnapshotInfo/Delete/5
//        public async Task<IActionResult> Delete(int? id)
//        {
//            if (id == null)
//            {
//                return NotFound();
//            }
//
//            var snapshotInfo = await _context.Snapshots
//                .FirstOrDefaultAsync(m => m.Id == id);
//            if (snapshotInfo == null)
//            {
//                return NotFound();
//            }
//
//            return View(snapshotInfo);
//        }
//
//        // POST: SnapshotInfo/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> DeleteConfirmed(int id)
//        {
//            var snapshotInfo = await _context.Snapshots.FindAsync(id);
//            _context.Snapshots.Remove(snapshotInfo);
//            await _context.SaveChangesAsync();
//            return RedirectToAction(nameof(Index));
//        }
//
//        private bool SnapshotInfoExists(int id)
//        {
//            return _context.Snapshots.Any(e => e.Id == id);
//        }
        }
    }
}
