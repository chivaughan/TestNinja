﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    public class ErrorLoggerTests
    {
        private ErrorLogger  _logger;
        [SetUp]
        public void Setup()
        {
            _logger = new ErrorLogger();
        }
        [Test]
        public void Log_WhenCalled_ShouldSetLastErrorProperty()
        {
            _logger.Log("a");

            Assert.That(_logger.LastError, Is.EqualTo("a"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ShouldThrowArgumentNullException(string error)
        {
            Assert.Throws<ArgumentNullException>(() => _logger.Log(error));

        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            var id = Guid.Empty;
            _logger.ErrorLogged += (sender, args) => { id = args; };

            _logger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
