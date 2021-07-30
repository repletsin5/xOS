using System;
using xOS.FileSystem;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using static xOS.FileSystem.GVariables;
using Cosmos.HAL;

namespace xOS.Commands
{
    class CommandHandler
    {
        private static string dLetter = @"0:\";
        void HandleInput(string input)
        {
            string[] splitInput = input.Split((char)32); //32 is a space character

            switch (splitInput[0])
            {
                case "help":
                    HelpCMD.RunHelpCMD();
                    break;
                case "mkdir":
                    FileSystem.DirectoryM.CreateDir(input);
                    break;
                case "rmdir":
                    FileSystem.DirectoryM.DeleteDir(input);
                    break;
                case "mf":
                    FileM.MoveFile(input);
                    break;
                case "rf":
                    FileM.Read_File(input);
                    break;
                case "wf":
                    FileM.Write_To_File(input);
                    break;
                case "af":
                    FileM.Append_To_File(input);
                    break;
                case "fcopy":
                    FileM.CopyFile(input);
                    break;
                case "fmove":
                    FileM.MoveFile(input);
                    break;
                case "user":
                    if (splitInput[1] == "create")
                        UsrCMDS.CreateUser();
                    if (splitInput[1] == "delete")
                        UsrCMDS.DeleteUser(input);
                        break;
                // system commands
                case "ls":
                    if (input.Length == 2)
                    {
                        Root.Test_Root();
                    }
                    else
                    {
                        input = input.Split(' ')[1];
                        Root.Test_Root(input);
                    }
                    break;
                case "clear":
                    Console.Clear();
                    Console.WriteLine("--------------------Welcome to xOS----------------------");
                    break;
                case "shutdown":
                    Console.WriteLine("xOS is shuting down!");
                    CLog.CLog.SysLog_LoadOS("xOS is shuting down!");
                    Thread.Sleep(1500);
                    Cosmos.System.Power.Shutdown();
                    break;
                case"reboot":
                    // WIP
                    Console.WriteLine("xOS is restarting!");
                    CLog.CLog.SysLog_LoadOS("xOS is restarting!");
                    Thread.Sleep(1500);
                    Cosmos.System.Power.Reboot();
                    break;
                case "cd":
                    try
                    {
                        string DirPath = input.Split(' ')[1];
                        string DirPathSaved = File.ReadAllText(cDirFile);

                        if (string.IsNullOrEmpty(DirPathSaved))
                        {

                            if (System.IO.Directory.Exists(DirPath))
                            {
                                if (DirPath.Contains(dLetter))
                                {
                                    File.WriteAllText(cDirFile, DirPath);
                                }
                                else
                                {
                                    File.WriteAllText(cDirFile, dLetter + DirPath);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Direcotry {DirPath} does not exist!");
                            }
                        }
                        else
                        {
                            if (System.IO.Directory.Exists(DirPathSaved + @"\" + DirPath))
                            {
                                if (DirPath.Contains(dLetter))
                                {
                                    File.WriteAllText(cDirFile, DirPath);
                                }
                                else
                                {
                                    File.WriteAllText(cDirFile, DirPathSaved + @"\" + DirPath);
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Direcotry {DirPath} does not exist!");
                            }
                        }
                    }
                    catch
                    {
                        File.WriteAllText(cDirFile, string.Empty);
                    }
                    break;
                case "logout":
                    File.WriteAllText(LoginFile, "0");
                    Console.Clear();
                    break;
                case"time":
                    string d = RTC.DayOfTheMonth.ToString();
                    string y = RTC.Year.ToString();
                    string mo = RTC.Month.ToString();
                    string h = RTC.Hour.ToString();
                    string m = RTC.Minute.ToString();
                    string s = RTC.Second.ToString();
                    Console.WriteLine($"Date: {d}-{mo}-{y} / Time: {h}:{m}:{s}");
                    break;
                default:
                    Console.WriteLine($"Unkown command {splitInput[0]}");
                    break;
            }
        }
    }
}
