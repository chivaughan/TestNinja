using System.Collections.Generic;

namespace TestNinja.Mocking
{
    public interface IBookingsStorage
    {
        IEnumerable<Booking> GetActiveBookings(Booking excludedBooking);
    }
}