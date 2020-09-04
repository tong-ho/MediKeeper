using System.ComponentModel.DataAnnotations;

namespace MediKeeper.RestApi.Models
{
    /// <summary>
    /// The model used to create an item.
    /// </summary>
    public class ItemCreateDto
    {
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
