using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppCRUD.Data
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        public string? OrderName { get; set; }
        public string? State { get; set; }
        public List<Window> Windows { get; set; } = new List<Window>();
    }
}

