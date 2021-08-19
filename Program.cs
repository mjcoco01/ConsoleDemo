using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationCore.MultiValueDictionary multiValueDictionary = new ApplicationCore.MultiValueDictionary();
            while (true)
            {
                Console.Write("Please enter a command: ");
                string EnteredText = Console.ReadLine();

                if (EnteredText.ToLower() == "exit") return;

                RegexOptions options = RegexOptions.None;
                Regex regex = new Regex("[ ]{2,}", options);
                var _splitString = ReturnSplitString(regex.Replace(EnteredText, " ").Trim());

                switch (_splitString[0])
                {
                    case "keys":
                        multiValueDictionary.DisplayKeys();
                        break;
                    case "members":
                        if (_splitString.Count() == 2)
                        {
                            multiValueDictionary.DisplayMembers(_splitString[1].Replace("\"", ""));
                        }
                        else
                        {
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "add":
                        if (_splitString.Count() == 3)
                        {
                            multiValueDictionary.AddData(_splitString[1].Replace("\"", ""), _splitString[2].Replace("\"", ""));
                        }
                        else
                        {
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "remove":
                        if (_splitString.Count() == 3)
                        {
                            multiValueDictionary.RemoveValue(_splitString[1].Replace("\"", ""), _splitString[2].Replace("\"", ""));
                        }
                        else
                        {
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "removeall":
                        if (_splitString.Count() == 2)
                        {
                            multiValueDictionary.RemoveAll(_splitString[1].Replace("\"", ""));
                        }
                        else
                        { 
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "clear":
                        multiValueDictionary.RemoveAll();
                        break;
                    case "keyexists":
                        if (_splitString.Count() == 2)
                        {
                            Console.WriteLine(multiValueDictionary.KeyExists(_splitString[1].Replace("\"", "")));
                        }
                        else
                        {
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "memberexists":
                        if (_splitString.Count() == 3)
                        {
                            Console.WriteLine(multiValueDictionary.MemberExists(_splitString[1].Replace("\"", ""), _splitString[2].Replace("\"", "")));
                        }
                        else
                        {
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "leavemember":
                        if (_splitString.Count() == 3)
                        {
                            multiValueDictionary.LeaveMember(_splitString[1].Replace("\"", ""), _splitString[2].Replace("\"", ""));
                        }
                        else
                        {
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "leavekey":
                        if (_splitString.Count() == 2)
                        {
                            multiValueDictionary.LeaveKey(_splitString[1].Replace("\"", ""));
                        }
                        else
                        {
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "leaveonly":
                        if (_splitString.Count() == 3)
                        {
                            multiValueDictionary.LeaveMember(_splitString[1].Replace("\"", ""), _splitString[2].Replace("\"", ""));
                            multiValueDictionary.LeaveKey(_splitString[1].Replace("\"", ""));
                        }
                        else
                        {
                            CommandNotProperlyFormed();
                        }
                        break;
                    case "allmembers":
                        multiValueDictionary.DisplayMembers();
                        break;
                    case "items":
                        multiValueDictionary.DisplayItems();
                        break;
                    case "help":
                        GetHelp();
                        break;
                    case "clearconsole":
                        Console.Clear();
                        break;
                    case "populatedata":
                        multiValueDictionary.PopulateData();
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid command. If you are having trouble type Help.");
                        Console.ResetColor();
                        break;

                }

            }
        }


        private static void GetHelp()
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
            Console.WriteLine("Use commands by following the pattern below");
            Console.WriteLine("POPULATEDATA            - populate key member with data");
            Console.WriteLine("KEYS                    - return all the keys in the dictionary");
            Console.WriteLine("MEMBERS key             - returns the collection strings for the given key");
            Console.WriteLine("ADD key member          - adds a member to the collection for a given key value pair");
            Console.WriteLine("ADD \"key word\" member   - adds a member to the collection for a given key value pair with spaces");
            Console.WriteLine("REMOVE key member       - removes a member from a key");
            Console.WriteLine("REMOVEALL key           - removes all members from a key");
            Console.WriteLine("CLEAR                   - removes all keys and members");
            Console.WriteLine("KEYEXISTS key           - check to see if key exists");
            Console.WriteLine("MEMBEREXISTS key member - check to see if member exists");
            Console.WriteLine("ALLMEMBERS              - return all members in the dictionary");
            Console.WriteLine("ITEMS                   - return all key/members in the dictionary");
            Console.WriteLine("CLEARCONSOLE            - return all key/members in the dictionary");
            Console.WriteLine("LEAVEMEMBER             - remove all members from key except for member specified");
            Console.WriteLine("LEAVEKEY                - remove all keys except for key specified");
            Console.WriteLine("LEAVEONLY               - remove all keys and members except for key member specified");
            Console.WriteLine("EXIT                    - to quit the console application");
            Console.WriteLine("--------------------------------------------------------------------------------------------------");
        }


        private static void CommandNotProperlyFormed()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You did not properly form your command.");
            Console.WriteLine("Type \"help\" if you need assistance");
            Console.ResetColor();
        }

        private static string[] ReturnSplitString(string text)
        {
            return Regex.Split(text, " (?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
        }
    }
}
