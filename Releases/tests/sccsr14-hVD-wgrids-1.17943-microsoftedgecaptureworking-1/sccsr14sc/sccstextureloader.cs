//referenced from the rastertek tutorial series of github user dan6040. steve chassé aka ninekorn

using SharpDX.Direct3D11;
using SharpDX.WIC;

namespace SCCoreSystems.SC_Graphics
{
    public class sccstextureloader
    {
        public SharpDX.DataStream bufferfortests { get;  set; }
        public BitmapSource bitmapSource { get; private set; }
        public SharpDX.DataStream buffer { get; private set; }
        public Texture2D texture { get; private set; }
        public ShaderResourceView TextureResource { get; private set; }

        public bool Initialize(Device device, string fileName)
        {
            try
            {


                texture = LoadFromFile(device, new SharpDX.WIC.ImagingFactory(), fileName);
                //using (var texture = LoadFromFile(device, new SharpDX.WIC.ImagingFactory(), fileName))
                {
                    ShaderResourceViewDescription srvDesc = new ShaderResourceViewDescription()
                    {
                        Format = texture.Description.Format,
                        Dimension = SharpDX.Direct3D.ShaderResourceViewDimension.Texture2D,
                    };
                    srvDesc.Texture2D.MostDetailedMip = 0;
                    srvDesc.Texture2D.MipLevels = -1;

                    TextureResource = new ShaderResourceView(device, texture, srvDesc);
                    device.ImmediateContext.GenerateMips(TextureResource);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public void ShutDown()
        {
            TextureResource?.Dispose();
            TextureResource = null;
        }

        public Texture2D LoadFromFile(Device device, ImagingFactory factory, string fileName)
        {
            using (var bs = LoadBitmap(factory, fileName))
                return CreateTexture2DFromBitmap(device, bs);
        }
        public BitmapSource LoadBitmap(ImagingFactory factory, string filename)
        {
            var bitmapDecoder = new SharpDX.WIC.BitmapDecoder(
                factory,
                filename,
                SharpDX.WIC.DecodeOptions.CacheOnDemand
                );

            var result = new SharpDX.WIC.FormatConverter(factory);

            result.Initialize(
                bitmapDecoder.GetFrame(0),
                SharpDX.WIC.PixelFormat.Format32bppPRGBA,
                SharpDX.WIC.BitmapDitherType.None,
                null,
                0.0,
                SharpDX.WIC.BitmapPaletteType.Custom);

            return result;
        }
        public Texture2D CreateTexture2DFromBitmap(Device device, BitmapSource bitmapSource)
        {
            int stride = bitmapSource.Size.Width * 4;
            buffer = new SharpDX.DataStream(bitmapSource.Size.Height * stride, true, true);

            //using (SharpDX.DataStream buffer = new SharpDX.DataStream(bitmapSource.Size.Height * stride, true, true))
            {
                bitmapSource.CopyPixels(stride, buffer);
                return new SharpDX.Direct3D11.Texture2D(device, new SharpDX.Direct3D11.Texture2DDescription()
                {
                    Width = bitmapSource.Size.Width,
                    Height = bitmapSource.Size.Height,
                    ArraySize = 1,
                    BindFlags = SharpDX.Direct3D11.BindFlags.ShaderResource | BindFlags.RenderTarget,
                    Usage = SharpDX.Direct3D11.ResourceUsage.Default,
                    CpuAccessFlags = SharpDX.Direct3D11.CpuAccessFlags.None,
                    Format = SharpDX.DXGI.Format.R8G8B8A8_UNorm,
                    MipLevels = 1,
                    OptionFlags = ResourceOptionFlags.GenerateMipMaps,
                    SampleDescription = new SharpDX.DXGI.SampleDescription(1, 0),
                }, new SharpDX.DataRectangle(buffer.DataPointer, stride));
                


                /*
                bitmapSource.CopyPixels(stride, buffer);
                return new SharpDX.Direct3D11.Texture2D(device, new SharpDX.Direct3D11.Texture2DDescription()
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                    Format = SharpDX.DXGI.Format.B8G8R8A8_UNorm,
                    Width = bitmapSource.Size.Width,
                    Height = bitmapSource.Size.Height,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Staging
                }, new SharpDX.DataRectangle(buffer.DataPointer, stride));
                */
                

                /*
                this._textureDescription = new Texture2DDescription
                {
                    CpuAccessFlags = CpuAccessFlags.Read,
                    BindFlags = BindFlags.None,//BindFlags.None, //| BindFlags.RenderTarget
                    Format = Format.B8G8R8A8_UNorm,
                    Width = _width,
                    Height = _height,
                    OptionFlags = ResourceOptionFlags.None,
                    MipLevels = 1,
                    ArraySize = 1,
                    SampleDescription = { Count = 1, Quality = 0 },
                    Usage = ResourceUsage.Staging
                };*/
            }
        }
    }
}