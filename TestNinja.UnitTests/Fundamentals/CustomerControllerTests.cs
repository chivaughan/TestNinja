using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class CustomerControllerTests
    {
        [Test]
        public void GetCustomer_IdIsZero_ReturnsNotFound()
        {
            var custController = new CustomerController();

            var result = custController.GetCustomer(0);

            Assert.That(result, Is.TypeOf<NotFound>());
        }

        [Test]
        public void GetCustomer_IdIsNotZero_ReturnsOK()
        {
            var custController = new CustomerController();

            var result = custController.GetCustomer(1);

            Assert.That(result, Is.TypeOf<Ok>());
        }
    }
}
