using aspDatabase.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;


public class BookingRequestsController : Controller
{
    private readonly BookingDBContext _context;

    public BookingRequestsController(BookingDBContext context)
    {
        _context = context;
    }

    // GET: BookingRequests
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Index()
    {
        var bookingrequests = _context.BookingRequests
                            .Include(a => a.Hotel)
                            .Include(a => a.Room)
                            .ToList();
        return View(bookingrequests);
    }

    // GET: BookingRequests/Details/5
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookingRequest = await _context.BookingRequests
            .Include(b => b.Hotel)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (bookingRequest == null)
        {
            return NotFound();
        }

        return View(bookingRequest);
    }

    // GET: BookingRequests/Create
    public IActionResult Create(int? roomId, int? hotelId)
    {
        var hotels = _context.Hotels.ToList();
        var rooms = _context.Rooms.ToList();

        Debug.WriteLine($"Hotels count: {hotels.Count}");
        Debug.WriteLine($"Rooms count: {rooms.Count}");

        ViewData["Idhotel"] = new SelectList(_context.Hotels, "Id", "Name");
        ViewData["roomID"] = new SelectList(_context.Rooms, "ID", "AdressRoom");
        var bookingRequest = new BookingRequest();
        /*if (roomId.HasValue)
        {
            bookingRequest.roomID = roomId.Value; // Заполните поле номера комнаты
        }
        if (hotelId.HasValue)
        {
            bookingRequest.roomID = hotelId.Value; // Заполните поле номера отеля
        }*/
        return View(bookingRequest);
    }

    // POST: BookingRequests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,SecondName,FirstName,ThirdName,NumberofPhone,passportSeries,nubmerPassport,BookingDateStart,BookingDateEnd,hotelId,roomID")] BookingRequest bookingRequest)
    {
        if (ModelState.IsValid)
        {
            _context.Add(bookingRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Idhotel"] = new SelectList(_context.Hotels, "Id", "Name", bookingRequest.hotelId);
        ViewData["roomID"] = new SelectList(_context.Rooms, "ID", "AdressRoom", bookingRequest.roomID);
        return View(bookingRequest);
    }

    public JsonResult GetRoomsByHotelId(int hotelId)
    {
        var rooms = _context.Rooms
                            .Where(r => r.Idhotel == hotelId)
                            .Select(r => new { r.ID, r.AdressRoom })
                            .ToList();
        return Json(rooms);
    }
    [HttpGet]
    public async Task<IActionResult> ConfirmBooking(int id)
    {
        Console.WriteLine("ConfirmBooking method called with id: " + id);
        var bookingRequest = await _context.BookingRequests.FindAsync(id);

        if (bookingRequest == null)
        {
            return NotFound(); 
        }
        var days = (bookingRequest.BookingDateEnd - bookingRequest.BookingDateStart).Days;
        // Создаем новую запись в таблице Agreements на основе данных из BookingRequest
        var client = new Client
        {
            FirstName = bookingRequest.FirstName,
            SecondName = bookingRequest.SecondName,
            ThirdName = bookingRequest.ThirdName,
            passportSeries = bookingRequest.passportSeries,
            nubmerPassport = bookingRequest.nubmerPassport,
            NumberofPhone = bookingRequest.NumberofPhone
        };
        var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.FirstName == bookingRequest.FirstName && c.SecondName == bookingRequest.SecondName && c.ThirdName == bookingRequest.ThirdName && c.passportSeries == bookingRequest.passportSeries && c.nubmerPassport == bookingRequest.nubmerPassport);

        int clientId;
        if (existingClient != null)
        {
            clientId = existingClient.ID;
        }
        else
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            clientId = client.ID;
        }
        var room = await _context.Rooms.FirstOrDefaultAsync(r => r.ID == bookingRequest.roomID);
        int cost = 0;
        if (room != null)
        {
            cost = room.Cost * days;
        }
        var agreement = new Agreement
        {
            clientID = clientId, 
            dateDoc = DateTime.Now,
            hotelId = bookingRequest.hotelId,
            roomID = (int)bookingRequest.roomID,
            reservEnd = bookingRequest.BookingDateEnd,
            reservStart = bookingRequest.BookingDateStart,
            Cost = cost
        };

        // Добавляем новую запись в таблицу Agreements и сохраняем изменения в базе данных
        _context.Agreements.Add(agreement);
        await _context.SaveChangesAsync();

        _context.BookingRequests.Remove(bookingRequest);
        await _context.SaveChangesAsync();

        // После успешного сохранения возвращаем редирект на страницу Index или другую нужную страницу
        return RedirectToAction(nameof(Index));
    }

    [Authorize(Roles = "admin")]
    // GET: BookingRequests/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookingRequest = await _context.BookingRequests.FindAsync(id);
        if (bookingRequest == null)
        {
            return NotFound();
        }
        ViewData["Idhotel"] = new SelectList(_context.Hotels, "Id", "Id", bookingRequest.hotelId);
        return View(bookingRequest);
    }
    [Authorize(Roles = "admin")]
    // POST: BookingRequests/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,SecondName,FirstName,ThirdName,NumberofPhone,passportSeries,nubmerPassport,BookingDateStart,BookingDateEnd,Idhotel,AdressRoom")] BookingRequest bookingRequest)
    {
        if (id != bookingRequest.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(bookingRequest);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookingRequestExists(bookingRequest.Id))
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
        ViewData["Idhotel"] = new SelectList(_context.Hotels, "Id", "Id", bookingRequest.hotelId);
        return View(bookingRequest);
    }
    [Authorize(Roles = "admin")]
    // GET: BookingRequests/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var bookingRequest = await _context.BookingRequests
            .Include(b => b.Hotel)
            .FirstOrDefaultAsync(m => m.Id == id);
        if (bookingRequest == null)
        {
            return NotFound();
        }

        return View(bookingRequest);
    }
    [Authorize(Roles = "admin")]
    // POST: BookingRequests/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var bookingRequest = await _context.BookingRequests.FindAsync(id);
        if (bookingRequest != null)
        {
            _context.BookingRequests.Remove(bookingRequest);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookingRequestExists(int id)
    {
        return _context.BookingRequests.Any(e => e.Id == id);
    }
}