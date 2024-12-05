using Microsoft.EntityFrameworkCore;
using temuruang_be.Dtos.BookingDTO;
using temuruang_be.Models;

namespace temuruang_be.Services;

public interface IBookingService
{
    Task<Booking> FetchBookingByID(int id);
    Task<Booking> CreateBooking(CreateBookDTO dto);
    Task<bool> UpdateBooking(int id, UpdateBookDTO dto);
    Task<bool> DeleteBooking(int id, Guid userId);
}

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _dbCtx;

    public BookingService(ApplicationDbContext dbContext)
    {
        _dbCtx = dbContext;
    }

    public async Task<Booking?> FetchBookingByID(int id)
    {
        Booking? booking = await _dbCtx.Booking.Where(b => b.Id == id).AsNoTracking().FirstOrDefaultAsync();

        return booking;
    }

    public async Task<Booking> CreateBooking(CreateBookDTO dto) 
    {
        Booking booking = CreateBookDTO.ToBooking(dto);
        _dbCtx.Add(booking);

        await _dbCtx.SaveChangesAsync();   

        return booking;
    }
    public async Task<bool> UpdateBooking(int id, UpdateBookDTO dto) 
    {
        Booking? existing = await FetchBookingByID(id);

        if (existing == null)
        {
            return false;
        }

        Booking booking = UpdateBookDTO.ToBooking(dto);
        booking.Id = id;
        booking.UserId = existing.UserId;
        booking.WorkspaceId = existing.WorkspaceId;

        _dbCtx.Update(booking);

        await _dbCtx.SaveChangesAsync();

        return true;
    }
    public async Task<bool> DeleteBooking(int id, Guid userId) 
    {
        Booking? existing = await FetchBookingByID(id);

        if (existing == null)
        {
            return false;
        }

        // should do some validation operation
        if (!existing.UserId.Equals(userId))
        {
            return false;
        }

        _dbCtx.Remove(existing);

        await _dbCtx.SaveChangesAsync();

        return true; 
    }
}