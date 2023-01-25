using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace API_MSSQL.Models
{
    [Table("entries")]
    public class Entry
    {
        [Key]
        [Column("id")]
        public int? Id { get; set; }

        [Required]
        [ForeignKey("AuthorId")]
        [Column("author")]
        public User? Author { get; set; }

        [Required]
        [Column("text")]
        public string? Text { get; set; }

        [Required]
        [Column("hashtags")]
        public string? Hashtags { get; set; }
    }
}
