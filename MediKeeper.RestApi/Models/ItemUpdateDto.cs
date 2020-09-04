using System.ComponentModel.DataAnnotations;

namespace MediKeeper.RestApi.Models
{
    /// <summary>
    /// Model used to update an existing item. The item id should be passed as a query parameter.
    /// </summary>
    public class ItemUpdateDto
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
