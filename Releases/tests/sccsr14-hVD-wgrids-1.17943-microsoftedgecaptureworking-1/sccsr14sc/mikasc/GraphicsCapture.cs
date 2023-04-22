using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;

using Windows.Graphics.Capture;
using Windows.Graphics.DirectX;
using Windows.Graphics.DirectX.Direct3D11;

using SharpDX.Direct3D11;
using SharpDX.DXGI;

using Win32.Shared;
using Win32.Shared.Interfaces;

using WinRT.GraphicsCapture.Interop;

using Device = SharpDX.Direct3D11.Device;

using sccs;
using WindowsInput;
using System.Threading;
using System.Diagnostics;

using System.Collections;
using System.Collections.Generic;
using System.Linq;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System;
using System.Diagnostics;
using System.Windows.Forms;

using SharpDX;
//using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
//using SharpDX.Windows;
using Buffer = SharpDX.Direct3D11.Buffer;
//using Device = SharpDX.Direct3D11.Device;
//using System.Windows.Threading;

using System;

//using Win32.Shared;
using System.Diagnostics;
using SharpDX.DXGI;

using SharpDX.Direct2D1;
using SharpDX.Mathematics;
using System.Runtime.InteropServices;

/*
using Jitter.LinearMath;
using sccs.scgraphics;
using sccs.sccore;
using sccs.scconsole;
using WindowsInput;*/
using System.Threading.Tasks;
//using System.Speech.Recognition;
//using System.Speech.Synthesis;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Globalization;
using System.Text;
using System;
using System.Windows;
//using System.Windows.Interop;

//using Win32.DwmThumbnail.Interop;
//using Win32.Shared;
//using System.Windows.Controls;
//using Win32.DesktopDuplication;
using System;

//using Win32.Shared;
//using Win32.Shared.Interfaces;
//using Win32.Shared.Interop;


using Win32;
using Win32.DwmSharedSurface;

using Jitter;
using Jitter.Dynamics;
using Jitter.LinearMath;

using sccs.scgraphics;
using sccs.sccore;
using sccs.scconsole;

using WindowsInput;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;



//using SharpDX;
using SharpDX.DirectInput;
using System.Threading;
using System.Runtime;
using System.Runtime.CompilerServices;
using sccs.scmessageobject;
using System.ComponentModel;
using Jitter.DataStructures;
using Jitter.Forces;
using System.Windows.Interop;
using SharpDX.Windows;


using sccsr14sc;

using System.Diagnostics;
using System.Runtime.InteropServices;

using SharpDX.RawInput;
using SharpDX.Multimedia;
using WinRT.GraphicsCapture;
using Win32.Shared.Interop;
using Win32.Shared;

using System.Runtime.Remoting.Messaging;
using Matrix = SharpDX.Matrix;


using System.Runtime.ConstrainedExecution;

using System.IO;
using VirtualKeyCode = WindowsInput.Native.VirtualKeyCode;


namespace WinRT.GraphicsCapture
{
    internal class GraphicsCapture : ICaptureMethod, IDisposable
    {




        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;

            public int Top;

            public int Right;

            public int Bottom;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct MonitorInfoEx
        {
            public int cbSize;
            public Rect rcMonitor;
            public Rect rcWork;
            public UInt32 dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szDeviceName;
        }


        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        protected static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("User32")]
        public static extern IntPtr MonitorFromWindow(IntPtr hWnd, int dwFlags);

        [DllImport("user32", EntryPoint = "GetMonitorInfo", CharSet = CharSet.Auto,
            SetLastError = true)]
        internal static extern bool GetMonitorInfoEx(IntPtr hMonitor, ref MonitorInfoEx lpmi);


