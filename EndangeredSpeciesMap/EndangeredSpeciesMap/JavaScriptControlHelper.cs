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
    public class JavaScriptControlHelperMain
    {
        MainWindow prozor;
        public JavaScriptControlHelperMain(MainWindow w)
        {
            prozor = w;
        }
    }

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [ComVisible(true)]
    public class JavaScriptControlHelperAddSpecie
    {
        AddSpecieWindow prozor;
        public JavaScriptControlHelperAddSpecie(AddSpecieWindow w)
        {
            prozor = w;
        }
    }
}
