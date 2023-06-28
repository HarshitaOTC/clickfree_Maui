using clickfree_Maui.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.Windows
{
    public class BaseWindow : Window
    {

        #region Properties

        public virtual bool HideSysMenu { get { return true; } }

        #endregion

        #region Ctor

        public BaseWindow()
        {
            ContentPage_Loaded();
        }

        #endregion

        #region Event handlers

        private void BaseWindow_Loaded(object sender, EventArgs e)
        {
            if (HideSysMenu)
            {
              //  WinAPI.HideSysMENU(new win);
            }

            ContentPage_Loaded();
        }

        #endregion

        #region Protected virtual

        protected virtual void ContentPage_Loaded()
        {

        }

        #endregion

    }
}
