using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using Moq;
using NUnit.Framework;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class EmployeeControllerTests
    {
        private EmployeeController _controller;
        private Mock<IEmployeeStorage> _employeeStorage;

        [SetUp]
        public void Setup()
        {
            _employeeStorage = new Mock<IEmployeeStorage>();
            _controller = new EmployeeController(_employeeStorage.Object);
        }

        [Test]
        public void DeleteEmployee_WhenCalled_RemoveEmployeeFromDatabase()
        {
            _controller.DeleteEmployee(1);

            _employeeStorage.Verify(s => s.DeleteEmployee(1));
        }

        

        [Test]
        public void DeleteEmployee_EmployeeDeletedOrDoesNotExist_RedirectUserToEmployeesAction()
        {
            var result = _controller.DeleteEmployee(1);

            Assert.That(result, Is.TypeOf<RedirectResult>());
        }
    }
}
