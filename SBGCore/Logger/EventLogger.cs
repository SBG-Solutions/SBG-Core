using System.Diagnostics;

namespace SBGCore
{
    /// <summary>
    /// An eventlog logger
    /// </summary>
    public class EventLogger : LogBase
    {
        /// <summary>
        /// Log a string message into the os's eventlog
        /// </summary>
        /// <param name="msg"></param>
        public override void Log(string msg)
        {
            lock (lockObj)
            {
                EventLog eventLog = new EventLog()
                {
                    Source = SBGCore.C0re.OwnerName + @" Application Log"
                };
                eventLog.WriteEntry(msg);
            }
        }
    }
}
