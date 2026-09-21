using SuperShopDF.Web.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace SuperShopDF.Web.Data.Entities
{
    // 12.14 - vídeo ASP-NET_MVC_22
    public class OrderDetail : IEntity
    {
        public int Id { get; set; }

        [Required]
        public Product Product { get; set; } // xpto

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }

        public decimal Value => Price * (decimal)Quantity;

    } // end class OrderDetail
} // end namespace SuperShopDF.Web.Data.Entities
