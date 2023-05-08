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
//using System.Windows.Forms;
using System.Runtime.ConstrainedExecution;

namespace WinRT.GraphicsCapture
{
    public class GraphicsCapture : ICaptureMethod //internal
    {
        private static readonly Guid _graphicsCaptureItemIid = new Guid("79C3F95B-31F7-4EC2-A464-632EF5D30760");
        private Direct3D11CaptureFramePool _captureFramePool;
        private GraphicsCaptureItem _captureItem;
        private GraphicsCaptureSession _captureSession;

        public static bool HasClosedCapture;// { get; private set; }
        //public Form currentform;

        public int typeofcapture { get; set; }

        public GraphicsCapture()
        {
            IsCapturing = false;
            typeofcapture = 0;
        }

        public bool IsCapturing { get; private set; }


        //public bool HasClosedCapture { get; set; }

        public void Dispose()
        {
            StopCapture();
            Console.WriteLine("GraphicsCapture.cs Line 45 has stopped capture 2.");
        }





        public void StartCapture(IntPtr hWnd, Device device, Factory factory)
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



            var capturePicker = new WindowPicker();

            //Program.MessageBox((IntPtr)0, "handle:" + hWnd, "sccsmsg", 0);

            var captureHandle =  capturePicker.PickCaptureTarget(hWnd); //hWnd;//
            if (captureHandle == IntPtr.Zero)
                return;


            //Program.MessageBox((IntPtr)0, "handle:" + hWnd, "sccsmsg", 0);






            _captureItem = CreateItemForWindow(captureHandle);

            #endregion

            if (_captureItem == null)
                return;

            _captureItem.Closed += CaptureItemOnClosed;

            var hr = NativeMethods.CreateDirect3D11DeviceFromDXGIDevice(device.NativePointer, out var pUnknown);
            if (hr != 0)
            {
                StopCapture();
                return;
            }

            var winrtDevice = (IDirect3DDevice) Marshal.GetObjectForIUnknown(pUnknown);
            Marshal.Release(pUnknown);

            _captureFramePool = Direct3D11CaptureFramePool.Create(winrtDevice, DirectXPixelFormat.B8G8R8A8UIntNormalized, 2, _captureItem.Size);
            _captureSession = _captureFramePool.CreateCaptureSession(_captureItem);
            _captureSession.StartCapture();
            IsCapturing = true;
        }

        public Texture2D TryGetNextFrameAsTexture2D(Device device)
        {

            if (canclosecapture == 0)
            {
                IsCapturing = true;

            }
            else
            {

            }


            frame = _captureFramePool?.TryGetNextFrame();
            if (frame == null)
                return null;


            /*
            using var frame = _captureFramePool?.TryGetNextFrame();
            if (frame == null)
                return null;*/

            // ReSharper disable once SuspiciousTypeConversion.Global
            var surfaceDxgiInterfaceAccess = (IDirect3DDxgiInterfaceAccess) frame.Surface;
            var pResource = surfaceDxgiInterfaceAccess.GetInterface(new Guid("dc8e63f3-d12b-4952-b47b-5e45026a862d"));


            surfaceTexture = new Texture2D(pResource); // shared resource
            //using var surfaceTexture = new Texture2D(pResource); // shared resource
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


            return texture2d;
        }

        Texture2D surfaceTexture;
        Direct3D11CaptureFrame frame;
        Texture2D lastsurfaceTexture;
        Direct3D11CaptureFrame lastframe;


        public void StopCapture() // ...or release resources
        {
            _captureSession?.Dispose();
            _captureFramePool?.Dispose();
            _captureSession = null;
            _captureFramePool = null;

            //Marshal.Release(_captureItem);
            //DeleteObject(_captureItem);

            /*Marshal.Release(interop);
            Marshal.Release(factory);*/
            _captureItem = null;
            IsCapturing = false;

            lastframe = frame;
            if (lastframe != null)
            {
                lastframe.Dispose();
                lastframe = null;
            }

            lastsurfaceTexture = surfaceTexture;

            if (lastsurfaceTexture != null)
            {
                lastsurfaceTexture.Dispose();
                lastsurfaceTexture = null;
            }
            if (_captureItem != null)
            {
                GC.SuppressFinalize(_captureItem);
            }
            GC.Collect();
        }

        /*
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        override protected bool ReleaseHandle()
        {
            // Here, we must obey all rules for constrained execution regions.
            return NativeMethods.CloseHandle(handle);
            // If ReleaseHandle failed, it can be reported via the
            // "releaseHandleFailed" managed debugging assistant (MDA).  This
            // MDA is disabled by default, but can be enabled in a debugger
            // or during testing to diagnose handle corruption problems.
            // We do not throw an exception because most code could not recover
            // from the problem.
        }*/

        static GraphicsCaptureItem capture;


        // ReSharper disable once SuspiciousTypeConversion.Global
        private static GraphicsCaptureItem CreateItemForWindow(IntPtr hWnd)
        {
            
            //Program.MessageBox((IntPtr)0, "Graphics Capture handle:" + hWnd, "sccsmsg", 0);

            //Console.WriteLine(hWnd);

            if (hWnd != IntPtr.Zero)
            {
                var factory = WindowsRuntimeMarshal.GetActivationFactory(typeof(GraphicsCaptureItem));


                var interop = (IGraphicsCaptureItemInterop)factory;

                int errorcheck = 0;


                for (int i = 0; i < 9; i++)
                {
                    try
                    {

                        var pointer = interop.CreateForWindow(hWnd, typeof(GraphicsCaptureItem).GetInterface("IGraphicsCaptureItem").GUID);

                        //interop.


                        capture = Marshal.GetObjectForIUnknown(pointer) as GraphicsCaptureItem;

                        capture.Closed += Capture_Closed;



                        Marshal.Release(pointer);
                        DeleteObject(pointer);

                        /*Marshal.Release(interop);
                        Marshal.Release(factory);*/

                        GC.SuppressFinalize(factory);
                        GC.SuppressFinalize(interop);
                        GC.SuppressFinalize(pointer);
                        GC.Collect();
                        errorcheck = 1;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        //errorcheck = 0;
                    }

                    if (errorcheck == 1)
                    {
                        break;
                    }

                }



                if (errorcheck == 0)
                {
                    return null;

                }
                else
                {

                    return capture;
                }







                //return null;
            }
            else
            {
                return null;
            }
            //return null;
        }

        private static void Capture_Closed(GraphicsCaptureItem sender, object args)
        {

            Console.WriteLine("GraphicsCapture.cs Line 315. The capture was closed");
            //throw new NotImplementedException();

            //Program.MessageBox((IntPtr)0, "Capture_Closed", "sccsmsg", 0);
            //IsCapturing = false;
            //HasClosedCapture = true;
            HasClosedCapture = true;
            //IsCapturing = false;
            //StopCapture();
        }




        public static int canclosecapture = 0;
        private void CaptureItemOnClosed(GraphicsCaptureItem sender, object args)
        {
            canclosecapture = 1;
            IsCapturing = false;
            StopCapture();
            Console.WriteLine("test");
            
        }


        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
    }
}