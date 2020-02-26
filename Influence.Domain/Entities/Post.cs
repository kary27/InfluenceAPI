using System;
using System.Collections.Generic;
using System.Text;

namespace Influence.Domain.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string Imagesrc { get; set; }
        public int UserId { get; set; }
        public List<Like> Likes { get; set; }
        public List<Comment> Comments{ get; set; }
    }
}
