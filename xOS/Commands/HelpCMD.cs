﻿using System;
using System.Collections.Generic;
using System.Text;

namespace xOS.Commands
{
    /// <summary>
    /// Help command displats the xOS commands that can be used.
    /// ex: info
    /// </summary>
    public static class HelpCMD
    {
        /// <summary>
        /// info command
        /// </summary>
        public static void RunHelpCMD()
        {
                Console.WriteLine(@"
List if commands that can be used on xOS:
    info - Displays this message
    
------File/Directory Commands------

    mkdir - Creates a directory in a specific path. Ex: mkdir [PATH]\[DirName]
    rmdir - Deletes a directory from a specific path with all the contet in it. Ex: rmdir [PATH]\[DirName]

    mf - Creates a file in a specific path. Ex: mf [PATH]\[FileName]
    rf - Deletes a file from a specific path. Ex: rf [PATH]\[FileName]
    df - Displays content of a file from a specific path in console. Ex: df [PATH]\[FileName]
    wf - Writes data to a file in a specific path with overwrite. Ex: wf [PATH]\[FileName]
    af - Appends data to a file in a specific path with overwrite. Ex: af [PATH]\[FileName] input_data
    fcopy - Copy a file to a certain location. Ex: fcopy [PATH]\[FileName] [PATH]\[FileName]
    fmove - Moves a file to a certain location. Ex: fmove [PATH]\[FileName] [PATH]\[FileName]

-----------------------------------");


                Console.WriteLine("Press any key for continue reading...");
                Console.ReadKey();
                Console.Clear();

                Console.WriteLine(@"                   
------System Commands------

    ls - Displays folders and files from current location
    cd - Go to other directory location path (Work in progress)
    clear - Clears the console info
    restart - Restarts the OS
    shutdow - Shutdown the OS
    logout - Logs out the current loged user
    time - Displays the current time

------------------------------------
------User Management-------

    cuser - Creates a OS user. Adminitrator user required
    duser - Deletes a OS user. Adminitrator user required

------------------------------------
");
            
        }
    }
}
