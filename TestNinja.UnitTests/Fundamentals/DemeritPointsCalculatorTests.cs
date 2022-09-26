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
    public class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _calculator;

        [SetUp]
        public void Setup()
        {
            _calculator = new DemeritPointsCalculator();
        }
        [Test]
        [TestCase(-1)]
        [TestCase(301)]
        public void CalculateDemeritPoints_InvalidSpeed_ThrowsArgumentOutOfRangeException(int speed)
        {
            Assert.That(() => _calculator.CalculateDemeritPoints(speed), Throws.Exception.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(65)]
        public void CalculateDemeritPoints_SpeedLessThanOrEqualToSpeedLimit_ReturnsZero(int speed)
        {
            var result = _calculator.CalculateDemeritPoints(speed);
            Assert.That(result, Is.Zero);
        }

        [Test]
        [TestCase(66,0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        [TestCase(80, 3)]
        [TestCase(85, 4)]
        public void CalculateDemeritPoints_SpeedIsValid_ReturnsDemeritPoints(int speed, int demeritPoints)
        {
            var result = _calculator.CalculateDemeritPoints(speed);
            Assert.That(result, Is.EqualTo(demeritPoints));
        }
    }
}
