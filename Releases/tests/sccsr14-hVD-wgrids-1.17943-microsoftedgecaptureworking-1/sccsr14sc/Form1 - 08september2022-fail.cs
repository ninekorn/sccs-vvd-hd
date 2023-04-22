//made by Steve Chassé's core systems


using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using sccs;
using sccs.sccore;
using sccs.scconsole;
using sccs.scgraphics;

using System.Threading;
using System.Threading.Tasks;

using System.Runtime.InteropServices;


using System.Windows.Interop;

using SharpDX.RawInput;
using SharpDX.Multimedia;

using System.Windows.Threading;
using sccs.scmessageobject;

//using WindowsInput;
using SharpDX.DirectInput;
using SharpDX.RawInput;

using WindowsInput;

using InputSimulator = WindowsInput.InputSimulator;

using VirtualKeyCode = WindowsInput.Native.VirtualKeyCode;
using System.Runtime.ConstrainedExecution;

using System.Windows;


namespace sccsr14sc
{
    [System.ComponentModel.Browsable(false)]
    public partial class Form1 : Form
    {



        public static string capturedwindownameform1 = "";



        public static void UnSetFocus(IntPtr hwndTarget, string childClassName)
        {

            //IntPtr targetThreadID = GetWindowThreadProcessId(hwndTarget, IntPtr.Zero); //target thread id
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
            /*IntPtr targetThreadID = GetWindowThreadProcessId(hwndTarget, IntPtr.Zero); //target thread id
            IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id
            try
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, -1); // attach current thread id to target window

                // if it's not already in the foreground...
                lRet = BringWindowToTop(hwndTarget);
                SetForegroundWindow(hwndTarget);

                // if you know the child win class name do something like this (enumerate windows using Win API again)...
                var hwndChild = EnumAllWindows(hwndTarget, childClassName).FirstOrDefault();

                if (hwndChild == IntPtr.Zero)
                {
                    // or use keyboard etc. to focus, i.e. send keys/input...
                    // SendInput (...);
                    return;
                }

                // you can use also the edit control's hwnd or some child window (of target) here
                SetFocus(hwndChild); // hwndTarget);
            }
            finally
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, 0); //detach from foreground window
            }*/
        }
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

        void SetFocus(IntPtr hwndTarget, string childClassName)
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
                /*var hwndChild = EnumAllWindows(hwndTarget, childClassName).FirstOrDefault();

                if (hwndChild == IntPtr.Zero)
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
            /*IntPtr targetThreadID = GetWindowThreadProcessId(hwndTarget, IntPtr.Zero); //target thread id
            IntPtr myThreadID = GetCurrentThread(); // calling thread id, our thread id
            try
            {
                bool lRet = AttachThreadInput(myThreadID, targetThreadID, -1); // attach current thread id to target window

                // if it's not already in the foreground...
                lRet = BringWindowToTop(hwndTarget);
                SetForegroundWindow(hwndTarget);

                // if you know the child win class name do something like this (enumerate windows using Win API again)...
                var hwndChild = EnumAllWindows(hwndTarget, childClassName).FirstOrDefault();

                if (hwndChild == IntPtr.Zero)
                {
                    // or use keyboard etc. to focus, i.e. send keys/input...
                    // SendInput (...);
                    return;
                }

                // you can use also the edit control's hwnd or some child window (of target) here
                SetFocus(hwndChild); // hwndTarget);
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
        
        
        
        
        /*
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
        }*/
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

        //https://stackoverflow.com/questions/36952600/determine-if-a-window-is-topmost-or-not
        [DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_TOPMOST = 0x0008;

        public static bool IsWindowTopMost(IntPtr hWnd)
        {
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (exStyle & WS_EX_TOPMOST) == WS_EX_TOPMOST;
        }

        static long WS_MAXIMIZE = 0x01000000L;

        public static bool IsWindowMaximized(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_STYLE);
            return (exStyle & WS_MAXIMIZE) == WS_MAXIMIZE;
        }

        public static bool IsWindowEdge(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (exStyle & WS_EX_WINDOWEDGE) == WS_EX_WINDOWEDGE;
        }


        static long WS_EX_STATICEDGE = 0x00020000L;
        public static bool IsWindowWS_EX_OVERLAPPEDWINDOW(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (exStyle & WS_EX_STATICEDGE) == WS_EX_STATICEDGE;
        }


        /*
        public static bool IsWindowminimized(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (exStyle & WS_EX_STATICEDGE) == WS_EX_STATICEDGE;
        }*/






        [DllImport("user32.dll")]
        public static extern int FindWindow(
        string lpClassName, // class name 
        string lpWindowName // window name 
        );


        [DllImport("user32.dll")]
        public static extern bool PostMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        public const int WM_KEYDOWN = 0x100;
        public const int WM_KEYUP = 0x101;


        const uint SWP_SHOWWINDOW = 0x40;


        int numericupdown1swtc = 0;
        int numericupdown2swtc = 0;
        int numericupdown3swtc = 0;
        int numericupdown4swtc = 0;
        int numericupdown5swtc = 0;
        int numericupdown6swtc = 0;

        public int left;
        public int top;
        public int width;
        public int height;

        /*public NumericUpDown numericUpDown01;
        public NumericUpDown numericUpDown02;
        public NumericUpDown numericUpDown03;
        public NumericUpDown numericUpDown04;
        public NumericUpDown numericUpDown05;
        public NumericUpDown numericUpDown06;*/


        public Label labeltext0;
        //public Label labeltext1;
        public Label labeltext2;
        public Label labeltext3;


        public static ComboBox comboboxcapturelist;

