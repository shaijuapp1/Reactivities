
using Domain;

namespace Application.TableFields
{
    public class TableFieldDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; } 
        public int TableId { get; set; }
        //public virtual TableName Table { get; set; }
        
    }
}