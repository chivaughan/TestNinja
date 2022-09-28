using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;
using Moq;
using NUnit.Framework;
using System.Net;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class InstallerHelperTests
    {
        private InstallerHelper _helper;
        private Mock<IFileDownloader> _fileDownloader;
        [SetUp]
        public void Setup()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _helper = new InstallerHelper(_fileDownloader.Object);

        }
        [Test]
        public void DownloadInstaller_DownloadSuccessful_ReturnsTrue()
        {
            var result = _helper.DownloadInstaller("customerName", "installerName");

            Assert.That(result, Is.True);
        }

        [Test]
        public void DownloadInstaller_DownloadFailed_ReturnsFalse()
        {
            _fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>()))
                .Throws<WebException>();
            var result = _helper.DownloadInstaller("customer", "installer");
            Assert.That(result, Is.False);                 
        }
    }
}
