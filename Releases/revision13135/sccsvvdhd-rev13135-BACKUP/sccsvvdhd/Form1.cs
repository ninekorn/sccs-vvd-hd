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

using WindowsInput;
using SharpDX.DirectInput;
using SharpDX.RawInput;
using System.Windows;

using Microsoft.Win32.SafeHandles;
using System.Collections.Specialized;
using System.Runtime.ConstrainedExecution;
using VirtualKeyCode = WindowsInput.Native.VirtualKeyCode;

using System.IO;
using System.Security.Cryptography;
using SharpDX.Direct2D1.Effects;
using SharpDX;

using Rectangle = System.Drawing.Rectangle;
using RectangleF = System.Drawing.RectangleF;
using Color = System.Drawing.Color;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

using ComboBox = System.Windows.Forms.ComboBox;
using TrackBar = System.Windows.Forms.TrackBar;
using TextBox = System.Windows.Forms.TextBox;
using Button = System.Windows.Forms.Button;

namespace sccsvvdhd
{
    [System.ComponentModel.Browsable(false)]
    public partial class Form1 : Form
    {

        public int initminimizedwidth = 640;
        public int initminimizedheight = 480;
        public IntPtr lastcapturedprogram;


        public int lightintensityvalueswtc = 0;
        public int lightintensityvalue;
        public int lightintensityvaluemax = 150;
        public int lightintensityvaluemin = 50;
        public int lightintensitypvaluetickfreq = 1;

        public int screencapturebrightnessvalueswtc = 0;
        public float screencapturebrightnessvalue;
        public int screencapturebrightnessvaluemax = 100;
        public int screencapturebrightnessvaluemin = 1;
        public int screencapturebrightnesspvaluetickfreq;

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
        //public int ismenuenabled = 0;

        private string SelectedTitle = null;

        public ComboBox comboboxcapturelist;
        public ComboBox comboboxcapturelist2;
        public ComboBox comboboxcapturelist3;
        public ComboBox comboboxcapturelist4;
        public ComboBox comboboxcapturelist5;
        public ComboBox comboboxcapturelist6;

        public static CheckBox checkbox;


        public Label thelabel2;



        [DllImport("user32.dll")]
        static extern int SetWindowRgn(IntPtr hWnd, IntPtr hRgn, bool bRedraw);
        [DllImport("gdi32.dll")]

        static extern IntPtr CreateRoundRectRgn(int x1, int y1, int x2, int y2, int cx, int cy);


        public static CheckedListBox checkedlistbox;

        public static TrackBar trackbar;
        public static TrackBar trackbar2;
        public static TrackBar trackbar3;
        public static TrackBar trackbar4;

        public static TextBox textBox;
        public static Button thebutton;
        public PictureBox thepicturebox1;
        public PictureBox thepicturebox2;

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

        public static Graphics pictureboxgraphics1;
        public static Graphics pictureboxgraphics2;
        public static CheckBox thecheckbox1;
        public static CheckBox thecheckbox2;
        public static Button thebutton3;

