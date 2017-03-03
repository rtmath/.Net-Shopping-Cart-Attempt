using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using WingtipToys.Models;

namespace WingtipToys.Logic
{
    [Table("ShoppingCartActions")]
    public class ShoppingCartActions
    {
        [Key]
        public string ShoppingCartId { get; set; }

        public const string CartSessionKey = "CartId";
        
        public ShoppingCartActions()
        {

        }
    }
}
