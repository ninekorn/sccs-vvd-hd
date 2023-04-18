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
using Device = SharpDX.Direct3D11.Device;
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
//using Win32.DwmSharedSurface;

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


using sccsvvdhd;

using System.Diagnostics;
using System.Runtime.InteropServices;

using SharpDX.RawInput;
using SharpDX.Multimedia;
using WinRT.GraphicsCapture;
using Win32.Shared.Interop;
using Win32.Shared;

using System.Runtime.Remoting.Messaging;

using sccsmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;
//using SharpDX.Direct3D9;
using SharpDX.DirectWrite;
using Win32.Shared.Interfaces;
using static sccs.Program;

using Win32;
using Win32.DwmSharedSurface;
using SharpDX.MediaFoundation.DirectX;
using System.IO;
using System.Drawing;
using System.Runtime.ConstrainedExecution;




namespace sccs
{
    internal static unsafe class Program
    {
        public static int usealttexture = 0;
        public static int createikrig = 1;

        public static System.Drawing.Bitmap thirdthebitmap;
        public static System.Drawing.Bitmap lastthebitmap;

        public static sccs.scgraphicssec sccsgraphicssec;
        public static sccsscreenframe screencaptureframe;
        public static sccssharpdxscreencapture sharpdxscreencapture;


        static Texture2D lasttexture2d;
        static Texture2D _texture2d;
        static int bmpstride;

        static int startformsswtc = 0;
        static int startthreadswtc = 0;
        static Thread _mainThread;
        static ICaptureMethod icap;
        static Texture2D texture2d;


        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);

        public static string lastcapturetypevalue = "";
        public static bool rundedicatedthreadapp = false;

        static int canworkuithread = 1;
        static int canworksysthread = 2;
        public static bool somerunapptype = false;
        public static bool lastrunapptype = false;
        static int createtimers = 0;
        public static int fpsCounterupdatethread = 0;
        public static int fpsCounterrenderthread = 0;

        public static Stopwatch clock;
        public static Stopwatch fpsTimerUpdateThread;
        public static Stopwatch fpsTimerShaderRenderThread;
        public static Stopwatch fpsTimerShaderPresentThread;

        public static int fpsTimerShaderPresentThreadswtc;
        public static int fpsTimerShaderRenderThreadswtc;
        public static int fpsTimerUpdateThreadswtc;


        public static int fpsTimerShaderPresentThreadcounter;
        public static int fpsTimerShaderRenderThreadcounter;
        public static int fpsTimerUpdateThreadcounter;



