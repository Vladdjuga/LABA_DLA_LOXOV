using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LAB02_DLL.Models
{
    public class Article
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Lead { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public Author? Author { get; set; }
        public int? AuthorId { get; set; }
        public Category? Category { get; set; }
        public int? CategoryId { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Tag>? Tags { get; set; }
        public Match? Match { get; set; }
        public int? MatchId { get; set; }
    }
}
