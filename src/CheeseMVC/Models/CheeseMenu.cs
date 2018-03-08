using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CheeseMVC.Models
{
    public class CheeseMenu
    {
        public int CheeseID { set; get; }
        public Cheese Cheese { set; get; }

        public int MenuID { set; get; }
        public Menu Menu { set; get; }
    }
}