        public static Form1 currentform;


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
            if (_fading)
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
            }
        }




        public Form1()
        {
            currentform = this;
            InitializeComponent();


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


            }

            this.MouseMove += Mouse_Move;
            this.MouseEnter += new System.EventHandler(Form1_MouseEnter);
            this.MouseEnter += Form1_MouseEnter;
            this.MouseLeave += Form1_MouseLeave;
            this.MouseClick += form1mouseclicked;
            this.MouseDown += form1mousedown;
            this.MouseUp += form1mouseup;
            this.Load += Form1_Load;
            this.Shown += Form1_Shown;


            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = TIMER_INTERVAL;       // Set interval
            _timer.Tick += _timer_Tick;




            

            thepicturebox1 = this.pictureBox1;

            this.pictureBox1.Paint += pictureBox1_Paint;
            pictureboxgraphics1 = pictureBox1.CreateGraphics();
            //this.pictureBox1.Visible = false;


            thepicturebox2 = this.pictureBox2;
            this.thepicturebox2.Paint += pictureBox2_Paint;
            pictureboxgraphics2 = thepicturebox2.CreateGraphics();
            






            this.checkBox1.CheckedChanged += checkBox1_CheckedChanged;


            this.checkBox4.CheckedChanged += checkBox4_CheckedChanged;
            this.checkBox6.CheckedChanged += checkBox6_CheckedChanged;
            this.checkBox8.CheckedChanged += checkBox8_CheckedChanged;

            
            this.checkBox7.Visible = false;




            thecheckbox1 = this.checkBox1;
            thecheckbox2 = this.checkBox2;

            thebutton3 = this.button3;

            this.trackBar1.ValueChanged += new System.EventHandler(trackBar1_Scroll);
            /*this.trackBar1.Minimum = -20000;
            this.trackBar1.Maximum = 20000;
            this.trackBar1.Value = -1000;

            this.trackBar1.TickFrequency = 25; //150*/
            trackbar = this.trackBar1;
            trackbar.Minimum = -2500;
            trackbar.Maximum = 2500;
            trackbar.Value = -2000; //15
            trackbar.TickFrequency = 1;




            this.numericUpDown2.Value = 1;


            heightmapvalue = this.trackBar1.Value;
            heightmapvaluemax = this.trackBar1.Maximum;
            heightmapvaluemin = this.trackBar1.Minimum;
            heightmapvaluetickfreq = this.trackBar1.TickFrequency;
            trackbar = this.trackBar1;



            this.trackBar2.ValueChanged += new System.EventHandler(trackBar2_Scroll);
            this.trackBar2.Minimum = lightintensityvaluemin;
            this.trackBar2.Maximum = lightintensityvaluemax;
            this.trackBar2.Value = lightintensityvaluemax-1;// (int)((lightintensityvaluemax - lightintensityvaluemin) * 0.50f) + lightintensityvaluemin; //155 //155 //75 //0.15f

            this.trackBar2.TickFrequency = 1; //150

            lightintensityvalue = this.trackBar2.Value;
            //lightintensityvaluemax = this.trackBar2.Maximum;
            //lightintensityvaluemin = this.trackBar2.Minimum;
            lightintensitypvaluetickfreq = this.trackBar2.TickFrequency;
            trackbar2 = this.trackBar2;





            this.trackBar3.ValueChanged += new System.EventHandler(trackBar3_Scroll);
            this.trackBar3.Minimum = screencapturebrightnessvaluemin;
            this.trackBar3.Maximum = screencapturebrightnessvaluemax;
            this.trackBar3.Value = screencapturebrightnessvaluemin;// (int)((screencapturebrightnessvaluemax - screencapturebrightnessvaluemin) * 0.5f) + screencapturebrightnessvaluemin; //955 //755 //355

            this.trackBar3.TickFrequency = 1; //150

            screencapturebrightnessvalue = this.trackBar3.Value;
            screencapturebrightnessvaluemax = this.trackBar3.Maximum;
            screencapturebrightnessvaluemin = this.trackBar3.Minimum;
            screencapturebrightnesspvaluetickfreq = this.trackBar3.TickFrequency;
            trackbar3 = this.trackBar3;







            this.trackBar4.ValueChanged += new System.EventHandler(trackBar4_Scroll);
            this.trackBar4.Minimum = precisiondirtyrgbchunklidervaluemin;
            this.trackBar4.Maximum = precisiondirtyrgbchunklidervaluemax;
            this.trackBar4.Value = 1;//(int)((precisiondirtyrgbchunklidervaluemax - precisiondirtyrgbchunklidervaluemin) * 0.5f) + precisiondirtyrgbchunklidervaluemin //955 //755 //355

            this.trackBar4.TickFrequency = 1; //150

            precisiondirtyrgbchunklidervalue = this.trackBar4.Value;
            precisiondirtyrgbchunklidervaluemax = this.trackBar4.Maximum;
            precisiondirtyrgbchunklidervaluemin = this.trackBar4.Minimum;
            precisiondirtyrgbchunklidervaluetickfreq = this.trackBar4.TickFrequency;
            trackbar4 = this.trackBar4;
            this.trackBar4.Visible = false;








            this.CheckedListBox1.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheckChanged);  //checkedListBox1_SelectedIndexChanged;


            someform = this;
       
            checkedlistbox = this.CheckedListBox1;
            checkbox = this.checkBox1;


            comboboxcapturelist = this.comboBox1;
            this.comboBox1.SelectedIndex = 0;


            this.comboBox1.SelectedValueChanged += new System.EventHandler(comboBox1_SelectedIndexChanged);




            this.comboBox2.SelectedValueChanged += new System.EventHandler(comboBox2_SelectedIndexChanged);
            comboboxcapturelist2 = this.comboBox2;
            this.comboBox2.SelectedIndex = 0;

   

            this.comboBox4.SelectedValueChanged += new System.EventHandler(comboBox4_SelectedIndexChanged);
            comboboxcapturelist4 = this.comboBox4;
            this.comboBox4.SelectedIndex = 1;



            //5 COLORED FACES OR 1 COLORED FACES AND 4 GRAY FACES OR ALL GRAYFACES // #1 TO #3
            //5 COLORED FACES OR 1 COLORED FACES AND 4 GRAY FACES OR ALL GRAYFACES // #1 TO #3
            this.comboBox3.SelectedValueChanged += new System.EventHandler(comboBox3_SelectedIndexChanged);
            comboboxcapturelist3 = this.comboBox3;
            this.comboBox3.SelectedIndex = 1;
            //5 COLORED FACES OR 1 COLORED FACES AND 4 GRAY FACES OR ALL GRAYFACES // #1 TO #3
            //5 COLORED FACES OR 1 COLORED FACES AND 4 GRAY FACES OR ALL GRAYFACES // #1 TO #3



            //HALF VOXEL MODE OR FULL VOXEL MODE // #1 TO #2
            //HALF VOXEL MODE OR FULL VOXEL MODE // #1 TO #2
            this.comboBox5.SelectedValueChanged += new System.EventHandler(comboBox5_SelectedIndexChanged);
            comboboxcapturelist5 = this.comboBox5;
            this.comboBox5.SelectedIndex = 0;
            sccsvvdhdresolutionvalue = this.comboBox5.SelectedIndex;
            sccsvvdhdresolutionvalueswtc = 1;
            //HALF VOXEL MODE OR FULL VOXEL MODE // #1 TO #2
            //HALF VOXEL MODE OR FULL VOXEL MODE // #1 TO #2


            //RESOLUTION OPTIONS #1 TO #5
            //RESOLUTION OPTIONS #1 TO #5
            this.comboBox6.SelectedValueChanged += new System.EventHandler(comboBox6_SelectedIndexChanged);
            comboboxcapturelist6 = this.comboBox6;
            this.comboBox6.SelectedIndex = 4;
            //RESOLUTION OPTIONS #1 TO #5
            //RESOLUTION OPTIONS #1 TO #5
            //RESOLUTION OPTIONS #1 TO #5












            thelabel2 = this.label2;


            this.checkBox2.Checked = true;
            this.checkBox3.Checked = true;


            this.checkBox1.Visible = false;
            //this.CheckedListBox1.Visible = false;



            hasmaximizedinternal = 1;



            this.FormClosing += Form1_Closing;

            trackBar4.Visible = false;
            pictureBox1.Visible = false;
            CheckedListBox1.Visible = false;
            pictureBox2.Visible = true;



            /*this.label2.Parent = this;
            this.label2.BackColor = System.Drawing.Color.Empty;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            */


            /*
            SetWindowLong(this.label2.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.label2.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
            SetLayeredWindowAttributes(this.label2.Handle, 0, 255, LWA_ALPHA);
            */
            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/79e22a15-fb3d-4a9d-9e14-62fed9a8d491/how-to-read-bitmap-from-a-file?forum=netfxbcl => Sander Rijken post 2005

            /*string path = System.Windows.Forms.Application.StartupPath + @"\transparent.jpg";
            FileStream sw1 = new FileStream(path, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite);
            //Bitmap thebitmap = new System.Drawing.Bitmap(128, 128);
            thebitmap = new System.Drawing.Bitmap(sw1);

            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;

            SetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
            SetLayeredWindowAttributes(this.pictureBox1.Handle, 0, 0, LWA_ALPHA);
            */


            refresh();
        }

     

        Bitmap thebitmap;

        public int hasclickedmouse = 0;




        private void form1mouseclicked(object sender, EventArgs e)
        {
            label2.Focus();
            refresh();


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
            /*IntPtr sult = Program.SetWindowLongPtr(sccsvvdhd.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                //return;
                Console.WriteLine("Unable to alter window style.\nSorry.");
            }*/

            Rectsccs = new Program.RECT();
            Program.GetWindowRect(sccsvvdhd.Form1.theHandle, ref Rectsccs);

            //Program.SetWindowPos(sccsvvdhd.Form1.theHandle, IntPtr.Zero, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
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


            Program.SetWindowPos(sccsvvdhd.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, correctedw, correctedh, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            */


            //Rectsccslast = rec
            hasclickedmouse = 1;
        }




        private void form1mousedown(object sender, EventArgs e)
        {


            if (hasclickedmouse == 0)
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
                /*IntPtr sult = Program.SetWindowLongPtr(sccsvvdhd.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
                if (sult == IntPtr.Zero)
                {
                    //in some cases SWL just outright fails, so we can notify the user and abort
                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                    //return;
                    Console.WriteLine("Unable to alter window style.\nSorry.");
                }*/

                Rectsccs = new Program.RECT();
                Program.GetWindowRect(sccsvvdhd.Form1.theHandle, ref Rectsccs);

                //Program.SetWindowPos(sccsvvdhd.Form1.theHandle, IntPtr.Zero, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
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


                Program.SetWindowPos(sccsvvdhd.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, correctedw, correctedh, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                */



                hasclickedmouse = 1;
            }

        }






        private void form1mouseup(object sender, EventArgs e)
        {
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
            /*IntPtr sult = Program.SetWindowLongPtr(sccsvvdhd.Form1.theHandle, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                //return;
                Console.WriteLine("Unable to alter window style.\nSorry.");
            }

            Program.SetWindowPos(sccsvvdhd.Form1.theHandle, IntPtr.Zero, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen
            //MIT-LICENSE-RichardBrass-BorderlessFullscreen

            Program.SetWindowPos(sccsvvdhd.Form1.theHandle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, Rectsccs.Left, Rectsccs.Top, Rectsccs.Right, Rectsccs.Bottom, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
            */

            hasclickedmouse = 0;
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
            //this.Size = new System.Drawing.Size(Program._desktopboundswidth, Program._desktopboundsheight);
            //this.FormBorderStyle = FormBorderStyle.None;
            //this.WindowState = FormWindowState.Maximized;
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
            //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));



            //ShowCursor(false);
            //System.Windows.Forms.Cursor.Hide();
            //Cursor.Hide();
            //MessageBox((IntPtr)0, "Hide", "sccsmsg", 0);


        }







        private void pictureBox2_Paint(object sender, PaintEventArgs e)
        {
            DrawStringRectangleF2(e);
        }

        public static String drawString2 = ""; //Sample Text


        public String picturebox2drawString0 = "mu:";
        public String picturebox2drawString1 = "mw:";
        public String picturebox2drawString2 = "mr:";


        public float picturebox2fpsfloat0 = 0;
        public float picturebox2fpsfloat1 = 0;
        public float picturebox2fpsfloat2 = 0;

        public float lastpicturebox2fpsfloat0 = 0;
        public float lastpicturebox2fpsfloat1 = 0;
        public float lastpicturebox2fpsfloat2 = 0;



        //https://social.msdn.microsoft.com/Forums/en-US/5599c575-99dd-4889-aed0-999cac4dab6f/label-with-transparent-background?forum=winforms
        public void DrawStringRectangleF2(PaintEventArgs e)
        {



            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/79e22a15-fb3d-4a9d-9e14-62fed9a8d491/how-to-read-bitmap-from-a-file?forum=netfxbcl
            //pictureBox1.BackgroundImage = 





            /*BinaryReader bw1 = new BinaryReader(sw1);


            while ()
            {

            }*/



            /*BinaryWriter bw1 = new BinaryWriter(sw1);
            bw1.Write(thebitmap, 0, 4001);  // 4001 is the size of the bytes allocated
            bw1.Close();
            sw1.Close();
            // use the image to be displayed in a picturebox
            Bitmap MyImage = null;*/
            // pbFace is the picturebox control
            /*pbFace.SizeMode = PictureBoxSizeMode.StretchImage;
            MyImage = new Bitmap(path);
            pbFace.Image = (Image)MyImage;*/




            /*
            SetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
            SetLayeredWindowAttributes(this.pictureBox1.Handle, 0, 255, LWA_ALPHA);
            */






            //sccs.Program.MessageBox((IntPtr)0, "DrawStringRectangleF", "sccsmsg", 0);

            // Create string to draw.

            // Create font and brush.

            Font drawFont = new Font("Arial", 8);

            SolidBrush drawBrush = new SolidBrush(Color.Red);
            // Create rectangle for drawing.

       


            //Bitmap myBitmap = new Bitmap("Spiral.png");

            //if (pictureBox1.BackgroundImage != null)
            {


                /*Bitmap myBitmap = (Bitmap)pictureBox1.BackgroundImage;


                //Rectangle expansionRectangle = new Rectangle(135, 10,
                //   myBitmap.Width, myBitmap.Height);

                Rectangle compressionRectangle = new Rectangle(0, 0,
                   myBitmap.Width / 10, myBitmap.Height / 10);

                //e.Graphics.DrawImage(myBitmap, 10, 10);
                //e.Graphics.DrawImage(myBitmap, expansionRectangle);
                e.Graphics.DrawImage(myBitmap, compressionRectangle);
                */




                //BitmapSource

                /*//https://social.msdn.microsoft.com/Forums/vstudio/en-US/8b525820-98a1-4d5d-a679-c2ad5bd16949/how-can-i-quothookquot-gdi-graphics-from-other-forms-and-redraw-them-on-a-remote-computer?forum=csharpgeneral
                IntPtr hdcBitmap = e.Graphics.GetHdc();

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
                    //7704 // memorymapstride 4*Program._desktopboundswidth
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
                }*/




                //Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)

                //var thebitmap = new System.Drawing.Bitmap(columns, rows, columns * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, hdcBitmap); //Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)




                // Draw rectangle to screen.

                Pen blackPen = new Pen(Color.Black);

                float x = 0;

                float y = 0;

                float width = Program._desktopboundswidth;// 200.0F;

                float height = Program._desktopboundsheight;// 50.0F;
                RectangleF drawRect = new RectangleF(x, y, width, height);
                e.Graphics.DrawRectangle(blackPen, x, y, width, height);

                // Draw string to screen.
                e.Graphics.DrawString(picturebox2drawString0 + " " + picturebox2fpsfloat0, drawFont, drawBrush, drawRect);

                x = 0;
                y = 10;
                width = Program._desktopboundswidth;
                height = Program._desktopboundsheight;
                drawRect = new RectangleF(x, y, width, height);


                // Draw string to screen.
                e.Graphics.DrawString(picturebox2drawString1 + " " + picturebox2fpsfloat1, drawFont, drawBrush, drawRect);

                x = 0;
                y = 20;
                width = Program._desktopboundswidth;
                height = Program._desktopboundsheight;
                drawRect = new RectangleF(x, y, width, height);

                // Draw string to screen.
                e.Graphics.DrawString(picturebox2drawString2 + " " + picturebox2fpsfloat2, drawFont, drawBrush, drawRect);



                


                /*this.pictureBox1.BackgroundImage = thebitmap;
                SetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
                SetLayeredWindowAttributes(this.pictureBox1.Handle, 0, 255, LWA_ALPHA);
                this.pictureBox1.BackColor = System.Drawing.Color.Transparent;*/
            }
        }



        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawStringRectangleF(e);
        }

        public static String drawString = ""; //Sample Text


        public String picturebox1drawString = "";//"dirty pixel rgb:";

        public int picturebox1regiondirtypixelrgb = 0;
        public int lastpicturebox1regiondirtypixelrgb = 0;

        //https://social.msdn.microsoft.com/Forums/en-US/5599c575-99dd-4889-aed0-999cac4dab6f/label-with-transparent-background?forum=winforms
        public void DrawStringRectangleF(PaintEventArgs e)
        {
            


            //https://social.msdn.microsoft.com/Forums/vstudio/en-US/79e22a15-fb3d-4a9d-9e14-62fed9a8d491/how-to-read-bitmap-from-a-file?forum=netfxbcl
            //pictureBox1.BackgroundImage = 





            /*BinaryReader bw1 = new BinaryReader(sw1);


            while ()
            {

            }*/



            /*BinaryWriter bw1 = new BinaryWriter(sw1);
            bw1.Write(thebitmap, 0, 4001);  // 4001 is the size of the bytes allocated
            bw1.Close();
            sw1.Close();
            // use the image to be displayed in a picturebox
            Bitmap MyImage = null;*/
            // pbFace is the picturebox control
            /*pbFace.SizeMode = PictureBoxSizeMode.StretchImage;
            MyImage = new Bitmap(path);
            pbFace.Image = (Image)MyImage;*/




            /*
            SetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
            SetLayeredWindowAttributes(this.pictureBox1.Handle, 0, 255, LWA_ALPHA);
            */






            //sccs.Program.MessageBox((IntPtr)0, "DrawStringRectangleF", "sccsmsg", 0);

            // Create string to draw.
            
            // Create font and brush.

            Font drawFont = new Font("Arial", 8);

            SolidBrush drawBrush = new SolidBrush(Color.Red);
            // Create rectangle for drawing.

            float x = 0;

            float y = 0;

            float width = Program._desktopboundswidth;// 200.0F;

            float height = Program._desktopboundsheight;// 50.0F;
            RectangleF drawRect = new RectangleF(x, y, width, height);


            //Bitmap myBitmap = new Bitmap("Spiral.png");

            //if (pictureBox1.BackgroundImage != null)
            {
                

                /*Bitmap myBitmap = (Bitmap)pictureBox1.BackgroundImage;


                //Rectangle expansionRectangle = new Rectangle(135, 10,
                //   myBitmap.Width, myBitmap.Height);

                Rectangle compressionRectangle = new Rectangle(0, 0,
                   myBitmap.Width / 10, myBitmap.Height / 10);

                //e.Graphics.DrawImage(myBitmap, 10, 10);
                //e.Graphics.DrawImage(myBitmap, expansionRectangle);
                e.Graphics.DrawImage(myBitmap, compressionRectangle);
                */




                //BitmapSource

                /*//https://social.msdn.microsoft.com/Forums/vstudio/en-US/8b525820-98a1-4d5d-a679-c2ad5bd16949/how-can-i-quothookquot-gdi-graphics-from-other-forms-and-redraw-them-on-a-remote-computer?forum=csharpgeneral
                IntPtr hdcBitmap = e.Graphics.GetHdc();

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
                    //7704 // memorymapstride 4*Program._desktopboundswidth
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
                }*/




                //Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)

                //var thebitmap = new System.Drawing.Bitmap(columns, rows, columns * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, hdcBitmap); //Marshal.UnsafeAddrOfPinnedArrayElement(_textureByteArray, 0)




                // Draw rectangle to screen.

                Pen blackPen = new Pen(Color.Black);
                e.Graphics.DrawRectangle(blackPen, x, y, width, height);

                // Draw string to screen.
                e.Graphics.DrawString(picturebox1drawString + " " + picturebox1regiondirtypixelrgb, drawFont, drawBrush, drawRect);


                /*this.pictureBox1.BackgroundImage = thebitmap;
                SetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.pictureBox1.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
                SetLayeredWindowAttributes(this.pictureBox1.Handle, 0, 255, LWA_ALPHA);
                this.pictureBox1.BackColor = System.Drawing.Color.Transparent;*/
            }
        }

        public static IntPtr theHandle;
        //scupdate updatescript;

        private void Form1_Load(object sender, EventArgs e)
        {
            theHandle = this.Handle;
            initForm = 1;

            //this.label2.Parent = listBox1;
            /*this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            //this.checkBox3.BackColor = System.Drawing.Color.Transparent;

            this.label2.ForeColor = System.Drawing.Color.Red;

            SetWindowLong(this.label2.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.label2.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
            SetLayeredWindowAttributes(this.label2.Handle, 0, 255, LWA_ALPHA);

            this.label3.ForeColor = System.Drawing.Color.Red;

            SetWindowLong(this.label3.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.label3.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
            SetLayeredWindowAttributes(this.label3.Handle, 0, 255, LWA_ALPHA);

            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            */






            //sccs.Program.MessageBox((IntPtr)0, "Form1_Load0", "scmsg", 0);






        }

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

        public static uint testGetWindowThreadProcessId;

        /*[DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        */
        public static int usesharpdxscreencapture = 0;

        /*[DllImport("USER32.DLL")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        */
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [DllImport("user32.dll")]
        static extern bool DrawMenuBar(IntPtr hWnd);

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        /*
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);

        [DllImport("user32", ExactSpelling = true, SetLastError = true)]
        internal static extern int MapWindowPoints(IntPtr hWndFrom, IntPtr hWndTo, [In, Out] ref RECT rect, [MarshalAs(UnmanagedType.U4)] int cPoints);
        */
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

      

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int Left;

            public int Top;

            public int Right;

            public int Bottom;
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
        private static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);*/

        /*[DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        */


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

        /*private void Form1_Resize(object sender, System.EventArgs e)
        {
            //MessageBox((IntPtr)0, "Form1_Resize0", "sccsmsg", 0);
            Control control = (Control)sender;

            thepanel.Width = control.Width;
            thepanel.Height = control.Height;

            //MessageBox((IntPtr)0, "Form1_Resize1", "sccsmsg", 0);
        }*/


        public int mousex;
        public int mousey;
        private void Mouse_Move(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            mousex = e.X;
            mousey = e.Y;

        }

        public void deactivatecursor()
        {
          
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


            }



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
        }

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

      

        private void panel1_Paint_2(object sender, PaintEventArgs e)
        {

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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen
            //INTERNAL- isfullscreen





            if (checkBox1 != null)
            {
                checkBox1.Focus();
            }

            if (checkBox1.Checked)
            {
                this.button3.Text = "Shrink";

                this.Size = new System.Drawing.Size(Program._desktopboundswidth, Program._desktopboundsheight);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;

                if (this.checkBox2.Checked)
                {

                }
                else if (!this.checkBox2.Checked)
                {
                    SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));
                }
                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));




            }
            else
            {
                this.button3.Text = "Maximize";

                this.Size = new System.Drawing.Size(initminimizedwidth, initminimizedheight);
                this.FormBorderStyle = FormBorderStyle.Sizable;
                this.WindowState = FormWindowState.Normal;
            }
        }

        public int counterscreencapturechanged = -1;
        public int screencaptureindextype = 0;
        public int lastscreencaptureindextype = 0;
        //https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.combobox.selectedindexchanged?view=windowsdesktop-6.0
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (comboboxcapturelist != null)
            {
                comboboxcapturelist.Focus();
            }

            if (counterscreencapturechanged >= 1)
            {
                counterscreencapturechanged = 0;
            }


            if (counterscreencapturechanged != -1)
            {
                ComboBox comboBox = (ComboBox)sender;
                string selectedscreencaptureindex = (string)comboboxcapturelist.SelectedItem;
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
            }

            counterscreencapturechanged++;*/

            ComboBox comboBox = (ComboBox)sender;
            string selectedscreencaptureindex = (string)comboboxcapturelist.SelectedItem;
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

            label2.Focus();
        }



        public int lastprojectiontypeindex = 0;
        public int projectiontypeindex = 0;


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

            ComboBox comboBox = (ComboBox)sender;
            string selectedscreencaptureindex = (string)this.comboBox2.SelectedItem;
            projectiontypeindex = this.comboBox2.FindStringExact(selectedscreencaptureindex);


            if (projectiontypeindex == 0)
            {
                trackbar.Minimum = -2500;
                trackbar.Maximum = 2500;
                //trackbar.Value = -1000;

                trackbar.TickFrequency = 1;
            }
            else if (projectiontypeindex == 1)
            {
                trackbar.Minimum = -5000;
                trackbar.Maximum = 2500;
                //trackbar.Value = -1000;

                trackbar.TickFrequency = 1;
            }


            if (projectiontypeindex != lastprojectiontypeindex)
            {

            }



            //Console.WriteLine("index:" + projectiontypeindex);



            /*if (screencaptureindextype != lastscreencaptureindextype)
            {
                Program.screencapturetype = screencaptureindextype;
                Program.changedscreencapturetype = 1;

                lastscreencaptureindextype = screencaptureindextype;
                //sccs.Program.MessageBox((IntPtr)0, "combobox screencapture type changed!", "scmsg", 0);
                //thepanel.Focus();
                //comboboxcapturelist.
            }*/

            lastprojectiontypeindex = projectiontypeindex;
            label2.Focus();
        }





        private void label2_Click(object sender, EventArgs e)
        {

        }

        //user selects an item in the listbox
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Form1.capturedprogram != IntPtr.Zero)
            {


                /*int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);

                //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                Program.SetWindowPos(Form1.capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                //var thisformhandlerect = new Rect();
                //GetWindowRect(currentform.Handle, out thisformhandlerect);

                var param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                var therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = screenHeight;
                therect.Right = screenWidth;

                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                Program.SetWindowPlacement(Form1.capturedprogram, ref param);
                */




                
                Marshal.Release(Form1.capturedprogram);
                
                UnSetFocus(Form1.capturedprogram, "");
                GC.SuppressFinalize(Form1.capturedprogram);
                DeleteObject(Form1.capturedprogram);
                GC.Collect();

                GetVolumes();

             
              

            }


            if (listBox1.SelectedItem != null)
            {
                SelectedTitle = listBox1.SelectedItem.ToString();
            }

            button1.Enabled = true;
        }


        public static int startcaptureonce = 1;

        //https://www.pinvoke.net/default.aspx/user32.ReleaseDC
        /*[DllImport("user32.dll")]
        static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);
        */


        string lastSelectedTitle = "";
        int firstclickswtc = 0;
        int firstclick = 0;
        //user clicks 'Refresh' button
        private void button2_Click(object sender, EventArgs e)
        {
            /*if (firstclickswtc == 0)
            {
                if (firstclick >= 1)
                {
                    Program.changedscreencapturetype = 1;
                    firstclick = 0;
                    firstclickswtc = 1;
                }
                firstclick++;
            }*/

            if (Form1.capturedprogram != IntPtr.Zero)
            {

            
            }

            if (lastSelectedTitle != null)
            {



                if (!lastSelectedTitle.ToLower().Contains("microsoft") || !lastSelectedTitle.ToLower().Contains("edge"))
                {
                    //if (lastSelectedTitle.ToLower().Contains("void") && lastSelectedTitle.ToLower().Contains("expanse"))
                    {
                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;



                        var param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(lastcapturedprogram, ref param);

                        Program.SetWindowPos(lastcapturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    }
                }
                else
                {
                    if (lastSelectedTitle.ToLower().Contains("microsoft") && lastSelectedTitle.ToLower().Contains("edge"))
                    {
                        //set the window to a borderless style
                        const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = SetWindowLongPtr(lastcapturedprogram, GWL_STYLE, (UIntPtr)(WindowStyles.WS_SIZEFRAME | WindowStyles.WS_CAPTION | WindowStyles.WS_VISIBLE | WindowStyles.WS_GROUP | WindowStyles.WS_VSCROLL | WindowStyles.WS_SYSMENU | WindowStyles.WS_MAXIMIZEBOX));// WS_POPUP);
                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                        }
                        //otherwise we need to resize and reposition the window to take up the full screen
                        const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                        var screenWidth = GetSystemMetrics(0);
                        var screenHeight = GetSystemMetrics(1);
                        SetWindowPos(lastcapturedprogram, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                        Program.SetWindowPos(lastcapturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        /*if (SelectedTitle.ToLower().Contains("void") && SelectedTitle.ToLower().Contains("expanse"))
                        {
                            Program.SetWindowPos(capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                        }*/
                    }
                    else
                    {
                        //set the window to a borderless style
                        const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = SetWindowLongPtr(lastcapturedprogram, GWL_STYLE, (UIntPtr)(WindowStyles.WS_SIZEFRAME | WindowStyles.WS_CAPTION | WindowStyles.WS_VISIBLE | WindowStyles.WS_GROUP | WindowStyles.WS_VSCROLL | WindowStyles.WS_SYSMENU | WindowStyles.WS_MAXIMIZEBOX));// WS_POPUP);
                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                        }
                        //otherwise we need to resize and reposition the window to take up the full screen
                        const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                        var screenWidth = GetSystemMetrics(0);
                        var screenHeight = GetSystemMetrics(1);
                        SetWindowPos(lastcapturedprogram, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                        Program.SetWindowPos(lastcapturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                        
                    }
                }
            }

            

            if (SelectedTitle != null)
            {

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
                startcaptureonce = 1;

            }



            lastSelectedTitle = SelectedTitle;

            refresh();
        }

        //user clicks 'Activate' button
        private void button1_Click(object sender, EventArgs e)
        {
            refresh();
        }

        //Remove listbox selection state, Scan for windows, refresh contents of listbox
        public void refresh()
        {
            SelectedTitle = null;
            //button1.Enabled = false;
            listBox1.BeginUpdate();
            listBox1.Items.Clear();
            EnumWindows(enumWindowProc, IntPtr.Zero);
            listBox1.EndUpdate();
            label2.Focus();

        }

        /*public IntPtr PickCaptureTarget(IntPtr hWnd)
        {
            //new WindowInteropHelper(this).Owner = hWnd;
            //ShowDialog();

            //return ((CapturableWindow?)Windows.SelectedItem)?.Handle ?? IntPtr.Zero;

            
        }*/



        public static IntPtr capturedprogram;


        //attempt to change the selected item to borderless windowed "mode"
        private void executeModeChange()
        {
            //find the window matching the title (this could potentially be problematic if there's more than one window with the same title, but in practice it's not been an issue)
            capturedprogram = FindWindow(null, SelectedTitle);
            lastcapturedprogram = capturedprogram;



            if (!SelectedTitle.ToLower().Contains("microsoft") || !SelectedTitle.ToLower().Contains("edge"))
            {
                if (SelectedTitle.ToLower().Contains("void") && !SelectedTitle.ToLower().Contains("expanse"))
                {

                }
                else
                {

                }
            }
            else
            {
                if (SelectedTitle.ToLower().Contains("microsoft") && SelectedTitle.ToLower().Contains("edge"))
                {


                }
            }



            //Program.MessageBox((IntPtr)0, "Form1Line169.executeModeChange", "sccsmsg", 0);

            //
            /*if (SelectedTitle == "VoidExpanse")
            {
               System.Windows.Forms.MessageBox.Show("User chose VoidExpanse. intptr handle:" + capturedprogram);

                //WinRT.GraphicsCapture graphcapture = new WinRT.GraphicsCapture();

                //Program theprogram = new
                //Process.Start("notepad.exe", uname, password, domain);

                //WinRT.GraphicsCapture.winrtgraphicscapture winrtgraphicscapture = new WinRT.GraphicsCapture.winrtgraphicscapture();
                //winrtgraphicscapture.startprogram();

                //WinRT.GraphicsCapture.GraphicsCapture winrtgraphicscapture = new WinRT.GraphicsCapture.GraphicsCapture();               
           }*/

            if (capturedprogram == null)
            {
                //MessageBox.Show("Failed to acquire window handle.\nPlease try again."); 

            }


            //set the window to a borderless style
            const int GWL_STYLE = -16; //want to change the window style
            const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
            IntPtr sult = SetWindowLongPtr(capturedprogram, GWL_STYLE, (UIntPtr)WS_POPUP);
            if (sult == IntPtr.Zero)
            {
                //in some cases SWL just outright fails, so we can notify the user and abort
                //MessageBox.Show("Unable to alter window style.\nSorry.");
                return;
            }
            //otherwise we need to resize and reposition the window to take up the full screen
            const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active

            int beforewidth = this.Width;
            int beforeheight = this.Height;

            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics(1);
            SetWindowPos(capturedprogram, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

            //Program.SetWindowPos(capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, beforewidth, beforeheight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

            if (SelectedTitle.ToLower().Contains("void") && SelectedTitle.ToLower().Contains("expanse"))
            {
                //Program.SetWindowPos(capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                var thisformhandlerect = new Rect();
                GetWindowRect(currentform.Handle, out thisformhandlerect);

                var param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                var therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = screenHeight;
                therect.Right = screenWidth;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                Program.SetWindowPlacement(capturedprogram, ref param);


                //TO READD
                //TO READD
                //TO READD

                currentform.Size = new System.Drawing.Size(screenWidth, screenHeight);
                currentform.FormBorderStyle = FormBorderStyle.None;
                currentform.WindowState = FormWindowState.Maximized;
                currentform.TopMost = true;

                //TO READD
                //TO READD
                //TO READD





                //set the window to a borderless style
                //const int GWL_STYLE = -16; //want to change the window style
                //const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                sult = SetWindowLongPtr(capturedprogram, GWL_STYLE, (UIntPtr)(WindowStyles.WS_VISIBLE | WindowStyles.WS_MAXIMIZE));// WS_POPUP);
                if (sult == IntPtr.Zero)
                {
                    //in some cases SWL just outright fails, so we can notify the user and abort
                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                    //return;
                }
                //otherwise we need to resize and reposition the window to take up the full screen
                //const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                screenWidth = GetSystemMetrics(0);
                screenHeight = GetSystemMetrics(1);
                SetWindowPos(capturedprogram, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                Program.SetWindowPos(capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, this.Width, this.Height, Program.SWP_SHOWWINDOW);


            }

        }

        //this is the procedure passed to the window enumerator so that it can identify the active windows, extract their titles, and add them to the listbox
        private bool enumWindowProc(IntPtr hWnd, IntPtr lParam)
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

                
                if (sb.ToString() == SelectedTitle)
                {
                    //MessageBox.Show("User chose VoidExpanse. intptr handle:" + hWnd);
                    //Program.MessageBox((IntPtr)0, "User chose. intptr handle:" + hWnd + "/title:" + SelectedTitle, "sccsmsg", 0);

                    if (SelectedTitle.ToLower().Contains("microsoft") && SelectedTitle.ToLower().Contains("edge"))
                    {

                        capturedprogram = hWnd;

                        //Program.MessageBox((IntPtr)0, "TEST" , "sccs", 0);

                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        var param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        //bool isfullscreen = EnumTheWindows(_hWnd, IntPtr.Zero);

                        // //Program.MessageBox((IntPtr)0, "isfullscreen:" + isfullscreen, "sccs", 0);

                        var iswinmax = sccsvvdhd.Form1.IsWindowMaximized(hWnd);

                        var iswindowtitlebaredge = IsWindowTitlebar(hWnd); //if fullscreen then windowtitlebar == false. for edge
                        var iswindowmaximizedboxdedge = IsWindowMaximizeBox(hWnd);

                        Program.RECT rectmicrosoftedge = new Program.RECT();
                        Program.GetWindowRect(hWnd, ref rectmicrosoftedge);


                        //Program.MessageBox((IntPtr)0, "iswindowtitlebar:" + iswindowtitlebaredge + "/iswindowmaximizebox:" + iswindowmaximizedboxdedge, "sccs", 0);

                        //Program.MessageBox((IntPtr)0, "0/w:" + (rectmicrosoftedge.Right - rectmicrosoftedge.Left) + "/h:" + (rectmicrosoftedge.Bottom - rectmicrosoftedge.Top), "sccs", 0);


                        var windowisminimized = IsWindowMinimized(hWnd);

                        //Program.MessageBox((IntPtr)0, "windowisminimized:" + windowisminimized, "sccs", 0);

                        bool isalttab = isAltTabWindow(hWnd);
                        bool isfullscreen = EnumTheWindows(hWnd, IntPtr.Zero);
                        //SetWindowLong(hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        //Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        if (windowisminimized)
                        {
                            //BringWindowToTop(hWnd);
                            //ShowWindow(hWnd, SW_SHOW);

                            SetWindowLong(hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                            Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);


                            //Program.MessageBox((IntPtr)0, "iswindowtitlebaredge:" + iswindowtitlebaredge + "/iswindowmaximizedboxdedge:" + iswindowmaximizedboxdedge, "sccs", 0);


                            if (!iswindowtitlebaredge) // doesnt have a title bar, maybe the window is already fullscreen
                            {
                                //window might be fullscreen already.

                                if (iswindowmaximizedboxdedge) //edge has maximize box enabled in f11 fullscreen mode
                                {
                                    /*var iswindowoverlappedboxdedge = IsWindowOverlapped(hWnd);

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
                                    Program.SetWindowPlacement(hWnd, ref param);


                                    uint lpdwProcessId;
                                    uint foreThread = GetWindowThreadProcessId(GetForegroundWindow(), out lpdwProcessId); //IntPtr.Zero

                                    uint appThread = GetCurrentThreadId();

                                    uint lpdwProcessIdcapturedprogram;
                                    uint foreThreadcapturedapp = GetWindowThreadProcessId(hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(hWnd);

                                    //ShowWindow(hWnd, SW_SHOW);
                                    SetFocus(hWnd, "");
                                    /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(hWnd, "");*/

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
                            uint foreThreadcapturedapp = GetWindowThreadProcessId(hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                            if (foreThread != foreThreadcapturedapp) // then, this program is not foreground// there is another program foreground, is it the captured program
                            {
                                SetWindowLong(hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                                Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                                if (!iswindowtitlebaredge) // doesnt have a title bar, maybe the window is already fullscreen
                                {
                                    //window might be fullscreen already.

                                    if (iswindowmaximizedboxdedge) //edge has maximize box enabled in f11 fullscreen mode
                                    {
                                        /*var iswindowoverlappedboxdedge = IsWindowOverlapped(hWnd);

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
                                    foreThreadcapturedapp = GetWindowThreadProcessId(hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(hWnd);

                                    //ShowWindow(hWnd, SW_SHOW);
                                    SetFocus(hWnd, "");
                                    /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(hWnd, "");
                                    */
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
                                        /*var iswindowoverlappedboxdedge = IsWindowOverlapped(hWnd);

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
                                    foreThreadcapturedapp = GetWindowThreadProcessId(hWnd, out lpdwProcessIdcapturedprogram); //IntPtr.Zero


                                    AttachThreadInput(foreThreadcapturedapp, appThread, true);

                                    //BringWindowToTop(hWnd);

                                    //ShowWindow(hWnd, SW_SHOW);
                                    SetFocus(hWnd, "");
                                    /*Program.keyboardsim.KeyPress(WindowsInput.Native.VirtualKeyCode.F11);
                                    SetFocus(hWnd, "");*/

                                    AttachThreadInput(foreThreadcapturedapp, appThread, false);
                                }
                            }
                        }


                        //set the window to a borderless style
                        const int GWL_STYLE = -16; //want to change the window style
                        const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                        IntPtr sult = SetWindowLongPtr(hWnd, GWL_STYLE, (UIntPtr)WindowStyles.WS_POPUP);// WS_POPUP);
                        if (sult == IntPtr.Zero)
                        {
                            //in some cases SWL just outright fails, so we can notify the user and abort
                            //MessageBox.Show("Unable to alter window style.\nSorry.");
                            //return;
                        }
                        //otherwise we need to resize and reposition the window to take up the full screen
                        const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                        screenWidth =  GetSystemMetrics(0);
                        screenHeight = GetSystemMetrics(1);
                        SetWindowPos(hWnd, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                        Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        /*
                        var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(hWnd, ref param);*/








                        /*
                        Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                        */
                        /*var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(hWnd, ref param);
                        */

                        var thisformhandlerect = new Rect();
                        GetWindowRect(currentform.Handle, out thisformhandlerect);

                        param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(currentform.Handle, ref param);


                        //TO READD
                        //TO READD
                        //TO READD
                        
                        currentform.Size = new System.Drawing.Size(screenWidth, screenHeight);
                        currentform.FormBorderStyle = FormBorderStyle.None;
                        currentform.WindowState = FormWindowState.Maximized;
                        currentform.TopMost = true;

                        //TO READD
                        //TO READD
                        //TO READD
                        


                        Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, this.Width, this.Height, Program.SWP_SHOWWINDOW);







                        //Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, thisformhandlerect.Left, thisformhandlerect.Top, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        //Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);
                        
                        
                        
                        this.checkBox1.Checked = true;



                        /*
                        if (this.checkBox1.Checked)
                        {
                            this.checkBox1.Checked = false;
                        }
                        else if (!this.checkBox1.Checked)
                        {
                            this.checkBox1.Checked = true;
                        }*/





                    }
                    else
                    {













                        var thisformhandlerect = new Rect();
                        GetWindowRect(currentform.Handle, out thisformhandlerect);


                        int screenWidth = Program.GetSystemMetrics(0);
                        int screenHeight = Program.GetSystemMetrics(1);

                        //SetWindowLong(_hWnd, Program.GWL_EXSTYLE, (IntPtr)(GetWindowLong(_hWnd, Program.GWL_EXSTYLE) | Program.WS_EX_TOPMOST));
                        Program.SetWindowPos(hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                        var param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        var therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(hWnd, ref param);



                        /*
                        param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(currentform.Handle, ref param);
                        */



                        /*//TO READD
                        //TO READD
                        //TO READD
                        
                        currentform.Size = new System.Drawing.Size(this.Width, this.Height);
                        currentform.FormBorderStyle = FormBorderStyle.None;
                        currentform.WindowState = FormWindowState.Normal;
                        currentform.TopMost = true;

                        //TO READD
                        //TO READD
                        //TO READD
                        
                        //Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, this.Width, this.Height, Program.SWP_SHOWWINDOW);
                        Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, thisformhandlerect.Left, thisformhandlerect.Top, this.Width, this.Height, Program.SWP_SHOWWINDOW);

                        */


                        //SetWindowLong(currentform.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                        /*param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(currentform.Handle, ref param);*/

                        thisformhandlerect = new Rect();
                        GetWindowRect(currentform.Handle, out thisformhandlerect);

                        param = new Program.WINDOWPLACEMENT();
                        param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                        therect = new Program.RECT();
                        therect.Left = 0;
                        therect.Top = 0;
                        therect.Bottom = screenHeight;
                        therect.Right = screenWidth;

                        param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                        param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                        Program.SetWindowPlacement(currentform.Handle, ref param);






                        //TO READD
                        //TO READD
                        //TO READD

                        currentform.Size = new System.Drawing.Size(screenWidth, screenHeight);
                        currentform.FormBorderStyle = FormBorderStyle.None;
                        currentform.WindowState = FormWindowState.Maximized;
                        currentform.TopMost = true;

                        //TO READD
                        //TO READD
                        //TO READD



                        Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        //Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, thisformhandlerect.Left, thisformhandlerect.Top, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                        //Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);

                    }









                    lastcapturedprogram = hWnd;
                }
            }
            return true;
        }

        //This function checks to see if a window is alt-tabbable.
        //It sometimes returns false positives for tray icons and similar, but
        //it's never caused any real problems.
        private bool isAltTabWindow(IntPtr hWnd)
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
        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;

            thepanel.Width = control.Width;
            thepanel.Height = control.Height;

            //These magic numbers are just sizes for margins in the form.
            //It seemed needlessly pedantic to give them their own variables in this case.
            listBox1.Size = new System.Drawing.Size(control.Size.Width - 40, control.Size.Height - 100);
            button1.Location = new System.Drawing.Point(control.Size.Width - 180, control.Size.Height - 82);
            button2.Location = new System.Drawing.Point(button2.Location.X, control.Size.Height - 82);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            if (trackbar != null)
            {
                trackbar.Focus();
                heightmapvalue = trackbar.Value;

                if (trackbar.Value < trackbar.Minimum + 1)
                {
                    trackbar.Value = trackbar.Minimum + 1;
                }

                if (trackbar.Value > trackbar.Maximum - 1)
                {
                    trackbar.Value = trackbar.Maximum - 1;
                }
            }

           
        }










        [DllImport("user32", EntryPoint = "GetMonitorInfo", CharSet = CharSet.Auto,
         SetLastError = true)]
        internal static extern bool GetMonitorInfoEx(IntPtr hMonitor, ref MonitorInfoEx lpmi);
        [DllImport("User32")]
        public static extern IntPtr MonitorFromWindow(IntPtr hWnd, int dwFlags);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        internal struct MonitorInfoEx
        {
            public int cbSize;
            public Rect rcMonitor;
            public Rect rcWork;
            public UInt32 dwFlags;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)] public string szDeviceName;
        }
       
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
        [DllImport("user32.dll")]
        protected static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        protected static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(int smIndex);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, UIntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) { return SetWindowLongPtr64(hWnd, nIndex, dwNewLong); }
            else { return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToUInt32())); }
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, uint dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, UIntPtr dwNewLong);


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

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetFocus(IntPtr hWnd);
        public static void SetFocus(IntPtr hwndTarget, string childClassName)
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
                SetFocus(hwndTarget); // hwndTarget);

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



        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();


        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);


        [DllImport("user32.dll")]
        static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern long GetWindowLongPtr(IntPtr hWnd, int nIndex);


        public static bool IsWindowMaximized(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_STYLE);
            return (exStyle & (long)WindowStyles.WS_MAXIMIZE) == (long)WindowStyles.WS_MAXIMIZE;
        }


        public static bool IsWindowMinimized(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_STYLE);
            return (exStyle & (long)WindowStyles.WS_MINIMIZE) == (long)WindowStyles.WS_MINIMIZE;
        }

        public static bool IsWindowTitlebar(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_STYLE);
            return (exStyle & (long)WindowStyles.WS_CAPTION) == (long)WindowStyles.WS_CAPTION;
        }

        public static bool IsWindowMaximizeBox(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_STYLE);
            return (exStyle & (long)WindowStyles.WS_MAXIMIZEBOX) == (long)WindowStyles.WS_MAXIMIZEBOX;
        }

        public static bool IsWindowOverlapped(IntPtr hWnd)
        {
            int GWL_STYLE = -16;

            int exStyle = GetWindowLong(hWnd, GWL_STYLE);
            return (exStyle & (long)WindowStyles.WS_OVERLAPPED) == (long)WindowStyles.WS_OVERLAPPED;
        }

        //https://stackoverflow.com/questions/36952600/determine-if-a-window-is-topmost-or-not
        /*[DllImport("user32.dll", SetLastError = true)]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        */
        //const int GWL_EXSTYLE = -20;
        //const int WS_EX_TOPMOST = 0x0008;

        public static bool IsWindowTopMost(IntPtr hWnd)
        {
            int exStyle = GetWindowLong(hWnd, GWL_EXSTYLE);
            return (exStyle & WS_EX_TOPMOST) == WS_EX_TOPMOST;
        }

     
        /// <summary>
        /// Window Styles.
        /// The following styles can be specified wherever a window style is required. After the control has been created, these styles cannot be modified, except as noted.
        /// </summary>
        [Flags()]
        public enum WindowStyles : uint
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
        [DllImport("user32.dll")]
        protected static extern bool IsWindowVisible(IntPtr hWnd);



  
        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);
        // When you don't want the ProcessId, use this overload and pass IntPtr.Zero for the second parameter
        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, IntPtr ProcessId);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hWnd, out Rect lpRect);
        [StructLayout(LayoutKind.Sequential)]
        public struct RECT { public int Left, Top, Right, Bottom; }

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
        }*/


        //https://www.pinvoke.net/default.aspx/kernel32.findfirstvolume
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern FindVolumeSafeHandle FindFirstVolume([Out] StringBuilder lpszVolumeName, uint cchBufferLength);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FindNextVolume(FindVolumeSafeHandle hFindVolume, [Out] StringBuilder lpszVolumeName, uint cchBufferLength);
        
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool FindVolumeClose(IntPtr hFindVolume);



        
        public class FindVolumeSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
        {
            private FindVolumeSafeHandle()
            : base(true)
            {
            }

            public FindVolumeSafeHandle(IntPtr preexistingHandle, bool ownsHandle)
            : base(ownsHandle)
            {
                SetHandle(preexistingHandle);
            }

            protected override bool ReleaseHandle()
            {
                return FindVolumeClose(handle);
            }
        }

        public static StringCollection GetVolumes()
        {
            const uint bufferLength = 1024;
            StringBuilder volume = new StringBuilder((int)bufferLength, (int)bufferLength);
            StringCollection ret = new StringCollection();

            using (FindVolumeSafeHandle volumeHandle = FindFirstVolume(volume, bufferLength))
            {
                if (volumeHandle.IsInvalid)
                {
                    throw new System.ComponentModel.Win32Exception(Marshal.GetLastWin32Error());
                }

                do
                {
                    ret.Add(volume.ToString());
                } while (FindNextVolume(volumeHandle, volume, bufferLength));

                return ret;
            }
        }



        public static int hasmaximizedinternal = 0;

        public static int hasmaximizedinit = 0;

        private void button3_Click(object sender, EventArgs e)
        {


            if (this.checkBox1.Checked)
            {
                //this.button3.Text = "Shrink";

                if (hasmaximizedinit == 0)
                {

                    int screenWidth = initminimizedwidth;// GetSystemMetrics(0);
                    int screenHeight = initminimizedheight;//GetSystemMetrics(1);
                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    var therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                    Program.SetWindowPlacement(currentform.Handle, ref param);

                    //TO READD
                    //TO READD
                    //TO READD

                    currentform.Size = new System.Drawing.Size(screenWidth, screenHeight);
                    currentform.FormBorderStyle = FormBorderStyle.None;
                    currentform.WindowState = FormWindowState.Maximized;
                    currentform.TopMost = true;

                    //TO READD
                    //TO READD
                    //TO READD

                    Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);


                    /*if (ismenuenabled == 1)
                    {

                    }
                    else if (ismenuenabled == 0)
                    {
                        SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

                    }*/

                    /*if (checkBox2.Checked)
                    {

                    }
                    else if (!checkBox2.Checked)
                    {
                        SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

                    }*/

                    hasmaximizedinit = 1;
                }
                else
                {

                    int screenWidth = GetSystemMetrics(0);
                    int screenHeight = GetSystemMetrics(1);
                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    var therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                    Program.SetWindowPlacement(currentform.Handle, ref param);

                    //TO READD
                    //TO READD
                    //TO READD

                    currentform.Size = new System.Drawing.Size(Program._desktopboundswidth, Program._desktopboundsheight);
                    currentform.FormBorderStyle = FormBorderStyle.None;
                    currentform.WindowState = FormWindowState.Maximized;
                    currentform.TopMost = true;

                    //TO READD
                    //TO READD
                    //TO READD

                    Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);


                    /*if (ismenuenabled == 1)
                    {

                    }
                    else if (ismenuenabled == 0)
                    {
                        SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

                    }

                    if (checkBox2.Checked)
                    {

                    }
                    else if (!checkBox2.Checked)
                    {
                        SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

                    }*/

                }






                hasmaximizedinternal = 0;


                this.checkBox1.Checked = false;
            }
            else if (!this.checkBox1.Checked)
            {
                //this.button3.Text = "Maximize";





                int screenWidth = GetSystemMetrics(0);
                int screenHeight = GetSystemMetrics(1);
                var param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                var therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = initminimizedheight;
                therect.Right = initminimizedwidth;

                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                Program.SetWindowPlacement(currentform.Handle, ref param);


                //TO READD
                //TO READD
                //TO READD

                currentform.Size = new System.Drawing.Size(initminimizedwidth, initminimizedheight);
                currentform.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                currentform.WindowState = FormWindowState.Normal;
                currentform.TopMost = true;

                //TO READD
                //TO READD
                //TO READD

                Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, initminimizedwidth, initminimizedheight, Program.SWP_SHOWWINDOW);



                hasmaximizedinternal = 1;


                this.checkBox1.Checked = true;

            }










            /*
            if (hasmaximizedinternal == 0)
            {
                this.button3.Text = "Maximize";




                int screenWidth = GetSystemMetrics(0);
                int screenHeight = GetSystemMetrics(1);
                var param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                var therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = initminimizedheight;
                therect.Right = initminimizedwidth;

                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                Program.SetWindowPlacement(currentform.Handle, ref param);


                //TO READD
                //TO READD
                //TO READD

                currentform.Size = new System.Drawing.Size(initminimizedwidth, initminimizedheight);
                currentform.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                currentform.WindowState = FormWindowState.Normal;
                currentform.TopMost = true;

                //TO READD
                //TO READD
                //TO READD

                Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, initminimizedwidth, initminimizedheight, Program.SWP_SHOWWINDOW);



                hasmaximizedinternal = 1;
            }
            else if (hasmaximizedinternal == 1)
            {
                this.button3.Text = "Shrink";


                if (hasmaximizedinit == 0)
                {

                    int screenWidth = initminimizedwidth;// GetSystemMetrics(0);
                    int screenHeight = initminimizedheight;//GetSystemMetrics(1);
                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    var therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                    Program.SetWindowPlacement(currentform.Handle, ref param);

                    //TO READD
                    //TO READD
                    //TO READD

                    currentform.Size = new System.Drawing.Size(screenWidth, screenHeight);
                    currentform.FormBorderStyle = FormBorderStyle.None;
                    currentform.WindowState = FormWindowState.Maximized;
                    currentform.TopMost = true;

                    //TO READD
                    //TO READD
                    //TO READD

                    Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);


                    /*if (ismenuenabled == 1)
                    {

                    }
                    else if (ismenuenabled == 0)
                    {
                        SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

                    }*/

            /*if (checkBox2.Checked)
            {

            }
            else if (!checkBox2.Checked)
            {
                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

            }

            hasmaximizedinit = 1;
        }
        else
        {

            int screenWidth = GetSystemMetrics(0);
            int screenHeight = GetSystemMetrics(1);
            var param = new Program.WINDOWPLACEMENT();
            param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

            var therect = new Program.RECT();
            therect.Left = 0;
            therect.Top = 0;
            therect.Bottom = screenHeight;
            therect.Right = screenWidth;

            param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
            param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
            Program.SetWindowPlacement(currentform.Handle, ref param);

            //TO READD
            //TO READD
            //TO READD

            currentform.Size = new System.Drawing.Size(Program._desktopboundswidth, Program._desktopboundsheight);
            currentform.FormBorderStyle = FormBorderStyle.None;
            currentform.WindowState = FormWindowState.Maximized;
            currentform.TopMost = true;

            //TO READD
            //TO READD
            //TO READD

            Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);


            /*if (ismenuenabled == 1)
            {

            }
            else if (ismenuenabled == 0)
            {
                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

            }

            if (checkBox2.Checked)
            {

            }
            else if (!checkBox2.Checked)
            {
                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED));

            }

        }






        hasmaximizedinternal = 0;
    }*/

        }




        //CHECKBOX2 IS FOR IS THE UI MENU ENABLED OR NOT
        //CHECKBOX2 IS FOR IS THE UI MENU ENABLED OR NOT
        //CHECKBOX2 IS FOR IS THE UI MENU ENABLED OR NOT


        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            //INTERNAL - ismenuenabled - return to game text
            //INTERNAL - ismenuenabled - return to game text
            //INTERNAL - ismenuenabled - return to game text
            //INTERNAL - ismenuenabled - return to game text
            //INTERNAL - ismenuenabled - return to game text
            //INTERNAL - ismenuenabled - return to game text
            //INTERNAL - ismenuenabled - return to game text
            //INTERNAL - ismenuenabled - return to game text
            //INTERNAL - ismenuenabled - return to game text

            if (this.checkBox2.Checked)
            {
                label2.Text = "rev.13135 - Made by Steve Chassé's Core Systems voxel virtual desktop high definition ! (sccs-vvd-hd). using sharpdx.directx11. made in C#"; 

                /*sccsvvdhd.Form1.thebutton3.Invoke((MethodInvoker)delegate
                {

                    sccsvvdhd.Form1.thebutton3.Text = "Shrink";
                });*/

                /*sccsvvdhd.Form1.checkbox1.Invoke((MethodInvoker)delegate
                {

                    sccsvvdhd.Form1.checkbox1.Checked = false;
                });*/


                /*this.listBox1.Visible = true;
                this.button1.Visible = true;
                this.button2.Visible = true;
                this.button3.Visible = true;
                //this.label2.Visible = true;
                this.comboBox1.Visible = true;
                //this.checkBox1.Visible = true;
                this.checkBox2.Visible = true;
                //this.CheckedListBox1.Visible = true;
                this.trackBar1.Visible = true;
                this.button4.Visible = true;
                this.checkBox3.Visible = true;
                this.button5.Visible = true;
                this.comboBox2.Visible = true;*/



                this.checkBox2.Visible = true;
                this.checkBox3.Visible = true;
                this.checkBox4.Visible = true;
                this.checkBox6.Visible = true;
                //this.checkBox7.Visible = true;
                this.checkBox8.Visible = true;

                //this.checkBox4.Visible = true;
                //this.checkBox5.Visible = true;
                //this.checkBox6.Visible = true;
                this.label1.Visible = true;
                this.label3.Visible = true;
                this.label4.Visible = true;
                this.label5.Visible = true;
                this.label6.Visible = true;

                this.numericUpDown1.Visible = true;
                this.numericUpDown2.Visible = true;

                this.listBox1.Visible = true;

                this.button1.Visible = true;
                this.button2.Visible = true;
                this.button3.Visible = true;
                this.button4.Visible = true;
                this.button5.Visible = true;

                //this.label2.Visible = true;
                this.comboBox1.Visible = true;
                this.comboBox2.Visible = true;
                this.comboBox3.Visible = true;
                this.comboBox4.Visible = true;
                this.comboBox5.Visible = true;

                //this.checkBox1.Visible = true;
                this.CheckedListBox1.Visible = true;
                this.trackBar1.Visible = true;
                this.trackBar2.Visible = true;
                this.trackBar3.Visible = true;
                //this.trackBar4.Visible = true;

                if (checkBox6.Checked)
                {
                    this.trackBar4.Visible = true;
                    checkdirtyrgbchunkoptionvalue = 1;
                }
                else if (!checkBox6.Checked)
                {
                    this.trackBar4.Visible = false;
                    checkdirtyrgbchunkoptionvalue = 0;
                }

                this.button6.Visible = true;
                this.button7.Visible = true;
                this.button8.Visible = true;

                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DqISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ // program.cs //Lines 1663-1701 and Lines 2056-2127
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ // program.cs //Lines 1663-1701 and Lines 2056-2127
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ // program.cs //Lines 1663-1701 and Lines 2056-2127
                this.pictureBox1.Visible = true;
                this.pictureBox2.Visible = true;
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ // program.cs //Lines 1663-1701 and Lines 2056-2127
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ // program.cs //Lines 1663-1701 and Lines 2056-2127
                //TO BE REINCORPORATED. DOESN'T WORK. AN IMAGE IS DISPLAYED AND NOT UPDATED. BUT WRITING 2-3 TIMES ON TOP MAKES IT WORK? MADE BY STEVE CHASSÉ // program.cs //Lines 1663-1701 and Lines 2056-2127




                if (this.checkBox8.Checked)
                {
                    this.CheckedListBox1.Visible = true;
                }
                else
                {
                    this.CheckedListBox1.Visible = false;

                }









                /*
                if (this.checkBox2.Checked)
                {
                    if (this.checkBox8.Checked)
                    {
                        this.CheckedListBox1.Visible = false;
                    }
                    else if (!this.checkBox8.Checked)
                    {
                        this.CheckedListBox1.Visible = true;
                    }
                }*/

                /*
                if (this.checkBox2.Checked)
                {
                    checkBox8.Visible = false;
                }
                else if (!this.checkBox2.Checked)
                {
                    if (this.checkBox2.Checked)
                    {
                        checkBox8.Visible = false;
                    }
                    else if (!this.checkBox2.Checked)
                    {
                        checkBox8.Visible = true;
                    }
                }*/







                //set the window to a borderless style
                const int GWL_STYLE = -16; //want to change the window style
                /*const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                IntPtr sult = SetWindowLongPtr(this.Handle, GWL_STYLE, (UIntPtr)WS_POPUP);
                if (sult == IntPtr.Zero)
                {
                    //in some cases SWL just outright fails, so we can notify the user and abort
                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                    return;
                }*/
                //otherwise we need to resize and reposition the window to take up the full screen
                /*const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                int screenWidth = GetSystemMetrics(0);
                int screenHeight = GetSystemMetrics(1);
                SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                //SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TOPMOST));
                SetWindowLong(this.Handle, GWL_STYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_STYLE) | WS_EX_TOPMOST));
                */




                if (checkBox1.Checked)
                {
                    int screenWidth = GetSystemMetrics(0);
                    int screenHeight = GetSystemMetrics(1);

                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    var therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                    Program.SetWindowPlacement(currentform.Handle, ref param);

                    //TO READD
                    //TO READD
                    //TO READD

                    currentform.Size = new System.Drawing.Size(screenWidth, screenHeight);
                    currentform.FormBorderStyle = FormBorderStyle.None;
                    currentform.WindowState = FormWindowState.Maximized;
                    currentform.TopMost = true;

                    //TO READD
                    //TO READD
                    //TO READD
                    var rectform = new Program.RECT();
                    Program.GetWindowRect(currentform.Handle, ref rectform);

                    Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, rectform.Left, rectform.Top, screenWidth, screenHeight, Program.SWP_SHOWWINDOW);


                }
                else if (!checkBox1.Checked)
                {

                    var param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                    var therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = this.Height;
                    therect.Right = this.Width;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                    Program.SetWindowPlacement(currentform.Handle, ref param);

                    //TO READD
                    //TO READD
                    //TO READD

                    currentform.Size = new System.Drawing.Size(this.Width, this.Height);
                    currentform.FormBorderStyle = FormBorderStyle.SizableToolWindow;
                    currentform.WindowState = FormWindowState.Normal;
                    currentform.TopMost = true;

                    //TO READD
                    //TO READD
                    //TO READD
                    var rectform = new Program.RECT();
                    Program.GetWindowRect(currentform.Handle, ref rectform);

                    Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, rectform.Left, rectform.Top, this.Width, this.Height, Program.SWP_SHOWWINDOW);


                }




                //ismenuenabled = 1;
                //this.checkBox2.Checked = false;
            }
            else if (!this.checkBox2.Checked)
            {
                label2.Text = "Notes:" + "\n" + " Press F9 to show menu! ";// + "\n" + "Press NumpadEnter to reset Camera Angle!";


                /*sccsvvdhd.Form1.thebutton3.Invoke((MethodInvoker)delegate
                {

                    sccsvvdhd.Form1.thebutton3.Text = "Maximize";
                });*/
                //this.button3.Text = "Maximize";


                //sccs.Program.updatescript.canmovecamera = 1;

                /*if (checkBox3.Checked)
                {
                    checkBox3.Checked = false;
                }
                else if (!checkBox3.Checked)
                {
                    checkBox3.Checked = true;
                }*/



                //LOCK CAMERA WHEN THERE ISNT A MENU OTHERWISE MOUSE CLICKS AND KEYBOARD CLICKS WILL BE TRANSMITTED TO CAPTURED APP.
                checkBox3.Checked = true;
                //LOCK CAMERA WHEN THERE ISNT A MENU OTHERWISE MOUSE CLICKS AND KEYBOARD CLICKS WILL BE TRANSMITTED TO CAPTURED APP.

                this.checkBox2.Visible = false;
                this.checkBox3.Visible = false;
                this.checkBox4.Visible = false;
                this.checkBox6.Visible = false;
                //this.checkBox7.Visible = false;
                this.checkBox8.Visible = false;


                //this.checkBox4.Visible = false;
                //this.checkBox5.Visible = false;
                //this.checkBox6.Visible = false;
                this.label1.Visible = false;
                this.label3.Visible = false;
                this.label4.Visible = false;
                this.label5.Visible = false;
                this.label6.Visible = false;

                this.numericUpDown1.Visible = false;
                this.numericUpDown2.Visible = false;

                this.listBox1.Visible = false;

                this.button1.Visible = false;
                this.button2.Visible = false;
                this.button3.Visible = false;
                this.button4.Visible = false;
                this.button5.Visible = false;

                //this.label2.Visible = false;
                this.comboBox1.Visible = false;
                this.comboBox2.Visible = false;
                this.comboBox3.Visible = false;
                this.comboBox4.Visible = false;
                this.comboBox5.Visible = false;

                //this.checkBox1.Visible = false;
                this.CheckedListBox1.Visible = false;
                this.trackBar1.Visible = false;
                this.trackBar2.Visible = false;
                this.trackBar3.Visible = false;
                this.trackBar4.Visible = false;

                this.button6.Visible = false;
                this.button7.Visible = false;
                this.button8.Visible = false;


                this.pictureBox1.Visible = false;
                this.pictureBox2.Visible = false;

                int sizex = GetSystemMetrics(0);
                int sizey = GetSystemMetrics(1);

                this.Size = new System.Drawing.Size(sizex, sizey);




                /*if (this.checkBox2.Checked)
                {
                    checkBox8.Visible = false;
                }
                else if (!this.checkBox2.Checked)
                {
                    if (this.checkBox2.Checked)
                    {
                        checkBox8.Visible = false;
                    }
                    else if (!this.checkBox2.Checked)
                    {
                        checkBox8.Visible = true;
                    }
                }*/
                /* this.FormBorderStyle = FormBorderStyle.None;
                 this.WindowState = FormWindowState.Maximized;
                 SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
                 SetLayeredWindowAttributes(this.Handle, 0, 255, LWA_ALPHA);
                */
                /*
                this.AllowTransparency = true;
                this.TransparencyKey = System.Drawing.Color.LightGray;
                this.BackColor = System.Drawing.Color.LightGray;

                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;*/
                /*Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));


                Program.RECT therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = sizey;
                therect.Right = sizex;

                param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(Program.vewindowsfoundedz, ref param);

                Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, sizex, sizey, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);




                this.Size = new System.Drawing.Size(sizex, sizey);
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Maximized;
                this.TopMost = true;
                Program.SetWindowPos(this.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, sizex, sizey, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));
                */





                SetWindowLong(capturedprogram, GWL_EXSTYLE, (IntPtr)(GetWindowLong(capturedprogram, GWL_EXSTYLE) | WS_EX_TOPMOST));
                Program.SetWindowPos(capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, Program._desktopboundswidth, Program._desktopboundsheight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);



                var param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                var therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = this.Height;
                therect.Right = this.Width;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                Program.SetWindowPlacement(currentform.Handle, ref param);


                //TO READD
                //TO READD
                //TO READD

                currentform.Size = new System.Drawing.Size(this.Width, this.Height);
                currentform.FormBorderStyle = FormBorderStyle.None;
                currentform.WindowState = FormWindowState.Maximized;
                currentform.TopMost = true;

                //TO READD
                //TO READD
                //TO READD

                Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, this.Width, this.Height, Program.SWP_SHOWWINDOW);

                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));



                /*
                var param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));

                var therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = this.Height;
                therect.Right = this.Width;

                param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                Program.SetWindowPlacement(currentform.Handle, ref param);


                //TO READD
                //TO READD
                //TO READD

                currentform.Size = new System.Drawing.Size(this.Width, this.Height);
                currentform.FormBorderStyle = FormBorderStyle.None;
                currentform.WindowState = FormWindowState.Maximized;
                currentform.TopMost = true;

                //TO READD
                //TO READD
                //TO READD

                Program.SetWindowPos(currentform.Handle, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, this.Width, this.Height, Program.SWP_SHOWWINDOW);


                SetWindowLong(this.Handle, GWL_EXSTYLE, (IntPtr)(GetWindowLong(this.Handle, GWL_EXSTYLE) | WS_EX_TRANSPARENT | WS_EX_LAYERED | WS_EX_TOPMOST));

                */


                //this.checkBox2.Checked = true;
                //ismenuenabled = 1;
            }



























            /*
            if (this.checkBox2.Checked)
            {
                ismenuenabled = 0;
                this.checkBox2.Checked = false;
            }
            else if (!this.checkBox2.Checked)
            {
                ismenuenabled = 1;
                this.checkBox2.Checked = true;
            }*/
        }

        private void button4_Click(object sender, EventArgs e)
        {

            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            /*if (Program.last_hWnd != IntPtr.Zero)
            {

                int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);

                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                Program.RECT therect = new Program.RECT();

                //if (Program.vewindowsfoundedz != IntPtr.Zero)
                {

                    param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                    therect = new Program.RECT();



                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.ShowMaximized; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    /*Program.SetWindowPlacement(last_hWnd, ref param);
                   


                    Program.SetWindowPos(Program.last_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    Program.SetWindowPos(Program.last_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                    DeleteObject(Program.last_hWnd);
                    GC.SuppressFinalize(Program.last_hWnd);

                    
                    Program.SetWindowPos(Program.vewindowsfoundedz, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);

                }
            }*/

            if (capturedprogram != IntPtr.Zero)
            {

                int screenWidth = Program.GetSystemMetrics(0);
                int screenHeight = Program.GetSystemMetrics(1);

                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                Program.RECT therect = new Program.RECT();

                //if (capturedprogram != IntPtr.Zero)
                {


                    if (SelectedTitle != null)
                    {
                        if (!SelectedTitle.ToLower().Contains("microsoft") || !SelectedTitle.ToLower().Contains("edge"))
                        {

                        }
                        else
                        {
                            if (SelectedTitle.ToLower().Contains("microsoft") && SelectedTitle.ToLower().Contains("edge"))
                            {
                                //set the window to a borderless style
                                const int GWL_STYLE = -16; //want to change the window style
                                const UInt32 WS_POPUP = 0x80000000; //to WS_POPUP, which is a window with no border
                                IntPtr sult = SetWindowLongPtr(capturedprogram, GWL_STYLE, (UIntPtr)(WindowStyles.WS_SIZEFRAME | WindowStyles.WS_CAPTION | WindowStyles.WS_VISIBLE | WindowStyles.WS_GROUP | WindowStyles.WS_VSCROLL | WindowStyles.WS_SYSMENU | WindowStyles.WS_MAXIMIZEBOX));// WS_POPUP);
                                if (sult == IntPtr.Zero)
                                {
                                    //in some cases SWL just outright fails, so we can notify the user and abort
                                    //MessageBox.Show("Unable to alter window style.\nSorry.");
                                    //return;
                                }
                                //otherwise we need to resize and reposition the window to take up the full screen
                                const uint SWP_SHOWWINDOW = 0x40; //this flag will cause SetWindowPos to refresh the state of the window so that the changes made will become active
                                screenWidth = GetSystemMetrics(0);
                                screenHeight = GetSystemMetrics(1);
                                SetWindowPos(capturedprogram, IntPtr.Zero, 0, 0, screenWidth, screenHeight, SWP_SHOWWINDOW);

                            }
                        }
                    }
                   
                    /*
                    SetWindowLong(capturedprogram, GWL_EXSTYLE, (IntPtr)(GetWindowLong(capturedprogram, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST));
                    */
                    /*
                    SetWindowLong(capturedprogram, GWL_EXSTYLE, (IntPtr)(GetWindowLong(capturedprogram, GWL_EXSTYLE) | WS_EX_LAYERED | WS_EX_OVERLAPPEDWINDOW | WS_EX_TOPMOST | WS_EX_TRANSPARENT));
                    SetLayeredWindowAttributes(capturedprogram, 0, 255, LWA_ALPHA);
                    */






                    param = new Program.WINDOWPLACEMENT();
                    param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                    therect = new Program.RECT();


                    /*
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.Normal; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                    *//*Program.SetWindowPlacement(last_hWnd, ref param);
                   */
                    



                    
                    therect = new Program.RECT();
                    therect.Left = 0;
                    therect.Top = 0;
                    therect.Bottom = screenHeight;
                    therect.Right = screenWidth;

                    param.ShowCmd = Program.ShowWindowCommands.Maximize; //SW_SHOWNORMAL
                    param.NormalPosition = therect;// new RECT(0, 0, 500, 500);                   
                    Program.SetWindowPlacement(capturedprogram, ref param);



                    Program.SetWindowPos(capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_TOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    Program.SetWindowPos(capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);
                    
                    DeleteObject(capturedprogram);
                    GC.SuppressFinalize(capturedprogram);

                    /*
                    Program.SetWindowPos(capturedprogram, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, (uint)Program.SetWindowPosFlags.SWP_SHOWWINDOW);*/

                }
            }
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM
            //TO RETURN THE CAPTURED PROGRAM TO IT'S ORIGINAL STATE WHEN QUITTING THIS PROGRAM




            /*button1exit.PerformClick();
            Program.GetCursorPos(out somepoint);

            mousex = somepoint.X;
            mousey = somepoint.Y;


            int iHandle = scgraphicssec.FindWindow(null, Program.capturedwindowname);
            scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONDOWN, 0, (mousex << 16) | mousey);
            scgraphicssec.SendMessage(iHandle, scgraphicssec.WM_RBUTTONUP, 0, 0);

            //button1exit.Select();
            //button1exit.Focus();

            button1exit.PerformClick();
            button1exit.Update();

            Console.WriteLine("onbuttonmouseclick");*/
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
            

            if (Program.updatescript.exitthread0 == 0)
            {
                Program.updatescript.exitthread0 = 1;
            }
            if (Program.updatescript.exitthread1 == 0)
            {
                Program.updatescript.exitthread1 = 1;
            }
            Program.exitedprogram = 1;



            System.Windows.Forms.Application.ExitThread();
            System.Windows.Forms.Application.Exit();
            var process = System.Diagnostics.Process.GetCurrentProcess();


            process.Kill();
            process.Dispose();


            CloseWindow(sccsvvdhd.Form1.someform.Handle);
            sccsvvdhd.Form1.someform.Close();
            sccsvvdhd.Form1.someform.Dispose();

            InputSimulator inputsim = new InputSimulator();

            //InputSimulator.SimulateModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);
            inputsim.Keyboard.ModifiedKeyStroke(VirtualKeyCode.LWIN, VirtualKeyCode.VK_E);

            inputsim = null;

            System.Environment.Exit(1);

            System.Environment.Exit(0);












        }



        //CHECKBOX2 IS FOR "IS THE UI MENU ENABLED OR NOT"
        //CHECKBOX2 IS FOR "IS THE UI MENU ENABLED OR NOT"
        //CHECKBOX2 IS FOR "IS THE UI MENU ENABLED OR NOT"

        [return: MarshalAs(UnmanagedType.Bool)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [DllImport("user32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern bool CloseWindowStation(IntPtr hWinsta);

        [DllImport("user32.dll")]
        static extern bool CloseWindow(IntPtr hWnd);




        public int canmovethecamera = 0;

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {




            if (this.checkBox3.Checked)
            {
                this.checkBox3.Text = "Camera Locked";

                canmovethecamera = 1;

                /*if (sccs.Program.updatescript != null)
                {
                    sccs.Program.updatescript.canmovecamera = 1;

                }*/

            }
            else if (!this.checkBox3.Checked)
            {
                this.checkBox3.Text = "Camera Unlocked";
                /*if (sccs.Program.updatescript != null)
                {
                    sccs.Program.updatescript.canmovecamera = 0;

                }*/
                canmovethecamera = 0;

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Program.updatescript.resetcamera();

            if (this.checkBox3.Checked)
            {
                this.checkBox3.Text = "Camera Locked";

                if (sccs.Program.updatescript != null)
                {
                    sccs.Program.updatescript.canmovecamera = 1;

                }

            }
            else if (!this.checkBox3.Checked)
            {
                this.checkBox3.Text = "Camera Unlocked";
                if (sccs.Program.updatescript != null)
                {
                    sccs.Program.updatescript.canmovecamera = 0;

                }

            }
        }




        private void button6_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            //MyDialog.Color = textBox1.ForeColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                //textBox1.ForeColor = MyDialog.Color;
                cursorColor = MyDialog.Color;
            }

        
        }




        public static System.Drawing.Color cursorColor;
        public static System.Drawing.Color gridColor;


        private void button7_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            //MyDialog.Color = textBox1.ForeColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                //textBox1.ForeColor = MyDialog.Color;
                gridColor = MyDialog.Color;
            }

        
        }



        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

            if ((int)numericUpDown1.Value >= 8)
            {
                numericUpDown1.Value = 0;
            }

            if ((int)numericUpDown1.Value <= -1)
            {
                numericUpDown1.Value = 7;
            }


            /*if ((int)numericUpDown1.Value >= 8)
            {
                numericUpDown1.Value = 0;
            }*/

            gridtypeoption = (int)numericUpDown1.Value;

            label2.Focus();


        }



        public int screencapturecolororgrayscale = 0;
        /*private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {
            /*if (this.checkBox6.Checked)
            {
                this.checkBox6.Text = "VVD Grayscale";
                screencapturecolororgrayscale = 1;
            }
            else
            {
                this.checkBox6.Text = "VVD Color";
                screencapturecolororgrayscale = 0;
            }
        }*/

        /*private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBox4.Checked)
            {
                cursorlightoption = 1;
            }
            else
            {
                cursorlightoption = 0;
            }
        }*/



        public int isgridenabled = 0;
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            /*if (this.checkBox5.Checked)
            {
                isgridenabled = 1;
                //gridtypeoption = 1;
            }
            else
            {
                isgridenabled = 0;
                //gridtypeoption = 0;
            }*/
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }



        public int voxelvirtualdesktoptypeswtc = 0;
        public int voxelvirtualdesktoptype = 0;
        public int voxelvirtualdesktoptypemax = 3;
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if ((int)numericUpDown2.Value >= voxelvirtualdesktoptypemax)
            {
                numericUpDown2.Value = 0;
            }

            if ((int)numericUpDown2.Value <= -1)
            {
                numericUpDown2.Value = voxelvirtualdesktoptypemax - 1;
            }


            /*if ((int)numericUpDown1.Value >= 8)
            {
                numericUpDown1.Value = 0;
            }*/

            voxelvirtualdesktoptype = (int)numericUpDown2.Value;

            voxelvirtualdesktoptypeswtc = 1;

            label2.Focus();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }





        //public int counterscreencapturechanged = -1;
        public int sccsvvdhdgrayscaleorcolored = 0;
        public int lastsccsvvdhdgrayscaleorcolored = 0;

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {



            ComboBox comboBox = (ComboBox)sender;
            string selectedscreencaptureindex = (string)comboboxcapturelist3.SelectedItem;
            sccsvvdhdgrayscaleorcolored = comboboxcapturelist3.FindStringExact(selectedscreencaptureindex);

            if (sccsvvdhdgrayscaleorcolored != lastsccsvvdhdgrayscaleorcolored)
            {
                if (sccsvvdhdgrayscaleorcolored == 0 || sccsvvdhdgrayscaleorcolored == 1)
                {
                    
                    //trackbar2.TickFrequency = 1;
                    /*trackbar2.Minimum = lightintensityvaluemin;
                    trackbar2.Maximum = lightintensityvaluemax;*/
                    /*trackbar2.Value = 155;
                    trackbar3.Value = 50;*/

                    /*trackbar2.Value = 155;
                    trackbar3.Value = 50;*/

                    //trackbar2.Value = 10;
                    //trackbar3.Value = 10;
                }
                else if (sccsvvdhdgrayscaleorcolored == 2)
                {
                   
                    //trackbar2.TickFrequency = 1;
                    //trackbar2.Minimum = -25;
                    //trackbar2.Maximum = 100;

                    /*trackbar2.Value = 50;
                    trackbar3.Value = 755;*/

                    //trackbar2.Value = 10;
                    //trackbar3.Value = 10;
                }


                if (sccsvvdhdgrayscaleorcolored == 0 || sccsvvdhdgrayscaleorcolored == 1)
                {
                   
                    //trackbar3.TickFrequency = 1;
                    /*trackbar3.Minimum = screencapturebrightnessvaluemin;
                    trackbar3.Maximum = screencapturebrightnessvaluemax;*/

                    /*trackbar2.Value = 155;
                    trackbar3.Value = 755;*/

                    //trackbar2.Value = 10;
                    //trackbar3.Value = 10;
                }
                else if (sccsvvdhdgrayscaleorcolored == 2)
                {
                   
                    //trackbar3.TickFrequency = 1;
                    //trackbar3.Minimum = -25;
                    //trackbar3.Maximum = 100;


                    //trackbar2.Value = 10;
                    //trackbar3.Value = 10;

                    /*trackbar2.Value = 50;
                    trackbar3.Value = 65;*/
                }




                //Program.screencapturetype = sccsvvdhdgrayscaleorcolored;
                //Program.changedscreencapturetype = 1;

                if (sccsvvdhdgrayscaleorcolored == 0)
                {
                    //this.checkBox6.Text = "VVD Color 5 Faces";
                    screencapturecolororgrayscale = 0;                  
                }
                else if (sccsvvdhdgrayscaleorcolored == 1)
                {
                    //this.checkBox6.Text = "VVD Front face Colored and 4 faces Grayscale";
                    screencapturecolororgrayscale = 1;
                }
                else if (sccsvvdhdgrayscaleorcolored == 2)
                {
                    //this.checkBox6.Text = "VVD Grayscale";
                    screencapturecolororgrayscale = 2;
                }

                /*if (this.checkBox6.Checked)
                {
                    this.checkBox6.Text = "VVD Grayscale";
                    screencapturecolororgrayscale = 1;
                }
                else
                {
                    this.checkBox6.Text = "VVD Color";
                    screencapturecolororgrayscale = 0;
                }*/


                lastsccsvvdhdgrayscaleorcolored = sccsvvdhdgrayscaleorcolored;
                //sccs.Program.MessageBox((IntPtr)0, "combobox screencapture type changed!", "scmsg", 0);
                //thepanel.Focus();
                //comboboxcapturelist.
            }

            //label2.Focus();



            /*ComboBox comboBox = (ComboBox)sender;
            string selectedscreencaptureindex = (string)this.comboBox2.SelectedItem;
            projectiontypeindex = this.comboBox2.FindStringExact(selectedscreencaptureindex);


            if (projectiontypeindex != lastprojectiontypeindex)
            {

            }*/



            //Console.WriteLine("index:" + projectiontypeindex);



            /*if (screencaptureindextype != lastscreencaptureindextype)
            {
                Program.screencapturetype = screencaptureindextype;
                Program.changedscreencapturetype = 1;

                lastscreencaptureindextype = screencaptureindextype;
                //sccs.Program.MessageBox((IntPtr)0, "combobox screencapture type changed!", "scmsg", 0);
                //thepanel.Focus();
                //comboboxcapturelist.
            }*/

            lastprojectiontypeindex = projectiontypeindex;
            label2.Focus();
        }




        public int sccsvvdhdpointlighttype = 0;
        public int lastsccsvvdhdpointlighttype = 0;
        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedscreencaptureindex = (string)comboboxcapturelist4.SelectedItem;
            sccsvvdhdpointlighttype = comboboxcapturelist4.FindStringExact(selectedscreencaptureindex);

            if (sccsvvdhdpointlighttype != lastsccsvvdhdpointlighttype)
            {
                //Program.screencapturetype = sccsvvdhdgrayscaleorcolored;
                //Program.changedscreencapturetype = 1;

                if (sccsvvdhdpointlighttype == 0)
                {
                    //this.checkBox6.Text = "VVD Color 5 Faces";
                    screencapturecolororgrayscale = 0;
                }
                else if (sccsvvdhdpointlighttype == 1)
                {
                    //this.checkBox6.Text = "VVD Front face Colored and 4 faces Grayscale";
                    screencapturecolororgrayscale = 1;
                }
                else if (sccsvvdhdpointlighttype == 2)
                {
                    //this.checkBox6.Text = "VVD Grayscale";
                    screencapturecolororgrayscale = 2;
                }
                else if (sccsvvdhdpointlighttype == 3)
                {
                    //this.checkBox6.Text = "VVD Grayscale";
                    screencapturecolororgrayscale = 3;
                }
                /*if (this.checkBox6.Checked)
                {
                    this.checkBox6.Text = "VVD Grayscale";
                    screencapturecolororgrayscale = 1;
                }
                else
                {
                    this.checkBox6.Text = "VVD Color";
                    screencapturecolororgrayscale = 0;
                }*/


                //sccs.Program.MessageBox((IntPtr)0, "combobox screencapture type changed!", "scmsg", 0);
                //thepanel.Focus();
                //comboboxcapturelist.
            }

            //label2.Focus();

            lastsccsvvdhdpointlighttype = sccsvvdhdpointlighttype;


            /*ComboBox comboBox = (ComboBox)sender;
            string selectedscreencaptureindex = (string)this.comboBox2.SelectedItem;
            projectiontypeindex = this.comboBox2.FindStringExact(selectedscreencaptureindex);


            if (projectiontypeindex != lastprojectiontypeindex)
            {

            }*/



            //Console.WriteLine("index:" + projectiontypeindex);



            /*if (screencaptureindextype != lastscreencaptureindextype)
            {
                Program.screencapturetype = screencaptureindextype;
                Program.changedscreencapturetype = 1;

                lastscreencaptureindextype = screencaptureindextype;
                //sccs.Program.MessageBox((IntPtr)0, "combobox screencapture type changed!", "scmsg", 0);
                //thepanel.Focus();
                //comboboxcapturelist.
            }*/

            label2.Focus();
        }

   




        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            if (trackbar2 != null)
            {
                /*if (sccsvvdhdgrayscaleorcolored != lastsccsvvdhdgrayscaleorcolored)
                {
                    if (sccsvvdhdgrayscaleorcolored == 0 || sccsvvdhdgrayscaleorcolored == 1)
                    {
                        //trackbar2.TickFrequency = 1;
                        trackbar2.Minimum = lightintensityvaluemin;
                        trackbar2.Maximum = lightintensityvaluemax;
                    }
                    else if (sccsvvdhdgrayscaleorcolored == 2)
                    {
                        //trackbar2.TickFrequency = 1;
                        trackbar2.Minimum = -25;
                        trackbar2.Maximum = 100;
                    }
                }*/

                /*if (sccsvvdhdgrayscaleorcolored == 2)
                {
                    if (sccsvvdhdgrayscaleorcolored != lastsccsvvdhdgrayscaleorcolored)
                    {
                        if (trackbar2.Minimum == -25 || trackbar2.Maximum == 100)
                        {
                            if (trackbar2.Value < -25 || trackbar2.Value > 100)
                            {
                                if (trackbar2.Value < -25)
                                {
                                    trackbar2.Value = -25 - 1;
                                }
                                else if (trackbar2.Value > 100)
                                {
                                    trackbar2.Value = 100 - 1;
                                }
                            }
                        }
                    }
                }
                else
                {

                }*/

                

                trackbar2.Focus();

                lightintensityvalue = trackbar2.Value;


                if (trackBar2.Value < trackBar2.Minimum + 1)
                {
                    trackBar2.Value = trackBar2.Minimum + 1;
                }

                if (trackBar2.Value > trackBar2.Maximum - 1)
                {
                    trackBar2.Value = trackBar2.Maximum - 1;
                }



            }

            lightintensityvalueswtc = 1;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
   
        private void trackBar3_Scroll(object sender, EventArgs e)
        {
            if (trackbar3 != null)
            {





                /*//if (sccsvvdhdgrayscaleorcolored != lastsccsvvdhdgrayscaleorcolored)
                {
                    if (sccsvvdhdgrayscaleorcolored == 0 || sccsvvdhdgrayscaleorcolored == 1)
                    {
                        //trackbar3.TickFrequency = 1;
                        trackbar3.Minimum = screencapturebrightnessvaluemin;
                        trackbar3.Maximum = screencapturebrightnessvaluemax;
                    }
                    else if (sccsvvdhdgrayscaleorcolored == 2)
                    {
                        trackbar3.Value = 0;
                        //trackbar3.TickFrequency = 1;
                        trackbar3.Minimum = -25;
                        trackbar3.Maximum = 100;
                    }
                }*/

                /*if (sccsvvdhdgrayscaleorcolored == 2)
                {
                    if (sccsvvdhdgrayscaleorcolored != lastsccsvvdhdgrayscaleorcolored)
                    {
                        if (trackbar3.Minimum == -25 || trackbar3.Maximum == 100)
                        {
                            if (trackbar3.Value < -25 || trackbar3.Value > 100)
                            {
                                if (trackbar3.Value < -25)
                                {
                                    trackbar3.Value = -25 - 1;
                                }
                                else if (trackbar3.Value > 100)
                                {
                                    trackbar3.Value = 100 - 1;
                                }
                            }
                        }
                    }
                }
                else
                {

                }*/
                

                trackbar3.Focus();

                screencapturebrightnessvalue = trackbar3.Value;


                /*if (trackbar3.Value < trackbar3.Minimum + 1)
                {
                    trackbar3.Value = trackbar3.Minimum + 1;
                }

                if (trackbar3.Value > trackbar3.Maximum - 1)
                {
                    trackbar3.Value = trackbar3.Maximum - 1;
                }*/







                /*
                trackbar3.Focus();
                screencapturebrightnessvalue = trackbar3.Value;
                */

                /*if (sccsvvdhdgrayscaleorcolored == 0 || sccsvvdhdgrayscaleorcolored == 1)
                {
                    trackbar3.TickFrequency = 1;
                    trackbar3.Minimum = screencapturebrightnessvaluemin;
                    trackbar3.Maximum = screencapturebrightnessvaluemax;
                }
                else if (sccsvvdhdgrayscaleorcolored == 2)
                {
                    trackbar3.TickFrequency = 1;
                    trackbar3.Minimum = -25;
                    trackbar3.Maximum = 100;
                }

                trackbar3.Focus();
                screencapturebrightnessvalue = trackbar3.Value;
                */






                if (trackbar3.Value < trackbar3.Minimum + 1)
                {
                    trackbar3.Value = trackbar3.Minimum + 1;
                }

                if (trackbar3.Value > trackbar3.Maximum - 1)
                {
                    trackbar3.Value = trackbar3.Maximum - 1;
                }



            }

            screencapturebrightnessvalueswtc = 1;
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        public int sccsvvdhdresolutionvalueswtc = 0;
        public int sccsvvdhdresolutionvalue = 0;
        public int lastsccsvvdhdresolutionvalue = 0;
        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedscreencaptureindex = (string)comboboxcapturelist5.SelectedItem;
            sccsvvdhdresolutionvalue = comboboxcapturelist5.FindStringExact(selectedscreencaptureindex);

            if (sccsvvdhdresolutionvalue != lastsccsvvdhdresolutionvalue)
            {
                /*if (sccsvvdhdresolutionvalue == 0)
                {
                    //this.checkBox6.Text = "VVD Color 5 Faces";
                    screencapturecolororgrayscale = 0;
                }
                else if (sccsvvdhdresolutionvalue == 1)
                {
                    //this.checkBox6.Text = "VVD Front face Colored and 4 faces Grayscale";
                    screencapturecolororgrayscale = 1;
                }*/

            }

            //label2.Focus();
            sccsvvdhdresolutionvalueswtc = 1;




            lastsccsvvdhdresolutionvalue = sccsvvdhdresolutionvalue;



            label2.Focus();
        }



        public int verticalsyncoptionvalueswtc = 0;
        public int verticalsyncoptionvalue = 0;
        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox4.Checked)
            {
                verticalsyncoptionvalue = 1;
            }
            else if (!checkBox4.Checked)
            {
                verticalsyncoptionvalue = 0;
            }

            verticalsyncoptionvalueswtc = 1;
            //Console.WriteLine(verticalsyncoptionvalue);
        }




        public int checkdirtyrgbchunkoptionvalueswtc = 0;
        public int checkdirtyrgbchunkoptionvalue = 0;
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox6.Checked)
            {
                pictureBox1.Visible = true;
                trackBar4.Visible = true;
                checkdirtyrgbchunkoptionvalue = 1;
            }
            else if (!checkBox6.Checked)
            {
                pictureBox1.Visible = false;
                trackBar4.Visible = false;
                checkdirtyrgbchunkoptionvalue = 0;
            }

            checkdirtyrgbchunkoptionvalueswtc = 1;
            //Console.WriteLine(verticalsyncoptionvalue);
        }


        public int precisiondirtyrgbchunklidervalueswtc = 0;
        public float precisiondirtyrgbchunklidervalue;
        public int precisiondirtyrgbchunklidervaluemax = 10000;
        public int precisiondirtyrgbchunklidervaluemin = 1;
        public int precisiondirtyrgbchunklidervaluetickfreq = 1;


        private void trackBar4_Scroll(object sender, EventArgs e)
        {

            if (trackbar4 != null)
            {
                trackbar4.Focus();

                precisiondirtyrgbchunklidervalue = trackbar4.Value;

                if (trackbar4.Value < trackbar4.Minimum + 1)
                {
                    trackbar4.Value = trackbar4.Minimum + 1;
                }

                if (trackbar4.Value > trackbar4.Maximum - 1)
                {
                    trackbar4.Value = trackbar4.Maximum - 1;
                }


            }
            precisiondirtyrgbchunklidervalueswtc = 1;
        }

        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (Program.SCGLOBALSACCESSORS != null)
            {
                if (Program.SCGLOBALSACCESSORS.SCCONSOLECORE != null)
                {
                    if (Program.SCGLOBALSACCESSORS.SCCONSOLECORE.handle != IntPtr.Zero)
                    {
                        Program.CloseWindow(Program.SCGLOBALSACCESSORS.SCCONSOLECORE.handle);

                        var process = Process.GetCurrentProcess();

                        if (process != null)
                        {
                            process.Kill();
                        }

                    }
                }
            }
        }



        public int swtccheckbox8 = 0;
        private void checkBox8_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox8.Checked)
            {
                /*if (this.checkBox2.Checked)
                {
                    CheckedListBox1.Visible = true;
                }
                else if (!this.checkBox2.Checked)
                {
                    CheckedListBox1.Visible = false;
                }*/

                /*if (this.checkBox2.Checked)
                {
                    CheckedListBox1.Visible = false;
                }
                else if (!this.checkBox2.Checked)
                {
                    CheckedListBox1.Visible = true;
                }*/




                if (this.checkBox2.Checked)
                {
                    this.CheckedListBox1.Visible = true;
                }
                else
                {
                    this.CheckedListBox1.Visible = false;

                }




                swtccheckbox8 = 1;
            }
            else if (!checkBox8.Checked)
            {

                /*if (this.checkBox2.Checked)
                {
                    CheckedListBox1.Visible = false;
                }
                else if (!this.checkBox2.Checked)
                {
                    CheckedListBox1.Visible = true;
                }*/


                if (this.checkBox2.Checked)
                {
                    this.CheckedListBox1.Visible = false;
                }
                else
                {
                    this.CheckedListBox1.Visible = false;

                }

                swtccheckbox8 = 0;
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }




        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }






        public static System.Drawing.Color sceneskyColor;


        private void button8_Click(object sender, EventArgs e)
        {
            ColorDialog MyDialog = new ColorDialog();
            // Keeps the user from selecting a custom color.
            MyDialog.AllowFullOpen = true;
            // Allows the user to get help. (The default is false.)
            MyDialog.ShowHelp = true;
            // Sets the initial color select to the current text color.
            //MyDialog.Color = textBox1.ForeColor;

            // Update the text box color if the user clicks OK 
            if (MyDialog.ShowDialog() == DialogResult.OK)
            {
                //textBox1.ForeColor = MyDialog.Color;
                sceneskyColor = MyDialog.Color;
            }


        }





        public int sccsvvdhdvoxelresolutionvalueswtc = 0;
        public int sccsvvdhdvoxelresolutionvalue = 0;
        public int lastsccsvvdhdvoxelresolutionvalue = 0;
        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            string selectedscreencaptureindex = (string)comboboxcapturelist6.SelectedItem;
            sccsvvdhdvoxelresolutionvalue = comboboxcapturelist6.FindStringExact(selectedscreencaptureindex);


            //Program._voxelboundswidth
            //Program._voxelboundsheight





            if (sccsvvdhdvoxelresolutionvalue == 0)
            {
                Program._voxelboundswidth = 1920;
                Program._voxelboundsheight = 1080;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 1)
            {
                Program._voxelboundswidth = 1760;
                Program._voxelboundsheight = 990;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 2)
            {
                Program._voxelboundswidth = 1680;
                Program._voxelboundsheight = 1050;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 3)
            {
                Program._voxelboundswidth = 1600;
                Program._voxelboundsheight = 900;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 4)
            {
                Program._voxelboundswidth = 1440;
                Program._voxelboundsheight = 900;
            }
            /*else if (sccsvvdhdvoxelresolutionvalue == 5)
            {
                Program._voxelboundswidth = 1366;
                Program._voxelboundsheight = 768;
            }
            
            else if (sccsvvdhdvoxelresolutionvalue == 6)
            {
                Program._voxelboundswidth = 1280;
                Program._voxelboundsheight = 1024;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 7)
            {
                Program._voxelboundswidth = 1280;
                Program._voxelboundsheight = 960;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 8)
            {
                Program._voxelboundswidth = 1280;
                Program._voxelboundsheight = 800;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 9)
            {
                Program._voxelboundswidth = 1280;
                Program._voxelboundsheight = 720;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 10)
            {
                Program._voxelboundswidth = 1152;
                Program._voxelboundsheight = 864;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 11)
            {
                Program._voxelboundswidth = 1128;
                Program._voxelboundsheight = 634;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 12)
            {
                Program._voxelboundswidth = 1024;
                Program._voxelboundsheight = 768;
            }

            else if (sccsvvdhdvoxelresolutionvalue == 13)
            {
                Program._voxelboundswidth = 832;
                Program._voxelboundsheight = 624;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 14)
            {
                Program._voxelboundswidth = 800;
                Program._voxelboundsheight = 600;
            }
            else if (sccsvvdhdvoxelresolutionvalue == 15)
            {
                Program._voxelboundswidth = 640;
                Program._voxelboundsheight = 480;
            }*/


            /*
            if (Program._desktopboundswidth == 1920 && Program._desktopboundsheight == 1080)
            {
            }
            else if (Program._desktopboundswidth == 1760 && Program._desktopboundsheight == 990)
            {
            }
            else if (Program._desktopboundswidth == 1680 && Program._desktopboundsheight == 1050)
            {

            }
            else if (Program._desktopboundswidth == 1600 && Program._desktopboundsheight == 900)
            {
            }
            else if (Program._desktopboundswidth == 1440 && Program._desktopboundsheight == 900)
            {
            }
            else if (Program._desktopboundswidth == 1366 && Program._desktopboundsheight == 768)
            {
            }
            else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 1024)
            {
            }
            else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 960)
            {
            }
            else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 800)
            {
            }
            else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 720)
            {
            }
            else if (Program._desktopboundswidth == 1152 && Program._desktopboundsheight == 864)
            {
            }
            else if (Program._desktopboundswidth == 1128 && Program._desktopboundsheight == 634)
            {
            }
            else if (Program._desktopboundswidth == 1024 && Program._desktopboundsheight == 768)
            {
            }
            else if (Program._desktopboundswidth == 832 && Program._desktopboundsheight == 624)
            {
            }
            else if (Program._desktopboundswidth == 800 && Program._desktopboundsheight == 600)
            {
            }
            else if (Program._desktopboundswidth == 640 && Program._desktopboundsheight == 480)
            {
            }*/









            if (sccsvvdhdvoxelresolutionvalue != lastsccsvvdhdvoxelresolutionvalue)
            {
                /*if (sccsvvdhdresolutionvalue == 0)
                {
                    //this.checkBox6.Text = "VVD Color 5 Faces";
                    screencapturecolororgrayscale = 0;
                }
                else if (sccsvvdhdresolutionvalue == 1)
                {
                    //this.checkBox6.Text = "VVD Front face Colored and 4 faces Grayscale";
                    screencapturecolororgrayscale = 1;
                }*/

            }

            //label2.Focus();
            sccsvvdhdvoxelresolutionvalueswtc = 1;
            sccsvvdhdresolutionvalueswtc = 1;

            lastsccsvvdhdvoxelresolutionvalue = sccsvvdhdvoxelresolutionvalue;

            label2.Focus();
        }

        /*private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }*/
    }
}




/* 1920 x 1080
1760 x 990
1680 x 1050
1600 x 900
1440 x 900
1366 x 768
1280 x 1024
1280 x 960
1280 x 800
1280 x 720
1152 x 864
1128 x 634
1024 x 768
832 x 624
800 x 600
640 x 480*/



/*
1
2
3
4
5
6
7
8
9
10
11
12
13
14
15
16
17
18
19
20
21
22
23
24
25
26
27
28
29
30
31
32
33
34
35
36
37
38
39
40
41
42
43
44
45
46
47
48
49
50
51
52
53
54
55
56
57
58
59
60
61
62
63
64
65
66
67
68
69
70
71
72
73
74
*/