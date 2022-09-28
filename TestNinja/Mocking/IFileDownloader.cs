namespace TestNinja.Mocking
{
    public interface IFileDownloader
    {
        void DownloadFile(string fileUrl, string destinationFilePath);
    }
}