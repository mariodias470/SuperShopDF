using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SuperShopDF.Web.Models
{
    // 12.25 - vídeo ASP-NET_MVC_23

    // Ordem de implementação (há três situações):
    // A)
    //     1º modelo
    //     2º controlador
    //     3º view
    //     Só que aqui, neste caso, temos um repositório no meio....
    // B)
    //     1º entidade
    //     2º repositório
    //     3º controlador
    //     4º view
    // C)
    //     1º modelo
    //     2º controlador
    //     3º view
    /*
        ProductId { get; set; }
        Quantity{ get; set; }
        Products { get; set; }
     */

    public class AddItemViewModel
    {
        [Display(Name = "Product")]
        [Range(1, int.MaxValue, ErrorMessage = "A product must be selected!")]
        public int ProductId { get; set; }

        [Range(0.0001, double.MaxValue, ErrorMessage = "The quantity must be a positive number!")]
        public double Quantity { get; set; }

        public IEnumerable<SelectListItem> Products { get; set; }

        public AddItemViewModel()
        {
        }

    } // end class AddItemViewModel
} // end namespace SuperShopDF.Web.Models
