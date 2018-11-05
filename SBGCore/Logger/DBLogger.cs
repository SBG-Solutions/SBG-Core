using System;

namespace SBGCore
{
    /// <summary>
    /// Database logger
    /// </summary>
    public class DBLogger : LogBase
    {
        private string connectionString = string.Empty;

        /// <summary>
        /// Log a string into a database
        /// </summary>
        /// <param name="msg">Message string</param>
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
