using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SBGCore.Logger;

namespace SBGCore
{
    public class Logger
    {
        /// <summary>
        /// A collection
        /// </summary>
        public enum LogTarget
        {
            File,
            DB,
            EventLog,
            NUL = -1
        }
    }

    public abstract class LogBase
    {
        protected readonly object lockObj = new object();

        public abstract void Log(string msg);
    }

    /// <summary>
    /// Use this to simply log to one of the <seealso cref="LogTarget"/>s
    /// </summary>
    public static class LogHelper
    {
        private static LogBase logger = null;

        /// <summary>
        /// <para>Log a message to a <seealso cref="LogTarget"/></para>
        /// <para>(If the msg string is empty, we are actually deleting the old log file)</para>
        /// </summary>
        /// <param name="target">Where do we want to write the log entry to <seealso cref="LogTarget"/></param>
        /// <param name="msg">The message to log. if this is empty it empties the log target</param>
        public static void Log(LogTarget target, string msg)
        {
            switch (target)
            {
                case LogTarget.File:
                    {
                        logger = new FileLogger();
                        break;
                    }

                case LogTarget.DB:
                    {
                        logger = new DBLogger();
                        break;
                    }
                case LogTarget.EventLog:
                    {
                        logger = new EventLogger();
                        break;
                    }
                case LogTarget.NUL:
                    {
                        logger = new FileLogger();
                        break;
                    }
                default:
                    break;
            }
            if (logger != null)
            {
                logger.Log(msg);
            }
            else
            {
                throw new ArgumentNullException("logger", "No Valid Logtarget has been specified, couldn't log the event :\n" + msg);
            }
        }
    }
}