        //https://stackoverflow.com/questions/32244415/how-to-check-whether-application-is-running-fullscreen-mode-on-any-screen
        protected static bool EnumTheWindows(IntPtr hWnd, IntPtr lParam)
        {
            const int MONITOR_DEFAULTTOPRIMARY = 1;
            var mi = new MonitorInfoEx();
            mi.cbSize = Marshal.SizeOf(mi);
            GetMonitorInfoEx(MonitorFromWindow(hWnd, MONITOR_DEFAULTTOPRIMARY), ref mi);

            Rect appBounds;
            GetWindowRect(hWnd, out appBounds);
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0 && IsWindowVisible(hWnd))
            {
                var sb = new StringBuilder(size);
                GetWindowText(hWnd, sb, size);

                if (sb.Length > 20)
                {
                    sb.Remove(20, sb.Length - 20);
                }

                int windowHeight = appBounds.Right - appBounds.Left;
                int windowWidth = appBounds.Bottom - appBounds.Top;

                int monitorHeight = mi.rcMonitor.Right - mi.rcMonitor.Left;
                int monitorWidth = mi.rcMonitor.Bottom - mi.rcMonitor.Top;

                bool fullScreen = (windowHeight == monitorHeight) && (windowWidth == monitorWidth);

                sb.AppendFormat(" Wnd:({0} | {1}) Mtr:({2} | {3} | Name: {4}) - {5}", windowWidth, windowHeight, monitorWidth, monitorHeight, mi.szDeviceName, fullScreen);

                Console.WriteLine(sb.ToString());
            }
            return true;
        }

        protected delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);












        //const uint SW_SHOW = 5;
        private const int SW_SHOW = 9;

        private const int SW_RESTORE = 9;
        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        /*
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);
        */
        //https://stackoverflow.com/questions/9503027/p-pnvoke-setfocus-to-a-particular-control
        [DllImport("kernel32.dll")]
        static extern uint GetCurrentThreadId();
        [DllImport("kernel32.dll")]
        private static extern IntPtr GetCurrentThread();
        [DllImport("user32.dll")]
        static extern bool AttachThreadInput(uint idAttach, uint idAttachTo,
   bool fAttach);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool BringWindowToTop(HandleRef hWnd);
        /// <summary>
        ///     Brings the thread that created the specified window into the foreground and activates the window. Keyboard input is
        ///     directed to the window, and various visual cues are changed for the user. The system assigns a slightly higher
        ///     priority to the thread that created the foreground window than it does to other threads.
        ///     <para>See for https://msdn.microsoft.com/en-us/library/windows/desktop/ms633539%28v=vs.85%29.aspx more information.</para>
        /// </summary>
        /// <param name="hWnd">
        ///     C++ ( hWnd [in]. Type: HWND )<br />A handle to the window that should be activated and brought to the foreground.
        /// </param>
        /// <returns>
        ///     <c>true</c> or nonzero if the window was brought to the foreground, <c>false</c> or zero If the window was not
        ///     brought to the foreground.
        /// </returns>
        /// <remarks>
        ///     The system restricts which processes can set the foreground window. A process can set the foreground window only if
        ///     one of the following conditions is true:
        ///     <list type="bullet">
        ///     <listheader>
        ///         <term>Conditions</term><description></description>
        ///     </listheader>
        ///     <item>The process is the foreground process.</item>
        ///     <item>The process was started by the foreground process.</item>
        ///     <item>The process received the last input event.</item>
        ///     <item>There is no foreground process.</item>
        ///     <item>The process is being debugged.</item>
        ///     <item>The foreground process is not a Modern Application or the Start Screen.</item>
        ///     <item>The foreground is not locked (see LockSetForegroundWindow).</item>
        ///     <item>The foreground lock time-out has expired (see SPI_GETFOREGROUNDLOCKTIMEOUT in SystemParametersInfo).</item>
        ///     <item>No menus are active.</item>
        ///     </list>
        ///     <para>
        ///     An application cannot force a window to the foreground while the user is working with another window.
        ///     Instead, Windows flashes the taskbar button of the window to notify the user.
        ///     </para>
        ///     <para>
        ///     A process that can set the foreground window can enable another process to set the foreground window by
        ///     calling the AllowSetForegroundWindow function. The process specified by dwProcessId loses the ability to set
        ///     the foreground window the next time the user generates input, unless the input is directed at that process, or
        ///     the next time a process calls AllowSetForegroundWindow, unless that process is specified.
        ///     </para>
        ///     <para>
        ///     The foreground process can disable calls to SetForegroundWindow by calling the LockSetForegroundWindow
        ///     function.
        ///     </para>
        /// </remarks>
        // For Windows Mobile, replace user32.dll with coredll.dll
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        public void SetFocus(IntPtr hwndTarget, string childClassName)
        {

            IntPtr targetThreadID = GetWindowThreadProcessId(hwndTarget, IntPtr.Zero); //target thread id
            IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id
            try
            {
                IntPtr hFocus = IntPtr.Zero;
                IntPtr hFore;
                uint id = 0;
                hFore = GetForegroundWindow();
                var idAttach = GetWindowThreadProcessId(hFore, out id);
                var curThreadId = GetCurrentThreadId();
                // To attach to current thread
                bool lRet = AttachThreadInput(idAttach, curThreadId, true);
                // To dettach from current thread
                //AttachThreadInput(idAttach, curThreadId, false);


                // if it's not already in the foreground...
                lRet = BringWindowToTop(hwndTarget);
                SetForegroundWindow(hwndTarget);


                // if you know the child win class name do something like this (enumerate windows using Win API again)...
                /*var hwndChild = EnumAllWindows(_hWndTarget, childClassName).FirstOrDefault();

                if (_hWndChild == IntPtr.Zero)
                {
                    // or use keyboard etc. to focus, i.e. send keys/input...
                    // SendInput (...);
                    return;
                }
                */
                // you can use also the edit control's hwnd or some child window (of target) here
                sccsr14sc.Form1.SetFocus(hwndTarget); // hwndTarget);

            }
            finally
            {
                IntPtr hFocus = IntPtr.Zero;
                IntPtr hFore;
                uint id = 0;
                hFore = GetForegroundWindow();
                var idAttach = GetWindowThreadProcessId(hFore, out id);
                var curThreadId = GetCurrentThreadId();

                // To dettach from current thread
                AttachThreadInput(idAttach, curThreadId, false);


            }




            // hwndTarget is the other app's main window 
            // ...
            /*IntPtr targetThreadID = GetWindowThreadProcessId(_hWndTarget, IntPtr.Zero); //target thread id
            IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id
            try
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, -1); // attach current thread id to target window

                // if it's not already in the foreground...
                lRet = BringWindowToTop(_hWndTarget);
                SetForegroundWindow(_hWndTarget);

                // if you know the child win class name do something like this (enumerate windows using Win API again)...
                var hwndChild = EnumAllWindows(_hWndTarget, childClassName).FirstOrDefault();

                if (_hWndChild == IntPtr.Zero)
                {
                    // or use keyboard etc. to focus, i.e. send keys/input...
                    // SendInput (...);
                    return;
                }

                // you can use also the edit control's hwnd or some child window (of target) here
                SetFocus(_hWndChild); // hwndTarget);
            }
            finally
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, 0); //detach from foreground window
            }*/
        }

        public void UnSetFocus(IntPtr hwndTarget, string childClassName)
        {

            //IntPtr targetThreadID = GetWindowThreadProcessId(_hWndTarget, IntPtr.Zero); //target thread id
            //IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id

            IntPtr hFocus = IntPtr.Zero;
            IntPtr hFore;
            uint id = 0;
            hFore = GetForegroundWindow();
            var idAttach = GetWindowThreadProcessId(hFore, out id);
            var curThreadId = GetCurrentThreadId();

            // To dettach from current thread
            AttachThreadInput(idAttach, curThreadId, false);




            // hwndTarget is the other app's main window 
            // ...
            /*IntPtr targetThreadID = GetWindowThreadProcessId(_hWndTarget, IntPtr.Zero); //target thread id
            IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id
            try
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, -1); // attach current thread id to target window

                // if it's not already in the foreground...
                lRet = BringWindowToTop(_hWndTarget);
                SetForegroundWindow(_hWndTarget);

                // if you know the child win class name do something like this (enumerate windows using Win API again)...
                var hwndChild = EnumAllWindows(_hWndTarget, childClassName).FirstOrDefault();

                if (_hWndChild == IntPtr.Zero)
                {
                    // or use keyboard etc. to focus, i.e. send keys/input...
                    // SendInput (...);
                    return;
                }

                // you can use also the edit control's hwnd or some child window (of target) here
                SetFocus(_hWndChild); // hwndTarget);
            }
            finally
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, 0); //detach from foreground window
            }*/
        }
        internal struct WINDOWINFO
        {
            public uint ownerpid;
            public uint childpid;
        }

        #region User32
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        /// <summary>
        /// Delegate for the EnumChildWindows method
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="parameter">Caller-defined variable; we use it for a pointer to our list</param>
        /// <returns>True to continue enumerating, false to bail.</returns>
        public delegate bool EnumWindowProc(IntPtr hWnd, IntPtr parameter);

        [DllImport("user32", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumChildWindows(IntPtr hWndParent, EnumWindowProc lpEnumFunc, IntPtr lParam);
        #endregion

        #region Kernel32
        public const UInt32 PROCESS_QUERY_INFORMATION = 0x400;
        public const UInt32 PROCESS_VM_READ = 0x010;

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool QueryFullProcessImageName([In] IntPtr hProcess, [In] int dwFlags, [Out] StringBuilder lpExeName, ref int lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr OpenProcess(
            UInt32 dwDesiredAccess,
            [MarshalAs(UnmanagedType.Bool)]
            Boolean bInheritHandle,
            Int32 dwProcessId
        );
        #endregion
        public static string GetProcessName(IntPtr hWnd)
        {
            string processName = null;

            //hWnd = GetForegroundWindow();

            if (hWnd == IntPtr.Zero)
                return null;

            uint pID;
            GetWindowThreadProcessId(hWnd, out pID);

            IntPtr proc;
            if ((proc = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, (int)pID)) == IntPtr.Zero)
                return null;

            int capacity = 2000;
            StringBuilder sb = new StringBuilder(capacity);
            QueryFullProcessImageName(proc, 0, sb, ref capacity);

            processName = sb.ToString(0, capacity);

            // UWP apps are wrapped in another app called, if this has focus then try and find the child UWP process
            if (System.IO.Path.GetFileName(processName).Equals("ApplicationFrameHost.exe"))
            {
                processName = UWP_AppName(hWnd, pID);
            }

            return processName;
        }
        #region Get UWP Application Name

        /// <summary>
        /// Find child process for uwp apps, edge, mail, etc.
        /// </summary>
        /// <param name="hWnd">hWnd</param>
        /// <param name="pID">pID</param>
        /// <returns>The application name of the UWP.</returns>
        private static string UWP_AppName(IntPtr hWnd, uint pID)
        {
            WINDOWINFO windowinfo = new WINDOWINFO();
            windowinfo.ownerpid = pID;
            windowinfo.childpid = windowinfo.ownerpid;

            IntPtr pWindowinfo = Marshal.AllocHGlobal(Marshal.SizeOf(windowinfo));

            Marshal.StructureToPtr(windowinfo, pWindowinfo, false);

            EnumWindowProc lpEnumFunc = new EnumWindowProc(EnumChildWindowsCallback);
            EnumChildWindows(hWnd, lpEnumFunc, pWindowinfo);

            windowinfo = (WINDOWINFO)Marshal.PtrToStructure(pWindowinfo, typeof(WINDOWINFO));

            IntPtr proc;
            if ((proc = OpenProcess(PROCESS_QUERY_INFORMATION | PROCESS_VM_READ, false, (int)windowinfo.childpid)) == IntPtr.Zero)
                return null;

            int capacity = 2000;
            StringBuilder sb = new StringBuilder(capacity);
            QueryFullProcessImageName(proc, 0, sb, ref capacity);

            Marshal.FreeHGlobal(pWindowinfo);

            return sb.ToString(0, capacity);
        }

        /// <summary>
        /// Callback for enumerating the child windows.
        /// </summary>
        /// <param name="hWnd">hWnd</param>
        /// <param name="lParam">lParam</param>
        /// <returns>always <c>true</c>.</returns>
        private static bool EnumChildWindowsCallback(IntPtr hWnd, IntPtr lParam)
        {
            WINDOWINFO info = (WINDOWINFO)Marshal.PtrToStructure(lParam, typeof(WINDOWINFO));

            uint pID;
            GetWindowThreadProcessId(hWnd, out pID);

            if (pID != info.ownerpid)
                info.childpid = pID;

            Marshal.StructureToPtr(info, lParam, true);

            return true;
        }
        #endregion





        /*
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);
        */

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("gdi32.dll")]

        static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);



        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern long GetWindowLongPtr(IntPtr hWnd, int nIndex);


        private static readonly Guid _graphicsCaptureItemIid = new Guid("79C3F95B-31F7-4EC2-A464-632EF5D30760");
        private Direct3D11CaptureFramePool _captureFramePool;
        private GraphicsCaptureItem _captureItem;
        private GraphicsCaptureSession _captureSession;


        public string capturedwindowname = "";

        public GraphicsCapture()
        {
            IsCapturing = false;
        }

        public bool IsCapturing { get; private set; }

        public void Dispose()
        {
            _captureSession?.Dispose();
            _captureSession = null;

            _captureFramePool?.Dispose();
            _captureFramePool = null;


            _captureItem = null;
            IsCapturing = false;
            factory = null;
            interop = null;


            if (texture2d != null)
            {
                texture2d.Dispose();
                texture2d = null;
            }

            if (surfaceDxgiInterfaceAccess != null)
            {
                surfaceDxgiInterfaceAccess.Dispose();
                surfaceDxgiInterfaceAccess = null;
            }
            /*if (picker != null)
            {
                picker.Dispose();
                //sccsr14sc.Form1.MessageBox((IntPtr)0, "", "sccsmsg", 0);
                picker = null;
            }*/

            if (winrtDevice != null)
            {
                winrtDevice.Dispose();
                winrtDevice = null;
            }


            hr = 0;



            if (pointer != IntPtr.Zero)
            {
                DeleteObject(pointer);
                GC.SuppressFinalize(pointer);
            }

            if (pResource != IntPtr.Zero)
            {
                DeleteObject(pResource);
                GC.SuppressFinalize(pResource);
            }




            capture = null;
            //this.Dispose();
            StopCapture();
        }


        public string SelectedTitle { get; set; }

        
        public IntPtr _hWnd { get; set; }

        //public IntPtr _hWnd;
        WindowPicker picker;
        IDirect3DDevice winrtDevice;
        uint hr;


        public int typeofwindowpicker = -1;
        public int fullscreen = -1;

        public void StartCapture(IntPtr hWnd, Device device) //, Factory factory
        {
            #region GraphicsCapturePicker version

            /*
            var capturePicker = new GraphicsCapturePicker();

            // ReSharper disable once PossibleInvalidCastException
            // ReSharper disable once SuspiciousTypeConversion.Global
            var initializer = (IInitializeWithWindow)(object)capturePicker;
            initializer.Initialize(_hWnd);

            _captureItem = capturePicker.PickSingleItemAsync().AsTask().Result;
            */

            #endregion

            //#region Window Handle version


            //if (typeofwindowpicker == 0)
            {
                //picker.createwindowpickerandinit();


                //StopCapture();


                /*
                picker = new WindowPicker();

                picker.FindWindows();
                */
                //int iHandle = scgraphicssec.FindWindow(null, SelectedTitle);





                //using (picker = new WindowPicker())
                {


                    //picker.typeofwindowpicker = typeofwindowpicker;


                    /*if (typeofwindowpicker == 0)
                    {
                        //picker.createwindowpickerandinit();

                    }
                    else if (typeofwindowpicker == 1)
                   {
                       //swtc = 0;

                       //picker.createwindowpicker();
                   }*/

                    //picker.createwindowpickerandinit();

                    /*GC.SuppressFinalize(_hWnd);
                    DeleteObject(_hWnd);*/

                    //_hWnd = picker.PickCaptureTarget(_hWnd, IntPtr.Zero,"" + SelectedTitle);



                    //Program.MessageBox((IntPtr)0, "_hWnd:" + _hWnd, "sccsmsg", 0);

                    if (_hWnd == IntPtr.Zero)
                        return;



                    GetWindowThreadProcessId(_hWnd, out var processId);

                    // ignore by process name
                    var process = Process.GetProcessById((int)processId);


                    sccsr14sc.Form1.capturedwindownameform1 = process.ProcessName;

                    //sccsr14sc.Form1.capturedwindownameform1 = picker.selectedwindowname;
                    capturedwindowname = sccsr14sc.Form1.capturedwindownameform1;
                    
                    //Program.MessageBox((IntPtr)0, "" + capturedwindowname, "sccsmsg", 0);
                    /*
                    Console.WriteLine("test:");
                    Console.WriteLine("test:");
                    Console.WriteLine("test:");
                    Console.WriteLine("test:");
                    Console.WriteLine("test:");
                    Console.WriteLine("test:");

                    Console.WriteLine("test:");
                    Console.WriteLine("test:");
                    Console.WriteLine("test:");*/











                    var somename = GetProcessName(_hWnd); // path + exe.




                    //Program.MessageBox((IntPtr)0, "capturedwindowname:" + capturedwindowname + "//somename:" + somename, "sccsmsg", 0);



                    string altcapturedwindowname = capturedwindowname.ToLower();

                    //Console.WriteLine("GraphicsCapture.csline823=>somename:" + somename + "/altcapturedwindowname:" + capturedwindowname);

                    if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                    {

                       

                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        var param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        //bool isfullscreen = EnumTheWindows(_hWnd, IntPtr.Zero);

                        // //Program.MessageBox((IntPtr)0, "isfullscreen:" + isfullscreen, "sccs", 0);

                        var iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);

                        var iswindowtitlebaredge = sccsr14sc.Form1.IsWindowTitlebar(_hWnd); //if fullscreen then windowtitlebar == false. for edge
                        var iswindowmaximizedboxdedge = sccsr14sc.Form1.IsWindowMaximizeBox(_hWnd);

                        Program.RECT rectmicrosoftedge = new Program.RECT();
                        Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);


                        //Program.MessageBox((IntPtr)0, "iswindowtitlebar:" + iswindowtitlebaredge + "/iswindowmaximizebox:" + iswindowmaximizedboxdedge, "sccs", 0);

                        //Program.MessageBox((IntPtr)0, "0/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                        var windowisminimized = sccsr14sc.Form1.IsWindowMinimized(_hWnd);

                        //Program.MessageBox((IntPtr)0, "windowisminimized:" + windowisminimized, "sccs", 0);

                        bool isalttab = Program.isAltTabWindow(_hWnd);
                        bool isfullscreen = EnumTheWindows(_hWnd, IntPtr.Zero);
                        //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        if (windowisminimized)
                        {
                            //BringWindowToTop(_hWnd);
                            //ShowWindow(_hWnd, SW_SHOW);

                            SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                            //Program.MessageBox((IntPtr)0, "iswindowtitlebaredge:" + iswindowtitlebaredge + "/iswindowmaximizedboxdedge:" + iswindowmaximizedboxdedge, "sccs", 0);


                            if (!iswindowtitlebaredge) // doesnt have a title bar, maybe the window is already fullscreen
                            {
                                //window might be fullscreen already.

                                if (iswindowmaximizedboxdedge) //edge has maximize box enabled in f11 fullscreen mode
                                {
                                    /*var iswindowoverlappedboxdedge = sccsr14sc.Form1.IsWindowOverlapped(_hWnd);

                                    if (iswindowoverlappedboxdedge)
                                    {
                                        if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                        {

                                        }
                                    }*/
                                }
                            }
                            else //if(iswindowtitlebaredge)
                            {
                                if (iswindowmaximizedboxdedge) //edge has maximize box enabled in f11 fullscreen mode
                                {
                                    var therect0 = new Program.RECT();
                                    therect0.Left = 0;
                                    therect0.Top = 0;
                                    therect0.Bottom = screenHeight;
                                    therect0.Right = screenWidth;

                                    param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                    param.NormalPosition = therect0;// new RECT(0, 0, 500, 500);                   
                                    Program.SetWindowPlacement(_hWnd, ref param);


                                    uint lpdwProcessId;
                                    uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                    uint appThread = GetCurrentThreadId();

                                    uint lpdwProcessIdcapturedprogram;
                                    uint foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(_hWnd);

                                    //ShowWindow(_hWnd, SW_SHOW);
                                    SetFocus(_hWnd, "");
                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(_hWnd, "");

                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                }
                                else //if (!iswindowmaximizedboxdedge)
                                {

                                }
                            }

                        }
                        else
                        {
                            uint lpdwProcessId;
                            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                            uint appThread = GetCurrentThreadId();

                            uint lpdwProcessIdcapturedprogram;
                            uint foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                            if (foreThread != foreThreadcapturedapp) // then, this program is not foreground// there is another program foreground, is it the captured program
                            {
                                SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                                if (!iswindowtitlebaredge) // doesnt have a title bar, maybe the window is already fullscreen
                                {
                                    //window might be fullscreen already.

                                    if (iswindowmaximizedboxdedge) //edge has maximize box enabled in f11 fullscreen mode
                                    {
                                        /*var iswindowoverlappedboxdedge = sccsr14sc.Form1.IsWindowOverlapped(_hWnd);

                                        if (iswindowoverlappedboxdedge)
                                        {
                                            if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                            {

                                            }
                                        }*/
                                    }
                                }
                                else
                                {
                                    //uint lpdwProcessId;
                                    foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                    appThread = GetCurrentThreadId();

                                    //lpdwProcessIdcapturedprogram;
                                    foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(_hWnd);

                                    //ShowWindow(_hWnd, SW_SHOW);
                                    SetFocus(_hWnd, "");
                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(_hWnd, "");

                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                }
                            }
                            else //if(foreThread == foreThreadcapturedapp)
                            {
                                if (!iswindowtitlebaredge) // doesnt have a title bar, maybe the window is already fullscreen
                                {
                                    //window might be fullscreen already.

                                    if (iswindowmaximizedboxdedge) //edge has maximize box enabled in f11 fullscreen mode
                                    {
                                        /*var iswindowoverlappedboxdedge = sccsr14sc.Form1.IsWindowOverlapped(_hWnd);

                                        if (iswindowoverlappedboxdedge)
                                        {
                                            if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                            {

                                            }
                                        }*/
                                    }
                                }
                                else
                                {
                                    //uint lpdwProcessId;
                                    foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                    appThread = GetCurrentThreadId();

                                    //lpdwProcessIdcapturedprogram;
                                    foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(_hWnd);

                                    //ShowWindow(_hWnd, SW_SHOW);
                                    SetFocus(_hWnd, "");
                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(_hWnd, "");

                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                }
                            }
                        }


                        var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(_hWnd, ref param);



                        Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);













                    }
                    else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program"))
                    {

                        if (altcapturedwindowname.Contains("gnu image manipulation program"))
                        {





                            int screenWidth = Program.GetSystemMetrics(0);
                            int screenHeight = Program.GetSystemMetrics(1);


                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));



                            Program.RECT rectmicrosoftedge = new Program.RECT();
                            Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);


                            //sccsr14sc.Form1.SetFocus(_hWnd);
                            var therect = new Program.RECT();




                            var iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);


                            if (!iswinmax)
                            {

                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(_hWnd, ref param);

                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                                Program.SetWindowPlacement(_hWnd, ref param);




                                iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);

                                /*
                                sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinmax:" + iswinmax, "sccsmsg", 0);

                                var iswinfullscreen = sccsr14sc.Form1.IsWindowWS_EX_OVERLAPPEDWINDOW(_hWnd);


                                sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinfullscreen:" + iswinfullscreen, "sccsmsg", 0);
                                */






                                if (!iswinmax)
                                {



                                }
                                else if (iswinmax)
                                {

                                    SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                                    Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                                    //sccsr14sc.Form1.MessageBox((IntPtr)0, "1iswinmax:" + iswinmax, "sccsmsg", 0);

                                    if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                                    {


                                        if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                        {
                                            // SetFocus(_hWnd, "");
                                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                        }


                                        Console.WriteLine("captured program is topmost0. iHandle:");

                                    }
                                    else
                                    {
                                        //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
                                        Console.WriteLine("captured program is not topmost0. iHandle:");



                                    }

                                }

                            }
                            else
                            {
                                SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                                Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                                //sccsr14sc.Form1.MessageBox((IntPtr)0, "1iswinmax:" + iswinmax, "sccsmsg", 0);

                                if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                                {


                                    if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                    {
                                        //SetFocus(_hWnd, "");
                                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                        sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                    }


                                    Console.WriteLine("captured program is topmost0. iHandle:");

                                }
                                else
                                {
                                    //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
                                    Console.WriteLine("captured program is not topmost0. iHandle:");



                                }
                            }



                        }


                    }
                    else if (altcapturedwindowname.Contains("void") && altcapturedwindowname.Contains("expanse"))
                    {
                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        var param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        //bool isfullscreen = EnumTheWindows(_hWnd, IntPtr.Zero);

                        // //Program.MessageBox((IntPtr)0, "isfullscreen:" + isfullscreen, "sccs", 0);

                        var iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);

                        var iswindowtitlebarvoidexpanse = sccsr14sc.Form1.IsWindowTitlebar(_hWnd); //if fullscreen then windowtitlebar == false. for edge
                        var iswindowmaximizedboxdvoidexpanse = sccsr14sc.Form1.IsWindowMaximizeBox(_hWnd);

                        Program.RECT rectmicrosoftedge = new Program.RECT();
                        Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);


                        //Program.MessageBox((IntPtr)0, "iswindowtitlebar:" + iswindowtitlebaredge + "/iswindowmaximizebox:" + iswindowmaximizedboxdedge, "sccs", 0);

                        //Program.MessageBox((IntPtr)0, "0/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                        var windowisminimized = sccsr14sc.Form1.IsWindowMinimized(_hWnd);

                        //Program.MessageBox((IntPtr)0, "windowisminimized:" + windowisminimized, "sccs", 0);

                        bool isalttab = Program.isAltTabWindow(_hWnd);
                        bool isfullscreen = EnumTheWindows(_hWnd, IntPtr.Zero);
                        //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        if (windowisminimized)
                        {
                            //BringWindowToTop(_hWnd);
                            //ShowWindow(_hWnd, SW_SHOW);

                            SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                            //Program.MessageBox((IntPtr)0, "iswindowtitlebaredge:" + iswindowtitlebaredge + "/iswindowmaximizedboxdedge:" + iswindowmaximizedboxdedge, "sccs", 0);

                            //Program.MessageBox((IntPtr)0, "iswindowtitlebar:" + iswindowtitlebaredge + "/iswindowmaximizebox:" + iswindowmaximizedboxdedge, "sccs", 0);

                            //Program.MessageBox((IntPtr)0, "0/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                            if (!iswindowtitlebarvoidexpanse) // doesnt have a title bar, maybe the window is already fullscreen
                            {
                                //window might be fullscreen already.

                                if (iswindowmaximizedboxdvoidexpanse) //edge has maximize box enabled in f11 fullscreen mode
                                {
                                    /*var iswindowoverlappedboxdedge = sccsr14sc.Form1.IsWindowOverlapped(_hWnd);

                                    if (iswindowoverlappedboxdedge)
                                    {
                                        if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                        {

                                        }
                                    }*/
                                }
                            }
                            else //if(iswindowtitlebaredge)
                            {
                                if (iswindowmaximizedboxdvoidexpanse) //voidexpanse has maximize box enabled in f11 fullscreen mode
                                {
                                    var therect0 = new Program.RECT();
                                    therect0.Left = 0;
                                    therect0.Top = 0;
                                    therect0.Bottom = screenHeight;
                                    therect0.Right = screenWidth;

                                    param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                    param.NormalPosition = therect0;// new RECT(0, 0, 500, 500);                   
                                    Program.SetWindowPlacement(_hWnd, ref param);


                                    /*uint lpdwProcessId;
                                    uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                    uint appThread = GetCurrentThreadId();

                                    uint lpdwProcessIdcapturedprogram;
                                    uint foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(_hWnd);

                                    //ShowWindow(_hWnd, SW_SHOW);
                                    SetFocus(_hWnd, "");
                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(_hWnd, "");

                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);*/
                                }
                                else //if (!iswindowmaximizedboxdvoidexpanse)
                                {

                                }
                            }

                        }
                        else //window is not minimized 
                        {
                            uint lpdwProcessId;
                            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                            uint appThread = GetCurrentThreadId();

                            uint lpdwProcessIdcapturedprogram;
                            uint foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                            if (foreThread != foreThreadcapturedapp) // then, this program is not foreground// there is another program foreground, is it the captured program
                            {
                                SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                                //Program.MessageBox((IntPtr)0, "iswindowtitlebar:" + iswindowtitlebarvoidexpanse + "/iswindowmaximizebox:" + iswindowmaximizedboxdvoidexpanse, "sccs", 0);


                                if (!iswindowtitlebarvoidexpanse) // doesnt have a title bar, maybe the window is already fullscreen
                                {
                                    //window might be fullscreen already.

                                    if (iswindowmaximizedboxdvoidexpanse) //voidexpanse has maximize box enabled in f11 fullscreen mode
                                    {
                                        /*var iswindowoverlappedboxdvoidexpanse = sccsr14sc.Form1.IsWindowOverlapped(_hWnd);

                                        if (iswindowoverlappedboxdvoidexpanse)
                                        {
                                            if ((rectmicrosoftvoidexpanse.Right) == screenWidth && (rectmicrosoftvoidexpanse.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                            {

                                            }
                                        }*/
                                    }
                                }
                                else //if(iswindowtitlebarvoidexpanse) //void expanse in small window has a title bar although theres no title. minimize and close are there but not maximize box.
                                {

                                    if (iswindowmaximizedboxdvoidexpanse) //voidexpanse has maximize box enabled in f11 fullscreen mode
                                    {

                                    }
                                    else
                                    {
                                        /*var therect0 = new Program.RECT();
                                        therect0.Left = 0;
                                        therect0.Top = 0;
                                        therect0.Bottom = screenHeight;
                                        therect0.Right = screenWidth;

                                        param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                                        param.NormalPosition = therect0;// new RECT(0, 0, 500, 500);                   
                                        Program.SetWindowPlacement(_hWnd, ref param);*/

                                     

                                        AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                        //BringWindowToTop(_hWnd);

                                        //ShowWindow(_hWnd, SW_SHOW);
                                        SetFocus(_hWnd, "");

                                        const int GWL_STYLE = -16; //want to change the window style
                                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);
                                        if (sult == IntPtr.Zero)
                                        {
                                            //in some cases SWL just outright fails, so we can notify the user and abort
                                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                                            //return;
                                            Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                        }
                                        sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_MAXIMIZE);

                                        SetFocus(_hWnd, "");

                                        AttachThreadInput(foreThreadcapturedapp, appThread, false);

                                    }


                                    //uint lpdwProcessId;
                                    /*foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                    appThread = GetCurrentThreadId();

                                    //lpdwProcessIdcapturedprogram;
                                    foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(_hWnd);

                                    //ShowWindow(_hWnd, SW_SHOW);
                                    SetFocus(_hWnd, "");
                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(_hWnd, "");

                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);*/
                                }
                            }
                            else //if(foreThread == foreThreadcapturedapp)
                            {
                                if (!iswindowtitlebarvoidexpanse) // doesnt have a title bar, maybe the window is already fullscreen
                                {
                                    //window might be fullscreen already.

                                    if (iswindowmaximizedboxdvoidexpanse) //edge has maximize box enabled in f11 fullscreen mode
                                    {
                                        /*var iswindowoverlappedboxdedge = sccsr14sc.Form1.IsWindowOverlapped(_hWnd);

                                        if (iswindowoverlappedboxdedge)
                                        {
                                            if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                            {

                                            }
                                        }*/
                                    }
                                }
                                else
                                {
                                    //uint lpdwProcessId;
                                    /*foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                    appThread = GetCurrentThreadId();

                                    //lpdwProcessIdcapturedprogram;
                                    foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(_hWnd);

                                    //ShowWindow(_hWnd, SW_SHOW);
                                    SetFocus(_hWnd, "");
                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(_hWnd, "");

                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);*/
                                }
                            }
                        }


                        var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Show; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(_hWnd, ref param);



                        Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);









                    }
                    /*else if (altcapturedwindowname.Contains("it lurks below"))
                    {
                        Program.MessageBox((IntPtr)0, "" + "ILB", "sccsmsg", 0);
                    }*/




                        /*if (_hWnd == IntPtr.Zero)
                            return;

                        _captureItem = CreateItemForWindow(_hWnd);

                        #endregion

                        if (_captureItem == null)
                            return;

                        _captureItem.Closed += CaptureItemOnClosed;

                        hr = NativeMethodsgc.CreateDirect3D11DeviceFromDXGIDevice(device.NativePointer, out var pUnknown);
                        if (hr != 0)
                        {
                            StopCapture();
                            return;
                        }

                        winrtDevice = (IDirect3DDevice)Marshal.GetObjectForIUnknown(pUnknown);
                        //Marshal.Release(pUnknown);
                        DeleteObject(pUnknown);


                        _captureFramePool = Direct3D11CaptureFramePool.Create(winrtDevice, DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, _captureItem.Size);
                        _captureSession = _captureFramePool.CreateCaptureSession(_captureItem);
                        _captureSession.StartCapture();*/
                        IsCapturing = true;

                }
            }
            //else if (typeofwindowpicker == 1)
            {
                //swtc = 0;
                //picker.createwindowpicker();


                /*
                _hWnd = picker.PickCaptureTarget(_hWnd);
                capturedwindowname = picker.selectedwindowname;
                */

                /*
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));







                Program.RECT therect = new Program.RECT();*/
                /*therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;
                param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);               

                therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;
                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);
                */

                //set the window to a borderless style
                /*const int GWL_STYLE = -16; //want to change the window style
                const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);


                if (sult == IntPtr.Zero)
                {
                    //in some cases SWL just outright fails, so we can notify the user and abort
                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                    //return;
                    Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                }

                sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_MAXIMIZE);
                */



                /*therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);*/


                /*
                 int screenWidth = Program.GetSystemMetrics(0);
                 int screenHeight = Program.GetSystemMetrics(1);
                 Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW); //| (uint)SetWindowPosFlags.SWP_DRAWFRAME
                */


                /*
                if (fullscreen == -1 || fullscreen == 0)
                {
                    
                }
                else
                {
                    therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 1080;
                    therect.Right = 1920;

                    param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(_hWnd, ref param);
                }

                */



                /*
                therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;
                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                               //Program.SetWindowPlacement(_hWnd, ref param);
                */

                /*




                therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);

                screenWidth = Program.GetSystemMetrics(0);
                screenHeight = Program.GetSystemMetrics(1);
                */



                //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW); //| (uint)SetWindowPosFlags.SWP_DRAWFRAME


                //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOP, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                //Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                /*
                therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;
                param.ShowCmd = Program.ShowWindowCommands.Show; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);*/

                /*
                therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);*/





                //Program.SelectedTitle = capturedwindowname;
                //Program.executeModeChange();

                //Program.EnumWindows(Program.enumWindowProc, IntPtr.Zero);





                //Program.progcanpause = 2;



                //Program.MessageBox((IntPtr)0, "" + "test", "sccsmsg", 0);

                /*int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);
                
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                Program.RECT therect = new Program.RECT();*/
                /*
                therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = screenHeight;
                therect.Right = screenWidth;

                /*param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);*/

                //Program.RECT therect = new Program.RECT();
                /*therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = screenHeight;
                therect.Right = screenWidth;

                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);

                sccsr14sc.Form1.SetFocus(_hWnd);
                */
                //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW











                _captureItem = CreateItemForWindow(_hWnd);

                //#endregion

                if (_captureItem == null)
                    return;

                _captureItem.Closed += CaptureItemOnClosed;

                hr = NativeMethodsgc.CreateDirect3D11DeviceFromDXGIDevice(device.NativePointer, out var pUnknown);
                if (hr != 0)
                {
                    StopCapture();
                    return;
                }

                winrtDevice = (IDirect3DDevice)Marshal.GetObjectForIUnknown(pUnknown);
                //Marshal.Release(pUnknown);
                DeleteObject(pUnknown);


                _captureFramePool = Direct3D11CaptureFramePool.Create(winrtDevice, DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, _captureItem.Size);
                _captureSession = _captureFramePool.CreateCaptureSession(_captureItem);
                _captureSession.StartCapture();
                IsCapturing = true;

            }

        }


        Texture2DDescription texture2dDescription;

        int swtc = 0;
        Texture2D texture2d;



        int letoneframerip = 0;


        public Texture2D TryGetNextFrameAsTexture2D(Device device)
        {

            if (letoneframerip >=1)
            {

                letoneframerip = 0;
            }











            using var frame = _captureFramePool?.TryGetNextFrame();
            if (frame == null)
                return null;

            // ReSharper disable once SuspiciousTypeConversion.Global
            surfaceDxgiInterfaceAccess = (IDirect3DDxgiInterfaceAccess)frame.Surface;



            pResource = surfaceDxgiInterfaceAccess.GetInterface(new Guid("dc8e63f3-d12b-4952-b47b-5e45026a862d"));

            /*using var surfaceTexture = new Texture2D(pResource); // shared resource
            var texture2dDescription = new Texture2DDescription
            {
                ArraySize = 1,
                BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                CpuAccessFlags = CpuAccessFlags.None,
                Format = Format.B8G8R8A8_UNorm,
                Height = surfaceTexture.Description.Height,
                MipLevels = 1,
                SampleDescription = new SampleDescription(1, 0),
                Usage = ResourceUsage.Default,
                Width = surfaceTexture.Description.Width
            };
            var texture2d = new Texture2D(device, texture2dDescription);
            device.ImmediateContext.CopyResource(surfaceTexture, texture2d);

            */


            using var surfaceTexture = new Texture2D(pResource);
            {
                if (swtc == 0)
                {
                    /*texture2dDescription = new Texture2DDescription
                    {
                        ArraySize = 1,
                        BindFlags = BindFlags.None, // ShaderResource | BindFlags.RenderTarget,
                        CpuAccessFlags = CpuAccessFlags.Read,
                        Format = Format.B8G8R8A8_UNorm,
                        Height = surfaceTexture.Description.Height,
                        MipLevels = 1,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Staging,
                        Width = surfaceTexture.Description.Width
                    };

                    texture2d = new Texture2D(device, texture2dDescription);*/

                    // using var surfaceTexture = new Texture2D(phSurface);

                    /*texture2dDescription = new Texture2DDescription
                    {
                        CpuAccessFlags = CpuAccessFlags.Read,
                        BindFlags = BindFlags.None, //ShaderResource | BindFlags.RenderTarget,
                        Format = Format.B8G8R8A8_UNorm,
                        Width = surfaceTexture.Description.Width,
                        Height = surfaceTexture.Description.Height,
                        OptionFlags = ResourceOptionFlags.None,
                        MipLevels = 1,
                        ArraySize = 1,
                        SampleDescription = { Count = 1, Quality = 0 },
                        Usage = ResourceUsage.Staging
                    };*/


                    texture2dDescription = new Texture2DDescription
                    {
                        ArraySize = 1,
                        BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                        CpuAccessFlags = CpuAccessFlags.None,
                        Format = Format.B8G8R8A8_UNorm,
                        Height = surfaceTexture.Description.Height,
                        MipLevels = 1,
                        SampleDescription = new SampleDescription(1, 0),
                        Usage = ResourceUsage.Default,
                        Width = surfaceTexture.Description.Width
                    };



                    texture2d = new Texture2D(device, texture2dDescription);
                    swtc = 1;
                }


                /*texture2dDescription = new Texture2DDescription
                {
                    ArraySize = 1,
                    BindFlags = BindFlags.ShaderResource | BindFlags.RenderTarget,
                    CpuAccessFlags = CpuAccessFlags.None,
                    Format = Format.B8G8R8A8_UNorm,
                    Height = surfaceTexture.Description.Height,
                    MipLevels = 1,
                    SampleDescription = new SampleDescription(1, 0),
                    Usage = ResourceUsage.Default,
                    Width = surfaceTexture.Description.Width
                };
                texture2d = new Texture2D(device, texture2dDescription);*/



                device.ImmediateContext.CopyResource(surfaceTexture, texture2d);

                DeleteObject(pResource);
                if (surfaceTexture != null)
                {
                    surfaceTexture.Dispose();
                    //surfaceTexture = null;
                }


                if (lastframe != null)
                {
                    lastframe.Dispose();
                    lastframe = null;
                }
                lastframe = frame;

                if (lastsurfaceTexture != null)
                {
                    lastsurfaceTexture.Dispose();
                    lastsurfaceTexture = null;
                }
                lastsurfaceTexture = surfaceTexture;

            }

            if (surfaceDxgiInterfaceAccess != null)
            {
                surfaceDxgiInterfaceAccess.Dispose();
                surfaceDxgiInterfaceAccess = null;
            }

            /*if (frame != null)
            {
                frame.Dispose();
                //frame = null;
            }*/


            letoneframerip++;
            return texture2d;
        }
        Texture2D lastsurfaceTexture;
        Direct3D11CaptureFrame lastframe;




        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        public void StopCapture() // ...or release resources
        {
            IntPtr hFocus = IntPtr.Zero;
            IntPtr hFore;
            uint id = 0;
            hFore = GetForegroundWindow();
            var idAttach = GetWindowThreadProcessId(hFore, out id);
            var curThreadId = GetCurrentThreadId();

            // To dettach from current thread
            AttachThreadInput(idAttach, curThreadId, false);



            _captureSession?.Dispose();
            _captureSession = null;

            _captureFramePool?.Dispose();
            _captureFramePool = null;


            _captureItem = null;
            IsCapturing = false;
            factory = null;
            interop = null;


            if (texture2d != null)
            {
                texture2d.Dispose();
                texture2d = null;
            }

            if (surfaceDxgiInterfaceAccess != null)
            {
                surfaceDxgiInterfaceAccess.Dispose();
                surfaceDxgiInterfaceAccess = null;
            }
            /*if (picker != null)
            {
                picker.Dispose();
                //sccsr14sc.Form1.MessageBox((IntPtr)0, "", "sccsmsg", 0);
                picker = null;
            }*/

            if (winrtDevice != null)
            {
                winrtDevice.Dispose();
                winrtDevice = null;
            }


            hr = 0;

            if (pointer != IntPtr.Zero)
            {
                DeleteObject(pointer);
                GC.SuppressFinalize(pointer);
            }

            if (pResource != IntPtr.Zero)
            {
                DeleteObject(pResource);
                GC.SuppressFinalize(pResource);
            }


            capture = null;
            //this.Dispose();
        }
        static IDirect3DDxgiInterfaceAccess surfaceDxgiInterfaceAccess;

        static IActivationFactory factory;
        static IGraphicsCaptureItemInterop interop;
        static IntPtr pointer;

        static IntPtr pResource;

        static GraphicsCaptureItem capture;

        // ReSharper disable once SuspiciousTypeConversion.Global
        private static GraphicsCaptureItem CreateItemForWindow(IntPtr hWnd)
        {
            /*factory = WindowsRuntimeMarshal.GetActivationFactory(typeof(GraphicsCaptureItem));
            interop = (IGraphicsCaptureItemInterop)factory;
            pointer = interop.CreateForWindow(hWnd, typeof(GraphicsCaptureItem).GetInterface("IGraphicsCaptureItem").GUID);
            capture = Marshal.GetObjectForIUnknown(pointer) as GraphicsCaptureItem;
            DeleteObject(pointer);
            return capture;*/

            if (hWnd != IntPtr.Zero)
            {
                factory = WindowsRuntimeMarshal.GetActivationFactory(typeof(GraphicsCaptureItem));
                interop = (IGraphicsCaptureItemInterop)factory;




                pointer = interop.CreateForWindow(hWnd, typeof(GraphicsCaptureItem).GetInterface("IGraphicsCaptureItem").GUID);



                capture = Marshal.GetObjectForIUnknown(pointer) as GraphicsCaptureItem;
                Marshal.Release(pointer);
                return capture;
                //return null;
            }
            else
            {
                return null;
            }




        }

        private void CaptureItemOnClosed(GraphicsCaptureItem sender, object args)
        {
            StopCapture();
        }
    }
}