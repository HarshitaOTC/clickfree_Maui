using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clickfree_Maui.Exceptions
{
    public class UsbDriveNotFoundException : Exception
    {
        #region Ctor
        public UsbDriveNotFoundException()
        {
        }
        public UsbDriveNotFoundException(string message, Exception e) : base(message, e)
        {
        }
        #endregion
    }
}
