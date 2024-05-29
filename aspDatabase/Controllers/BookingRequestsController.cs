using aspDatabase.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

[Authorize(Roles = "admin")]
public class BookingRequestsController : Controller
{
    private readonly BookingDBContext _context;

    public BookingRequestsController(BookingDBContext context)
    {
        _context = context;
    }

    // GET: BookingRequests
    public async Task<IActionResult> Index()
    {
        var bookingDBContext = _context.BookingRequests.Include(b => b.Hotel);
        return View(await bookingDBContext.ToListAsync());
    }

    // GET: BookingRequests/Details/5
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
        ViewData["Idhotel"] = new SelectList(_context.Hotels, "Id", "Name");
        var bookingRequest = new BookingRequest();
        if (roomId.HasValue)
        {
            bookingRequest.IdRoom = roomId.Value; // Заполните поле номера комнаты
        }
        if (hotelId.HasValue)
        {
            bookingRequest.Idhotel = hotelId.Value; // Заполните поле номера отеля
        }
        return View(bookingRequest);
    }

    // POST: BookingRequests/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,SecondName,FirstName,ThirdName,NumberofPhone,passportSeries,nubmerPassport,BookingDateStart,BookingDateEnd,Idhotel,AdressRoom")] BookingRequest bookingRequest)
    {
        if (ModelState.IsValid)
        {
            _context.Add(bookingRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["Idhotel"] = new SelectList(_context.Hotels, "Id", "Id", bookingRequest.Idhotel);
        return View(bookingRequest);
    }

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
        ViewData["Idhotel"] = new SelectList(_context.Hotels, "Id", "Id", bookingRequest.Idhotel);
        return View(bookingRequest);
    }

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
        ViewData["Idhotel"] = new SelectList(_context.Hotels, "Id", "Id", bookingRequest.Idhotel);
        return View(bookingRequest);
    }

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