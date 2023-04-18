using SharpDX.Direct3D11;
using System;
using Win32.Shared;
using System.Runtime.InteropServices;
using System.Windows;
using System.Diagnostics;

namespace WinRT.GraphicsCapture
{
    //winrtgraphicscapture
    public class Program

    //internal static class Program
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);


        /*public void startprogram()
        {
            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
            //AppDomain.CurrentDomain.

            //AppDomain.CurrentDomain.

            /*window = new DxWindow(".NET Window Capture Samples - WinRT.GraphicsCapture", new GraphicsCapture());
            //using var window = new DxWindow(".NET Window Capture Samples - WinRT.GraphicsCapture", new GraphicsCapture());
            window.Show();

            //Application.Current.Windows.


            var theprocess = Process.GetCurrentProcess();
        }
    */

        [STAThread]
        public static void Main()
        {
            /*AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
            //AppDomain.CurrentDomain.

            window = new DxWindow(".NET Window Capture Samples - WinRT.GraphicsCapture", new GraphicsCapture());
            //using var window = new DxWindow(".NET Window Capture Samples - WinRT.GraphicsCapture", new GraphicsCapture());
            window.Show();


            //Application.Current.Windows.


            var theprocess = Process.GetCurrentProcess();*/

            //theprocess.Disposed += ProcessDisposed;

            /*if (theprocess.)
            {

            }*/

        }

        static DxWindow window;

        /*static void ProcessDisposed(object sender, EventArgs e)
        {
            MessageBox((IntPtr)0, "ProcessDisposed", "sccsmsg", 0);
            if (window != null)
            {
                MessageBox((IntPtr)0, "window != null. " + window.device.Tag.ToString(), "sccsmsg", 0);
                window.Dispose();
                window = null;
            }
        }*/




        static void ProcessExitHandler(object sender, EventArgs e)
        {

            MessageBox((IntPtr)0, "ProcessExitHandler", "sccsmsg", 0);
            if (window != null)
            {
                //MessageBox((IntPtr)0, "window != null. " + window.device.Tag.ToString(), "sccsmsg", 0);

                /*if (window.device!= null)
                {
                    MessageBox((IntPtr)0, "window != null. " + window.device.DebugName, "sccsmsg", 0);
                }*/

                MessageBox((IntPtr)0, "disposing of the dxwindow", "sccsmsg", 0);

                window.Dispose();
                window = null;



            }
        }
    }
}