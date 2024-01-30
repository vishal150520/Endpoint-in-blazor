using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppCRUD.Data
{
    public class Window
    {
        [Key]
        public int WindowId { get; set; }
        public string? WindowName { get; set; }
        public int QuantityOfWindows { get; set; }
        public int TotalSubElements { get; set; }
        [ForeignKey(nameof(Order))]
        public int OrderId { get; set; }
        public Order? Orders { get; set; }
        public List<SubElement> SubElements { get; set; } = new List<SubElement>();
    }
}
