using System.Diagnostics;

namespace SBGCore
{
    public class EventLogger : LogBase
    {
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
