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
    public class VideServiceTests
    {
        public Mock<IVideoRepository> _videoRepository;
        public Mock<IFileReader> _fileReader;
        public VideoService _service;
        [SetUp]
        public void Setup()
        {
            _videoRepository = new Mock<IVideoRepository>();
            _fileReader = new Mock<IFileReader>();
            _service = new VideoService(_fileReader.Object, _videoRepository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnsAnErrorMessage()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");

            var result = _service.ReadVideoTitle();

            Assert.That(result, Does.Contain("error").IgnoreCase);
        }

        [Test]
        public void ReadVideoTitle_FileExists_ReturnsFileContent()
        {
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("{title: 'My Video Title'}");

            var result = _service.ReadVideoTitle();

            Assert.That(result, Is.EqualTo("My Video Title"));
        }
        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnsEmptyString()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_SomeVideosAreUnProcessed_ReturnsVideoIdsAsCsv()
        {
            _videoRepository.Setup(r => r.GetUnprocessedVideos()).Returns(
                new List<Video>() 
                { 
                new Video { Id=1},
                new Video { Id=2},
                new Video { Id=3}                
            });

            var result = _service.GetUnprocessedVideosAsCsv();

            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
