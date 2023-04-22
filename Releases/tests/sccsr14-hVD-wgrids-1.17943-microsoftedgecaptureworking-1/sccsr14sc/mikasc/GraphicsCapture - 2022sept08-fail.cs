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
            if (picker != null)
            {
                picker.Dispose();
                //sccsr14sc.Form1.MessageBox((IntPtr)0, "", "sccsmsg", 0);
                picker = null;
            }

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



        public IntPtr _hWnd;
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

            #region Window Handle version


            //if (typeofwindowpicker == 0)
            {
                //picker.createwindowpickerandinit();


                StopCapture();

                using (picker = new WindowPicker())
                {


                    picker.typeofwindowpicker = typeofwindowpicker;


                    /*if (typeofwindowpicker == 0)
                    {
                        //picker.createwindowpickerandinit();

                    }
                    else if (typeofwindowpicker == 1)
                   {
                       //swtc = 0;

                       //picker.createwindowpicker();
                   }*/

                    picker.createwindowpickerandinit();

                    GC.SuppressFinalize(_hWnd);
                    DeleteObject(_hWnd);

                    _hWnd = picker.PickCaptureTarget(_hWnd);
                    sccsr14sc.Form1.capturedwindownameform1 = picker.selectedwindowname;

                    capturedwindowname = sccsr14sc.Form1.capturedwindownameform1;







                    if (_hWnd == IntPtr.Zero)
                        return;









                    var somename = GetProcessName(_hWnd); // path + exe.

                    Console.WriteLine("somename:" + somename);







                    string altcapturedwindowname = capturedwindowname.ToLower();

                    if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                    {

                       

                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        var param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        uint lpdwProcessId;
                        uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                        uint appThread = GetCurrentThreadId();



                        uint lpdwProcessIdcapturedprogram;
                        uint foreThreadcapturedapp = GetWindowThreadProcessId(_hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero







                        //bool isfullscreen = EnumTheWindows(_hWnd, IntPtr.Zero);

                        // Program.MessageBox((IntPtr)0, "isfullscreen:" + isfullscreen, "sccs", 0);

                        var iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);

                        var iswindowtitlebaredge = sccsr14sc.Form1.IsWindowTitlebar(_hWnd); //if fullscreen then windowtitlebar == false. for edge
                        var iswindowmaximizedboxdedge = sccsr14sc.Form1.IsWindowMaximizeBox(_hWnd);

                        Program.RECT rectmicrosoftedge = new Program.RECT();
                        Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);


                        Program.MessageBox((IntPtr)0, "iswindowtitlebar:" + iswindowtitlebaredge + "/iswindowmaximizebox:" + iswindowmaximizedboxdedge, "sccs", 0);

                        Program.MessageBox((IntPtr)0, "0/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                        if (foreThread != foreThreadcapturedapp) // then, this program is not foreground// there is another program foreground, is it the captured program
                        {
                            //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);



                            /*
                            SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            */



                            /*
                            SetWindowLong(_hWnd, GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                            Program.SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            */

                            //bool isalttab = Program.isAltTabWindow(_hWnd);

                            //it should be topmost. but whatever, hence the else statement todo later.
                            SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);










                            //bool isfullscreen = EnumTheWindows(_hWnd, IntPtr.Zero);
                            //Program.MessageBox((IntPtr)0, "isfullscreen:" + isfullscreen, "sccs", 0);


                            //Program.MessageBox((IntPtr)0, "0window is fullscreen:" +iswinmax, "sccs", 0);
                        

                            bool isfullscreen = EnumTheWindows(_hWnd, IntPtr.Zero);

                            if (isfullscreen)
                            {

                            }
                            else
                            {



                            }
                         
                            if (!iswindowtitlebaredge)
                            {
                                //window might be fullscreen already.

                                if (iswindowmaximizedboxdedge) //edge has maximize box enabled in f11 fullscreen mode
                                {
                                    var iswindowoverlappedboxdedge = sccsr14sc.Form1.IsWindowOverlapped(_hWnd);
                                    
                                    if (iswindowoverlappedboxdedge)
                                    {
                                        //window is not fullscreen in edge Version 105.0.1343.27 (Official build) (64-bit)

                                        /*AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                        SetFocus(_hWnd, "");

                                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                        
                                        //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                        //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                        

                                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                        //WORKING
                                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                        //WORKING

                                        SetFocus(_hWnd, "");


                                        AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                        //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                        //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);
                                        */


                                        
                                        if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                        {
                                            //window is not fullscreen in edge Version 105.0.1343.27 (Official build) (64-bit)

                                            AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                            SetFocus(_hWnd, "");

                                            //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                            //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                            //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);

                                            /*
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING*/
                                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                            SetFocus(_hWnd, "");


                                            AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                            //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                            //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);
                                        }
                                        else
                                        {
                                            //window is not fullscreen in edge Version 105.0.1343.27 (Official build) (64-bit)

                                            AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                            SetFocus(_hWnd, "");

                                            //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                            //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                            //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);

                                            /*
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING*/

                                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                            /*
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING*/

                                            SetFocus(_hWnd, "");


                                            AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                            //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                            //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);
                                        }

                                    }
                                    else
                                    {
                                        //window is fullscreen already. do nothing.
                                        //window is not fullscreen in edge Version 105.0.1343.27 (Official build) (64-bit)

                                        AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                        SetFocus(_hWnd, "");

                                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                        //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                        //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);

                                        /*sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                        //WORKING
                                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                        //WORKING
                                        */
                                        SetFocus(_hWnd, "");


                                        AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                        //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                        //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);


                                    }
                                }
                                else
                                {
                                    //did microsoft change anything about the f11 fullscreen mode in later/earlier revisions. todo later. or
                                    //when the window is movable and not maximized, it seems that this API recognizes that there is no titlebar and no maximize box or not.




                                }

                            }
                            else // iswindowtitlebar == true
                            {




                                //Program.MessageBox((IntPtr)0, "iswindowtitlebar:" + iswindowtitlebaredge + "/iswindowmaximizebox:" + iswindowmaximizedboxdedge, "sccs", 0);

                                /*if ()
                                {

                                }*/


                                //window might be fullscreen already.

                                if (iswindowmaximizedboxdedge) //edge has maximize box enabled in f11 fullscreen mode
                                {
                                    var iswindowoverlappedboxdedge = sccsr14sc.Form1.IsWindowOverlapped(_hWnd);

                                    //Program.MessageBox((IntPtr)0, "iswindowoverlappedboxdedge:" + iswindowoverlappedboxdedge, "sccs", 0);

                                    if (iswindowoverlappedboxdedge)
                                    {
                                        //window is not fullscreen in edge Version 105.0.1343.27 (Official build) (64-bit)

                                        AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                        SetFocus(_hWnd, "");

                                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                        //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                        //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);

                                        /*
                                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                        //WORKING
                                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                        //WORKING*/
                                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                        SetFocus(_hWnd, "");


                                        AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                        //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                        //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);


                                        Console.WriteLine("pressed f11");

                                        /*if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                        {
                                            //window is not fullscreen in edge Version 105.0.1343.27 (Official build) (64-bit)

                                            AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                            SetFocus(_hWnd, "");

                                            //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                            //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                            //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);


                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING

                                            SetFocus(_hWnd, "");


                                            AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                            //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                            //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);
                                        }
                                        else
                                        {
                                            //window is not fullscreen in edge Version 105.0.1343.27 (Official build) (64-bit)

                                            AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                            SetFocus(_hWnd, "");

                                            //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                            //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                            //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);


                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            /*
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING
                                            sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                            //WORKING

                                            SetFocus(_hWnd, "");


                                            AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                            //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                            //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);
                                        }*/

                                    }
                                    else
                                    {
                                        //window is fullscreen already. do nothing.
                                        //window is not fullscreen in edge Version 105.0.1343.27 (Official build) (64-bit)

                                        AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                        SetFocus(_hWnd, "");

                                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                        //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                        //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);

                                        /*
                                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                        //WORKING
                                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                        //WORKING*/

                                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                        SetFocus(_hWnd, "");


                                        AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                        //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                        //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);


                                    }
                                }
                                else
                                {
                                    //did microsoft change anything about the f11 fullscreen mode in later/earlier revisions. todo later. or
                                    //when the window is movable and not maximized, it seems that this API recognizes that there is no titlebar and no maximize box or not.




                                }


                                /*AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                SetFocus(_hWnd, "");

                                //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                
                                //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                //sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                

                                sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                //WORKING
                                sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                //WORKING

                                SetFocus(_hWnd, "");


                                AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);
                                */
                            }





                            if (iswinmax)
                            {
                              


                                //Program.MessageBox((IntPtr)0, "0/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                                if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                {

                                 

                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    SetFocus(_hWnd, "");

                                    //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    /*
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                    */
                                    /*
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING*/

                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                    SetFocus(_hWnd, "");


                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                    //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                    //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);

                                }
                                else if((rectmicrosoftedge.Right) > screenWidth && (rectmicrosoftedge.Bottom) > screenHeight ||
                                    (rectmicrosoftedge.Right) > screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                                    (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) > screenHeight)
                                {
                                   // Program.MessageBox((IntPtr)0, "window is already fullscreen maybe ", "sccs", 0);

                                }
                                else
                                {
                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    SetFocus(_hWnd, "");

                                    //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    /*
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                    */
                                    /*
                                     sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                     //WORKING
                                     sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                     //WORKING*/

                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                    SetFocus(_hWnd, "");


                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                    //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                }

                            }
                            else
                            {
                                rectmicrosoftedge = new Program.RECT();
                                Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);


                                //Program.MessageBox((IntPtr)0, "1/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                                if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                {
                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    SetFocus(_hWnd, "");

                                    //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    /*
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                    */
                                    /*
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING*/

                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                    SetFocus(_hWnd, "");


                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                    //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                    //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);

                                }
                                else if ((rectmicrosoftedge.Right) > screenWidth && (rectmicrosoftedge.Bottom) > screenHeight ||
                                    (rectmicrosoftedge.Right) > screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                                    (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) > screenHeight)
                                {
                                   // Program.MessageBox((IntPtr)0, "1window is already fullscreen maybe ", "sccs", 0);

                                }
                                else
                                {
                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    SetFocus(_hWnd, "");

                                    //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    /*
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                    */
                                    /*
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING*/

                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                                    SetFocus(_hWnd, "");


                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                    //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                }

                            }



                            /*
                            while (true)
                            {
                                EnumWindows(EnumTheWindows, IntPtr.Zero);
                                Console.ReadKey();
                                Console.Clear();
                            }*/



                            //SetWindowLong(sccsr14sc.Form1.theHandle, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(sccsr14sc.Form1.theHandle, Program.GWL_EXSTYLE) | Program.WS_EX_TRANSPARENT | Program.WS_EX_LAYERED | Program.WS_EX_TOPMOST));
                            //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            /*
                            AttachThreadInput(foreThreadcapturedapp, appThread, true);
                            
                          

                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                            /*
                            sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 5);
                            sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 5);
                            
                            //SendMessage(iHandle, WM_KEYDOWN | WM_KEYUP, (int)0x57, 0);
                            //SendMessage(iHandle, WM_KEYUP, 0, (int)0x57);#

                            AttachThreadInput(foreThreadcapturedapp, appThread, false);
                            */

                            /*
                            if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                            {
                               // Program.MessageBox((IntPtr)0, "3window is fullscreen ", "sccs", 0);

                                //it should be topmost. but whatever, hence the else statement todo later.

                                AttachThreadInput(foreThreadcapturedapp, appThread, true);


                                //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                               // Program.MessageBox((IntPtr)0, "window is fullscreen ", "sccs", 0);


                                sccs.scgraphics.scgraphicssec.SendMessage((int)foreThread,sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 5); 
                                sccs.scgraphics.scgraphicssec.SendMessage((int)foreThread, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 5);

                                //SendMessage(iHandle, WM_KEYDOWN | WM_KEYUP, (int)0x57, 0);
                                //SendMessage(iHandle, WM_KEYUP, 0, (int)0x57);#

                                AttachThreadInput(foreThreadcapturedapp, appThread, false);


                            }
                            else
                            {

                            }*/



                        }
                        else //if(foreThread == foreThreadcapturedapp)
                        {

                            iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);


                            //Program.MessageBox((IntPtr)0, "0window is fullscreen:" +iswinmax, "sccs", 0);



                            if (iswinmax)
                            {
                                rectmicrosoftedge = new Program.RECT();
                                Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);


                                //Program.MessageBox((IntPtr)0, "0/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                                if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                {
                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    SetFocus(_hWnd, "");

                                    //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    /*
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                    */

                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING

                                    SetFocus(_hWnd, "");


                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                    //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                    //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);

                                }
                                else if ((rectmicrosoftedge.Right) > screenWidth && (rectmicrosoftedge.Bottom) > screenHeight ||
                                    (rectmicrosoftedge.Right) > screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                                    (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) > screenHeight)
                                {
                                   // Program.MessageBox((IntPtr)0, "window is already fullscreen maybe ", "sccs", 0);

                                }
                                else
                                {
                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    SetFocus(_hWnd, "");

                                    //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    /*
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                    */

                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING

                                    SetFocus(_hWnd, "");


                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                    //Program.MessageBox((IntPtr)0, "0window is maximized. trying to go fullscreen", "sccs", 0);
                                }

                            }
                            else
                            {
                                rectmicrosoftedge = new Program.RECT();
                                Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);


                               // Program.MessageBox((IntPtr)0, "1/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                                if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight) // window is maximized, not fullscreen.
                                {
                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    SetFocus(_hWnd, "");

                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                    /* sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                    */
                                    /*
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING
                                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                    //WORKING*/

                                    SetFocus(_hWnd, "");


                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);


                                    //Program.MessageBox((IntPtr)0, "1window is fullscreen ", "sccs", 0);

                                }
                                else if ((rectmicrosoftedge.Right) > screenWidth && (rectmicrosoftedge.Bottom) > screenHeight ||
                                    (rectmicrosoftedge.Right) > screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                                    (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) > screenHeight)
                                {
                                   // Program.MessageBox((IntPtr)0, "window is already fullscreen maybe ", "sccs", 0);

                                }
                                else
                                {
                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    SetFocus(_hWnd, "");

                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                    /* sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYDOWN, (int)VirtualKeyCode.VK_W, 0);
                                    sccs.scgraphics.scgraphicssec.SendMessage((int)foreThreadcapturedapp, sccsr14sc.Form1.WM_KEYUP, (int)VirtualKeyCode.VK_W, 0);
                                    */
                                    /*
                                     sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                     //WORKING
                                     sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                                     //WORKING
                                    */
                                    SetFocus(_hWnd, "");


                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                    //Program.MessageBox((IntPtr)0, "window is maximized. trying to go fullscreen", "sccs", 0);
                                }
                            }


                        }



                        Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
































                        /*
                        Console.WriteLine("MICROSOFT EDGE: " + altcapturedwindowname);

                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);


                        //SetFocus(_hWnd, "");







                        //SOMETHING IS WRONG WITH THE WINDOW DIMENSIONS, DISPROPORTIONATE OF AT LEAST 20X TIMES MORE THAN THE MAXIMUM SOMETIMES.
                        //SOMETHING IS WRONG WITH THE WINDOW DIMENSIONS, DISPROPORTIONATE OF AT LEAST 20X TIMES MORE THAN THE MAXIMUM SOMETIMES.
                        Program.RECT rectmicrosoftedge = new Program.RECT();
                        Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);








                        /*(rectmicrosoftedge.Right - rectmicrosoftedge.Left) == screenWidth && (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) == screenHeight ||
                            (rectmicrosoftedge.Right - rectmicrosoftedge.Left) == screenWidth && (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) == screenHeight - 1

                        //Program.MessageBox((IntPtr)0, "window is fullscreen " + iswinmax, "sccs", 0);




                        //Program.MessageBox((IntPtr)0, "/w:" + (rectmicrosoftedge.Right) + "/h:" + (rectmicrosoftedge.Bottom), "sccs", 0);


                        if (rectmicrosoftedge.Right > screenWidth && rectmicrosoftedge.Bottom > screenHeight)
                        {

                        }

                        var iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);


                        //Program.MessageBox((IntPtr)0, "window is fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);


                        //0.01081916537867078825347758887172
                        //0.05740740740740740740740740740741


                        double isfullscreenmsedgew = Math.Ceiling((0.01081916537867078825347758887172d * screenWidth) + screenWidth);
                        double isfullscreenmsedgeh = Math.Ceiling((0.05740740740740740740740740740741d * screenHeight) + screenHeight);




                        bool isalttab = Program.isAltTabWindow(_hWnd);

                       // Program.MessageBox((IntPtr)0, "/isalttab:" + isalttab+  "/0window is fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom + "/altw:" + isfullscreenmsedgew + "/alth:" + isfullscreenmsedgeh, "sccs", 0);


                        if (!isalttab)
                        {
                            // ||(rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight - 1
                            if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                                (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight - 1 ||
                                isfullscreenmsedgew == rectmicrosoftedge.Right && isfullscreenmsedgeh == rectmicrosoftedge.Bottom || iswinmax)
                            {

                                //Microsoft edge
                                if (isfullscreenmsedgew == rectmicrosoftedge.Right && isfullscreenmsedgeh == rectmicrosoftedge.Bottom)
                                {
                                    Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                                    if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                    {
                                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                        sccsr14sc.Form1.wasf11pressedbynativeonedge = 2;
                                    }
                                    //Program.MessageBox((IntPtr)0, "1window is fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom + "/altw:" + isfullscreenmsedgew + "/alth:" + isfullscreenmsedgeh, "sccs", 0);


                                    //Console.WriteLine("" + "1window is fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom + "/altw:" + isfullscreenmsedgew + "/alth:" + isfullscreenmsedgeh);


                                    //Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                                   // Program.MessageBox((IntPtr)0, "0window was fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);
                                }
                                else
                                {
                                    if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                                        (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight - 1)
                                    {
                                        if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                        {

                                            if (!iswinmax)
                                            {
                                                uint lpdwProcessId;
                                                uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                                uint appThread = GetCurrentThreadId();

                                                AttachThreadInput(foreThread, appThread, true);

                                                /*BringWindowToTop(_hWnd);

                                                ShowWindow(_hWnd, SW_SHOW);
                                                
                                                if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                                {
                                                    SetFocus(_hWnd, "");
                                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                                    SetFocus(_hWnd, "");
                                                    sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                                }

                                                AttachThreadInput(foreThread, appThread, false);




                                               // Program.MessageBox((IntPtr)0, "0window was maximized " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);

                                            }
                                            else
                                            {
                                                uint lpdwProcessId;
                                                uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                                uint appThread = GetCurrentThreadId();

                                                AttachThreadInput(foreThread, appThread, true);

                                                /*BringWindowToTop(_hWnd);

                                                ShowWindow(_hWnd, SW_SHOW);
                                                */
                        /*if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                        {
                            SetFocus(_hWnd, "");
                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                            SetFocus(_hWnd, "");
                            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                        }

                        AttachThreadInput(foreThread, appThread, false);
                    }


                    sccsr14sc.Form1.wasf11pressedbynativeonedge = 2;
                }
            }
        }


        //Program.MessageBox((IntPtr)0, "window is fullscreen " + iswinmax, "sccs", 0);

        //SetFocus(_hWnd, "");
        //// Program.MessageBox((IntPtr)0,"/w:" + (rectmicrosoftedge.Right) + "/h:" + (rectmicrosoftedge.Bottom), "sccs", 0);
        /*const int GWL_STYLE = -16; //want to change the window style
        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

        if (sult == IntPtr.Zero)
        {
            //in some cases SWL just outright fails, so we can notify the user and abort
            //MessageBox.Show("Unable to alter window style.\nSorry.");
            //return;
            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

        }
        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

        //SetWindowRgn(_hWnd, CreateRoundRectRgn(0, 0, screenWidth, screenHeight, 0, 0), true);
        SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);





        //SetFocus(_hWnd, "");

    }
    else
    {

        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
        Program.RECT therect = new Program.RECT();
        therect.Left = 0;
        therect.Top = 0;
        therect.Bottom = screenHeight;
        therect.Right = screenWidth;

        param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
        Program.SetWindowPlacement(_hWnd, ref param);


        uint lpdwProcessId;
        uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

        uint appThread = GetCurrentThreadId();

        AttachThreadInput(foreThread, appThread, true);

        /*BringWindowToTop(_hWnd);

        ShowWindow(_hWnd, SW_SHOW);

        if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
        {
            SetFocus(_hWnd, "");
            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
            SetFocus(_hWnd, "");
            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
        }

        AttachThreadInput(foreThread, appThread, false);






       // Program.MessageBox((IntPtr)0, "1window was less than maximized " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);

        //check if width and height values are different for gimp/firefox/chrome/opera when doing F11 to maximize.
        //isfullscreenmsedgew == rectmicrosoftedge.Right && isfullscreenmsedgeh == rectmicrosoftedge.Bottom)


        //window is maximized or fullscreen
        //Program.MessageBox((IntPtr)0, "window is not maximized", "sccs", 0);


        /*
        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
        Program.RECT therect = new Program.RECT();



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

        //sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinmax:" + iswinmax, "sccsmsg", 0);

        //var iswinfullscreen = sccsr14sc.Form1.IsWindowWS_EX_OVERLAPPEDWINDOW(_hWnd);

        //sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinfullscreen:" + iswinfullscreen, "sccsmsg", 0);



        SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

        //sccsr14sc.Form1.MessageBox((IntPtr)0, "1iswinmax:" + iswinmax, "sccsmsg", 0);

        if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
        {


            /*
            var refreshDXEngineAction = new Action(delegate
            {
                SetFocus(_hWnd, "");

            });
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);


            uint lpdwProcessId;
            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(),out lpdwProcessId); //IntPtr.Zero

            uint appThread = GetCurrentThreadId();

            Console.WriteLine("foreThread:" + foreThread + "/appThread:" + appThread + "/lpdwProcessId:" + lpdwProcessId);

            AttachThreadInput(foreThread, appThread, true);
            SetFocus(_hWnd, "");

            if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
            {
                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
            }
            SetFocus(_hWnd, "");
            AttachThreadInput(foreThread, appThread, false);


            /*
            refreshDXEngineAction = new Action(delegate
            {
                SetFocus(_hWnd, "");

            });
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);







            Console.WriteLine("captured program is topmost0. iHandle:");

        }
        else
        {
            //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
            Console.WriteLine("captured program is not topmost0. iHandle:");



        }
        */
                        //Program.MessageBox((IntPtr)0,"/w:" + (rectmicrosoftedge.Right) + "/h:" + (rectmicrosoftedge.Bottom), "sccs", 0);
                        /*const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

                        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        //SetWindowRgn(_hWnd, CreateRoundRectRgn(0, 0, screenWidth, screenHeight, 0, 0), true);
                        SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);



                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        if (!iswinmax)
                        {

                        }
                        else if (iswinmax)
                        {


                        }
                    }
                }
                else
                {





                    //IS ALT TABBED WINDOW
                    //IS ALT TABBED WINDOW
                    //IS ALT TABBED WINDOW
                    //IS ALT TABBED WINDOW
                    //IS ALT TABBED WINDOW
                    //IS ALT TABBED WINDOW




                    uint lpdwProcessId;
                    uint foreThread;
                    uint appThread;

                    /*uint lpdwProcessId;
                    uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                    uint appThread = GetCurrentThreadId();

                    AttachThreadInput(foreThread, appThread, true);

                    /*BringWindowToTop(_hWnd);

                    ShowWindow(_hWnd, SW_SHOW);

                    AttachThreadInput(foreThread, appThread, false);





                    double altw = (Math.Ceiling(0.00416666666666666666666666666667d * screenWidth) + screenWidth); //1928*1040
                    double alth = (Math.Ceiling(0.96296296296296296296296296296296d * screenHeight)); //1928*1040


                    // ||(rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight - 1
                    if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                        (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight - 1 ||
                        isfullscreenmsedgew == rectmicrosoftedge.Right && isfullscreenmsedgeh == rectmicrosoftedge.Bottom || iswinmax ||
                                (rectmicrosoftedge.Right) == altw && (rectmicrosoftedge.Bottom) == alth)
                    {

                        //Microsoft edge
                        if (isfullscreenmsedgew == rectmicrosoftedge.Right && isfullscreenmsedgeh == rectmicrosoftedge.Bottom)
                        {
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                            if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                            {
                                //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                sccsr14sc.Form1.wasf11pressedbynativeonedge = 2;
                            }
                            //Program.MessageBox((IntPtr)0, "1window is fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom + "/altw:" + isfullscreenmsedgew + "/alth:" + isfullscreenmsedgeh, "sccs", 0);


                            //Console.WriteLine("" + "1window is fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom + "/altw:" + isfullscreenmsedgew + "/alth:" + isfullscreenmsedgeh);


                            //Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                           // Program.MessageBox((IntPtr)0, "1window was fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);
                        }
                        else
                        {



                            if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                                (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight - 1 ||
                                (rectmicrosoftedge.Right) == altw && (rectmicrosoftedge.Bottom) == alth)
                            {
                                if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                {

                                    if (!iswinmax)
                                    {
                                        //uint lpdwProcessId;
                                        /* foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                        appThread = GetCurrentThreadId();

                                        AttachThreadInput(foreThread, appThread, true);

                                        /*BringWindowToTop(_hWnd);

                                        ShowWindow(_hWnd, SW_SHOW);

                                        if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                        {
                                            SetFocus(_hWnd, "");
                                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                            SetFocus(_hWnd, "");
                                            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                        }

                                        AttachThreadInput(foreThread, appThread, false);




                                        //uint lpdwProcessId;
                                        foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                        appThread = GetCurrentThreadId();

                                        AttachThreadInput(foreThread, appThread, true);

                                        /*BringWindowToTop(_hWnd);

                                        ShowWindow(_hWnd, SW_SHOW);

                                        if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                        {
                                            SetFocus(_hWnd, "");
                                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                            SetFocus(_hWnd, "");
                                            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                        }

                                        AttachThreadInput(foreThread, appThread, false);

                                        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                                        Program.RECT therect = new Program.RECT();
                                        therect.Left = 0;
                                        therect.Top = 0;
                                        therect.Bottom = screenHeight;
                                        therect.Right = screenWidth;

                                        param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                        Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);



                                        //Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                                        //Program.MessageBox((IntPtr)0, "3window was maximized " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);


                                        //Program.MessageBox((IntPtr)0, "1window was maximized " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);

                                    }
                                    else
                                    {





                                        //uint lpdwProcessId;
                                        /*foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                        appThread = GetCurrentThreadId();

                                       AttachThreadInput(foreThread, appThread, true);

                                       BringWindowToTop(_hWnd);

                                       ShowWindow(_hWnd, SW_SHOW);
                                       */
                        /*if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                        {
                            SetFocus(_hWnd, "");
                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                            SetFocus(_hWnd, "");
                            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                        }

                        AttachThreadInput(foreThread, appThread, false);


                    }


                    sccsr14sc.Form1.wasf11pressedbynativeonedge = 2;
                }
            }
        }


        //Program.MessageBox((IntPtr)0, "window is fullscreen " + iswinmax, "sccs", 0);

        //SetFocus(_hWnd, "");
        //// Program.MessageBox((IntPtr)0,"/w:" + (rectmicrosoftedge.Right) + "/h:" + (rectmicrosoftedge.Bottom), "sccs", 0);
        /*const int GWL_STYLE = -16; //want to change the window style
        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

        if (sult == IntPtr.Zero)
        {
            //in some cases SWL just outright fails, so we can notify the user and abort
            //MessageBox.Show("Unable to alter window style.\nSorry.");
            //return;
            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

        }
        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

        //SetWindowRgn(_hWnd, CreateRoundRectRgn(0, 0, screenWidth, screenHeight, 0, 0), true);
        SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);





        //SetFocus(_hWnd, "");

    }
    else
    {

        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
        Program.RECT therect = new Program.RECT();
        therect.Left = 0;
        therect.Top = 0;
        therect.Bottom = screenHeight;
        therect.Right = screenWidth;

        param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
        Program.SetWindowPlacement(_hWnd, ref param);


        //uint lpdwProcessId;
        foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

        appThread = GetCurrentThreadId();

        AttachThreadInput(foreThread, appThread, true);

        /*BringWindowToTop(_hWnd);

        ShowWindow(_hWnd, SW_SHOW);

        if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
        {
            SetFocus(_hWnd, "");
            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
            SetFocus(_hWnd, "");
            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
        }

        AttachThreadInput(foreThread, appThread, false);






       // Program.MessageBox((IntPtr)0, "2window was less than maximized " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);

        //check if width and height values are different for gimp/firefox/chrome/opera when doing F11 to maximize.
        //isfullscreenmsedgew == rectmicrosoftedge.Right && isfullscreenmsedgeh == rectmicrosoftedge.Bottom)


        //window is maximized or fullscreen
        //Program.MessageBox((IntPtr)0, "window is not maximized", "sccs", 0);


        /*
        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
        Program.RECT therect = new Program.RECT();



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

        //sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinmax:" + iswinmax, "sccsmsg", 0);

        //var iswinfullscreen = sccsr14sc.Form1.IsWindowWS_EX_OVERLAPPEDWINDOW(_hWnd);

        //sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinfullscreen:" + iswinfullscreen, "sccsmsg", 0);



        SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

        //sccsr14sc.Form1.MessageBox((IntPtr)0, "1iswinmax:" + iswinmax, "sccsmsg", 0);

        if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
        {


            /*
            var refreshDXEngineAction = new Action(delegate
            {
                SetFocus(_hWnd, "");

            });
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);


            uint lpdwProcessId;
            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(),out lpdwProcessId); //IntPtr.Zero

            uint appThread = GetCurrentThreadId();

            Console.WriteLine("foreThread:" + foreThread + "/appThread:" + appThread + "/lpdwProcessId:" + lpdwProcessId);

            AttachThreadInput(foreThread, appThread, true);
            SetFocus(_hWnd, "");

            if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
            {
                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
            }
            SetFocus(_hWnd, "");
            AttachThreadInput(foreThread, appThread, false);


            /*
            refreshDXEngineAction = new Action(delegate
            {
                SetFocus(_hWnd, "");

            });
            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);







            Console.WriteLine("captured program is topmost0. iHandle:");

        }
        else
        {
            //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
            Console.WriteLine("captured program is not topmost0. iHandle:");



        }
        */
                        //Program.MessageBox((IntPtr)0,"/w:" + (rectmicrosoftedge.Right) + "/h:" + (rectmicrosoftedge.Bottom), "sccs", 0);
                        /*const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

                        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        //SetWindowRgn(_hWnd, CreateRoundRectRgn(0, 0, screenWidth, screenHeight, 0, 0), true);
                        SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);



                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        if (!iswinmax)
                        {

                        }
                        else if (iswinmax)
                        {


                        }
                    }
                }




                //0.01081916537867078825347758887172
                //0.05740740740740740740740740740741







                //1941 / 1142 => 


                iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);
               // Program.MessageBox((IntPtr)0, "2window is fullscreen " + iswinmax + "/w:" + rectmicrosoftedge.Right + "/h:" + rectmicrosoftedge.Bottom, "sccs", 0);

            */



                        //Console.WriteLine(altcapturedwindowname);


                        /*

                        int iHandle = sccsr14sc.Form1.FindWindow(null, capturedwindowname);// "VoidExpanse");

                        Console.WriteLine(iHandle);

                        Console.WriteLine(capturedwindowname);
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                         


                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        //WORKING
                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        //WORKING


                        //iHandle = scgraphicssec.FindWindow(null, Program.capturedwindowname);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);

                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);

                        Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);
                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                        */










                        /*

                        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));






                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);









                        Program.RECT therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;
                        param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);



                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOP, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                        */
























                        //set the window to a borderless style
                        /*const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);

                        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                        *///MIT-LICENSE-RichardBrass-BorderlessFullscreen
                          //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                          //MIT-LICENSE-RichardBrass-BorderlessFullscreen























                        /*

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







                            if (!iswinmax)
                            {



                            }
                            else if (iswinmax)
                            {

                                SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                                Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                                //sccsr14sc.Form1.MessageBox((IntPtr)0, "1iswinmax:" + iswinmax, "sccsmsg", 0);
                                SetFocus(_hWnd, "");

                                if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                                {


                                    if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                    {
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
                                    SetFocus(_hWnd, "");
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
                        }*/



                        //iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);


                        //sccsr14sc.Form1.MessageBox((IntPtr)0, "1iswinmax:" + iswinmax, "sccsmsg", 0);


                        //sccsr14sc.Form1.someform.hooker(_hWnd);






                        //Program.EnumWindows(Program.enumWindowProc, IntPtr.Zero);
                        /*
                        Program.enumWindowProc(_hWnd, IntPtr.Zero);
                        */


                        /*var _hwndSource = HwndSource.FromHwnd(_hWnd);
                        if (_hwndSource != null)
                            _hwndSource.AddHook(sccsr14sc.Form1.WndProc);
                        */
                        /*
                        sccsr14sc.Form1.someform.hooker(_hWnd);
                        */



                        //SetFocus(_hWnd, "");
                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);







                        /*
                        Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        Thread.Sleep(1);
                        Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/
                        /* Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                         */

                        /*sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        Thread.Sleep(1);
                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        */

                        /*
                        SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                       */



                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                        //sccs.scgraphics.scgraphicssec.
                        /*
                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        Thread.Sleep(1);
                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        */

                        /*
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        */
                        /*Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        Thread.Sleep(1);
                        Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/




                        /*
                        rectmicrosoftedge = new Program.RECT();
                        Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);

                        if ((rectmicrosoftedge.Right - rectmicrosoftedge.Left) != screenWidth || (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) != screenHeight)
                        {
                            var sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_POPUP));//Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW

                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                               // Program.MessageBox((IntPtr)0, "SetWindowLongPtr", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                            GC.SuppressFinalize(sult);
                            DeleteObject(sult);
                        }
                        else
                        {
                            //sccsr14sc.Form1.MessageBox((IntPtr)0, "rects are equal", "sccsmsg", 0);

                        }
                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                        //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        */



                        /*
                        int iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, p.MainWindowTitle);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                        */

                        /*
                        sccsr14sc.Form1.SetFocus(_hWnd);
                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);*/
                        /*screenWidth = Program.GetSystemMetrics(0);
                        screenHeight = Program.GetSystemMetrics(1);

                        SetWindowRgn(_hWnd, CreateRoundRectRgn(0, 0, screenWidth, screenHeight, 0, 0), true);
                        //SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);






                        //Program.EnumWindows(Program.enumWindowProc, IntPtr.Zero);
                        //Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);



                        if ((rectmicrosoftedge.Right - rectmicrosoftedge.Left) != screenWidth || (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) != screenHeight)
                        {
                            /*var therect = new Program.RECT();
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
                            *
                            /*therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(_hWnd, ref param);
                            */

                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);





                        /*
                        SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);




                        Process currentProcess = Process.GetCurrentProcess();

                        // Get all processes running on the local computer.
                        Process[] localAll = Process.GetProcesses();

                        foreach (Process p in localAll)
                        {
                            if (p.ProcessName == altcapturedwindowname)
                            {
                                //Console.WriteLine("FIREFOX IS RUNNING");

                            }

                            if (p.ProcessName.Contains("edge") || p.ProcessName.Contains("msedge") || p.ProcessName.Contains("microsoft edge") || p.ProcessName.Contains("Microsoft Edge") || p.ProcessName.Contains("MicrosoftEdge") || p.ProcessName.Contains("microsoftedge"))
                            {
                                //Console.WriteLine("process list" + p.ProcessName);
                                if (p.ProcessName.Contains("MicrosoftEdge"))
                                {
                                    Console.WriteLine("AAAAAAAAAAAAAAAAA" + p.ProcessName);

                                }

                                /*
                                if (p.StartInfo.WindowStyle == ProcessWindowStyle.Maximized)
                                {
                                   // Program.MessageBox((IntPtr)0, "ProcessWindowStyle.Maximized", "sccs message 1", 0);

                                }





                                if (p.MainWindowTitle != "")
                                {
                                    Console.WriteLine("TESTESTEFETESt" + p.MainWindowTitle);

                                    //if (p.MainWindowTitle.Contains(capturedwindowname))
                                    {
                                        //Console.WriteLine("------------------MICRoSOFT EDGE IS RUNNING");



                                        int iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, p.MainWindowTitle);
                                        if (iHandle != 0)
                                        {
                                           // Program.MessageBox((IntPtr)0, "0found handle " + iHandle, "sccs message 1", 0);

                                            /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, (screenWidth << 16) | 1);
                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                                        }






                                    }
                                }


                            }

                        }



                        var currentProcesses = Process.GetProcessesByName("MicrosoftEdge");



                        foreach (Process p in currentProcesses)
                        {
                            /*if (p.Id)
                            {

                            }

                            Console.WriteLine("p.Id:" + p.Id);
                        }



                        currentProcesses = Process.GetProcessesByName("Microsoft Edge");



                        foreach (Process p in currentProcesses)
                        {
                            /*if (p.Id)
                            {

                            }

                            Console.WriteLine("p.Id:" + p.Id);
                        }



                        currentProcesses = Process.GetProcessesByName("msedge");


                        foreach (Process p in currentProcesses)
                        {
                            /*if (p.Id)
                            {

                            }

                            Console.WriteLine("p.Id:" + p.Id);
                        }





                        var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                        if (RunningProcessPaths.Contains("msedge.exe"))
                        {






                            /*
                            //Dictionary<string, IntPtr> edgelist = new Dictionary<string, IntPtr>< string, IntPtr> ();// IntPtr.Zero;

                            List<IntPtr> edgelistptr = new List<IntPtr>();// IntPtr.Zero;
                            List<string> edgeliststr = new List<string>();

                            Process[] processlist = Process.GetProcesses();

                            string processname = "";
                            foreach (Process process in processlist)
                            {

                                //Program.MessageBox((IntPtr)0, "edge" + " " + processes.ProcessName, "sccoresystems0", 0);

                                if (process.ProcessName.ToLower() == "msedge")
                                {
                                    //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                                    //Program.MessageBox((IntPtr)0, "0edge" + " " + process.MainWindowHandle, "sccoresystems0", 0);
                                    //vewindowsfoundedz = process.MainWindowHandle;
                                    //edge = process.MainWindowHandle;
                                    //processname = process.ProcessName;
                                    edgelistptr.Add(process.MainWindowHandle);
                                    edgeliststr.Add(process.ProcessName);
                                    //break;
                                }                       
                            }



                            for (int i = 0; i < edgelistptr.Count; i++)
                            {
                                var title = new StringBuilder(1024);
                                GetWindowText(edgelistptr[i], title, title.Capacity);

                                //if (string.IsNullOrWhiteSpace(title.ToString()))

                                Program.GetWindowThreadProcessId(edgelistptr[i], out var processId);
                                // ignore by process name
                                var process = Process.GetProcessById((int)processId);

                                var pname = $"{title} ({process.ProcessName}.exe)";

                                int iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, edgeliststr[i]);// "VoidExpanse"); //process.ProcessName

                                //int iHandle = FindWindow(null, "VoidExpanse");
                                if (iHandle != 0)
                                {
                                   // Program.MessageBox((IntPtr)0, "found handle " + iHandle, "sccs message 1", 0);

                                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, (screenWidth << 16) | 1);
                                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                                }

                            }
                            */



                        /*
                        for (int i = 0; i < edgelistptr.Count; i++)
                        {
                            var title = new StringBuilder(1024);
                            NativeMethodsother.GetWindowText(Program.vewindowsfoundedz, title, title.Capacity);

                            //if (string.IsNullOrWhiteSpace(title.ToString()))

                            NativeMethodsother.GetWindowThreadProcessId(Program.vewindowsfoundedz, out var processId);
                            // ignore by process name
                            var process = Process.GetProcessById((int)processId);

                            var pname = $"{title} ({process.ProcessName}.exe)";

                            int iHandle = FindWindow(null, edgeliststr[i]);// "VoidExpanse"); //process.ProcessName

                            //int iHandle = FindWindow(null, "VoidExpanse");
                            if (iHandle != 0)
                            {
                                //Program.MessageBox((IntPtr)0, "found handle " + iHandle, "sccs message 1", 0);

                                SendMessage(iHandle, WM_LBUTTONDOWN, 0, (mousex << 16) | mousey);
                                SendMessage(iHandle, WM_LBUTTONUP, 0, 0);
                            }                            

                        }*/















                        /*
                        Process[] Edge = Process.GetProcessesByName("MicrosoftEdge");
                        foreach (Process Item in Edge)
                        {

                        }*/






                        /*
                        uint lpdwProcessId;
                        var pID = sccsr14sc.Form1.GetWindowThreadProcessId(_hWnd, out lpdwProcessId);

                        var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, gotval);
                        int msg0 = sccs.scgraphics.scgraphicssec.SendMessage((int)pID, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 1);
                        int msg1 = sccs.scgraphics.scgraphicssec.SendMessage((int)pID, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);





                        Console.WriteLine("captured program is not topmost0. pID:" + pID + "/msg0:" + msg0 + "/msg1:" + msg1);
                        */



                        //SetFocus(pID);

                        /*
                        int iHandle =  scgraphicssec.FindWindow(null, Process.GetCurrentProcess().ProcessName); //(int)pID;/













                                            /*
                                            string gotval;
                                            RunningProcessPaths.TryGetValue("msedge.exe", out gotval);





                                            //Console.WriteLine("found browser microsoft edge");

                                            var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, gotval);
                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 5);
                                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);


                                            if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                                            {


                                                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                                Console.WriteLine("captured program is topmost0. iHandle:" + iHandle + " gotval " + gotval);

                                            }
                                            else
                                            {
                                                //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
                                                Console.WriteLine("captured program is not topmost0. iHandle:" + iHandle + " gotval " + gotval);



                                            }
                    }



                    /*
                    var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, "msedge");
                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 5);
                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);


                    if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                    {


                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                        Console.WriteLine("captured program is topmost0. iHandle:" + iHandle);

                    }
                    else
                    {
                        //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
                        Console.WriteLine("captured program is not topmost0. iHandle:" + iHandle);



                    }
                    */




                        /*var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, Program.capturedwindowname);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 3);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);

                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                        */
                        /*if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                        {
                            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                        }
                    }
                    else
                    {
                        /*var therect = new Program.RECT();
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
                        Program.SetWindowPlacement(_hWnd, ref param);*/

                        /*therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);
                        */





                        /*
                        SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        */


                        /*Process currentProcess = Process.GetCurrentProcess();

                        // Get all processes running on the local computer.
                        Process[] localAll = Process.GetProcesses();

                        foreach (Process p in localAll)
                        {
                            if (p.ProcessName == altcapturedwindowname)
                            {
                                //Console.WriteLine("FIREFOX IS RUNNING");

                            }

                            /*if (p.ProcessName.Contains("edge") || p.ProcessName.Contains("msedge") || p.ProcessName.Contains("microsoft edge") || p.ProcessName.Contains("Microsoft Edge") || p.ProcessName.Contains("MicrosoftEdge") || p.ProcessName.Contains("microsoftedge"))
                            {
                                //Console.WriteLine("process list" + p.ProcessName);
                                if (p.ProcessName.Contains("MicrosoftEdge"))
                                {
                                    Console.WriteLine("process list" + p.ProcessName);

                                }

                            }
                            */





                        /*
                        uint lpdwProcessId;
                        var pID = sccsr14sc.Form1.GetWindowThreadProcessId(_hWnd, out lpdwProcessId);

                        if (p.Id == pID)
                        {
                            Console.WriteLine("TESTSTSTETETSTEDWsdgsbhdsd");

                        }

                    }





                        var currentProcesses = Process.GetProcessesByName("MicrosoftEdge");



                        foreach (Process p in currentProcesses)
                        {
                            /*if (p.Id)
                            {

                            }

                            Console.WriteLine("p.Id:" + p.Id);
                        }



                        currentProcesses = Process.GetProcessesByName("Microsoft Edge");



                        foreach (Process p in currentProcesses)
                        {
                            /*if (p.Id)
                            {

                            }

                            Console.WriteLine("p.Id:" + p.Id);
                        }



                        currentProcesses = Process.GetProcessesByName("msedge");


                        foreach (Process p in currentProcesses)
                        {
                            /*if (p.Id)
                            {

                            }

                            Console.WriteLine("p.Id:" + p.Id);
                        }








                        var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                        if (RunningProcessPaths.Contains("msedge.exe"))
                        {




                            Process currentProcess = Process.GetCurrentProcess();

                            // Get all processes running on the local computer.
                            Process[] localAll = Process.GetProcesses();

                            foreach (Process p in localAll)
                            {
                                if (p.ProcessName == altcapturedwindowname)
                                {
                                    //Console.WriteLine("FIREFOX IS RUNNING");

                                }

                                if (p.ProcessName.Contains("edge") || p.ProcessName.Contains("msedge") || p.ProcessName.Contains("microsoft edge") || p.ProcessName.Contains("Microsoft Edge") || p.ProcessName.Contains("MicrosoftEdge") || p.ProcessName.Contains("microsoftedge"))
                                {
                                    //Console.WriteLine("process list" + p.ProcessName);
                                    if (p.ProcessName.Contains("MicrosoftEdge"))
                                    {
                                        Console.WriteLine("process list" + p.ProcessName);

                                    }

                                    Program.GetWindowThreadProcessId(p.MainWindowHandle, out var processId);
                                    // ignore by process name
                                    var process = Process.GetProcessById((int)processId);

                                    /*var pname = $"{p.MainWindowTitle} ({process.ProcessName})";

                                    Console.WriteLine("pname:" + pname);


                                    int iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, pname);// "VoidExpanse"); //process.ProcessName

                                    //int iHandle = FindWindow(null, "VoidExpanse");
                                    if (iHandle != 0)
                                    {
                                       // Program.MessageBox((IntPtr)0, "found handle " + iHandle, "sccs message 1", 0);

                                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, (screenWidth << 16) | 1);
                                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                                    }




                                }


                                /*uint lpdwProcessId;
                                var pID = sccsr14sc.Form1.GetWindowThreadProcessId(_hWnd, out lpdwProcessId);
                                */
                        /*if (p.Id == pID)
                        {
                            Console.WriteLine("TESTSTSTETETSTEDWsdgsbhdsd");

                        }
                    }


                    //Dictionary<string, IntPtr> edgelist = new Dictionary<string, IntPtr>< string, IntPtr> ();// IntPtr.Zero;

                    /*List<IntPtr> edgelistptr = new List<IntPtr>();// IntPtr.Zero;
                    List<string> edgeliststr = new List<string>();

                    Process[] processlist = Process.GetProcesses();

                    string processname = "";
                    foreach (Process process in processlist)
                    {

                        //Program.MessageBox((IntPtr)0, "edge" + " " + processes.ProcessName, "sccoresystems0", 0);

                        if (process.ProcessName.ToLower() == "msedge")
                        {
                            //Console.WriteLine("Process: {0} ID: {1} Window title: {2}", process.ProcessName, process.Id, process.MainWindowTitle);
                            //Program.MessageBox((IntPtr)0, "1edge" + " " + process.MainWindowHandle, "sccoresystems0", 0);
                            //vewindowsfoundedz = process.MainWindowHandle;
                            //edge = process.MainWindowHandle;
                            //processname = process.ProcessName;
                            edgelistptr.Add(process.MainWindowHandle);
                            edgeliststr.Add(process.ProcessName);
                            //break;
                        }
                    }



                    for (int i = 0; i < edgelistptr.Count; i++)
                    {
                        var title = new StringBuilder(1024);
                        GetWindowText(edgelistptr[i], title, title.Capacity);

                        //if (string.IsNullOrWhiteSpace(title.ToString()))

                        Program.GetWindowThreadProcessId(edgelistptr[i], out var processId);
                        // ignore by process name
                        var process = Process.GetProcessById((int)processId);

                        var pname = $"{title} ({process.ProcessName}.exe)";

                        int iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, edgeliststr[i]);// "VoidExpanse"); //process.ProcessName

                        //int iHandle = FindWindow(null, "VoidExpanse");
                        if (iHandle != 0)
                        {
                           // Program.MessageBox((IntPtr)0, "found handle " + iHandle, "sccs message 1", 0);

                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, (screenWidth << 16) | 1);
                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                        }

                    }*/


                        /*

                        uint lpdwProcessId;
                        var pID = sccsr14sc.Form1.GetWindowThreadProcessId(_hWnd, out lpdwProcessId);



                        var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, gotval);
                        int msg0 = sccs.scgraphics.scgraphicssec.SendMessage((int)pID, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 1);
                        int msg1 = sccs.scgraphics.scgraphicssec.SendMessage((int)pID, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);





                        Console.WriteLine("captured program is not topmost0. pID:" + pID + "/msg0:" + msg0 + "/msg1:" + msg1);
                        */
                        /*string gotval;
                        RunningProcessPaths.TryGetValue("msedge.exe", out gotval);





                        //Console.WriteLine("found browser microsoft edge");

                        var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, gotval);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 5);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);


                        if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                        {


                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                            Console.WriteLine("captured program is topmost0. iHandle:" + iHandle + " gotval " + gotval);

                        }
                        else
                        {
                            //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
                            Console.WriteLine("captured program is not topmost0. iHandle:" + iHandle + " gotval " + gotval);



                        }
                    }



                    /*var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, "msedge");
                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 5);
                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);


                    if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                    {


                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                        Console.WriteLine("captured program is topmost0. iHandle:" + iHandle);

                    }
                    else
                    {
                        //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
                        Console.WriteLine("captured program is not topmost0. iHandle:" + iHandle);



                    }*/


                        /*
                        screenWidth = Program.GetSystemMetrics(0);
                        screenHeight = Program.GetSystemMetrics(1);

                        //SetWindowRgn(Program.vewindowsfoundedz, CreateRoundRectRgn(0, 0, 800, 600, 20, 20), true);
                        SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);*/


                        /*var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, Program.capturedwindowname);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 3);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);

                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                        */
                        /*if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                        {
                            sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                        }
                    }



                    /*var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, Program.capturedwindowname);
                    //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                    //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);

                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                    sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);

                    Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                    Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/
                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                        //Program.SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(Program.GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //sccsr14sc.Form1.SetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE, (IntPtr)(sccsr14sc.Form1.GetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE) | sccsr14sc.Form1.WS_EX_TOPMOST));

                        /*var sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_POPUP));//Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "SetWindowLongPtr", "sccs", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                        
                        GC.SuppressFinalize(sult);
                        DeleteObject(sult);
                        */





                        /*if (sccsr14sc.Form1.wasf11pressedonfirefox == 0)
                        {
                            sccsr14sc.Form1.wasf11pressedonfirefox = 1;
                        }
                        else if (sccsr14sc.Form1.wasf11pressedonfirefox == 1)
                        {
                            sccsr14sc.Form1.wasf11pressedonfirefox = 0;
                        }*/


                        /*rectmicrosoftedge = new Program.RECT();
                        Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);

                        if ((rectmicrosoftedge.Right - rectmicrosoftedge.Left) != screenWidth || (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) != screenHeight)
                        {
                            /*var sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_POPUP));//Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW

                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                               // Program.MessageBox((IntPtr)0, "SetWindowLongPtr", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                            GC.SuppressFinalize(sult);
                            DeleteObject(sult);
                        }
                        else
                        {
                            //sccsr14sc.Form1.MessageBox((IntPtr)0, "rects are equal", "sccsmsg", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                    */













                        /*var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, Program.capturedwindowname);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);

                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        
                        Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/
                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                        //Program.SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(Program.GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //sccsr14sc.Form1.SetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE, (IntPtr)(sccsr14sc.Form1.GetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE) | sccsr14sc.Form1.WS_EX_TOPMOST));

                        /*var sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_POPUP));//Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "SetWindowLongPtr", "sccs", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);


                        GC.SuppressFinalize(sult);
                        DeleteObject(sult);
                        */


                        /*SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                        //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                        var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                        if (sccsr14sc.Form1.haspressedf11 == 0)
                        {
                            sccsr14sc.Form1.haspressedf11 = 1;
                        }
                        else if (sccsr14sc.Form1.haspressedf11 == 1)
                        {
                            sccsr14sc.Form1.haspressedf11 = 0;
                        }



                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                        */


                        //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
                        /*hWnd = FindWindow(null, capturedwindowname);

                        if (_hWnd == null) {

                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                           // Program.MessageBox((IntPtr)0, "Failed to acquire window handle.\nPlease try again.", "sccs", 0);

                        }
                        */

                        //set the window to a borderless style
                        /*const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_OVERLAPPED));
                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                           // Program.MessageBox((IntPtr)0, "Unable to alter window style.\nSorry.", "sccs", 0);

                        }
                        //otherwise we need to resize and reposition the window to take up the full screen
                        const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                        screenWidth = Program.GetSystemMetrics(0);
                        screenHeight = Program.GetSystemMetrics(1);
                        Program.SetWindowPos(_hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                        */

                        /*therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                        */


                        //iHandle = scgraphicssec.FindWindow(null, Program.capturedwindowname);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        /*scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                        scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_LBUTTONUP, 0, 0);

                        scgraphicssec.SendMessage(iHandle, (uint)WM_KEYDOWN, (int)VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                        scgraphicssec.SendMessage(iHandle, (uint)WM_KEYUP, (int)VirtualKeyCode.F11, 0);
                        */
                        //Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        //Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);





                        /*screenWidth = Program.GetSystemMetrics(0);
                        screenHeight = Program.GetSystemMetrics(1);

                        //SetWindowRgn(Program.vewindowsfoundedz, CreateRoundRectRgn(0, 0, 800, 600, 20, 20), true);
                        SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);
                        */










                        /*
                        GC.SuppressFinalize(sult);
                        DeleteObject(sult);*/
                        /*GC.SuppressFinalize(_hWnd);
                        DeleteObject(_hWnd);*/


                        /*SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);

                      

                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);

                        var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_OVERLAPPED);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                            //Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                        */


                        /*
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                        */



                        /*
                       therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        */



                        /*const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border

                        //(UIntPtr)(Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW)
                        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_OVERLAPPED)); //

                        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                        */

                        /*const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border

                        //(UIntPtr)(Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW)
                        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_POPUP)); //

                        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                        */




                        //SetWindowLong(_hWnd, GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                        //Program.SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        /*PostMessage(Program.vewindowsfoundedz, WM_KEYDOWN, (int)VirtualKeyCode.F11, 0);
                        //WORKING
                        PostMessage(Program.vewindowsfoundedz, WM_KEYUP, (int)VirtualKeyCode.F11, 0);
                        //WORKING
                        */

                        /*
                        Program.keyboardsim.KeyDown(VirtualKeyCode.F11);

                        Program.keyboardsim.KeyUp(VirtualKeyCode.F11);*/


                        /*
                        int iHandle = FindWindow(null, Program.capturedwindowname);// "VoidExpanse");
                        Console.WriteLine(iHandle);


                        Console.WriteLine(Program.capturedwindowname);
                        //button1exit.PerformClick();
                        Program.GetCursorPos(out somepoint);

                        mousex = somepoint.X;
                        mousey = somepoint.Y;

                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        //iHandle = scgraphicssec.FindWindow(null, Program.capturedwindowname);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                        scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_LBUTTONUP, 0, 0);

                        scgraphicssec.SendMessage(iHandle, (uint)WM_KEYDOWN, (int)VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                        scgraphicssec.SendMessage(iHandle, (uint)WM_KEYUP, (int)VirtualKeyCode.F11, 0);

                        //Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        //Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);
                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                        */
                        /* PostMessage(Program.vewindowsfoundedz, (uint)WM_KEYDOWN, (int)VirtualKeyCode.F11, 0);
                         //WORKING
                         PostMessage(Program.vewindowsfoundedz, (uint)WM_KEYUP, (int)VirtualKeyCode.F11, 0);
                         //WORKING
                        */

                        /*

                        //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                        Program.RECT therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 1080;
                        therect.Right = 1920;

                        param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        */

                        /*this.Size = new System.Drawing.Size(1920, 1080);
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.WindowState = FormWindowState.Maximized;
                        this.TopMost = true;

                        Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                        */
                        //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));












                        /*

                        Program.RECT therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;
                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);



                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOP, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        //set the window to a borderless style
                        /*const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

                        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        */





                        //set the window to a borderless style
                        /* const int GWL_STYLE = -16; //want to change the window style
                         const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                         IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

                         //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                         if (sult == IntPtr.Zero)
                         {
                             //in some cases SWL just outright fails, so we can notify the user and abort
                             //MessageBox.Show("Unable to alter window style.\nSorry.");
                             //return;
                             //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                            // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                         }
                         Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                         *///MIT-LICENSE-RichardBrass-BorderlessFullscreen
                           //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                           //MIT-LICENSE-RichardBrass-BorderlessFullscreen





























                        /*therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = 1920;
                        param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);               

                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
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


                        /*
                        var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                           // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                        //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                        //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                        //MIT-LICENSE-RichardBrass-BorderlessFullscreen


                        */

                        /*
                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
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
                        */

                        /* int iHandle = sccsr14sc.Form1.FindWindow(null, capturedwindowname);// "VoidExpanse");

                         Console.WriteLine(iHandle);

                         Console.WriteLine(capturedwindowname);



                         Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                         /*sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                         //WORKING
                         sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                         //WORKING*/


                        //iHandle = scgraphicssec.FindWindow(null, Program.capturedwindowname);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        /* sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                         sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                         *//*
                         sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                         sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                         */
                        /*Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/
                        /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                        */

                        /*
                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);


                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);


                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                        */

                    }
                    else if (altcapturedwindowname.Contains("firefox"))
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


                            //sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinmax:" + iswinmax, "sccsmsg", 0);
                            /*
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
                                //SetFocus(_hWnd, "");

                                if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                                {


                                    if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                    {
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
                        Console.WriteLine("void expanse: " + altcapturedwindowname);

                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);


                        //SetFocus(_hWnd, "");







                        //SOMETHING IS WRONG WITH THE WINDOW DIMENSIONS, DISPROPORTIONATE OF AT LEAST 20X TIMES MORE THAN THE MAXIMUM SOMETIMES.
                        //SOMETHING IS WRONG WITH THE WINDOW DIMENSIONS, DISPROPORTIONATE OF AT LEAST 20X TIMES MORE THAN THE MAXIMUM SOMETIMES.
                        Program.RECT rectmicrosoftedge = new Program.RECT();
                        Program.GetWindowRect(_hWnd, ref rectmicrosoftedge);








                        /*(rectmicrosoftedge.Right - rectmicrosoftedge.Left) == screenWidth && (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) == screenHeight ||
                            (rectmicrosoftedge.Right - rectmicrosoftedge.Left) == screenWidth && (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) == screenHeight - 1*/





                        ///Program.MessageBox((IntPtr)0, "/w:" + (rectmicrosoftedge.Right) + "/h:" + (rectmicrosoftedge.Bottom), "sccs", 0);
                        if ((rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight ||
                            (rectmicrosoftedge.Right) == screenWidth && (rectmicrosoftedge.Bottom) == screenHeight - 1)
                        {


                            var iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);

                            //Program.MessageBox((IntPtr)0, "window is fullscreen " + iswinmax, "sccs", 0);

                            //SetFocus(_hWnd, "");
                            //// Program.MessageBox((IntPtr)0,"/w:" + (rectmicrosoftedge.Right) + "/h:" + (rectmicrosoftedge.Bottom), "sccs", 0);
                            /*const int GWL_STYLE = -16; //want to change the window style
                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                            IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

                            //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                               // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                            //SetWindowRgn(_hWnd, CreateRoundRectRgn(0, 0, screenWidth, screenHeight, 0, 0), true);
                            SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);
                            */


                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);


                            if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                            {
                                //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                sccsr14sc.Form1.wasf11pressedbynativeonedge = 2;
                            }


                            //SetFocus(_hWnd, "");

                        }
                        else
                        {
                            //window is maximized or fullscreen
                            //Program.MessageBox((IntPtr)0, "window is not maximized", "sccs", 0);



                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                            Program.RECT therect = new Program.RECT();



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


                            var iswinmax = sccsr14sc.Form1.IsWindowMaximized(_hWnd);

                            //sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinmax:" + iswinmax, "sccsmsg", 0);

                            //var iswinfullscreen = sccsr14sc.Form1.IsWindowWS_EX_OVERLAPPEDWINDOW(_hWnd);

                            //sccsr14sc.Form1.MessageBox((IntPtr)0, "0iswinfullscreen:" + iswinfullscreen, "sccsmsg", 0);



                            SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                            //sccsr14sc.Form1.MessageBox((IntPtr)0, "1iswinmax:" + iswinmax, "sccsmsg", 0);

                            if (sccsr14sc.Form1.IsWindowTopMost(_hWnd))
                            {

                                //SetFocus(_hWnd, "");

                                /*if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                {
                                    Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                    sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                }*/
                                //SetFocus(_hWnd, "");


                                Console.WriteLine("captured program is topmost0. iHandle:");

                            }
                            else
                            {
                                //var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, capturedwindowname);
                                Console.WriteLine("captured program is not topmost0. iHandle:");



                            }
                            //Program.MessageBox((IntPtr)0,"/w:" + (rectmicrosoftedge.Right) + "/h:" + (rectmicrosoftedge.Bottom), "sccs", 0);
                            /*const int GWL_STYLE = -16; //want to change the window style
                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                            IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

                            //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                               // Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                            //SetWindowRgn(_hWnd, CreateRoundRectRgn(0, 0, screenWidth, screenHeight, 0, 0), true);
                            SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);
                            */


                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                            if (!iswinmax)
                            {

                            }
                            else if (iswinmax)
                            {


                            }




                        }









                    }




                    if (_hWnd == IntPtr.Zero)
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
                    _captureSession.StartCapture();
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

        public Texture2D TryGetNextFrameAsTexture2D(Device device)
        {
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

            }

            if (surfaceDxgiInterfaceAccess != null)
            {
                surfaceDxgiInterfaceAccess.Dispose();
                surfaceDxgiInterfaceAccess = null;
            }

            if (frame != null)
            {
                frame.Dispose();
                //frame = null;
            }


            return texture2d;
        }

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
            if (picker != null)
            {
                picker.Dispose();
                //sccsr14sc.Form1.MessageBox((IntPtr)0, "", "sccsmsg", 0);
                picker = null;
            }

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
            factory = WindowsRuntimeMarshal.GetActivationFactory(typeof(GraphicsCaptureItem));
            interop = (IGraphicsCaptureItemInterop)factory;
            pointer = interop.CreateForWindow(hWnd, typeof(GraphicsCaptureItem).GetInterface("IGraphicsCaptureItem").GUID);
            capture = Marshal.GetObjectForIUnknown(pointer) as GraphicsCaptureItem;
            DeleteObject(pointer);
            return capture;
        }

        private void CaptureItemOnClosed(GraphicsCaptureItem sender, object args)
        {
            StopCapture();
        }
    }
}