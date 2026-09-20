using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SuperShopDF.Web.Data.Entities
{
    // 08.00 - vídeo ASP-NET_MVC_22
    public class OrderDetailTemp : IEntity
    {
        public int Id { get; set; }

        [Required]
        public User user { get; set; }

        [Required]
        public Product product { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }

        public decimal Value => Price * (decimal)Quantity;

    } // end class OrderDetailTemp
} // end namespace SuperShopDF.Web.Data.Entities
