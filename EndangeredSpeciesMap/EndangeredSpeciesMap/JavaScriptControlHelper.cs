using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using EndangeredSpeciesMap;

namespace EndangeredSpeciesMap
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelper
    {
        MainWindow prozor;
        public JavaScriptControlHelper(MainWindow w)
        {
            prozor = w;
        }
    }
}
