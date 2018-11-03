using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SBGCore;
using static SBGCore.Logger;

namespace SBGCore_demo
{
    class Program
    {
        private static SBGCore.SBGConfig _config;

        static void Main(string[] args)
        {
            LogHelper.Log(Logger.LogTarget.File,"");
            _config = new SBGCore.SBGConfig("SBGCoreDemo");
            _config.SetConfig("Application", "SBGCore Demo");
            _config.SetConfig("TestParam1", 2.4f);
            _config.SetConfig("TestParam2", 4);
            LogHelper.Log(Logger.LogTarget.File, "Successfully started the application");
            Console.WriteLine("Test1: {0}",_config.GetConfig("TestParam1"));
            Console.WriteLine("Test2: {0}", _config.GetConfig("TestParam2"));
            _config.WriteConfig();
            //Console.ReadKey();
            LogHelper.Log(Logger.LogTarget.File, "Closing application!");
        }
    }
}
