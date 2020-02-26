using System;
using System.Collections.Generic;
using System.Text;

namespace Influence.Domain.Entities
{
    public class Like
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public int UserId { get; set; }

        public int Amount { get; set; }
    }
}
