﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CheeseMVC.Data;
using CheeseMVC.Models;
using CheeseMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CheeseMVC.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CheeseDbContext context;

        public CategoryController(CheeseDbContext dbContext)
        {
            context = dbContext;
        }

        public IActionResult Index()
        {
            List<CheeseCategory> categories = context.Categories.ToList();

            return View(categories);
        }

        public IActionResult Add()
        {
            return View(new AddCategoryViewModel());
        }

        [HttpPost]
        public IActionResult Add(AddCategoryViewModel category)
        {
            if(ModelState.IsValid)
            {
                CheeseCategory newCategory = new CheeseCategory()
                {
                    Name = category.Name
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();
                return Redirect("/Category");
            }

            return View(category);
        }
    }
}