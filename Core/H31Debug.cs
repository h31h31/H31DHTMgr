//======================================================================
//Copyright (C) 2008 ARTM
//All rights reserved

//Filename :H31Debug
//Created by Miao at  12/08/2008 15:34:34
//Description :输出调试信息和错误信息
//======================================================================
using System;
using System.IO;

using System.Runtime.InteropServices;

namespace H31DHTMgr
{
        public static class H31Debug
        {
            public static void Print(string format, params object[] args)
            {
                string msg = GetMessage(format, args);
                DateTime dt = System.DateTime.Now.ToLocalTime();

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Gray;
                if (_newLine)
                {
                    string str = string.Format("[{0}]: ", dt);
                    Console.Write(str);
                    WriteToLogFile(str);
                    _newLine = false;
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write(msg);
                WriteToLogFile(msg);
                Console.ForegroundColor = color;
            }

            public static void PrintLn(string format, params object[] args)
            {
                Print(format, args);
                WriteToLogFile("\r\n");
                Console.WriteLine();
                _newLine = true;
            }

            public static void Error(string format, params object[] args)
            {
                string msg = GetMessage(format, args);
                DateTime dt = System.DateTime.Now.ToLocalTime();

                ConsoleColor color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Red;
                if (!_newLine)
                {
                    WriteToLogFile("\r\n");
                    Console.WriteLine();
                }
                string str = string.Format("[{0}]: {1}", dt, msg);
                WriteToLogFile(str + "\r\n");
                Console.WriteLine(str);
                Console.ForegroundColor = color;
                _newLine = true;
            }

            private static string GetMessage(string format, object[] args)
            {
                if (args.Length > 0)
                {
                    return string.Format(format, args);
                }
                else
                {
                    return format;
                }
            }

            private static void WriteToLogFile(string str)
            {
                if (!WriteToFile)
                {
                    return;
                }
                if (string.IsNullOrEmpty(str))
                {
                    return;
                }

                str = str.Replace("\b", "");
                StreamWriter sw = new StreamWriter(LogFile, true);
                sw.Write(str);
                sw.Flush();
                sw.Close();
            }

            public static string LogFile = "";
            public static string c_LogFile = "log" + DateTime.Today.ToString("yyyy-MM-dd-hh") + ".txt";
            private static bool _newLine = true;
            public static bool WriteToFile = true;
        }

}
