using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SuperShopDF.Web.Data.Entities
{
    // 14.12 - vídeo ASP-NET_MVC_22
    /*
        public int 				            Id 		        { get; set; }
        public DateTime  			        OrderDate 	    { get; set; }
        public DateTime 			        DeliveryDate 	{ get; set; }
        public User 				        User 		    { get; set; }
        public IEnumerable<OrderDetail> 	Items		    { get; set; } b// <-- ligação de um para muitos
        public double 				        Quantity 	    => Items == null ? 0 : Items.Sum(i => i.Quantity);
        public decimal 				        Value 		    => Items == null ? 0 : Items.Sum(i => i.Value);
    */

    public class Order : IEntity
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name ="Order date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime  OrderDate { get; set; }

        [Required]
        [Display(Name = "Delivery date")]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}", ApplyFormatInEditMode = false)]
        public DateTime DeliveryDate { get; set; }

        // 22.17 do vídeo ASP.NET_MVC_22 (ligação de muitos para um)
        [Required]
        public User User{ get; set; }

        // 18.03 do vídeo ASP.NET_MVC_22:
        // 22.17 do vídeo ASP.NET_MVC_22 (ligação de um para muitos)
        public IEnumerable<OrderDetail> Items{ get; set; } // <-- ligação de um para muitos

        // 19.39 do vídeo ASP.NET_MVC_22:
        [DisplayFormat(DataFormatString = "{0:N2}")]
        public double Quantity => Items == null ? 0 : Items.Sum(i => i.Quantity);

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Value => Items == null ? 0 : Items.Sum(i => i.Value);

    } // end class Order
} // end namespace SuperShopDF.Web.Data.Entities
