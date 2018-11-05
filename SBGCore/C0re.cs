using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SBGCore
{
    /// <summary>
    /// SBGCore class
    /// </summary>
    public class C0re
    {
        /// <summary>
        /// Who owns this instance of the config parser
        /// </summary>
        private static string _ownerName;

        /// <summary>
        /// This is the public name of the Owner for this instance of SBGConfig
        /// </summary>
        public static string OwnerName { get => _ownerName; set => _ownerName = value; }

        /// <summary>
        /// Give me the directory where the exe resides that called this assembly/dll
        /// </summary>
        public static readonly string CoreDirectory = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
    }
}
