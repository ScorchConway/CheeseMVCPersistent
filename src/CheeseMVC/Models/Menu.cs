using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class Menu
    {

        public int ID { set; get; }
        public string Name { set; get; }
        
        public IList<CheeseMenu> CheeseMenu { set; get; }

    }
}
