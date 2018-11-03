using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static SBGCore.SBGConfig;

namespace SBGCore
{
    internal class FileHandler
    {
        /// <summary>
        /// A simple handle for writing conf files
        /// </summary>
        private StreamWriter _writeHandle;

        /// <summary>
        /// A simple handle for reading a conf file as a Stream
        /// </summary>
        private FileStream _readHandle;

        private void WriteFile()
        {
            XmlSerializer myCerealizer = new XmlSerializer(typeof(List<ConfigEntry>));
            try
            {
                _writeHandle = new StreamWriter(_filePath);
                myCerealizer.Serialize(_writeHandle, _entries);
            }
            catch (Exception)
            {
                Log(Logger.LogTarget.File, "Some error occured, check the logs...");
            }
            finally
            {
                _writeHandle.Close();
            }
        }

        private bool ReadFile()
        {
            XmlSerializer myCerealizer = new XmlSerializer(typeof(List<ConfigEntry>));
            _readHandle = new FileStream(_filePath, FileMode.Open);
            try
            {
                _entries = (List<ConfigEntry>)myCerealizer.Deserialize(_readHandle);
                return true;
            }
            catch (Exception)
            {
                Log(Logger.LogTarget.File, "Something went wrong when trying to read the file");
                return false;
            }
            finally
            {
                _readHandle.Close();
            }
        }
    }
}