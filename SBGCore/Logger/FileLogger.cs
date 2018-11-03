using System.IO;
using System.Runtime.CompilerServices;

namespace SBGCore
{
    public class FileLogger : LogBase
    {
        public string filePath;
        private readonly string _extension = ".log";

        /// <summary>
        /// Ensure we are working with a valid path/filename.
        /// If there is no content in the variables, create a name for the logfile else
        /// use the existing variables and create a name for the logfile.
        /// </summary>
        /// <param name="callerName"></param>
        public void SanitizeFileName([CallerMemberName] string callerName = "")
        {
            if (SBGCore.C0re.CoreDirectory==string.Empty || SBGCore.C0re.OwnerName==string.Empty) {
                filePath = "SBGCore." + callerName + _extension;
            } else {
                filePath = SBGCore.C0re.CoreDirectory + @"\" + SBGCore.C0re.OwnerName + "." + callerName + _extension;
            }
        }

        /// <summary>
        /// This logs a message to a specified file, after the name for the file has been sanitized
        /// </summary>
        /// <param name="msg">the message to write to the file log</param>
        public override void Log(string msg)
        {
            SanitizeFileName();
            if (msg!="")
            {
                /// If the message has content write this away to the file (appending it)
                lock (lockObj)
                {
                    using (StreamWriter fileHandle = new StreamWriter(filePath, true))
                    {
                        fileHandle.WriteLine(msg);
                        fileHandle.Close();
                    }
                } 
            } else
            {
                /// If the message is empty, open and close the file without appending and then flush it.
                lock (lockObj)
                {
                    using (StreamWriter fileHandle = new StreamWriter(filePath, false))
                    {
                        fileHandle.Flush();
                        fileHandle.Close();
                    }
                }
            }
            
        }
    }
}
