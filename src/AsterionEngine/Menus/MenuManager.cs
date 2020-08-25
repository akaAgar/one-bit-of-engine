using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asterion.Menus
{
    public sealed class MenuManager
    {
        private MenuPage CurrentPage = null;

        public bool InMenu { get { return CurrentPage != null; } }

        public AsterionGame Game { get; private set; }

        internal MenuManager(AsterionGame game)
        {
            Game = game;
        }

        public void ShowPage<T>(params object[] parameters) where T : MenuPage, new()
        {
            ClosePage();
            CurrentPage = new T();
            CurrentPage.Initialize(this, parameters);
        }

        public void ClosePage()
        {
            if (CurrentPage == null) return;
            CurrentPage.Dispose();
            CurrentPage = null;
        }

        internal void Dispose()
        {
            ClosePage();
        }

        internal void Render()
        {
            if (!InMenu) return;
        }
    }
}
