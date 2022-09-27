using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TestNinja.Mocking
{
    public class VideoService
    {
        public IFileReader Reader { get; set; }
        public IVideoRepository Repository { get; set; }

        public VideoService()
        {
            Repository = new VideoRepository();
            Reader = new FileReader();
        }

        public VideoService(IFileReader reader, IVideoRepository repository)
        {
            Reader = reader;
            Repository = repository;
        }

        public string ReadVideoTitle()
        {
            var str = Reader.Read("video.txt");
            var video = JsonConvert.DeserializeObject<Video>(str);
            if (video == null)
                return "Error parsing the video.";
            return video.Title;
        }

        public string GetUnprocessedVideosAsCsv()
        {
            var videoIds = new List<int>();

            var videos = Repository.GetUnprocessedVideos();
            foreach (var v in videos)
                videoIds.Add(v.Id);
            return String.Join(",", videoIds);
            
        }
    }

    public class Video
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsProcessed { get; set; }
    }

    public class VideoContext : DbContext
    {
        public DbSet<Video> Videos { get; set; }
    }
}