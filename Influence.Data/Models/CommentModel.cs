using System;
using System.Collections.Generic;
using System.Text;

namespace Influence.Data.Models
{
    public class CommentModel
    {
        public string Text { get; set; }
        public int PostId { get; set; }
        public string UserName { get; set; }
    }
}
