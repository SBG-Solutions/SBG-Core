using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SBGCore.Logger;

namespace SBGCore
{
    /// <summary>
    /// Logger class
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// A collection of logtargets
        /// </summary>
        public enum LogTarget
        {
            /// <summary>
            /// LogTarget: File
            /// </summary>
            File,

            /// <summary>
            /// LogTarget Database
            /// </summary>
            DB,

            /// <summary>
            /// LogTarget: OS Eventlogger
            /// </summary>
            EventLog,

            /// <summary>
            /// LogTarget: no target
            /// </summary>
            NUL = -1
        }
    }

    /// <summary>
    /// Abstract logger class from which all loggers inherit
    /// </summary>
    public abstract class LogBase
    {
        /// <summary>
        /// An object we use for exclusive locking
        /// </summary>
        protected readonly object lockObj = new object();

        /// <summary>
        /// Log a message
        /// </summary>
        /// <param name="msg">The message string to log</param>
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
