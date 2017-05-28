using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CheapIdeas.Web.Models.Entities
{
    public class Idea
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public string UserId { get; set; }
        [NotMapped]
        public string UserName { get; set; }
    }
}