        //static DwmSharedSurface sometest;
        //static GraphicsCapture somegcap;

        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);

        static SwapChain1 swapChain1;

        public static string capturedwindowname = "";

        public static int lastscreencapturetype = 0;
        public static int changedscreencapturetype = 0;
        public static int screencapturetype = 0;

        //public static int usesharpdxscreencapture = 0;
        public static int _useOculusRift = 0;

        public static RenderForm somerenderform;
        public static sccsvvdhd.Form1 someform;
        static byte* srcPointer;
        static byte* dstPointer;


        public static IntPtr consoleHandle;

        public static int usethirdpersonview = 0;
        public static float offsetthirdpersonview = 0.35f;//at or over 1 to get a decent ootb working 3rdpersonview.
        public static int usetypeofvoxel = 0; //0.000000001f //0.0f
        public static int usejitterphysics = 0;
        public static int usejitterphysicsbuo = 0;
        public static int useArduinoOVRTouchKeymapper = 0;
        public static int useSendScreenToArduino = 0;

        public static JVector _world_gravity = new JVector(0, -9.81f, 0); //-9.81f base
        public static int worlditerations = 3; // as high as possible normally for higher precision
        public static int worldsmalliterations = 3; // as high as possible normally for higher precision
        public static float worldallowedpenetration = 0.00123f; //0.00123f  _world_gravity = new JVector(0, -9.81f, 0);
        public static bool allowdeactivation = true;

        public static int physicsengineinstancex = 1; //4
        public static int physicsengineinstancey = 1; //1
        public static int physicsengineinstancez = 1; //4

        public static int worldwidth = 1;
        public static int worldheight = 1;
        public static int worlddepth = 1;


        public static int exitedprogram = -1;
        public static scupdate updatescript;
        public static scsystemconfiguration config;
        public static int initdirectXmainswtch = -1;
        public static int initvrmainswtch = 2;
        public static int has_init_directx = 0;


        public static SharpDX.DirectInput.KeyboardState keyboardstate;
        //public static keyboardinput keyboardinput;
        //public static InputSimulator inputsim;
        //public static KeyboardSimulator keyboardsim;
        //public static MouseSimulator mousesim;


        static int MaxSizeMainObject = 16;
        public static int MaxSizeMessageObject = 32;
        static scmessageobject.scmessageobject[] mainreceivedmessages;//
        static scmessageobjectjitter[][] sccsjittertasks = null;
        static jitter_sc[] jitter_sc;





        public static uint testGetWindowThreadProcessId;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);





        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);

       /* [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
       */
        /*[DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);*/

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();


        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall, ExactSpelling = true, SetLastError = true)]
        internal static extern bool GetWindowRect(IntPtr hWnd, ref RECT rect);







        /*
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        */





        /// <summary>
        /// Sets the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpwndpl">
        /// A pointer to a WINDOWPLACEMENT structure that specifies the new show state and window positions.
        /// <para>
        /// Before calling SetWindowPlacement, set the length member of the WINDOWPLACEMENT structure to sizeof(WINDOWPLACEMENT). SetWindowPlacement fails if the length member is not set correctly.
        /// </para>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPlacement(IntPtr hWnd,
           [In] ref WINDOWPLACEMENT lpwndpl);

        /// <summary>
        /// Retrieves the show state and the restored, minimized, and maximized positions of the specified window.
        /// </summary>
        /// <param name="hWnd">
        /// A handle to the window.
        /// </param>
        /// <param name="lpwndpl">
        /// A pointer to the WINDOWPLACEMENT structure that receives the show state and position information.
        /// <para>
        /// Before calling GetWindowPlacement, set the length member to sizeof(WINDOWPLACEMENT). GetWindowPlacement fails if lpwndpl-> length is not set correctly.
        /// </para>
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is nonzero.
        /// <para>
        /// If the function fails, the return value is zero. To get extended error information, call GetLastError.
        /// </para>
        /// </returns>
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        //public static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }

            public static implicit operator System.Drawing.Point(POINT p)
            {
                return new System.Drawing.Point(p.X, p.Y);
            }

            public static implicit operator POINT(System.Drawing.Point p)
            {
                return new POINT(p.X, p.Y);
            }
        }
        public enum ShowWindowCommands
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
            /// <summary>
            /// Activates the window and displays it as a maximized window.
            /// </summary>      
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position.
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the
            /// STARTUPINFO structure passed to the CreateProcess function by the
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread
            /// that owns the window is not responding. This flag should only be
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }

        /// <summary>
        /// Contains information about the placement of a window on the screen.
        /// </summary>
        [Serializable]
        [StructLayout(LayoutKind.Sequential)]
        internal struct WINDOWPLACEMENT
        {
            /// <summary>
            /// The length of the structure, in bytes. Before calling the GetWindowPlacement or SetWindowPlacement functions, set this member to sizeof(WINDOWPLACEMENT).
            /// <para>
            /// GetWindowPlacement and SetWindowPlacement fail if this member is not set correctly.
            /// </para>
            /// </summary>
            public int Length;

            /// <summary>
            /// Specifies flags that control the position of the minimized window and the method by which the window is restored.
            /// </summary>
            public int Flags;

            /// <summary>
            /// The current show state of the window.
            /// </summary>
            public ShowWindowCommands ShowCmd;

            /// <summary>
            /// The coordinates of the window's upper-left corner when the window is minimized.
            /// </summary>
            public POINT MinPosition;

            /// <summary>
            /// The coordinates of the window's upper-left corner when the window is maximized.
            /// </summary>
            public POINT MaxPosition;

            /// <summary>
            /// The window's coordinates when the window is in the restored position.
            /// </summary>
            public RECT NormalPosition;

            /// <summary>
            /// Gets the default (empty) value.
            /// </summary>
            public static WINDOWPLACEMENT Default
            {
                get
                {
                    WINDOWPLACEMENT result = new WINDOWPLACEMENT();
                    result.Length = Marshal.SizeOf(result);
                    return result;
                }
            }
        }

  
        [StructLayout(LayoutKind.Sequential)]

        internal struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
        /*private static readonly string WINDOW_NAME = "TestTitle";  //name of the window
        private const int GWL_STYLE = -16;              //hex constant for style changing
        private const int WS_BORDER = 0x00800000;       //window with border
        private const int WS_CAPTION = 0x00C00000;      //window with a title bar
        private const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
        private const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox
        */

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();
        [DllImport(@"kernel32.dll", SetLastError = true)]
        static extern bool AllocConsole();
        [DllImport(@"user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SwHide = 0;
        const int SwShow = 5;







        static DataBox dataBox1;

        static int memoryBitmapStride;
        static int columns;
        static int rows;
        static IntPtr interptr1;






        static ShaderResourceView shaderResourceView;
        static int startmainthread = 0;
        static int bitmapcounter = 0;
        static int textureresetswtc = 0;
        static int alttextureresetswtc = 0;

        static System.Drawing.Bitmap _bitmap;
        static System.Drawing.Rectangle boundsRect;
        static System.Drawing.Imaging.BitmapData bmpData;
        static int _bytesTotal;
        static Texture2DDescription _textureDescription;
        static byte[] _textureByteArray;
        static Texture2D _texture2D;

        static ShaderResourceView lastshaderresourceview;

        static Texture2D lasttexture2D0;
        static Texture2D lasttexture2D1;

        static private IntPtr hWndParent;
        static private Process pDocked;
        static private IntPtr hWndOriginalParent;
        static private IntPtr hWndDocked;

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


     






        //public static Panel thepanel;


        //https://www.unknowncheats.me/forum/c/62019-c-non-hooked-external-directx-overlay.html

        private static Margins marg;

        internal struct Margins
        {
            public int Left, Right, Top, Bottom;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);


        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x00080000; //0x80000
        //public const int WS_EX_TRANSPARENT = 0x20;

        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;
        public const long WS_EX_TOPMOST = 0x00000008L;
        public const long WS_NOACTIVATE = 0x08000000L;
        const long WS_EX_WINDOWEDGE = 0x00000100L;
        public const long WS_EX_TRANSPARENT = 0x00000020L;
        public const long WS_EX_CLIENTEDGE = 0x00000200L;
        const long WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE;


        [DllImport("user32.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins pMargins);



        /*[DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        public static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, bottom, right;
        }*/
        //public static RenderForm form;


        public static readonly string WINDOW_NAME = "voidexpanse";  //name of the window
        public const int GWL_STYLE = -16;              //hex constant for style changing
        public const int WS_BORDER = 0x00800000;       //window with border
        public const int WS_CAPTION = 0x00C00000;      //window with a title bar
        public const int WS_SYSMENU = 0x00080000;      //window with no borders etc.
        public const int WS_MINIMIZEBOX = 0x00020000;  //window with minimizebox



        public static IntPtr window;
        public static RECT rect;

        //static int _init_main = 1;
        public static IntPtr HWND_DESKTOP;

        //[DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        //static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        public static IntPtr vewindowsfoundedz;

        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
        public delegate int Callback(int hWnd, int lParam);
        static Callback myCallBack = new Callback(EnumChildGetValue);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnumChildWindows(IntPtr hwndParent, EnumWindowsProc lpEnumFunc, IntPtr lParam);

        private static List<IntPtr> GetChildWindows(IntPtr parent)
        {
            List<IntPtr> result = new List<IntPtr>();
            GCHandle listHandle = GCHandle.Alloc(result);
            try
            {
                EnumWindowsProc childProc = new EnumWindowsProc(EnumWindow);
                EnumChildWindows(parent, childProc, GCHandle.ToIntPtr(listHandle));
            }
            finally
            {
                if (listHandle.IsAllocated)
                    listHandle.Free();
            }
            return result;
        }




        //[DllImport("user32.dll")]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //internal static extern bool GetCursorPos(ref Win32Point pt);


        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(out Win32Point pt);



        [DllImport("winmm.dll")]
        private static extern long mciSendString(string strCommand, StringBuilder strReturn, int iReturnLength, IntPtr hwndCallback);
        private static bool EnumWindow(IntPtr hWnd, IntPtr lParam)
        {
            GCHandle gcChildhandlesList = GCHandle.FromIntPtr(lParam);

            if (gcChildhandlesList == null || gcChildhandlesList.Target == null)
            {
                return false;
            }

            List<IntPtr> childHandles = gcChildhandlesList.Target as List<IntPtr>;
            childHandles.Add(hWnd);

            return true;
        }
        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool EnableWindow(IntPtr hWnd, bool bEnable);




        public static int EnumChildGetValue(int hWnd, int lParam)
        {
            StringBuilder formDetails = new StringBuilder(256);
            string editText = "";
            StringBuilder ClassName = new StringBuilder(256);
            var nRet = GetClassName(new IntPtr(hWnd), ClassName, ClassName.Capacity);
            Console.WriteLine("Control Caption : " + editText + " hWnd : " + hWnd.ToString("X") + " Class Name : " + ClassName);
            Trace.WriteLine("Class Name : " + ClassName);
            Console.ReadLine();

            if (ClassName.ToString().Equals("Edit"))
            {
                Console.WriteLine("Edit Control Found");
                Console.WriteLine("Current Control : " + hWnd.ToString("X"));
                Console.WriteLine("Disabling Notepad Edit Component");
                EnableWindow(new IntPtr(hWnd), true);
            }
            return 1;
        }
        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };



        [return: MarshalAs(UnmanagedType.Bool)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CloseWindowStation(IntPtr hWinsta);

        [DllImport("user32.dll")]
       public static extern bool CloseWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern int ShowCursor(bool bShow);






        //https://social.msdn.microsoft.com/Forums/vstudio/en-US/3295e38d-2267-425a-a7f8-9e6035331e53/how-to-capture-closing-of-c-console-apps?forum=csharpgeneral
        static void OnProcessExit()
        {
            Console.WriteLine("OnProcessExitProgram.cs - console program:" + SCGLOBALSACCESSORS.SCCONSOLECORE.handle);
            //Program.MessageBox((IntPtr)0, "OnProcessExit - console program:" + SCGLOBALSACCESSORS.SCCONSOLECORE.handle, "sccsmsg", 0);


            //string path = @"D:\OnProcessExitRecording.txt";
            /*if (!File.Exists(path))
            {
                File.Create(path);
                TextWriter tw = new StreamWriter(path);
                tw.WriteLine("This program has exited at " + DateTime.Now.ToString());
                tw.Close();
            }
            else if (File.Exists(path))
            {
                TextWriter tw = new StreamWriter(path, true);
                tw.WriteLine("This program has exited at " + DateTime.Now.ToString());
                tw.Close();
            }*/
        }
        static bool ConsoleEventCallback(int eventType)
        {
            if (eventType == 2)
            {
                Console.WriteLine("OnProcessExitProgram.cs - console program:" + SCGLOBALSACCESSORS.SCCONSOLECORE.handle);

                OnProcessExit();
                //Program.MessageBox((IntPtr)0, "OnProcessExit - console program:" + SCGLOBALSACCESSORS.SCCONSOLECORE.handle, "sccsmsg", 0);
            }




            return false;
        }
        static ConsoleEventDelegate handler;   // Keeps it from getting garbage collected
                                               // Pinvoke
        private delegate bool ConsoleEventDelegate(int eventType);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetConsoleCtrlHandler(ConsoleEventDelegate callback, bool add);






        static int getwindowthreadprocessidint = 0;
        //public static DInput keynmouseinput;
        static void ProcessExitHandler(object sender, EventArgs e)
        {
            ShowCursor(true);
            exitedprogram = 1;


            

            //updatescript.heightmapthread.Abort();
            //updatescript.heightmapthread.Suspend();


            /*if (updatescript != null)
            {
                updatescript.threadupdateswtc = -1;
                updatescript.canworkphysics = -1;
                scupdate.stopovr = -1;

                if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                {
                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                    {
                        for (int i = 0; i < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; i++)
                        {

                            for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; j++)
                            {
                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j].ShutDown();
                            }

                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].ShutDown();
                        }
                    }
                }
                //updatescript.heightmapthread = null;

            }

            //updatescript.scgraphicssecpackagemessage.scgraphicssec.
            scdirectx.D3D.ShutDown();
            if (updatescript != null)
            {
                updatescript = null;
            }


            if (_mainThread != null)
            {
                _mainThread.Abort();
                _mainThread = null;
            }*/

            int isexitingprogram = 0;
        threadexitloop:





            if (updatescript.exitthread0 == 0)
            {
                updatescript.exitthread0 = 1;
            }
            if (updatescript.exitthread1 == 0)
            {
                updatescript.exitthread1 = 1;
            }



            if (updatescript.exitthread0 == 2 || updatescript.main_thread_update0 == null)
            {
                if (updatescript.main_thread_update0 == null)
                {
                    updatescript.hasfinishedframe0 = 1;
                }
                else
                {
                    updatescript.main_thread_update0 = null;
                }

                updatescript.exitthread0 = 3;
            }
            if (updatescript.exitthread1 == 2 || updatescript.main_thread_update1 == null)
            {
                if (updatescript.main_thread_update1 == null)
                {
                    updatescript.hasfinishedframe1 = 1;
                }
                else
                {
                    updatescript.main_thread_update1 = null;
                }
                updatescript.exitthread1 = 3;
            }



            if (updatescript.exitthread0 == 3 && updatescript.exitthread1 == 3)
            {



                //sccs.Program.MessageBox((IntPtr)0, "capture reset0", "scmsg", 0);

                if (updatescript.hasfinishedframe0 == 1 && updatescript.hasfinishedframe1 == 1)
                {


                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                    {

                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                        {
                            for (int i = 0; i < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; i++)
                            {


                                for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData.Length; j++)
                                {

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantLightBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].constantMatrixPosBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].ConstantTessellationBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].IndicesBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instanceBufferHeightmap = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocD = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocH = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceBufferLocW = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferFORWARD = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferRIGHT = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].InstanceRotationBufferUP = null;
                                    }


                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].mapBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].vertexBuffer = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].DomainShader = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].GeometryShader = null;
                                    }


                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].HullShader = null;
                                    }


                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].PixelShader = null;
                                    }


                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader.Dispose();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].VertexShader = null;
                                    }

                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j] = null;
                                }

                                for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; j++)
                                {
                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j].ShutDown();
                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j] = null;
                                }

                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].shaderOfChunk = null;
                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].ShutDown();

                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i] = null;
                            }
                        }
                    }


                    if (sccsjittertasks != null)
                    {
                        if (sccsjittertasks[0] != null)
                        {
                            if (sccsjittertasks[0].Length > 0)
                            {
                                if (sccsjittertasks[0][0].shaderresource != null)
                                {

                                    sccsjittertasks[0][0].shaderresource.Dispose();
                                    sccsjittertasks[0][0].shaderresource = null;
                                }

                                if (sccsjittertasks[0][0].frameByteArray != null)
                                {
                                    sccsjittertasks[0][0].frameByteArray = null;
                                }
                                //sccsjittertasks[0][0] = null;
                            }
                            sccsjittertasks[0] = null;
                        }
                        sccsjittertasks = null;
                    }


                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                    {
                        updatescript.scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                        updatescript.scgraphicssecpackagemessage.scgraphicssec = null;
                    }



                    /*
                    if (updatescript.captureMethod != null)
                    {
                        updatescript.captureMethod.StopCapture();// (sccsvvdhd.Form1.theHandle, updatescript.device, factoryy); //

                        updatescript.captureMethod.Dispose();
                        updatescript.captureMethod = null;
                    }*/

                    /*if (sometest != null)
                    {
                        sometest.Dispose();
                        sometest = null;
                    }
                    if (somegcap != null)
                    {
                        somegcap.Dispose();
                        somegcap = null;
                    }*/


                    if (swapChain1 != null)
                    {
                        swapChain1.Dispose();
                        swapChain1 = null;
                    }





                    if (updatescript.SwapChain != null)
                    {
                        updatescript.SwapChain.Dispose();
                        updatescript.SwapChain = null;
                    }

                    if (factoryy != null)
                    {
                        factoryy.Dispose();
                        factoryy = null;
                    }


                    updatescript.ShutDownGraphics();

                    if (sccs.scgraphics.scdirectx.D3D != null)
                    {
                        sccs.scgraphics.scdirectx.D3D.ShutDown();
                        sccs.scgraphics.scdirectx.D3D = null;
                    }



                    updatescript.exitthread0 = 0;
                    updatescript.exitthread1 = 0;
                    updatescript = null;





                    /*if (screencapturetype == 2)
                    {
                        usesharpdxscreencapture = 1;
                    }
                    else
                    {
                        if (usesharpdxscreencapture != 0)
                        {
                            usesharpdxscreencapture = 0;
                        }
                    }*/


                    if (shaderResourceView != null)
                    {
                        shaderResourceView.Dispose();
                        shaderResourceView = null;
                    }

                    if (lastshaderresourceview != null)
                    {
                        lastshaderresourceview.Dispose();
                        lastshaderresourceview = null;
                    }

                    if (texture2d != null)
                    {
                        texture2d.Dispose();
                        texture2d = null;
                    }

                    if (_texture2D != null)
                    {
                        _texture2D.Dispose();
                        _texture2D = null;
                    }

                    if (_bitmap != null)
                    {
                        _bitmap.Dispose();
                        _bitmap = null;
                    }



                    /*
                    if (sccsjittertasks[0][0].shaderresource != null)
                    {
                        if (sccsjittertasks[0][0].shaderresource.Resource != null)
                        {
                            sccsjittertasks[0][0].shaderresource.Resource.Dispose();
                            //sccsjittertasks[0][0].shaderresource.Resource = null;
                        }
                    }*/



                    DeleteObject(vewindowsfoundedz);

                    getwindowthreadprocessidint = 0;
                    textureresetswtc = 0;
                    alttextureresetswtc = 0;
                    GC.Collect();
                    changedscreencapturetype = 0;
                    isexitingprogram = 1;

                    CloseWindow(SCGLOBALSACCESSORS.SCCONSOLECORE.handle);

                    //createinputsswtc = 0;
                    //sccs.Program.MessageBox((IntPtr)0, "capture reset1", "scmsg", 0);


                    /*if (lastscreencapturetype != screencapturetype) //2 == sharpdx11.1 screencapture
                    {


                    }*/
                }
            }
            if (isexitingprogram == 0)
            {

                Thread.Sleep(1);
                goto threadexitloop;
            }
            else
            {
                if (backgroundWorker != null)
                {
                    backgroundWorker.Dispose();
                    backgroundWorker = null;
                }

                if (_mainThread != null)
                {
                    _mainThread.Abort();
                    _mainThread = null;
                }

            }





            //MessageBox((IntPtr)0, "exiting", "scmsg", 0);
            //throw new NotImplementedException("program has exited");
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        static int initthread = 0;
        // static int initform = 0;

        static SharpDX.DXGI.Factory factoryy;
        //static sccssharpdxscreencapture sharpdxscreencapture;
        //static sccsscreenframe screencaptureframe;

        static Stopwatch panelchangedwatch = new Stopwatch();
        static int panelchangedswtc = 0;
        static int counterpanelchanged = 0;
        static int counterpanelchangedmax = 100;


        static int createinputsswtc = 0;


        static int gccollectcounter = 0;
        static int gccollectcountermax = 1000;

        static Stopwatch gccollectstopwatch = new Stopwatch();




        static Stopwatch heightmaptrackbarchangedwatch = new Stopwatch();
        static int heightmaptrackbarchangedswtc = 0;


        /*// Constant values from the "winuser.h" header file.
        internal const int WM_LBUTTONUP = 0x0202,
                           WM_RBUTTONUP = 0x0205;

        internal static IntPtr ApplicationMessageFilter(
            IntPtr hwnd, int message, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            // Handle messages passed to the visual.
            switch (message)
            {
                // Handle the left and right mouse button up messages.
                case WM_LBUTTONUP:
                case WM_RBUTTONUP:
                    System.Windows.Point pt = new System.Windows.Point();
                    pt.X = (uint)lParam & (uint)0x0000ffff;  // LOWORD = x
                    pt.Y = (uint)lParam >> 16;               // HIWORD = y
                    //MyShape.OnHitTest(pt, message);

                    break;
            }

            return IntPtr.Zero;
        }*/


      static  int hasresettedcapture = 0;
        internal static class WinCursors
        {
            [DllImport("user32.dll")]
            private static extern int ShowCursor(bool bShow);


            internal static void ShowCursor()
            {
                while (ShowCursor(true) < 0)
                {
                    ShowCursor(true);
                }
            }

            internal static void HideCursor()
            {
                while (ShowCursor(false) >= 0)
                {
                    ShowCursor(false);
                }
            }
        }


        public static int progcanpause = 0;


        public static keyboardinput keynmouseinput;
        //public static DInput keynmouseinput;
        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;
        public static void createinputs(IntPtr thehandle)
        {
            inputsim = new InputSimulator();
            mousesim = new MouseSimulator(inputsim);
            keyboardsim = new KeyboardSimulator(inputsim);
            /*
            keynmouseinput = new DInput();
            keynmouseinput.Initialize(Program.config, SCGLOBALSACCESSORS.SCCONSOLECORE.handle); //sccsvvdhd.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle //thehandle
            */

            keynmouseinput = new keyboardinput();
        }



        public static sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS;
        static BackgroundWorker backgroundWorker;


        [STAThread]
        static void Main()
        {

            //System.Windows.Forms.Application.Run(theform);

            //AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;
            //AppDomain.CurrentDomain.

            /*window = new DxWindow(".NET Window Capture Samples - WinRT.GraphicsCapture", new GraphicsCapture());
            //using var window = new DxWindow(".NET Window Capture Samples - WinRT.GraphicsCapture", new GraphicsCapture());
            window.Show();*/


            //Application.Current.Windows.
            //var theprocess = Process.GetCurrentProcess();
            /*_handler += new EventHandler(Handler);
            SetConsoleCtrlHandler(_handler, true);
            */







            /*
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                while (true)
                {





                    if (gccollectcounter >= gccollectcountermax || gccollectstopwatch.Elapsed.Seconds >= 5)
                    {
                        gccollectstopwatch.Stop();
                        gccollectstopwatch.Reset();
                        gccollectstopwatch.Restart();
                        GC.Collect();
                        gccollectcounter = 0;
                    }
                    gccollectcounter++;



                    

                    Thread.Sleep(1);
                }


            };

            backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
            {
                Console.WriteLine("worker ended prematurely ");
                MessageBox((IntPtr)0, "worker ended prematurely", "scmsg", 0);
            };

            backgroundWorker.RunWorkerAsync();
            */



            //System.Windows.Forms.Cursor.Hide();
            //MessageBox((IntPtr)0, "form initiated0", "scmsg", 0);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            someform = new sccsvvdhd.Form1();




            /*//textBox = this.textBox1;
            textBox = new System.Windows.Forms.TextBox() { Dock = DockStyle.Fill, Multiline = true, Text = "Interact with the mouse or the keyboard...\r\n", ReadOnly = true };
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Text = "Interact with the mouse or the keyboard...\r\n";
            textBox.ReadOnly = true;


            someform.Controls.Add(textBox);
            //this.Visible = true;


            var _hwndSource = HwndSource.FromHwnd(someform.Handle);
            if (_hwndSource != null)
                _hwndSource.AddHook(WndProc);


            //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
            //    SharpDX.RawInput.DeviceFlags.None, this.Handle);
            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                SharpDX.RawInput.DeviceFlags.None, someform.Handle);
            //SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;*/


            //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, SharpDX.RawInput.DeviceFlags.None);
            //SharpDX.RawInput.Device.KeyboardInput += (sender, args) => textBox.Invoke(new UpdateTextCallback(UpdateKeyboardText), args);





            //someform = new RenderForm("sccsr14");
            someform.Size = new System.Drawing.Size(640, 480); //1920 / 1080
            //someform.Size = new System.Drawing.Size(1920, 1080);

            //someform.CreateControl();
            //someform.TransparencyKey = System.Drawing.Color.Black;
            //someform.BackColor = System.Drawing.Color.Black;
            //someform.Activate();

            //someform.FormBorderStyle = FormBorderStyle.None;
            //someform.WindowState = FormWindowState.Maximized;
            //someform.Opacity = 0.5f;
            someform.TopMost = true;
            //someform.Activate();



            //WinCursors.HideCursor();









            AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;

            mainreceivedmessages = new scmessageobject.scmessageobject[MaxSizeMainObject];

            for (int i = 0; i < mainreceivedmessages.Length; i++)
            {
                mainreceivedmessages[i] = new scmessageobject.scmessageobject();
                mainreceivedmessages[i]._received_switch_in = -1;
                mainreceivedmessages[i]._received_switch_out = -1;
                mainreceivedmessages[i]._sending_switch_in = -1;
                mainreceivedmessages[i]._sending_switch_out = -1;
                mainreceivedmessages[i]._timeOut0 = -1;
                mainreceivedmessages[i]._ParentTaskThreadID0 = -1;
                mainreceivedmessages[i]._main_cpu_count = 1;
                mainreceivedmessages[i]._passTest = "";
                mainreceivedmessages[i]._welcomePackage = -1;
                mainreceivedmessages[i]._work_done = -1;
                mainreceivedmessages[i]._current_menu = -1;
                mainreceivedmessages[i]._last_current_menu = -1;
                mainreceivedmessages[i]._main_menu = -1;
                mainreceivedmessages[i]._menuOption = "";
                mainreceivedmessages[i]._voRecSwtc = -1;
                mainreceivedmessages[i]._voRecMsg = "";
                mainreceivedmessages[i]._someData = new object[MaxSizeMainObject];

                for (int j = 0; j < mainreceivedmessages[i]._someData.Length; j++)
                {
                    mainreceivedmessages[i]._someData[j] = new object();
                }

            }
            ///////////////////////////////
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////  
            ///message_thread_safe_kinda///   
            ///////////////////////////////   
            ///////////////////////////////   
            ///////////////////////////////


            config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);


            int screencaptureresultswtc = 0;


            int lastwindowwidth = 0;
            int lastwindowheight = 0;

            int initform = 0;




            RenderLoop.Run(someform, () =>
            {





                if (createinputsswtc == 0)
                {
                    if (sccsvvdhd.Form1.currentform != null)
                    {
                        if (sccsvvdhd.Form1.currentform.Handle != IntPtr.Zero)
                        {
                            SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages);
                            if (SCGLOBALSACCESSORS == null)
                            {
                                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
                            }
                            else
                            {
                                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
                            }

                            createinputs(SCGLOBALSACCESSORS.SCCONSOLECORE.handle);

                            consoleHandle = someform.Handle;// sccsvvdhd.Form1.theHandle;

                           /* //https://social.msdn.microsoft.com/Forums/vstudio/en-US/3295e38d-2267-425a-a7f8-9e6035331e53/how-to-capture-closing-of-c-console-apps?forum=csharpgeneral
                            handler = new ConsoleEventDelegate(ConsoleEventCallback);
                            SetConsoleCtrlHandler(handler, true);
                            //Console.ReadLine();
                           */


                            createinputsswtc = 1;

                           

                        }
                    }
                }


           




                
                if (createinputsswtc == 1)
                {
                    if (sccsvvdhd.Form1.currentform != null)
                    {
                        if (sccsvvdhd.Form1.currentform.Handle != IntPtr.Zero)
                        {
                            if (sccsvvdhd.Form1.currentform.thepicturebox1 != null)
                            {

                                if (sccsvvdhd.Form1.currentform.lastpicturebox1regiondirtypixelrgb != sccsvvdhd.Form1.currentform.picturebox1regiondirtypixelrgb)
                                {
                                    //Console.WriteLine("frame arrives here0");                             
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        sccsvvdhd.Form1.currentform.thepicturebox1.Invoke((MethodInvoker)delegate
                                        {
                                            //sccsvvdhd.Form1.drawString = "test";

                                            sccsvvdhd.Form1.currentform.lastpicturebox1regiondirtypixelrgb = sccsvvdhd.Form1.currentform.picturebox1regiondirtypixelrgb;
                                            sccsvvdhd.Form1.currentform.thepicturebox1.Refresh();
                                        });
                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                }
                            }


                            if (sccsvvdhd.Form1.currentform.thepicturebox2 != null)
                            {

                                if (sccsvvdhd.Form1.currentform.lastpicturebox2fpsfloat0 != sccsvvdhd.Form1.currentform.picturebox2fpsfloat0)
                                {
                                    //Console.WriteLine("frame arrives here0");                             
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        sccsvvdhd.Form1.currentform.thepicturebox2.Invoke((MethodInvoker)delegate
                                        {
                                            //sccsvvdhd.Form1.drawString = "test";

                                            sccsvvdhd.Form1.currentform.lastpicturebox2fpsfloat0 = sccsvvdhd.Form1.currentform.picturebox2fpsfloat0;
                                            sccsvvdhd.Form1.currentform.thepicturebox2.Refresh();
                                        });
                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                }

                                if (sccsvvdhd.Form1.currentform.lastpicturebox2fpsfloat1 != sccsvvdhd.Form1.currentform.picturebox2fpsfloat1)
                                {
                                    //Console.WriteLine("frame arrives here0");                             
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        sccsvvdhd.Form1.currentform.thepicturebox2.Invoke((MethodInvoker)delegate
                                        {
                                            //sccsvvdhd.Form1.drawString = "test";

                                            sccsvvdhd.Form1.currentform.lastpicturebox2fpsfloat1 = sccsvvdhd.Form1.currentform.picturebox2fpsfloat1;
                                            sccsvvdhd.Form1.currentform.thepicturebox2.Refresh();
                                        });
                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                }
                            }


                        }
                    }
                }






                    /*
                    if (sccsvvdhd.Form1.trackbar2 != null)
                    {
                        if (sccsvvdhd.Form1.someform.lightintensityvalueswtc == 1 && sccsvvdhd.Form1.someform.sccsvvdhdgrayscaleorcolored == 2)
                        {
                            sccsvvdhd.Form1.trackbar2.Minimum = 10;//
                            sccsvvdhd.Form1.trackbar2.Maximum = 100;//
                            sccsvvdhd.Form1.someform.lightintensityvalueswtc = 0;
                        }
                        else if (sccsvvdhd.Form1.someform.lightintensityvalueswtc == 1 && sccsvvdhd.Form1.someform.sccsvvdhdgrayscaleorcolored == 0 ||
                                sccsvvdhd.Form1.someform.lightintensityvalueswtc == 1 && sccsvvdhd.Form1.someform.sccsvvdhdgrayscaleorcolored == 1)
                        {
                            sccsvvdhd.Form1.trackbar2.Minimum = -75;//
                            sccsvvdhd.Form1.trackbar2.Maximum = 185;//
                            sccsvvdhd.Form1.someform.lightintensityvalueswtc = 0;
                        }
                    }*/

                    if (keynmouseinput != null)
                {

                    keynmouseinput.ReadKeyboard();
                }

                if (sccsvvdhd.Form1.currentform != null)
                {
                    if (sccsvvdhd.Form1.capturedprogram != IntPtr.Zero)
                    {
                        if (sccsvvdhd.Form1.startcaptureonce == 1)
                        {
                            if (sccsvvdhd.Form1.currentform.comboboxcapturelist != null)
                            {


                                /*if (Form1.capturedprogram == IntPtr.Zero)
                                {

                                }

                                if (!SelectedTitle.ToLower().Contains("microsoft") || !SelectedTitle.ToLower().Contains("edge"))
                                {
                                    executeModeChange();
                                }
                                else
                                {
                                    if (SelectedTitle.ToLower().Contains("microsoft") && SelectedTitle.ToLower().Contains("edge"))
                                    {
                                        EnumWindows(enumWindowProc, IntPtr.Zero);
                                    }
                                }

                                refresh();*/

                                //sccs.Program.MessageBox((IntPtr)0, "starting capture", "scmsg", 0);


                                //if (sccs.scgraphics.scdirectx.D3D != null)
                                {
                                    //sccs.Program.MessageBox((IntPtr)0, "starting capture", "scmsg", 0);

                                    if (sccsvvdhd.Form1.currentform.comboboxcapturelist.SelectedIndex == 0)
                                    {
                                        
                                        
                                        if (icap != null)
                                        {
                                            


                                            icap.Dispose();
                                            icap = null;
                                        }
                                        /*if (Form1.capturedprogram != IntPtr.Zero)
                                        {
                                            DeleteObject(Form1.capturedprogram);
                                        }*/
                                        icap = (ICaptureMethod)new WinRT.GraphicsCapture.GraphicsCapture();

                                        icap.StartCapture(sccsvvdhd.Form1.capturedprogram, sccs.scgraphics.scdirectx.D3D.Device, sccs.scgraphics.scdirectx.D3D.factory1);




                                    }
                                    else if (sccsvvdhd.Form1.currentform.comboboxcapturelist.SelectedIndex == 1)//DWMSHAREDSURFACE
                                    {
                                        if (icap != null)
                                        {
                                            icap.Dispose();
                                            icap = null;
                                        }
                                        /*if (Form1.capturedprogram != IntPtr.Zero)
                                        {
                                            DeleteObject(Form1.capturedprogram);
                                        }*/
                                        icap = (ICaptureMethod) new DwmSharedSurface();

                                        icap.StartCapture(sccsvvdhd.Form1.capturedprogram, sccs.scgraphics.scdirectx.D3D.Device, sccs.scgraphics.scdirectx.D3D.factory1);

                                    }
                                    else if (sccsvvdhd.Form1.currentform.comboboxcapturelist.SelectedIndex == 2)//SHARPDXSCREENCAPTURE
                                    {
                                        if (icap != null)
                                        {
                                            icap.Dispose();
                                            icap = null;
                                        }
                                        /*if (Form1.capturedprogram != IntPtr.Zero)
                                        {
                                            DeleteObject(Form1.capturedprogram);
                                        }*/

                                        if (sharpdxscreencapture!= null)
                                        {
                                            sharpdxscreencapture.Disposer();
                                            sharpdxscreencapture = null;
                                        }


                                        sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, sccs.scgraphics.scdirectx.D3D.Device);


                                        /*icap = (ICaptureMethod)new WinRT.GraphicsCapture.GraphicsCapture();

                                        icap.StartCapture(sccsvvdhd.Form1.capturedprogram, device, factory);*/
                                    }
                                }

                                sccsvvdhd.Form1.startcaptureonce = 2;

                                lastscreencapturetype = sccsvvdhd.Form1.currentform.comboboxcapturelist.SelectedIndex;
                            }

                        }
                        else
                        {

                        }

                    }
                }
                
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                
                /*if (sccsvvdhd.Form1.currentform != null)
                {
                    if (sccsvvdhd.Form1.currentform.thepicturebox1 != null)
                    {
                        //Console.WriteLine("frame arrives here0");
                        if (sccsvvdhd.Form1.currentform.thepicturebox1.BackgroundImage != null)
                        {

                            //Console.WriteLine("frame arrives here1");

                            IntPtr hdcBitmap = sccsvvdhd.Form1.pictureboxgraphics.GetHdc();
                            sccsvvdhd.Form1.pictureboxgraphics.ReleaseHdc(hdcBitmap);

                            System.Drawing.Rectangle standarddimensionsrectangle = new System.Drawing.Rectangle(0, 0, ((System.Drawing.Bitmap)sccsvvdhd.Form1.currentform.thepicturebox1.BackgroundImage).Width, ((System.Drawing.Bitmap)sccsvvdhd.Form1.currentform.thepicturebox1.BackgroundImage).Height);

                            System.Drawing.Rectangle expansionRectangle = new System.Drawing.Rectangle(0, 0, ((System.Drawing.Bitmap)sccsvvdhd.Form1.currentform.thepicturebox1.BackgroundImage).Width, ((System.Drawing.Bitmap)sccsvvdhd.Form1.currentform.thepicturebox1.BackgroundImage).Height);

                            //System.Drawing.Rectangle compressionRectangle = new System.Drawing.Rectangle(0, 0, thebitmap.Width / 2, thebitmap.Height / 2);

                            //sccsvvdhd.Form1.pictureboxgraphics.DrawImage(thebitmap, 1, 1);
                            //sccsvvdhd.Form1.pictureboxgraphics.DrawImage(thebitmap, expansionRectangle);
                            var refreshDXEngineAction = new Action(delegate
                            {
                                sccsvvdhd.Form1.pictureboxgraphics.DrawImage(queueofimagedequeueddata, expansionRectangle);
                                IntPtr hdcBitmap = sccsvvdhd.Form1.pictureboxgraphics.GetHdc();
                                sccsvvdhd.Form1.pictureboxgraphics.ReleaseHdc(hdcBitmap);
                            });
                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);

                        }
                    }
                }*/
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ







                if (initthread == 0)
                {
                    _mainThread = new Thread((tester0000) =>
                    {
                        //sccs.Program.MessageBox((IntPtr)0, "Form1", "scmsg", 0);
                        //sccsgraphicssec = new sccs.scgraphics.scgraphicssec();


                        updatescript = new scupdate();

                        //sccsgraphicssec = new sccs.scgraphics.scgraphicssec();

                        int initcaptureagain = 0;
                        int startcreatingstuff = 0;

                    _thread_main_loop:





                        /*
                        if (sccs.scgraphics.scgraphicssec.currentscgraphicssec != null)
                        {
                        Console.WriteLine("null sccs.scgraphics.scgraphicssec.currentscgraphicssec");
                        }*/





                        if (startcreatingstuff == 0 && updatescript != null)
                        {
                            //Console.WriteLine("null sccs.scgraphics.scgraphicssec.currentscgraphicssec");


                            if (usejitterphysics == 1)
                            {
                                jitter_sc = new jitter_sc[physicsengineinstancex * physicsengineinstancey * physicsengineinstancez];
                                sccsjittertasks = new sccsmessageobjectjitter[physicsengineinstancex * physicsengineinstancey * physicsengineinstancez][];

                                sc_jitter_data _sc_jitter_data = new sc_jitter_data();
                                _sc_jitter_data.alloweddeactivation = allowdeactivation;
                                _sc_jitter_data.allowedpenetration = worldallowedpenetration;
                                _sc_jitter_data.width = worldwidth;
                                _sc_jitter_data.height = worldheight;
                                _sc_jitter_data.depth = worlddepth;
                                _sc_jitter_data.gravity = _world_gravity;
                                _sc_jitter_data.smalliterations = worldsmalliterations;
                                _sc_jitter_data.iterations = worlditerations;

                                for (int xx = 0; xx < physicsengineinstancex; xx++)
                                {
                                    for (int yy = 0; yy < physicsengineinstancey; yy++)
                                    {
                                        for (int zz = 0; zz < physicsengineinstancez; zz++)
                                        {
                                            var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);
                                            //_jitter_physics[indexer00] = DoSpecialThing();
                                            sccsjittertasks[indexer00] = new sccsmessageobjectjitter[worldwidth * worldheight * worlddepth];

                                            for (int x = 0; x < worldwidth; x++)
                                            {
                                                for (int y = 0; y < worldheight; y++)
                                                {
                                                    for (int z = 0; z < worlddepth; z++)
                                                    {
                                                        var indexer01 = x + worldwidth * (y + worldheight * z);
                                                        sccsjittertasks[indexer00][indexer01] = new sccsmessageobjectjitter();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                //Console.WriteLine("built0");
                                //jitter_sc = create_jitter_instances(jitter_sc, _sc_jitter_data);

                                for (int xx = 0; xx < physicsengineinstancex; xx++)
                                {
                                    for (int yy = 0; yy < physicsengineinstancey; yy++)
                                    {
                                        for (int zz = 0; zz < physicsengineinstancez; zz++)
                                        {
                                            var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);


                                            //if (jitter_sc.Length > 0)
                                            //{
                                            //    Console.WriteLine("built00");
                                            //}
                                            //
                                            //Console.WriteLine("index: " + indexer00);
                                            jitter_sc[indexer00]._sc_create_jitter_world(_sc_jitter_data);


                                            for (int x = 0; x < worldwidth; x++)
                                            {
                                                for (int y = 0; y < worldheight; y++)
                                                {
                                                    for (int z = 0; z < worlddepth; z++)
                                                    {
                                                        var indexer1 = x + worldwidth * (y + worldheight * z);

                                                        var world = jitter_sc[indexer00].return_world(indexer1);

                                                        if (world == null)
                                                        {
                                                            Console.WriteLine("null");
                                                        }
                                                        else
                                                        {
                                                            //Console.WriteLine("!null");

                                                            sccsjittertasks[indexer00][indexer1]._world_data = new object[2];
                                                            sccsjittertasks[indexer00][indexer1]._work_index = -1;
                                                            sccsjittertasks[indexer00][indexer1]._world_data[0] = world;
                                                            //Console.WriteLine("index: " + indexer1);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                //, consoleHandle
                                //sccsjittertasks = sccs.scgraphics.scgraphicssec.currentscgraphicssec.i(sccsjittertasks); //, SCGLOBALSACCESSORS.SCCONSOLEWRITER*/
                                //sccsjittertasks = sccs.scgraphics.scgraphicssec.currentscgraphicssec._sc_create_world_objects(sccsjittertasks);
                                sccsjittertasks = updatescript.init_update_variables(sccsjittertasks, config); //
                            }
                            else if (usejitterphysics == 0)
                            {
                                sccsjittertasks = new sccsmessageobjectjitter[1][];
                                sccsjittertasks[0] = new sccsmessageobjectjitter[1];
                                sccsjittertasks[0][0] = new sccsmessageobjectjitter();
                                for (int xx = 0; xx < physicsengineinstancex; xx++)
                                {
                                    for (int yy = 0; yy < physicsengineinstancey; yy++)
                                    {
                                        for (int zz = 0; zz < physicsengineinstancez; zz++)
                                        {
                                            var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);
                                            //_jitter_physics[indexer00] = DoSpecialThing();
                                            sccsjittertasks[indexer00] = new sccsmessageobjectjitter[worldwidth * worldheight * worlddepth];

                                            for (int x = 0; x < worldwidth; x++)
                                            {
                                                for (int y = 0; y < worldheight; y++)
                                                {
                                                    for (int z = 0; z < worlddepth; z++)
                                                    {
                                                        var indexer01 = x + worldwidth * (y + worldheight * z);
                                                        sccsjittertasks[indexer00][indexer01] = new sccsmessageobjectjitter();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                //, consoleHandle
                                sccsjittertasks = updatescript.init_update_variables(sccsjittertasks, config); //, SCGLOBALSACCESSORS.SCCONSOLEWRITER
                                                                                                               //sccsjittertasks = sccs.scgraphics.scgraphicssec.currentscgraphicssec._sc_create_world_objects(sccsjittertasks);
                            }
                            startcreatingstuff = 1;
                        }

                        if (createtimers == 0)
                        {
                            // Use clock 
                            clock = new Stopwatch();
                            clock.Start();

                            fpsTimerUpdateThread = new Stopwatch();
                            fpsTimerUpdateThread.Start();

                            fpsTimerShaderRenderThread = new Stopwatch();
                            fpsTimerShaderRenderThread.Start();


                            fpsTimerShaderPresentThread = new Stopwatch();
                            fpsTimerShaderPresentThread.Start();

                            createtimers = 1;
                        }



                        /*
                        if (updatesec != null)
                        {
                            
                       
                        }*/

                       







                        if (lastrunapptype != rundedicatedthreadapp)
                        {
                            if (!rundedicatedthreadapp)
                            {
                                canworkuithread = 1;
                                canworksysthread = 0;
                            }
                            else if (rundedicatedthreadapp)
                            {
                                canworkuithread = 0;
                                canworksysthread = 1;

                            }
                        }



                        /*if (initform == 1 && sccsvvdhd.Form1.initForm == 1)
                        {



                            initform = 2;
                        }*/








                        if (initform == 1 && startcreatingstuff == 1)
                        {

                            if (sccsvvdhd.Form1.capturedprogram != IntPtr.Zero)
                            {

                                if (icap != null)
                                {
                                    //sccs.Program.MessageBox((IntPtr)0, "TryGetNextFrameAsTexture2D", "scmsg", 0);

                                    var texture2d = icap.TryGetNextFrameAsTexture2D(sccs.scgraphics.scdirectx.D3D.device);

                                    if (texture2d != null)
                                    {



                                        if (textureresetswtc == 0)
                                        {
                                            _textureDescription = new Texture2DDescription
                                            {
                                                CpuAccessFlags = CpuAccessFlags.Read,
                                                BindFlags = BindFlags.None,// ShaderResource | BindFlags.RenderTarget,
                                                Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                                                Width = texture2d.Description.Width,
                                                Height = texture2d.Description.Height,
                                                OptionFlags = ResourceOptionFlags.None,
                                                MipLevels = 1,
                                                ArraySize = 1,
                                                SampleDescription = { Count = 1, Quality = 0 },
                                                Usage = ResourceUsage.Staging
                                            };
                                            _texture2d = new Texture2D(sccs.scgraphics.scdirectx.D3D.device, _textureDescription);



                                            _bitmap = new System.Drawing.Bitmap(_texture2d.Description.Width, _texture2d.Description.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                            boundsRect = new System.Drawing.Rectangle(0, 0, _texture2d.Description.Width, _texture2d.Description.Height);

                                            ///rectanglebitmap = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                                            //bitmapData = bitmap.LockBits(rectanglebitmap, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);

                                            bmpData = _bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                                            _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                                            _bitmap.UnlockBits(bmpData);
                                            _textureByteArray = new byte[_bytesTotal];
                                            bmpstride = bmpData.Stride;



                                            //bmpData = null;
                                            /*if (_bitmap != null)
                                            {
                                                _bitmap.Dispose();
                                                _bitmap = null;
                                            }*/

                                            textureresetswtc = 1;
                                        }



                                        //var thebitmapofscreen = new System.Drawing.Bitmap(_texture2d.Description.Width, _texture2d.Description.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);





                                        if (texture2d != null)
                                        {
                                            //if (updatescript != null)
                                            {
                                                if (sccs.scgraphics.scdirectx.D3D.device != null)
                                                {
                                                    sccs.scgraphics.scdirectx.D3D.device.ImmediateContext.CopyResource(texture2d, _texture2d);

                                                    //DISCARDED
                                                    //DISCARDED
                                                    //DISCARDED
                                                    dataBox1 = sccs.scgraphics.scdirectx.D3D.device.ImmediateContext.MapSubresource(_texture2d, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                                                    memoryBitmapStride = _textureDescription.Width * 4;
                                                    //8801024
                                                    //MessageBox((IntPtr)0, "fail " + memoryBitmapStride, "scmsg", 0); 
                                                    //int memoryBitmapStridey = _textureDescription.Height * 4;
                                                    columns = _textureDescription.Width;
                                                    rows = _textureDescription.Height;
                                                    interptr1 = dataBox1.DataPointer;

                                                    if (dataBox1.RowPitch == memoryBitmapStride)
                                                    {
                                                        Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                                                        //Utilities.CopyMemory(interptr1, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0), _textureByteArray.Length);
                                                        // Stride not the same - copy line by line
                                                        // Stride is the same
                                                        //MessageBox((IntPtr)0, "fail0", "scmsg", 0);
                                                    }
                                                    else
                                                    {
                                                        //7704 // memorymapstride 4*1920
                                                        //7936 // databox.rowpitch
                                                        //8801024 // databox.slicepitch


                                                        //var rowStride = Math.Min(dataBox1.RowPitch, memoryBitmapStride);
                                                        //_textureByteArray = new byte[rowStride * rows];
                                                        //MessageBox((IntPtr)0, "fail " + memoryBitmapStride + " " + rowStride + " " + dataBox1.RowPitch + " " + dataBox1.SlicePitch, "scmsg", 0);

                                                        //Utilities.CopyMemory(interptr1, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0), _textureByteArray.Length);
                                                        for (int y = 0; y < rows; y++)
                                                        {
                                                            Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                                                            //Utilities.CopyMemory(interptr1 + y , Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)+y, memoryBitmapStride);

                                                        }

                                                        //MessageBox((IntPtr)0, "fail1", "scmsg", 0);
                                                        //Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                                                    }










                                                    


                                                    //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                                                    //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                                                    //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ

                                                    //var thebitmap = new System.Drawing.Bitmap(columns, rows, columns * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0));
                                                    /*
                                                    thebitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows.ToString("00") + columns.ToString("00") + ".png");
                                                    bitmapcounter++;*/

                                                    /*var boundsRect = new System.Drawing.Rectangle(0, 0, 50, 40);
                                                    var bitmap = thebitmap.Clone(boundsRect, thebitmap.PixelFormat);
                                                    var test = cropAtRect(thebitmap, boundsRect);

                                                    test.Save(@"C:\Users\steve\Desktop\screenRecord1\" + bitmapcounter + "_" + 40.ToString("00") + 50.ToString("00") + ".png");
                                                    bitmapcounter++;
                                                    */


                                                    //string path = System.Windows.Forms.Application.StartupPath + @"\transparent.jpg";
                                                    //FileStream sw1 = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);

                                                    //FileStream sw1 = new FileStream(thebitmap);
                                                    //Bitmap thebitmap = new System.Drawing.Bitmap(128, 128);
                                                    //var thebitmap = new System.Drawing.Bitmap(sw1);


                                                    /*if (sccsvvdhd.Form1.thepicturebox1!= null)
                                                    {
                                                        sccsvvdhd.Form1.thepicturebox1.BackgroundImage = thebitmap;
                                                    }*/

                                                    /*
                                                    if (sccsvvdhd.Form1.thepicturebox1 != null)
                                                    {

                                                        //System.Drawing.Bitmap myBitmap = (System.Drawing.Bitmap)sccsvvdhd.Form1.thepicturebox1.BackgroundImage;
                                                        sccsvvdhd.Form1.thepicturebox1.BackgroundImage = thebitmap;

                                                        //Rectangle expansionRectangle = new Rectangle(135, 10,
                                                        //   myBitmap.Width, myBitmap.Height);

                                                        /*System.Drawing.Rectangle compressionRectangle = new System.Drawing.Rectangle(0, 0,
                                                           thebitmap.Width, thebitmap.Height); // /10
                                                        */
                                                        /*System.Drawing.Rectangle standarddimensionsrectangle = new System.Drawing.Rectangle(0, 0, thebitmap.Width, thebitmap.Height);

                                                         System.Drawing.Rectangle expansionRectangle = new System.Drawing.Rectangle(0, 0, thebitmap.Width, thebitmap.Height);
                                                        */
                                                        //System.Drawing.Rectangle compressionRectangle = new System.Drawing.Rectangle(0, 0, thebitmap.Width / 2, thebitmap.Height / 2);

                                                        //sccsvvdhd.Form1.pictureboxgraphics.DrawImage(thebitmap, 1, 1);
                                                        //sccsvvdhd.Form1.pictureboxgraphics.DrawImage(thebitmap, expansionRectangle);
                                                        /*var refreshDXEngineAction = new Action(delegate
                                                        {
                                                            sccsvvdhd.Form1.pictureboxgraphics.DrawImage(thebitmap, expansionRectangle);
                                                            IntPtr hdcBitmap = sccsvvdhd.Form1.pictureboxgraphics.GetHdc();
                                                            sccsvvdhd.Form1.pictureboxgraphics.ReleaseHdc(hdcBitmap);
                                                        });
                                                        System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                                        



                                                        //e.Graphics.DrawImage(myBitmap, 10, 10);
                                                        //e.Graphics.DrawImage(myBitmap, expansionRectangle);
                                                        //e.Graphics.DrawImage(myBitmap, compressionRectangle);
                                                        //sccsvvdhd.Form1.pictureboxgraphics.DrawImage(thebitmap, compressionRectangle);
                                                    }

                                                    //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                                                    //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ
                                                    //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ

                                                    /*Bitmap myBitmap = (Bitmap)pictureBox1.BackgroundImage;


                                                    //Rectangle expansionRectangle = new Rectangle(135, 10,
                                                    //   myBitmap.Width, myBitmap.Height);

                                                    Rectangle compressionRectangle = new Rectangle(0, 0,
                                                        myBitmap.Width / 10, myBitmap.Height / 10);

                                                    //e.Graphics.DrawImage(myBitmap, 10, 10);
                                                    //e.Graphics.DrawImage(myBitmap, expansionRectangle);
                                                    e.Graphics.DrawImage(myBitmap, compressionRectangle);
                                                    */







                                                    /*
                                                    if (lastthebitmap != null)
                                                    {
                                                        lastthebitmap.Dispose();
                                                        lastthebitmap = null;
                                                    }
                                                    lastthebitmap = thebitmap;
                                                    */




                                                    /*
                                                    if (thirdthebitmap != null)
                                                    {
                                                        thirdthebitmap.Dispose();
                                                    }
                                                    thirdthebitmap = lastthebitmap;*/











                                                    sccs.scgraphics.scdirectx.D3D.device.ImmediateContext.UnmapSubresource(_texture2d, 0);

                                                    GC.SuppressFinalize(interptr1);
                                                    DeleteObject(interptr1);




                                                    /*
                                                    if (lastthebitmap!= null)
                                                    {
                                                        lastthebitmap.Dispose();
                                                    }

                                                    lastthebitmap = thebitmap;*/
                                                    /*
                                                    thebitmap.Dispose();
                                                    */











                                                    /*
                                                    shaderResourceView = Ab3d.DirectX.TextureLoader.CreateShaderResourceView(updatescript.device, _textureByteArray, texture2d.Description.Width, texture2d.Description.Height, bmpstride, Format.B8G8R8A8_UNorm, true);
                                                    */

                                                    shaderResourceView = new ShaderResourceView(sccs.scgraphics.scdirectx.D3D.device, texture2d);

                                                    if (lastshaderresourceview != null)
                                                    {
                                                        lastshaderresourceview.Dispose();
                                                        lastshaderresourceview = null;
                                                    }
                                                    lastshaderresourceview = shaderResourceView;

                                                }
                                            }
                                        }

                                        //device.ImmediateContext.PixelShader.SetShaderResource(0, shaderResourceView);
                                        //device.ImmediateContext.Draw(4, 0);
                                        //swapChain1.Present(1, PresentFlags.None, new PresentParameters());

                                        if (sccsjittertasks != null)
                                        {
                                            if (sccsjittertasks[0] != null)
                                            {
                                                if (sccsjittertasks[0].Length > 0)
                                                {
                                                    sccsjittertasks[0][0].frameByteArray = _textureByteArray;
                                                    sccsjittertasks[0][0].shaderresource = shaderResourceView;
                                                }
                                            }
                                        }







                                        //MessageBox((IntPtr)0, "texture !null", "scmsg", 0);
                                        //Console.WriteLine("texture !null");
                                    }
                                    else
                                    {
                                        //MessageBox((IntPtr)0, "texture null", "scmsg", 0);
                                        //Console.WriteLine("texture null");
                                    }

                                    if (lasttexture2d != null)
                                    {
                                        lasttexture2d.Dispose();
                                        lasttexture2d = null;
                                    }

                                    lasttexture2d = texture2d;
                                }
                                else
                                {

                                    //SHARPDXCAPTURE WILL START

                                    if (sccsvvdhd.Form1.currentform != null)
                                    {



                                        //sccsvvdhd.Form1.currentform.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                        //{

                                        //sccsvvdhd.Form1.currentform.comboboxcapturelist.Text = "Shrink";
                                        if (screencapturetype == 2)//sccsvvdhd.Form1.currentform.comboboxcapturelist.SelectedIndex == 2)//Program.screencapturetype == 2)
                                        {
                                            if (sharpdxscreencapture != null)
                                            {
                                                // Program.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);



                                                for (int i = 0; i < 3;)
                                                {
                                                    screencaptureresultswtc = 0;
                                                    try
                                                    {
                                                        screencaptureframe = sharpdxscreencapture.ScreenCapture(3);




                                                        if (sccsjittertasks != null)
                                                        {
                                                            if (sccsjittertasks[0] != null)
                                                            {
                                                                if (sccsjittertasks[0].Length > 0)
                                                                {
                                                                    sccsjittertasks[0][0].frameByteArray = screencaptureframe.frameByteArray;
                                                                    sccsjittertasks[0][0].shaderresource = screencaptureframe.ShaderResource;

                                                                    //Program.MessageBox((IntPtr)0, "test2", "sccsmsg", 0);
                                                                }
                                                            }
                                                        }


                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, sccs.scgraphics.scdirectx.D3D.device);
                                                        screencaptureresultswtc = 1;
                                                    }
                                                    i++;
                                                    if (screencaptureresultswtc == 0)
                                                    {
                                                        break;
                                                    }
                                                }

                                                if (sharpdxscreencapture.hasinit == 2)
                                                {

                                                    //Program.MessageBox((IntPtr)0, "test1", "sccsmsg", 0);


                                                }
                                            }
                                        }
                                        // });




                                        /*
                                        if (sccsvvdhd.Form1.currentform.comboboxcapturelist != null)
                                        {
                                            if (sccsvvdhd.Form1.currentform.comboboxcapturelist.SelectedIndex == 2)//Program.screencapturetype == 2)
                                            {
                                                if (sharpdxscreencapture != null)
                                                {
                                                    // Program.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);



                                                    for (int i = 0; i < 3;)
                                                    {
                                                        screencaptureresultswtc = 0;
                                                        try
                                                        {
                                                            screencaptureframe = sharpdxscreencapture.ScreenCapture(3);




                                                            if (sccsjittertasks != null)
                                                            {
                                                                if (sccsjittertasks[0] != null)
                                                                {
                                                                    if (sccsjittertasks[0].Length > 0)
                                                                    {
                                                                        sccsjittertasks[0][0].frameByteArray = screencaptureframe.frameByteArray;
                                                                        sccsjittertasks[0][0].shaderresource = screencaptureframe.ShaderResource;

                                                                        //Program.MessageBox((IntPtr)0, "test2", "sccsmsg", 0);
                                                                    }
                                                                }
                                                            }


                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, sccs.scgraphics.scdirectx.D3D.device);
                                                            screencaptureresultswtc = 1;
                                                        }
                                                        i++;
                                                        if (screencaptureresultswtc == 0)
                                                        {
                                                            break;
                                                        }
                                                    }

                                                    if (sharpdxscreencapture.hasinit == 2)
                                                    {

                                                        //Program.MessageBox((IntPtr)0, "test1", "sccsmsg", 0);


                                                    }
                                                }
                                            }
                                        }*/
                                    }

                                }
                            }
                            else
                            {
                                if (scgraphicssec.currentscgraphicssec.idletexture != null)
                                {
                                    if (alttextureresetswtc == 0)
                                    {
                                        //Console.WriteLine("trying alt texture");
                                        
                                        var _textureDescription = new Texture2DDescription
                                        {
                                            CpuAccessFlags = CpuAccessFlags.Read,
                                            BindFlags = BindFlags.None,// ShaderResource | BindFlags.RenderTarget,
                                            Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                                            Width = scgraphicssec.currentscgraphicssec.idletexture.width,
                                            Height = scgraphicssec.currentscgraphicssec.idletexture.height,
                                            OptionFlags = ResourceOptionFlags.None,
                                            MipLevels = 1,
                                            ArraySize = 1,
                                            SampleDescription = { Count = 1, Quality = 0 },
                                            Usage = ResourceUsage.Staging
                                        };

                                        var _texture2d = new Texture2D(sccs.scgraphics.scdirectx.D3D.device, _textureDescription);


                                        var _bitmap = new System.Drawing.Bitmap(_texture2d.Description.Width, _texture2d.Description.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                                        var boundsRect = new System.Drawing.Rectangle(0, 0, _texture2d.Description.Width, _texture2d.Description.Height);


                                        var bmpData = _bitmap.LockBits(boundsRect, System.Drawing.Imaging.ImageLockMode.ReadOnly, _bitmap.PixelFormat);
                                        var _bytesTotal = Math.Abs(bmpData.Stride) * _bitmap.Height;
                                        _bitmap.UnlockBits(bmpData);
                                        var _textureByteArray = new byte[_bytesTotal];
                                        //var bmpstride = bmpData.Stride;


                                        
                                        sccs.scgraphics.scdirectx.D3D.device.ImmediateContext.CopyResource(scgraphicssec.currentscgraphicssec.idletexture.TextureResource.Resource, _texture2d);

                                        //DISCARDED
                                        //DISCARDED
                                        //DISCARDED
                                        var dataBox1 = sccs.scgraphics.scdirectx.D3D.device.ImmediateContext.MapSubresource(_texture2d, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

                                        var memoryBitmapStride = _textureDescription.Width * 4;
                                        //8801024
                                        //MessageBox((IntPtr)0, "fail " + memoryBitmapStride, "scmsg", 0); 
                                        //int memoryBitmapStridey = _textureDescription.Height * 4;
                                        var columns = _textureDescription.Width;
                                        var rows = _textureDescription.Height;
                                        var interptr1 = dataBox1.DataPointer;

                                        if (dataBox1.RowPitch == memoryBitmapStride)
                                        {
                                            Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                                            //Utilities.CopyMemory(interptr1, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0), _textureByteArray.Length);
                                            // Stride not the same - copy line by line
                                            // Stride is the same
                                            //MessageBox((IntPtr)0, "fail0", "scmsg", 0);
                                        }
                                        else
                                        {
                                            //7704 // memorymapstride 4*1920
                                            //7936 // databox.rowpitch
                                            //8801024 // databox.slicepitch


                                            //var rowStride = Math.Min(dataBox1.RowPitch, memoryBitmapStride);
                                            //_textureByteArray = new byte[rowStride * rows];
                                            //MessageBox((IntPtr)0, "fail " + memoryBitmapStride + " " + rowStride + " " + dataBox1.RowPitch + " " + dataBox1.SlicePitch, "scmsg", 0);

                                            //Utilities.CopyMemory(interptr1, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0), _textureByteArray.Length);
                                            for (int y = 0; y < rows; y++)
                                            {
                                                Marshal.Copy(interptr1 + y * dataBox1.RowPitch, _textureByteArray, y * memoryBitmapStride, memoryBitmapStride);
                                                //Utilities.CopyMemory(interptr1 + y , Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)+y, memoryBitmapStride);

                                            }

                                            //MessageBox((IntPtr)0, "fail1", "scmsg", 0);
                                            //Marshal.Copy(interptr1, _textureByteArray, 0, _bytesTotal);
                                        }


                                        sccs.scgraphics.scdirectx.D3D.device.ImmediateContext.UnmapSubresource(scgraphicssec.currentscgraphicssec.idletexture.TextureResource.Resource, 0);

                                        sccsjittertasks[0][0].frameByteArray = _textureByteArray;
                                        sccsjittertasks[0][0].shaderresource = scgraphicssec.currentscgraphicssec.idletexture.TextureResource;



                                        alttextureresetswtc = 1;
                                    }
                                }
                            }

                      


                            if (updatescript != null) // && lastscreencapturetype == screencapturetype
                            {
                                if (exitedprogram != 1 && changedscreencapturetype == 0)
                                {
                                    if (usejitterphysics == 0)
                                    {


                                        var datetimenow = DateTime.Now;


                                        if (fpsTimerUpdateThreadcounter >= 10)
                                        {

                                            //if (fpsTimerUpdateThread.ElapsedMilliseconds > 1000)
                                            {
                                                //var typeStr = directx.D3D.currentState.Type.ToString();
                                                //if (directx.D3D.currentState.Type != directx.TestType.Immediate && !directx.D3D.supportCommandList) typeStr += "*";



                                                //sccsr15forms.Form1.currentform.Text = string.Format("SharpDX - Threaded MultiCube D3D11  - (F1) {0} - (F2) {1} - (F3) {2} - Threads ↑↓{3} - Count ←{4}→ - FPS: {5:F2} ({6:F2}ms)  - Threaded: (F4) {7}", typeStr, directx.D3D.currentState.UseMap ? "Map/UnMap" : "UpdateSubresource", directx.D3D.currentState.SimulateCpuUsage ? "BurnCPU On" : "BurnCpu Off", directx.D3D.currentState.Type == directx.TestType.Deferred ? directx.D3D.currentState.ThreadCount : 1, directx.D3D.currentState.CountCubes * directx.D3D.currentState.CountCubes, 1000.0 * fpsCounter / fpsTimer.ElapsedMilliseconds, (float)fpsTimer.ElapsedMilliseconds / fpsCounter, runthreadedapp);
                                                //fpsTimerUpdateThread.Reset();


                                                fpsTimerUpdateThread.Reset();
                                                fpsTimerUpdateThread.Restart();

                                                //fpsTimerUpdateThread.Start();
                                                fpsCounterupdatethread = 0;
                                                fpsTimerUpdateThreadswtc = 1;
                                            }
                                            fpsCounterupdatethread++;


                                            fpsTimerUpdateThreadcounter = 0;
                                        }

                                        updatescript.Update(null, sccsjittertasks);

                                        if (fpsTimerUpdateThreadswtc == 1)
                                        {
                                            fpsTimerUpdateThread.Stop();

                                            var elapsedtimerticks = fpsTimerUpdateThread.ElapsedMilliseconds;

                                            sccsvvdhd.Form1.currentform.picturebox2fpsfloat0 = elapsedtimerticks;

                                            fpsTimerUpdateThreadswtc = 0;
                                        }


                                        fpsTimerUpdateThreadcounter++;

                                        /*
                                        fpsTimerShaderRenderThread.Reset();
                                        fpsTimerShaderRenderThread.Restart();*/



                                        if (fpsTimerShaderRenderThreadcounter >= 10)
                                        {

                                            //

                                            //fpsTimerShaderRenderThread.ElapsedTicks / fpsTimerShaderRenderThread.ElapsedMilliseconds


                                            //if (fpsTimerShaderRenderThread.ElapsedMilliseconds > 1000)
                                            {
                                                //fpsTimerShaderRenderThread.ElapsedTicks
                                                //var typeStr = directx.D3D.currentState.Type.ToString();
                                                //if (directx.D3D.currentState.Type != directx.TestType.Immediate && !directx.D3D.supportCommandList) typeStr += "*";


                                                //sccsr15forms.Form1.currentform.Text = string.Format("SharpDX - Threaded MultiCube D3D11  - (F1) {0} - (F2) {1} - (F3) {2} - Threads ↑↓{3} - Count ←{4}→ - FPS: {5:F2} ({6:F2}ms)  - Threaded: (F4) {7}", typeStr, directx.D3D.currentState.UseMap ? "Map/UnMap" : "UpdateSubresource", directx.D3D.currentState.SimulateCpuUsage ? "BurnCPU On" : "BurnCpu Off", directx.D3D.currentState.Type == directx.TestType.Deferred ? directx.D3D.currentState.ThreadCount : 1, directx.D3D.currentState.CountCubes * directx.D3D.currentState.CountCubes, 1000.0 * fpsCounter / fpsTimer.ElapsedMilliseconds, (float)fpsTimer.ElapsedMilliseconds / fpsCounter, runthreadedapp);
                                                fpsTimerShaderRenderThread.Reset();
                                                fpsTimerShaderRenderThread.Restart();
                                                //fpsCounterrenderthread = 0;
                                                fpsTimerShaderRenderThreadswtc = 1;
                                            }
                                            //fpsCounterrenderthread++;

                                            fpsTimerShaderRenderThreadcounter = 0;
                                        }



                                        updatescript.RenderAll(null, sccsjittertasks);







                                        if (fpsTimerShaderRenderThreadswtc == 1)
                                        {
                                            fpsTimerShaderRenderThread.Stop();

                                            var elapsedtimerticks = fpsTimerShaderRenderThread.ElapsedMilliseconds;

                                            sccsvvdhd.Form1.currentform.picturebox2fpsfloat1 = elapsedtimerticks;

                                            /*
                                            float secondfpsvalue = fpsTimerShaderRenderThread.ElapsedTicks;// fpsTimerShaderRenderThread.ElapsedTicks;// (float)fpsTimerShaderRenderThread.ElapsedMilliseconds / fpsCounterrenderthread;
                                            sccsvvdhd.Form1.currentform.picturebox2fpsfloat1 = secondfpsvalue;
                                            */

                                            //fpsTimerShaderRenderThread.Reset();
                                            //fpsTimerShaderRenderThread.Stop();
                                            //fpsTimerShaderRenderThread.Start();

                                            fpsTimerShaderRenderThreadswtc = 0;
                                        }
                                        fpsTimerShaderRenderThreadcounter++;




                                        if (fpsTimerShaderPresentThreadcounter >= 10)
                                        {
                                            //sccsr15forms.Form1.currentform.Text = string.Format("SharpDX - Threaded MultiCube D3D11  - (F1) {0} - (F2) {1} - (F3) {2} - Threads ↑↓{3} - Count ←{4}→ - FPS: {5:F2} ({6:F2}ms)  - Threaded: (F4) {7}", typeStr, directx.D3D.currentState.UseMap ? "Map/UnMap" : "UpdateSubresource", directx.D3D.currentState.SimulateCpuUsage ? "BurnCPU On" : "BurnCpu Off", directx.D3D.currentState.Type == directx.TestType.Deferred ? directx.D3D.currentState.ThreadCount : 1, directx.D3D.currentState.CountCubes * directx.D3D.currentState.CountCubes, 1000.0 * fpsCounter / fpsTimer.ElapsedMilliseconds, (float)fpsTimer.ElapsedMilliseconds / fpsCounter, runthreadedapp);
                                            fpsTimerShaderPresentThread.Reset();
                                            fpsTimerShaderPresentThread.Restart();
                                            //fpsCounterrenderthread = 0;
                                            fpsTimerShaderPresentThreadswtc = 1;
                                            fpsTimerShaderPresentThreadcounter = 0;
                                        }



                                        updatescript.PresentDirectx(null, sccsjittertasks);



                                        if (fpsTimerShaderPresentThreadswtc == 1)
                                        {
                                            fpsTimerShaderPresentThread.Stop();

                                            var elapsedtimerticks = fpsTimerShaderPresentThread.ElapsedMilliseconds;

                                            sccsvvdhd.Form1.currentform.picturebox2fpsfloat2 = elapsedtimerticks;

                                            fpsTimerShaderPresentThreadswtc = 0;
                                        }
                                        fpsTimerShaderPresentThreadcounter++;







                                        //Console.WriteLine("test1");
                                    }
                                    else if (usejitterphysics == 1)
                                    {

                                        updatescript.RenderAll(jitter_sc, sccsjittertasks);
                                        //updatescript.Update(jitter_sc, sccsjittertasks);


                                    }

                                }
                            }
                            //Console.WriteLine("test");

                        }


















                        if (changedscreencapturetype == 1) //(changedscreencapturetype == 1
                        {
                            /*
                            updatescript.ShutDownGraphics();


                            if (sccs.scgraphics.scdirectx.D3D != null)
                            {
                                sccs.scgraphics.scdirectx.D3D.ShutDown();
                            }




                            updatescript.exitthread0 = 0;
                            updatescript.exitthread1 = 0;
                            updatescript = null;

                                 

                            DeleteObject(vewindowsfoundedz);

                            getwindowthreadprocessidint = 0;
                            textureresetswtc = 0;
                            GC.Collect();
                            changedscreencapturetype = 0;
                            //createinputsswtc = 0;
                            hasresettedcapture = 1;
                            initform = 1;
                            //sccs.Program.MessageBox((IntPtr)0, "capture reset1", "scmsg", 0);
                              */
                            changedscreencapturetype = 0;
                        }
                        else
                        {
                            lastscreencapturetype = screencapturetype;
                        }


                        //sccs.Program.MessageBox((IntPtr)0, "thread initiated", "scmsg", 0);
                        Thread.Sleep(1);

                        //MessageBox((IntPtr)0, "threadm0", "scmsg", 0);
                        goto _thread_main_loop;
                        //MessageBox((IntPtr)0, "threadm -1", "scmsg", 0);
                        //_thread_start:
                    }, 0); //100000 //999999999

                    _mainThread.IsBackground = true;
                    _mainThread.Priority = ThreadPriority.Normal; //AboveNormal
                    _mainThread.SetApartmentState(ApartmentState.STA);
                    _mainThread.Start();



                    //sccs.Program.MessageBox((IntPtr)0, "thread initiated", "scmsg", 0);
                    initthread = 2;
                }


                if (initthread == 2)
                {
                    if (initform == 0)
                    {
                        initform = 1;



                        string WINDOW_NAME = "sccsr14";
                        //IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
                        //SetWindowLong(someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_TRANSPARENT)); // | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW | WS_EX_LAYERED 
                        //SetLayeredWindowAttributes(someform.Handle, 0, 255, LWA_ALPHA);


                        /*if (System.Diagnostics.Debugger.IsAttached)
                        {
                            //MessageBox((IntPtr)0, "threadm1", "scmsg", 0);


                            Thread.Sleep(1);
                            goto mainthreadloop;
                        }
                        else
                        {
                            //MessageBox((IntPtr)0, "threadm2", "scmsg", 0);


                            Thread.Sleep(1);
                            goto mainthreadloop;
                        }*/

                        //MessageBox((IntPtr)0, "form initiated1", "scmsg", 0);

                    }
                    else if (initform == 2)
                    {
                        /*if (System.Diagnostics.Debugger.IsAttached)
                        {
                            //MessageBox((IntPtr)0, "threadm1", "scmsg", 0);
                            Thread.Sleep(1);
                            goto mainthreadloop;
                        }
                        else
                        {
                            //MessageBox((IntPtr)0, "threadm2", "scmsg", 0);
                            Thread.Sleep(1);
                            goto mainthreadloop;
                        }*/
                    }

                    /*if (System.Diagnostics.Debugger.IsAttached)
                    {
                        //MessageBox((IntPtr)0, "threadm1", "scmsg", 0);
                        Thread.Sleep(1);
                        goto mainthreadloop;
                    }
                    else
                    {
                        //MessageBox((IntPtr)0, "threadm2", "scmsg", 0);
                        Thread.Sleep(1);
                        goto mainthreadloop;
                    }*/
                }
                else
                {
                    //MessageBox((IntPtr)0, "threadm3", "scmsg", 0);
                    //AppDomain.CurrentDomain.ProcessExit += ProcessExitHandler;


                    //Thread.Sleep(1);
                    //goto mainthreadloop;
                }
                // ReSharper restore AccessToDisposedClosure
                Thread.Sleep(1);
            });

        
        }
        //public static RenderForm form;



        static System.Windows.Forms.TextBox textBox;








        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);



        //public static RenderForm form;
        /// <summary>
        /// Updates the mouse text.
        /// </summary>
        /// <param name="rawArgs">The <see cref="SharpDX.RawInput.RawInputEventArgs"/> instance containing the event data.</param>
        static void UpdateMouseText(object sender, MouseInputEventArgs rawArgs) // EventArgs e  //object sender,
        {
            //MessageBox((IntPtr)0, "test0", "scmsg", 0);
            var args = (MouseInputEventArgs)rawArgs;

            //textBox.AppendText(string.Format("(x,y):({0},{1}) Buttons: {2} State: {3} Wheel: {4}\r\n", args.X, args.Y, args.ButtonFlags, args.Mode, args.WheelDelta));
            //Console.WriteLine(args.X + " " + args.Y + " " + args.ButtonFlags + " "  + args.Mode);

            //mrbuttondown = 0;
            //mlbuttondown = 0;

            if (args.ButtonFlags == mouseleftdownflag)
            {
                mlbuttondown = 1;
                //mlbuttondown = 1;
                //MessageBox((IntPtr)0, "mldown", "scmsg", 0);
            }
            else
            {
                mlbuttondown = 0;
            }


            if (args.ButtonFlags == mouserightdownflag)
            {
                mrbuttondown = 1;
                //MessageBox((IntPtr)0, "mrdown", "scmsg", 0);
            }
            else
            {
                mrbuttondown = 0;
            }




            //Console.WriteLine(args.X + " " + args.Y + " " + args.ButtonFlags + " " + args.Mode + " " + mlbuttondown);
        }

        public static int mlbuttondown = 0;
        public static int mrbuttondown = 0;






        static MouseButtonFlags mouseleftdownflag = MouseButtonFlags.LeftButtonDown;
        static MouseButtonFlags mouserightdownflag = MouseButtonFlags.RightButtonDown;



        /// <summary>
        /// Updates the keyboard text.
        /// </summary>
        /// <param name="rawArgs">The <see cref="SharpDX.RawInput.RawInputEventArgs"/> instance containing the event data.</param>
        static void UpdateKeyboardText(object sender, KeyboardInputEventArgs rawArgs) //object sender,
        {
            //MessageBox((IntPtr)0, "test1", "scmsg", 0);
            var args = (KeyboardInputEventArgs)rawArgs;
            textBox.AppendText(string.Format("Key: {0} State: {1} ScanCodeFlags: {2}\r\n", args.Key, args.State, args.ScanCodeFlags));

            //Console.WriteLine(args.Key + " " + args.State);
        }

        /// <summary>
        /// Delegate use for printing events
        /// </summary>
        /// <param name="args">The <see cref="SharpDX.RawInput.RawInputEventArgs"/> instance containing the event data.</param>
        public delegate void UpdateTextCallback(RawInputEventArgs args);



        private const int WM_INPUT = 0x00FF;
        private static IntPtr WndProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_INPUT)
            {
                SharpDX.RawInput.Device.HandleMessage(lParam, hWnd);
            }
            //MessageBox((IntPtr)0, "error WndProc", "scmsg", 0);
            return IntPtr.Zero;
        }





        /// </summary>
        public enum SpecialWindowHandles
        {
            // ReSharper disable InconsistentNaming
            /// <summary>
            ///     Places the window at the top of the Z order.
            /// </summary>
            HWND_TOP = 0,
            /// <summary>
            ///     Places the window at the bottom of the Z order. If the hWnd parameter identifies a topmost window, the window loses its topmost status and is placed at the bottom of all other windows.
            /// </summary>
            HWND_BOTTOM = 1,
            /// <summary>
            ///     Places the window above all non-topmost windows. The window maintains its topmost position even when it is deactivated.
            /// </summary>
            HWND_TOPMOST = -1,
            /// <summary>
            ///     Places the window above all non-topmost windows (that is, behind all topmost windows). This flag has no effect if the window is already a non-topmost window.
            /// </summary>
            HWND_NOTOPMOST = -2
            // ReSharper restore InconsistentNaming
        }



        [Flags]
        public enum SetWindowPosFlags : uint
        {
            // ReSharper disable InconsistentNaming

            /// <summary>
            ///     If the calling thread and the thread that owns the window are attached to different input queues, the system posts the request to the thread that owns the window. This prevents the calling thread from blocking its execution while other threads process the request.
            /// </summary>
            SWP_ASYNCWINDOWPOS = 0x4000,

            /// <summary>
            ///     Prevents generation of the WM_SYNCPAINT message.
            /// </summary>
            SWP_DEFERERASE = 0x2000,

            /// <summary>
            ///     Draws a frame (defined in the window's class description) around the window.
            /// </summary>
            SWP_DRAWFRAME = 0x0020,

            /// <summary>
            ///     Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE is sent only when the window's size is being changed.
            /// </summary>
            SWP_FRAMECHANGED = 0x0020,

            /// <summary>
            ///     Hides the window.
            /// </summary>
            SWP_HIDEWINDOW = 0x0080,

            /// <summary>
            ///     Does not activate the window. If this flag is not set, the window is activated and moved to the top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOACTIVATE = 0x0010,

            /// <summary>
            ///     Discards the entire contents of the client area. If this flag is not specified, the valid contents of the client area are saved and copied back into the client area after the window is sized or repositioned.
            /// </summary>
            SWP_NOCOPYBITS = 0x0100,

            /// <summary>
            ///     Retains the current position (ignores X and Y parameters).
            /// </summary>
            SWP_NOMOVE = 0x0002,

            /// <summary>
            ///     Does not change the owner window's position in the Z order.
            /// </summary>
            SWP_NOOWNERZORDER = 0x0200,

            /// <summary>
            ///     Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent window uncovered as a result of the window being moved. When this flag is set, the application must explicitly invalidate or redraw any parts of the window and parent window that need redrawing.
            /// </summary>
            SWP_NOREDRAW = 0x0008,

            /// <summary>
            ///     Same as the SWP_NOOWNERZORDER flag.
            /// </summary>
            SWP_NOREPOSITION = 0x0200,

            /// <summary>
            ///     Prevents the window from receiving the WM_WINDOWPOSCHANGING message.
            /// </summary>
            SWP_NOSENDCHANGING = 0x0400,

            /// <summary>
            ///     Retains the current size (ignores the cx and cy parameters).
            /// </summary>
            SWP_NOSIZE = 0x0001,

            /// <summary>
            ///     Retains the current Z order (ignores the hWndInsertAfter parameter).
            /// </summary>
            SWP_NOZORDER = 0x0004,

            /// <summary>
            ///     Displays the window.
            /// </summary>
            SWP_SHOWWINDOW = 0x0040,

            // ReSharper restore InconsistentNaming
        }


        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);





        const int SW_HIDE = 0;
        const int SW_SHOWNORMAL = 1;
        const int SW_NORMAL = 1;
        const int SW_SHOWMINIMIZED = 2;
        const int SW_SHOWMAXIMIZED = 3;
        const int SW_MAXIMIZE = 3;
        const int SW_SHOWNOACTIVATE = 4;
        const int SW_SHOW = 5;
        const int SW_MINIMIZE = 6;
        const int SW_SHOWMINNOACTIVE = 7;
        const int SW_SHOWNA = 8;
        const int SW_RESTORE = 9;
        const int SW_SHOWDEFAULT = 10;
        const int SW_FORCEMINIMIZE = 11;
        const int SW_MAX = 11;
        public const UInt32 SWP_NOSIZE = 0x0001;
        public const UInt32 SWP_NOMOVE = 0x0002;
        public const UInt32 SWP_NOZORDER = 0x0004;
        public const UInt32 SWP_NOREDRAW = 0x0008;
        public const UInt32 SWP_NOACTIVATE = 0x0010;
        public const UInt32 SWP_FRAMECHANGED = 0x0020;
        public const UInt32 SWP_SHOWWINDOW = 0x0040;
        public const UInt32 SWP_HIDEWINDOW = 0x0080;
        public const UInt32 SWP_NOCOPYBITS = 0x0100;
        public const UInt32 SWP_NOOWNERZORDER = 0x0200;
        public const UInt32 SWP_NOSENDCHANGING = 0x0400;
        public const UInt32 SWP_DRAWFRAME = SWP_FRAMECHANGED;
        public const UInt32 SWP_NOREPOSITION = SWP_NOOWNERZORDER;

        public const UInt32 SWP_DEFERERASE = 0x2000;
        public const UInt32 SWP_ASYNCWINDOWPOS = 0x4000;






        //https://stackoverflow.com/questions/14524720/correct-use-of-safehandles-in-this-p-invoke-use-case
        /*
        internal class MyOwnedSafeHandleA : MySafeHandleA
        {
            protected override bool ReleaseHandle()
            {
                releaseHandleToA(handle);
                return true;
            }
        }

        internal class MySafeHandleA : SafeHandle
        {
            private int refCountIncremented;

            internal void IncrementRefCount(Action<MySafeHandleA> nativeIncrement)
            {
                nativeIncrement(this);
                refCountIncremented++;
            }

            protected override bool ReleaseHandle()
            {
                while (refCountIncremented > 0)
                {
                    releaseHandleToA(handle);
                    refCountIncremented--;
                }

                return true;
            }
        }

        [DllImport("somedll.dll")]
        public extern MyOwnedSafeHandleA makeNewHandleOfA();
        [DllImport("somedll.dll")]
        private extern MySafeHandleA getHandleOfA(MySafeHandleB handleToB, int index);
        [DllImport("somedll.dll")]
        private extern void addRefHandleToA(MySafeHandleA handleToA);
        internal class MySafeHandleA : SafeHandle
        {
            MySafeHandleA(IntPtr handle) : base(IntPtr.Zero, true)
            {
                SetHandle(handle);
            }

            protected override bool ReleaseHandle()
            {
                releaseHandleToA(handle);
                return true;
            }
        }*/

        /*
        [DllImport("somedll.dll"]
        private extern IntPtr getHandleOfA(MySafeHandleB handleToB, int index);
        [DllImport("somedll.dll"]
        private extern void addRefHandleToA(IntPtr ptr);

        public MySafeHandleA _getHandleOfA(MySafeHandleB handleToB, int index)
        {
            IntPtr ptr = getHandleOfA(handleToB, index);
            addRefHandleToA(ptr);
            return new MySafeHandleA(ptr);
        }*/
    }
}
