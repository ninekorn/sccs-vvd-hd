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

namespace WinRT.GraphicsCapture
{
    internal class GraphicsCapture : ICaptureMethod, IDisposable
    {
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



            if(pointer!= IntPtr.Zero)
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
            initializer.Initialize(hWnd);

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

                    _hWnd = picker.PickCaptureTarget(hWnd);
                    capturedwindowname = picker.selectedwindowname;





                    string altcapturedwindowname = capturedwindowname.ToLower();

                    if (altcapturedwindowname.Contains("microsoft") && altcapturedwindowname.Contains("edge"))
                    {

                        Console.WriteLine("MICROSOFT EDGE: " + altcapturedwindowname);






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



                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));






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



                        /*var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, Program.capturedwindowname);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                        //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);

                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                        sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                        
                        Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                        Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/
                        Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                        //Program.SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(Program.GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //sccsr14sc.Form1.SetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE, (IntPtr)(sccsr14sc.Form1.GetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE) | sccsr14sc.Form1.WS_EX_TOPMOST));


                        SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                        /*
                        var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_BORDER));//Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW

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

                        //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
                        /*hWnd = FindWindow(null, capturedwindowname);

                        if (hWnd == null) {

                            //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                            Program.MessageBox((IntPtr)0, "Failed to acquire window handle.\nPlease try again.", "sccs", 0);

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
                            Program.MessageBox((IntPtr)0, "Unable to alter window style.\nSorry.", "sccs", 0);

                        }
                        //otherwise we need to resize and reposition the window to take up the full screen
                        const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                        screenWidth = Program.GetSystemMetrics(0);
                        screenHeight = Program.GetSystemMetrics(1);
                        Program.SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);
                        */

                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);

                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        /*
                        GC.SuppressFinalize(sult);
                        DeleteObject(sult);*/
                        /*GC.SuppressFinalize(hWnd);
                        DeleteObject(hWnd);*/


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
                            Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

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
                            Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

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
                            Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

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
                             Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

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
                            Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

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
                    else if (altcapturedwindowname.Contains("gnu") && altcapturedwindowname.Contains("image") && altcapturedwindowname.Contains("manipulation") && altcapturedwindowname.Contains("program"))
                    {

                        if (altcapturedwindowname.Contains("gnu image manipulation program"))
                        {
                            Console.WriteLine(altcapturedwindowname);




                            int screenWidth = Program.GetSystemMetrics(0);
                            int screenHeight = Program.GetSystemMetrics(1);

                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                            Program.RECT therect = new Program.RECT();


                            /*hWnd = FindWindow(null, capturedwindowname);

                            if (hWnd == null)
                            {

                                //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                                Program.MessageBox((IntPtr)0, "Failed to acquire window handle.\nPlease try again.", "sccs", 0);

                            }*/

                            //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                            /*therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(hWnd, ref param);
                            */
                            //Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                            //var sult = SetWindowLongPtr(sccsr14sc.Form1.theHandle, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);

                            /*SetWindowLong(hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_CLIENTEDGE));

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            */
                            /*var sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_EXSTYLE, (UIntPtr)Program.WindowStyles.WS_MAXIMIZE);
                            */









                            /*
                            //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(_hWnd, ref param);

                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
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
                            //Program.SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(Program.GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            //sccsr14sc.Form1.SetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE, (IntPtr)(sccsr14sc.Form1.GetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE) | sccsr14sc.Form1.WS_EX_TOPMOST));

                            /*
                            SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                            */






                            hWnd = FindWindow(null, capturedwindowname);

                            if (hWnd == null)
                            {

                                //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                                Program.MessageBox((IntPtr)0, "Failed to acquire window handle.\nPlease try again.", "sccs", 0);

                            }



                            therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(hWnd, ref param);

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);





                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);


                            /*
                            therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(_hWnd, ref param);

                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            */

                            /*var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, Program.capturedwindowname);
                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                            //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                            


                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                            
                            Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                            Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);
                            Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                            
                            */






                            /*
                            hWnd = FindWindow(null, capturedwindowname);

                            if (hWnd == null)
                            {

                                //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                                Program.MessageBox((IntPtr)0, "Failed to acquire window handle.\nPlease try again.", "sccs", 0);

                            }


                            SetWindowLong(hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                            therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(hWnd, ref param);

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            */
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

                            /*var sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_OVERLAPPED);

                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            GC.SuppressFinalize(sult);
                            DeleteObject(sult);
                            */



                            /*
                            therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(hWnd, ref param);

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                           
                            GC.SuppressFinalize(hWnd);
                            DeleteObject(hWnd);*/

                            /*hWnd = FindWindow(null, capturedwindowname);

                            if (hWnd == null)
                            {

                                //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                                Program.MessageBox((IntPtr)0, "Failed to acquire window handle.\nPlease try again.", "sccs", 0);

                            }
                            */

                            //set the window to a borderless style
                            /*const int GWL_STYLE = -16; //want to change the window style
                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                            IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_POPUP));
                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                Program.MessageBox((IntPtr)0, "Unable to alter window style.\nSorry.", "sccs", 0);

                            }*/

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

                            IntPtr sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_EXSTYLE, (UIntPtr)(Program.WS_EX_CLIENTEDGE));

                            */


                            /*
                            therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(hWnd, ref param);

                            SetWindowLong(hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                            //Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);

                           var iHandle = sccs.scgraphics.scgraphicssec.FindWindow(null, Program.capturedwindowname);
                            *///scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
                              //scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);
                            /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONDOWN, 0, ((screenWidth / 2) << 16) | 0);
                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, sccs.scgraphics.scgraphicssec.WM_LBUTTONUP, 0, 0);
                            */


                            /*sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYDOWN, (int)WindowsInput.Native.VirtualKeyCode.F11, ((screenWidth / 2) << 16) | 0);
                            sccs.scgraphics.scgraphicssec.SendMessage(iHandle, (uint)sccsr14sc.Form1.WM_KEYUP, (int)WindowsInput.Native.VirtualKeyCode.F11, 0);
                            *//*
                            Program.keyboardsim.KeyDown(WindowsInput.Native.VirtualKeyCode.F11);
                            Program.keyboardsim.KeyUp(WindowsInput.Native.VirtualKeyCode.F11);*/
                            /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                            



                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                            */

















                            /*
                            GC.SuppressFinalize(_hWnd);
                            DeleteObject(_hWnd);*/


                            /*screenWidth = Program.GetSystemMetrics(0);
                            screenHeight = Program.GetSystemMetrics(1);

                            //SetWindowRgn(Program.vewindowsfoundedz, CreateRoundRectRgn(0, 0, 800, 600, 20, 20), true);
                            SetWindowRgn(hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight),true);
                            */





                            /*hWnd = FindWindow(null, capturedwindowname);

                            if (hWnd == null)
                            {

                                //MessageBox.Show("Failed to acquire window handle.\nPlease try again.");
                                Program.MessageBox((IntPtr)0, "Failed to acquire window handle.\nPlease try again.", "sccs", 0);

                            }
                            //set the window to a borderless style
                            const int GWL_STYLE = -16; //want to change the window style
                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                            IntPtr sult = Program.SetWindowLongPtr(hWnd, GWL_STYLE, (UIntPtr)(Program.WindowStyles.WS_CAPTION | Program.WindowStyles.WS_POPUPWINDOW));
                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                Program.MessageBox((IntPtr)0, "Unable to alter window style.\nSorry.", "sccs", 0);

                            }
                            //otherwise we need to resize and reposition the window to take up the full screen
                            const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                            screenWidth = Program.GetSystemMetrics(0);
                            screenHeight = Program.GetSystemMetrics(1);
                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);




                            //Program.SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(Program.GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            //sccsr14sc.Form1.SetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE, (IntPtr)(sccsr14sc.Form1.GetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE) | sccsr14sc.Form1.WS_EX_TOPMOST));


                            SetWindowLong(hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                            therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(hWnd, ref param);

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            */





                            /*var sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_OVERLAPPED);

                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                            *///MIT-LICENSE-RichardBrass-BorderlessFullscreen
                              //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                              //MIT-LICENSE-RichardBrass-BorderlessFullscreen







                            /*
                            //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(hWnd, ref param);

                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            */
                            /*
                            sult = Program.SetWindowLongPtr(_hWnd, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen*/






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


                            int screenWidth = Program.GetSystemMetrics(0);
                            int screenHeight = Program.GetSystemMetrics(1);



                         
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



                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOP, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                            //set the window to a borderless style
                            const int GWL_STYLE = -16; //want to change the window style
                            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                            IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

                            //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                            if (sult == IntPtr.Zero)
                            {
                                //in some cases SWL just outright fails, so we can notify the user and abort
                                //MessageBox.Show("Unable to alter window style.\nSorry.");
                                //return;
                                //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                            */



                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                            //MIT-LICENSE-RichardBrass-BorderlessFullscreen















                            /*











                            int screenWidth = Program.GetSystemMetrics(0);
                            int screenHeight = Program.GetSystemMetrics(1);

                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));






                            //Program.SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(Program.GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            //sccsr14sc.Form1.SetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE, (IntPtr)(sccsr14sc.Form1.GetWindowLong(_hWnd, sccsr14sc.Form1.GWL_EXSTYLE) | sccsr14sc.Form1.WS_EX_TOPMOST));


                            SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));

                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);












                            //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                            Program.RECT therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(_hWnd, ref param);

                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                            */




                            /*

                            int screenWidth = Program.GetSystemMetrics(0);
                            int screenHeight = Program.GetSystemMetrics(1);

                            Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));





                            //MessageBox((IntPtr)0, "" + isVisible, "sccsmsg", 0);
                            var therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(_hWnd, ref param);

                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                            */
                            /*SetWindowRgn(_hWnd, CreateRectRgn(0, 0, screenWidth, screenHeight), true);


                            therect = new Program.RECT();
                            therect.Left = 0;
                            therect.Top = 0;
                            therect.Bottom = screenHeight;
                            therect.Right = screenWidth;

                            param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                            Program.SetWindowPlacement(_hWnd, ref param);

                            */
                            /*var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

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

                            param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
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
                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

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
                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                            }
                            Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                            */




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
                                Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

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


                    }
                    else
                    {

                        Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                        Program.SetWindowPlacement(_hWnd, ref param);






                        //set the window to a borderless style
                        const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        /*IntPtr sult = Program.SetWindowLongPtr(_hWnd, GWL_STYLE, (UIntPtr)WS_POPUP);

                        //var sult = Program.SetWindowLongPtr(sccsr14sc.Form1.theHandle, Program.GWL_STYLE, (UIntPtr)Program.WindowStyles.WS_POPUP);

                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                            //Console.WriteLine("Unable to alter window style WS_POPUP.\nSorry.");
                            Program.MessageBox((IntPtr)0, "sccs0", "sccs", 0);

                        }*/
                        //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                        //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                        //MIT-LICENSE-RichardBrass-BorderlessFullscreen
                        //MIT-LICENSE-RichardBrass-BorderlessFullscreen


                        Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOP, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

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
                _hWnd = picker.PickCaptureTarget(hWnd);
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



                if (_hWnd == IntPtr.Zero)
                    return;

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