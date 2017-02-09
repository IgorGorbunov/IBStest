using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBStest
{
    /// <summary>
    /// Класс любимых блюд
    /// </summary>
    public class FavoriteDish
    {
        /// <summary>
        /// Наименование блюда
        /// </summary>
        [DisplayName("Наименование")]
        public string Name { get; set; }

        public FavoriteDish()
        {
            
        }

        /// <summary>
        /// Конструктор задания любимого блюда
        /// </summary>
        /// <param name="name">Наименование блюда</param>
        public FavoriteDish(string name)
        {
            Name = name;
        }
    }
}
