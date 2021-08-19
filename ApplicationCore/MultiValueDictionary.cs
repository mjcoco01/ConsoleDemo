using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleDemo.ApplicationCore
{
    public class MultiValueDictionary
    {
        private Dictionary<string, List<string>> _data = new Dictionary<string, List<string>>();

        /// <summary>
        /// Add data to the multi value dictionary
        /// </summary>
        /// <param name="key">Key to add/check</param>
        /// <param name="value">Member to add</param>
        public void AddData(string key, string value)
        {          
            try
            {
                if (KeyExists(key))
                {

                    if (MemberExists(key, value))
                    {
                        WriteConsoleError("ERROR, member already exists");
                    }
                    else
                    {
                        _data[key].Add(value);
                        WriteConsoleInfo("Added");
                    }
                }
                else
                {
                    _data.Add(key, new List<string>() { value });
                    WriteConsoleInfo("Added");
                }

            }
            catch (ArgumentException e)
            {
                WriteConsoleError(key + ":" + value + " is already in the Dictionary");
            }
            catch(Exception e)
            {
                WriteConsoleError(e.Message.ToString());
            }
        }

        /// <summary>
        /// Check to see if the key exists in the dictionary
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <returns>Boolean value</returns>
        internal bool KeyExists(string key)
        {
            return _data.ContainsKey(key);
        }

        /// <summary>
        /// Check to see if a member of a key exists
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <param name="value">Member to check</param>
        /// <returns></returns>
        internal bool MemberExists(string key, string value)
        {
            if (KeyExists(key))
            {
                var valueFound = _data[key].Find(x => x.ToString() == value);
                if (valueFound == null)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        /// <summary>
        /// Display all keys and items
        /// </summary>
        internal void DisplayItems()
        {
            if (_data.Keys.Count == 0)
                WriteConsoleInfo("(empty set)");

            int x = 0;
            foreach (var key in _data.Keys)
            {
                foreach (var member in _data[key].ToList())
                {
                    Console.WriteLine(++x + ") " + key + ": " + member); 
                }
                
            }
        }

        /// <summary>
        /// Display all members
        /// </summary>
        internal void DisplayMembers()
        {
            if (_data.Keys.Count == 0)
                WriteConsoleInfo("(empty set)");

            int x = 0;
            foreach (var key in _data.Keys)
            {
                foreach (var value in _data[key].ToList())
                {
                    Console.WriteLine(++x + ") " + value);
                }
            }
        }

        /// <summary>
        /// Display members of a certain key
        /// </summary>
        /// <param name="key">KEY to retrieve</param>
        internal void DisplayMembers(string key)
        {
            if (!KeyExists(key))
            {
                WriteConsoleError("ERROR, key does not exist.");
            }
            else
            {
                int x = 0;
                foreach (var values in _data[key].ToList())
                {
                    Console.WriteLine(++x + ") " + values);
                }
            }
        }
        /// <summary>
        /// Leave only the member from the provided key
        /// </summary>
        /// <param name="key">To check</param>
        /// <param name="value">member to leave</param>
        internal void LeaveMember(string key, string value)
        {
            try
            {
                if (!KeyExists(key))
                {
                    WriteConsoleError("ERROR, key does not exist");
                    return;
                }
                foreach (string values in _data[key].Where(x => x.ToString() != value).ToList())
                {
                    _data[key].Remove(values);
                }
                WriteConsoleInfo("All members of " + key + " removed except " + value);
            }
            catch (Exception e)
            {
                WriteConsoleError("Unable to delete members");
            }
        }
        /// <summary>
        /// Remove all keys except the one specified
        /// </summary>
        /// <param name="key">Leave this key</param>
        internal void LeaveKey(string key)
        {
            try
            {
                if (!KeyExists(key))
                {
                    WriteConsoleError("ERROR, key does not exist");
                }
                foreach (string vKey in _data.Keys.Where(x => x.ToString() != key).ToList())
                {
                    _data.Remove(vKey);
                }
                WriteConsoleInfo("All keys removed except " + key);
            }
            catch(Exception e)
            {
                WriteConsoleError("Unable to delete keys");
            }
        }

        /// <summary>
        /// Populate dictionary with data
        /// </summary>
        internal void PopulateData()
        {
            try
            {
                AddData("foo", "bar");
                AddData("foo", "baz");
                AddData("foo", "zip");
                AddData("baz", "bang");
                AddData("baz", "bar");
                AddData("baz", "zip");
                AddData("boom", "pow");
                AddData("bang", "zip");
                AddData("bang", "bar");
                AddData("bang", "baz");
            }
            catch(Exception e)
            {
                WriteConsoleError(e.Message.ToString());
            }
        }

        /// <summary>
        /// Display keys currently in dictionary
        /// </summary>
        public void DisplayKeys()
        {
            if (_data.Keys.Count == 0)
            {
                WriteConsoleInfo("(empty set)");
            }
            int x = 0;
            foreach (var key in _data.Keys)
            {
                Console.WriteLine(++x + ") " + key);
            }
        }

        /// <summary>
        /// Remove value from multivalue dictionary
        /// </summary>
        /// <param name="key">Key to check</param>
        /// <param name="value">Member to remove</param>
        public void RemoveValue(string key, string value)
        {
            try
            {
                if (KeyExists(key))
                {
                    if (MemberExists(key, value))
                    {
                        _data[key].Remove(value);
                        WriteConsoleInfo(key + " " + value + " was removed");
                    }
                    else
                    {
                        WriteConsoleError("ERROR, member does not exist");
                    }
                    if (_data[key].Count == 0)
                    {
                        _data.Remove(key);
                    }
                }
                else
                {
                    WriteConsoleError("ERROR, member does not exist");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                WriteConsoleError("Index/Key not found in Dictionary");
            }
            catch(Exception e)
            {
                WriteConsoleError(e.Message);
            }
        }

        /// <summary>
        /// Remove key and all members from multivalue dictionary
        /// </summary>
        /// <param name="key">Pass the key to be removed</param>
        public void RemoveAll(string key)
        {
            try
            {
                if (KeyExists(key))
                {
                    _data.Remove(key);
                     WriteConsoleInfo(key + " key and members were removed");
                }
                else
                {
                    WriteConsoleError("ERROR, key does not exist");
                }
            }
            catch (ArgumentOutOfRangeException e)
            {
                WriteConsoleError("Index/Key not found in Dictionary");
            }
            catch (Exception e)
            {
                WriteConsoleError(e.Message);
            }
        }
        /// <summary>
        /// Remove all keys and members from multivalue dictionary
        /// </summary>
        public void RemoveAll()
        {
            foreach(string key in _data.Keys)
            {
                _data.Remove(key);
            }
            WriteConsoleInfo("Cleared");
        }


        private void WriteConsoleInfo(string Text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Text);
            Console.ResetColor();
        }
        private void WriteConsoleError(string Text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("---------" + Text);
            Console.ResetColor();
        }
    }
}
