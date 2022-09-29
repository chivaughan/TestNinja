using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public class BookingsStorage : IBookingsStorage
    {
        public IEnumerable<Booking> GetActiveBookings(Booking excludedBooking)
        {
            var unitOfWork = new UnitOfWork();
            var bookings =
                unitOfWork.Query<Booking>()
                    .Where(
                        b => b.Id != excludedBooking.Id && b.Status != "Cancelled");
            return bookings;
        }
    }
}
