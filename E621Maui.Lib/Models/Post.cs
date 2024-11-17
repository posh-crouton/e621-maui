using E621Maui.Lib.Enums;

namespace E621Maui.Lib.Models
{
    public class Post
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string LockedTags { get; set; }
        public long ChangeSeq { get; set; }
        public PostRating Rating { get; set; }
        public int FavCount { get; set; }
        public long ApproverId { get; set; }
        public long UploaderId { get; set; }
        public string Description { get; set; }
        public int CommentCount { get; set; }
        public bool IsFavorited { get; set; }
        public bool HasNotes { get; set; }
        public decimal? Duration { get; set; }
        public string[] Sources { get; set; }
        public string[] Pools { get; set; }

        public File File { get; set; }
        public Preview Preview { get; set; }
        public Sample Sample { get; set; }
        public Score Score { get; set; }
        public Flags Flags { get; set; }
        public Relationships Relationships { get; set; }
    }
}
