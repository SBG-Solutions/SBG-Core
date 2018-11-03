using System;

namespace SBGCore
{
    public class DBLogger : LogBase
    {
        string connectionString = string.Empty;

        public override void Log(string msg)
        {
            lock (lockObj)
            {
                //todo(smzb): Generate Database writing code
                throw new NotImplementedException(); 
            }
        }
    }
}
