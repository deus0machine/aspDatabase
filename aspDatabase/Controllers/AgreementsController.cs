using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using aspDatabase.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace aspDatabase.Controllers
{
    [Authorize(Roles = "admin")]
    public class AgreementsController : Controller
    {
        private readonly BookingDBContext _context;

        public AgreementsController(BookingDBContext context)
        {
            _context = context;
        }

        // GET: Agreements
        public async Task<IActionResult> Index()
        {
            var agreements = _context.Agreements
                            .Include(a => a.Client)
                            .Include(a => a.Hotel) 
                            .ToList();
            return View(agreements);
        }

        // GET: Agreements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agreement == null)
            {
                return NotFound();
            }

            return View(agreement);
        }

        // GET: Agreements/Create
        public IActionResult Create()
        {
            var hotels = _context.Hotels.ToList();
            var clients = _context.Clients.ToList();
            var rooms = _context.Rooms.ToList();

            Debug.WriteLine($"Hotels count: {hotels.Count}");
            Debug.WriteLine($"Clients count: {clients.Count}");
            Debug.WriteLine($"Rooms count: {rooms.Count}");

            ViewData["hotelId"] = new SelectList(_context.Hotels, "Id", "Name"); // Отображение имени отеля
            ViewData["clientID"] = new SelectList(_context.Clients, "ID", "SecondName"); // Отображение полного имени клиента
            ViewData["roomID"] = new SelectList(_context.Rooms, "ID", "AdressRoom"); // Отображение адреса комнаты
            var agreement = new Agreement
            {
                dateDoc = DateTime.Now // Установка текущей даты
            };

            return View(agreement);
        }

        // POST: Agreements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,clientID,dateDoc,hotelId,roomID,reservStart,reservEnd")] Agreement agreement)
        {
            if (ModelState.IsValid)
            {
                var room = _context.Rooms.FirstOrDefault(r => r.ID == agreement.roomID);

                if (room != null)
                {
                    // Рассчитайте количество дней
                    var days = (agreement.reservEnd - agreement.reservStart).Days;
                    // Рассчитайте стоимость
                    agreement.Cost = room.Cost * days;
                }
                _context.Add(agreement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["hotelId"] = new SelectList(_context.Hotels, "Id", "Name", agreement.hotelId);
            ViewData["clientID"] = new SelectList(_context.Clients, "ID", "SecondName", agreement.clientID);
            ViewData["roomID"] = new SelectList(_context.Rooms, "ID", "AdressRoom", agreement.roomID);
            return View(agreement);
        }
        [HttpGet]
        public JsonResult GetRoomsByHotelId(int hotelId)
        {
            var rooms = _context.Rooms
                                .Where(r => r.Idhotel == hotelId)
                                .Select(r => new { r.ID, r.AdressRoom })
                                .ToList();
            return Json(rooms);
        }
        [HttpGet]
        public JsonResult GetRoomCost(int roomId)
        {
            var cost = _context.Rooms
                               .Where(r => r.ID == roomId)
                               .Select(r => r.Cost)
                               .FirstOrDefault();
            return Json(cost);
        }

        // GET: Agreements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements.FindAsync(id);
            if (agreement == null)
            {
                return NotFound();
            }
            return View(agreement);
        }

        // POST: Agreements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,clientID,dateDoc,hotelId,roomID,reservStart,reservEnd")] Agreement agreement)
        {
            if (id != agreement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agreement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgreementExists(agreement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(agreement);
        }

        // GET: Agreements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agreement = await _context.Agreements
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agreement == null)
            {
                return NotFound();
            }

            return View(agreement);
        }

        // POST: Agreements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var agreement = await _context.Agreements.FindAsync(id);
            if (agreement != null)
            {
                _context.Agreements.Remove(agreement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgreementExists(int id)
        {
            return _context.Agreements.Any(e => e.Id == id);
        }
    }
}
