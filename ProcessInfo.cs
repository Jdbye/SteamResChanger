/***********************************************************************************************
 * File Name	: ProcessInfo.cs
 * Description	: This class demonstrates the use of WMI.
 *				  It provides a static method to query the list of running processes.
 *				  And it provides two events to delegate when an application has been started.
 * 
 * Author		: Asghar Panahy
 * Date			: 26-oct-2005
 ***********************************************************************************************/
using System;
using System.Data;
using System.Management;

namespace Win32Process
{
    /// <summary>
    /// ProcessInfo class.
    /// </summary>
    public class ProcessInfo
    {
        // definition of the delegates
        public delegate void StartedEventHandler(object sender, EventArrivedEventArgs e);
        public delegate void TerminatedEventHandler(object sender, EventArrivedEventArgs e);

        // events to subscribe
        public event StartedEventHandler? Started = null;
        public event TerminatedEventHandler? Terminated = null;

        // WMI event watcher
        private ManagementEventWatcher watcher;

        // The constructor uses the application name like notepad.exe
        // And it starts the watcher
        public ProcessInfo(string appName)
        {
            // query every 1 seconds
            string pol = "1";

            string queryString =
                "SELECT *" +
                "  FROM __InstanceOperationEvent " +
                "WITHIN  " + pol +
                " WHERE TargetInstance ISA 'Win32_Process' " +
                "   AND TargetInstance.Name = '" + appName + "'";

            // You could replace the dot by a machine name to watch to that machine
            string scope = @"\\.\root\CIMV2";

            // create the watcher and start to listen
            watcher = new ManagementEventWatcher(scope, queryString);
            watcher.EventArrived += new EventArrivedEventHandler(this.OnEventArrived);
            watcher.Start();
        }

        public void Dispose()
        {
            Started = null;
            Terminated = null;
            watcher.Stop();
            watcher.Dispose();
        }

        private void OnEventArrived(object sender, System.Management.EventArrivedEventArgs e)
        {
            try
            {
                string eventName = e.NewEvent.ClassPath.ClassName;

                if (eventName.CompareTo("__InstanceCreationEvent") == 0)
                {
                    // Started
                    Started?.Invoke(this, e);
                }
                else if (eventName.CompareTo("__InstanceDeletionEvent") == 0)
                {
                    // Terminated
                    Terminated?.Invoke(this, e);

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }
        }

    }
}
