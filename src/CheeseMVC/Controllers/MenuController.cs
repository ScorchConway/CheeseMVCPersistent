using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CheeseMVC.Controllers
{
    public class MenuController : Controller
    {

        private readonly CheeseDbContext context;

        public MenuController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<Menu> menus = context.Menus.ToList();

            return View(menus);
        }

        public IActionResult Add()
        {
            return View(new AddMenuViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddMenuViewModel menu)
        {

            if (ModelState.IsValid)
            {
                Menu newMenu = new Menu()
                {
                    Name = menu.Name
                };

                context.Menus.Add(newMenu);
                context.SaveChanges();

                return Redirect("/Menu/ViewMenu/" + newMenu.ID);
            }

            return View(menu);
        }

        // /Menu/ViewMenu/n  (where id = n)
        public IActionResult ViewMenu(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);

            List<CheeseMenu> items = context
            .CheeseMenus
            .Include(item => item.Cheese)
            .Where(cm => cm.MenuID == id)
            .ToList();

            ViewMenuViewModel viewModel = new ViewMenuViewModel()
            {
                Menu = menu,
                Items = items
            };

            return View(viewModel);
        }

        public IActionResult AddItem(int id)
        {
            Menu menu = context.Menus.Single(m => m.ID == id);
            IEnumerable<Cheese> cheeses = context.Cheeses.ToList();
            AddMenuItemViewModel viewModel = new AddMenuItemViewModel(menu, cheeses);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddItem(AddMenuItemViewModel viewModel)
        {

            if(ModelState.IsValid)
            {
                IList<CheeseMenu> existingItems = context.CheeseMenus
                    .Where(cm => cm.CheeseID == viewModel.CheeseID)
                    .Where(cm => cm.MenuID == viewModel.MenuID).ToList();
                
                if( ! existingItems.Any())
                {
                    CheeseMenu cm = new CheeseMenu()
                    {
                        CheeseID = viewModel.CheeseID,
                        MenuID = viewModel.MenuID
                        //might need to add Cheese and Menu properties to this block
                    };
                    context.CheeseMenus.Add(cm);
                    context.SaveChanges();
                }

                return Redirect("/Menu/ViewMenu/" + viewModel.MenuID);
            }

            return View();
        }
    }
}