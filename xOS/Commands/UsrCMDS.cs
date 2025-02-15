﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.IO;
using xOS.FileSystem;
namespace xOS.Commands
{
    public static class UsrCMDS

    {
        private static string UsrFile = GVariables.UsrFile;
        private static string LoginFile = GVariables.LoginFile;


        public static void CreateUser()
        {
            try
            {

                //we check if user file exists
                if (File.Exists(UsrFile))
                {
                    string UserAdmin = GetUserType(UsrFile, LoginFile);

                    if (UserAdmin == "a")
                    {
                        Console.Write("User Name: ");
                        string UserName = Console.ReadLine();
                        Console.Write("User Type (a - Administrator, u - Normal User): ");
                        string UserType = Console.ReadLine();
                        Console.Write("User Password: ");
                        string UserPass = Users.GetHiddenConsoleInput();
                        Console.WriteLine("\n");
                        string UsrFileRead;

                        UsrFileRead = File.ReadAllText(UsrFile);

                        //we check if user exists in file
                        if (UsrFileRead.Contains(UserName))
                        {
                            Console.WriteLine($"User {UserName}, already exist!");
                        }
                        else
                        {
                            File.AppendAllText(UsrFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|{UserType}\n");
                            Console.WriteLine($"Created user: {UserName}");
                            CLog.CLog.SysLog_LoadOS($"Created user: {UserName}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Your account is not Administrator!");
                    }
                }
                else
                {
                    Console.Write("User Name: ");
                    string UserName = Console.ReadLine();
                    Console.Write("User Password: ");
                    string UserPass = Users.GetHiddenConsoleInput();
                    Console.WriteLine("\n");
                    string UsrFileRead;

                    //we initialize the users file
                    File.Create(UsrFile);
                    CLog.CLog.SysLog_LoadOS($"Users file (usr.u) is initialized!");
                    UsrFileRead = File.ReadAllText(UsrFile);

                    //we check if user exists in file
                    if (UsrFileRead.Contains(UserName))
                    {
                        Console.WriteLine($"User {UserName}, already exist!");
                    }
                    else
                    {
                        File.AppendAllText(UsrFile, $"{UserName}|{Cryptography.Encrypt(UserPass)}|a\n");
                        Console.WriteLine($"Created user: {UserName}");
                        CLog.CLog.SysLog_LoadOS($"Created user: {UserName}");
                    }
                }

            }
                catch (Exception E)
            {
                Console.WriteLine(E.Message);
            }
        }
    
        public static void DeleteUser(string input)
        {
            try
            {
                if (File.Exists(UsrFile))
                {
                    string UserAdmin = GetUserType(UsrFile, LoginFile);
                    if (UserAdmin == "a")
                    {
                        string dUser = input.Split(' ')[2];
                        string uList = string.Empty;
                        var ReadUsers = File.ReadAllLines(UsrFile);
                        foreach (var User in ReadUsers)
                        {
                            if (!User.Contains(dUser) && User.Length > 0)
                            {
                                uList += User + Environment.NewLine;
                            }
                        }
                        File.WriteAllText(UsrFile, uList);
                        CLog.CLog.SysLog_LoadOS($"User {dUser} was deleted!");
                        Console.WriteLine($"User {dUser} was deleted!");
                    }
                    else
                    {
                        Console.WriteLine($"Your account is not Administrator!");
                    }
                }
                else
                {
                    Console.WriteLine("User file does not exist!");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    

        /// <summary>
        /// Get the type of the current user logged in 
        /// </summary>
        /// <param name="UserFile">Specify the user.u path</param>
        /// <param name="LoginFile">Specify the login.t path</param>
        /// <returns></returns>
        private static string GetUserType(string UserFile, string LoginFile)
        {
            string[] UsersList = File.ReadAllLines(UserFile);
            string LogedUser = File.ReadAllText(LoginFile).Split('|')[1];
            string UserAdmin = string.Empty;
            foreach (var user in UsersList)
            {
                if (user.Contains(LogedUser))
                {
                    UserAdmin = user.Split('|')[2].ToLower();
                }
            }
            return UserAdmin;
        }
    }
}
