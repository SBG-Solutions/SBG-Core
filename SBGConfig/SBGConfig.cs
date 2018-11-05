using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static SBGCore.LogHelper;

namespace SBGCore
{
    /// <summary>
    /// SBGCore.SBGConfig class
    /// </summary>
    public class SBGConfig
    {
        /// <summary>
        /// This allows "switch/case-ing" for the type of a variable
        /// </summary>
        /// <param name="tests"></param>
        /// <returns></returns>
        private Action<object> Switch(params Func<object, Action>[] tests)
        {
            return o =>
            {
                tests
                .Select(f => f(o))
                .FirstOrDefault(a => a != null)?.Invoke();
            };
        }

        /// <summary>
        /// This generates the resulting function that gets created for each switch/case
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        private Func<object, Action> Case<T>(Action<T> action)
        {
            return o => o is T ? (Action)(() => action((T)o)) : (Action)null;
        }

        /// <summary>
        /// This is how each and every config entry is composed
        /// </summary>
        [Serializable]
        public struct ConfigEntry
        {
            private int _id;

            /// <summary>
            /// Integer value for the ID
            /// </summary>
            public int Id { get => _id; set => _id = value; }

            private string _key;

            /// <summary>
            /// The key to the key/value pair of a config setting
            /// </summary>
            public string Key { get => _key; set => _key = value; }

            private ConfigTypes _type;

            /// <summary>
            /// What type of configuration are we?
            /// </summary>
            /// <seealso cref="ConfigTypes"/>
            internal ConfigTypes Type { get => _type; set => _type = value; }

            private object _value;

            /// <summary>
            /// Whats the value of this setting
            /// </summary>
            public object Value { get => _value; set => _value = value; }
        }

        /// <summary>
        /// The Different config entry type
        /// </summary>
        public enum ConfigTypes
        {
            /// <summary>
            /// Any Int32 value
            /// </summary>
            ConfigInt,

            /// <summary>
            /// A 64bit float
            /// </summary>
            ConfigFloat,

            /// <summary>
            /// A normal string
            /// </summary>
            ConfigString,

            /// <summary>
            /// A true or false value
            /// </summary>
            ConfigBool
        }

        /// <summary>
        /// Where are we storing the config xml?
        /// </summary>
        /// <remarks>Currently we are simply storing it right next the calling exe of this dll</remarks>
        public static string _filePath;

        /// <summary>
        /// A list of config entries
        /// </summary>
        public static List<ConfigEntry> _entries = new List<ConfigEntry>();

        /// <summary>
        /// This command factory allows for injecting commands into the App
        /// </summary>
        private CommandFactory cmd = new CommandFactory();

        private FileHandler fh = new FileHandler();

        /// <summary>
        /// The constuctor creates and sets up this instance of SBGConfig
        /// </summary>
        /// <param name="owner">String indicating who owns this config instance</param>
        public SBGConfig(string owner)
        {
            bool _success;
            C0re.OwnerName = owner;
            _filePath = SBGCore.C0re.CoreDirectory + "\\" + owner + ".conf";
            //This is to clear the existing log
            Log(Logger.LogTarget.File, "");
            // Add an UpdateCommand to the CommandFactory
            //var cmd = new CommandFactory();
            cmd.RegisterCommand("UpdateFile", UpdateFile);

            //Here we try to get a handle on the file and indicate the success
            try
            {
                fh._readHandle = new FileStream(_filePath, FileMode.Open);
                Log(Logger.LogTarget.File, "This should normally result in a successful handle on an xml config file near this dll");
                Log(Logger.LogTarget.File, "In this case it was '" + _filePath + "' and I was able to open it");
                _success = true;
                SetConfig("SBGConfig [" + owner + "] created", _success);
            }
            catch (Exception)
            {
                var s = "The file '" + _filePath + "' could not be found";
                _success = false;
                Log(Logger.LogTarget.File, s);
                SetConfig("SBGConfig [" + owner + "] created", _success);
            }
            finally
            {
                //Just to make sure we're freeing up the file
                if (fh._readHandle != null)
                {
                    fh._readHandle.Close();
                }
            }
            cmd.ExecuteCommand("UpdateFile");
        }

        private void UpdateFile()
        {
            if (_entries.Count == 0)
            {
                fh.ReadFile();
            }
            else
            {
                fh.WriteFile();
            }
        }

        /// <summary>
        /// This guy check the type of variable that _value contains and set the via ref passed in type accordingly
        /// </summary>
        /// <param name="_key">The key of the value to be retrieved</param>
        /// <param name="_value">The value to be checked for its variable type</param>
        /// <param name="_entryType">This sets the entryType via reference from the calling function</param>
        private void SetType(string _key, object _value, ref ConfigTypes _entryType)
        {
            var _t = _entryType;
            var @switch = Switch(new[]
            {
                Case<int>(x=>
                {
                    _t=ConfigTypes.ConfigInt;
                }),
                Case<String>(x=>
                {
                    _t=ConfigTypes.ConfigString;
                }),
                Case<Boolean>(x=>
                {
                    _t=ConfigTypes.ConfigBool;
                }),
                Case<float>(x =>
                {
                    _t=ConfigTypes.ConfigFloat;
                }),
            });
            @switch(_value);
            _entryType = _t;
        }

        /// <summary>
        /// Write configuration
        /// </summary>
        public void WriteConfig()
        {
            fh.WriteFile();
        }

        /// <summary>
        /// Read configuration
        /// </summary>
        public void ReadConfig()
        {
            fh.ReadFile();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="_key"></param>
        /// <param name="_value"></param>
        //todo(smzb): Trigger the appropriate XML write response here
        public void SetConfig(string _key, object _value)
        {
            var index = _entries.Count;
            var entry = new ConfigEntry { };
            var type = entry.Type;
            SetType(_key, _value, ref type);
            if (!_entries.Exists(x => x.Key == _key))
            {
                entry = new ConfigEntry { Id = index, Key = _key, Type = type, Value = _value };
                _entries.Add(entry);
            }
            else
            {
                entry = _entries.Find(x => x.Key == _key);
                entry.Value = _value;
            }
            // XML implementation
        }

        /// <summary>
        /// The reads the appropriate key value pair here from entries here
        /// </summary>
        /// <param name="_key">The key to read</param>
        /// <returns>The object containing the appropriate typed value</returns>
        //todo(smzb): Trigger the appropriate XML read response here
        public object GetConfig(string _key)
        {
            if (!_entries.Exists(x => x.Key == _key))
            {
                Log(Logger.LogTarget.File, "There is no such config entry \"" + _key + "\" please try again...");
                throw new Exception("There is no such config entry \"" + _key + "\" please try again...");
            }
            else
            {
                return _entries.Find(x => x.Key == _key).Value;
            }
        }
    }
}
