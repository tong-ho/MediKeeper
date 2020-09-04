using System.ComponentModel.DataAnnotations;

namespace MediKeeper.RestApi.Models
{
    /// <summary>
    /// The model of the item.
    /// </summary>
    public class ItemDto
    {
        /// <summary>
        /// The id of the item.
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// The name of the item.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The cost of the item.
        /// </summary>
        [Required]
        public decimal Cost { get; set; }
    }
}
