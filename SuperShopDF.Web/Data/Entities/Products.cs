using Microsoft.AspNetCore.WebUtilities;
using System;
using System.ComponentModel.DataAnnotations;


/*-------------------------------------------------------------
 | Para a geração do controlador, através da linha de comando, 
 | ProductsController.cs e das respectivas views 
 | VIDE 
 | o fim deste ficheiro!...
 +-------------------------------------------------------------*/

namespace SuperShopDF.Web.Data.Entities
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public decimal Price { get; set; }

        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        [Display(Name = "Last Purchase")]
        public DateTime LastPurchase { get; set; }

        [Display(Name = "Last Sale")]
        public DateTime LastSale { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}", ApplyFormatInEditMode = false)]
        public double Stock { get; set; }
    
    } // end class Product
} // end namespace SuperShopDF.Web.Data.Entities


/*
       =================================
       Geração do ProductsController.cs 
       e das Views.
       =================================

Abri uma command prompt:
Tools --> Command Line --> Developer Command Prompt

Fiz cd D:\CppCet105\RS2026\Projs\SuperShopDF\SuperShopDF.Web 
porque ele queixava-se de que não encontrava o project file.
       
para gerar o scaffolding:
D:\CppCet105\RS2026\Projs\SuperShopDF\SuperShopDF.Web>dotnet aspnet-codegenerator controller -name ProductsController -m Product -dc DataContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLib

O mesmo código em várias linhas:

D:\CppCet105\RS2026\Projs\SuperShopDF\SuperShopDF.Web>
                               dotnet aspnet-codegenerator 
							   controller -name ProductsController 
                               -m Product 
							   -dc DataContext 
							   --relativeFolderPath Controllers 
							   --useDefaultLayout 
							   --referenceScriptLib

resultado:



Vídeo 4 - Rafael Santos:
	1.14.33 --> add-migration InitDb
	1.18.45 --> update-database
	1.21.56 --> Criação do controlador Products respectivas Views
	1.34.14 --> Commit 'First Controller' (Commit All and Sync)

                FIM
                FIM
                FIM
=====================================================================================*/

