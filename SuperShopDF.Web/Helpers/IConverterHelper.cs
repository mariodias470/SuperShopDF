using SuperShopDF.Web.Data.Entities;
using SuperShopDF.Web.Models;

namespace SuperShopDF.Web.Helpers
{

    // 19.30 do vídeo ASP.NET_MVC_12 - para retirarmos os converters do controlador.
    public interface IConverterHelper
    {
        Product ToProduct(ProductViewModel model, string path, bool isNew);

        Product ToProductViewModel(Product product);


    } // end interface IConverterHelper
} // end namespace SuperShopDF.Web.Helpers
