namespace SE_Project.Models
{


   public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string ThumbnailUrl { get; set; }  // link poza
        public string AudioUrl { get; set; }      // link fisier .mp3
        public DateTime CreatedAt { get; set; }
        public int PlayCount { get; set; }        // pentru trending
    }
}