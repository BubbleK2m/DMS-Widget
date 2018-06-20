using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomitoryWidget.ViewModel
{
    class MealListViewModel
    {
        private string name;

        public string Name
        {
            get => name;
            set => name = value;
        }

        internal MealListViewModel(string name)
        {
            this.name = name;
        }
    }
}
