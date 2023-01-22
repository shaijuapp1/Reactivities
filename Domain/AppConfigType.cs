using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 
namespace Domain
{
    public class AppConfigType
    {
        [Key]
        // [DatabaseGenerated(DatabasecontGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
    }
}