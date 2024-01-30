using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorAppCRUD.Data
{
    public class SubElement
    {
        [Key]
        public int SubElementId { get; set; }
        public int Element { get; set; }
        public string? Type { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        [ForeignKey(nameof(Windows))]
        public int WindowId { get; set; }
        public Window? Windows { get; set; }

    }
}
