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

namespace sccs
{
    internal static unsafe class Program
    {


        //these are imports that make the necesary WinAPI calls available
        #region pinvoke stuff
        private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll", ExactSpelling = true)]
        private static extern IntPtr GetAncestor(IntPtr hwnd, GetAncestor_Flags gaFlags);

        private enum GetAncestor_Flags { GetParent = 1, GetRoot = 2, GetRootOwner = 3 }

        [DllImport("user32.dll")]
        private static extern IntPtr GetLastActivePopup(IntPtr hWnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetTitleBarInfo(IntPtr hwnd, ref TITLEBARINFO pti);

        [StructLayout(LayoutKind.Sequential)]
        struct TITLEBARINFO
        {
            public const int CCHILDREN_TITLEBAR = 5;
            public uint cbSize;
            public RECT rcTitleBar;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = CCHILDREN_TITLEBAR + 1)]
            public uint[] rgstate;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT { public int Left, Top, Right, Bottom; }

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int smIndex);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, UIntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) { return SetWindowLongPtr64(hWnd, nIndex, dwNewLong); }
            else { return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToUInt32())); }
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, UIntPtr dwNewLong);

        #endregion

        //which window title is selected in the listbox
        private static string SelectedTitle = null;

        //initialize
       /* public Form1()
        {
            InitializeComponent();
            refresh();
        }

        //user selects an item in the listbox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTitle = listBox1.SelectedItem.ToString();
            button1.Enabled = true;
        }

        //user clicks 'Refresh' button
        private void button2_Click(object sender, EventArgs e)
        {
            refresh();
        }

        //user clicks 'Activate' button
        private void button1_Click(object sender, EventArgs e)
        {
            executeModeChange();
            refresh();
        }*/

        //Remove listbox selection state, Scan for windows, refresh contents of listbox
        /*public void refresh()
        {
            SelectedTitle = null;
            button1.Enabled = false;
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            EnumWindows(enumWindowProc, IntPtr.Zero);
            listBox1.EndUpdate();
        }*/

        //attempt to change the selected item to borderless windowed "mode"
        private static void executeModeChange()
        {
            //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
            IntPtr hWnd = FindWindow(null, SelectedTitle);
            if (hWnd == null) { 
                //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                Console.WriteLine("Failed to acquire window handle.\nPlease try again.");
            }
            //set the window to a borderless style
            const int GWL_STYLE = -16; //want to change the window style
            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
            IntPtr sult = SetWindowLongPtr(hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                Console.WriteLine("Unable to alter window style.\nSorry.");
                return;
            }
            //otherwise we need to resize and reposition the window to take up the full screen
            const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics(1);
            SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
        }

        //this is the procedure passed to the window enumerator so that it can identify the active windows, extract their titles, and add them to the listbox
        /*private static bool enumWindowProc(IntPtr hWnd, IntPtr lParam)
        {
            //ignore self
            if (hWnd == this.Handle) { return true; }

            //if the window has a titlebar title and can be alt-tabbed to then it's probably one we want to list
            int size = GetWindowTextLength(hWnd);
            if (size++ > 0 && isAltTabWindow(hWnd))
            {
                //grab the window's title
                StringBuilder sb = new StringBuilder(size);
                GetWindowText(hWnd, sb, size);
                //add it to the list
                listBox1.Items.Add(sb.ToString());
            }
            return true;
        }*/

        //This function checks to see if a window is alt-tabbable.
        //It sometimes returns false positives for tray icons and similar, but
        //it's never caused any real problems.
        private static bool isAltTabWindow(IntPtr hWnd)
        {
            //ignore windows that aren't visible
            if (!IsWindowVisible(hWnd)) { return false; }

            //check to see if this window is its own root owner
            //derived from R.Chen's method here:
            //https://blogs.msdn.microsoft.com/oldnewthing/20071008-00/?p=24863/
            IntPtr hWndWalk = IntPtr.Zero;
            IntPtr hWndTry = GetAncestor(hWnd, GetAncestor_Flags.GetRootOwner);
            while (hWndTry != hWndWalk)
            {
                hWndWalk = hWndTry;
                hWndTry = GetLastActivePopup(hWndWalk);
                if (IsWindowVisible(hWndTry)) { break; }
            }
            if (hWndWalk != hWnd) { return false; }

            //fetch the properties of the title bar
            TITLEBARINFO ti = new TITLEBARINFO();
            ti.cbSize = (uint)Marshal.SizeOf(ti);
            GetTitleBarInfo(hWnd, ref ti);
            //if the title bar is set to invisible then we don't want this window
            const uint STATE_SYSTEM_INVISIBLE = 0x8000;
            if (ti.rgstate[0] == STATE_SYSTEM_INVISIBLE) { return false; }

            //if the window style is the one used for a floating toolbar then we don't want this window
            const int GWL_EXSTYLE = -20;
            const int WS_EX_TOOLWINDOW = 0x80;
            if (GetWindowLong(hWnd, GWL_EXSTYLE) == WS_EX_TOOLWINDOW) { return false; }

            return true;
        }

        //adjust the emelements if the form gets resized
        /*private static void Form1_Resize(object sender, EventArgs e)
        {
            Control form = (Control)sender;
            //These magic numbers are just sizes for margins in the form.
            //It seemed needlessly pedantic to give them their own variables in this case.
            listBox1.Size = new Size(form.Size.Width - 40, form.Size.Height - 100);
            button1.Location = new Point(form.Size.Width - 180, form.Size.Height - 82);
            button2.Location = new Point(button2.Location.X, form.Size.Height - 82);
        }*/











        /// <summary>
        /// Window Styles.
        /// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
        /// </summary>
        [Flags()]
        private enum WindowStyles : uint
        {
            /// <summary>The window has a thin-line border.</summary>
            WS_BORDER = 0x800000,

            /// <summary>The window has a title bar (includes the WS_BORDER style).</summary>
            WS_CAPTION = 0xc00000,

            /// <summary>The window is a child window. A window with this style cannot have a menu bar. This style cannot be used with the WS_POPUP style.</summary>
            WS_CHILD = 0x40000000,

            /// <summary>Excludes the area occupied by child windows when drawing occurs within the parent window. This style is used when creating the parent window.</summary>
            WS_CLIPCHILDREN = 0x2000000,

            /// <summary>
            /// Clips child windows relative to each other; that is, when a particular child window receives a WM_PAINT message, the WS_CLIPSIBLINGS style clips all other overlapping child windows out of the region of the child window to be updated.
            /// If WS_CLIPSIBLINGS is not specified and child windows overlap, it is possible, when drawing within the client area of a child window, to draw within the client area of a neighboring child window.
            /// </summary>
            WS_CLIPSIBLINGS = 0x4000000,

            /// <summary>The window is initially disabled. A disabled window cannot receive input from the user. To change this after a window has been created, use the EnableWindow function.</summary>
            WS_DISABLED = 0x8000000,

            /// <summary>The window has a border of a style typically used with dialog boxes. A window with this style cannot have a title bar.</summary>
            WS_DLGFRAME = 0x400000,

            /// <summary>
            /// The window is the first control of a group of controls. The group consists of this first control and all controls defined after it, up to the next control with the WS_GROUP style.
            /// The first control in each group usually has the WS_TABSTOP style so that the user can move from group to group. The user can subsequently change the keyboard focus from one control in the group to the next control in the group by using the direction keys.
            /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
            /// </summary>
            WS_GROUP = 0x20000,

            /// <summary>The window has a horizontal scroll bar.</summary>
            WS_HSCROLL = 0x100000,

            /// <summary>The window is initially maximized.</summary>
            WS_MAXIMIZE = 0x1000000,

            /// <summary>The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
            WS_MAXIMIZEBOX = 0x10000,

            /// <summary>The window is initially minimized.</summary>
            WS_MINIMIZE = 0x20000000,

            /// <summary>The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style. The WS_SYSMENU style must also be specified.</summary>
            WS_MINIMIZEBOX = 0x20000,

            /// <summary>The window is an overlapped window. An overlapped window has a title bar and a border.</summary>
            WS_OVERLAPPED = 0x0,
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_MAXIMIZE)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW

            /// <summary>The window is an overlapped window.</summary>
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,

            /// <summary>The window is a pop-up window. This style cannot be used with the WS_CHILD style.</summary>
            WS_POPUP = 0x80000000u,

            /// <summary>The window is a pop-up window. The WS_CAPTION and WS_POPUPWINDOW styles must be combined to make the window menu visible.</summary>
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,

            /// <summary>The window has a sizing border.</summary>
            WS_SIZEFRAME = 0x40000,

            /// <summary>The window has a window menu on its title bar. The WS_CAPTION style must also be specified.</summary>
            WS_SYSMENU = 0x80000,

            /// <summary>
            /// The window is a control that can receive the keyboard focus when the user presses the TAB key.
            /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.  
            /// You can turn this style on and off to change dialog box navigation. To change this style after a window has been created, use the SetWindowLong function.
            /// For user-created windows and modeless dialogs to work with tab stops, alter the message loop to call the IsDialogMessage function.
            /// </summary>
            WS_TABSTOP = 0x10000,

            /// <summary>The window is initially visible. This style can be turned on and off by using the ShowWindow or SetWindowPos function.</summary>
            WS_VISIBLE = 0x10000000,

            /// <summary>The window has a vertical scroll bar.</summary>
            WS_VSCROLL = 0x200000
        }

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
        const UInt32 SWP_NOSIZE = 0x0001;
        const UInt32 SWP_NOMOVE = 0x0002;
        const UInt32 SWP_NOZORDER = 0x0004;
        const UInt32 SWP_NOREDRAW = 0x0008;
        const UInt32 SWP_NOACTIVATE = 0x0010;
        const UInt32 SWP_FRAMECHANGED = 0x0020;
        const UInt32 SWP_SHOWWINDOW = 0x0040;
        const UInt32 SWP_HIDEWINDOW = 0x0080;
        const UInt32 SWP_NOCOPYBITS = 0x0100;
        const UInt32 SWP_NOOWNERZORDER = 0x0200;
        const UInt32 SWP_NOSENDCHANGING = 0x0400;
        const UInt32 SWP_DRAWFRAME = SWP_FRAMECHANGED;
        const UInt32 SWP_NOREPOSITION = SWP_NOOWNERZORDER;

        const UInt32 SWP_DEFERERASE = 0x2000;
        const UInt32 SWP_ASYNCWINDOWPOS = 0x4000;

        /*
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int left, top, bottom, right;
        }*/



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











        [DllImport("user32.dll")]
        static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
        static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
        static readonly IntPtr HWND_TOP = new IntPtr(0);
        static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        /// <summary>
        /// Window handles (HWND) used for hWndInsertAfter
        /// </summary>
        public static class HWND
        {
            public static IntPtr
            NoTopMost = new IntPtr(-2),
            TopMost = new IntPtr(-1),
            Top = new IntPtr(0),
            Bottom = new IntPtr(1);
        }

        /// <summary>
        /// SetWindowPos Flags
        /// </summary>
        public static class SWP
        {
            public static readonly int
            NOSIZE = 0x0001,
            NOMOVE = 0x0002,
            NOZORDER = 0x0004,
            NOREDRAW = 0x0008,
            NOACTIVATE = 0x0010,
            DRAWFRAME = 0x0020,
            FRAMECHANGED = 0x0020,
            SHOWWINDOW = 0x0040,
            HIDEWINDOW = 0x0080,
            NOCOPYBITS = 0x0100,
            NOOWNERZORDER = 0x0200,
            NOREPOSITION = 0x0200,
            NOSENDCHANGING = 0x0400,
            DEFERERASE = 0x2000,
            ASYNCWINDOWPOS = 0x4000;
        }

        /// <summary>
        ///     Special window handles
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









        /*[Flags()]
        private enum SetWindowPosFlags : uint
        {
            /// <summary>If the calling thread and the thread that owns the window are attached to different input queues,
            /// the system posts the request to the thread that owns the window. This prevents the calling thread from
            /// blocking its execution while other threads process the request.</summary>
            /// <remarks>SWP_ASYNCWINDOWPOS</remarks>
            AsynchronousWindowPosition = 0x4000,
            /// <summary>Prevents generation of the WM_SYNCPAINT message.</summary>
            /// <remarks>SWP_DEFERERASE</remarks>
            DeferErase = 0x2000,
            /// <summary>Draws a frame (defined in the window's class description) around the window.</summary>
            /// <remarks>SWP_DRAWFRAME</remarks>
            DrawFrame = 0x0020,
            /// <summary>Applies new frame styles set using the SetWindowLong function. Sends a WM_NCCALCSIZE message to
            /// the window, even if the window's size is not being changed. If this flag is not specified, WM_NCCALCSIZE
            /// is sent only when the window's size is being changed.</summary>
            /// <remarks>SWP_FRAMECHANGED</remarks>
            FrameChanged = 0x0020,
            /// <summary>Hides the window.</summary>
            /// <remarks>SWP_HIDEWINDOW</remarks>
            HideWindow = 0x0080,
            /// <summary>Does not activate the window. If this flag is not set, the window is activated and moved to the
            /// top of either the topmost or non-topmost group (depending on the setting of the hWndInsertAfter
            /// parameter).</summary>
            /// <remarks>SWP_NOACTIVATE</remarks>
            DoNotActivate = 0x0010,
            /// <summary>Discards the entire contents of the client area. If this flag is not specified, the valid
            /// contents of the client area are saved and copied back into the client area after the window is sized or
            /// repositioned.</summary>
            /// <remarks>SWP_NOCOPYBITS</remarks>
            DoNotCopyBits = 0x0100,
            /// <summary>Retains the current position (ignores X and Y parameters).</summary>
            /// <remarks>SWP_NOMOVE</remarks>
            IgnoreMove = 0x0002,
            /// <summary>Does not change the owner window's position in the Z order.</summary>
            /// <remarks>SWP_NOOWNERZORDER</remarks>
            DoNotChangeOwnerZOrder = 0x0200,
            /// <summary>Does not redraw changes. If this flag is set, no repainting of any kind occurs. This applies to
            /// the client area, the nonclient area (including the title bar and scroll bars), and any part of the parent
            /// window uncovered as a result of the window being moved. When this flag is set, the application must
            /// explicitly invalidate or redraw any parts of the window and parent window that need redrawing.</summary>
            /// <remarks>SWP_NOREDRAW</remarks>
            DoNotRedraw = 0x0008,
            /// <summary>Same as the SWP_NOOWNERZORDER flag.</summary>
            /// <remarks>SWP_NOREPOSITION</remarks>
            DoNotReposition = 0x0200,
            /// <summary>Prevents the window from receiving the WM_WINDOWPOSCHANGING message.</summary>
            /// <remarks>SWP_NOSENDCHANGING</remarks>
            DoNotSendChangingEvent = 0x0400,
            /// <summary>Retains the current size (ignores the cx and cy parameters).</summary>
            /// <remarks>SWP_NOSIZE</remarks>
            IgnoreResize = 0x0001,
            /// <summary>Retains the current Z order (ignores the hWndInsertAfter parameter).</summary>
            /// <remarks>SWP_NOZORDER</remarks>
            IgnoreZOrder = 0x0004,
            /// <summary>Displays the window.</summary>
            /// <remarks>SWP_SHOWWINDOW</remarks>
            ShowWindow = 0x0040,
        }*/
        /*Flags]
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
        }*/


        static Vector3 saveplayerposition = Vector3.Zero;
        static Matrix saveplayerfinalRotationMatrix = Matrix.Identity;

        static Matrix saveplayerrotatingMatrixForPelvis = Matrix.Identity;

        public static Vector3 saveplayermovePos = Vector3.Zero;
        public static float saveplayerrotx = 0;
        public static float saveplayerroty = 0;
        public static float saveplayerrotz = 0;
        /*
        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, UIntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) { return SetWindowLongPtr64(hWnd, nIndex, dwNewLong); }
            else { return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToUInt32())); }
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, UIntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int smIndex);
        */
        /// <summary>
        ///     Special window handles
        /// </summary>
        /*public enum SpecialWindowHandles
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
        }*/

        /*[Flags]
        public enum SetWindowPosFlags //: uint
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

            /*
             SWP_FRAMECHANGED = 0x0020,
             SWP_NOMOVE = 0x0002,
             SWP_NOSIZE = 0x0001,
             SWP_NOZORDER = 0x0004,
             SWP_NOOWNERZORDER = 0x0200,

            // ReSharper restore InconsistentNaming
        }*/



        /*
        [DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);
        */
        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);


        public static int usetypeofvoxel = 0; //0.000000001f //0.0f
        public static int _useOculusRift = 0;

        public static int createconsole = 1; //put app solution in console mode instead of window mode. // hide mode == 1 // showmode == 0

        static int getnewbitmaponce = 0;
        static int bmpstride;
        //static DwmSharedSurface sometest;
        //static GraphicsCapture somegcap;

        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);

        [DllImport("gdi32.dll")]
        static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);

        static SwapChain1 swapChain1;

        static Texture2D texture2d;
        public static string capturedwindowname = "";

        public static int lastscreencapturetype = 0;
        public static int changedscreencapturetype = 0;
        public static int screencapturetype = 0;

        //public static int usesharpdxscreencapture = 0;


        public static RenderForm somerenderform;
        public static Form1 someform;
        static byte* srcPointer;
        static byte* dstPointer;


        public static IntPtr consoleHandle;

        public static int usethirdpersonview = 0;
        public static float offsetthirdpersonview = 0.35f;//at or over 1 to get a decent ootb working 3rdpersonview.

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

        /*
        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        */

        /*
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, UInt32 uFlags);
        */







        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

   
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
        //[DllImport(@"user32.dll")]
        //static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);



        /*public static void makeBorderless()
        {
            // Get the handle of self
            IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
            RECT rect;
            // Get the rectangle of self (Size)
            GetWindowRect(window, out rect);
            // Get the handle of the desktop
            IntPtr HWND_DESKTOP = GetDesktopWindow();
            // Attempt to get the location of self compared to desktop
            MapWindowPoints(HWND_DESKTOP, window, ref rect, 2);
            // update self
            SetWindowLong(window, GWL_STYLE, WS_SYSMENU);
            // rect.left rect.top should work but they're returning negative values for me. I probably messed up
            SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);
            DrawMenuBar(window);
        }*/
        const int SwHide = 0;
        const int SwShow = 5;


        /*
        public static void makePanelBorderless()
        {
            // Get the handle of self
            IntPtr window = hWndOriginalParent;// FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
            RECT rect;
            // Get the rectangle of self (Size)
            GetWindowRect(window, out rect);
            // Get the handle of the desktop
            IntPtr HWND_DESKTOP = GetDesktopWindow();
            // Attempt to get the location of self compared to desktop
            MapWindowPoints(HWND_DESKTOP, window, ref rect, 2);
            // update self
            SetWindowLong(window, GWL_STYLE, WS_SYSMENU);
            // rect.left rect.top should work but they're returning negative values for me. I probably messed up
            SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);
            DrawMenuBar(window);
        }
        */






        static System.Drawing.Imaging.BitmapData bmpData1;
        static DataBox dataBox1;

        static int memoryBitmapStride;
        static int columns;
        static int rows;
        static IntPtr interptr1;






        static ShaderResourceView shaderResourceView;
        static int startmainthread = 0;
        static int bitmapcounter = 0;
        static int textureresetswtc = 0;
        static System.Drawing.Bitmap _bitmap;

        static System.Drawing.Bitmap _bitmap1;
        static System.Drawing.Rectangle boundsRect;
        static System.Drawing.Imaging.BitmapData bmpData;
        static int _bytesTotal;
        static Texture2DDescription _textureDescription;
        static byte[] _textureByteArray;
        static Texture2D _texture2d;

        static ShaderResourceView lastshaderresourceview;
        static System.Drawing.Bitmap lastbitmap;

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


        //https://stackoverflow.com/questions/5836176/docking-window-inside-another-window
        static private void dockIt()
        {

            //Panel somepanel = new System.Windows.Controls.Panel();



            //if (hWndDocked != IntPtr.Zero) //don't do anything if there's already a window docked.
            //    return;

            //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);





            //hWndParent = someform.Handle;// sccsr14sc.Form1.theHandle;// IntPtr.Zero;

            /*pDocked = Process.Start(@"notepad");
            while (hWndDocked == IntPtr.Zero)
            {
                pDocked.WaitForInputIdle(1000); //wait for the window to be ready for input;
                pDocked.Refresh();              //update process info
                if (pDocked.HasExited)
                {
                    return; //abort if the process finished before we got a handle.
                }
                hWndDocked = pDocked.MainWindowHandle;  //cache the window handle
            }*/

            //hWndDocked = hWndParent;// sccsr14sc.Form1.thepanel.Handle;// form.Handle;


            //Windows API call to change the parent of the target window.
            //It returns the hWnd of the window's parent prior to this call.


            hWndDocked = consoleHandle;
            hWndOriginalParent = SetParent(consoleHandle, sccsr14sc.Form1.thepanel.Handle);
            //hWndOriginalParent = SetParent(consoleHandle, someform.Handle);// sccsr14sc.Form1.thepanel.Handle);// sccsr14sc.Form1.thepanel.Handle);
            //hWndOriginalParent = SetParent(someform.Handle, consoleHandle);// sccsr14sc.Form1.thepanel.Handle);

            //Wire up the event to keep the window sized to match the control
            sccsr14sc.Form1.thepanel.SizeChanged += new EventHandler(thepanel_Resize);

            //sccsr14sc.Form1.size



            //SetWindowLong(someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT));
            //SetLayeredWindowAttributes(someform.Handle, 0, 255, LWA_ALPHA);



            //Perform an initial call to set the size.
            //thepanel_Resize(new Object(), new EventArgs());

            thepanel_Resize(new Object(), new EventArgs());
        }

        static private void undockIt()
        {
            //Restores the application to it's original parent.
            SetParent(someform.Handle, hWndOriginalParent);
        }

        static private void thepanel_Resize(object sender, EventArgs e)
        {

            //MessageBox((IntPtr)0, "thepanel_Resize", "scmsg", 0);
            //Change the docked windows size to match its parent's size. 
            MoveWindow(consoleHandle, 0, 0, sccsr14sc.Form1.thepanel.Width, sccsr14sc.Form1.thepanel.Height, true);
        }







        //public static Panel thepanel;


        //https://www.unknowncheats.me/forum/c/62019-c-non-hooked-external-directx-overlay.html

        private static Margins marg;

        internal struct Margins
        {
            public int Left, Right, Top, Bottom;
        }

        /*[DllImport("user32.dll", SetLastError = true)]
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);*/


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
        /*
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);*/
        public static IntPtr vewindowsfoundedz;

        //private delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);
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





        [DllImport("user32.dll")]
        static extern int ShowCursor(bool bShow);


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




                    if (updatescript.captureMethod != null)
                    {
                        updatescript.captureMethod.StopCapture();// (sccsr14sc.Form1.theHandle, updatescript.device, factoryy); //

                        updatescript.captureMethod.Dispose();
                        updatescript.captureMethod = null;
                    }

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

                    if (_texture2d != null)
                    {
                        _texture2d.Dispose();
                        _texture2d = null;
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
                    GC.Collect();
                    changedscreencapturetype = 0;
                    isexitingprogram = 1;
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

        static Thread _mainThread;
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


        static int hasresettedcapture = 0;
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
            /*inputsim = new InputSimulator();
            mousesim = new MouseSimulator(inputsim);
            keyboardsim = new KeyboardSimulator(inputsim);

            keynmouseinput = new DInput();
            keynmouseinput.Initialize(Program.config, SCGLOBALSACCESSORS.SCCONSOLECORE.handle); //sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle //thehandle
            */

            keynmouseinput = new keyboardinput();
        }



        static sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS;
        static BackgroundWorker backgroundWorker;


        [STAThread]
        static void Main()
        {



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





                    /*
                    if (createinputsswtc == 0)
                    {
                        if (sccsr14sc.Form1.someform != null)
                        {
                            if (sccsr14sc.Form1.theHandle != IntPtr.Zero)
                            {
                                var refreshDXEngineAction = new Action(delegate
                                {
                                    //sccs.scgraphics.scupdate.createinputs(sccsr14sc.Form1.theHandle);
                                    createinputs(sccsr14sc.Form1.theHandle);

                                    //someform = new RenderForm("sccsr14");
                                    /*someform.Size = new System.Drawing.Size(1920, 1080);
                                    someform.FormBorderStyle = FormBorderStyle.None;
                                    someform.WindowState = FormWindowState.Maximized;
                                    
                                    //sccsr14sc.Form1.someform.deactivatecursor();

                                    /*var _hwndSource = HwndSource.FromHwnd(someform.Handle);
                                    if (_hwndSource != null)
                                        _hwndSource.AddHook(WndProc);


                                    SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                                        SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                    //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                                    //    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                    SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
                                    // SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;
                                });
                                System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                //sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                createinputsswtc = 1;
                                //MessageBox((IntPtr)0, "createinputs ", "scmsg", 0);
                            }
                        }
                    }
                    */

                    /*if (createinputs == 1)
                    {

                        if (Program.vewindowsfoundedz != IntPtr.Zero)
                        {

                            var refreshDXEngineAction = new Action(delegate
                            {
                                if (sccsr14sc.Form1.someform.WindowState != FormWindowState.Maximized)
                                {
                                    //MessageBox((IntPtr)0, "wstate " + sccsr14sc.Form1.someform.WindowState, "scmsg", 0);
                                    //MessageBox((IntPtr)0, "wstate " + sccsr14sc.Form1.someform.WindowState, "scmsg", 0);


                                    sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                    //MessageBox((IntPtr)0, "wstate " + sccsr14sc.Form1.someform.WindowState, "scmsg", 0);

                                    createinputs = 2;
                                }
                                else
                                {
                                    //MessageBox((IntPtr)0, "wstate " + sccsr14sc.Form1.someform.WindowState, "scmsg", 0);
                                }
                            });
                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                        }
                    }*/






                    if (sccsr14sc.Form1.someform != null)
                    {
                        if (progcanpause == 1)
                        {
                            var refreshDXEngineAction = new Action(delegate
                            {
                                //Console.WriteLine("thebutton Visible");
                                //stackoverflow 661561 for invoking panel changes.
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
                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);

                            /*if (sccsr14sc.Form1.checkbox.Visible == false)
                            {
                               }*/
                            progcanpause = 0;
                        }












                        if (panelchangedswtc == 0)
                        {
                            panelchangedwatch.Stop();
                            panelchangedwatch.Reset();
                            panelchangedwatch.Restart();

                            counterpanelchanged = 0;
                            panelchangedswtc = 1;
                        }

                        if (panelchangedwatch.Elapsed.Milliseconds >= 10 && counterpanelchanged >= counterpanelchangedmax)
                        {
                            if (sccsr14sc.Form1.haspressedf9 == 1)
                            {
                                if (sccsr14sc.Form1.checkbox1.Visible == false)
                                {
                                    sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.checkbox1.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.checkbox1.Visible == true)
                                {
                                    sccsr14sc.Form1.checkbox1.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.checkbox1.Visible = false;
                                    });
                                }

                                if (sccsr14sc.Form1.someform.labeltext0.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.labeltext0.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.labeltext0.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.labeltext0.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.labeltext0.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.labeltext0.Visible = false;
                                    });
                                }






                                if (sccsr14sc.Form1.someform.labeltext1.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.labeltext1.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.labeltext1.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.labeltext1.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.labeltext1.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.labeltext1.Visible = false;
                                    });
                                }




                                if (sccsr14sc.Form1.someform.labeltext2.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.labeltext2.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.labeltext2.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.labeltext2.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.labeltext2.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.labeltext2.Visible = false;
                                    });
                                }


                                if (sccsr14sc.Form1.someform.labeltext3.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.labeltext3.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.labeltext3.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.labeltext3.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.labeltext3.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.labeltext3.Visible = false;
                                    });
                                }


                                if (sccsr14sc.Form1.someform.numericUpDown01.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown01.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown01.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.numericUpDown01.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown01.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown01.Visible = false;
                                    });
                                }





                                if (sccsr14sc.Form1.someform.numericUpDown02.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown02.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown02.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.numericUpDown02.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown02.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown02.Visible = false;
                                    });
                                }





                                if (sccsr14sc.Form1.someform.numericUpDown03.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown03.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown03.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.numericUpDown03.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown03.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown03.Visible = false;
                                    });
                                }





                                if (sccsr14sc.Form1.someform.numericUpDown04.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown04.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown04.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.numericUpDown04.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown04.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown04.Visible = false;
                                    });
                                }





                                if (sccsr14sc.Form1.someform.numericUpDown05.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown05.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown05.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.numericUpDown05.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown05.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown05.Visible = false;
                                    });
                                }





                                if (sccsr14sc.Form1.someform.numericUpDown06.Visible == false)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown06.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown06.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.someform.numericUpDown06.Visible == true)
                                {
                                    sccsr14sc.Form1.someform.numericUpDown06.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.someform.numericUpDown06.Visible = false;
                                    });
                                }













                                /*
                                if (sccsr14sc.Form1.checkbox2.Visible == false)
                                {
                                    sccsr14sc.Form1.checkbox2.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.checkbox2.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.checkbox2.Visible == true)
                                {
                                    sccsr14sc.Form1.checkbox2.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.checkbox2.Visible = false;
                                    });
                                }*/


                                if (sccsr14sc.Form1.comboboxcapturelist.Visible == false)
                                {
                                    sccsr14sc.Form1.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.comboboxcapturelist.Visible = true;
                                    });
                                }
                                else if (sccsr14sc.Form1.comboboxcapturelist.Visible == true)
                                {
                                    sccsr14sc.Form1.comboboxcapturelist.Invoke((MethodInvoker)delegate
                                    {
                                        sccsr14sc.Form1.comboboxcapturelist.Visible = false;
                                    });
                                }

                                /*
                                if (sccsr14sc.Form1.thepanel.Visible == false)
                                {
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        //Console.WriteLine("thebutton Visible");
                                        //stackoverflow 661561 for invoking panel changes.
                                        sccsr14sc.Form1.thepanel.Invoke((MethodInvoker)delegate
                                        {
                                            sccsr14sc.Form1.thepanel.Visible = true;
                                        });

                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                }
                                else if (sccsr14sc.Form1.thepanel.Visible == true)
                                {
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        //Console.WriteLine("thebutton Visible");
                                        //stackoverflow 661561 for invoking panel changes.
                                        sccsr14sc.Form1.thepanel.Invoke((MethodInvoker)delegate
                                        {
                                            sccsr14sc.Form1.thepanel.Visible = false;
                                        });

                                        //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);

                                }
                                */


                                if (sccsr14sc.Form1.trackbar.Visible == false)
                                {
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        //Console.WriteLine("thebutton Visible");
                                        //stackoverflow 661561 for invoking panel changes.
                                        sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                        {
                                            sccsr14sc.Form1.trackbar.Visible = true;
                                        });

                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                }
                                else if (sccsr14sc.Form1.trackbar.Visible == true)
                                {
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        //Console.WriteLine("thebutton Visible");
                                        //stackoverflow 661561 for invoking panel changes.
                                        sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                        {
                                            sccsr14sc.Form1.trackbar.Visible = false;
                                        });

                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                }



                                if (sccsr14sc.Form1.checkedlistbox.Visible == false)
                                {
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        //Console.WriteLine("thebutton Visible");
                                        //stackoverflow 661561 for invoking panel changes.
                                        sccsr14sc.Form1.checkedlistbox.Invoke((MethodInvoker)delegate
                                        {
                                            sccsr14sc.Form1.checkedlistbox.Visible = true;
                                        });

                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                }

                                else if (sccsr14sc.Form1.checkedlistbox.Visible == true)
                                {
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        //Console.WriteLine("thebutton Visible");
                                        //stackoverflow 661561 for invoking panel changes.
                                        sccsr14sc.Form1.checkedlistbox.Invoke((MethodInvoker)delegate
                                        {
                                            sccsr14sc.Form1.checkedlistbox.Visible = false;
                                        });

                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                }



                                sccsr14sc.Form1.haspressedf9 = 0;
                            }
                            /*else if (sccsr14sc.Form1.someform.haspressedf9 == 1)
                            {


                            }*/

                            sccsr14sc.Form1.someform.haspressedsomekeyboardkey = 0;
                            counterpanelchanged = 0;

                            panelchangedswtc = 0;
                            //sccsr14sc.Form1.someform.haspressedf9 = 2;
                            panelchangedwatch.Stop();



                        }
                        counterpanelchanged++;
                    }











                    if (sccsr14sc.Form1.someform != null && updatescript != null)
                    {
                        if (heightmaptrackbarchangedswtc == 0)
                        {
                            heightmaptrackbarchangedwatch.Stop();
                            heightmaptrackbarchangedwatch.Reset();
                            heightmaptrackbarchangedwatch.Restart();
                            heightmaptrackbarchangedswtc = 1;
                        }


                        if (hasresettedcapture == 1)
                        {
                            //this.trackBar1.Value = -1000;

                            var refreshDXEngineAction = new Action(delegate
                            {
                                //Console.WriteLine("thebutton Visible");
                                //stackoverflow 661561 for invoking panel changes.

                                //Console.WriteLine("thebutton Visible");
                                //stackoverflow 661561 for invoking panel changes.

                                sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                {
                                    /*if (sccsr14sc.Form1.trackbar.Value > sccsr14sc.Form1.trackbar.Minimum)
                                    {
                                        sccsr14sc.Form1.trackbar.Value -= sccsr14sc.Form1.trackbar.TickFrequency;
                                    }*/
                                    //sccsr14sc.Form1.trackbar.Value = 0; //-1000
                                    sccsr14sc.Form1.someform.heightmapvalue = sccsr14sc.Form1.trackbar.Value;
                                });

                                //sccsr14sc.Form1.someform.haspressedf9 = 2;
                            });
                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);


                            hasresettedcapture = 0;
                        }




                        if (updatescript.haspressedheightmapvaluedecrease == 1 || updatescript.haspressedheightmapvalueincrease == 1)
                        {
                            if (updatescript.haspressedheightmapvaluedecrease == 1)
                            {
                                if (panelchangedwatch.Elapsed.Ticks >= 1)
                                {

                                    if (sccsr14sc.Form1.someform.heightmapvalue > sccsr14sc.Form1.someform.heightmapvaluemin + (sccsr14sc.Form1.someform.heightmapvaluetickfreq * 1))
                                    {
                                        var refreshDXEngineAction = new Action(delegate
                                        {
                                            //Console.WriteLine("thebutton Visible");
                                            //stackoverflow 661561 for invoking panel changes.

                                            //Console.WriteLine("thebutton Visible");
                                            //stackoverflow 661561 for invoking panel changes.

                                            sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                            {
                                                /*if (sccsr14sc.Form1.trackbar.Value > sccsr14sc.Form1.trackbar.Minimum)
                                                {
                                                    sccsr14sc.Form1.trackbar.Value -= sccsr14sc.Form1.trackbar.TickFrequency;
                                                }*/
                                                sccsr14sc.Form1.trackbar.Value -= sccsr14sc.Form1.trackbar.TickFrequency;
                                            });

                                            //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                        });
                                        System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                    }
                                    else
                                    {
                                        sccsr14sc.Form1.someform.heightmapvalue = sccsr14sc.Form1.someform.heightmapvaluemin + (sccsr14sc.Form1.someform.heightmapvaluetickfreq * 1);
                                    }
                                    updatescript.haspressedheightmapvaluedecrease = 0;
                                    heightmaptrackbarchangedswtc = 0;
                                }

                            }

                            if (updatescript.haspressedheightmapvalueincrease == 1)
                            {
                                if (panelchangedwatch.Elapsed.Milliseconds >= 10)
                                {
                                    if (sccsr14sc.Form1.someform.heightmapvalue < sccsr14sc.Form1.someform.heightmapvaluemax - (sccsr14sc.Form1.someform.heightmapvaluetickfreq * 1))
                                    {
                                        var refreshDXEngineAction = new Action(delegate
                                        {
                                            //Console.WriteLine("thebutton Visible");
                                            //stackoverflow 661561 for invoking panel changes.

                                            //Console.WriteLine("thebutton Visible");
                                            //stackoverflow 661561 for invoking panel changes.

                                            sccsr14sc.Form1.trackbar.Invoke((MethodInvoker)delegate
                                            {
                                                /*if (sccsr14sc.Form1.trackbar.Value > sccsr14sc.Form1.trackbar.Minimum)
                                                {
                                                    sccsr14sc.Form1.trackbar.Value -= sccsr14sc.Form1.trackbar.TickFrequency;
                                                }*/
                                                sccsr14sc.Form1.trackbar.Value += sccsr14sc.Form1.trackbar.TickFrequency;
                                            });

                                            //sccsr14sc.Form1.someform.haspressedf9 = 2;
                                        });
                                        System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                    }
                                    else
                                    {
                                        sccsr14sc.Form1.someform.heightmapvalue = (sccsr14sc.Form1.someform.heightmapvaluemax - (sccsr14sc.Form1.someform.heightmapvaluetickfreq * 1));
                                    }
                                    updatescript.haspressedheightmapvalueincrease = 0;
                                    heightmaptrackbarchangedswtc = 0;
                                }
                            }
                        }
                    }




























                    if (sccsr14sc.Form1.someform != null && updatescript != null)
                    {
                        /*if (heightmaptrackbarchangedswtc == 0)
                        {
                            heightmaptrackbarchangedwatch.Stop();
                            heightmaptrackbarchangedwatch.Reset();
                            heightmaptrackbarchangedwatch.Restart();
                            heightmaptrackbarchangedswtc = 1;
                        }

                        hasgot0*/
                    }






                    /*if (sccsr14sc.Form1.someform!= null)
                    {
                        Console.WriteLine(sccsr14sc.Form1.someform.heightmapvalue);
                    }*/











                    Thread.Sleep(1);
                }


            };

            backgroundWorker.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs args)
            {
                Console.WriteLine("worker ended prematurely ");
                MessageBox((IntPtr)0, "worker ended prematurely", "scmsg", 0);
            };

            backgroundWorker.RunWorkerAsync();




            //System.Windows.Forms.Cursor.Hide();
            //MessageBox((IntPtr)0, "form initiated0", "scmsg", 0);
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

            someform = new Form1();




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

            //thepanel = new System.Windows.Controls.Panel();

            /*var _hwndSource = HwndSource.FromHwnd(sccsr14sc.Form1.theHandle);
            if (_hwndSource != null)
                _hwndSource.AddHook(WndProc);

            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                SharpDX.RawInput.DeviceFlags.None, sccsr14sc.Form1.theHandle);
            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                SharpDX.RawInput.DeviceFlags.None, sccsr14sc.Form1.theHandle);
            SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;*/

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


                //mainreceivedmessages[0]._someData[0] = new object();


                //VOICE RECOGNITION. NOT IMPLEMENTED YET.
                /*if (i == MaxSizeMainObject - 1)
                {
                    mainreceivedmessages[i]._someData[0] = _keyboard_input._KeyboardState;
                    mainreceivedmessages[i]._voRecSwtc = 1;
                }*/
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

            //SCGLOBALSACCESSORS AND GLOBALS CREATOR
            /*sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages);
            if (SCGLOBALSACCESSORS == null)
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
            }
            else
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
            }*/
            //borderlessconsole console_ = new borderlessconsole();
            //SCGLOBALSACCESSORS AND GLOBALS CREATOR

            //var somewindow = new WindowInteropHelper(sccsr14sc.Form1);
            //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);
            //consoleHandle = somewindow.EnsureHandle();



            //keynmouseinput.IsMouseButtonDown
            //mousesim.


            int screencaptureresultswtc = 0;


            int lastwindowwidth = 0;
            int lastwindowheight = 0;

            int initform = 0;


            /*inputsim = new InputSimulator();
            mousesim = new MouseSimulator(inputsim);
            keyboardsim = new KeyboardSimulator(inputsim);
            */
            /*keynmouseinput = new DInput();
            keynmouseinput.Initialize(config, sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle);
            */

            //SetWindowLong(SCGLOBALSACCESSORS.SCCONSOLECORE.handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(SCGLOBALSACCESSORS.SCCONSOLECORE.handle, GWL_EXSTYLE) | WS_EX_TOPMOST )); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
            //SetLayeredWindowAttributes(SCGLOBALSACCESSORS.SCCONSOLECORE.handle, 0, 255, LWA_ALPHA);





            RenderLoop.Run(someform, () =>
            {











                if (createinputsswtc == 0)
                {
                    if (sccsr14sc.Form1.someform != null)
                    {
                        if (sccsr14sc.Form1.theHandle != IntPtr.Zero)
                        {

                            SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages, createconsole);
                            if (SCGLOBALSACCESSORS == null)
                            {
                                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
                            }
                            else
                            {
                                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
                            }
                            createinputs(IntPtr.Zero);








                            consoleHandle = someform.Handle;// sccsr14sc.Form1.theHandle;
                            /*
                            var refreshDXEngineAction = new Action(delegate
                            {
                                //sccs.scgraphics.scupdate.createinputs(sccsr14sc.Form1.theHandle);
                                //createinputs(sccsr14sc.Form1.theHandle);

                                //someform = new RenderForm("sccsr14");
                                /*someform.Size = new System.Drawing.Size(1920, 1080);
                                someform.FormBorderStyle = FormBorderStyle.None;
                                someform.WindowState = FormWindowState.Maximized;

                                //sccsr14sc.Form1.someform.deactivatecursor();

                                /*var _hwndSource = HwndSource.FromHwnd(someform.Handle);
                                if (_hwndSource != null)
                                    _hwndSource.AddHook(WndProc);


                                SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                                    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                                //    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
                                // SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;
                            });
                            System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                            *///sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                            createinputsswtc = 1;
                            //MessageBox((IntPtr)0, "createinputs ", "scmsg", 0);
                        }
                    }
                }




                if (keynmouseinput != null)
                {

                    /*
                    var refreshDXEngineAction = new Action(delegate
                    {

                    });
                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                    */
                    keynmouseinput.ReadKeyboard();
                }



                // draw it
                //device.ImmediateContext.Draw(4, 0);
                //swapChain1.Present(1, PresentFlags.None, new PresentParameters());




                //mainthreadloop:


                /*
                if (updatescript != null)
                {
                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                    {

                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                        {
                            if (updatescript.scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (updatescript.scgraphicssecpackagemessage.scjittertasks.Length > 0)
                                {
                                    if (updatescript.scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        //if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);

                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);


                                            updatescript.scgraphicssecpackagemessage.scjittertasks = sccsjittertasks;
                                            sccsjittertasks = updatescript.StartRender(null,updatescript.scgraphicssecpackagemessage.scjittertasks);

                                            sccsjittertasks = updatescript.scgraphicssecpackagemessage.scgraphicssec.workonshaders(updatescript.scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                */
                if (initthread == 0)
                {
                    _mainThread = new Thread((tester0000) =>
                    {
                    //sccs.Program.MessageBox((IntPtr)0, "Form1", "scmsg", 0);



                    _thread_main_loop:



                        /*if (keynmouseinput != null)
                        {
                            keynmouseinput.Frame();
                        }*/


                        //WinCursors.HideCursor();
                        /*if (someform != null)
                        {
                            if (someform.Handle != IntPtr.Zero)
                            {
                                //consoleHandle = someform.Handle;

                                //sccsr14sc.Form1.theHandle;
                                //consoleHandle = scconsolecore.handle;

                                /*inputsim = new InputSimulator();
                                mousesim = new MouseSimulator(inputsim);
                                keyboardsim = new KeyboardSimulator(inputsim);

                                keynmouseinput = new DInput();
                                keynmouseinput.Initialize(config, scconsolecore.handle);

                                initform = 2;
                            }
                        }*/


                        /*if (Console.WindowWidth != lastwindowwidth || Console.WindowHeight  != lastwindowheight)
                        {
                            keynmouseinput = new DInput();
                            keynmouseinput.Initialize(config, consoleHandle);
                        }
                        keynmouseinput.Frame();
                        lastwindowwidth = Console.WindowWidth;
                        lastwindowheight = Console.WindowHeight;*/




                        if (initform == 1 && sccsr14sc.Form1.initForm == 1)
                        {
                            /*
                            keynmouseinput = new DInput();
                            keynmouseinput.Initialize(config, sccsr14sc.Form1.someform.SCGLOBALSACCESSORS.SCCONSOLECORE.handle);
                            */
                            /*keynmouseinput = new DInput();
                            keynmouseinput.Initialize(config, sccsr14sc.Form1.theHandle); //scconsolecore.handle
                            */

                            /*var _hwndSource = HwndSource.FromHwnd(sccsr14sc.Form1.theHandle);
                            if (_hwndSource != null)
                                _hwndSource.AddHook(WndProc);

                            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                                SharpDX.RawInput.DeviceFlags.None, sccsr14sc.Form1.theHandle);
                            SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                                SharpDX.RawInput.DeviceFlags.None, sccsr14sc.Form1.theHandle);
                            SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
                            SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;*/



                            // sccs.Program.MessageBox((IntPtr)0, "sccsr14sc.Form1.initForm ", "scmsg", 0);
                            //consoleHandle = sccsr14sc.Form1.theHandle;

                            //System.Windows.Forms.Cursor.Hide();
                            //ShowCursor(false);


                            /*if (saveplayerposition != )
                            {

                            }*/

                            if (screencapturetype == 0)
                            {
                                /*if (updatescript!= null)
                                {
                                    updatescript.ca
                                }*/

                                updatescript = new scupdate(new GraphicsCapture());

                                scupdate.OFFSETPOS = saveplayerposition;
                                scupdate.finalRotationMatrix = saveplayerfinalRotationMatrix;
                                scupdate.rotatingMatrix = saveplayerrotatingMatrixForPelvis;
                                scupdate.movePos = saveplayermovePos;
                                scupdate.rotx = saveplayerrotx;
                                scupdate.roty = saveplayerroty;
                                scupdate.rotz = saveplayerrotz;


                            }
                            else if (screencapturetype == 1)
                            {

                                updatescript = new scupdate(new DwmSharedSurface());
                                scupdate.OFFSETPOS = saveplayerposition;
                                scupdate.finalRotationMatrix = saveplayerfinalRotationMatrix;
                                scupdate.rotatingMatrix = saveplayerrotatingMatrixForPelvis;
                                scupdate.movePos = saveplayermovePos;
                                scupdate.rotx = saveplayerrotx;
                                scupdate.roty = saveplayerroty;
                                scupdate.rotz = saveplayerrotz;


                            }
                            else if (screencapturetype == 2)
                            {

                                updatescript = new scupdate(null);
                                scupdate.OFFSETPOS = saveplayerposition;
                                scupdate.finalRotationMatrix = saveplayerfinalRotationMatrix;
                                scupdate.rotatingMatrix = saveplayerrotatingMatrixForPelvis;
                                scupdate.movePos = saveplayermovePos;
                                scupdate.rotx = saveplayerrotx;
                                scupdate.roty = saveplayerroty;
                                scupdate.rotz = saveplayerrotz;

                            }



                            //sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, sccs.scgraphics.scdirectx.D3D.device);
                            //sccs.Program.MessageBox((IntPtr)0, "scupdate initiated", "scmsg", 0);

                            if (usejitterphysics == 1)
                            {
                                /*jitter_sc = new jitter_sc[physicsengineinstancex * physicsengineinstancey * physicsengineinstancez];
                                sccsjittertasks = new scmessageobjectjitter[physicsengineinstancex * physicsengineinstancey * physicsengineinstancez][];

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
                                            sccsjittertasks[indexer00] = new scmessageobjectjitter[worldwidth * worldheight * worlddepth];

                                            for (int x = 0; x < worldwidth; x++)
                                            {
                                                for (int y = 0; y < worldheight; y++)
                                                {
                                                    for (int z = 0; z < worlddepth; z++)
                                                    {
                                                        var indexer01 = x + worldwidth * (y + worldheight * z);
                                                        sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
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
                                sccsjittertasks = updatescript.init_update_variables(sccsjittertasks, config); //, SCGLOBALSACCESSORS.SCCONSOLEWRITER*/
                            }
                            else if (usejitterphysics == 0)
                            {
                                sccsjittertasks = new scmessageobjectjitter[1][];
                                sccsjittertasks[0] = new scmessageobjectjitter[1];
                                sccsjittertasks[0][0] = new scmessageobjectjitter();
                                /*for (int xx = 0; xx < physicsengineinstancex; xx++)
                                {
                                    for (int yy = 0; yy < physicsengineinstancey; yy++)
                                    {
                                        for (int zz = 0; zz < physicsengineinstancez; zz++)
                                        {
                                            var indexer00 = xx + physicsengineinstancex * (yy + physicsengineinstancey * zz);
                                            //_jitter_physics[indexer00] = DoSpecialThing();
                                            sccsjittertasks[indexer00] = new scmessageobjectjitter[worldwidth * worldheight * worlddepth];

                                            for (int x = 0; x < worldwidth; x++)
                                            {
                                                for (int y = 0; y < worldheight; y++)
                                                {
                                                    for (int z = 0; z < worlddepth; z++)
                                                    {
                                                        var indexer01 = x + worldwidth * (y + worldheight * z);
                                                        sccsjittertasks[indexer00][indexer01] = new scmessageobjectjitter();
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }*/

                                //, consoleHandle
                                updatescript.init_update_variables(sccsjittertasks, config); //, SCGLOBALSACCESSORS.SCCONSOLEWRITER
                            }

                            scupdate.Camera.SetPosition(saveplayermovePos.X, saveplayermovePos.Y, saveplayermovePos.Z);
                            scupdate.Camera.SetRotation(saveplayerrotx, saveplayerroty, saveplayerrotz);
                            //sccsjittertasks[0][0].device = updatescript.device;


                            /*swapChain1 = updatescript.SwapChain.QueryInterface<SwapChain1>();
                            // ignore all Windows events
                            factoryy = swapChain1.GetParent<SharpDX.DXGI.Factory>();
                            factoryy.MakeWindowAssociation(sccsr14sc.Form1.theHandle, WindowAssociationFlags.IgnoreAll);
                            */

                            initform = 2;
                        }



                        if (initform == 2)
                        {
                            //MessageBox((IntPtr)0, "_thread_looper0", "scmsg", 0);
                            // ReSharper disable AccessToDisposedClosure
                            if (screencapturetype == 0 && changedscreencapturetype == 0 || screencapturetype == 1 && changedscreencapturetype == 0)
                            {
                                if (updatescript != null)
                                {

                                    if (updatescript.scgraphicssec != null)
                                    {


                                        if (updatescript.scgraphicssec.activatevoxelinstancedvirtualdesktop == 1 && updatescript.scgraphicssec.activatevrheightmapfeature == 1)
                                        {

                                            if (updatescript.captureMethod != null)
                                            {

                                                if (!updatescript.captureMethod.IsCapturing)
                                                {
                                                    updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);

                                                    //updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);

                                                    if (getwindowthreadprocessidint == 0)
                                                    {
                                                        if (screencapturetype == 0)
                                                        {
                                                            //sometest = (DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface;
                                                            //var sometest = (GraphicsCapture)updatescript.captureMethod as GraphicsCapture;


                                                            //SetWindowPos(vewindowsfoundedz, SetWindowPosFlags.HWND_TOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);





                                                            DeleteObject(vewindowsfoundedz);
                                                            vewindowsfoundedz = ((GraphicsCapture)updatescript.captureMethod as GraphicsCapture)._hWnd;
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW
                                                            //SetWindowPos(vewindowsfoundedz, SpecialWindowHandles.HWND_TOPMOST, 0, 0, 0, 0, SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE);

                                                            int screenWidth = GetSystemMetrics(0);
                                                            int screenHeight = GetSystemMetrics(1);
                                                            //var windowHandler = vewindowsfoundedz;// GetActiveWindowHandle();

                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);


                                                            //var windowRec = GetWindowRect(windowHandler);
                                                            // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW



                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS); //SetWindowPosFlags.SWP_SHOWWINDOW


                                                            capturedwindowname = ((GraphicsCapture)updatescript.captureMethod as GraphicsCapture).capturedwindowname;
                                                            
                                                            
                                                            /*WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));


                                                            RECT therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            therect = new RECT();
                                                            therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            //*/

                                                            //https://social.msdn.microsoft.com/forums/azure/fr-fr/6791b517-1bc5-4e08-ac21-d73d443784cd/c-setwindowpos-for-minimized-windows?forum=csharpgeneral
                                                            if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                          


                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);


                                                            /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);*/

                                                            var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }


                                                            /*
                                                            sult = SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }*/
                                                            
                                                            
                                                            
                                                            sult = SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WindowStyles.WS_CAPTION);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }
                                                            
                                                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);

                                                            sccsr14sc.Form1.someform.Size = new System.Drawing.Size(1920, 1080);
                                                            sccsr14sc.Form1.someform.FormBorderStyle = FormBorderStyle.None;
                                                            sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                                            sccsr14sc.Form1.someform.TopMost = true;

                                                            /*
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
                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/


                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_DRAWFRAME);

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);



                                                            /*
                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);*/
                                                            /*
                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);
                                                            *//*

                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);


                                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */





                                                            //https://social.msdn.microsoft.com/forums/azure/fr-fr/6791b517-1bc5-4e08-ac21-d73d443784cd/c-setwindowpos-for-minimized-windows?forum=csharpgeneral


                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_NOSIZE | (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS);

                                                            /* if (vewindowsfoundedz == null)
                                                             {

                                                             }
                                                             //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                             //set the window to a borderless style
                                                             const int GWL_STYLE = -16; //want to change the window style
                                                             const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                             IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);


                                                             if (sult == IntPtr.Zero)
                                                             {
                                                                 //in some cases SWL just outright fails, so we can notify the user and abort
                                                                 //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                 //return;
                                                                 Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                             }

                                                             sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_MAXIMIZE);

                                                             if (sult == IntPtr.Zero)
                                                             {
                                                                 //in some cases SWL just outright fails, so we can notify the user and abort
                                                                 //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                 //return;
                                                                 Console.WriteLine("Unable to alter window style WS_VISIBLE.\nSorry.");
                                                             }


                                                             SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                             */
                                                            //executeModeChange
                                                            /*
                                                            SelectedTitle = capturedwindowname;
                                                            executeModeChange();

                                                            */
                                                            /*
                                                            WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                                                            RECT therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;

                                                            //

                                                            param.ShowCmd = ShowWindowCommands.Normal; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */

                                                            /*
                                                             SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | (long)WindowStyles.WS_MAXIMIZE));
                                                             *//*param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                             param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                             SetWindowPlacement(vewindowsfoundedz, ref param);



                                                             therect = new RECT();
                                                             therect.left = 0;
                                                             therect.top = 0;
                                                             therect.bottom = 500;
                                                             therect.right = 500;


                                                             param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                             param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                             SetWindowPlacement(vewindowsfoundedz, ref param);
                                                             */
                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */

                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;//scdirectx.D3D.SurfaceWidth;
                                                            therect.right = 500;// scdirectx.D3D.SurfaceHeight;


                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */




                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */

                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 0;// scdirectx.D3D.SurfaceWidth;
                                                            therect.right = 0;// scdirectx.D3D.SurfaceHeight;


                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */


                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                            //set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }
                                                            SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);*/
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);




                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);//




                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;

                                                            
                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
                                                            //IntPtr hWnd = vewindowsfoundedz;// FindWindow(null, SelectedTitle);
                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                            //set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }*/
                                                            //otherwise we need to resize and reposition the window to take up the full screen
                                                            //const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                                                            //int screenWidth = GetSystemMetrics(0);
                                                            //int screenHeight = GetSystemMetrics(1);
                                                            //SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                                                            //ShowWindow(vewindowsfoundedz, 0);

                                                            //ShowWindowAsync(vewindowsfoundedz, 0);
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);


                                                            //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                                                            //SetWindowLong(someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT));
                                                            /*SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_HIDEWINDOW);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_DRAWFRAME);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_FRAMECHANGED);


                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            */



















                                                            /*if (vewindowsfoundedz != IntPtr.Zero)
                                                            {
                                                                WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                                                                placement.Length = Marshal.SizeOf(ShowWindowCommands.ShowNoActivate); //Marshal.SizeOf(placement);
                                                                SetWindowPlacement(vewindowsfoundedz, ref placement);//WINDOWPLACEMENT.Default.ShowCmd);

                                                            }*/


                                                            /*
                                                            LONG lExStyle = GetWindowLong(hwnd, GWL_EXSTYLE);
                                                            lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);
                                                            SetWindowLong(hwnd, GWL_EXSTYLE, lExStyle);
                                                            And finally, to get your window to redraw with the changed styles, you can use SetWindowPos.

                                                            SetWindowPos(hwnd, NULL, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);
                                                            */


                                                            /*var WS_EX_STATICEDGE = 0x00020000L;
                                                            var WS_EX_DLGMODALFRAME = 0x00000001L;


                                                            //var testwindowptr = (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX | WS_SYSMENU)// WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);

                                                            var testwindowptr = (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);

                                                            //var getwindowptr = (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED)
                                                            //IntPtr lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, testwindowptr);

                                                            uint SWP_FRAMECHANGED = 0x0020;
                                                            uint SWP_NOMOVE = 0x0002;
                                                            uint SWP_NOSIZE = 0x0001;
                                                            uint SWP_NOZORDER = 0x0004;
                                                            uint SWP_NOOWNERZORDER = 0x0200;



                                                            //SetWindowPos(vewindowsfoundedz, null, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);

                                                            //SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);


                                                            var windowHandler = vewindowsfoundedz;// GetActiveWindowHandle();

                                                            //var windowRec = GetWindowRect(windowHandler);
                                                            // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
                                                            //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, Screen.AllScreens[monitor].WorkingArea.Left, Screen.AllScreens[monitor].WorkingArea.Top, windowRec.Size.Width + 16, windowRec.Size.Height + 38, SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, SetWindowPosFlags.SWP_FRAMECHANGED | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOOWNERZORDER )//SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, (SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER));//SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */

                                                            //capturedwindowname = "SC2_x64.exe";// sometest.capturedwindowname;

                                                            //MessageBox((IntPtr)0, "" + capturedwindowname, "scmsg", 0);


                                                            //GetWindowThreadProcessId(sometest._hWnd, out testGetWindowThreadProcessId);

                                                            //sccsr14sc.Form1.someform.deactivatecursor();

                                                            //EnableWindow(vewindowsfoundedz, false);
                                                        }
                                                        else if (screencapturetype == 1)
                                                        {
                                                            //somegcap = (GraphicsCapture)updatescript.captureMethod as GraphicsCapture;
                                                            //var sometest = (GraphicsCapture)updatescript.captureMethod as GraphicsCapture;

                                                            DeleteObject(vewindowsfoundedz);
                                                            int screenWidth = GetSystemMetrics(0);
                                                            int screenHeight = GetSystemMetrics(1);
                                                            vewindowsfoundedz = ((DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface)._hWnd;
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW)); //WS_EX_OVERLAPPEDWINDOW // WS_EX_TRANSPARENT | WS_EX_TOPMOST | WS_EX_OVERLAPPEDWINDOW

                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW); //SetWindowPosFlags.SWP_SHOWWINDOW

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS); //SetWindowPosFlags.SWP_SHOWWINDOW


                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_NOSIZE);


                                                            capturedwindowname = ((DwmSharedSurface)updatescript.captureMethod as DwmSharedSurface).capturedwindowname;


                                                            if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }


                                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_DRAWFRAME);





                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);

                                                            /*
                                                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);*/

                                                            /*
                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);

                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);


                                                            therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(sccsr14sc.Form1.theHandle, ref param);


                                                            SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */


                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                                                            Program.RECT therect = new Program.RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            Program.SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */



                                                            /*WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                                                            RECT therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            therect = new RECT();
                                                            therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;
                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);

                                                            therect = new RECT();
                                                            therect.Left = 0;
                                                            therect.Top = 0;
                                                            therect.Bottom = 1080;
                                                            therect.Right = 1920;

                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            //*/

                                                            //https://social.msdn.microsoft.com/forums/azure/fr-fr/6791b517-1bc5-4e08-ac21-d73d443784cd/c-setwindowpos-for-minimized-windows?forum=csharpgeneral
                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                                                            }
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);// | (uint)SetWindowPosFlags.SWP_NOSIZE | (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS
                                                            */
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_NOSIZE | (uint)SetWindowPosFlags.SWP_ASYNCWINDOWPOS);

                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            var sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_MAXIMIZE);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                                Program.MessageBox((IntPtr)0, "sccs1", "sccs", 0);

                                                            }*/


                                                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                            /*//set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);


                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                                            }
                                                            sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WindowStyles.WS_MAXIMIZE);

                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style WS_VISIBLE.\nSorry.");
                                                            }


                                                            SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW  );
                                                            */
                                                            /*
                                                            SelectedTitle = capturedwindowname;
                                                            executeModeChange();*/

                                                            /*
                                                            WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                                                            RECT therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;

                                                            //

                                                            param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);


                                                            */







                                                            /*SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | (long)WindowStyles.WS_MAXIMIZE));
                                                            */
                                                            /*//https://social.msdn.microsoft.com/forums/azure/fr-fr/6791b517-1bc5-4e08-ac21-d73d443784cd/c-setwindowpos-for-minimized-windows?forum=csharpgeneral
                                                            WINDOWPLACEMENT param = new WINDOWPLACEMENT();
                                                            param.Length = Marshal.SizeOf(typeof(WINDOWPLACEMENT));

                                                            RECT therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.Restore; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);



                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;//scdirectx.D3D.SurfaceWidth;
                                                            therect.right = 500;// scdirectx.D3D.SurfaceHeight;


                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/
                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/









                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/
                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.Show; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);*/

                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 0;//scdirectx.D3D.SurfaceWidth;
                                                            therect.right =0;// scdirectx.D3D.SurfaceHeight;


                                                            param.ShowCmd = ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */
                                                            /*
                                                            therect = new RECT();
                                                            therect.left = 0;
                                                            therect.top = 0;
                                                            therect.bottom = 500;
                                                            therect.right = 500;


                                                            param.ShowCmd = ShowWindowCommands.ShowMinimized; //SW_SHOWNORMAL
                                                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                                                            SetWindowPlacement(vewindowsfoundedz, ref param);
                                                            */


                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
                                                            //IntPtr hWnd = vewindowsfoundedz;// FindWindow(null, SelectedTitle);
                                                            /*if (vewindowsfoundedz == null)
                                                            {

                                                            }
                                                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

                                                            //set the window to a borderless style
                                                            const int GWL_STYLE = -16; //want to change the window style
                                                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                                            IntPtr sult = SetWindowLongPtr(vewindowsfoundedz, GWL_STYLE, (UIntPtr)WS_POPUP);
                                                            if (sult == IntPtr.Zero)
                                                            {
                                                                //in some cases SWL just outright fails, so we can notify the user and abort
                                                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                                                //return;
                                                                Console.WriteLine("Unable to alter window style.\nSorry.");
                                                            }
                                                            SetWindowPos(vewindowsfoundedz, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            */

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                                                            //SetWindowPos(sccsr14sc.Form1.theHandle, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);//


                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //otherwise we need to resize and reposition the window to take up the full screen
                                                            //const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                                                            //int screenWidth = GetSystemMetrics(0);
                                                            //int screenHeight = GetSystemMetrics(1);
                                                            //SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                                                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                                                            //ShowWindow(vewindowsfoundedz, 0);
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW | (uint)SetWindowPosFlags.SWP_FRAMECHANGED);

                                                            //ShowWindowAsync(vewindowsfoundedz, 0);
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)SetWindowPosFlags.SWP_SHOWWINDOW);


                                                            /*WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                                                            placement.Length = Marshal.SizeOf(placement);
                                                            GetWindowPlacement(vewindowsfoundedz, ref placement);*/



                                                            //var test = Marshal.SizeOf(ShowWindowCommands.Maximize);//WINDOWPLACEMENT.Default.ShowCmd;// sizeof(WINDOWPLACEMENT);

                                                            /*if (vewindowsfoundedz != IntPtr.Zero)
                                                            {
                                                                WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                                                                placement.Length = Marshal.SizeOf(ShowWindowCommands.ShowNoActivate); //Marshal.SizeOf(placement);
                                                                SetWindowPlacement(vewindowsfoundedz, ref placement);//WINDOWPLACEMENT.Default.ShowCmd);

                                                            }*/
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            //SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            /*SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_HIDEWINDOW);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_DRAWFRAME);
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_FRAMECHANGED);

                                                            
                                                            SetWindowPos(vewindowsfoundedz, (IntPtr)SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                                                            */



                                                            /*WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                                                            placement.show
                                                            WINDOWPLACEMENT.Default.*/




                                                            //IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;


                                                            //capturedwindowname = "SC2_x64.exe";// sometest.capturedwindowname;

                                                            //MessageBox((IntPtr)0, "" + capturedwindowname, "scmsg", 0);

                                                            /*
                                                            var WS_EX_STATICEDGE = 0x00020000L;
                                                            var WS_EX_DLGMODALFRAME = 0x00000001L;



                                                            var testwindowptr = (IntPtr)(GetWindowLong(vewindowsfoundedz, GWL_EXSTYLE) | WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);

                                                            //var getwindowptr = (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED)6666666666000 
                                                            //IntPtr lExStyle &= ~(WS_EX_DLGMODALFRAME | WS_EX_CLIENTEDGE | WS_EX_STATICEDGE);
                                                            //SetWindowLong(vewindowsfoundedz, GWL_EXSTYLE, testwindowptr);

                                                            uint SWP_FRAMECHANGED = 0x0020;
                                                            uint SWP_NOMOVE = 0x0002;
                                                            uint SWP_NOSIZE = 0x0001;
                                                            uint SWP_NOZORDER = 0x0004;
                                                            uint SWP_NOOWNERZORDER = 0x0200;



                                                            //SetWindowPos(vewindowsfoundedz, null, 0, 0, 0, 0, SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER);

                                                            //SetWindowPos(window, -2, 100, 75, rect.bottom, rect.right, 0x0040);


                                                            var windowHandler = vewindowsfoundedz;// GetActiveWindowHandle();

                                                            //var windowRec = GetWindowRect(windowHandler);
                                                            // When I move a window to a different monitor it subtracts 16 from the Width and 38 from the Height, Not sure if this is on my system or others.
                                                            //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, Screen.AllScreens[monitor].WorkingArea.Left, Screen.AllScreens[monitor].WorkingArea.Top, windowRec.Size.Width + 16, windowRec.Size.Height + 38, SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            //SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, SetWindowPosFlags.SWP_FRAMECHANGED | SetWindowPosFlags.SWP_NOMOVE | SetWindowPosFlags.SWP_NOSIZE | SetWindowPosFlags.SWP_NOZORDER | SetWindowPosFlags.SWP_NOOWNERZORDER )//SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            SetWindowPos(windowHandler, (IntPtr)SpecialWindowHandles.HWND_TOP, 0, 0, 0, 0, (SWP_FRAMECHANGED | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_NOOWNERZORDER));//SetWindowPosFlags.SWP_SHOWWINDOW);
                                                            */
                                                            //GetWindowThreadProcessId(sometest._hWnd, out testGetWindowThreadProcessId);

                                                            //sccsr14sc.Form1.someform.deactivatecursor();

                                                            //EnableWindow(vewindowsfoundedz, false);
                                                        }


                                                        //MessageBox((IntPtr)0, "var " + testGetWindowThreadProcessId, "scmsg", 0);
                                                        getwindowthreadprocessidint = 1;
                                                    }
                                                    //updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);

                                                    //updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);

                                                    //MessageBox((IntPtr)0, "_thread_looper1", "scmsg", 0);
                                                }

                                                //updatescript.captureMethod.StopCapture();
                                                //updatescript.captureMethod.StartCapture(sccsr14sc.Form1.theHandle, updatescript.device);

                                            }

                                        }
                                    }
                                }
                            }

                            //using ()
                            {
                                if (screencapturetype == 0 && changedscreencapturetype == 0 || screencapturetype == 1 && changedscreencapturetype == 0)
                                {
                                    if (updatescript != null)
                                    {
                                        if (updatescript.scgraphicssec != null)
                                        {
                                            if (updatescript.scgraphicssec.activatevoxelinstancedvirtualdesktop == 1 && updatescript.scgraphicssec.activatevrheightmapfeature == 1)
                                            {
                                                if (updatescript.captureMethod != null)
                                                {
                                                    //Thread.Sleep(0);
                                                    //Thread.Sleep(1);
                                                    texture2d = updatescript.captureMethod.TryGetNextFrameAsTexture2D(updatescript.device);


                                                    if (texture2d != null)
                                                    {
                                                        if (textureresetswtc == 0)
                                                        {
                                                            _textureDescription = new Texture2DDescription
                                                            {
                                                                CpuAccessFlags = CpuAccessFlags.Read,
                                                                BindFlags = BindFlags.None,// ShaderResource | BindFlags.RenderTarget,
                                                                Format = Format.B8G8R8A8_UNorm,
                                                                Width = texture2d.Description.Width,
                                                                Height = texture2d.Description.Height,
                                                                OptionFlags = ResourceOptionFlags.None,
                                                                MipLevels = 1,
                                                                ArraySize = 1,
                                                                SampleDescription = { Count = 1, Quality = 0 },
                                                                Usage = ResourceUsage.Staging
                                                            };
                                                            _texture2d = new Texture2D(updatescript.device, _textureDescription);



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








                                                        if (texture2d != null)
                                                        {
                                                            if (updatescript != null)
                                                            {
                                                                if (updatescript.device != null)
                                                                {
                                                                    updatescript.device.ImmediateContext.CopyResource(texture2d, _texture2d);

                                                                    //DISCARDED
                                                                    //DISCARDED
                                                                    //DISCARDED
                                                                    dataBox1 = updatescript.device.ImmediateContext.MapSubresource(_texture2d, 0, SharpDX.Direct3D11.MapMode.Read, SharpDX.Direct3D11.MapFlags.None);

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


                                                                    //var somebitmap = new System.Drawing.Bitmap(columns, rows, columns * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray,0));
                                                                    //somebitmap.Save(@"C:\Users\steve\Desktop\screenrecord\" + bitmapcounter + "_" + rows.ToString("00") + columns.ToString("00") + ".png");
                                                                    //bitmapcounter++;
                                                                    updatescript.device.ImmediateContext.UnmapSubresource(_texture2d, 0);
                                                                    DeleteObject(interptr1);




                                                                    /*_bitmap1 = new System.Drawing.Bitmap(texture2d.Description.Width, texture2d.Description.Height, texture2d.Description.Width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, interptr1);
                                                                    _bitSource01 = CreateBitmapSource(_bitmap1, bmpData1);
                                                                    shaderResourceView = Ab3d.DirectX.Materials.WpfMaterial.CreateTexture2D(scupdate._dxDevice, _bitSource01);

                                                                    if (lastbitmap!= null)
                                                                    {

                                                                        lastbitmap.Dispose();
                                                                        lastbitmap = null;

                                                                    }
                                                                    lastbitmap = _bitmap;

                                                                    if (_lastbitSource01!= null)
                                                                    {
                                                                        _bitSource01 = null;
                                                                    }
                                                                    _lastbitSource01 = _bitSource01;*/


                                                                    //shaderResourceView = Ab3d.DirectX.TextureLoader.CreateShaderResourceView(updatescript.device, _textureByteArray, texture2d.Description.Width, texture2d.Description.Height, bmpstride, Format.B8G8R8A8_UNorm, true);


                                                                    shaderResourceView = new ShaderResourceView(updatescript.device, texture2d);

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
                                                    }
                                                    else
                                                    {

                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }




                            
                            if (exitedprogram != 1 && changedscreencapturetype == 0)
                            {
                                if (usejitterphysics == 0)
                                {
                                    // Clear the depth buffer.
                                    //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                    // Clear the back buffer.
                                    //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);

                                    sccsjittertasks = updatescript.Update(null, sccsjittertasks);
                                    //Thread.Sleep(1);

                                    //if (!updatescript.Update(null, sccsjittertasks))
                                    //{
                                    //    updatescript.scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                                    //    updatescript.captureMethod.Dispose();
                                    //    updatescript.ShutDownGraphics();
                                    //    sccs.scgraphics.scdirectx.D3D.ShutDown();
                                    //}
                                    //sccsjittertasks = updatescript.Update(null, sccsjittertasks);
                                }
                                else if (usejitterphysics == 1)
                                {
                                    // Clear the depth buffer.
                                    //DeviceContext.ClearDepthStencilView(DepthStencilView, DepthStencilClearFlags.Depth, 1, 0);
                                    // Clear the back buffer.
                                    //DeviceContext.ClearRenderTargetView(_renderTargetView, givenColour);

                                    sccsjittertasks = updatescript.Update(jitter_sc, sccsjittertasks);
                                    //Thread.Sleep(1);
                                    //if (!updatescript.Update(jitter_sc, sccsjittertasks))
                                    //{
                                    //    updatescript.scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                                    //    updatescript.captureMethod.Dispose();
                                    //    updatescript.ShutDownGraphics();
                                    //    sccs.scgraphics.scdirectx.D3D.ShutDown();
                                    //}
                                }
                            }




                        }





                        if (changedscreencapturetype == 1) //(changedscreencapturetype == 1
                        {
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



                                    //sccs.Program.MessageBox((IntPtr)0, "capture reset1", "scmsg", 0);



                                    saveplayerposition = scupdate.OFFSETPOS;
                                    saveplayerfinalRotationMatrix = scupdate.finalRotationMatrix;

                                    saveplayerrotatingMatrixForPelvis = scupdate.rotatingMatrixForPelvis;

                                    saveplayermovePos = scupdate.movePos;

                                    saveplayerrotx = scupdate.rotx;
                                    saveplayerroty = scupdate.roty;
                                    saveplayerrotz = scupdate.rotz;




                                    double saveplayerrotationpitch = 0.0;
                                    double saveplayerrotationyaw = 0.0;
                                    double saveplayerrotationroll = 0.0;

                                    if (sccs.scgraphics.scdirectx.D3D != null)
                                    {
                                        sccs.scgraphics.scdirectx.D3D.ShutDown();
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

                                    if (_texture2d != null)
                                    {
                                        _texture2d.Dispose();
                                        _texture2d = null;
                                    }

                                    if (_bitmap != null)
                                    {
                                        _bitmap.Dispose();
                                        _bitmap = null;
                                    }

  
                                    _textureByteArray = null;// new byte[_bytesTotal];
                                    bmpData = null;








                                    
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                                    {
                                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop != null)
                                        {
                                            for (int i = 0; i < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop.Length; i++)
                                            {
                                                if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i] != null)
                                                {
                                                    for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData.Length; j++)
                                                    {
                                                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j] != null)
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




                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfDeVectorMapTemp = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfDeVectorMapTempTwo = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfSomeMap = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfVertex = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].heightmapmatrix = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesbytemaps = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesccsbytemapxyz = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesIndex = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesLocationD = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesLocationH = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesLocationW = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrix = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixb = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixc = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixd = null;




                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbuffer = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferb != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferb.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferb = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferc != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferc.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferc = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferd != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferd.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].instancesmatrixbufferd = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].Layout != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].Layout.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].Layout = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].lightBuffer != null)
                                                            {

                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].lightBuffer = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].matrixBuffer != null)
                                                            {

                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].matrixBuffer = null;
                                                            }


                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].samplerState != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].samplerState.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].samplerState = null;
                                                            }

                                                            if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].someoculusdirbuffer != null)
                                                            {
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].someoculusdirbuffer.Dispose();
                                                                updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].someoculusdirbuffer = null;
                                                            }

                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].originalArrayOfIndices = null;

                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].SC_instancedChunk_Instances = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].SC_instancedChunk_InstancesFORWARD = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].SC_instancedChunk_InstancesRIGHT = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].SC_instancedChunk_InstancesUP = null;

                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].someovrdir = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].tessellationBuffer = null;
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j].arrayOfVertex = null;

                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayOfChunkData[j] = null;

                                                        }

                                                    }

                                                    for (int j = 0; j < updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh.Length; j++)
                                                    {
                                                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j] != null)
                                                        {
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j].ShutDown();
                                                            updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].arrayofindexzeromesh[j] = null;
                                                        }
                                                    }


                                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].shaderOfChunk = null;
                                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i].ShutDown();

                                                    updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop[i] = null;
                                                }
                                            }
                                        }
                                    }

                                    if (updatescript.scgraphicssec != null)
                                    {
                                        updatescript.scgraphicssec.Shutdown();
                                        updatescript.scgraphicssec = null;
                                    }
                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec = null;
                                    }

                                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                                    {
                                        updatescript.scgraphicssecpackagemessage.scgraphicssec.somevoxelvirtualdesktop = null;
                                        scgraphicssec.somevoxelvirtualdesktopglobals = null;
                                    }






                                    if (updatescript.captureMethod != null)
                                    {
                                        updatescript.captureMethod.StopCapture();// (sccsr14sc.Form1.theHandle, updatescript.device, factoryy); //

                                        updatescript.captureMethod.Dispose();
                                        updatescript.captureMethod = null;
                                    }
                                    if (scupdate.sharpdxscreencapture != null)
                                    {
                                        //scupdate.sharpdxscreencapture.releaseFrame();
                                        scupdate.sharpdxscreencapture.Disposer();
                                        scupdate.sharpdxscreencapture = null;
                                    }


                                    /*
                                    if (sometest != null)
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



                                    updatescript.device?.Dispose();
                                    updatescript.device = null;


                                    updatescript.ShutDownGraphics();



                                 
                                    updatescript.exitthread0 = 0;
                                    updatescript.exitthread1 = 0;
                                    updatescript = null;

                                    /*
                                    if (screencapturetype == 2)
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

                             


                                    DeleteObject(vewindowsfoundedz);

                                    getwindowthreadprocessidint = 0;
                                    textureresetswtc = 0;
                                    GC.Collect();
                                    changedscreencapturetype = 0;
                                    //createinputsswtc = 0;
                                    hasresettedcapture = 1;
                                    initform = 1;
                                    //sccs.Program.MessageBox((IntPtr)0, "capture reset1", "scmsg", 0);
                                }
                            }
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


                        /*
                        if (createinputsswtc == 0)
                        {
                            if (sccsr14sc.Form1.someform != null)
                            {
                                if (sccsr14sc.Form1.theHandle != IntPtr.Zero)
                                {
                                    var refreshDXEngineAction = new Action(delegate
                                    {
                                        //sccs.scgraphics.scupdate.createinputs(sccsr14sc.Form1.theHandle);
                                        createinputs(sccsr14sc.Form1.theHandle);

                                        //someform = new RenderForm("sccsr14");
                                        /*someform.Size = new System.Drawing.Size(1920, 1080);
                                        someform.FormBorderStyle = FormBorderStyle.None;
                                        someform.WindowState = FormWindowState.Maximized;

                                        //sccsr14sc.Form1.someform.deactivatecursor();

                                        /*var _hwndSource = HwndSource.FromHwnd(someform.Handle);
                                        if (_hwndSource != null)
                                            _hwndSource.AddHook(WndProc);


                                        SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericMouse,
                                            SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                        //SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard,
                                        //    SharpDX.RawInput.DeviceFlags.None, someform.Handle);
                                        SharpDX.RawInput.Device.MouseInput += UpdateMouseText;
                                        // SharpDX.RawInput.Device.KeyboardInput += UpdateKeyboardText;
                                    });
                                    System.Windows.Threading.Dispatcher.CurrentDispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal, refreshDXEngineAction);
                                    //sccsr14sc.Form1.someform.WindowState = FormWindowState.Maximized;
                                    createinputsswtc = 1;
                                    //MessageBox((IntPtr)0, "createinputs ", "scmsg", 0);
                                }
                            }
                        }*/
                        //System.Windows.Forms.Cursor.Hide();
                        //MessageBox((IntPtr)0, "form initiated0", "scmsg", 0);
                        /*System.Windows.Forms.Application.EnableVisualStyles();
                        System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);

                        someform = new Form1();
                        */


                        /*
                        //textBox = this.textBox1;
                        textBox = new System.Windows.Forms.TextBox() { Dock = DockStyle.Fill, Multiline = true, Text = "Interact with the mouse or the keyboard...\r\n", ReadOnly = true };
                        textBox.Dock = DockStyle.Fill;
                        textBox.Multiline = true;
                        textBox.Text = "Interact with the mouse or the keyboard...\r\n";
                        textBox.ReadOnly = true;


                        someform.Controls.Add(textBox);
                        //this.Visible = true;


                        SharpDX.RawInput.Device.RegisterDevice(UsagePage.Generic, UsageId.GenericKeyboard, SharpDX.RawInput.DeviceFlags.None);
                        SharpDX.RawInput.Device.KeyboardInput += (sender, args) => textBox.Invoke(new UpdateTextCallback(UpdateKeyboardText), args);
                        */



                        /*
                        //someform = new RenderForm("sccsr14");
                        someform.Size = new System.Drawing.Size(640, 480); //1920 / 1080


                        someform.CreateControl();
                        //someform.TransparencyKey = System.Drawing.Color.Black;
                        someform.BackColor = System.Drawing.Color.Black;
                        someform.Activate();

                        someform.FormBorderStyle = FormBorderStyle.None;
                        //someform.WindowState = FormWindowState.Minimized;
                        someform.TopMost = true;
                        */
                        //someform.FormBorderStyle = FormBorderStyle.None;
                        //




                        //System.Windows.Forms.Cursor.Hide();
                        //someform.Cursor = System.Windows.Forms.Cursors.None;
                        //System.Windows.Forms.Cursor.Hide();


                        //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);
                        //consoleHandle = somewindow.EnsureHandle();


                        //SetWindowLong(someform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT));
                        //SetLayeredWindowAttributes(someform.Handle, 0,
                        //makeBoraderless();
                        //dockIt();
                        //makeBorderless(); 255, LWA_ALPHA);


                        //IntPtr window = FindWindowByCaption(IntPtr.Zero, WINDOW_NAME);
                        //SetWindowLong(window, GWL_STYLE, WS_SYSMENU);

                        //initform = 2;
                        //System.Windows.Forms.Application.Run(someform);
                        //System.Windows.Forms.Cursor.Hide();
                        //dockIt();
                        //makePanelBorderless();
                        //makeBorderless();

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





                /*
                if (updatescript != null)
                {
                    if (updatescript.scgraphicssecpackagemessage.scgraphicssec != null)
                    {

                        if (updatescript.scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                        {
                            if (updatescript.scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (updatescript.scgraphicssecpackagemessage.scjittertasks.Length > 0)
                                {
                                    if (updatescript.scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        //if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scjittertasks[0][0].hasinit == 1)
                                        {
                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);

                                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);


                                            //updatescript.scgraphicssecpackagemessage.scjittertasks = sccsjittertasks;
                                            sccsjittertasks = updatescript.StopRender(null, updatescript.scgraphicssecpackagemessage.scjittertasks);

                                            //sccsjittertasks = updatescript.scgraphicssecpackagemessage.scgraphicssec.workonshaders(updatescript.scgraphicssecpackagemessage);
                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                                        }
                                    }
                                }
                            }
                        }
                    }
                }*/




                Thread.Sleep(1);
            });

            //Console.WriteLine("Hello World!");



            //sccs.Program.MessageBox((IntPtr)0, "Program loaded0 ", "scmsg", 0);

            /*

            sccs.Program.MessageBox((IntPtr)0, "Program loaded0 ", "scmsg", 0);


            //var window = new DxWindow(".NET Window Capture Samples - Win32.DwmSharedSurface", new DwmSharedSurface());
            //window.Show();

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


                //mainreceivedmessages[0]._someData[0] = new object();


                //VOICE RECOGNITION. NOT IMPLEMENTED YET.
                /*if (i == MaxSizeMainObject - 1)
                {
                    mainreceivedmessages[i]._someData[0] = _keyboard_input._KeyboardState;
                    mainreceivedmessages[i]._voRecSwtc = 1;
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

            //SCGLOBALSACCESSORS AND GLOBALS CREATOR
            sccs.sccore.scglobalsaccessor SCGLOBALSACCESSORS = new sccs.sccore.scglobalsaccessor(mainreceivedmessages);
            if (SCGLOBALSACCESSORS == null)
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS NULL", "Console");
            }
            else
            {
                //System.Windows.MessageBox.Show("SCGLOBALSACCESSORS !NULL", "Console");
            }
            //borderlessconsole console_ = new borderlessconsole();
            //SCGLOBALSACCESSORS AND GLOBALS CREATOR




            //var somewindow = new WindowInteropHelper(System.Windows.Application.Current.MainWindow);
            //consoleHandle = somewindow.EnsureHandle();
            consoleHandle = scconsolecore.handle;

            //form = new RenderForm("sccsr14");
            //consoleHandle = form.Handle;
            config = new scsystemconfiguration("sc core systems", 1920, 1080, false, false);

            //form = new RenderForm("sccsr14");


            int swtc0 = 0;*/





            /*RenderLoop.Run(someform, () =>
            {

                // draw it
                //device.ImmediateContext.Draw(4, 0);
                //swapChain1.Present(1, PresentFlags.None, new PresentParameters());

                // ReSharper restore AccessToDisposedClosure
                Thread.Sleep(1);
            });*/



            //scdirectx directx = new scdirectx( new DwmSharedSurface());



            //SetWindowLong(consoleHandle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(window, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_TRANSPARENT));
            //SetLayeredWindowAttributes(consoleHandle, 0, 255, LWA_ALPHA);


            //sccs.Program.MessageBox((IntPtr)0, "Program loaded", "scmsg", 0);

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
        static BitmapSource _lastbitSource01;
        static BitmapSource _bitSource01;
        static BitmapSource source;
        static System.Drawing.Rectangle rectanglebitmap;
        static System.Drawing.Imaging.BitmapData bitmapData0;
        private static BitmapSource CreateBitmapSource(System.Drawing.Bitmap bitmap, System.Drawing.Imaging.BitmapData bitmapData)
        {
            try
            {
                rectanglebitmap = new System.Drawing.Rectangle(0, 0, bitmap.Width, bitmap.Height);

                bitmapData0 = bitmap.LockBits(rectanglebitmap, System.Drawing.Imaging.ImageLockMode.ReadOnly, bitmap.PixelFormat);
                source = BitmapSource.Create(bitmap.Width, bitmap.Height, 24, 24, PixelFormats.Pbgra32, null, bitmapData0.Scan0, bitmapData0.Stride * bitmap.Height, bitmapData0.Stride); //bitmap.HorizontalResolution, bitmap.VerticalResolution
                bitmap.UnlockBits(bitmapData0);

                bitmapData0 = null;
                bitmap.Dispose();

                return source;
            }
            catch
            {
                return null;
            }
        }
    }
}
