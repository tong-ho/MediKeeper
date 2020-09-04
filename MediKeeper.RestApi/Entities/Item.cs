using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediKeeper.RestApi.Entities
{
    public partial class Item
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(256)]
        public string Name { get; set; }
        [Column(TypeName = "decimal(16, 2)")]
        public decimal Cost { get; set; }
    }
}
