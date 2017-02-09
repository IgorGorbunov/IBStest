using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBStest
{
    public class FavoriteDish
    {
        [DisplayName("Наименование")]
        public string Name { get; set; }

        public FavoriteDish()
        {
            
        }

        public FavoriteDish(string name)
        {
            Name = name;
        }
    }
}
