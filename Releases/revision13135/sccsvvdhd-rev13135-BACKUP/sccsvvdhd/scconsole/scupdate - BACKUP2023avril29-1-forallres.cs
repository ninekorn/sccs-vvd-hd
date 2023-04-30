using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

//using Ab3d.DXEngine;
using Ab3d.OculusWrap;
//using Ab3d.DXEngine.OculusWrap;
using Ab3d.OculusWrap.DemoDX11;

using SharpDX;
using SharpDX.DXGI;
using SharpDX.Direct3D11;
using SharpDX.DirectInput;

using sccs.scgraphics;
//using sccs.scgraphics.scshadermanager;

using Jitter;
using Jitter.Dynamics;
using Jitter.Collision;
using Jitter.LinearMath;
using Jitter.Collision.Shapes;
using Jitter.Forces;

using System.Collections.Generic;
using System.Collections;
using System.Runtime;
using System.Runtime.CompilerServices;

using System.ComponentModel;
using SharpDX.D3DCompiler;

using scmessageobject = sccs.scmessageobject.scmessageobject;
using scmessageobjectjitter = sccs.scmessageobject.scmessageobjectjitter;
using scgraphicssecpackage = sccs.scmessageobject.scgraphicssecpackage;

using System.Drawing;
using System.Drawing.Imaging;
using System;
using System.Windows;
//using System.Windows.Interop;

//using Win32.DwmThumbnail.Interop;
using Win32.Shared;
//using System.Windows.Controls;
using Win32.Shared.Interop;
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
//using Win32;
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



using sccs.scmessageobject;
using Jitter.DataStructures;
using System.Windows.Interop;
using SharpDX.Windows;


using Matrix = SharpDX.Matrix;
using Vector3 = SharpDX.Vector3;
using Key = SharpDX.DirectInput.Key;
using Bitmap = System.Drawing.Bitmap;

using sccs.scgraphics.scshadermanager;

using WinRT.GraphicsCapture;

using Win32.Shared.Interfaces;



namespace sccs.scgraphics
{
    public class scupdate : scdirectx
    {

        public static Stopwatch fpsTimerShaderPresentThread;
        public static int fpsTimerShaderPresentThreadswtc;
        public static int fpsTimerShaderPresentThreadcounter;


        public Vector3 viewpositionorigin = new Vector3(0, 0, 0);
        public Vector3 _hmdPoser = new Vector3(0, 0.5f, 0);
        public Vector3 viewPosition = new Vector3(0, 0, 0);

        //OVR FOR IK
        public uint typeofsensortouchL;
        public uint lasttypeofsensortouchL;
        public uint typeofsensortouchR;
        public uint lasttypeofsensortouchR;



        int threadupdateswtc = 0;
        float fasterorslowermul = 1;

        public static float originrotx = 0;
        public static float originroty = 0;
        public static float originrotz = 0;


        int lastsccsvvdhdvoxelfaceoption = 0;

        public void resetcamera()
        {
            OFFSETPOS = Vector3.Zero;
            movePos = Vector3.Zero;// new Vector3(0, 0, 0);
            rotx = originrotx;
            roty = 0;
            rotz = 0;
            //Camera.SetPosition(0, 0, 0);
            Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);


            Camera.SetRotation(rotx, 0, 0);
            resetvaluesswtc = 0;

            speedRot = 1.25f; //1.95f // 0.25f
            speedRotArduino = 0.000001f;

            speedPos = 0.15f; //0.025f // 1.5f
            speedPosArduino = 0.001f;

            fasterorslowermul = 1;
        }




        //IntPtr sometest;
        //Graphics memgraph;
        //Graphics g;
        //System.Drawing.Bitmap bmp;
        //static RECT rect;
        //public static SharpDX.DirectInput.Keyboard _Keyboard;
        static System.Drawing.Rectangle windowbounds;// new System.Drawing.Rectangle(rect.left, rect.top,rect.right-rect.left,rect.bottom-rect.top);
        static Bitmap somebitmap;
        static int bitmapcounter = 0;

        Stopwatch canpressfmenukeystopwatch = new Stopwatch();
        int canpressfmenukeyswtc = 0;


        Stopwatch resetvaluesstopwatch = new Stopwatch();
        int resetvaluesswtc = 0;



        //int initinputs = 0;

        float rotx = 0;
        float roty = 0;
        float rotz = 0;
        public int hasfinishedframe0 = 0;
        public int hasfinishedframe1 = 0;


        public int exitthread0 = 0;
        public int exitthread1 = 0;

        public int haspressedheightmapvalueincrease = 0;
        public int haspressedheightmapvaluedecrease = 0;
        public int haspressedheightmapkey = 0;



        public static int stopovr = 0;
        public Thread main_thread_update0;
        public Thread main_thread_update1;
        //public Thread heightmapthread;
        public static DateTime startTime;// = DateTime.Now;

        public static Matrix oriProjectionMatrix;
        public static int[] arduinoDIYOculusTouchArrayOfData = new int[12];
        ShaderResourceView _lastShaderResourceView;
        ShaderResourceView shaderRes;
        int screencaptureresultswtc = 0;
        Texture2DDescription _textureDescriptionFinal;

        public static _sc_camera Camera;
        public scgraphicssecpackage scgraphicssecpackagemessage;
        SharpDX.Matrix finalRotationMatrix;
        Vector3 lookUp;
        Vector3 lookAt;
        //public static Vector3 viewPosition;
        public static Matrix viewMatrix;
        public static Matrix _projectionMatrix;
        public static Vector3 OFFSETPOS;

        public static SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Matrix originRot = SharpDX.Matrix.Identity;

        public static SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;
        public static SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;
        public int canworkphysics = 0;
        public static Matrix hmdmatrixRot = Matrix.Identity;
        public int updatethreadupdateswtc0 = 0;
        public int updatethreadupdateswtc1 = 0;
        Thread[] threads = new Thread[2];

        //OCULUS TOUCH SETTINGS 
        Ab3d.OculusWrap.Result resultsRight;
        uint buttonPressedOculusTouchRight;
        Vector2f[] thumbStickRight;
        public float[] handTriggerRight;
        float[] indexTriggerRight;
        Ab3d.OculusWrap.Result resultsLeft;
        uint buttonPressedOculusTouchLeft;
        Vector2f[] thumbStickLeft;
        public float[] handTriggerLeft;
        public static float[] indexTriggerLeft;
        public static Posef handPoseLeft;
        SharpDX.Quaternion _leftTouchQuat;
        public static Posef handPoseRight;
        SharpDX.Quaternion _rightTouchQuat;
        public static Matrix _leftTouchMatrix = Matrix.Identity;
        public static Matrix _rightTouchMatrix = Matrix.Identity;
        //OCULUS TOUCH SETTINGS

        //public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, -0.0725f, -0.0425f); //when - 

