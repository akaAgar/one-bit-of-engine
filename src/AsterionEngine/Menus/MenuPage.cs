using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asterion.Menus
{
    public abstract class MenuPage
    {
        public MenuPage() { }

        internal void Dispose()
        {

        }

        internal void Initialize(MenuManager menuManager, object[] parameters)
        {
            OnInitialize(parameters);
        }

        protected abstract void OnInitialize(object[] parameters);
    }
}
