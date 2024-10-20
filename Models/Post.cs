namespace Social_Media.Models;

public class Post
{
    public int PostID { get; set; }
    public int UserID { get; set; }
    public string Content { get; set; }
    public bool IsFlagged { get; set; } = false;  // Indicates if the content contains banned words

    public string FlagMessage { get; set; }
    public DateTime  
 CreatedAt { get; set; }

        public 
 List<Like> Likes { get; set; } = new List<Like>();
        public List<Comment> Comments { get; set; } = new List<Comment>();
    }

    public class Like
    {
        public int LikeID { get; set; }
        public int PostID { get; set; }
        public int UserID { get; set; }
    }

public record Comment(int CommentID, int PostID, int UserID, string
 Content);
