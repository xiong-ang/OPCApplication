using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;

namespace Client
{
    public static class Program
    {
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        // Set entry point to handle single instance of the application.
        [STAThread]
        public static void Main()
        {
            Process instance = GetRunningInstance();
            if (instance == null)
            //If no other application instance in running
            {
                App app = new App();
                app.InitializeComponent();
                app.Run();
            }
            else
            {
                MessageBox.Show("A program instance is already running！","Message");
                SetForegroundWindow(instance.MainWindowHandle);     //Set the window to foreground
            }
        }

        //Get another running process instance
        private static Process GetRunningInstance()
        {  
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id && process.MainModule.FileName == current.MainModule.FileName)
                    return process;
            }
            return null;
        }
        

        /*
        private static Mutex mutex;

        // Set entry point to handle single instance of the application.
        [STAThread]
        public static void Main()
        {
            mutex = new Mutex(true, "OnlyRun");
            if (mutex.WaitOne(0, false))
            {
                App app = new App();
                app.InitializeComponent();
                app.Run();
            }
            else
            {
                MessageBox.Show("A program instance is already running！");
            }
        }
        */
    }
}