        public static CheckBox checkbox1;
        public static CheckBox checkbox2;
        public static CheckBox checkbox3;
        public CheckBox checkbox4proj;


        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect);

        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("gdi32.dll")]

        static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);


        public static CheckedListBox checkedlistbox;

        public static TrackBar trackbar;

        public static TextBox textBox;
        public static Button thebutton;
        public static Button button1exit;

        public static Button button2changeprog;

        public int checkbox4swtc = 0;

        //[System.ComponentModel.Browsable(false)]
        //protected virtual bool ShowWithoutActivation { get; }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);
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

        public static int initForm = 0;
        //Thread _mainTasker00;

        public static Panel thepanel;
        public static Form1 someform;




        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);



        [DllImport("user32.dll")]
        static extern int ShowCursor(bool bShow);





        public TrackBar colortrackBar2;
        public TrackBar colortrackBar3;
        public TrackBar colortrackBar4;
        public TrackBar colortrackBar5;
        public TrackBar colortrackBar6;
        public TrackBar colortrackBar7;


        public Form1()
        {
            //System.Windows.Forms.Cursor.Hide();
            //System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.None;
            //ShowCursor(false);


            //this.Name = "sccs";

            InitializeComponent();


            this.KeyPreview = true;

            this.KeyDown += new KeyEventHandler(KeyboardKeyDownAlt);
            this.KeyDown += new KeyEventHandler(KeyboardKeyDownTab);
            this.KeyUp += new KeyEventHandler(KeyboardKeyUp);
            this.KeyPress += KeyboardKeyPress;


            /*
            textBox1.KeyDown += TextBox1_KeyDown;
            textBox1.KeyPress += TextBox1_KeyPress;
            textBox1.KeyUp += TextBox1_KeyUp;*/

            

            //System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.None;


            //textBox = this.textBox1;
            /*textBox = new TextBox() { Dock = DockStyle.Fill, Multiline = true, Text = "Interact with the mouse or the keyboard...\r\n", ReadOnly = true };
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Text = "Interact with the mouse or the keyboard...\r\n";
            textBox.ReadOnly = true;


            this.Controls.Add(textBox);*/
            //this.Visible = true;


            //Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
            //Device.KeyboardInput += (sender, args) => textBox.Invoke(new UpdateTextCallback(UpdateKeyboardText), args);


            /*
            var _hwndSource = HwndSource.FromHwnd(this.Handle);
            if (_hwndSource != null)
                _hwndSource.AddHook(WndProc);

            /*SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                SharpDX.RawInput.DeviceFlags.None, this.Handle);
            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                SharpDX.RawInput.DeviceFlags.None, this.Handle);
            //SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;
            */
            //this.BackColor = System.
            //OnLoad += Form1_Resize;

            //Loaded += Form1_Resize; ;

            //this.Cursor.
            //System.Windows.Forms.Cursor.Hide();
            mainreceivedmessages = new scmessageobject[MaxSizeMainObject];

            for (int i = 0; i < mainreceivedmessages.Length; i++)
            {
                mainreceivedmessages[i] = new scmessageobject();
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


                //mainreceivedmessages[0]._someData[0] = new object();


                //VOICE RECOGNITION. NOT IMPLEMENTED YET.
                /*if (i == MaxSizeMainObject - 1)
                {
                    mainreceivedmessages[i]._someData[0] = _keyboard_input._KeyboardState;
                    mainreceivedmessages[i]._voRecSwtc = 1;
                }*/
            }
            //SCGLOBALSACCESSORS AND GLOBALS CREATOR
            /*SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages);
            if (SCGLOBALSACCESSORS == null)
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
            }
            else
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
            }*/

            // Configure time, but don't start yet
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = TIMER_INTERVAL;       // Set interval
            _timer.Tick += _timer_Tick;

            this.MouseMove += Mouse_Move;
            this.MouseEnter += new System.EventHandler(Form1_MouseEnter);
            this.MouseEnter += Form1_MouseEnter;


            this.MouseLeave += Form1_MouseLeave;
            this.MouseClick += form1mouseclicked;

            this.MouseDown += form1mousedown;
            this.MouseUp += form1mouseup;


            this.Load += Form1_Load;
            this.Shown += Form1_Shown;

            this.checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            this.checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            this.checkBox3.CheckedChanged += checkBox3_CheckedChanged;
            this.checkBox4.CheckedChanged += checkBox4_CheckedChanged;

            checkbox4proj = this.checkBox4;

            this.button1.MouseEnter += onbuttonmouseenter;
            this.button1.MouseClick += onbuttonmouseclick;// button1_Click_1;
            this.button1.MouseHover += onbuttonmousehovers;
            //this.button1.MouseClick += button1_Click_1;

            button1exit = this.button1;

            button2changeprog = this.button2;




            //this.checkBox2.CheckedChanged += checkBox2_CheckedChanged;

            //this.trackBar1.Scroll += new System.EventHandler(trackBar1_Scroll);
            this.trackBar1.ValueChanged += new System.EventHandler(trackBar1_Scroll);
            //this.Load += OnLoaded;



            this.Resize += Form1_Resize;
            


            this.trackBar1.Minimum = -20000;
            this.trackBar1.Maximum = 20000;
            this.trackBar1.Value = -1000;

            this.trackBar1.TickFrequency = 150;


            this.colortrackBar2 = this.trackBar2;
            this.colortrackBar3 = this.trackBar3;
            this.colortrackBar4 = this.trackBar4;
            this.colortrackBar5 = this.trackBar5;
            this.colortrackBar6 = this.trackBar6;
            this.colortrackBar7 = this.trackBar7;



            this.colortrackBar2.ValueChanged += trackBar2_Scroll;
            this.colortrackBar3.ValueChanged += trackBar3_Scroll;
            this.colortrackBar4.ValueChanged += trackBar4_Scroll;
            this.colortrackBar5.ValueChanged += trackBar5_Scroll;
            this.colortrackBar6.ValueChanged += trackBar6_Scroll;
            this.colortrackBar7.ValueChanged += trackBar7_Scroll;
           
            
            this.colortrackBar2.Minimum = 0;
            this.colortrackBar2.Maximum = 255;
            this.colortrackBar2.Value = 0;
            this.colortrackBar2.TickFrequency = 1;

            this.colortrackBar3.Minimum = 0;
            this.colortrackBar3.Maximum = 255;
            this.colortrackBar3.Value = 0;
            this.colortrackBar3.TickFrequency = 1;

            this.colortrackBar4.Minimum = 0;
            this.colortrackBar4.Maximum = 255;
            this.colortrackBar4.Value = 0;
            this.colortrackBar4.TickFrequency = 1;

            this.colortrackBar5.Minimum = 0;
            this.colortrackBar5.Maximum = 255;
            this.colortrackBar5.Value = 0;
            this.colortrackBar5.TickFrequency = 1;

            this.colortrackBar6.Minimum = 0;
            this.colortrackBar6.Maximum = 255;
            this.colortrackBar6.Value = 0;
            this.colortrackBar6.TickFrequency = 1;

            this.colortrackBar7.Minimum = 0;
            this.colortrackBar7.Maximum = 255;
            this.colortrackBar7.Value = 0;
            this.colortrackBar7.TickFrequency = 1;








            //colortrackBar2




            heightmapvalue = this.trackBar1.Value;
            heightmapvaluemax = this.trackBar1.Maximum;
            heightmapvaluemin = this.trackBar1.Minimum;
            heightmapvaluetickfreq = this.trackBar1.TickFrequency;

            //this.OnResize += Form1_Resize;

            //this.SizeChanged += Form1_Resize;

            //ShowCursor(false);
            //this.Cursor = System.Windows.Input.Cursors.None;

            //this.Cursor = Cursors.hi ;
            //Cursor.Hide();


            this.CheckedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheckChanged);  //checkedListBox1_SelectedIndexChanged;

            /*
            this.numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            this.numericUpDown2.ValueChanged += numericUpDown2_ValueChanged;
            this.numericUpDown3.ValueChanged += numericUpDown3_ValueChanged;
            this.numericUpDown4.ValueChanged += numericUpDown4_ValueChanged;
            this.numericUpDown5.ValueChanged += numericUpDown5_ValueChanged;
            this.numericUpDown6.ValueChanged += numericUpDown6_ValueChanged;


            this.numericUpDown1.MouseDown += numericupdownresetfocus;
            this.numericUpDown2.MouseDown += numericupdownresetfocus;
            this.numericUpDown3.MouseDown += numericupdownresetfocus;
            this.numericUpDown4.MouseDown += numericupdownresetfocus;
            this.numericUpDown5.MouseDown += numericupdownresetfocus;
            this.numericUpDown6.MouseDown += numericupdownresetfocus;
            */



            //this.eve


            //this.bro
            //ShowWithoutActivation.fal;


            //sccs.Program.MessageBox((IntPtr)0, "Form1", "scmsg", 0);

            //System.Windows.Forms.Application.EnableVisualStyles();
            //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            //someform = new Form1();
            someform = this;
            //System.Windows.Forms.Application.Run(this);

            //sccs.Program.MessageBox((IntPtr)0, "Form1", "scmsg", 0);
            //thepanel = this.panel1;
            trackbar = this.trackBar1;
            checkedlistbox = this.CheckedListBox1;
            checkbox1 = this.checkBox1;
            checkbox2 = this.checkBox2;
            checkbox3 = this.checkBox3;
            checkbox4proj = this.checkBox4;
           
            
            
            
            /*
            checkBox1.Hide();
            checkBox2.Hide();
            checkBox3.Hide();*/


            //checkbox4proj.Hide();



            /*
            numericUpDown01 = this.numericUpDown1;
            numericUpDown02 = this.numericUpDown2;
            numericUpDown03 = this.numericUpDown3;
            numericUpDown04 = this.numericUpDown4;
            numericUpDown05 = this.numericUpDown5;
            numericUpDown06 = this.numericUpDown6;*/

            /*
            cursorlightcolorr = (int)numericUpDown01.Value;
            cursorlightcolorg = (int)numericUpDown02.Value;
            cursorlightcolorb = (int)numericUpDown03.Value;
            gridcolorr = (int)numericUpDown04.Value;
            gridcolorg = (int)numericUpDown05.Value;
            gridcolorb = (int)numericUpDown06.Value;*/












            comboboxcapturelist = this.comboBox1;
            this.comboBox1.SelectedIndex = 0;
            //comboboxcapturelist.SelectedItem =



            this.comboBox1.SelectedValueChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);


            left = this.Left;
            top = this.Top;
            width = this.Width;
            height = this.Height;


            labeltext0 = this.label1;
            //labeltext1 = this.label2;
            labeltext2 = this.label4;
            labeltext3 = this.label3;


            cursorlightoption = 3;



            //this.KeyPress+= new System.Windows.Forms.KeyPressEventHandler(KeyboardKeyPress);
            //this.KeyDown += KeyboardKeyDown;
            //this.KeyUp += KeyboardKeyUp;
            //this.KeyPress += KeyboardKeyPress;




            /*ComboBox comboBox = (ComboBox)sender;
            // Save the selected employee's name, because we will remove
            // the employee's name from the list.
            string selectedEmployee = (string)comboboxcapturelist.SelectedItem;
            int count = 0;
            int resultIndex = -1;
            // Call the FindStringExact method to find the first 
            // occurrence in the list.
            screencaptureindextype = comboboxcapturelist.FindStringExact(selectedEmployee);
            */



            //thepanel.BringToFront();


            //this.BringToFront();
            /*
            System.Windows.Forms.Cursor cursor = null;
            var info = System.Windows.Application.GetResourceStream(
                    new Uri(@"pack://application:,,,/Resources/Icon1.ico"));  //@"C:\Users\steve\Desktop\cursor.png"));//@"pack://application:,,,/Resources/MyCursor.cur"));

            //new Uri(@"pack://application:,,,/C:/User/steve/Documents/GitHub/sccs/tests/sccsr14-DemoNoControls/sccsr14sc/resources/cursor.png"));  //@"C:\Users\steve\Desktop\cursor.png"));//@"pack://application:,,,/Resources/MyCursor.cur"));

            cursor = new System.Windows.Forms.Cursor(info.Stream);

            this.Cursor = cursor;*/





            /*textBox = this.textBox1;
            //textBox = new TextBox() { Dock = DockStyle.Fill, Multiline = true, Text = "Interact with the mouse or the keyboard...\r\n", ReadOnly = true };
            textBox.Dock = DockStyle.Fill;
            textBox.Multiline = true;
            textBox.Text = "Interact with the mouse or the keyboard...\r\n";
            textBox.ReadOnly = true;*/


            //form.Controls.Add(textBox);
            //form.Visible = true;

            /*var backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += (object sender, DoWorkEventArgs args) =>
            {
                while (true)
                {
                    var refreshDXEngineAction = new Action(delegate
                    {
                        if (haspressedf9 == 1)
                        {
                            if (thepanel.Visible == false)
                            {
                                //sccs.Program.MessageBox((IntPtr)0, "thepanel Visible", "scmsg", 0);

                                Console.WriteLine("thepanel Visible");
                                //thepanel.Visible = true;
                            }
                            else if (thepanel.Visible == true)
                            {
                                Console.WriteLine("thepanel !Visible");
                                //sccs.Program.MessageBox((IntPtr)0, "! thepanel Visible", "scmsg", 0);


                                //thepanel.Visible = false;
                            }
                            haspressedf9 = 0;
                        }
                    });


                    Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);

                    //System.Windows.Threading.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction); //System.Windows.Threading.DispatcherPriority.Normal
                    Thread.Sleep(1);
                }


            };

            backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
            {
                Console.WriteLine("worker ended");
            };

            backgroundWorker.RunWorkerAsync();*/


            //thebutton = this.button1;

            //Program.createinputs(this.Handle);
            //Program.createinputs(Form1.someform.Handle);



            /*sccsr14sc.Form1.haspressedescape = 2;

            /*else if (sccsr14sc.Form1.someform.haspressedf9 == 1)
            {


            }

            sccsr14sc.Form1.someform.haspressedsomekeyboardkey = 0;
            counterpanelchanged = 0;

            panelchangedswtc = 0;
            //sccsr14sc.Form1.someform.haspressedf9 = 2;
            panelchangedwatch.Stop();*/










            /*
            sccsr14sc.Form1.button1exit.Visible = false;
            sccsr14sc.Form1.button2changeprog.Visible = false;
            sccsr14sc.Form1.someform.labeltext0.Visible = false;
            sccsr14sc.Form1.someform.labeltext1.Visible = false;

            sccsr14sc.Form1.someform.labeltext2.Visible = false;

            sccsr14sc.Form1.someform.labeltext3.Visible = false;
            sccsr14sc.Form1.someform.numericUpDown01.Visible = false;
            sccsr14sc.Form1.someform.numericUpDown02.Visible = false;

            sccsr14sc.Form1.someform.numericUpDown03.Visible = false;

            sccsr14sc.Form1.someform.numericUpDown04.Visible = false;
            sccsr14sc.Form1.someform.numericUpDown05.Visible = false;
            sccsr14sc.Form1.someform.numericUpDown06.Visible = false;
            sccsr14sc.Form1.comboboxcapturelist.Visible = false;
            sccsr14sc.Form1.trackbar.Visible = false;
            sccsr14sc.Form1.checkedlistbox.Visible = false;
            */







        }

        /*
        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            //ShowCursor(false);
            //System.Windows.Forms.Cursor.Hide();
            //Cursor.Hide();
            //MessageBox((IntPtr)0, "Hide", "sccsmsg", 0);
        }*/




        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);


        public void onbuttonmousehovers(object sender, EventArgs e)
        {

            /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));


            Program.RECT therect = new Program.RECT();
            therect.Left = 0;
            therect.Top = 0;
            therect.Bottom = 1080;
            therect.Right = 1920;

            param.ShowCmd = Program.ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
            Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);
            */
            /*
            //MessageBox((IntPtr)0, "onbuttonmouseclick", "sccsmsg", 0);

            button2exit.Select();
            button2exit.Focus();
            button2exit.Update();

            //sccsr14sc.Form1.someform.Size = new System.Drawing.Size(1920, 1080);
            sccsr14sc.Form1.someform.FormBorderStyle = FormBorderStyle.None;
            //sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
            sccsr14sc.Form1.someform.TopMost = true;
            //sccsr14sc.Form1.someform.Focus();
            //sccsr14sc.Form1.someform.Select();
            //button2exit.PerformClick();

            this.TopMost = true;
            this.Focus();
            this.BringToFront();
            this.Activate();
            SetFocus(theHandle);*/


        }


        Program.Win32Point somepoint;
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };


        public void onbuttonmouseclick(object sender, EventArgs e)
        {








            //button2.Image = 

            //MessageBox((IntPtr)0, "onbuttonmouseclick", "sccsmsg", 0);
            /*
            button1exit.Select();
            button1exit.Focus();
            button1exit.Update();*/

            //sccsr14sc.Form1.someform.Size = new System.Drawing.Size(1920, 1080);
            /*sccsr14sc.Form1.someform.FormBorderStyle = FormBorderStyle.None;
            //sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
            sccsr14sc.Form1.someform.TopMost = true;
            sccsr14sc.Form1.someform.Focus();
            sccsr14sc.Form1.someform.Select();*/
            //button1exit.PerformClick();



            /*
            if (mousex )
            {

            }
            */





            if (Program.exitedprogram == 1)
            {













                button1exit.PerformClick();
                /*Program.GetCursorPos(out somepoint);

                mousex = somepoint.X;
                mousey = somepoint.Y;

                
                iHandle = scgraphicssec.FindWindow(null, capturedwindownameform1);
                scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                */
                //button1exit.Select();
                //button1exit.Focus();
                
                button1exit.PerformClick();
                button1exit.Update();

                Console.WriteLine("onbuttonmouseclick");
                //button1exit.BeginInvoke();
                //button1exit.Invoke();
                //button1exit.




                //https://stackoverflow.com/questions/2398746/removing-window-border
                //LONG lStyle = GetWindowLong(hwnd, GWL_STYLE);
                //lStyle &= ~(WS_CAPTION | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU);
                //SetWindowLong(hwnd, GWL_STYLE, lStyle);

                //var lExStyle = GetWindowLong(hwnd, GWL_EXSTYLE);lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);    
                //SetWindowLong(hwnd, GWL_EXSTYLE, lExStyle);

                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE)));



                Program.exitedprogram = 1;


                System.Windows.Forms.Application.ExitThread();
                System.Windows.Forms.Application.Exit();
                var process = System.Diagnostics.Process.GetCurrentProcess();


                process.Kill();
                process.Dispose();


                CloseWindow(sccsr14sc.Form1.someform.Handle);
                sccsr14sc.Form1.someform.Close();
                sccsr14sc.Form1.someform.Dispose();

                InputSimulator inputsim = new InputSimulator();

                //InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);
                inputsim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);

                inputsim = null;

                System.Environment.Exit(1);

                System.Environment.Exit(0);


            }



        }
        public void onbuttonmouseenter(object sender, EventArgs e)
        {
            /*
            Program.GetCursorPos(out somepoint);

            mousex = somepoint.X;
            mousey = somepoint.Y;
            */







            /*
            Program.EnumWindows(Program.enumtheWindowProcexit, IntPtr.Zero);

            uint lpdwProcessId;
            var pID = GetWindowThreadProcessId(GetForegroundWindow(),out lpdwProcessId);*/

            //SetFocus(pID);

            /*
            int iHandle =  scgraphicssec.FindWindow(null, Process.GetCurrentProcess().ProcessName); //(int)pID;/
            scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
            scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
            */

            //Console.WriteLine("pID:" + pID + "/pID:" + iHandle);



            /*
            int iHandle = scgraphicssec.FindWindow(null, this.Name);
            scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
            scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
            */




            /*
            Program.EnumWindows(Program.enumWindowProc, IntPtr.Zero);
            //SetFocus(theHandle);
            Console.WriteLine("onbuttonmouseenter");*/




            //this.SetTopLevel(true);
            /*this.Size = new System.Drawing.Size(1920, 1080);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Normal;
            this.TopMost = true;


            var windowHandler = this.Handle;// GetActiveWindowHandle();
            */
            //SetWindowPos(windowHandler, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SetWindowPosFlags.SWP_SHOWWINDOW);

            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TOPMOST | WS_EX_TRANSPARENT | WS_EX_LAYERED));


            //var windowRec = GetWindowRect(windowHandler);
            // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

            /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

            Program.RECT therect = new Program.RECT();
            therect.Left = 0;
            therect.Top = 0;
            therect.Bottom = this.Size.Height;
            therect.Right = this.Size.Width;
            param.ShowCmd = Program.ShowWindowCommands.ShowDefault ; //SW_SHOWNORMAL
            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
            Program.SetWindowPlacement(this.Handle, ref param);
            

            int screenWidth = Program.GetSystemMetrics(0);
            int screenHeight = Program.GetSystemMetrics(1);

            var windowHandler = this.Handle;
            SetWindowPos(windowHandler, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SetWindowPosFlags.SWP_SHOWWINDOW);
            */
            /*
            button2exit.Select();
            button2exit.Focus();
            button2exit.Update();

            //sccsr14sc.Form1.someform.Size = new System.Drawing.Size(1920, 1080);
            //sccsr14sc.Form1.someform.FormBorderStyle = FormBorderStyle.None;
            //sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
            //sccsr14sc.Form1.someform.TopMost = true;
            sccsr14sc.Form1.someform.Focus();
            sccsr14sc.Form1.someform.Select();
            sccsr14sc.Form1.someform.Show();*/








            //var style = this.GetStyle();
            //someform.FormBorderStyle.

            //button2exit.PerformClick();

            if (Program.exitedprogram == 1)
            {




                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));






                int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);









                Program.RECT therect = new Program.RECT();
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


                /*Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                string altcapturedwindowname = capturedwindownameform1.ToLower();

                if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                {
                    var altcapturedwindownameindex = altcapturedwindowname.IndexOf("- microsoft? edge");

                    //var altcapturedwindownamediff = 

                    Console.WriteLine("altcapturedwindownameindex:" + altcapturedwindownameindex);
                    //var altaltcapturedwindowname = altcapturedwindowname.Substring(altcapturedwindownameindex - 1, 17);

                    if (altcapturedwindowname.Contains(" - microsoft? edge")) //altaltcapturedwindowname == "microsoft? edge")//altcapturedwindowname.Contains("microsoft? edge"))
                    {
                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                    }
                    else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                    {

                    }
                    else
                    {

                    }
                }
                */



                string altcapturedwindowname = capturedwindownameform1.ToLower();

                if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                {





                    var altcapturedwindownameindex = altcapturedwindowname.IndexOf("- microsoft? edge");

                    //var altcapturedwindownamediff = 

                    Console.WriteLine("altcapturedwindownameindex:" + altcapturedwindownameindex);
                    //var altaltcapturedwindowname = altcapturedwindowname.Substring(altcapturedwindownameindex - 1, 17);

                    if (altcapturedwindowname.Contains("- microsoft") && altcapturedwindowname.Contains("edge")) //altaltcapturedwindowname == "microsoft? edge")//altcapturedwindowname.Contains("microsoft? edge"))
                    {

                        /*therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 800;
                        therect.Right = 600;

                        param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                        */

                        /*Program.RECT Rectsccs = new Program.RECT();
                        Program.GetWindowRect(Program.vewindowsfoundedz, ref Rectsccs);



                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 600;
                        therect.Right = 800;

                        param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 600;
                        therect.Right = 800;

                        param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


                        int iHandle = sccsr14sc.Form1.FindWindow(null, capturedwindownameform1);// "VoidExpanse");

                        Console.WriteLine(iHandle);

                        Console.WriteLine(capturedwindownameform1);

                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW
                        */
                        /*sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        //WORKING
                        sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        //WORKING*/


                        //iHandle = scgraphicssec.FindWindow(null, capturedwindownameform1);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                        *//*
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        */
                        /*Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/


                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);




                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 600;
                        therect.Right = 800;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                        //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);



                        /*
                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 600;
                        therect.Right = 800;

                        param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW


                        */





                        /*
                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 600;
                        therect.Right = 800;

                        param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);
                        */


                        /*var sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                            //Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, Program.SWP_SHOWWINDOW);
                        *///MIT-LICENSE-RichardBrass-BorderlessFullscreen
                          //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                          //MIT-LICENSE-RichardBrass-BorderlessFullscreen



                        /*var sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW | Program.WindowStyles.WS_BORDER | Program.WindowStyles.WS_MINIMIZEBOX | Program.WindowStyles.WS_MAXIMIZEBOX | Program.WindowStyles.WS_HSCROLL));//| (UIntPtr)Program.WindowStyles.WS_MAXIMIZEBOX | (UIntPtr)Program.WindowStyles.WS_MINIMIZEBOX);
                        //var sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_OVERLAPPED));//| (UIntPtr)Program.WindowStyles.WS_MAXIMIZEBOX | (UIntPtr)Program.WindowStyles.WS_MINIMIZEBOX);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                            //Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_OVERLAPPED));//| (UIntPtr)Program.WindowStyles.WS_MAXIMIZEBOX | (UIntPtr)Program.WindowStyles.WS_MINIMIZEBOX);
                        //var sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_OVERLAPPED));//| (UIntPtr)Program.WindowStyles.WS_MAXIMIZEBOX | (UIntPtr)Program.WindowStyles.WS_MINIMIZEBOX);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                            //Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        */



                        /*sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_MAXIMIZEBOX);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                            //Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }

                        sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_MINIMIZEBOX);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                            //Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }
                        */

                        //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, Program.SWP_SHOWWINDOW);


                        //SetWindowRgn(Program.vewindowsfoundedz, CreateRoundRectRgn(0, 0, 800, 600, 20, 20), true);

                        //SetWindowRgn(Program.vewindowsfoundedz, CreateRoundRectRgn(0, 0, 800, 600, 20, 20), true);

                        //SetWindowRgn(Program.vewindowsfoundedz, CreateRectRgn(0, 0, 800, 600),true);

                    }

                }
                else if (altcapturedwindowname.Contains("firefox"))
                {
                    var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                    if (RunningProcessPaths.Contains("firefox.exe"))
                    {
                        //firefox is running
                        Console.WriteLine("firefox is running");
                    }

                    /*if (RunningProcessPaths.Contains("chrome.exe"))
                    {
                        //Google Chrome is running
                        Console.WriteLine("chrome is running");
                    }*/

                }
                else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program"))
                {

                    if (altcapturedwindowname.Contains("gnu image manipulation program"))
                    {
                        Console.WriteLine(altcapturedwindowname);


                    }


                }
                else
                {


                }




                /*this.TopMost = true;
                this.Focus();
                this.BringToFront();
                this.Activate();*/
                //SetFocus(theHandle);
                button1exit.PerformClick();
                
                
                
                /*Program.GetCursorPos(out somepoint);

                mousex = somepoint.X;
                mousey = somepoint.Y;
                var iHandlee = scgraphicssec.FindWindow(null, this.Name);
                scgraphicssec.SendMessage(iHandlee, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                scgraphicssec.SendMessage(iHandlee, scgraphicssec.WM_RBUTTONUP, 0, 0);
                

                Console.WriteLine("handle:" + iHandlee);*/

                /*button1exit.Select();
                button1exit.Focus();
                button1exit.Update();*/
                button1exit.PerformClick();
                button1exit.Update();

                Console.WriteLine("onbuttonmouseclick");
                //button2exit.BeginInvoke();
                //button2exit.Invoke();
                //button2exit.




                //https://stackoverflow.com/questions/2398746/removing-window-border
                //LONG lStyle = GetWindowLong(hwnd, GWL_STYLE);
                //lStyle &= ~(WS_CAPTION | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU);
                //SetWindowLong(hwnd, GWL_STYLE, lStyle);

                //var lExStyle = GetWindowLong(hwnd, GWL_EXSTYLE);lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);    
                //SetWindowLong(hwnd, GWL_EXSTYLE, lExStyle);

                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE)));



                Program.exitedprogram = 1;


                System.Windows.Forms.Application.ExitThread();
                System.Windows.Forms.Application.Exit();
                var process = System.Diagnostics.Process.GetCurrentProcess();


                process.Kill();
                process.Dispose();


                CloseWindow(sccsr14sc.Form1.someform.Handle);
                sccsr14sc.Form1.someform.Close();
                sccsr14sc.Form1.someform.Dispose();

                InputSimulator inputsim = new InputSimulator();

                //InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);
                inputsim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);

                inputsim = null;

                System.Environment.Exit(1);

                System.Environment.Exit(0);




            }
        }
        [return: MarshalAs(UnmanagedType.Bool)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CloseWindowStation(IntPtr hWinsta);

        [DllImport("user32.dll")]
        static extern bool CloseWindow(IntPtr hWnd);



        public static IntPtr theHandle;
        //scupdate updatescript;




        private void Form1_Load(object sender, EventArgs e)
        {



            theHandle = this.Handle;
            initForm = 1;

            //this.Name = "sccs";

            /*
            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

            SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
            //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


            //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
            Program.RECT therect = new Program.RECT();
            therect.Left = 0;
            therect.Top = 0;
            therect.Bottom = 1080;
            therect.Right = 1920;

            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
            Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

            Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            */

            /*
            this.Size = new System.Drawing.Size(1920, 1080);
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
            this.TopMost = true;*/


            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

            Program.RECT therect = new Program.RECT();
            therect.Left = 0;
            therect.Top = 0;
            therect.Bottom = 600;
            therect.Right = 800;

            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
            Program.SetWindowPlacement(this.Handle, ref param);

            Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);









            /*
            const int GWL_STYLE = -16; //want to change the window style
            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
            IntPtr sult = Program.SetWindowLongPtr(this.Handle, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                //return;
                Console.WriteLine("Unable to alter window style.\nSorry.");
            }

            Program.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 800, 600, Program.SWP_SHOWWINDOW);
            */



            /*
            const int GWL_STYLE = -16; //want to change the window style
            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
            IntPtr sult = Program.SetWindowLongPtr(this.Handle, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                //return;
                Console.WriteLine("Unable to alter window style.\nSorry.");
            }

            Program.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 800, 600, Program.SWP_SHOWWINDOW);*/





            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));


            //sccs.Program.MessageBox((IntPtr)0, "Form1_Load0", "scmsg", 0);

        }

        [DllImport("user32.dll")]
        static extern bool SetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);

        //public const int GWL_EXSTYLE = -20;
        public const int WS_EX_LAYERED = 0x00080000; //0x80000
        //public const int WS_EX_TRANSPARENT = 0x20;

        public const int LWA_ALPHA = 0x2;
        public const int LWA_COLORKEY = 0x1;
        //public const long WS_EX_TOPMOST = 0x00000008L;
        public const long WS_NOACTIVATE = 0x08000000L;
        const long WS_EX_WINDOWEDGE = 0x00000100L;
        public const long WS_EX_TRANSPARENT = 0x00000020L;
        public const long WS_EX_CLIENTEDGE = 0x00000200L;
        const long WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE;


        public const long WS_DLGFRAME = 0x00400000L;

        public const long WS_BORDER = 0x00800000L;
        public const long WS_SIZEBOX = 0x00040000L;
        public const int SWP_FRAMECHANGED = 0x0020;


        public static uint testGetWindowThreadProcessId;

        /*[DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        */
        public static int usesharpdxscreencapture = 0;

        [DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, bottom, right;
        }
        [DllImport("user32.dll")]
        static extern void DwmExtendFrameIntoClientArea(IntPtr hwnd, ref Margins pMargins);
        //https://www.unknowncheats.me/forum/c/62019-c-non-hooked-external-directx-overlay.html

        private static Margins marg;

        internal struct Margins
        {
            public int Left, Right, Top, Bottom;
        }

        /*[DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
        */
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern long GetWindowLongPtr(IntPtr hWnd, int nIndex);


        //public static sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS;
        static int MaxSizeMainObject = 16;
        public static int MaxSizeMessageObject = 32;
        static scmessageobject[] mainreceivedmessages;//
        static scmessageobjectjitter[][] sccsjittertasks = null;
        //static jitter_sc[] jitter_sc;

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetConsoleWindow();

        private void Form1_Shown(Object sender, EventArgs e)
        {
            theHandle = this.Handle;
            //System.Windows.Forms.Cursor.Hide();
            //string window = "VoidExpanse"; //VoidExpanse

            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
            //SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);

            //var handle = GetConsoleWindow();

            /*var _hwndSource = HwndSource.FromHwnd(this.Handle);
            if (_hwndSource != null)
                _hwndSource.AddHook(WndProc);
            */



            if (Program.vewindowsfoundedz!= IntPtr.Zero)
            {
                sccs.Program.MessageBox((IntPtr)0, "_hwndSource", "scmsg", 0);
                
                
                /*_hwndSource = HwndSource.FromHwnd(this.Handle);
                if (_hwndSource != null)
                    _hwndSource.AddHook(WndProc);*/
            }









            /*SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                SharpDX.RawInput.DeviceFlags.None, this.Handle);*/
            /*SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                SharpDX.RawInput.DeviceFlags.InputSink, this.Handle);
            //SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;*/

            //Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse, DeviceFlags.None);
            //Device.MouseInput += (sender, args) => textBox.Invoke(new UpdateTextCallback(UpdateMouseText), args);

            //Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, DeviceFlags.None);
            //Device.KeyboardInput += (sender, args) => textBox.Invoke(new UpdateTextCallback(UpdateKeyboardText), args);

            //SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);
            //ShowCursor(false);
            /*var _hwndSource = HwndSource.FromHwnd(this.Handle);// this.Handle); //this.Handle
            if (_hwndSource != null)
                _hwndSource.AddHook(WndProc);

            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                SharpDX.RawInput.DeviceFlags.InputSink, this.Handle);
            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                SharpDX.RawInput.DeviceFlags.InputSink, this.Handle);
            SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
            //SharpDX.RawInput.Device.KeyboardInput += new EventHandler<KeyboardInputEventArgs>(UpdateKeyboardText);
            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;
            */


            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST));

            //SetWindowRgn(this.Handle, CreateRoundRectRgn(0, 0, 1920, 1080, 20, 20), false);
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST));





            //| WS_EX_OVERLAPPEDWINDOW | WS_EX_TRANSPARENT
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TOPMOST | WS_NOACTIVATE  | WS_EX_LAYERED));
            //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
            //SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);                                                                                                                                                           //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TOPMOST | WS_NOACTIVATE | WS_EX_TRANSPARENT | WS_EX_LAYERED)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TOPMOST | WS_NOACTIVATE | WS_EX_OVERLAPPEDWINDOW | WS_EX_TRANSPARENT)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW

            //System.Windows.Input.Mouse.OverrideCursor = System.Windows.Input.Cursors.None;

            //SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);
            //ShowCursor(false);
            /*_hwndSource = HwndSource.FromHwnd(this.Handle);
            if (_hwndSource != null)
                _hwndSource.AddHook(WndProc);

            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                SharpDX.RawInput.DeviceFlags.InputSink, this.Handle);
            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                SharpDX.RawInput.DeviceFlags.InputSink, this.Handle);

            SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;*/

            /*
            someform.Size = new System.Drawing.Size(1920, 1080);
            someform.FormBorderStyle = FormBorderStyle.None;
            someform.WindowState = FormWindowState.Maximized;*/


            //this.WindowState = FormWindowState.Minimized;

            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
            //SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);

            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_OVERLAPPEDWINDOW | WS_EX_TRANSPARENT)); 
            //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW



            //SetWindowPos(this.Handle, SetWindowPosFlags.HWND_TOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);



            /*Thread _mainThread = new Thread((tester0000) =>
            {
            //sccs.Program.MessageBox((IntPtr)0, "Form1", "scmsg", 0);

            _thread_main_loop:


                if (haspressedf9 == 1)
                {
                    if (thepanel.Visible == false)
                    {
                        sccs.Program.MessageBox((IntPtr)0, "thepanel Visible", "scmsg", 0);
                        thepanel.Visible = true;
                    }
                    haspressedf9 = 0;
                }

                //sccs.Program.MessageBox((IntPtr)0, "thread initiated", "scmsg", 0);
                Thread.Sleep(1);
                //MessageBox((IntPtr)0, "threadm0", "scmsg", 0);
                goto _thread_main_loop;
                //MessageBox((IntPtr)0, "threadm -1", "scmsg", 0);
                //_thread_start:
            }, 0); //100000 //999999999

            _mainThread.IsBackground = true;
            _mainThread.Priority = ThreadPriority.Lowest; //AboveNormal
            _mainThread.SetApartmentState(ApartmentState.STA);
            _mainThread.Start();*/



            /*
        _thread_main_loop:

            if (haspressedf9 == 1)
            {
                if (thepanel.Visible == false)
                {
                    sccs.Program.MessageBox((IntPtr)0, "thepanel Visible", "scmsg", 0);
                    thepanel.Visible = true;
                }
                haspressedf9 = 0;
            }

            //sccs.Program.MessageBox((IntPtr)0, "thread initiated", "scmsg", 0);
            Thread.Sleep(1);
            goto _thread_main_loop;*/


            /*
        _thread_main_loop:


            if (haspressedf9 == 1)
            {
                if (thepanel.Visible == false)
                {
                    sccs.Program.MessageBox((IntPtr)0, "thepanel Visible", "scmsg", 0);
                    thepanel.Visible = true;
                }
                haspressedf9 = 0;
            }

            //sccs.Program.MessageBox((IntPtr)0, "thread initiated", "scmsg", 0);
            Thread.Sleep(1);
            //MessageBox((IntPtr)0, "threadm0", "scmsg", 0);
            goto _thread_main_loop;*/


            //Dispatcher thedispatcher = System.Windows.Threading.Dispatcher;




            // Create an AutoResetEvent to signal the timeout threshold in the
            // timer callback has been reached.0
            /*var autoEvent = new AutoResetEvent(false);
             AutoResetEvent0 = new AutoResetEvent(false);
             AutoResetEvent1 = new AutoResetEvent(false);

            currentWaitHandle = AutoResetEvent0;

            var statusChecker0 = new StatusChecker0(10, AutoResetEvent0, 0);
            var statusChecker1 = new StatusChecker0(10, AutoResetEvent1, 1);
            // Create a timer that invokes CheckStatus after one second, 
            // and every 1/4 second thereafter.
            //Console.WriteLine("{0:h:mm:ss.fff} Creating timer.\n", DateTime.Now);

            AutoResetEvent0.Set();
            var stateTimer0 = new System.Threading.Timer(statusChecker0.CheckStatus, AutoResetEvent0, 0, 1);
            //autoEvent.WaitOne();

            AutoResetEvent1.Set();
            var stateTimer1 = new System.Threading.Timer(statusChecker1.CheckStatus, AutoResetEvent1, 0, 1);
            *///autoEvent.WaitOne();
            /*//autoEvent.WaitOne();
            //Console.WriteLine("{0:h:mm:ss.fff} setting timer.\n", DateTime.Now);

            //Console.WriteLine("{0:h:mm:ss.fff} Creating timer.\n", DateTime.Now);

            var statusChecker1 = new StatusChecker1(10, AutoResetEvent1, 1);
            var stateTimer1 = new System.Threading.Timer(statusChecker1.CheckStatus, AutoResetEvent1, 0, 1);
            //autoEvent.WaitOne();
            //Console.WriteLine("{0:h:mm:ss.fff} setting timer.\n", DateTime.Now);
            */


            /*
            System.Windows.Forms.Timer sometimer = new System.Windows.Forms.Timer();
            sometimer.Interval = 1;
            sometimer.Tick += new System.EventHandler(mainthreadloop);
            sometimer.Start();
            */



            //stateTimer.Change(0, 1);
            // When autoEvent signals, change the period to every half second.
            /*autoEvent.WaitOne();
            stateTimer.Change(0, 500);
            Console.WriteLine("\nChanging period to .5 seconds.\n");*/

            /*// When autoEvent signals the second time, dispose of the timer.
            autoEvent.WaitOne();
            stateTimer.Dispose();
            Console.WriteLine("\nDestroying timer.");*/

            //https://stackoverflow.com/questions/2398746/removing-window-border
            //LONG lStyle = GetWindowLong(hwnd, GWL_STYLE);
            //lStyle &= ~(WS_CAPTION | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU);
            //SetWindowLong(hwnd, GWL_STYLE, lStyle);

            //var lExStyle = GetWindowLong(hwnd, GWL_EXSTYLE);lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);    
            //SetWindowLong(hwnd, GWL_EXSTYLE, lExStyle);

            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW

            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Handle, GWL_EXSTYLE) | WS_BORDER | WS_SIZEBOX | WS_DLGFRAME));

            //SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE
            //SetWindowPos(this.Handle, (IntPtr)WS_EX_TOPMOST, Left, Top, Width, Height, SetWindowPosFlags.SWP_FRAMECHANGED);






            //createinputs(this.Handle);
            form1hasshown = 1;
        }
        static int form1hasshown = 0;

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





        private void KeyboardKeyDownAlt(object sender, KeyEventArgs e)
        {

            if (e.Modifiers == Keys.Alt) // && (e.Alt || e.Tab || e.Shift)
            {
                //sccs.Program.MessageBox((IntPtr)0, "key down alt:", "scmsg", 0);
                //Console.WriteLine("alt ");
                altkeyhandle = 1;
            }
        }




            keyboardinput keynmouseinput = new keyboardinput();
        public int altkeyhandle = 0;
        public int tabkeyhandle = 0;
        private void KeyboardKeyDownTab(object sender, KeyEventArgs e)
        {
            //keynmouseinput.ReadKeyboard();
            //tabkeyhandle = 0;
            var somekeytest = e.KeyCode;
            //Console.WriteLine(somekeytest);
            //sccs.Program.MessageBox((IntPtr)0, "key:" + somekeytest, "scmsg", 0);

            
            /*if (e.KeyCode == Keys.Menu) // && (e.Alt || e.Tab || e.Shift)
            {
                //sccs.Program.MessageBox((IntPtr)0, "key down ALT:" + somekeytest, "scmsg", 0);
                //tabkeyhandle = 1;
            }*/


            if (e.KeyCode == Keys.Tab) // && (e.Alt || e.Tab || e.Shift)
            {
                Console.WriteLine(somekeytest);
                //Program.MessageBox((IntPtr)0, "TAB", "sc core systems message", 0);
                /*if (altkeyhandle == 1)
                {
                    
                }*/
                tabkeyhandle = 1;
            }

            /*if (keynmouseinput._KeyboardState != null && keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Tab))
            {
                //Program.MessageBox((IntPtr)0, "TAB", "sc core systems message", 0);
                //Console.WriteLine("tab");
                //alttabswtc1 = 1;
           
                tabkeyhandle = 1;
            }*/


           




            /*if (e.Modifiers == Keys.Tab) // && (e.Alt || e.Shift)
            {
                sccs.Program.MessageBox((IntPtr)0, "key down0:" + somekeytest, "scmsg", 0);
            }

            if (e.KeyCode == Keys.Tab) // && (e.Alt || e.Tab || e.Shift)
            {
                sccs.Program.MessageBox((IntPtr)0, "key down1:" + somekeytest, "scmsg", 0);
                //tabkeyhandle = 1;
            }
            */

            /*if (e.KeyCode == Keys.Tab) // && (e.Alt || e.Tab || e.Shift)
            {
                //sccs.Program.MessageBox((IntPtr)0, "key down:" + somekeytest, "scmsg", 0);
                tabkeyhandle = 1;
            }
            if (altkeyhandle == 1)
            {
                //sccs.Program.MessageBox((IntPtr)0, "key0:" + somekeytest, "scmsg", 0);
            }
            if (altkeyhandle == 1 && tabkeyhandle == 1)
            {
                sccs.Program.MessageBox((IntPtr)0, "key1:" + somekeytest, "scmsg", 0);
            }*/
        }





        private const double OPACITY_MIN = 0.5;
        private const double OPACITY_MAX = 1.0;
        private const double OPACITY_STEP = 0.05;// Step for changing opacity value
        private const int TIMER_INTERVAL = 10;//100 // Interval in miliseconds (1/1000 of second)

        private System.Windows.Forms.Timer _timer;                       // Timer control
        private bool _fading = false;               // Opacity mode: fading or showing


        //https://www.codeproject.com/Questions/852535/Csharp-How-To-Tell-If-A-WinForm-Has-Focus-Or-Not
        // This method is executed every TIMER_INTERVAL miliseconds is Timer is enabled
        private void _timer_Tick(object sender, EventArgs e)
        {
            // Decrease or increase value of Form's opacity property by OPACITY_STEP in every timer Tick
            // and stop the timer if MIN or MAX opacity value was reached
            /*if (_fading)
            {
                if (this.Opacity > OPACITY_MIN)
                {
                    this.Opacity -= OPACITY_STEP;
                }
                else
                {
                    this.Opacity = OPACITY_MIN;
                    _timer.Enabled = false;
                }
            }
            else
            {
                if (this.Opacity < OPACITY_MAX)
                {
                    this.Opacity += OPACITY_STEP;
                }
                else
                {
                    this.Opacity = OPACITY_MAX;
                    _timer.Enabled = false;
                }
            }*/
        }



        //https://www.codeproject.com/Questions/852535/Csharp-How-To-Tell-If-A-WinForm-Has-Focus-Or-Not
        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            // When mouse is leaving form we are setting mode to Fading and enabling timer

            //_fading = true;
            _timer.Enabled = true;

            //Console.WriteLine("form1mouseleave");
            //this.TopMost = false;
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_BORDER));
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

        }


        //https://www.codeproject.com/Questions/852535/Csharp-How-To-Tell-If-A-WinForm-Has-Focus-Or-Not
        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            // When mouse is entering form we are setting mode to Showing and enabling timer
            //this.Opacity = OPACITY_MAX;

            //_fading = false;
            _timer.Enabled = false;

            //this.AllowTransparency = true;
            //this.TopMost = true;

            //Console.WriteLine("form1mouseenter");
            //this.TopMost = true;
            //this.Size = new System.Drawing.Size(1920, 1080);
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));






        }






        private void KeyboardKeyUp(object sender, KeyEventArgs e)
        {
            
            var somekeytest = e.KeyCode;

            //sccs.Program.MessageBox((IntPtr)0, "key up:" + somekeytest, "scmsg", 0);
            if (e.KeyCode == Keys.Tab) // && (e.Alt || e.Tab || e.Shift)
            {
                //sccs.Program.MessageBox((IntPtr)0, "key up:" + somekeytest, "scmsg", 0);
                Console.WriteLine("Tab up");
                tabkeyhandle = 0;
            }
            /*if (e.KeyCode == Keys.Alt) // && (e.Alt || e.Tab || e.Shift)
            {
                Console.WriteLine("Alt up0");
                //sccs.Program.MessageBox((IntPtr)0, "key up:" + somekeytest, "scmsg", 0);
                altkeyhandle = 0;
            }
            if (e.Modifiers == Keys.Alt) // && (e.Alt || e.Tab || e.Shift)
            {
                Console.WriteLine("Alt up1");
                //sccs.Program.MessageBox((IntPtr)0, "key up:" + somekeytest, "scmsg", 0);
                altkeyhandle = 0;
            }
            if (e.Modifiers == Keys.Menu) // && (e.Alt || e.Tab || e.Shift)
            {
                Console.WriteLine("Alt up2");
                //sccs.Program.MessageBox((IntPtr)0, "key up:" + somekeytest, "scmsg", 0);
                altkeyhandle = 0;
            }*/
            if (e.KeyCode == Keys.Menu) // && (e.Alt || e.Tab || e.Shift)
            {
                Console.WriteLine("Alt up3");
                //sccs.Program.MessageBox((IntPtr)0, "key up:" + somekeytest, "scmsg", 0);
                altkeyhandle = 0;
            }










            //tabkeyhandle = 0;
            //altkeyhandle = 0;
            //var somekeytest = e.KeyCode;
            //Console.WriteLine(somekeytest);
            //sccs.Program.MessageBox((IntPtr)0, "key:" + somekeytest, "scmsg", 0);
        }

        private void KeyboardKeyPress(object sender, KeyPressEventArgs e)
        {
           //sccs.Program.MessageBox((IntPtr)0, "key:" + e.KeyChar + " ", "scmsg", 0);

            e.Handled = true;

            //var somekeytest = e.KeyCode;
            //Console.WriteLine(somekeytest);
            /*if (altkeyhandle == 1 && tabkeyhandle == 1)
            {
                sccs.Program.MessageBox((IntPtr)0, "alt tab1", "scmsg", 0);
            }*/
        }












        private void Form1_Load_1(object sender, EventArgs e)
        {
            initForm = 1;
            //sccs.Program.MessageBox((IntPtr)0, "Form1_Load1", "scmsg", 0);
            //System.Windows.Forms.Cursor.Hide();
            //this.Resize += Form1_Resize;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public event EventHandler Resize;


        int haspressedminioptionsbar = -1;
        int haspressedminioptionsbarswtc = -1;



        public static int wasf11pressedonfirefox = 0;
        public static int wasf11pressedbynativeonedge = 0;


        public void hooker(IntPtr thecapturedwindow)
        {

            var refreshDXEngineAction = new Action(delegate
            {
                var _hwndSource = HwndSource.FromHwnd(thecapturedwindow);

                if (_hwndSource != null)
                {
                    _hwndSource.AddHook(WndProc);
                    MessageBox((IntPtr)0, "0hooked on captured window, window events", "sccsmsg", 0);
                }
            });


            Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);



            //MessageBox((IntPtr)0, "0hooked on captured window, window events", "sccsmsg", 0);
        }




        //https://stackoverflow.com/questions/1295999/event-when-a-window-gets-maximized-un-maximized
        //This should handle the event on any window. SC_RESTORE is 0xF120, and SC_MINIMIZE is 0XF020, if you need those constants, too.
        protected override void WndProc(ref Message m)
        {


            //Console.WriteLine("WndProc" + m.Msg);


            if (m.HWnd == Program.vewindowsfoundedz)
            {
                MessageBox((IntPtr)0, "1 WndProc hooked on captured window, window events", "sccsmsg", 0);
            }


            if (m.Msg == 0x0112) // WM_SYSCOMMAND
            {
                // Check your window state here
                if (m.WParam == new IntPtr(0xF030)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    /*if (checkBox3_CheckedChangedint == 0)
                    {

                        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                        //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                        //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                        Program.RECT therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 1080;
                        therect.Right = 1920;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                        this.Size = new System.Drawing.Size(1920, 1080);
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.WindowState = FormWindowState.Maximized;
                        this.TopMost = true;

                        Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));






                        //checkBox3_CheckedChangedint = 1;
                    }
                    else if (checkBox3_CheckedChangedint == 1)
                    {
                        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                        //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                        /*const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            Console.WriteLine("Unable to alter window style.\nSorry.");
                        }

                        Program.SetWindowPos(Program.vewindowsfoundedz, IntPtr.Zero, 0, 0, 1920, 1080, Program.SWP_SHOWWINDOW);
                        

                        //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                        Program.RECT therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 1080;
                        therect.Right = 1920;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                        this.Size = new System.Drawing.Size(1920, 1080);
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.WindowState = FormWindowState.Maximized;
                        this.TopMost = true;
                        Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                        SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));


                        //checkBox3_CheckedChangedint = 0;
                    }*/





                    /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));

                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 1080;
                    therect.Right = 1920;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;
                    Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                    */

                    /*var refreshDXEngineAction0 = new Action(delegate
                    {
                        //Console.WriteLine("thebutton Visible");
                        //stackoverflow 661561 for invoking panel changes.

                        //sccsr14sc.Form1.haspressedf9 = 1;

                        sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                        {
                            if (sccsr14sc.Form1.checkbox1.Checked)
                            {
                                sccsr14sc.Form1.checkbox1.Checked = false;
                            }
                            else if (!sccsr14sc.Form1.checkbox1.Checked)
                            {
                                sccsr14sc.Form1.checkbox1.Checked = true;
                            }
                        });
                    });
                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                    */


                    /*sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                    {
                        if (sccsr14sc.Form1.checkbox1.Checked)
                        {
                            sccsr14sc.Form1.checkbox1.Checked = false;
                        }
                        else if (!sccsr14sc.Form1.checkbox1.Checked)
                        {
                            sccsr14sc.Form1.checkbox1.Checked = true;
                        }
                    });
                    */

                  


                    //checkBox1_CheckedChangedint = 0;
                    Console.WriteLine("maximized");
                    haspressedminioptionsbar = 0;
                    // THe window is being maximized
                }
                else if (m.WParam == new IntPtr(0XF020)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 0;
                    therect.Right = 0;

                    param.ShowCmd = Program.ShowWindowCommands.Minimize; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                    this.Size = new System.Drawing.Size(0, 0);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Minimized;
                    this.TopMost = true;
                    //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    
                    */


                    //checkBox1_CheckedChangedint = 0;
                    haspressedminioptionsbar = 1;
                    Console.WriteLine("minimized");
                    // THe window is being minimized
                }
                else if (m.WParam == new IntPtr(0xF120)) // Maximize event - SC_MAXIMIZE from Winuser.h
                {
                    /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 600;
                    therect.Right = 800;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);





                    this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;


                    Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    */

                    /*var refreshDXEngineAction0 = new Action(delegate
                    {
                        //Console.WriteLine("thebutton Visible");
                        //stackoverflow 661561 for invoking panel changes.

                        //sccsr14sc.Form1.haspressedf9 = 1;

                        sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                        {
                            if (sccsr14sc.Form1.checkbox1.Checked)
                            {
                                sccsr14sc.Form1.checkbox1.Checked = false;
                            }
                            else if (!sccsr14sc.Form1.checkbox1.Checked)
                            {
                                sccsr14sc.Form1.checkbox1.Checked = true;
                            }
                        });
                    });
                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction0);
                    */

                    /*
                    sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                    {
                        if (sccsr14sc.Form1.checkbox1.Checked)
                        {
                            sccsr14sc.Form1.checkbox1.Checked = false;
                        }
                        else if (!sccsr14sc.Form1.checkbox1.Checked)
                        {
                            sccsr14sc.Form1.checkbox1.Checked = true;
                        }
                    });*/

                    //checkBox1_CheckedChangedint = 1;
                    haspressedminioptionsbar = 2;
                    Console.WriteLine("restore");
                    // THe window is being minimized
                }
            }
          
            base.WndProc(ref m);
        }










        //https://stackoverflow.com/questions/1295999/event-when-a-window-gets-maximized-un-maximized
        private void Form1_Resize(object sender, System.EventArgs e)
        {
            /*//MessageBox((IntPtr)0, "Form1_Resize0", "sccsmsg", 0);
            Control control = (Control)sender;

            thepanel.Width = control.Width;
            thepanel.Height = control.Height;

            FormWindowState LastWindowState = FormWindowState.Minimized;
            // When window state changes
            if (WindowState != LastWindowState)
            {
                LastWindowState = WindowState;


                if (WindowState == FormWindowState.Maximized)
                {
                    Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    /*const int GWL_STYLE = -16; //want to change the window style
                    const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                    IntPtr sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                    if (sult == IntPtr.Zero)
                    {
                        //in some cases SWL just outright fails, so we can notify the user and abort
                        //MessageBox.Show("Unable to alter window style.\nSorry.");
                        //return;
                        Console.WriteLine("Unable to alter window style.\nSorry.");
                    }

                    Program.SetWindowPos(Program.vewindowsfoundedz, IntPtr.Zero, 0, 0, 1920, 1080, Program.SWP_SHOWWINDOW);
                    

                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 1080;
                    therect.Right = 1920;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;
                    Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));


                    // Maximized!
                }
                if (WindowState == FormWindowState.Normal)
                {
                    Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 600;
                    therect.Right = 800;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);





                    this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;


                    Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    // Restored!
                }
            }*/
            //Program.makeBorderless();
            //Program.makePanelBorderless();

            //MessageBox((IntPtr)0, "Form1_Resize1", "sccsmsg", 0);
        }







        public int mousex;
        public int mousey;
        private void Mouse_Move(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousex = e.X;
            mousey = e.Y;

        }

        public void deactivatecursor()
        {
            /*var refreshDXEngineAction = new Action(delegate
            {
                System.Windows.Forms.Cursor.Hide();
            });

            System.Windows.Threading.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);*/
            System.Windows.Forms.Cursor.Hide();
        }



        //public static RenderForm form;
        /// <summary>
        /// Updates the mouse text.
        /// </summary>
        /// <param name="rawArgs">The <see cref="SharpDX.RawInput.RawInputEventArgs"/> instance containing the event data.</param>
        void UpdateMouseText(object sender, MouseInputEventArgs rawArgs) // EventArgs e  //object sender,
        {
            //MessageBox((IntPtr)0, "test0", "scmsg", 0);
            var args = (MouseInputEventArgs)rawArgs;

            //textBox.AppendText(string.Format("(x,y):({0},{1}) Buttons: {2} State: {3} Wheel: {4}\r\n", args.X, args.Y, args.ButtonFlags, args.Mode, args.WheelDelta));
            Console.WriteLine(args.X + " " + args.Y + " " + args.ButtonFlags + " "  + args.Mode);

            //mrbuttondown = 0;
            //mlbuttondown = 0;

            if (args.ButtonFlags == mouseleftdownflag || args.ButtonFlags == mouserightdownflag)
            {
                if (args.ButtonFlags == mouseleftdownflag)
                {
                    mlbuttondown = 1;
                }


                if (args.ButtonFlags == mouserightdownflag)
                {
                    mrbuttondown = 1;
                }
                /*else
                {

                }*/
                //mlbuttondown = 1;
                //MessageBox((IntPtr)0, "mldown", "scmsg", 0);
            }
            else
            {


                if (args.ButtonFlags == mouseleftupflag)
                {
                    mlbuttondown = 0;
                }


                if (args.ButtonFlags == mouserightupflag)
                {
                    mrbuttondown = 0;
                }


                /*if (args.ButtonFlags != mouseleftdownflag && args.ButtonFlags != mouserightdownflag)
                {
                    if (args.ButtonFlags != mouseleftdownflag)
                    {
                        mlbuttondown = 0;
                    }
                    else if (args.ButtonFlags != mouserightdownflag)
                    {
                        mrbuttondown = 0;
                    }
                }*/
            }


            /*else
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
            }*/




            //Console.WriteLine(args.X + " " + args.Y + " " + args.ButtonFlags + " " + args.Mode + " " + mlbuttondown);
        }

        public static int mlbuttondown = 0;
        public static int mrbuttondown = 0;

        static MouseButtonFlags mouseleftdownflag = MouseButtonFlags.LeftButtonDown;
        static MouseButtonFlags mouserightdownflag = MouseButtonFlags.RightButtonDown;

        static MouseButtonFlags mouseleftupflag = MouseButtonFlags.LeftButtonUp;
        static MouseButtonFlags mouserightupflag = MouseButtonFlags.RightButtonUp;



        /// <summary>
        /// Updates the keyboard text.
        /// </summary>
        /// <param name="rawArgs">The <see cref="SharpDX.RawInput.RawInputEventArgs"/> instance containing the event data.</param>
        static void UpdateKeyboardText(object sender, RawInputEventArgs rawArgs) //object sender,

        //static void UpdateKeyboardText(object sender, KeyboardInputEventArgs rawArgs) //object sender,
        {
            //MessageBox((IntPtr)0, "test1", "scmsg", 0);
            var args = (KeyboardInputEventArgs)rawArgs;
            //textBox.AppendText(string.Format("Key: {0} State: {1} ScanCodeFlags: {2}\r\n", args.Key, args.State, args.ScanCodeFlags));

            Console.WriteLine(string.Format("Key: {0} State: {1} ScanCodeFlags: {2}\r\n", args.Key, args.State, args.ScanCodeFlags));

            if (args.Key == Keys.F9)
            {
                MessageBox((IntPtr)0, "F9", "scmsg", 0);
                haspressedf9 = 1;
            }

            if (args.Key == Keys.F11)
            {
                //MessageBox((IntPtr)0, "F11", "scmsg", 0);

                /*if (haspressedf11 == 0)
                {
                    haspressedf11 = 1;

                }
                else if (haspressedf11 == 1)
                {
                    haspressedf11 = 0;

                }*/

                if (IsWindowTopMost(Program.vewindowsfoundedz))
                {
                    if (wasf11pressedbynativeonedge == 1)
                    {
                        wasf11pressedbynativeonedge = 0;
                    }
                }


                /*if ( IsWindowTopMost(Program.vewindowsfoundedz))
                {
                    MessageBox((IntPtr)0, "F11", "scmsg", 0);

                }*/





            }
        }


        public static int haspressedf11 = 0;
        public static int haspressedescape = 0;
        public static int haspressedf9 = 0;
        public int haspressedsomekeyboardkey = 0;





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
                //MessageBox((IntPtr)0, "error WndProc" + msg + " " + lParam, "scmsg", 0);
            }
            //MessageBox((IntPtr)0, "error WndProc", "scmsg", 0);
            return IntPtr.Zero;
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {

        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

            if (trackbar != null)
            {
                trackbar.Focus();
                heightmapvalue = trackbar.Value;





            }
        }

        public int heightmapvalue;
        public int heightmapvaluemax;
        public int heightmapvaluemin;
        public int heightmapvaluetickfreq;

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_ItemCheckChanged(object sender, ItemCheckEventArgs e)
        {
            if (checkedlistbox != null)
            {
                checkedlistbox.Focus();
            }

            //e.Focus();
            //Console.WriteLine(e.Index);

            if (e.NewValue == CheckState.Checked)
            {
                if (e.Index == 0)
                {
                    hasgot0 = 1;
                    hasgotswtc0 = 1;
                }
                else if (e.Index == 1)
                {
                    hasgot1 = 1;
                    hasgotswtc1 = 1;
                }
                else if (e.Index == 2)
                {
                    hasgot2 = 1;
                    hasgotswtc2 = 1;
                }
                else if (e.Index == 3)
                {
                    hasgot3 = 1;
                    hasgotswtc3 = 1;
                }
                else if (e.Index == 4)
                {
                    hasgot4 = 1;
                    hasgotswtc4 = 1;
                }
                else if (e.Index == 5)
                {
                    hasgot5 = 1;
                    hasgotswtc5 = 1;
                }
                else if (e.Index == 6)
                {
                    hasgot6 = 1;
                    hasgotswtc6 = 1;
                }
                else if (e.Index == 7)
                {
                    hasgot7 = 1;
                    hasgotswtc7 = 1;
                }
                else if (e.Index == 8)
                {
                    hasgot8 = 1;
                    hasgotswtc8 = 1;
                }
                else if (e.Index == 9)
                {
                    hasgot9 = 1;
                    hasgotswtc9 = 1;
                }
                else if (e.Index == 10)
                {
                    hasgot10 = 1;
                    hasgotswtc10 = 1;
                }
                else if (e.Index == 11)
                {
                    hasgot11 = 1;
                    hasgotswtc11 = 1;
                }
            }
            else if (e.NewValue == CheckState.Unchecked)
            {
                if (e.Index == 0)
                {
                    hasgot0 = 0;
                    hasgotswtc0 = 1;
                }
                else if (e.Index == 1)
                {
                    hasgot1 = 0;
                    hasgotswtc1 = 1;
                }
                else if (e.Index == 2)
                {
                    hasgot2 = 0;
                    hasgotswtc2 = 1;
                }
                else if (e.Index == 3)
                {
                    hasgot3 = 0;
                    hasgotswtc3 = 1;
                }
                else if (e.Index == 4)
                {
                    hasgot4 = 0;
                    hasgotswtc4 = 1;
                }
                else if (e.Index == 5)
                {
                    hasgot5 = 0;
                    hasgotswtc5 = 1;
                }
                else if (e.Index == 6)
                {
                    hasgot6 = 0;
                    hasgotswtc6 = 1;
                }
                else if (e.Index == 7)
                {
                    hasgot7 = 0;
                    hasgotswtc7 = 1;
                }
                else if (e.Index == 8)
                {
                    hasgot8 = 0;
                    hasgotswtc8 = 1;
                }
                else if (e.Index == 9)
                {
                    hasgot9 = 0;
                    hasgotswtc9 = 1;
                }
                else if (e.Index == 10)
                {
                    hasgot10 = 0;
                    hasgotswtc10 = 1;
                }
                else if (e.Index == 11)
                {
                    hasgot11 = 0;
                    hasgotswtc11 = 1;
                }
            }
            if (labeltext2!= null)
            {
                labeltext2.Focus();
            }
            
        }

        public int hasgot0 = 1;
        public int hasgot1 = 0;
        public int hasgot2 = 0;
        public int hasgot3 = 0;
        public int hasgot4 = 0;
        public int hasgot5 = 0;
        public int hasgot6 = 0;
        public int hasgot7 = 0;
        public int hasgot8 = 0;
        public int hasgot9 = 0;
        public int hasgot10 = 0;
        public int hasgot11 = 0;

        public int hasgotswtc0 = 1;
        public int hasgotswtc1 = 0;
        public int hasgotswtc2 = 0;
        public int hasgotswtc3 = 0;
        public int hasgotswtc4 = 0;
        public int hasgotswtc5 = 0;
        public int hasgotswtc6 = 0;
        public int hasgotswtc7 = 0;
        public int hasgotswtc8 = 0;
        public int hasgotswtc9 = 0;
        public int hasgotswtc10 = 0;
        public int hasgotswtc11 = 0;



    


        Program.RECT lastRectsccs = new Program.RECT();
        Program.RECT Rectsccs = new Program.RECT();

        public int hasinitfullscreen = 0;

        public int checkBox1_CheckedChangedint = 0;


        public int hasclickedmouse = 0;


        Program.RECT Rectsccslast = new Program.RECT();
        private void form1mouseclicked(object sender, EventArgs e)
        {




            var Rectsccs = new Program.RECT();
            Program.GetWindowRect(this.Handle, ref Rectsccs);

            var Rectsccscaptured = new Program.RECT();
            Program.GetWindowRect(Program.vewindowsfoundedz, ref Rectsccscaptured);



            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

            Program.RECT therect = new Program.RECT();
            therect.Left = Rectsccs.Left;
            therect.Top = Rectsccs.Top;
            therect.Bottom = Rectsccs.Bottom;
            therect.Right = Rectsccs.Right;

            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
            Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

            /*int correctedw = Rectsccs.Right;
            int correctedh = Rectsccs.Bottom;

            if (Rectsccs.Right < 800 || Rectsccs.Bottom < 600)
            {
                if (Rectsccs.Right < 800)
                {
                    correctedw = 800;
                }
                if (Rectsccs.Bottom < 600)
                {
                    correctedh = 600;
                }
            }*/


            Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

            Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);






            /*int screenWidth = Program.GetSystemMetrics(0);
            int screenHeight = Program.GetSystemMetrics(1);
            //set the window to a borderless style
            const int GWL_STYLE = -16; //want to change the window style
            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border*/
            /*IntPtr sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                //return;
                Console.WriteLine("Unable to alter window style.\nSorry.");
            }*/
            /*
            Rectsccs = new Program.RECT();
            Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);
            */
            //Program.SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

            /*int correctedw = Rectsccs.Right;
            int correctedh = Rectsccs.Bottom;

            if (Rectsccs.Right < 800 || Rectsccs.Bottom < 600)
            {
                if (Rectsccs.Right < 800)
                {
                    correctedw = 800;
                }
                if (Rectsccs.Bottom < 600)
                {
                    correctedh = 600;
                }
            }


            Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, correctedw, correctedh, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            */


            //Rectsccslast = rec
            hasclickedmouse = 1;
        }




        private void form1mousedown(object sender, EventArgs e)
        {


            //if (hasclickedmouse == 1)
            {
                var Rectsccs = new Program.RECT();
                Program.GetWindowRect(this.Handle, ref Rectsccs);

                var Rectsccscaptured = new Program.RECT();
                Program.GetWindowRect(Program.vewindowsfoundedz, ref Rectsccscaptured);



                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                Program.RECT therect = new Program.RECT();
                therect.Left = Rectsccs.Left;
                therect.Top = Rectsccs.Top;
                therect.Bottom = Rectsccs.Bottom;
                therect.Right = Rectsccs.Right;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                /*int correctedw = Rectsccs.Right;
                int correctedh = Rectsccs.Bottom;

                if (Rectsccs.Right < 800 || Rectsccs.Bottom < 600)
                {
                    if (Rectsccs.Right < 800)
                    {
                        correctedw = 800;
                    }
                    if (Rectsccs.Bottom < 600)
                    {
                        correctedh = 600;
                    }
                }*/


                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //hasclickedmouse = 0;
            }




            /*if (hasclickedmouse == 0)
            {
                var Rectsccs = new Program.RECT();
                Program.GetWindowRect(this.Handle, ref Rectsccs);

                var Rectsccscaptured = new Program.RECT();
                Program.GetWindowRect(Program.vewindowsfoundedz, ref Rectsccscaptured);



                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                Program.RECT therect = new Program.RECT();
                therect.Left = Rectsccs.Left;
                therect.Top = Rectsccs.Top;
                therect.Bottom = Rectsccs.Bottom;
                therect.Right = Rectsccs.Right;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


                int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);
                //set the window to a borderless style
                const int GWL_STYLE = -16; //want to change the window style
                const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                /*IntPtr sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
                if (sult == IntPtr.Zero)
                {
                    //in some cases SWL just outright fails, so we can notify the user and abort
                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                    //return;
                    Console.WriteLine("Unable to alter window style.\nSorry.");
                }

                Rectsccs = new Program.RECT();
                Program.GetWindowRect(sccsr14sc.Form1.theHandle, ref Rectsccs);

                //Program.SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                /*int correctedw = Rectsccs.Right;
                int correctedh = Rectsccs.Bottom;

                if (Rectsccs.Right < 800 || Rectsccs.Bottom < 600)
                {
                    if (Rectsccs.Right < 800)
                    {
                        correctedw = 800;
                    }
                    if (Rectsccs.Bottom < 600)
                    {
                        correctedh = 600;
                    }
                }


                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, correctedw, correctedh, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                hasclickedmouse = 1;
            }*/

        }






        private void form1mouseup(object sender, EventArgs e)
        {


            //if(hasclickedmouse == 1)
            {
                var Rectsccs = new Program.RECT();
                Program.GetWindowRect(this.Handle, ref Rectsccs);

                var Rectsccscaptured = new Program.RECT();
                Program.GetWindowRect(Program.vewindowsfoundedz, ref Rectsccscaptured);



                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                Program.RECT therect = new Program.RECT();
                therect.Left = Rectsccs.Left;
                therect.Top = Rectsccs.Top;
                therect.Bottom = Rectsccs.Bottom;
                therect.Right = Rectsccs.Right;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                /*int correctedw = Rectsccs.Right;
                int correctedh = Rectsccs.Bottom;

                if (Rectsccs.Right < 800 || Rectsccs.Bottom < 600)
                {
                    if (Rectsccs.Right < 800)
                    {
                        correctedw = 800;
                    }
                    if (Rectsccs.Bottom < 600)
                    {
                        correctedh = 600;
                    }
                }*/


                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, (Rectsccs.Right - Rectsccs.Left), Rectsccs.Bottom - Rectsccs.Top, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


               // hasclickedmouse = 0;
            }




            /*
            var Rectsccs = new Program.RECT();
            Program.GetWindowRect(Program.vewindowsfoundedz, ref Rectsccs);


            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

            Program.RECT therect = new Program.RECT();
            therect.Left = Rectsccs.Left;
            therect.Top = Rectsccs.Top;
            therect.Bottom = Rectsccs.Bottom;
            therect.Right = Rectsccs.Right;

            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
            Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


            int screenWidth = Program.GetSystemMetrics(0);
            int screenHeight = Program.GetSystemMetrics(1);
            //set the window to a borderless style
            const int GWL_STYLE = -16; //want to change the window style
            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
            /*IntPtr sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                //return;
                Console.WriteLine("Unable to alter window style.\nSorry.");
            }

            Program.SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

            Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            */

            hasclickedmouse = 0;
        }






        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {


            //MessageBox((IntPtr)0, "checkBox1_CheckedChanged", "", 0);


            if (hasinitfullscreen == 0)
            {



                Rectsccs = new Program.RECT();
                Program.GetWindowRect(Program.vewindowsfoundedz, ref Rectsccs);

                /*const int GWL_STYLE = -16; //want to change the window style
                const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                IntPtr sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                if (sult == IntPtr.Zero)
                {
                    //in some cases SWL just outright fails, so we can notify the user and abort
                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                    //return;
                    Console.WriteLine("Unable to alter window style.\nSorry.");
                }

                Program.SetWindowPos(Program.vewindowsfoundedz, IntPtr.Zero, 0, 0, 800, 600, Program.SWP_SHOWWINDOW);
                */




                hasinitfullscreen = 1;
            }




            try
            {


                if (checkBox1 != null)
                {
                    checkBox1.Focus();
                }

                if (checkBox1.Checked)
                {


                    /*
                    if ((rectmicrosoftedge.Right - rectmicrosoftedge.Left) == Program.GetSystemMetrics(0) && (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) == Program.GetSystemMetrics(1) ||
                        (rectmicrosoftedge.Right - rectmicrosoftedge.Left) == Program.GetSystemMetrics(0) && (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top) == Program.GetSystemMetrics(1) - 1 ||

                        rectmicrosoftedge.Right == Program.GetSystemMetrics(0) && rectmicrosoftedge.Bottom == Program.GetSystemMetrics(1))
                    {

                    }
                    */


                    //Console.WriteLine(altcapturedwindowname);
                    /*
                    int screenWidth = Program.GetSystemMetrics(0);
                    int screenHeight = Program.GetSystemMetrics(1);



                    int iHandle = sccsr14sc.Form1.FindWindow(null, capturedwindowname);// "VoidExpanse");

                    Console.WriteLine(iHandle);

                    Console.WriteLine(capturedwindowname);
                    Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW




                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                    //WORKING
                    sccsr14sc.Form1.PostMessage(_hWnd, sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                    //WORKING


                    //iHandle = scgraphicssec.FindWindow(null, capturedwindownameform1);
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
                    therect.Bottom = 800;
                    therect.Right = 600;
                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);



                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOP, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);




                    //set the window to a borderless style
                    const int GWL_STYLE = -16; //want to change the window style
                    const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                    IntPtr sult = Program.SetWindowLongPtr(Program.vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);

                    //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                    if (sult == IntPtr.Zero)
                    {
                        //in some cases SWL just outright fails, so we can notify the user and abort
                        //MessageBox.Show("Unable to alter window style.\nSorry.");
                        //return;
                        //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                        Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                    }
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, Program.SWP_SHOWWINDOW);
                    //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                    //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                    //MIT-LICENSE-RichardBrass-BorderlessFullscreen



                    this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;

                    Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                    //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));

                    */





                    int screenWidth = Program.GetSystemMetrics(0);
                    int screenHeight = Program.GetSystemMetrics(1);




                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    /*
                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    */


                    /*UnSetFocus(this.Handle, "");
                    SetFocus(Program.vewindowsfoundedz , "");
                    */


                    if (capturedwindownameform1 != null && capturedwindownameform1 != "")
                    {
                        string altcapturedwindowname = capturedwindownameform1.ToLower();

                        if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                        {







                            uint lpdwProcessId;
                            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero


                            uint appThread = GetCurrentThreadId();

                            //Console.WriteLine("foreThread:" + foreThread + "/appThread:" + appThread + "/lpdwProcessId:" + lpdwProcessId);





                            if (sccsr14sc.Form1.IsWindowTopMost(Program.vewindowsfoundedz))
                            {


                                AttachThreadInput(foreThread, appThread, true);

                                //SetFocus(Program.vewindowsfoundedz, "");

                                //if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 1)
                                //{
                                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                AttachThreadInput(foreThread, appThread, false);

                                //sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                //}
                                //SetFocus(Program.vewindowsfoundedz, "");
                                /*
                                SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                */
                                Program.RECT therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = 600;
                                therect.Right = 800;

                                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);



                                this.Size = new System.Drawing.Size(800, 600);
                                this.FormBorderStyle = FormBorderStyle.Sizable;
                                this.WindowState = FormWindowState.Normal;
                                this.TopMost = true;


                                Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                            }
                            else
                            {

                                SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                                AttachThreadInput(foreThread, appThread, true);

                                //SetFocus(Program.vewindowsfoundedz, "");

                                //if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 0)
                                //{
                                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                //sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                //}
                                //SetFocus(Program.vewindowsfoundedz, "");
                                AttachThreadInput(foreThread, appThread, false);


                                Program.RECT therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = 600;
                                therect.Right = 800;

                                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);





                                this.Size = new System.Drawing.Size(800, 600);
                                this.FormBorderStyle = FormBorderStyle.Sizable;
                                this.WindowState = FormWindowState.Normal;
                                this.TopMost = true;


                                Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);









                            }












                        }
                        else if (altcapturedwindowname.Contains("firefox"))
                        {
                            var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                            if (RunningProcessPaths.Contains("firefox.exe"))
                            {
                                //firefox is running
                                Console.WriteLine("checkbox1 firefox is running");



                            }

                            /*if (RunningProcessPaths.Contains("chrome.exe"))
                            {
                                //Google Chrome is running
                                Console.WriteLine("chrome is running");
                            }*/

                        }
                        else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                        {


                        }
                        else if (altcapturedwindowname.Contains("void") && altcapturedwindowname.Contains("expanse"))
                        {



                        }
                        else
                        {

                        }
                    }
                    else
                    {           
                        sccs.Program.MessageBox((IntPtr)0, "null capturedprogram string", "sccsmsg", 0);
                    }





                    /*PostMessage(Program.vewindowsfoundedz, WM_KEYDOWN, (int)VirtualKeyCode.F11, 0);
                    //WORKING
                    PostMessage(Program.vewindowsfoundedz, WM_KEYUP, (int)VirtualKeyCode.F11, 0);
                    //WORKING*/


                    /*int iHandle = FindWindow(null, capturedwindownameform1);// "VoidExpanse");
                    Console.WriteLine(iHandle);


                    Console.WriteLine(capturedwindownameform1);
                    //button1exit.PerformClick();
                    Program.GetCursorPos(out somepoint);

                    mousex = somepoint.X;
                    mousey = somepoint.Y;

                    int screenWidth = Program.GetSystemMetrics(0);
                    int screenHeight = Program.GetSystemMetrics(1);

                    //iHandle = scgraphicssec.FindWindow(null, capturedwindownameform1);
                    //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                    //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                    scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                    scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_LBUTTONUP, 0, 0);

                    scgraphicssec.SendMessage(iHandle, (uint)WM_KEYDOWN, (int)VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                    scgraphicssec.SendMessage(iHandle, (uint)WM_KEYUP, (int)VirtualKeyCode.F11, 0);
                    */


                    //Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                    //Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);
                    //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                    /* PostMessage(Program.vewindowsfoundedz, (uint)WM_KEYDOWN, (int)VirtualKeyCode.F11, 0);
                     //WORKING
                     PostMessage(Program.vewindowsfoundedz, (uint)WM_KEYUP, (int)VirtualKeyCode.F11, 0);
                     //WORKING
                    */

                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                    /*var therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 600;
                    therect.Right = 800;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect; // new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                    this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;


                    Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);





                    int screenWidth = Program.GetSystemMetrics(0);
                    int screenHeight = Program.GetSystemMetrics(1);

                    /*SetWindowRgn(Program.vewindowsfoundedz, CreateRoundRectRgn(0, 0, screenWidth, screenHeight, 20, 20), true);
                    SetWindowRgn(Program.vewindowsfoundedz, CreateRectRgn(0, 0, screenWidth, screenHeight), true);*/

                    /*therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 600;
                    therect.Right = 800;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect; // new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    */




                    //SetWindowRgn(Program.vewindowsfoundedz, CreateRectRgn(0, 0, 800, 600), true);







                    checkBox1_CheckedChangedint = 1;
                }
                else
                {





                    if (checkBox3_CheckedChangedint == 0)
                    {





                        string altcapturedwindowname = capturedwindownameform1.ToLower();

                        if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                        {

                            int screenWidth = Program.GetSystemMetrics(0);
                            int screenHeight = Program.GetSystemMetrics(1);


                            SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                          



                            var param = new Program.WINDOWPLACEMENT();
                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));



                            uint lpdwProcessId;
                            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero


                            uint appThread = GetCurrentThreadId();


                            if (sccsr14sc.Form1.IsWindowTopMost(Program.vewindowsfoundedz))
                            {



                                //SetFocus(Program.vewindowsfoundedz, "");

                                //if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 1)
                                //{
                                //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                //sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                //}
                                //SetFocus(Program.vewindowsfoundedz, "");
                                /*
                                SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                */

                                Program.RECT therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


                                /*Window thewindow = new Window();
                                //thewindow.
                                var wih = new System.Windows.Interop.WindowInteropHelper(thewindow); //this
                                var hWnd = wih.Handle;

                                thewindow.Show();*/









                                /*
                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;


                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);*/

                                /*
                                SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                *



                                AttachThreadInput(foreThread, appThread, true);

                                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                AttachThreadInput(foreThread, appThread, false);


                                this.Size = new System.Drawing.Size(screenWidth, screenHeight);
                                this.FormBorderStyle = FormBorderStyle.None;
                                this.WindowState = FormWindowState.Maximized;
                                this.TopMost = true;



                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(this.Handle, ref param);

                                Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                            }
                            else
                            {

                                Program.RECT therect = new Program.RECT();
                                /*therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);*/
                                /*
                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);
                                */
                                //SetFocus(Program.vewindowsfoundedz, "");

                                //if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 1)
                                //{


                                //SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                                AttachThreadInput(foreThread, appThread, true);

                                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                //sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                //}
                                //SetFocus(Program.vewindowsfoundedz, "");
                                
                              
                            

                                AttachThreadInput(foreThread, appThread, false);


                                this.Size = new System.Drawing.Size(screenWidth, screenHeight);
                                this.FormBorderStyle = FormBorderStyle.None;
                                this.WindowState = FormWindowState.Maximized;
                                this.TopMost = true;



                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(this.Handle, ref param);


                                Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));

                            }








                        }
                        else if (altcapturedwindowname.Contains("firefox"))
                        {
                            var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                            if (RunningProcessPaths.Contains("firefox.exe"))
                            {


                            }

                            /*if (RunningProcessPaths.Contains("chrome.exe"))
                            {
                                //Google Chrome is running
                                Console.WriteLine("chrome is running");
                            }*/

                        }
                        else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                        {


                        }
                        else if (altcapturedwindowname.Contains("void") && altcapturedwindowname.Contains("expanse"))
                        {
                            //if (altcapturedwindowname == "void expanse")
                            {
                                
                            }
                        }
                        else
                        {


                        }


                    }
                    else if (checkBox3_CheckedChangedint == 1)
                    {



                        string altcapturedwindowname = capturedwindownameform1.ToLower();

                        if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                        {


                            int screenWidth = Program.GetSystemMetrics(0);
                            int screenHeight = Program.GetSystemMetrics(1);




                            var param = new Program.WINDOWPLACEMENT();
                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                            uint lpdwProcessId;
                            uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero


                            uint appThread = GetCurrentThreadId();


                            if (sccsr14sc.Form1.IsWindowTopMost(Program.vewindowsfoundedz))
                            {



                                //SetFocus(Program.vewindowsfoundedz, "");

                                //if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 1)
                                //{

                                //sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                //}
                                //SetFocus(Program.vewindowsfoundedz, "");
                                /*
                                SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                */
                                Program.RECT therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);
                                /*
                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);*/

                                /*SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                */
                                AttachThreadInput(foreThread, appThread, true);

                                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                AttachThreadInput(foreThread, appThread, false);


                                this.Size = new System.Drawing.Size(screenWidth, screenHeight);
                                this.FormBorderStyle = FormBorderStyle.None;
                                this.WindowState = FormWindowState.Maximized;
                                this.TopMost = true;



                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(this.Handle, ref param);


                                Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                            }
                            else
                            {

                               

                                Program.RECT therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);


                                /*
                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);
                                */
                                //SetFocus(Program.vewindowsfoundedz, "");

                                //if (sccsr14sc.Form1.wasf11pressedbynativeonedge == 1)
                                //{
                                /*
                                SetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                */

                                AttachThreadInput(foreThread, appThread, true);

                                Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                                //sccsr14sc.Form1.wasf11pressedbynativeonedge = 1;
                                //}
                                //SetFocus(Program.vewindowsfoundedz, "");




                                AttachThreadInput(foreThread, appThread, false);


                                this.Size = new System.Drawing.Size(screenWidth, screenHeight);
                                this.FormBorderStyle = FormBorderStyle.None;
                                this.WindowState = FormWindowState.Maximized;
                                this.TopMost = true;



                                therect = new Program.RECT();
                                therect.Left = 0;
                                therect.Top = 0;
                                therect.Bottom = screenHeight;
                                therect.Right = screenWidth;

                                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                Program.SetWindowPlacement(this.Handle, ref param);


                                Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));

                            }



                        }
                        else if (altcapturedwindowname.Contains("firefox"))
                        {
                            var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                            if (RunningProcessPaths.Contains("firefox.exe"))
                            {


                            }

                            /*if (RunningProcessPaths.Contains("chrome.exe"))
                            {
                                //Google Chrome is running
                                Console.WriteLine("chrome is running");
                            }*/

                        }
                        else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                        {


                        }
                        else if (altcapturedwindowname.Contains("void") && altcapturedwindowname.Contains("expanse"))
                        {
                            //if (altcapturedwindowname == "void expanse")
                            {


                            }
                        }
                        else
                        {


                        }

                    }



                    checkBox1_CheckedChangedint = 0;
                    /*
                    if (haspressedf9 == 0)
                    {
                        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));



                        SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                        //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                        //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);


                        //int result = GetWindowLong(Program.vewindowsfoundedz, GWL_STYLE);
                        //bool isVisible = ((result & WS_VISIBLE) != 0);

                        Program.RECT therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = 1080;
                        therect.Right = 1920;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                        Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                        this.Size = new System.Drawing.Size(1920, 1080);
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.WindowState = FormWindowState.Maximized;
                        this.TopMost = true;


                        SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));

                        /*
                        this.Size = new System.Drawing.Size(800, 600);
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.WindowState = FormWindowState.Normal;
                    }
                    else //if (haspressedf9 == 2)
                    {
                        this.Size = new System.Drawing.Size(800, 600);
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.WindowState = FormWindowState.Normal;
                    }*/

                    /*
                    if (haspressedf9 == 0)
                    {
                        this.Size = new System.Drawing.Size(800, 600);
                        this.FormBorderStyle = FormBorderStyle.Sizable;
                        this.WindowState = FormWindowState.Normal;
                    }
                    else if (haspressedf9 == 1)
                    {
                        this.Size = new System.Drawing.Size(1920, 1080);
                        this.FormBorderStyle = FormBorderStyle.None;
                        this.WindowState = FormWindowState.Maximized;
                        this.TopMost = true;
                    }
                     */

                    /*
                    this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;*/
                    //hasclickedcheckbox = 2;
                    //checkBox3_CheckedChangedint = 0;
                }

                if (labeltext2 != null)
                {
                    labeltext2.Focus();
                }


                haspressedf9 = 0;


                lastRectsccs = Rectsccs;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }




            //checkBox1.Checked = false;
        }

        int hasclickedcheckbox = 0;



        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2 != null)
            {
                checkBox2.Focus();
            }


            //Program.MessageBox((IntPtr)0, "checkBox2_CheckedChanged start ", "sccsmsg", 0);



            //Program.EnumWindows(Program.enumWindowProc, IntPtr.Zero);

            
            if (checkBox2.Checked)
            {

                /*int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);

                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                */

                //Program.EnumWindows(Program.enumWindowProc, IntPtr.Zero);

                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;

                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));
                */









                /*
                int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);

                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);*/
                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;

                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));*/




                /*
                var param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                var therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;
                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);

                 param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                 therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;
                param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/

                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;

                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));*/


                /*
                int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);

                Program.SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                */

                /*if (haspressedf9 == 0)
                {
                    Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);



                    //int result = GetWindowLong(Program.vewindowsfoundedz, GWL_STYLE);
                    // bool isVisible = ((result & WS_VISIBLE) != 0);

                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 1080;
                    therect.Right = 1920;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;
                }
                else if (haspressedf9 == 1)
                {
                    this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                }
                */

                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));


                /*
                if (haspressedescape == 0)
                {
                    /*this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;



                    Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);


                    
                    //int result = GetWindowLong(Program.vewindowsfoundedz, GWL_STYLE);
                   // bool isVisible = ((result & WS_VISIBLE) != 0);
                    
                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 1080;
                    therect.Right = 1920;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;


                    SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));

                }
                else if (haspressedescape == 1)
                {
                   /* this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;



                    Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);


                    
                    //int result = GetWindowLong(Program.vewindowsfoundedz, GWL_STYLE);
                    //bool isVisible = ((result & WS_VISIBLE) != 0);
                    
                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 600;
                    therect.Right = 800;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;


                   //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));


                    /*
                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;
                    this.TopMost = true;
                }*/



                /*
                if (sccsr14sc.Form1.checkbox1.Checked)
                {
                    sccsr14sc.Form1.checkbox1.Checked = false;
                }
                else if (!sccsr14sc.Form1.checkbox1.Checked)
                {
                    sccsr14sc.Form1.checkbox1.Checked = true;
                }
                */

                if (haspressedescape == 0)
                {

                    Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);



                    //int result = GetWindowLong(Program.vewindowsfoundedz, GWL_STYLE);
                    // bool isVisible = ((result & WS_VISIBLE) != 0);

                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 1080;
                    therect.Right = 1920;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;


                    //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));

                }
                else //if (haspressedescape == 2)
                {
                    
                    Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                    //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);



                    //int result = GetWindowLong(Program.vewindowsfoundedz, GWL_STYLE);
                    // bool isVisible = ((result & WS_VISIBLE) != 0);

                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 1080;
                    therect.Right = 1920;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;


                    SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                    
                    
                    /*
                    this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;*/


                }

            }
            else
            {

                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;


                if (haspressedf9 == 0)
                {
                    /*this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;
                    */

                    /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    Program.RECT therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = 1080;
                    therect.Right = 1920;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                    this.Size = new System.Drawing.Size(1920, 1080);
                    this.FormBorderStyle = FormBorderStyle.None;
                    this.WindowState = FormWindowState.Maximized;
                    this.TopMost = true;


                    SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));*/

                }
                else if (haspressedf9 == 1)
                {



                    /*
                    this.Size = new System.Drawing.Size(800, 600);
                    this.FormBorderStyle = FormBorderStyle.Sizable;
                    this.WindowState = FormWindowState.Normal;*/

                }
            }


            haspressedf9 = 0;
            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }

            //sccsr14sc.Form1.haspressedf9 = 0;

            /*if (checkBox2 != null)
            {
                checkBox2.Focus();
            }

            if (checkBox2.Checked)
            {
                this.Size = new System.Drawing.Size(1920, 1080);


                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST| WS_EX_TRANSPARENT));
                SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);

                this.AllowTransparency = true;
                this.TransparencyKey = System.Drawing.Color.LightGray;
                this.BackColor = System.Drawing.Color.LightGray;

                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                pinvokeornot = 1;
            }
            else
            {
                this.Size = new System.Drawing.Size(800, 600);


                this.AllowTransparency = false;
                this.TransparencyKey = System.Drawing.Color.LightGray;
                this.BackColor = System.Drawing.Color.LightGray;
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));
                pinvokeornot = 0;
            }*/
        }


        public static int counterscreencapturechanged = -1;
        public static int screencaptureindextype = 0;
        public static int lastscreencaptureindextype = 0;
        public static string selectedscreencaptureindex;
        //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.combobox.selectedindexchanged?view=windowsdesktop-6.0
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboboxcapturelist != null)
            {
                comboboxcapturelist.Focus();
            }

            if (counterscreencapturechanged >= 1)
            {
                counterscreencapturechanged = 0;
            }

            if (counterscreencapturechanged == 0)
            {
                ComboBox comboBox = (ComboBox)sender;
                selectedscreencaptureindex = (string)comboboxcapturelist.SelectedItem;
                screencaptureindextype = comboboxcapturelist.FindStringExact(selectedscreencaptureindex);






                if (screencaptureindextype != lastscreencaptureindextype)
                {
                    Program.screencapturetype = screencaptureindextype;
                    Program.changedscreencapturetype = 1;

                    lastscreencaptureindextype = screencaptureindextype;
                    //sccs.Program.MessageBox((IntPtr)0, "combobox screencapture type changed!", "scmsg", 0);
                    //thepanel.Focus();
                    //comboboxcapturelist.
                }
                else
                {
                    //the captured window has been closed. let's restart capture.

                }
                Program.lastcapturetypevalue = selectedscreencaptureindex;
                counterscreencapturechanged = 0;
            }
            else if(counterscreencapturechanged == 2)
            {

                Console.WriteLine("comboboxforcedchanged");
                ComboBox comboBox = (ComboBox)sender;
                string selectedscreencaptureindex = (string)comboboxcapturelist.SelectedItem;
                screencaptureindextype = comboboxcapturelist.FindStringExact(selectedscreencaptureindex);






                if (screencaptureindextype != lastscreencaptureindextype)
                {
                    /*Program.screencapturetype = screencaptureindextype;
                    Program.changedscreencapturetype = 1;

                    lastscreencaptureindextype = screencaptureindextype;*/
                    //sccs.Program.MessageBox((IntPtr)0, "combobox screencapture type changed!", "scmsg", 0);
                    //thepanel.Focus();
                    //comboboxcapturelist.
                }
                else
                {
                    /*Program.screencapturetype = screencaptureindextype;
                    Program.changedscreencapturetype = 1;

                    lastscreencaptureindextype = screencaptureindextype;
                    //the captured window has been closed. let's restart capture.*/

                    //Program.

                }
                Program.lastcapturetypevalue = selectedscreencaptureindex;
            }
            //counterscreencapturechanged = 0;
            counterscreencapturechanged++;
            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }
        }

        //https://docs.microsoft.com/en-us/dotnet/api/system.threading.timer?view=net-6.0
        /*class TimerExample
        {
            static void Main()
            {
                
            }
        }*/

        static AutoResetEvent AutoResetEvent0;
        static AutoResetEvent AutoResetEvent1;

        class StatusChecker0
        {
            private int invokeCount;
            private int maxCount;

            AutoResetEvent AutoResetEvent;
            AutoResetEvent autoEvent;
            int indextimer;

            public StatusChecker0(int count, AutoResetEvent AutoResetEvent_,int indextimer_)
            {
                invokeCount = 0;
                maxCount = count;
                indextimer = indextimer_;
                AutoResetEvent = AutoResetEvent_;
            }

            // This method is called by the timer delegate.
            public void CheckStatus(Object stateInfo)
            {
                /*threadloop:


              

                Thread.Sleep(1);
                goto threadloop;*/

                autoEvent = (AutoResetEvent)stateInfo;
                autoEvent.Set();

                /*if (form1hasshown == 1)
                {
                    if (keynmouseinput != null)
                    {
                        keynmouseinput.Frame();
                        Console.WriteLine("can read keyboard" + " tindex:" + indextimer);
                    }
                }
                */

                //AutoResetEvent1.WaitOne();
                Console.WriteLine("tindex:" + indextimer + " // " + "{0} Checking status {1,2}.",
                       DateTime.Now.ToString("h:mm:ss.fff"),
                       (++invokeCount).ToString());


                if (invokeCount == maxCount)
                {
                    //AutoResetEvent0.WaitOne();
                    Console.WriteLine("reached max " + "tindex:" + indextimer);
                    
                    invokeCount = 0;
                }

                //Execute();
                
                /*
                  if (indextimer == 0)
                  {
                      Console.WriteLine("tindex:" + indextimer + " // " + "{0} Checking status {1,2}.",
                         DateTime.Now.ToString("h:mm:ss.fff"),
                         (++invokeCount).ToString());
                      if (invokeCount == maxCount)
                      {
                          Console.WriteLine("reached max " + "tindex:" + indextimer);
                          AutoResetEvent.WaitOne();
                          invokeCount = 0;
                      }
                  }

                  if (indextimer == 1)
                  {
                      Console.WriteLine("tindex:" + indextimer + " // " + "{0} Checking status {1,2}.",
                          DateTime.Now.ToString("h:mm:ss.fff"),
                          (++invokeCount).ToString());
                      if (invokeCount == maxCount)
                      {

                          Console.WriteLine("reached max " + "tindex:" + indextimer);
                          AutoResetEvent.Set();
                          invokeCount = 0;
                      }
                  }*/



                //Program.createinputs(Form1.someform.Handle);


                /*threadloop:

                    /*Console.WriteLine("{0} Checking status {1,2}.",
                    DateTime.Now.ToString("h:mm:ss.fff"),
                    (++invokeCount).ToString());
                    */

                /*if (form1hasshown == 1)
                {
                    if (keynmouseinput != null)
                    {
                        keynmouseinput.Frame();
                    }

                }
               

                Thread.Sleep(1);
                goto threadloop;*/

                /*Console.WriteLine("tindex:" + indextimer + " // " + "{0} Checking status {1,2}.",
                    DateTime.Now.ToString("h:mm:ss.fff"),

                    (++invokeCount).ToString());*/

                //Execute();
                //currentWaitHandle = (currentWaitHandle == AutoResetEvent0) ? AutoResetEvent1 : AutoResetEvent0;
                //currentWaitHandle.WaitOne();


                //autoEvent.WaitOne();

                /*if (invokeCount == maxCount)
                {
                    autoEvent.Set();
                    Execute();
                    // Reset the counter and signal the waiting thread.
                    invokeCount = 0;
                    //resetcounter();
                    //
                }*/
            }

            public void Execute()
            {
                //currentTaskIndex = 0;
                //waitingThreadCount = 0;

                /*currentWaitHandle.Set();
                //PumpTasks();

                //while (waitingThreadCount < threads.Length - 1) Thread.Sleep(0);

        
                currentWaitHandle = (currentWaitHandle == AutoResetEvent0) ? AutoResetEvent1 : AutoResetEvent0;
                currentWaitHandle.WaitOne();*/
                //currentWaitHandle.Reset();
                //tasks.Clear();
                //parameters.Clear();
            }

            public void resetcounter()
            {
                autoEvent.WaitOne();
            }
        }


       static AutoResetEvent currentWaitHandle;


        class StatusChecker1
        {
            private int invokeCount;
            private int maxCount;

            AutoResetEvent AutoResetEvent;
            AutoResetEvent autoEvent;
            int indextimer;

            public StatusChecker1(int count, AutoResetEvent AutoResetEvent_, int indextimer_)
            {
                invokeCount = 0;
                maxCount = count;
                indextimer = indextimer_;
                AutoResetEvent = AutoResetEvent_;
            }

            // This method is called by the timer delegate.
            public void CheckStatus(Object stateInfo)
            {
               
                Console.WriteLine("tindex:" + indextimer + " // " + "{0} Checking status {1,2}.",
                         DateTime.Now.ToString("h:mm:ss.fff"),
                         (++invokeCount).ToString());
                if (invokeCount == maxCount)
                {
                    AutoResetEvent1.Set();
                    Console.WriteLine("reached max " + "tindex:" + indextimer);
                    
                    invokeCount = 0;
                }


                /*
                if (indextimer == 0)
                {
                    Console.WriteLine("tindex:" + indextimer + " // " + "{0} Checking status {1,2}.",
                       DateTime.Now.ToString("h:mm:ss.fff"),
                       (++invokeCount).ToString());
                    if (invokeCount == maxCount)
                    {
                        Console.WriteLine("reached max " + "tindex:" + indextimer);
                        AutoResetEvent.WaitOne();
                        invokeCount = 0;
                    }
                }

                if (indextimer == 1)
                {
                    Console.WriteLine("tindex:" + indextimer + " // " + "{0} Checking status {1,2}.",
                        DateTime.Now.ToString("h:mm:ss.fff"),
                        (++invokeCount).ToString());
                    if (invokeCount == maxCount)
                    {

                        Console.WriteLine("reached max " + "tindex:" + indextimer);
                        AutoResetEvent.Set();
                        invokeCount = 0;
                    }
                }
                */


                //Program.createinputs(Form1.someform.Handle);


                /*threadloop:

                    /*Console.WriteLine("{0} Checking status {1,2}.",
                    DateTime.Now.ToString("h:mm:ss.fff"),
                    (++invokeCount).ToString());
                    */

                /*if (form1hasshown == 1)
                {
                    if (keynmouseinput != null)
                    {
                        keynmouseinput.Frame();
                    }

                }
               

                Thread.Sleep(1);
                goto threadloop;*/

                /*autoEvent = (AutoResetEvent)stateInfo;
                Console.WriteLine("{0} Checking status {1,2}.",
                    DateTime.Now.ToString("h:mm:ss.fff"),
                    (++invokeCount).ToString());

                if (invokeCount == maxCount)
                {
                    // Reset the counter and signal the waiting thread.
                    invokeCount = 0;
                    //resetcounter();
                    autoEvent.Set();
                }*/
            }


            public void resetcounter()
            {
                autoEvent.WaitOne();
            }











        }



        /*public static DInput keynmouseinput;
        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;
        public static void createinputs(IntPtr thehandle)
        {
            inputsim = new InputSimulator();
            mousesim = new MouseSimulator(inputsim);
            keyboardsim = new KeyboardSimulator(inputsim);

            keynmouseinput = new DInput(); //sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle
            keynmouseinput.Initialize(Program.config, SCGLOBALSACCESSORS.SCCONSOLECORE.handle); //sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle //thehandle
        }

        */


        static int invokeCount = 0;
        private void mainthreadloop(object sender, EventArgs e)
        {
            /*Console.WriteLine("{0} Checking status {1,2}.",
                          DateTime.Now.ToString("h:mm:ss.fff"),
                          (++invokeCount).ToString());*/
            /*if (invokeCount == maxCount)
            {
                AutoResetEvent1.Set();
                Console.WriteLine("reached max " + "tindex:" + indextimer);

                invokeCount = 0;
            }*/

            /*
            if (form1hasshown == 1)
            {
                if (keynmouseinput != null)
                {
                    keynmouseinput.Frame();
                    //Console.WriteLine("can read keyboard");
                }
            }
            Thread.Sleep(1);*/
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }


        public int pinvokeornot = 0;
        
        public int cursorlightoption = 0;
        public int gridtypeoption = 0;

        public int swtccursorlightr = 0;
        public int swtccursorlightg = 0;
        public int swtccursorlightb = 0;

        public int swtcgridr = 0;
        public int swtcgridg = 0;
        public int swtcgridb = 0;

        public int cursorlightcolorr = 0;
        public int cursorlightcolorg = 0;
        public int cursorlightcolorb = 0;

        public int gridcolorr = 0;
        public int gridcolorg = 0;
        public int gridcolorb = 0;

        /*
        private void numericupdownresetfocus(object sender, EventArgs e)
        {

            if (numericupdown1swtc == 1)
            {
                if (numericUpDown01 != null)
                {
                    numericUpDown01.Focus();
                }

                if (numericUpDown01.Value < 0)
                {
                    numericUpDown01.Value = 0;
                }
                else if (numericUpDown01.Value >= 255)
                {
                    numericUpDown01.Value = 255;
                }
                cursorlightcolorr = (int)numericUpDown01.Value;
                swtccursorlightr = 1;
              
                numericupdown1swtc = 0;
            }
            else if (numericupdown2swtc == 1)
            {
                if (numericUpDown02 != null)
                {
                    numericUpDown02.Focus();
                }
                if (numericUpDown02.Value < 0)
                {
                    numericUpDown02.Value = 0;
                }
                else if (numericUpDown02.Value >= 255)
                {
                    numericUpDown02.Value = 255;
                }
                cursorlightcolorr = (int)numericUpDown02.Value;
                swtccursorlightr = 1;
               
                numericupdown2swtc = 0;
            }
            else if (numericupdown3swtc == 1)
            {
                if (numericUpDown03 != null)
                {
                    numericUpDown03.Focus();
                }
                if (numericUpDown03.Value < 0)
                {
                    numericUpDown03.Value = 0;
                }
                else if (numericUpDown03.Value >= 255)
                {
                    numericUpDown03.Value = 255;
                }
                cursorlightcolorr = (int)numericUpDown03.Value;
                swtccursorlightr = 1;
              
                numericupdown3swtc = 0;
            }
            else if (numericupdown4swtc == 1)
            {
                if (numericUpDown04 != null)
                {
                    numericUpDown04.Focus();
                }
                if (numericUpDown04.Value < 0)
                {
                    numericUpDown04.Value = 0;
                }
                else if (numericUpDown04.Value >= 255)
                {
                    numericUpDown04.Value = 255;
                }
                cursorlightcolorr = (int)numericUpDown04.Value;
                swtccursorlightr = 1;
              
                numericupdown4swtc = 0;
            }
            else if (numericupdown5swtc == 1)
            {
                if (numericUpDown05 != null)
                {
                    numericUpDown05.Focus();
                }
                if (numericUpDown05.Value < 0)
                {
                    numericUpDown05.Value = 0;
                }
                else if (numericUpDown05.Value >= 255)
                {
                    numericUpDown05.Value = 255;
                }
                cursorlightcolorr = (int)numericUpDown05.Value;
                swtccursorlightr = 1;
             
                numericupdown5swtc = 0;

            }
            else if (numericupdown6swtc == 1)
            {
                if (numericUpDown06 != null)
                {
                    numericUpDown06.Focus();
                }
                if (numericUpDown06.Value < 0)
                {
                    numericUpDown06.Value = 0;
                }
                else if (numericUpDown06.Value >= 255)
                {
                    numericUpDown06.Value = 255;
                }
                cursorlightcolorr = (int)numericUpDown06.Value;
                swtccursorlightr = 1;
             
                numericupdown6swtc = 0;
            }















        }


        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                

                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                //this.TopMost = true;
            }

            if (numericUpDown01.Value < 0)
            {
                numericUpDown01.Value = 0;
            }
            else if (numericUpDown01.Value >= 255)
            {
                numericUpDown01.Value = 255;
            }
            cursorlightcolorr = (int)numericUpDown01.Value;
            swtccursorlightr = 1;
            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }
            numericupdown1swtc = 1;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                

                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                //this.TopMost = true;
            }

            if (numericUpDown02.Value < 0)
            {
                numericUpDown02.Value = 0;
            }
            else if (numericUpDown02.Value >= 255)
            {
                numericUpDown02.Value = 255;
            }
            cursorlightcolorg = (int)numericUpDown02.Value;
            swtccursorlightg = 1;
            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }


            numericupdown2swtc = 1;


        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                

                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                //this.TopMost = true;
            }

            if (numericUpDown03.Value < 0)
            {
                numericUpDown03.Value = 0;
            }
            else if (numericUpDown03.Value >= 255)
            {
                numericUpDown03.Value = 255;
            }
            cursorlightcolorb = (int)numericUpDown03.Value;
            swtccursorlightb = 1;
            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }

            numericupdown3swtc = 1;

        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                

                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                //this.TopMost = true;
            }

            if (numericUpDown04.Value < 0)
            {
                numericUpDown04.Value = 0;
            }
            else if (numericUpDown04.Value >= 255)
            {
                numericUpDown04.Value = 255;
            }
            gridcolorr = (int)numericUpDown04.Value;
            swtcgridr = 1;
            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }

            numericupdown4swtc = 1;


        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true


                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                

                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                //this.TopMost = true;
            }

            if (numericUpDown05.Value < 0)
            {
                numericUpDown05.Value = 0;
            }
            else if (numericUpDown05.Value >= 255)
            {
                numericUpDown05.Value = 255;
            }
            gridcolorg = (int)numericUpDown05.Value;
            swtcgridg = 1;
            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }
            numericupdown5swtc = 1;

        }

        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                

                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                //this.TopMost = true;
            }

            if (numericUpDown06.Value < 0)
            {
                numericUpDown06.Value = 0;
            }
            else if (numericUpDown06.Value >= 255)
            {
                numericUpDown06.Value = 255;
            }
            gridcolorb = (int)numericUpDown06.Value;
            swtcgridb = 1;
            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }

            numericupdown6swtc = 1;

        }*/

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {
            //labeltext2.Focus();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }




        // Handle the KeyUp event to print the type of character entered into the control.
        private void TextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            //textBox1.AppendText($"KeyUp code: {e.KeyCode}, value: {e.KeyValue}, modifiers: {e.Modifiers}" + "\r\n");
            //sccs.Program.MessageBox((IntPtr)0, "TextBox1_KeyUp", "scmsg", 0);
        }

        // Handle the KeyPress event to print the type of character entered into the control.
        private void TextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //textBox1.AppendText($"KeyPress keychar: {e.KeyChar}" + "\r\n");
            //sccs.Program.MessageBox((IntPtr)0, "TextBox1_KeyPress", "scmsg", 0);
        }

        // Handle the KeyDown event to print the type of character entered into the control.
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //textBox1.AppendText($"KeyDown code: {e.KeyCode}, value: {e.KeyValue}, modifiers: {e.Modifiers}" + "\r\n");
            //sccs.Program.MessageBox((IntPtr)0, "TextBox1_KeyDown", "scmsg", 0);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            if (Program.updatescript.exitthread0 == 0)
            {
                Program.updatescript.exitthread0 = 1;
            }
            if (Program.updatescript.exitthread1 == 0)
            {
                Program.updatescript.exitthread1 = 1;
            }
            Program.exitedprogram = 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.changedscreencapturetype = 1;

        }


        public int checkBox3_CheckedChangedint = 0;
        int menuopenedinit = 0;

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3 != null)
            {
                checkBox3.Focus();
            }


            //Program.MessageBox((IntPtr)0, "checkBox3_CheckedChanged start ", "sccsmsg", 0);



            //Program.EnumWindows(Program.enumWindowProc, IntPtr.Zero);


            if (checkBox3.Checked)
            {
                if (checkBox1_CheckedChangedint == 0)
                {



                }
                else if (checkBox1_CheckedChangedint == 1)
                {
                    /*int screenWidth = Program.GetSystemMetrics(0);
                    int screenHeight = Program.GetSystemMetrics(1);



                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);




                    string altcapturedwindowname = capturedwindownameform1.ToLower();

                    if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                    {



                    }
                    else if (altcapturedwindowname.Contains("firefox"))
                    {
                        var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                        if (RunningProcessPaths.Contains("firefox.exe"))
                        {
                            //firefox is running
                            Console.WriteLine("checkbox1 firefox is running");




                            if (sccsr14sc.Form1.wasf11pressedonfirefox == 1)
                            {
                                if (IsWindowTopMost(Program.vewindowsfoundedz))
                                {

                                }
                                else
                                {

                                }
                            }
                            else
                            {


                            }


                        }

                        /*if (RunningProcessPaths.Contains("chrome.exe"))
                        {
                            //Google Chrome is running
                            Console.WriteLine("chrome is running");
                        }

                    }
                    else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                    {

                    }
                    else
                    {

                    }*/

                }


                checkBox3_CheckedChangedint = 1;
            }
            else
            {
                if (checkBox1_CheckedChangedint == 0)
                {
                    /*int screenWidth = Program.GetSystemMetrics(0);
                    int screenHeight = Program.GetSystemMetrics(1);



                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);




                    string altcapturedwindowname = capturedwindownameform1.ToLower();

                    if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                    {






                    }
                    else if (altcapturedwindowname.Contains("firefox"))
                    {
                        var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                        if (RunningProcessPaths.Contains("firefox.exe"))
                        {
                            //firefox is running
                            Console.WriteLine("checkbox1 firefox is running");




                            if (sccsr14sc.Form1.wasf11pressedonfirefox == 1)
                            {
                                if (IsWindowTopMost(Program.vewindowsfoundedz))
                                {

                                }
                                else
                                {

                                }
                            }
                            else
                            {


                            }


                        }

                        /*if (RunningProcessPaths.Contains("chrome.exe"))
                        {
                            //Google Chrome is running
                            Console.WriteLine("chrome is running");
                        }

                    }
                    else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                    {

                    }
                    else
                    {

                    }*/
                }
                else if (checkBox1_CheckedChangedint == 1)
                {
                    /*int screenWidth = Program.GetSystemMetrics(0);
                    int screenHeight = Program.GetSystemMetrics(1);



                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);




                    string altcapturedwindowname = capturedwindownameform1.ToLower();

                    if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                    {





                    }
                    else if (altcapturedwindowname.Contains("firefox"))
                    {
                        var RunningProcessPaths = ProcessFileNameFinderClass.GetAllRunningProcessFilePaths();

                        if (RunningProcessPaths.Contains("firefox.exe"))
                        {
                            //firefox is running
                            Console.WriteLine("checkbox1 firefox is running");




                            if (sccsr14sc.Form1.wasf11pressedonfirefox == 1)
                            {
                                if (IsWindowTopMost(Program.vewindowsfoundedz))
                                {

                                }
                                else
                                {

                                }
                            }
                            else
                            {


                            }


                        }

                        /*if (RunningProcessPaths.Contains("chrome.exe"))
                        {
                            //Google Chrome is running
                            Console.WriteLine("chrome is running");
                        }

                    }
                    else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program") && altcapturedwindowname.Length == 30)
                    {

                    }
                    else
                    {

                    }*/
                }

                checkBox3_CheckedChangedint = 0;
            }

            if (labeltext2 != null)
            {
                labeltext2.Focus();
            }
            //sccsr14sc.Form1.haspressedf9 = 0;

            //sccsr14sc.Form1.haspressedf9 = 0;

            //checkBox3_CheckedChangedint = 0;
        }


        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                */

                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 600;
                therect.Right = 800;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                */
                /*
                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;*/
            }



            if (this.trackBar2 != null)
            {
                this.trackBar2.Focus();
            }

            if (this.trackBar2.Value < 0)
            {
                this.trackBar2.Value = 0;
            }
            else if (this.trackBar2.Value >= 255)
            {
                this.trackBar2.Value = 255;
            }
            cursorlightcolorr = (int)this.trackBar2.Value;
            swtccursorlightr = 1;
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                */

                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 600;
                therect.Right = 800;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                */
                /*
                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;*/
            }
            if (this.trackBar3 != null)
            {
                this.trackBar3.Focus();
            }

            if (this.trackBar3.Value < 0)
            {
                this.trackBar3.Value = 0;
            }
            else if (this.trackBar3.Value >= 255)
            {
                this.trackBar3.Value = 255;
            }
            cursorlightcolorg = (int)this.trackBar3.Value;
            swtccursorlightg = 1;
        }
        private void trackBar4_Scroll(object sender, EventArgs e)
        {

            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                */

                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 600;
                therect.Right = 800;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                */
                /*
                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;*/
            }
            if (this.trackBar4 != null)
            {
                this.trackBar4.Focus();
            }

            if (this.trackBar4.Value < 0)
            {
                this.trackBar4.Value = 0;
            }
            else if (this.trackBar4.Value >= 255)
            {
                this.trackBar4.Value = 255;
            }
            cursorlightcolorb = (int)this.trackBar4.Value;
            swtccursorlightb = 1;
        }
        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                */

                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 600;
                therect.Right = 800;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                */
                /*
                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;*/
            }
            if (this.trackBar5 != null)
            {
                this.trackBar5.Focus();
            }

            if (this.trackBar5.Value < 0)
            {
                this.trackBar5.Value = 0;
            }
            else if (this.trackBar5.Value >= 255)
            {
                this.trackBar5.Value = 255;
            }
            gridcolorr = (int)this.trackBar5.Value;
            swtcgridr = 1;
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                */

                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 600;
                therect.Right = 800;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                */
                /*
                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;*/
            }
            if (this.trackBar6 != null)
            {
                this.trackBar6.Focus();
            }

            if (this.trackBar6.Value < 0)
            {
                this.trackBar6.Value = 0;
            }
            else if (this.trackBar6.Value >= 255)
            {
                this.trackBar6.Value = 255;
            }
            gridcolorg = (int)this.trackBar6.Value;
            swtcgridg = 1;
        }

        private void trackBar7_Scroll(object sender, EventArgs e)
        {
            if (checkBox1_CheckedChangedint == 0)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                

                /*
                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                */

                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                //Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            }
            else if (checkBox1_CheckedChangedint == 1)
            {
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                //SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 600;
                therect.Right = 800;

                param.ShowCmd = Program.ShowWindowCommands.ShowNoActivate; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 800, 600, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                


                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                SetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(Program.vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_TOPMOST));
                //Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, 1920, 1080, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                this.Size = new System.Drawing.Size(1920, 1080);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                */
                /*
                this.Size = new System.Drawing.Size(800, 600);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
                this.TopMost = true;*/
            }
            if (this.trackBar7 != null)
            {
                this.trackBar7.Focus();
            }

            if (this.trackBar7.Value < 0)
            {
                this.trackBar7.Value = 0;
            }
            else if (this.trackBar7.Value >= 255)
            {
                this.trackBar7.Value = 255;
            }
            gridcolorb = (int)this.trackBar7.Value;
            swtcgridb = 1;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            /*if (checkbox4swtc == 0)
            {
                checkbox4swtc = 1;
            }*/

            checkbox4swtc = 1;
        }



        /*
private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
{

}
*/














        // The example displays output like the following:
        //       11:59:54.202 Creating timer.
        //       
        //       11:59:55.217 Checking status  1.
        //       11:59:55.466 Checking status  2.
        //       11:59:55.716 Checking status  3.
        //       11:59:55.968 Checking status  4.
        //       11:59:56.218 Checking status  5.
        //       11:59:56.470 Checking status  6.
        //       11:59:56.722 Checking status  7.
        //       11:59:56.972 Checking status  8.
        //       11:59:57.223 Checking status  9.
        //       11:59:57.473 Checking status 10.
        //       
        //       Changing period to .5 seconds.
        //       
        //       11:59:57.474 Checking status  1.
        //       11:59:57.976 Checking status  2.
        //       11:59:58.476 Checking status  3.
        //       11:59:58.977 Checking status  4.
        //       11:59:59.477 Checking status  5.
        //       11:59:59.977 Checking status  6.
        //       12:00:00.478 Checking status  7.
        //       12:00:00.980 Checking status  8.
        //       12:00:01.481 Checking status  9.
        //       12:00:01.981 Checking status 10.
        //       
        //       Destroying timer.


    }
        }
