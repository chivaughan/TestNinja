using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Mocking;
using Moq;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelperTests
    {
        private Mock<IBookingsStorage> _storage;
        private Booking booking1;
        private Booking booking2;
        private Booking booking3;
        [SetUp]
        public void Setup()
        {
            _storage = new Mock<IBookingsStorage>();
            booking1 = new Booking
            {
                Id = 1,
                Status = "Active",
                Reference = "abc",
                ArrivalDate = DateTime.Today,
                DepartureDate = DateTime.Today.AddDays(20)
            };
            booking2 = new Booking
            {
                Id = 2,
                Status = "Active",
                Reference = "def",
                ArrivalDate = DateTime.Today.AddDays(-2),
                DepartureDate = DateTime.Today.AddDays(5)
            };
            booking3 = new Booking
            {
                Id = 3,
                Status = "Active",
                Reference = "ghi",
                ArrivalDate = DateTime.Today.AddDays(15),
                DepartureDate = DateTime.Today.AddDays(-5)
            };
        }
        [Test]
        public void OverlappingBookingsExist_CancelledBooking_ReturnsAnEmptyString()
        {
            Booking booking = new Booking { Status = "Cancelled" };
            _storage.Setup(s => s.GetActiveBookings(booking));

            var result = BookingHelper.OverlappingBookingsExist(booking,_storage.Object);

            Assert.That(result, Is.EqualTo(String.Empty));
        }

        [Test]
        public void OverlappingBookingsExist_ValidBookingExistsAndOverlaps_ReturnsTheOverlappingBookingReference()
        {            
            _storage.Setup(s => s.GetActiveBookings(booking1)).Returns(new List<Booking> { booking2, booking3 });

            var result = BookingHelper.OverlappingBookingsExist(booking1, _storage.Object);

            Assert.That(result, Is.EqualTo("def"));
        }

        [Test]
        public void OverlappingBookingsExist_ValidBookingExistsButDoesNotOverlap_ReturnsAnEmptyString()
        {
            // Arrange
            booking1.ArrivalDate = DateTime.Today;
            booking1.DepartureDate = DateTime.Today.AddDays(20);
            booking2.ArrivalDate = DateTime.Today.AddDays(15);
            booking2.DepartureDate = DateTime.Today.AddDays(-5);
            booking3.ArrivalDate = DateTime.Today.AddDays(15);
            booking3.DepartureDate = DateTime.Today.AddDays(-5);

            _storage.Setup(s => s.GetActiveBookings(booking1)).Returns(new List<Booking> { booking2,booking3});

            // Act
            var result = BookingHelper.OverlappingBookingsExist(booking1, _storage.Object);

            //Assert
            Assert.That(result, Is.EqualTo(String.Empty));
        }
    }
}
