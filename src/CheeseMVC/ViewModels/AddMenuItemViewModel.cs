using CheeseMVC.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.ViewModels
{
    public class AddMenuItemViewModel
    {
        public Menu Menu { get; set; }
        public List<SelectListItem> Cheeses { get; set; }

        [Required]
        public int CheeseID { get; set; }
        [Required]
        public int MenuID { get; set; }

        public AddMenuItemViewModel() { }

        public AddMenuItemViewModel(Menu menu, IEnumerable<Cheese> cheeses)
        {
            Menu = menu;
            Cheeses = new List<SelectListItem>();

            foreach (Cheese c in cheeses)
            {
                Cheeses.Add(new SelectListItem
                {
                    Value = c.ID.ToString(),
                    Text = c.Name
                });
            }
        }
    }
}