        //0.015 APPROX TOADJUST SCREEN CORNERS/BOUNDARIES
        //0.0f APPROX TO LET VOXEL AFFECTED WITH PERLIN IN FRUSTRUM VIEW OF THE CAMERA
        public static SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 0, 0.0f); //new SharpDX.Vector3(-10, 1, 10); //0.645f // - 0.0425f // 0.015 APPROX TOADJUST SCREEN CORNERS/BOUNDARIES
        public static SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 0, -0.365f);

        public static SharpDX.Vector3 savedposition7 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition8 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition9 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition4 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition5 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedposition6 = new SharpDX.Vector3(0, 0, 0);

        public static SharpDX.Vector3 savedrotation7 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation8 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation9 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation4 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation5 = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotation6 = new SharpDX.Vector3(0, 0, 0);

        public static SharpDX.Vector3 savedpositiontosetto = new SharpDX.Vector3(0, 0, 0);
        public static SharpDX.Vector3 savedrotationtosetto = new SharpDX.Vector3(0, 0, 0);

        public Stopwatch savedposrotwatch = new Stopwatch();
        int savedposrotswtc = 0;
        int savedposrotswtcoptionsaveorselect = 0;






        float disco_sphere_rot_speed = 0.5f;

        float speedRot = 1.25f; //1.95f // 0.25f
        float speedRotArduino = 0.000001f;

        float speedPos = 0.15f; //0.025f // 1.5f
        float speedPosArduino = 0.001f;
        public static Matrix hmd_matrix;
        Matrix hmd_matrix_test;


        public static double RotationY = 0;//{ get; set; } { get; set; }
        public static double RotationX = 0;//{ get; set; } { get; set; }
        public static double RotationZ = 0;//{ get; set; } { get; set; }

        public static float RotationOriginY = 0;//{ get; set; }
        public static float RotationOriginX = 0;//{ get; set; }
        public static float RotationOriginZ = 0;//{ get; set; }

        float thumbstickIsRight;
        float thumbstickIsUp;


        double displayMidpoint;
        TrackingState trackingState;
        Posef[] eyePoses;
        EyeType eye;
        EyeTexture eyeTexture;
        bool latencyMark = false;
        TrackingState trackState;
        PoseStatef poseStatefer;
        Posef hmdPose;
        Quaternionf hmdRot;
        //public static Vector3 _hmdPoser;
        Quaternion _hmdRoter;

        public static Matrix tempmatter;
        public static Vector4 dirikvoxelbodyInstanceRight0;
        public static Vector4 dirikvoxelbodyInstanceUp0;
        public static Vector4 dirikvoxelbodyInstanceForward0;

        public static double RotationY4Pelvis;
        public static double RotationX4Pelvis;
        public static double RotationZ4Pelvis;

        public static double RotationY4PelvisTwo;
        public static double RotationX4PelvisTwo;
        public static double RotationZ4PelvisTwo;


        public static double RotationGrabbedYOff;
        public static double RotationGrabbedXOff;
        public static double RotationGrabbedZOff;


        public static double RotationGrabbedY;
        public static double RotationGrabbedX;
        public static double RotationGrabbedZ;


        public static double Rotationscreenx;
        public static double Rotationscreeny;
        public static double Rotationscreenz;

        public static sccsscreenframe screencaptureframe;
        public static sccssharpdxscreencapture sharpdxscreencapture;

        public static SC_ShaderManager _shaderManager;

        /*
        public static DInput keynmouseinput;
        public static InputSimulator inputsim;
        public static KeyboardSimulator keyboardsim;
        public static MouseSimulator mousesim;*/

        public Stopwatch pausewatch = new Stopwatch();
        public int pauseswtc = 0;
        public int canpause = 0;

        //Stopwatch cammovementwatch = new Stopwatch();
        int cammovementswtc = 0;
        public int canmovecamera = 1;

        //public ICaptureMethod captureMethod;





        //ICaptureMethod captureMethod_




        public scupdate() //: base(new DwmSharedSurface())
        //public scupdate() : base(new GraphicsCapture())
        {
            //captureMethod = captureMethod_;

            fpsTimerShaderPresentThread = new Stopwatch();
            fpsTimerShaderPresentThread.Start();
        }


        //override
        public void ShutDownGraphics()
        {

            if (_shaderManager != null)
            {
                _shaderManager.Dispose();
                _shaderManager = null;

            }


            if (sharpdxscreencapture != null)
            {
                //sharpdxscreencapture.releaseFrame();
                sharpdxscreencapture.Disposer();
                sharpdxscreencapture = null;

            }



            Camera = null;
            //scgraphicssecpackagemessage = null;
            /*
            inputsim = null;// new InputSimulator();
            mousesim = null;//new MouseSimulator(inputsim);
            keyboardsim = null;// new KeyboardSimulator(inputsim);
            keynmouseinput = null;//= new DInput();*/

            if (main_thread_update0 != null)
            {
                main_thread_update0.Abort();
            }
            if (main_thread_update1 != null)
            {
                main_thread_update1.Abort();
            }



            main_thread_update0 = null;
            main_thread_update1 = null;

            /*if (captureMethod != null)
            {
                captureMethod.StopCapture();
                captureMethod.Dispose();
                captureMethod = null;
            }*/









            //sometest = IntPtr.Zero;
            //memgraph = null;
            //g = null;
            //bmp = null;
            //rect = 0;
            //public static SharpDX.DirectInput.Keyboard _Keyboard;
            //windowbounds = 0;
            somebitmap?.Dispose();
            somebitmap = null;
            //bitmapcounter = 0 = null;

            canpressfmenukeystopwatch = null;// = new Stopwatch();
            canpressfmenukeyswtc = 0;


            resetvaluesstopwatch = null;
            resetvaluesswtc = 0;



            //initinputs = 0;
            rotx = 0;
            roty = 0;
            rotz = 0;
            hasfinishedframe0 = 0;
            hasfinishedframe1 = 0;
            exitthread0 = 0;
            exitthread1 = 0;
            haspressedheightmapvalueincrease = 0;
            haspressedheightmapvaluedecrease = 0;
            haspressedheightmapkey = 0;





            stopovr = 0;
            main_thread_update0 = null;
            main_thread_update1 = null;
            //public Thread heightmapthread;
            threadupdateswtc = 0;
            //startTime = null;
            //oriProjectionMatrix = null;
            arduinoDIYOculusTouchArrayOfData = null;// = new int[12];

            _lastShaderResourceView?.Dispose();
            _lastShaderResourceView = null;

            shaderRes?.Dispose();
            shaderRes = null;

            screencaptureresultswtc = 0;
            //_textureDescriptionFinal = null;


            //scgraphicssecpackagemessage = null;
            //finalRotationMatrix = null;
            //lookUp = null;
            //lookAt = null;

            //Vector3 viewPosition;
            //Matrix viewMatrix;
            //Matrix _projectionMatrix;
            // Vector3 OFFSETPOS;

            //SharpDX.Vector3 movePos = new SharpDX.Vector3(0, 0, 0);
            //SharpDX.Matrix originRot = SharpDX.Matrix.Identity;

            //SharpDX.Matrix rotatingMatrixForPelvis = SharpDX.Matrix.Identity;
            //SharpDX.Matrix rotatingMatrix = SharpDX.Matrix.Identity;
            canworkphysics = 0;
            //Matrix hmdmatrixRot = Matrix.Identity;
            updatethreadupdateswtc0 = 0;
            updatethreadupdateswtc1 = 0;

            if (threads != null)
            {
                if (threads.Length > 0)
                {
                    for (int i = 0; i < threads.Length; i++)
                    {
                        threads[i] = null;
                    }

                }
            }




            //OCULUS TOUCH SETTINGS 
            // Ab3d.OculusWrap.Result resultsRight = null;
            // uint buttonPressedOculusTouchRight = null;
            thumbStickRight = null;
            handTriggerRight = null;
            indexTriggerRight = null;
            // Ab3d.OculusWrap.Result resultsLeft = null;
            // uint buttonPressedOculusTouchLeft = null;
            thumbStickLeft = null;
            handTriggerLeft = null;
            indexTriggerLeft = null;



            /*  Posef handPoseLeft;
          SharpDX.Quaternion _leftTouchQuat;
           Posef handPoseRight;
          SharpDX.Quaternion _rightTouchQuat;
           Matrix _leftTouchMatrix = Matrix.Identity;
           Matrix _rightTouchMatrix = Matrix.Identity;
          //OCULUS TOUCH SETTINGS 

           SharpDX.Vector3 originPos = new SharpDX.Vector3(0, 0, 0); //new SharpDX.Vector3(-10, 1, 10); //0.645f
           SharpDX.Vector3 originPosScreen = new SharpDX.Vector3(0, 0, -0.365f);

           SharpDX.Vector3 savedposition7 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedposition8 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedposition9 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedposition4 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedposition5 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedposition6 = new SharpDX.Vector3(0, 0, 0);

           SharpDX.Vector3 savedrotation7 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedrotation8 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedrotation9 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedrotation4 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedrotation5 = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedrotation6 = new SharpDX.Vector3(0, 0, 0);

           SharpDX.Vector3 savedpositiontosetto = new SharpDX.Vector3(0, 0, 0);
           SharpDX.Vector3 savedrotationtosetto = new SharpDX.Vector3(0, 0, 0);
              */
            savedposrotwatch = null;// new Stopwatch();
            savedposrotswtc = 0;
            savedposrotswtcoptionsaveorselect = 0;






            disco_sphere_rot_speed = 0;

            speedRot = 0; //1.95f // 0.25f
            speedRotArduino = 0;

            speedPos = 0; //0.025f // 1.5f
            speedPosArduino = 0;
            // Matrix hmd_matrix;
            // Matrix hmd_matrix_test;


            RotationY = 0;//{ get; set; } { get; set; }
            RotationX = 0;//{ get; set; } { get; set; }
            RotationZ = 0;//{ get; set; } { get; set; }

            RotationOriginY = 0;//{ get; set; }
            RotationOriginX = 0;//{ get; set; }
            RotationOriginZ = 0;//{ get; set; }

            thumbstickIsRight = 0;
            thumbstickIsUp = 0;


            //displayMidpoint = 0;
            //TrackingState trackingState;
            Posef[] eyePoses = null;
            //Vector3 viewpositionorigin;
            //EyeType eye;
            //EyeTexture eyeTexture;
            // bool latencyMark = false;
            //TrackingState trackState;
            //PoseStatef poseStatefer;
            // Posef hmdPose;
            // Quaternionf hmdRot;
            //  Vector3 _hmdPoser;
            //  Quaternion _hmdRoter;

            //Matrix tempmatter;
            //Vector4 dirikvoxelbodyInstanceRight0;
            //Vector4 dirikvoxelbodyInstanceUp0;
            // Vector4 dirikvoxelbodyInstanceForward0;

            RotationY4Pelvis = 0;
            RotationX4Pelvis = 0;
            RotationZ4Pelvis = 0;

            RotationY4PelvisTwo = 0;
            RotationX4PelvisTwo = 0;
            RotationZ4PelvisTwo = 0;


            RotationGrabbedYOff = 0;
            RotationGrabbedXOff = 0;
            RotationGrabbedZOff = 0;


            RotationGrabbedY = 0;
            RotationGrabbedX = 0;
            RotationGrabbedZ = 0;


            Rotationscreenx = 0;
            Rotationscreeny = 0;
            Rotationscreenz = 0;

            //sccsscreenframe screencaptureframe;
            //sccssharpdxscreencapture sharpdxscreencapture;

            //SC_ShaderManager _shaderManager;


            pausewatch = null; //new Stopwatch();
            pauseswtc = 0;
            canpause = 0;

            //cammovementwatch = null;
            cammovementswtc = 0;
            canmovecamera = 0;


            if (screencaptureframe._texture2DFinal != null)
            {
                screencaptureframe._texture2DFinal.Dispose();
                screencaptureframe._texture2DFinal = null;
            }



            if (screencaptureframe.ShaderResource != null)
            {
                screencaptureframe.ShaderResource.Dispose();
                screencaptureframe.ShaderResource = null;
            }



            if (screencaptureframe.ShaderResourceArray != null)
            {
                if (screencaptureframe.ShaderResourceArray.Length > 0)
                {
                    for (int i = 0; i < screencaptureframe.ShaderResourceArray.Length; i++)
                    {
                        if (screencaptureframe.ShaderResourceArray[i] != null)
                        {
                            screencaptureframe.ShaderResourceArray[i].Dispose();
                            screencaptureframe.ShaderResourceArray[i] = null;
                        }
                    }
                }
            }

            if (screencaptureframe.arrayOfFRACSCREENSPECTRUMBytes != null)
            {
                screencaptureframe.arrayOfFRACSCREENSPECTRUMBytes = null;
            }

            if (screencaptureframe.somebitmapforarduino != null)
            {
                screencaptureframe.somebitmapforarduino.Dispose();
                screencaptureframe.somebitmapforarduino = null;
            }

            if (screencaptureframe.bitmapByteArray != null)
            {
                screencaptureframe.somebitmapforarduino = null;
            }


            if (screencaptureframe.bitmapEmptyByteArray != null)
            {
                screencaptureframe.bitmapEmptyByteArray = null;
            }


            if (screencaptureframe.frameByteArray != null)
            {
                screencaptureframe.frameByteArray = null;
            }

            if (screencaptureframe.screencapturearrayofbytes != null)
            {
                screencaptureframe.screencapturearrayofbytes = null;
            }


            if (scgraphicssecpackagemessage.scjittertasks != null)
            {
                if (scgraphicssecpackagemessage.scjittertasks.Length > 0)
                {

                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks[0].Length > 0)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks[0][0].frameByteArray != null)
                            {
                                scgraphicssecpackagemessage.scjittertasks[0][0].frameByteArray = null;
                            }
                        }
                    }
                }
            }


            if (scgraphicssecpackagemessage.scjittertasks != null)
            {
                if (scgraphicssecpackagemessage.scjittertasks.Length > 0)
                {
                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks[0].Length > 0)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks[0][0].shaderresource != null)
                            {
                                scgraphicssecpackagemessage.scjittertasks[0][0].shaderresource.Dispose();
                                scgraphicssecpackagemessage.scjittertasks[0][0].shaderresource = null;
                            }
                        }
                    }
                }

            }




            if (Camera != null)
            {
                Camera = null;
            }


            if (scgraphicssecpackagemessage.scgraphicssec != null)
            {
                scgraphicssecpackagemessage.scgraphicssec.Shutdown();
                scgraphicssecpackagemessage.scgraphicssec = null;
            }



            if (device != null)
            {
                device.Dispose();
                device = null;
            }

            if (D3D != null)
            {
                D3D.ShutDown();
                D3D = null;
            }




            GC.Collect();


        }






        public struct RECT
        {
            public int left, top, bottom, right;
        }


        //protected override //override
        public scmessageobjectjitter[][] init_update_variables(scmessageobjectjitter[][] _sc_jitter_tasks, sccs.sccore.scsystemconfiguration configuration) //, scconsole.scconsolewriter _writer //, IntPtr hwnd
        {
            try
            {

                Camera = new _sc_camera();


                if (Program._useOculusRift == 0)
                {
                    //originPos.Y += 4;
                    //originPos.Z -= 2f;
                    //Program.MessageBox((IntPtr)0, "1", "sc core systems message", 0);


                    Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);

                    Camera.SetRotation(0, 0, 0);//originrotx
                }
                else if (Program._useOculusRift == 1)
                {





                    speedRot = 1.75f;
                    speedPos = 1.25f;
                    RotationX = 0;
                    RotationY = 0;
                    RotationZ = 0;

                    RotationX4Pelvis = 0;
                    RotationY4Pelvis = 180;
                    RotationZ4Pelvis = 0;

                    //float pitch = (float)(RotationX * 0.0174532925f);
                    //float yaw = (float)(RotationY * 0.0174532925f);
                    //float roll = (float)(RotationZ * 0.0174532925f);

                    float pitch = (float)(Math.PI * (RotationX) / 180.0f);
                    float yaw = (float)(Math.PI * (RotationY) / 180.0f);
                    float roll = (float)(Math.PI * (RotationZ) / 180.0f);

                    rotatingMatrix = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                    //pitch = (float)(RotationX4Pelvis * 0.0174532925f);
                    //yaw = (float)(RotationY4Pelvis * 0.0174532925f);
                    //roll = (float)(RotationZ4Pelvis * 0.0174532925f);

                    pitch = (float)(Math.PI * (RotationX4Pelvis) / 180.0f);
                    yaw = (float)(Math.PI * (RotationY4Pelvis) / 180.0f);
                    roll = (float)(Math.PI * (RotationZ4Pelvis) / 180.0f);

                    rotatingMatrixForPelvis = SharpDX.Matrix.RotationYawPitchRoll(yaw, pitch, roll);
                }

                resetcamera();


                scgraphicssecpackagemessage = new scgraphicssecpackage();
                scgraphicssecpackagemessage.threadresponseswtc = 0;

                startTime = DateTime.Now;



                _shaderManager = new SC_ShaderManager();
                _shaderManager.Initialize(D3D.Device, Program.consoleHandle);


                if (Program.screencapturetype == 2)
                {
                    sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, device);
                }



                if (updatethreadupdateswtc1 == 0)
                {
                    Console.WriteLine("scupdate.cs => sccsvvdhd creating scgraphicssec.cs");

                    scgraphicssecpackagemessage.scgraphicssec = new sccs.scgraphicssec();

                    Console.WriteLine("scupdate.cs => sccsvvdhd creating arrays for mesh objects and worldmatrices in scgraphicssec.cs");

                    scgraphicssecpackagemessage.scgraphicssec.createarraysforallvoxelassets(scgraphicssecpackagemessage.scjittertasks);



                    //Console.WriteLine("created world object");

                    updatethreadupdateswtc1 = 1;
                }


                //_sc_jitter_tasks[0][0].hasinit = 0;
            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }
            return _sc_jitter_tasks;
        }

        //protected override
        public bool Update(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks) //scmessageobjectjitter[][]
        {
            try
            {
                _sc_jitter_tasks = _FrameVRTWO(jitter_sc, _sc_jitter_tasks); //_sc_jitter_tasks = 

            }
            catch (Exception ex)
            {
                //ShutDown
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }

            return true; // _sc_jitter_tasks;
        }


        public bool RenderAll(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks) //scmessageobjectjitter[][]
        {
            try
            {
                //_FrameVRTWO(jitter_sc, _sc_jitter_tasks); //_sc_jitter_tasks = 



                if (Program._useOculusRift == 0 && canworkphysics == 1)
                {
                    if (scgraphicssecpackagemessage.scgraphicssec != null)
                    {

                        if (scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks.Length > 0)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {


                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 2)
                                        {




                                            if (Program.createikrig == 1)
                                            {
                                                //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonikshaders(scgraphicssecpackagemessage);

                                            }
                                            scgraphicssecpackagemessage.scgraphicssec.rendervoxels();

                                            if (sccsvvdhd.Form1.currentform.voxelvirtualdesktoptype == 2)
                                            {
                                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.renderstandardvirtualdesktop(scgraphicssecpackagemessage);

                                            }
                                            else
                                            {

                                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                            }

                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaderforsccubes(scgraphicssecpackagemessage);
                                        }


                                    }
                                }
                            }
                        }
                    }


                }

            }
            catch (Exception ex)
            {
                //ShutDown
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }

            return true; // _sc_jitter_tasks;
        }


        public bool PresentDirectx(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks) //scmessageobjectjitter[][]
        {
            try
            {
                //_FrameVRTWO(jitter_sc, _sc_jitter_tasks); //_sc_jitter_tasks = 

                if (Program._useOculusRift == 0 && canworkphysics == 1)
                {
                    if (sccsvvdhd.Form1.currentform.verticalsyncoptionvalue == 0)
                    {
                        //Console.WriteLine("vertical sync disabled");

                        D3D.SwapChain.Present(0, PresentFlags.None);
                    }
                    else if (sccsvvdhd.Form1.currentform.verticalsyncoptionvalue == 1)
                    {
                        //Console.WriteLine("vertical sync enabled");

                        D3D.SwapChain.Present(1, PresentFlags.None);
                    }
                }

            }
            catch (Exception ex)
            {
                //ShutDown
                Program.MessageBox((IntPtr)0, "" + ex.ToString(), "sc core systems message", 0);
            }

            return true; // _sc_jitter_tasks;
        }



        int somekeyboardswtc = 0;

        private unsafe scmessageobjectjitter[][] _FrameVRTWO(jitter_sc[] jitter_sc, scmessageobjectjitter[][] _sc_jitter_tasks)
        {



            //Program.keynmouseinput.Frame();
            if (Program.keynmouseinput != null && somekeyboardswtc == 0)
            {

                if (pauseswtc == 0)
                {
                    pausewatch.Stop();
                    pausewatch.Reset();
                    pausewatch.Restart();
                    pauseswtc = 1;
                }


                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad6))
                {
                    if (pausewatch.Elapsed.Milliseconds >= 150)
                    {
                        Program.progcanpause = 1;

                        /*if (canpause == 0)
                        {
                            Program.progcanpause = 1;
                            canpause = 1;
                        }*/
                        /*else if (canpause == 1)
                        {
                            Program.progcanpause = 0;
                            canpause = 0;
                        }*/
                        pauseswtc = 0;
                    }
                }


                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.F9))
                {
                    if (pausewatch.Elapsed.Milliseconds >= 150)
                    {
                        sccsvvdhd.Form1.thecheckbox2.Invoke((MethodInvoker)delegate
                        {
                            if (sccsvvdhd.Form1.thecheckbox2.Checked)
                            {
                                sccsvvdhd.Form1.thecheckbox2.Checked = false;
                            }
                            else if (!sccsvvdhd.Form1.thecheckbox2.Checked)
                            {
                                sccsvvdhd.Form1.thecheckbox2.Checked = true;
                            }
                        });
                        pauseswtc = 0;
                        /*
                        if (sccsvvdhd.Form1.currentform.ismenuenabled == 0)
                        {

                            sccsvvdhd.Form1.currentform.ismenuenabled = 1;
                        }
                        else if (sccsvvdhd.Form1.currentform.ismenuenabled == 1)
                        {

                            sccsvvdhd.Form1.currentform.ismenuenabled = 0;
                        }*/
                    }
                }











                /*
                if (cammovementswtc == 0)
                {
                    cammovementwatch.Stop();
                    cammovementwatch.Reset();
                    cammovementwatch.Restart();
                    cammovementswtc = 1;
                }


                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad0))
                {
                    if (cammovementwatch.Elapsed.Milliseconds >= 10)
                    {
                        if (canmovecamera == 0)
                        {
                            canmovecamera = 1;
                        }
                        else if (canmovecamera == 1)
                        {
                            canmovecamera = 0;
                        }
                        cammovementswtc = 0;
                    }
                }
                */







                if (canpressfmenukeyswtc == 0)
                {
                    canpressfmenukeystopwatch.Stop();
                    canpressfmenukeystopwatch.Reset();
                    canpressfmenukeystopwatch.Restart();
                    canpressfmenukeyswtc = 1;
                }

                if (canpressfmenukeystopwatch.Elapsed.Milliseconds >= 250 && canpressfmenukeyswtc == 1)
                {
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.F9))
                    {

                        sccsvvdhd.Form1.haspressedf9 = 1;
                        sccsvvdhd.Form1.someform.haspressedsomekeyboardkey = 1;
                        /*
                        if (sccsvvdhd.Form1.thepanel.Visible == false)
                        {
                            if (sccsvvdhd.Form1.someform.haspressedf9 == 1)
                            {
                                sccsvvdhd.Form1.someform.haspressedf9 = 0;
                                sccsvvdhd.Form1.someform.haspressedsomekeyboardkey = 1;
                            }
                        }
                        else if (sccsvvdhd.Form1.thepanel.Visible == true)
                        {
                            if (sccsvvdhd.Form1.someform.haspressedf9 == 0)
                            {
                                sccsvvdhd.Form1.someform.haspressedf9 = 1;
                                sccsvvdhd.Form1.someform.haspressedsomekeyboardkey = 1;                
                            }
                        }
                        */
                        /*else if (sccsvvdhd.Form1.someform.haspressedf9 == 2)
                        {
                            canpressfmenukeyswtc = 0;

                        }*/

                        canpressfmenukeystopwatch.Stop();
                        canpressfmenukeyswtc = 0;
                    }
                }








                /*
                if (resetvaluesswtc == 0)
                {
                    resetvaluesstopwatch.Stop();
                    resetvaluesstopwatch.Reset();
                    resetvaluesstopwatch.Restart();
                    resetvaluesswtc = 1;
                }


                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPadEnter))
                {
                    if (resetvaluesstopwatch.Elapsed.Milliseconds >= 20)
                    {
                        resetcamera();
                    }
                }*/
















                /*
                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad1))
                {
                    if (haspressedheightmapvaluedecrease == 0)
                    {

                        haspressedheightmapvaluedecrease = 1;
                    }
                }

                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad3))
                {
                    if (haspressedheightmapvalueincrease == 0)
                    {
                        haspressedheightmapvalueincrease = 1;
                    }
                }



                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad7))
                {
                    if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                    {
                        if (savedposrotswtcoptionsaveorselect == 2)
                        {
                            savedposition7 = OFFSETPOS;
                            savedrotation7 = Camera.GetRotation();
                            savedposrotswtcoptionsaveorselect = 0;
                        }
                        else if (savedposrotswtcoptionsaveorselect == 1)
                        {
                            Camera.SetPosition(savedposition7.X, savedposition7.Y, savedposition7.Z);
                            Camera.SetRotation(savedrotation7.X, savedrotation7.Y, savedrotation7.Z);
                            //OFFSETPOS = savedposition7;
                            rotx = savedrotation7.X;
                            roty = savedrotation7.Y;
                            rotz = savedrotation7.Z;
                            movePos = savedposition7;
                            savedposrotswtcoptionsaveorselect = 0;
                        }

                        resetvaluesswtc = 0;
                    }
                }



                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad8))
                {
                    if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                    {
                        if (savedposrotswtcoptionsaveorselect == 2)
                        {
                            savedposition8 = OFFSETPOS;
                            savedrotation8 = Camera.GetRotation();
                            savedposrotswtcoptionsaveorselect = 0;
                        }
                        else if (savedposrotswtcoptionsaveorselect == 1)
                        {
                            Camera.SetPosition(savedposition8.X, savedposition8.Y, savedposition8.Z);
                            Camera.SetRotation(savedrotation8.X, savedrotation8.Y, savedrotation8.Z);
                            //OFFSETPOS = savedposition8;
                            rotx = savedrotation8.X;
                            roty = savedrotation8.Y;
                            rotz = savedrotation8.Z;
                            movePos = savedposition8;// Vector3.Zero;
                            savedposrotswtcoptionsaveorselect = 0;
                        }

                        resetvaluesswtc = 0;
                    }
                }


                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad9))
                {
                    if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                    {
                        if (savedposrotswtcoptionsaveorselect == 2)
                        {
                            savedposition9 = OFFSETPOS;
                            savedrotation9 = Camera.GetRotation();
                            savedposrotswtcoptionsaveorselect = 0;
                        }
                        else if (savedposrotswtcoptionsaveorselect == 1)
                        {
                            Camera.SetPosition(savedposition9.X, savedposition9.Y, savedposition9.Z);
                            Camera.SetRotation(savedrotation9.X, savedrotation9.Y, savedrotation9.Z);
                            //OFFSETPOS = savedposition9;
                            rotx = savedrotation9.X;
                            roty = savedrotation9.Y;
                            rotz = savedrotation9.Z;
                            movePos = savedposition9;// Vector3.Zero;
                            savedposrotswtcoptionsaveorselect = 0;
                        }

                        //OFFSETPOS = Vector3.Zero;
                        //movePos = Vector3.Zero;// new Vector3(0, 0, 0);
                        //rotx = 0;
                        //roty = 0;
                        //rotz = 0;
                        //Camera.SetPosition(originPos.X, originPos.Y, originPos.Z);
                        //Camera.SetRotation(0, 0, 0);
                        resetvaluesswtc = 0;
                    }
                }


                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad4))
                {
                    if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                    {
                        savedposrotswtcoptionsaveorselect = 2;
                        resetvaluesswtc = 0;
                    }
                }

                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad5))
                {
                    if (resetvaluesstopwatch.Elapsed.Milliseconds >= 10)
                    {
                        savedposrotswtcoptionsaveorselect = 1;
                        resetvaluesswtc = 0;
                    }
                }*/



                if (resetvaluesswtc == 0)
                {
                    resetvaluesstopwatch.Stop();
                    resetvaluesstopwatch.Reset();
                    resetvaluesstopwatch.Restart();
                    resetvaluesswtc = 1;
                }



                /*
                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad9))
                {
                    if (resetvaluesstopwatch.Elapsed.Milliseconds >= 150)
                    {
                        sccsvvdhd.Form1.someform.gridtypeoption++;

                        if (sccsvvdhd.Form1.someform.gridtypeoption >= 7)
                        {
                            sccsvvdhd.Form1.someform.gridtypeoption = 0;
                        }

                        if (sccsvvdhd.Form1.someform.gridtypeoption <= -1)
                        {
                            sccsvvdhd.Form1.someform.gridtypeoption = 6;
                        }



                        /*else
                        {
                            
                        }

                        if (sccsvvdhd.Form1.someform.gridtypeoption == 0)
                        {
                            sccsvvdhd.Form1.someform.gridtypeoption = 1;
                        }
                        

                        resetvaluesswtc = 0;
                    }
                }*/















                /*
                if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.NumberPad7))
                {
                    if (resetvaluesstopwatch.Elapsed.Milliseconds >= 150)
                    {
                        //sccsvvdhd.Form1.someform.cursorlightoption--;
                        //sccsvvdhd.Form1.someform.cursorlightoption++;

                        //sccsvvdhd.Form1.someform.screencapturecolor = 


                        /*
                        if (sccsvvdhd.Form1.someform.cursorlightoption <= -1)
                        {

                            sccsvvdhd.Form1.someform.cursorlightoption = 1;
                        }

                        if (sccsvvdhd.Form1.someform.cursorlightoption > 2)
                        {

                            sccsvvdhd.Form1.someform.cursorlightoption = 0;
                        }*/


                /*
                if (sccsvvdhd.Form1.someform.cursorlightoption == 0)
                {
                    if (scgraphicssecpackagemessage.scjittertasks != null)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                        {
                            if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                            {
                                //scgraphicssecpackagemessage.scgraphicssec.hasinit = 0;
                                //scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, 0);
                                //scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;

                                if (scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 == 0)
                                {
                                    scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 = 3;
                                }
                                if (scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 == 0)
                                {
                                    scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 = 3;
                                }




                            }
                        }
                    }
                    //deletevirtualdesktop5faces
                    //createvirtualdesktop1face
                }*/
                /*else
                {

                }

                if (sccsvvdhd.Form1.someform.gridtypeoption == 0)
                {
                    sccsvvdhd.Form1.someform.gridtypeoption = 1;
                }


                resetvaluesswtc = 0;
            }
        }*/




                if (scgraphicssecpackagemessage.scjittertasks != null)
                {
                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                    {
                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 == 2 && scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 == 2)
                            {
                                /*scgraphicssecpackagemessage.scgraphicssec.hasinit = 0;
                                Program.sccsgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, 1);
                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 = 0;
                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 = 0;*/

                            }

                            if (scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 == 4 && scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 == 4)
                            {
                                /*scgraphicssecpackagemessage.scgraphicssec.hasinit = 0;
                                Program.sccsgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, 0);
                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 = 0;
                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 = 0;*/
                            }
                        }
                    }
                }






            }







            if (Program._useOculusRift == 0 && canworkphysics == 1)
            {
                if (scgraphicssecpackagemessage.scjittertasks != null)
                {
                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                    {
                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                        {
                            if (sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc == 1)
                            {
                                Console.WriteLine("scupdate.cs => sccsvvdhd restart request");

                                //HAS THE THREAD RESPONDED TO A CLOSE REQUEST.
                                if (scgraphicssecpackagemessage.threadresponseswtc == 1)
                                {
                                    sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 2;

                                }
                                else
                                {
                                    //KEEP TRYING TO CLOSE THREAD
                                    threadupdateswtc = 1;
                                }

                            }
                            else if (sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc == 2)
                            {



                                Console.WriteLine("scupdate.cs => sccsvvdhd flush started");
                                scgraphicssecpackagemessage.scgraphicssec.flushsccsvvdhd();
                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 3;
                            }
                            else if (sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc == 3)
                            {
                                Console.WriteLine("scupdate.cs => sccsvvdhd flush ended");
                                Console.WriteLine("scupdate.cs => sccsvvdhd building the voxel virtual desktop START...");

                                if (sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalue == 0)
                                {
                                    scgraphicssecpackagemessage.scgraphicssec.buildtherestofassets();

                                    Console.WriteLine("scupdate.cs => sccsvvdhd user choice Low Voxel Resolution");




                                    if (Program._desktopboundswidth == 1920 && Program._desktopboundsheight == 1080)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            //TO FIX LATER 120 X 68 WOULD BE HALFWAY
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(128, 72, 1, 0.0005f);  // 

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(113, 57, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(101, 57, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(80, 47, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(40, 30, 1, 0.0005f); 
                                        }





                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 5) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(110, 62, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 6) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(98, 55, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 7) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(83, 52, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 8) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(74, 42, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 9) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(37, 28, 1, 0.0005f);
                                        }





                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 10) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(112, 70, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 11) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(98, 58, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 12) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(93, 58, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 13) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(73, 45, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 14) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(36, 31, 1, 0.0005f);
                                        }


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 15) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(106, 60, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 16) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(88, 50, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 17) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(84, 50, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 18) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(72, 41, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 19) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(34, 26, 1, 0.0005f);
                                        }


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 20) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(96, 60, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 21) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(80, 50, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 22) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(75, 50, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 23) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(65, 41, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 24) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(30, 26, 1, 0.0005f);
                                        }


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 25) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(91, 51, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 26) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(76, 43, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 27) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(72, 43, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 28) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(62, 35, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 29) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(28, 22, 1, 0.0005f);
                                        }

                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 30) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(85, 69, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 31) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(71, 57, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 32) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(67, 57, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 33) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(55, 45, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 34) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(26, 19, 1, 0.0005f);
                                        }
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 35) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(85, 64, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 36) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(71, 54, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 37) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(67, 54, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 38) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(55, 42, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 39) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(26, 18, 1, 0.0005f);
                                        }


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 40) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(85, 54, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 41) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(71, 45, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 42) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(67, 45, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 43) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(55, 35, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 44) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(26, 15, 1, 0.0005f);
                                        }

                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 45) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(85, 49, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 46) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(71, 41, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 47) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(67, 41, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 48) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(55, 31, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 49) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(26, 14, 1, 0.0005f);
                                        }


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 50) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(77, 57, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 51) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(64, 48, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 52) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(60, 48, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 53) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(50, 37, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 54) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(24, 16, 1, 0.0005f);
                                        }



                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 55) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(75, 43, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 56) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(63, 35, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 57) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(59, 35, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 58) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(49, 27, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 59) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(23, 12, 1, 0.0005f);
                                        }


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 60) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(68, 51, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 61) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(57, 42, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 62) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(54, 42, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 63) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(44, 34, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 64) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(21, 15, 1, 0.0005f);
                                        }


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 65) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(59, 45, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 66) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(49, 37, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 67) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(46, 37, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 68) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(37, 29, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 69) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(17, 12, 1, 0.0005f);
                                        }

                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 70) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(57, 43, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 71) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(47, 35, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 72) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(44, 35, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 73) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(36, 27, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 74) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(16, 11, 1, 0.0005f);
                                        }










                                    }
                                    else if (Program._desktopboundswidth == 1760 && Program._desktopboundsheight == 990)
                                    {
                                        /* 
                                        1920 x 1080 #
                                        1760 x 990
                                        1680 x 1050
                                        1600 x 900 #
                                        1440 x 900 #
                                        1366 x 768
                                        1280 x 1024
                                        1280 x 960
                                        1280 x 800
                                        1280 x 720 #
                                        1152 x 864
                                        1128 x 634
                                        1024 x 768
                                        832 x 624
                                        800 x 600
                                        640 x 480 # 
                                        */




                                        //1920 * 1080 = RATIO W 1 H 1
                                        //1600 * 900 = RATIO W 0.84210526315789473684210526315789 H 0.83333333333333333333333333333333
                                        //1440 * 900 = RATIO W 0.75 H 0.83333333333333333333333333333333
                                        //1280 * 720 = RATIO W 0.66666666666666666666666666666667 H 0.66666666666666666666666666666667
                                        //640 * 480 = RATIO W 0.33333333333333333333333333333333 H 0.44444444444444444444444444444444

                                        /*if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(110, 62, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(98, 55, 1, 0.0005f);  
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(83, 52, 1, 0.0005f);   
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(74, 42, 1, 0.0005f);  
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(37, 28, 1, 0.0005f); 
                                        }*/



                                        /*
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(110, 62, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(104, 55, 1, 0.0005f);  
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(98, 55, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(76, 43, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(37, 29, 1, 0.0005f); 
                                        }*/

                                        /*
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(110, 62, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(104, 59, 1, 0.0005f);  
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(98, 55, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(76, 43, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(37, 29, 1, 0.0005f); 
                                        }*/




                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(110, 62, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(98, 55, 1, 0.0005f);
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(98, 55, 1, 0.0005f);

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(77, 43, 1, 0.0005f); 

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(38, 27, 1, 0.0005f);
                                        }










                                    }
                                    else if (Program._desktopboundswidth == 1680 && Program._desktopboundsheight == 1050)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(112, 70, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(98, 58, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(93, 58, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(73, 45, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(36, 31, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1600 && Program._desktopboundsheight == 900)
                                    {



                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(106, 60, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(88, 50, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(84, 50, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(72, 41, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(34, 26, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1440 && Program._desktopboundsheight == 900)
                                    {


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(96, 60, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(80, 50, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(75, 50, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(65, 41, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(30, 26, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1366 && Program._desktopboundsheight == 768)
                                    {


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(91, 51, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(76, 43, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(72, 43, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(62, 35, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(28, 22, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 1024)
                                    {


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(85, 69, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(71, 57, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(67, 57, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(55, 45, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(26, 19, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 960)
                                    {


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(85, 64, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(71, 54, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(67, 54, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(55, 42, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(26, 18, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 800)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(85, 54, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(71, 45, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(67, 45, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(55, 35, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(26, 15, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 720)
                                    {



                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(85, 49, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(71, 41, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(67, 41, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(55, 31, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(26, 14, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1152 && Program._desktopboundsheight == 864)
                                    {

                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(77, 57, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(64, 48, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(60, 48, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(50, 37, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(24, 16, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1128 && Program._desktopboundsheight == 634)
                                    {

                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(75, 43, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(63, 35, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(59, 35, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(49, 27, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(23, 12, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1024 && Program._desktopboundsheight == 768)
                                    {


                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(68, 51, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(57, 42, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(54, 42, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(44, 34, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(21, 15, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 832 && Program._desktopboundsheight == 624)
                                    {

                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(59, 45, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(49, 37, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(46, 37, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(37, 29, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(17, 12, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 800 && Program._desktopboundsheight == 600)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(57, 43, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(47, 35, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(44, 35, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(36, 27, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(16, 11, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 640 && Program._desktopboundsheight == 480)
                                    {
                                        //TODO WHEN I WILL TEST ON A MONITOR WITH RESOLUTION 640X480.
                                        //TODO WHEN I WILL TEST ON A MONITOR WITH RESOLUTION 640X480.
                                        //TODO WHEN I WILL TEST ON A MONITOR WITH RESOLUTION 640X480.

                                        Program.MessageBox((IntPtr)0, "resolution not developed yet.", "sccsmsg", 0);
                                    }










                                    sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 4;
                                }
                                else if (sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalue == 1)
                                {
                                    scgraphicssecpackagemessage.scgraphicssec.buildtherestofassets();

                                    Console.WriteLine("scupdate.cs => sccsvvdhd user choice High Voxel Resolution");



                                    int optionvoxelresolutionmultiplier = 2;


                                    if (Program._desktopboundswidth == 1920 && Program._desktopboundsheight == 1080)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            //Console.WriteLine("TEST");
                                            //scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(240, 135, 1, 0.0005f); 
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((128 * optionvoxelresolutionmultiplier) - 16, (72 * optionvoxelresolutionmultiplier) - 9, 1, 0.0005f);  //240 * 135
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((107 * optionvoxelresolutionmultiplier) - 1, (57 * optionvoxelresolutionmultiplier) - 6, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((101 * optionvoxelresolutionmultiplier)-9, (57 * optionvoxelresolutionmultiplier)-6, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((80 * optionvoxelresolutionmultiplier), (45 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((40 * optionvoxelresolutionmultiplier), (30 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1760 && Program._desktopboundsheight == 990)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((117 * optionvoxelresolutionmultiplier)-14, (66 * optionvoxelresolutionmultiplier)-8, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((103 * optionvoxelresolutionmultiplier)-11, (55 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((98 * optionvoxelresolutionmultiplier), (55 * optionvoxelresolutionmultiplier), 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((76 * optionvoxelresolutionmultiplier)-7, (43 * optionvoxelresolutionmultiplier)-3, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((37 * optionvoxelresolutionmultiplier)-1, (28 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1680 && Program._desktopboundsheight == 1050)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((112 * optionvoxelresolutionmultiplier)-14, (70 * optionvoxelresolutionmultiplier)-8, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((98 * optionvoxelresolutionmultiplier)-9, (58 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((93 * optionvoxelresolutionmultiplier), (58 * optionvoxelresolutionmultiplier), 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((73 * optionvoxelresolutionmultiplier)-7, (45 * optionvoxelresolutionmultiplier)-2, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((36 * optionvoxelresolutionmultiplier)-2, (31 * optionvoxelresolutionmultiplier)-3, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1600 && Program._desktopboundsheight == 900)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((106 * optionvoxelresolutionmultiplier)-12, (60 * optionvoxelresolutionmultiplier)-7, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((88 * optionvoxelresolutionmultiplier), (50 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((84 * optionvoxelresolutionmultiplier)-7, (50 * optionvoxelresolutionmultiplier), 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((72 * optionvoxelresolutionmultiplier), (41 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((34 * optionvoxelresolutionmultiplier)-2, (26 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1440 && Program._desktopboundsheight == 900)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((96 * optionvoxelresolutionmultiplier)-12, (60 * optionvoxelresolutionmultiplier)-7, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((80 * optionvoxelresolutionmultiplier), (50 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((75 * optionvoxelresolutionmultiplier)-6, (50 * optionvoxelresolutionmultiplier), 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((65 * optionvoxelresolutionmultiplier), (41 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((30 * optionvoxelresolutionmultiplier), (26 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1366 && Program._desktopboundsheight == 768)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((91 * optionvoxelresolutionmultiplier)-12, (51 * optionvoxelresolutionmultiplier)-7, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((76 * optionvoxelresolutionmultiplier), (43 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((72 * optionvoxelresolutionmultiplier)-7, (43 * optionvoxelresolutionmultiplier), 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((62 * optionvoxelresolutionmultiplier), (35 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((28 * optionvoxelresolutionmultiplier)-1, (22 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 1024)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((85 * optionvoxelresolutionmultiplier)-12, (69 * optionvoxelresolutionmultiplier)-9, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((71 * optionvoxelresolutionmultiplier), (57 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((67 * optionvoxelresolutionmultiplier)-7, (57 * optionvoxelresolutionmultiplier), 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((55 * optionvoxelresolutionmultiplier)-5, (45 * optionvoxelresolutionmultiplier)-4, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((26 * optionvoxelresolutionmultiplier)-1, (19 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 960)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((85 * optionvoxelresolutionmultiplier)-12, (64 * optionvoxelresolutionmultiplier)-7, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((71 * optionvoxelresolutionmultiplier), (54 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((67 * optionvoxelresolutionmultiplier)-7, (54 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((55 * optionvoxelresolutionmultiplier)-4, (42 * optionvoxelresolutionmultiplier)-3, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((26 * optionvoxelresolutionmultiplier)-1, (18 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 800)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((85 * optionvoxelresolutionmultiplier)-12, (54 * optionvoxelresolutionmultiplier)-7, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((71 * optionvoxelresolutionmultiplier), (45 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((67 * optionvoxelresolutionmultiplier)-6, (45 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((55 * optionvoxelresolutionmultiplier)-3, (35 * optionvoxelresolutionmultiplier)-3, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((26 * optionvoxelresolutionmultiplier)-1, (15 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1280 && Program._desktopboundsheight == 720)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((85 * optionvoxelresolutionmultiplier)-12, (49 * optionvoxelresolutionmultiplier)-7, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((71 * optionvoxelresolutionmultiplier), (41 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((67 * optionvoxelresolutionmultiplier)-7, (41 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((55 * optionvoxelresolutionmultiplier)-3, (31 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((26 * optionvoxelresolutionmultiplier)-1, (14 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1152 && Program._desktopboundsheight == 864)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((77 * optionvoxelresolutionmultiplier)-10, (57 * optionvoxelresolutionmultiplier)-5, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((64 * optionvoxelresolutionmultiplier), (48 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((60 * optionvoxelresolutionmultiplier)-4, (48 * optionvoxelresolutionmultiplier), 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((50 * optionvoxelresolutionmultiplier)-4, (37 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((24 * optionvoxelresolutionmultiplier), (16 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1128 && Program._desktopboundsheight == 634)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((75 * optionvoxelresolutionmultiplier)-10, (43 * optionvoxelresolutionmultiplier)-6, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((63 * optionvoxelresolutionmultiplier)-2, (35 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((59 * optionvoxelresolutionmultiplier)-6, (35 * optionvoxelresolutionmultiplier), 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((49 * optionvoxelresolutionmultiplier)-4, (27 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((23 * optionvoxelresolutionmultiplier)-1, (12 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 1024 && Program._desktopboundsheight == 768)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((68 * optionvoxelresolutionmultiplier)-8, (51 * optionvoxelresolutionmultiplier)-5, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((57 * optionvoxelresolutionmultiplier), (42 * optionvoxelresolutionmultiplier)+1, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((54 * optionvoxelresolutionmultiplier)-6, (42 * optionvoxelresolutionmultiplier)+1, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((44 * optionvoxelresolutionmultiplier)-3, (34 * optionvoxelresolutionmultiplier)-3, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((21 * optionvoxelresolutionmultiplier)-1, (15 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 832 && Program._desktopboundsheight == 624)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((59 * optionvoxelresolutionmultiplier), (45 * optionvoxelresolutionmultiplier)-2, 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((49 * optionvoxelresolutionmultiplier)-6, (37 * optionvoxelresolutionmultiplier)-4, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((46 * optionvoxelresolutionmultiplier)-1, (37 * optionvoxelresolutionmultiplier)-4, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((37 * optionvoxelresolutionmultiplier), (29 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((17 * optionvoxelresolutionmultiplier)-1, (12 * optionvoxelresolutionmultiplier)-1, 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 800 && Program._desktopboundsheight == 600)
                                    {
                                        if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 0) // 1920 * 1080
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((57 * optionvoxelresolutionmultiplier)-1, (43 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 1) // 1600 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((47 * optionvoxelresolutionmultiplier)-6, (35 * optionvoxelresolutionmultiplier)-3, 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 2) //1440 * 900
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((44 * optionvoxelresolutionmultiplier), (35 * optionvoxelresolutionmultiplier)-3, 1, 0.0005f);   

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 3) //1280 * 720
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((36 * optionvoxelresolutionmultiplier), (27 * optionvoxelresolutionmultiplier), 1, 0.0005f);  

                                        }
                                        else if (sccsvvdhd.Form1.currentform.sccsvvdhdvoxelresolutionvalue == 4) // 640 * 480
                                        {
                                            scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((16 * optionvoxelresolutionmultiplier), (11 * optionvoxelresolutionmultiplier), 1, 0.0005f); 
                                        }
                                    }
                                    else if (Program._desktopboundswidth == 640 && Program._desktopboundsheight == 480)
                                    {
                                        //TODO WHEN I WILL TEST ON A MONITOR WITH RESOLUTION 640X480.
                                        //TODO WHEN I WILL TEST ON A MONITOR WITH RESOLUTION 640X480.
                                        //TODO WHEN I WILL TEST ON A MONITOR WITH RESOLUTION 640X480.

                                        Program.MessageBox((IntPtr)0, "resolution not developed yet.", "sccsmsg", 0);
                                    }






                                    //scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions((int)(Math.Ceiling((Program._voxelboundswidth / 15.0f))) * 2, (int)(Math.Ceiling((Program._voxelboundsheight / 15.0f))) * 2, 1, 0.0005f); 


                                    sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 4;
                                }


                                //scgraphicssecpackagemessage.scgraphicssec.rebuildvirtualdesktopdimensions(480, 270, 1);



                            }
                            else if (sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc == 4)
                            {
                                Console.WriteLine("scupdate.cs => sccsvvdhd building the voxel virtual desktop END...");

                                Console.WriteLine("scupdate.cs => sccsvvdhd building pencil/stick and some voxel mesh objects...");

                                if (scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets == 0)
                                {
                                    //Console.WriteLine("scupdate.cs => building otherobjects");
                                    //scgraphicssecpackagemessage.scgraphicssec.buildtherestofassets();

                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktopassets(scgraphicssecpackagemessage.scjittertasks);
                                    scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets = 1;
                                    sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 5;
                                }
                                else
                                {
                                    Console.WriteLine("scupdate.cs => sccsvvdhd building nothing else. 1 frame has passed ...");
                                    sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 5;
                                }
                                if (Program.createikrig == 1)
                                {
                                    Console.WriteLine("scupdate.cs => sccsvvdhd building voxel human ik rig START...");
                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.createikrig(scgraphicssecpackagemessage);
                                }
                            }



                            if (sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc == 5 && sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc == 0)
                            {
                                Console.WriteLine("scupdate.cs => sccsvvdhd building voxel human ik rig END...");

                                Console.WriteLine("scupdate.cs => REBUILD SHADERS");


                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                        {
                                            if (sccsvvdhd.Form1.currentform.voxelvirtualdesktoptype == 0)
                                            {
                                                lastsccsvvdhdvoxelfaceoption = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);

                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }
                                            else if (sccsvvdhd.Form1.currentform.voxelvirtualdesktoptype == 1)
                                            {
                                                lastsccsvvdhdvoxelfaceoption = 1;
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);

                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }
                                            else if (sccsvvdhd.Form1.currentform.voxelvirtualdesktoptype == 2)
                                            {
                                                lastsccsvvdhdvoxelfaceoption = 2;
                                                //scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktopstandard(scgraphicssecpackagemessage.scjittertasks);
                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }


                                            /*if (scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 == 0)
                                            {
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 0;

                                                lastsccsvvdhdvoxelfaceoption = 1;
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);
                                               
                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;

                                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 = 1;
                                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 = 0;
                                       
                                            }
                                            else if (scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 == 1)
                                            {
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 0;

                                                lastsccsvvdhdvoxelfaceoption = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);



                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 = 0;
                                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 = 0;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }*/

                                            /*if (sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc == 1)
                                            {
                                                sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc = 0;
                                            }*/
                                            //

                                        }
                                    }
                                }

                            }






                            if (sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 1)
                            {
                                Console.WriteLine("REBUILD SHADERS voxelvirtualdesktoptypeswtc == 1 && hasinitsccsvvdhd == 1");

                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                        {
                                            if (sccsvvdhd.Form1.currentform.voxelvirtualdesktoptype == 0)
                                            {
                                                lastsccsvvdhdvoxelfaceoption = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);

                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }
                                            else if (sccsvvdhd.Form1.currentform.voxelvirtualdesktoptype == 1)
                                            {
                                                lastsccsvvdhdvoxelfaceoption = 1;
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);

                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }
                                            else if (sccsvvdhd.Form1.currentform.voxelvirtualdesktoptype == 2)
                                            {
                                                lastsccsvvdhdvoxelfaceoption = 2;
                                                //scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktopstandard(scgraphicssecpackagemessage.scjittertasks);

                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }




                                            /*
                                            if (scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 == 0)
                                            {
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 0;

                                                lastsccsvvdhdvoxelfaceoption = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);

                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 = 1;
                                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 = 0;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }
                                            else if (scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 == 1)
                                            {
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 0;

                                                lastsccsvvdhdvoxelfaceoption = 1;
                                                scgraphicssecpackagemessage.scgraphicssec.createvirtualdesktop5faces(scgraphicssecpackagemessage.scjittertasks, lastsccsvvdhdvoxelfaceoption);

                                                scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd = 2;
                                                threadupdateswtc = 0;
                                                updatethreadupdateswtc0 = 0;
                                                scgraphicssecpackagemessage.scgraphicssec.hasinit = 1;
                                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet0 = 0;
                                                scgraphicssecpackagemessage.scjittertasks[0][0].swtcvirtualdesktoptypet1 = 0;
                                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 6;
                                            }*/

                                            //if (sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc == 1)
                                            {
                                                sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc = 0;
                                            }

                                        }
                                    }
                                }
                            }
                            else if (sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 2 || sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 1 || sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 0)
                            {
                                sccsvvdhd.Form1.currentform.sccsvvdhdresolutionvalueswtc = 1;
                                sccsvvdhd.Form1.someform.voxelvirtualdesktoptypeswtc = 0;
                            }
                        }
                    }
                }



                //updatethreadupdateswtc0 = 0;

                Matrix tempmat = Camera.rotationMatrix;
                Quaternion quatt;
                Quaternion.RotationMatrix(ref tempmat, out quatt);


                if (Program.usethirdpersonview == 0)
                {
                    /*//FOR THE VERTEX SHADER
                    Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmat, out somedirquat1);
                    dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                    dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                    dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);

                    viewMatrix = Camera.ViewMatrix;*/
                }
                else if (Program.usethirdpersonview == 1)
                {
                    /*Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref tempmat, out somedirquat1);
                    dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                    dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                    dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);


                    Matrix viewmat = Camera.ViewMatrix;

                    //CAMERA THIRD PERSON VIEW OFFSET
                    //CAMERA THIRD PERSON VIEW OFFSET
                    //CAMERA THIRD PERSON VIEW OFFSET
                    //Quaternion somedirquat1;
                    Quaternion.RotationMatrix(ref viewmat, out somedirquat1);
                    var dirikvoxelbodyInstanceRight1 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                    var dirikvoxelbodyInstanceUp1 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                    var dirikvoxelbodyInstanceForward1 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);

                    var theviewpos = OFFSETPOS + (-new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z) * Program.offsetthirdpersonview * 0.0035f); //Program.offsetthirdpersonview
                                                                                                                                                                                                              //viewMatrix = Matrix.LookAtRH(theviewpos, theviewpos + new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z), new Vector3(dirikvoxelbodyInstanceUp1.X, dirikvoxelbodyInstanceUp1.Y, dirikvoxelbodyInstanceUp1.Z));
                    //Camera.ViewMatrix = Matrix.LookAtRH(theviewpos, theviewpos + new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z), new Vector3(dirikvoxelbodyInstanceUp1.X, dirikvoxelbodyInstanceUp1.Y, dirikvoxelbodyInstanceUp1.Z));

                    Vector3 cameraoffsetpos = theviewpos;

                    Camera.SetPosition(cameraoffsetpos.X, cameraoffsetpos.Y, cameraoffsetpos.Z);
                    */
                    //Console.WriteLine("frame arrives here");

                    //_viewMatrix = Matrix.LookAtRH(theviewpos, theviewpos + new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z), new Vector3(dirikvoxelbodyInstanceUp1.X, dirikvoxelbodyInstanceUp1.Y, dirikvoxelbodyInstanceUp1.Z));
                    //_projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                    //CAMERA THIRD PERSON VIEW OFFSET
                    //CAMERA THIRD PERSON VIEW OFFSET
                    //CAMERA THIRD PERSON VIEW OFFSET


                }

                //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;

                if (Program.screencapturetype == 2)
                {
                    if (sharpdxscreencapture != null)
                    {
                        // Program.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);



                        for (int i = 0; i < 1;)
                        {
                            screencaptureresultswtc = 0;

                            try
                            {
                                screencaptureframe = sharpdxscreencapture.ScreenCapture(3);

                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scjittertasks[0].Length > 0)
                                        {
                                            scgraphicssecpackagemessage.scjittertasks[0][0].frameByteArray = screencaptureframe.frameByteArray;
                                            scgraphicssecpackagemessage.scjittertasks[0][0].shaderresource = screencaptureframe.ShaderResource;

                                            //Program.MessageBox((IntPtr)0, "test2", "sccsmsg", 0);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, device);
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





                SharpDX.Color peruserchoicecolor = new SharpDX.Color(sccsvvdhd.Form1.sceneskyColor.R, sccsvvdhd.Form1.sceneskyColor.G, sccsvvdhd.Form1.sceneskyColor.B, sccsvvdhd.Form1.sceneskyColor.A);

                // Clear views
                D3D.DeviceContext.ClearDepthStencilView(_depthStencilView, DepthStencilClearFlags.Depth, 1.0f, 0);
                D3D.DeviceContext.ClearRenderTargetView(_renderTargetView, peruserchoicecolor);
                // SharpDX.Color.Black); //LightGray //Black //CornflowerBlue
                //D3D.DeviceContext.clea





                float ratio = (float)SurfaceWidth / (float)SurfaceHeight;
                //_projectionMatrix = Matrix.PerspectiveFovLH(3.14F / 3.0F, ratio, 0.001f, 1000);
                //_projectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), 0.00001f, 1000);
                _projectionMatrix = Matrix.PerspectiveFovLH((float)(Math.PI / 4), ((float)SurfaceWidth / (float)SurfaceHeight), 0.01f, 1000);





                //viewMatrix = Camera.ViewMatrix;




                //Program.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                if (canworkphysics == 1)
                {
                    var somerotvec = Camera.GetRotation();
                    float pitch = somerotvec.X * 0.0174532925f;
                    float yaw = somerotvec.Y * 0.0174532925f; ;
                    float roll = somerotvec.Z * 0.0174532925f; ;
                    Matrix somerotmat = Camera.rotationMatrix;// Matrix.RotationYawPitchRoll(yaw, pitch, roll);

                    scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                    scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;
                    scgraphicssecpackagemessage.originRot = originRot;
                    scgraphicssecpackagemessage.rotatingMatrix = somerotmat; //rotatingMatrix
                    scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                    scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                    scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                    scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                    scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                    scgraphicssecpackagemessage.oriProjectionMatrix = oriProjectionMatrix;
                    scgraphicssecpackagemessage.someextrapelvismatrix = rotatingMatrixForPelvis;
                    scgraphicssecpackagemessage.offsetpos = OFFSETPOS;// Camera.GetPosition();
                    scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                    scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;
                    //scgraphicssecpackagemessage.scgraphicssec = null;
                    scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;




                }
            }







            if (scgraphicssecpackagemessage.scjittertasks != null)
            {
                scgraphicssecpackagemessage.offsetpos = OFFSETPOS;




                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                {
                    if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                    {
                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                        //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                        // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);


                        if (scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 2)
                        {
                            if (Program.createikrig == 1)
                            {

                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonikshaders(scgraphicssecpackagemessage);


                                //scgraphicssecpackagemessage.scgraphicssec.rendervoxels();
                            }
                        }




                        if (scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 2)
                        {



                            /*if (Program.createikrig == 1)
                            {
                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                scgraphicssecpackagemessage.scgraphicssec.writevoxelstobuffer();
                                scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                            }*/


                            //Console.WriteLine("frame arrives here");

                            //scgraphicssecpackagemessage.scgraphicssec.writevoxelstobuffer();

                            //scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                        }
                    }
                }
            }











            if (scgraphicssecpackagemessage.scjittertasks != null)
            {
                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                {
                    if (scgraphicssecpackagemessage.scgraphicssec != null && canworkphysics == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                    {


                        if (scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 2)
                        {
                            updateplayermovementandrotation();




                            if (Camera != null)
                            {
                                if (Program.usethirdpersonview == 0)
                                {
                                    Camera.Render(Vector3.Zero, Vector3.Zero, Vector3.Zero);
                                }
                                else if (Program.usethirdpersonview == 1)
                                {
                                    //Camera.Render(Vector3.Zero, Vector3.Zero, Vector3.Zero);




                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    if (scgraphicssec.currentscgraphicssec.ikvoxelbody != null)
                                    {
                                        if (scgraphicssec.currentscgraphicssec.ikvoxelbody[0] != null)
                                        {
                                            Matrix viewmat = scgraphicssec.currentscgraphicssec.ikvoxelbody[0]._player_pelvis[0][0]._arrayOfInstances[0].current_pos;
                                            Matrix viewmattorso = scgraphicssec.currentscgraphicssec.ikvoxelbody[0]._player_torso[0][0]._arrayOfInstances[0].current_pos;

                                            Vector3 posofhead = new Vector3(scgraphicssec.currentscgraphicssec.ikvoxelbody[0]._player_head[0][0]._arrayOfInstances[0].current_pos.M41,
                                                scgraphicssec.currentscgraphicssec.ikvoxelbody[0]._player_head[0][0]._arrayOfInstances[0].current_pos.M42,
                                                scgraphicssec.currentscgraphicssec.ikvoxelbody[0]._player_head[0][0]._arrayOfInstances[0].current_pos.M43);

                                            Quaternion somedirquat1;
                                            Quaternion.RotationMatrix(ref viewmattorso, out somedirquat1);
                                            var dirikvoxelbodyInstanceRight1 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                            var dirikvoxelbodyInstanceUp1 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                            var dirikvoxelbodyInstanceForward1 = new Vector4(-sc_maths._newgetdirforward(somedirquat1), 0);

                                            var theviewposlookat = posofhead + (new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z) * Program.offsetthirdpersonview * 0.015f);
                                            theviewposlookat = theviewposlookat + (new Vector3(dirikvoxelbodyInstanceUp1.X, dirikvoxelbodyInstanceUp1.Y, dirikvoxelbodyInstanceUp1.Z) * Program.offsetthirdpersonview * 0.00015f);

                                            //Camera.SetPosition(theviewposlookat.X, theviewposlookat.Y, theviewposlookat.Z);
                                            Camera.Render(theviewposlookat, Vector3.Zero, Vector3.Zero);
                                        }
                                    }
                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    //CAMERA THIRD PERSON VIEW OFFSET






                                    /*Matrix viewmat = Camera.ViewMatrix;

                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    Quaternion somedirquat1;
                                    Quaternion.RotationMatrix(ref viewmat, out somedirquat1);
                                    var dirikvoxelbodyInstanceRight1 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                    var dirikvoxelbodyInstanceUp1 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                    var dirikvoxelbodyInstanceForward1 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);

                                    var theviewposlookat = OFFSETPOS + (-new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z) * Program.offsetthirdpersonview * 0.015f);
                                    theviewposlookat = theviewposlookat + (new Vector3(dirikvoxelbodyInstanceUp1.X, dirikvoxelbodyInstanceUp1.Y, dirikvoxelbodyInstanceUp1.Z) * Program.offsetthirdpersonview * 0.0015f);
                                    */

                                    //Program.offsetthirdpersonview                                                                                                                                                                                                                 
                                    //viewMatrix = Matrix.LookAtRH(theviewpos, theviewpos + new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z), new Vector3(dirikvoxelbodyInstanceUp1.X, dirikvoxelbodyInstanceUp1.Y, dirikvoxelbodyInstanceUp1.Z));                                                                                                                                                                                                               
                                    //Camera.ViewMatrix = Matrix.LookAtRH(theviewpos, theviewpos + new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z), new Vector3(dirikvoxelbodyInstanceUp1.X, dirikvoxelbodyInstanceUp1.Y, dirikvoxelbodyInstanceUp1.Z));

                                    //Vector3 cameraoffsetpos = theviewpos;

                                    //Camera.SetPosition(cameraoffsetpos.X, cameraoffsetpos.Y, cameraoffsetpos.Z);

                                    //Console.WriteLine("frame arrives here");

                                    //_viewMatrix = Matrix.LookAtRH(theviewpos, theviewpos + new Vector3(dirikvoxelbodyInstanceForward1.X, dirikvoxelbodyInstanceForward1.Y, dirikvoxelbodyInstanceForward1.Z), new Vector3(dirikvoxelbodyInstanceUp1.X, dirikvoxelbodyInstanceUp1.Y, dirikvoxelbodyInstanceUp1.Z));
                                    //_projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    //CAMERA THIRD PERSON VIEW OFFSET
                                    //CAMERA THIRD PERSON VIEW OFFSET



                                }

                            }
                            scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer(scgraphicssecpackagemessage.projectionMatrix, viewMatrix);



                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                            scgraphicssecpackagemessage.scgraphicssec.writevoxelstobuffer();

                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                        }




                        Matrix tempmatt = Camera.rotationMatrix;

                        /*
                        if (scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 2)
                        {
                            scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer(scgraphicssecpackagemessage.projectionMatrix, tempmatt);
                        }*/







                        if (updatethreadupdateswtc0 == 0)
                        {
                            /*
                            scgraphicssecpackagemessage.scgraphicssec = new sccs.scgraphicssec();
                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec._sc_create_world_objects(scgraphicssecpackagemessage.scjittertasks);
                            */

                            main_thread_update0 = new Thread(() =>
                            {

                            _thread_looper:
                                try
                                {

                                    //_ticks_watch.Stop();
                                    //_ticks_watch.Restart();


                                    if (threadupdateswtc == 0) //0
                                    {
                                        scgraphicssecpackagemessage.threadresponseswtc = 0;



                                        if (scgraphicssecpackagemessage.scjittertasks != null)
                                        {
                                            if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                            {
                                                if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                                {

                                                    if (fpsTimerShaderPresentThreadcounter >= 10)
                                                    {
                                                        //sccsr15forms.Form1.currentform.Text = string.Format("SharpDX - Threaded MultiCube D3D11  - (F1) {0} - (F2) {1} - (F3) {2} - Threads ↑↓{3} - Count ←{4}→ - FPS: {5:F2} ({6:F2}ms)  - Threaded: (F4) {7}", typeStr, directx.D3D.currentState.UseMap ? "Map/UnMap" : "UpdateSubresource", directx.D3D.currentState.SimulateCpuUsage ? "BurnCPU On" : "BurnCpu Off", directx.D3D.currentState.Type == directx.TestType.Deferred ? directx.D3D.currentState.ThreadCount : 1, directx.D3D.currentState.CountCubes * directx.D3D.currentState.CountCubes, 1000.0 * fpsCounter / fpsTimer.ElapsedMilliseconds, (float)fpsTimer.ElapsedMilliseconds / fpsCounter, runthreadedapp);
                                                        fpsTimerShaderPresentThread.Reset();
                                                        fpsTimerShaderPresentThread.Restart();
                                                        //fpsCounterrenderthread = 0;
                                                        fpsTimerShaderPresentThreadswtc = 1;
                                                        fpsTimerShaderPresentThreadcounter = 0;
                                                    }
                                                    //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                    //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                    // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);

                                                    if (scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 2)
                                                    {









                                                        scgraphicssecpackagemessage.scgraphicssec.sccswriteheightmapstoarraybuffers(scgraphicssecpackagemessage.scjittertasks);

                                                        scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks, out hasfinishedframe0);







                                                    }






                                                    if (fpsTimerShaderPresentThreadswtc == 1)
                                                    {
                                                        fpsTimerShaderPresentThread.Stop();

                                                        var elapsedtimerticks = fpsTimerShaderPresentThread.ElapsedMilliseconds;

                                                        sccsvvdhd.Form1.currentform.picturebox2fpsfloat2 = elapsedtimerticks;

                                                        fpsTimerShaderPresentThreadswtc = 0;
                                                    }
                                                    fpsTimerShaderPresentThreadcounter++;
                                                }
                                            }
                                        }
                                    }

                                    if (updatethreadupdateswtc1 == 0)
                                    {
                                        if (scgraphicssecpackagemessage.scjittertasks != null)
                                        {
                                            if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                            {
                                                if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                                {

                                                }
                                            }
                                        }
                                        //updatethreadupdateswtc1 = 1;
                                    }



                                    //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                                }
                                catch (Exception ex)
                                {
                                    Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                }


                                if (threadupdateswtc == 0)
                                {
                                    Thread.Sleep(0);
                                    goto _thread_looper;

                                }
                                else
                                {
                                    Thread.Sleep(10);
                                }


                                scgraphicssecpackagemessage.threadresponseswtc = 1;


                                //ShutDown();
                                //ShutDownGraphics();

                            }, 0);

                            main_thread_update0.IsBackground = true;
                            main_thread_update0.Priority = ThreadPriority.Lowest; //AboveNormal
                            main_thread_update0.SetApartmentState(ApartmentState.STA);
                            main_thread_update0.Start();

                            updatethreadupdateswtc0 = 1;
                        }
                    }
                }
            }






            //Camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);





            if (Program._useOculusRift == 0 && canworkphysics == 1)
            {
                /*if (scgraphicssecpackagemessage.scgraphicssec != null)
                {

                    if (scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                    {
                        if (scgraphicssecpackagemessage.scjittertasks != null)
                        {
                            if (scgraphicssecpackagemessage.scjittertasks.Length > 0)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                {


                                    if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhdassets == 1 && scgraphicssecpackagemessage.scgraphicssec.hasinitsccsvvdhd == 2)
                                    {






                                        if (sccsvvdhd.Form1.currentform.voxelvirtualdesktoptype == 2)
                                        {
                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.renderstandardvirtualdesktop(scgraphicssecpackagemessage);

                                        }
                                        else
                                        {
                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                        }

                                        scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaderforsccubes(scgraphicssecpackagemessage);
                                        scgraphicssecpackagemessage.scgraphicssec.rendervoxels();
                                    }


                                }
                            }
                        }
                    }
                }*/




                /*
                if (sccsvvdhd.Form1.currentform.verticalsyncoptionvalue == 0)
                {
                    //Console.WriteLine("vertical sync disabled");

                    D3D.SwapChain.Present(0, PresentFlags.None);
                }
                else if (sccsvvdhd.Form1.currentform.verticalsyncoptionvalue == 1)
                {
                    //Console.WriteLine("vertical sync enabled");

                    D3D.SwapChain.Present(1, PresentFlags.None);
                }
                */
























            }
            else if (Program._useOculusRift == 1 && canworkphysics == 1)
            {





                /*
                //0 no index touch nothing
                //4352 index touch
                //8448 no index touch
                //4864 index touch
                //20480 index touch
                //5120 index touch
                //9216 no index touch
                //24576 no index touch
                //8960 no index touch
                //8704 no index touch
                //768 index touch
                //4096 index touch
                //8192 no index touch
                //11008 no index touch
                //256 no index touch
                //9728 no index touch - thumbstick movement
                //16384
                //1024
                //10496
                //10240
                //9984



                //index is being pressed.
                if (D3D.inputStateLTouch.Touches == 10496
                    || D3D.inputStateLTouch.Touches == 1024
                    || D3D.inputStateLTouch.Touches == 16384
                    || D3D.inputStateLTouch.Touches == 8448
                    || D3D.inputStateLTouch.Touches == 9216
                    || D3D.inputStateLTouch.Touches == 24576
                    || D3D.inputStateLTouch.Touches == 8960
                    || D3D.inputStateLTouch.Touches == 8704
                    || D3D.inputStateLTouch.Touches == 256
                    || D3D.inputStateLTouch.Touches == 0
                    || D3D.inputStateLTouch.Touches == 8192
                    || D3D.inputStateLTouch.Touches == 11008
                    || D3D.inputStateLTouch.Touches == 9728
                    || D3D.inputStateLTouch.Touches == 10240
                    || D3D.inputStateLTouch.Touches == 9984)
                {
                    typeofsensortouchL = D3D.inputStateLTouch.Touches;
                }
                else
                {
                    typeofsensortouchL = 9999999;
                }

                if (lasttypeofsensortouchL != D3D.inputStateLTouch.Touches)
                {
                    //Console.WriteLine(D3D.inputStateLTouch.Touches);
                }


                lasttypeofsensortouchL = D3D.inputStateLTouch.Touches;


                //1
                //33
                //96
                //35
                //32
                //43
                //34
                //36
                //37
                //39
                //41
                //64
                //0
                //3
                //20

                //index is being pressed.
                if (D3D.inputStateRTouch.Touches == 1
                    || D3D.inputStateRTouch.Touches == 33
                    || D3D.inputStateRTouch.Touches == 96
                    || D3D.inputStateRTouch.Touches == 35
                    || D3D.inputStateRTouch.Touches == 32
                    || D3D.inputStateRTouch.Touches == 43
                    || D3D.inputStateRTouch.Touches == 34
                    || D3D.inputStateRTouch.Touches == 36
                    || D3D.inputStateRTouch.Touches == 37
                    || D3D.inputStateRTouch.Touches == 39
                    || D3D.inputStateRTouch.Touches == 41
                    || D3D.inputStateRTouch.Touches == 64
                    || D3D.inputStateRTouch.Touches == 0
                    || D3D.inputStateRTouch.Touches == 3
                    || D3D.inputStateRTouch.Touches == 20)
                {
                    typeofsensortouchR = D3D.inputStateRTouch.Touches;
                }
                else
                {
                    typeofsensortouchR = 9999999;
                }

                if (lasttypeofsensortouchR != D3D.inputStateRTouch.Touches)
                {
                    //Console.WriteLine(D3D.inputStateRTouch.Touches);
                }


                lasttypeofsensortouchR = D3D.inputStateRTouch.Touches;
                */



                if (Program._useOculusRift == 1)
                {
                    //HEADSET POSITION
                    displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                    trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                    latencyMark = false;
                    trackState = D3D.OVR.GetTrackingState(D3D.sessionPtr, 0.0f, latencyMark);
                    poseStatefer = trackState.HeadPose;
                    hmdPose = poseStatefer.ThePose;
                    hmdRot = hmdPose.Orientation;
                    _hmdPoser = new Vector3(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                    _hmdRoter = new Quaternion(hmdPose.Orientation.X, hmdPose.Orientation.Y, hmdPose.Orientation.Z, hmdPose.Orientation.W);

                    //SET CAMERA POSITION
                    Camera.SetPosition(hmdPose.Position.X, hmdPose.Position.Y, hmdPose.Position.Z);
                    Quaternion quatter = new Quaternion(hmdRot.X, hmdRot.Y, hmdRot.Z, hmdRot.W);
                    Vector3 oculusRiftDir = sc_maths._getDirection(Vector3.ForwardRH, quatter);


                    Matrix.RotationQuaternion(ref quatter, out hmd_matrix);

                    Matrix.RotationQuaternion(ref quatter, out hmd_matrix_test);

                    hmd_matrix_test = hmd_matrix_test * finalRotationMatrix;

                    //TOUCH CONTROLLER RIGHT
                    resultsRight = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeRTouch, ref D3D.inputStateRTouch);

                    buttonPressedOculusTouchRight = D3D.inputStateRTouch.Buttons;

                    thumbStickRight = D3D.inputStateRTouch.Thumbstick;
                    handTriggerRight = D3D.inputStateRTouch.HandTrigger;
                    indexTriggerRight = D3D.inputStateRTouch.IndexTrigger;
                    handPoseRight = trackingState.HandPoses[1].ThePose;

                    _rightTouchQuat.X = handPoseRight.Orientation.X;
                    _rightTouchQuat.Y = handPoseRight.Orientation.Y;
                    _rightTouchQuat.Z = handPoseRight.Orientation.Z;
                    _rightTouchQuat.W = handPoseRight.Orientation.W;

                    _rightTouchQuat = new SharpDX.Quaternion(handPoseRight.Orientation.X, handPoseRight.Orientation.Y, handPoseRight.Orientation.Z, handPoseRight.Orientation.W);
                    SharpDX.Matrix.RotationQuaternion(ref _rightTouchQuat, out _rightTouchMatrix);

                    _rightTouchMatrix.M41 = handPoseRight.Position.X + originPos.X + movePos.X;
                    _rightTouchMatrix.M42 = handPoseRight.Position.Y + originPos.Y + movePos.Y;
                    _rightTouchMatrix.M43 = handPoseRight.Position.Z + originPos.Z + movePos.Z;

                    //TOUCH CONTROLLER LEFT
                    resultsLeft = D3D.OVR.GetInputState(D3D.sessionPtr, D3D.controllerTypeLTouch, ref D3D.inputStateLTouch);
                    buttonPressedOculusTouchLeft = D3D.inputStateLTouch.Buttons;


                    thumbStickLeft = D3D.inputStateLTouch.Thumbstick;
                    handTriggerLeft = D3D.inputStateLTouch.HandTrigger;
                    indexTriggerLeft = D3D.inputStateLTouch.IndexTrigger;
                    handPoseLeft = trackingState.HandPoses[0].ThePose;

                    _leftTouchQuat.X = handPoseLeft.Orientation.X;
                    _leftTouchQuat.Y = handPoseLeft.Orientation.Y;
                    _leftTouchQuat.Z = handPoseLeft.Orientation.Z;
                    _leftTouchQuat.W = handPoseLeft.Orientation.W;

                    _leftTouchQuat = new SharpDX.Quaternion(handPoseLeft.Orientation.X, handPoseLeft.Orientation.Y, handPoseLeft.Orientation.Z, handPoseLeft.Orientation.W);

                    SharpDX.Matrix.RotationQuaternion(ref _leftTouchQuat, out _leftTouchMatrix);
                    //_other_left_touch_matrix = _leftTouchMatrix;
                    //_other_left_touch_matrix.M41 = handPoseLeft.Position.X;
                    //_other_left_touch_matrix.M42 = handPoseLeft.Position.Y;
                    //_other_left_touch_matrix.M43 = handPoseLeft.Position.Z;

                    _leftTouchMatrix.M41 = handPoseLeft.Position.X + originPos.X + movePos.X;
                    _leftTouchMatrix.M42 = handPoseLeft.Position.Y + originPos.Y + movePos.Y;
                    _leftTouchMatrix.M43 = handPoseLeft.Position.Z + originPos.Z + movePos.Z;







                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.A))
                    {
                        roty -= speedRot;
                    }
                    else if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.D))
                    {
                        roty += speedRot;
                    }



                    //Matrix tempmat = rotatingMatrixForPelvis;
                    Matrix tempmat = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;
                    Quaternion otherQuat;
                    Quaternion.RotationMatrix(ref tempmat, out otherQuat);

                    Vector3 direction_feet_forward;
                    Vector3 direction_feet_right;
                    Vector3 direction_feet_up;

                    direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);



                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Up))
                    {
                        //direction_feet_forward.Z += speed * speedPos;
                        movePos += direction_feet_forward * speedPos * speedPos;
                    }
                    else if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Down))
                    {
                        movePos -= direction_feet_forward * speedPos * speedPos;
                        //direction_feet_forward.Z -= speed * speedPos;
                    }
                    else if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Q))
                    {
                        movePos += direction_feet_up * speedPos * speedPos;
                        //direction_feet_forward.Y += speed * speedPos;
                    }
                    else if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Z))
                    {
                        movePos -= direction_feet_up * speedPos * speedPos;
                        //direction_feet_forward.Y -= speed * speedPos;
                    }
                    else if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Left))
                    {
                        movePos -= direction_feet_right * speedPos * speedPos;
                        //direction_feet_forward.X -= speed * speedPos;
                    }
                    else if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Right))
                    {
                        movePos += direction_feet_right * speedPos * speedPos;
                        //direction_feet_forward.X += speed * speedPos;
                    }



                    OFFSETPOS = originPos + movePos + _hmdPoser;


                }










                scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;













                if (sharpdxscreencapture != null)
                {
                    // Program.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);



                    for (int i = 0; i < 1;)
                    {
                        screencaptureresultswtc = 0;
                        try
                        {
                            screencaptureframe = sharpdxscreencapture.ScreenCapture(3);




                            if (scgraphicssecpackagemessage.scjittertasks != null)
                            {
                                if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0].Length > 0)
                                    {
                                        scgraphicssecpackagemessage.scjittertasks[0][0].frameByteArray = screencaptureframe.frameByteArray;
                                        scgraphicssecpackagemessage.scjittertasks[0][0].shaderresource = screencaptureframe.ShaderResource;

                                        //Program.MessageBox((IntPtr)0, "test2", "sccsmsg", 0);
                                    }
                                }
                            }


                        }
                        catch (Exception ex)
                        {
                            sharpdxscreencapture = new sccssharpdxscreencapture(0, 0, D3D.device);
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

                //Program.MessageBox((IntPtr)0, "test0", "sccsmsg", 0);

                if (D3D != null && Program.exitedprogram != 1)
                {
                    if (D3D.OVR != null && stopovr == 0)
                    {












                        Vector3f[] hmdToEyeViewOffsets = { D3D.eyeTextures[0].HmdToEyeViewOffset, D3D.eyeTextures[1].HmdToEyeViewOffset };
                        displayMidpoint = D3D.OVR.GetPredictedDisplayTime(D3D.sessionPtr, 0);
                        trackingState = D3D.OVR.GetTrackingState(D3D.sessionPtr, displayMidpoint, true);
                        eyePoses = new Posef[2];
                        D3D.OVR.CalcEyePoses(trackingState.HeadPose.ThePose, hmdToEyeViewOffsets, ref eyePoses);

                        //float timeSinceStart = (float)(DateTime.Now - startTime).TotalSeconds;

                        for (int eyeIndex = 0; eyeIndex < 2; eyeIndex++) // 2
                        {
                            Matrix someextrapelvismatrix = rotatingMatrixForPelvis; //originRot



                            eye = (EyeType)eyeIndex;
                            eyeTexture = D3D.eyeTextures[eyeIndex];

                            if (eyeIndex == 0)
                            {
                                D3D.layerEyeFov.RenderPoseLeft = eyePoses[0];
                            }
                            else
                            {
                                D3D.layerEyeFov.RenderPoseRight = eyePoses[1];
                            }
                            // Update the render description at each frame, as the HmdToEyeOffset can change at runtime.
                            eyeTexture.RenderDescription = OVR.GetRenderDesc(sessionPtr, eye, hmdDesc.DefaultEyeFov[eyeIndex]);

                            // Retrieve the index of the active texture
                            int textureIndex;
                            D3D.result = eyeTexture.SwapTextureSet.GetCurrentIndex(out textureIndex);
                            D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to retrieve texture swap chain current index.");

                            D3D.device.ImmediateContext.OutputMerger.SetRenderTargets(eyeTexture.DepthStencilView, eyeTexture.RenderTargetViews[textureIndex]);
                            D3D.device.ImmediateContext.ClearRenderTargetView(eyeTexture.RenderTargetViews[textureIndex], SharpDX.Color.Black); //DimGray Black //LightGray
                            D3D.device.ImmediateContext.ClearDepthStencilView(eyeTexture.DepthStencilView, DepthStencilClearFlags.Depth | DepthStencilClearFlags.Stencil, 1.0f, 0);
                            D3D.device.ImmediateContext.Rasterizer.SetViewport(eyeTexture.Viewport);


                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            SharpDX.Matrix eyeQuaternionMatrix = SharpDX.Matrix.RotationQuaternion(new SharpDX.Quaternion(eyePoses[eyeIndex].Orientation.X, eyePoses[eyeIndex].Orientation.Y, eyePoses[eyeIndex].Orientation.Z, eyePoses[eyeIndex].Orientation.W));
                            SharpDX.Vector3 eyePos = SharpDX.Vector3.Transform(new SharpDX.Vector3(eyePoses[eyeIndex].Position.X, eyePoses[eyeIndex].Position.Y, eyePoses[eyeIndex].Position.Z), originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot).ToVector3();
                            //finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix;
                            finalRotationMatrix = eyeQuaternionMatrix * originRot * rotatingMatrix * rotatingMatrixForPelvis * hmdmatrixRot;
                            lookUp = Vector3.Transform(new Vector3(0, 1, 0), finalRotationMatrix).ToVector3();
                            lookAt = Vector3.Transform(new Vector3(0, 0, -1), finalRotationMatrix).ToVector3();
                            viewpositionorigin = eyePos;
                            viewPosition = eyePos + OFFSETPOS; // 
                            tempmatter = hmd_matrix * rotatingMatrixForPelvis * hmdmatrixRot;

                            Quaternion quatt;
                            Quaternion.RotationMatrix(ref tempmatter, out quatt);

                            if (Program.usethirdpersonview == 0)
                            {

                                //FOR THE VERTEX SHADER
                                Quaternion somedirquat1;
                                Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                                dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);
                            }
                            else if (Program.usethirdpersonview == 1)
                            {
                                Quaternion somedirquat1;
                                Quaternion.RotationMatrix(ref tempmatter, out somedirquat1);
                                dirikvoxelbodyInstanceRight0 = new Vector4(-sc_maths._newgetdirleft(somedirquat1), 0);
                                dirikvoxelbodyInstanceUp0 = new Vector4(sc_maths._newgetdirup(somedirquat1), 0);
                                dirikvoxelbodyInstanceForward0 = new Vector4(sc_maths._newgetdirforward(somedirquat1), 0);

                                viewPosition = viewPosition + (new Vector3(dirikvoxelbodyInstanceForward0.X, dirikvoxelbodyInstanceForward0.Y, dirikvoxelbodyInstanceForward0.Z) * Program.offsetthirdpersonview);
                            }





                            viewMatrix = Matrix.LookAtRH(viewPosition, viewPosition + lookAt, lookUp);
                            _projectionMatrix = D3D.OVR.Matrix4f_Projection(eyeTexture.FieldOfView, 0.001f, 1000.0f, ProjectionModifier.None).ToMatrix();
                            oriProjectionMatrix = _projectionMatrix;
                            _projectionMatrix.Transpose();


                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.
                            //CODE REFERENCED FROM ANDREJ BENEDIK'S OCULUS RIFT DXENGINE.OCULUSWRAP SAMPLE ON GITHUB WHICH ISN'T IN A REPO THAT IS MIT LICENSED. BUT IN ORDER TO HAVE INVERSE KINEMATICS ROTATIONS OF LIMBS WORK, I HAD TO ADD THINGS.




                            //Program.MessageBox((IntPtr)0, "0", "sc core systems message", 0);
                            if (canworkphysics == 1)
                            {
                                scgraphicssecpackagemessage.viewMatrix = viewMatrix;
                                scgraphicssecpackagemessage.projectionMatrix = _projectionMatrix;
                                scgraphicssecpackagemessage.originRot = originRot;
                                scgraphicssecpackagemessage.rotatingMatrix = rotatingMatrix;
                                scgraphicssecpackagemessage.hmdmatrixRot = hmdmatrixRot;
                                scgraphicssecpackagemessage.hmd_matrix = hmd_matrix;
                                scgraphicssecpackagemessage.rotatingMatrixForPelvis = rotatingMatrixForPelvis;
                                scgraphicssecpackagemessage.rightTouchMatrix = _rightTouchMatrix;
                                scgraphicssecpackagemessage.leftTouchMatrix = _leftTouchMatrix;
                                scgraphicssecpackagemessage.oriProjectionMatrix = oriProjectionMatrix;
                                scgraphicssecpackagemessage.someextrapelvismatrix = someextrapelvismatrix;
                                scgraphicssecpackagemessage.offsetpos = OFFSETPOS;
                                scgraphicssecpackagemessage.handPoseRight = handPoseRight;
                                scgraphicssecpackagemessage.handPoseLeft = handPoseLeft;
                                //scgraphicssecpackagemessage.scgraphicssec = null;
                                //scgraphicssecpackagemessage.scjittertasks = _sc_jitter_tasks;


                                if (updatethreadupdateswtc0 == 0)
                                {


                                    scgraphicssecpackagemessage.scgraphicssec = new sccs.scgraphicssec();
                                    scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.createarraysforallvoxelassets(scgraphicssecpackagemessage.scjittertasks);



                                    main_thread_update0 = new Thread(() =>
                                    {
                                        int threadupdateswtc = 0;

                                    _thread_looper:
                                        try
                                        {

                                            //_ticks_watch.Stop();
                                            //_ticks_watch.Restart();


                                            if (threadupdateswtc == 0) //0
                                            {
                                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                                {
                                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                                    {
                                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                                        {

                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.sccswriteikrigtobuffer(scgraphicssecpackagemessage);
                                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                                            // scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.scwritedirectiontobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                            scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks, out hasfinishedframe0);


                                                        }
                                                    }
                                                }
                                            }

                                            if (updatethreadupdateswtc1 == 0)
                                            {
                                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                                {
                                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                                    {
                                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                                        {
                                                            scgraphicssecpackagemessage.scgraphicssec.oculuscontrolsNRecordSoundNMousePointer(scgraphicssecpackagemessage.projectionMatrix, viewMatrix);
                                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.sccswritescreenassetstobuffer(scgraphicssecpackagemessage.scjittertasks);
                                                        }
                                                    }
                                                }
                                                //updatethreadupdateswtc1 = 1;
                                            }



                                            //Console.WriteLine(_ticks_watch.Elapsed.Milliseconds);


                                        }
                                        catch (Exception ex)
                                        {
                                            Program.MessageBox((IntPtr)0, "001" + ex.ToString(), "sc core systems message", 0);
                                        }

                                        Thread.Sleep(1);
                                        goto _thread_looper;

                                        //ShutDown();
                                        //ShutDownGraphics();

                                    }, 0);

                                    main_thread_update0.IsBackground = true;
                                    main_thread_update0.Priority = ThreadPriority.Lowest; //AboveNormal
                                    main_thread_update0.SetApartmentState(ApartmentState.STA);
                                    main_thread_update0.Start();



                                    updatethreadupdateswtc0 = 1;
                                }




                                if (scgraphicssecpackagemessage.scjittertasks != null)
                                {
                                    if (scgraphicssecpackagemessage.scjittertasks[0] != null)
                                    {
                                        if (scgraphicssecpackagemessage.scgraphicssec != null && scgraphicssecpackagemessage.scgraphicssec.hasinit == 1)
                                        {
                                            //Program.MessageBox((IntPtr)0, "test", "sccsmsg", 0);
                                            //scgraphicssecpackagemessage.scjittertasks = 
                                            //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);

                                            //scgraphicssecpackagemessage.scgraphicssec.scwritetobuffer(scgraphicssecpackagemessage.scjittertasks);
                                            //scgraphicssecpackagemessage.scjittertasks= scgraphicssecpackagemessage.scgraphicssec.writetobufferagain(scgraphicssecpackagemessage.scjittertasks);

                                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scgraphicssec.workonvoxelterrain(scgraphicssecpackagemessage);
                                            scgraphicssecpackagemessage.scjittertasks = scgraphicssecpackagemessage.scgraphicssec.workonshaders(scgraphicssecpackagemessage);
                                        }
                                    }
                                }
                            }


                            //_sc_jitter_tasks = scgraphicssecpackagemessage.scjittertasks;

                            D3D.result = eyeTexture.SwapTextureSet.Commit();
                            D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to commit the swap chain texture.");
                        }


                        D3D.result = D3D.OVR.SubmitFrame(D3D.sessionPtr, 0L, IntPtr.Zero, ref D3D.layerEyeFov);

                        if (D3D != null)
                        {
                            if (D3D.OVR != null)
                            {
                                /*if (D3D.result != null && Program.exitedprogram != 1)
                                {
                                    //D3D.WriteErrorDetails(D3D.OVR, D3D.result, "Failed to submit the frame of the current layers.");
                                }*/
                            }
                            D3D.DeviceContext.CopyResource(D3D.mirrorTextureD3D, D3D.BackBuffer);
                            D3D.SwapChain.Present(0, PresentFlags.None);
                        }
                    }
                }
            }



            if (canworkphysics != -1) //-1 to exit program safe
            {
                canworkphysics = 1;
            }

            //_can_work_physics_objects = 1;

            //Program.MessageBox((IntPtr)0, "001", "sc core systems message", 0);


            return _sc_jitter_tasks;
        }


        private void updateplayermovementandrotation()
        {
            if (Program._useOculusRift == 0 && Program.keynmouseinput != null)
            {
                //Program.keynmouseinput.Frame();

                //float speed = 0.015f;
                //float speedRot = 0.1f;

                if (sccsvvdhd.Form1.currentform.canmovethecamera == 0)
                {
                    if (fasterorslowermul < 0.0001f)
                    {
                        fasterorslowermul = 0.0001f;
                    }
                    if (fasterorslowermul > 1)
                    {
                        fasterorslowermul = 1;
                    }

                    if (speedRot < 0.0001f)
                    {
                        speedRot = 0.0001f;
                    }
                    if (speedRot > 1)
                    {
                        speedRot = 1;
                    }

                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.A))
                    {
                        //speedRot *= fasterorslowermul;

                        roty -= speedRot;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.D))
                    {
                        //speedRot *= fasterorslowermul;

                        roty += speedRot;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.R))
                    {
                        //speedRot *= fasterorslowermul;

                        rotx -= speedRot;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.F))
                    {
                        //speedRot *= fasterorslowermul;

                        rotx += speedRot;
                    }

                    var somerot = Camera.GetRotation();
                    Camera.SetRotation(rotx, roty, somerot.Z);

                }

                if (sccsvvdhd.Form1.currentform.canmovethecamera == 0)
                {
                    Matrix tempmat = Camera.rotationMatrix;
                    Quaternion otherQuat;
                    Quaternion.RotationMatrix(ref tempmat, out otherQuat);

                    Vector3 direction_feet_forward;
                    Vector3 direction_feet_right;
                    Vector3 direction_feet_up;

                    direction_feet_forward = sc_maths._getDirection(Vector3.ForwardRH, otherQuat);
                    direction_feet_right = sc_maths._getDirection(Vector3.Right, otherQuat);
                    direction_feet_up = sc_maths._getDirection(Vector3.Up, otherQuat);

                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Up))
                    {
                        //Program.MessageBox((IntPtr)0, "000", "sc core systems message", 0);
                        //direction_feet_forward.Z += speed * speedPos;
                        movePos -= direction_feet_forward * speedPos * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Down))
                    {
                        movePos += direction_feet_forward * speedPos * speedPos;
                        //direction_feet_forward.Z -= speed * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Q))
                    {
                        movePos += direction_feet_up * speedPos * speedPos;
                        //direction_feet_forward.Y += speed * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Z))
                    {
                        movePos -= direction_feet_up * speedPos * speedPos;
                        //direction_feet_forward.Y -= speed * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Left))
                    {
                        movePos -= direction_feet_right * speedPos * speedPos;
                        //direction_feet_forward.X -= speed * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Right))
                    {
                        movePos += direction_feet_right * speedPos * speedPos;
                        //direction_feet_forward.X += speed * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Add))
                    {

                        speedPos += 0.001f;
                        speedRot += 0.001f;
                        //direction_feet_forward.X -= speed * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Subtract))
                    {
                        if (speedPos > 0)
                        {
                            speedPos -= 0.001f;
                        }
                        if (speedPos < 0.0001f)
                        {
                            speedPos = 0.0001f;
                        }

                        if (speedRot > 0)
                        {
                            speedRot -= 0.001f;
                        }
                        if (speedRot < 0.0001f)
                        {
                            speedRot = 0.0001f;
                        }
                        //direction_feet_forward.X += speed * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Multiply))
                    {

                        if (fasterorslowermul < 0.0001f)
                        {
                            fasterorslowermul = 0.0001f;
                        }
                        if (fasterorslowermul > 1)
                        {
                            fasterorslowermul = 1;
                        }
                        fasterorslowermul *= 1.012345f;

                        speedPos *= fasterorslowermul;
                        speedRot *= fasterorslowermul;

                        //direction_feet_forward.X -= speed * speedPos;
                    }
                    if (Program.keynmouseinput._KeyboardState != null && Program.keynmouseinput._KeyboardState.PressedKeys.Contains(Key.Divide))
                    {


                        fasterorslowermul /= 1.012345f;

                        if (fasterorslowermul < 0.0001f)
                        {
                            fasterorslowermul = 0.0001f;
                        }
                        if (fasterorslowermul > 1)
                        {
                            fasterorslowermul = 1;
                        }
                        speedPos *= fasterorslowermul;
                        speedRot *= fasterorslowermul;


                        //direction_feet_forward.X += speed * speedPos;
                    }











                    OFFSETPOS = originPos + movePos;

                    Camera.SetPosition(OFFSETPOS.X, OFFSETPOS.Y, OFFSETPOS.Z);
                }
            }
        }




        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out RECT lpRect);
        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool PrintWindow(IntPtr hwnd, IntPtr hDC, uint nFlags);
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}

