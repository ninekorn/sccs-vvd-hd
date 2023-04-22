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

namespace WinRT.GraphicsCapture
{
    internal class GraphicsCapture : ICaptureMethod, IDisposable
    {
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
            DeleteObject(pointer);
            DeleteObject(pResource);

            capture = null;
            //this.Dispose();
            StopCapture();
        }



        public IntPtr _hWnd;
        WindowPicker picker;
        IDirect3DDevice winrtDevice;
        uint hr;

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

            using (picker = new WindowPicker())
            {





                _hWnd = picker.PickCaptureTarget(hWnd);

                capturedwindowname = picker.selectedwindowname;


                
                Program.WINDOWPLACEMENT param = new Program.WINDOWPLACEMENT();
                param.Length = Marshal.SizeOf(typeof(Program.WINDOWPLACEMENT));
                
                
                



                
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



                therect = new Program.RECT();
                therect.Left = 0;
                therect.Top = 0;
                therect.Bottom = 1080;
                therect.Right = 1920;

                param.ShowCmd = Program.ShowWindowCommands.ShowDefault; //SW_SHOWNORMAL
                param.NormalPosition = therect;// new RECT(0, 0, 500, 500);
                Program.SetWindowPlacement(_hWnd, ref param);

            
                /*
                 int screenWidth = Program.GetSystemMetrics(0);
                 int screenHeight = Program.GetSystemMetrics(1);
                 Program.SetWindowPos(_hWnd, (IntPtr)Program.SpecialWindowHandles.HWND_NOTOPMOST, 0, 0, screenWidth, screenHeight, Program.SWP_SHOWWINDOW); //| (uint)SetWindowPosFlags.SWP_DRAWFRAME
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


        Texture2DDescription texture2dDescription;

        int swtc = 0;
        Texture2D texture2d;

        public Texture2D TryGetNextFrameAsTexture2D(Device device)
        {
            using var frame = _captureFramePool?.TryGetNextFrame();
            if (frame == null)
                return null;

            // ReSharper disable once SuspiciousTypeConversion.Global
            surfaceDxgiInterfaceAccess = (IDirect3DDxgiInterfaceAccess) frame.Surface;

          

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

            if (surfaceDxgiInterfaceAccess!= null)
            {
                surfaceDxgiInterfaceAccess.Dispose();
                surfaceDxgiInterfaceAccess = null;
            }

            if (frame!= null)
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
     

            if (texture2d!= null)
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
            DeleteObject(pointer);
            DeleteObject(pResource);

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