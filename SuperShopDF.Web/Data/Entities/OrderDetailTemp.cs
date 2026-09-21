using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SuperShopDF.Web.Data.Entities
{
    // 08.00 - vídeo ASP-NET_MVC_22
    public class OrderDetailTemp : IEntity
    {
        /*
                Id          { get; set; }
                User        { get; set; }
                Product     { get; set; }
                Price       { get; set; }
                Quantity    { get; set; }
                Value       => Price * (decimal)Quantity;
         */

        public int Id { get; set; }

        [Required]
        public User User { get; set; }

        [Required]
        public Product Product { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Price { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity { get; set; }

        public decimal Value => Price * (decimal)Quantity;
        // <=>
        // public decimal Value
        // {
        //     get
        //     {
        //         return Price * (decimal)Quantity;
        //     }
        // }

    } // end class OrderDetailTemp
} // end namespace SuperShopDF.Web.Data.Entities
