using System.Collections.Generic;

namespace Influence.Data.Models
{
    public class PostModel
    {
        public string Text { get; set; }

        public string Imagesrc { get; set; }

        public int UserId { get; set; }

        public List<LikeModel> Likes { get; set; }

        public List<CommentModel> Comments { get; set; }
    }
}
