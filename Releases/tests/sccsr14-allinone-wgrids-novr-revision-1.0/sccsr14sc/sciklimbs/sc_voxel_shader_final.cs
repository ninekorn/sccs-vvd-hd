﻿using SharpDX;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D11;
using System;
using System.Runtime.InteropServices;
using Device = SharpDX.Direct3D11.Device;

using sccs.scconsole;


namespace sccs.scgraphics
{
    public class sc_voxel_shader_final                 // 228 lines
    {
        DataStream mappedResource;

        [StructLayout(LayoutKind.Sequential)]
        internal struct DMatrixBuffer
        {
            public Matrix world;
            public Matrix view;
            public Matrix projection;
        }

        public GeometryShader GeometryShader { get; set; }
        
        public VertexShader VertexShader { get; set; }
        public PixelShader PixelShader { get; set; }
        public InputLayout Layout { get; set; }
        public SharpDX.Direct3D11.Buffer ConstantMatrixBuffer { get; set; }
        public SamplerState SamplerState { get; set; }

        public sc_voxel_shader_final()
        {

        }

        public SharpDX.Direct3D11.Buffer _constantLightBuffer;

        public sccs.scgraphics.sc_voxel.DLightBuffer[] _DLightBuffer;

        public bool Initialize(Device device, IntPtr windowsHandler, SharpDX.Direct3D11.Buffer ConstantLightBuffer, sccs.scgraphics.sc_voxel.DLightBuffer[] DLightBuffer)
        {
            this._DLightBuffer = DLightBuffer;
            this._constantLightBuffer = ConstantLightBuffer;
            return InitializeShader(device, windowsHandler, "texture_voxel.vs", "texture_voxel.ps");
        }

        ShaderBytecode vertexShaderByteCode;
        ShaderBytecode pixelShaderByteCode;
        private bool InitializeShader(Device device, IntPtr windowsHandler, string vsFileName, string psFileName)
        {
            try
            {
                /*vsFileName = "../../../sc_instance_shader/" + "texture_voxel.vs";
                psFileName = "../../../sc_instance_shader/" + "texture_voxel.ps";

                vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileName, "TextureVertexShader", "vs_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileName, "TexturePixelShader", "ps_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                */
                /*
                if (Program.is_wpf == 1)
                {
                    vsFileName = sccs.Properties.Resources.texture_voxel1;// "../../../_sc_instance_shader/" + "texture.vs";
                    psFileName = sccs.Properties.Resources.texture_voxel;// "../../../_sc_instance_shader/" + "texture.ps";

                     vertexShaderByteCode = ShaderBytecode.Compile(vsFileName, "TextureVertexShader", "vs_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                     pixelShaderByteCode = ShaderBytecode.Compile(psFileName, "TexturePixelShader", "ps_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);

                }
                else
                {
                  }*/



                /**/


                vsFileName = sccsr14sc.Properties.Resources.texture_voxelVS;// "../../../_sc_instance_shader/" + "texture.vs";
                psFileName = sccsr14sc.Properties.Resources.texture_voxelPS;// "../../../_sc_instance_shader/" + "texture.ps";

                //vertexShaderByteCode = ShaderBytecode.Compile(vsFileName, "TextureVertexShader", "vs_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                //pixelShaderByteCode = ShaderBytecode.Compile(psFileName, "TexturePixelShader", "ps_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                var gsFileName = sccsr14sc.Properties.Resources.HLSL;// "../../../_sc_instance_shader/" + "texture.ps";

                vertexShaderByteCode = ShaderBytecode.Compile(vsFileName, "TextureVertexShader", "vs_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                pixelShaderByteCode = ShaderBytecode.Compile(psFileName, "TexturePixelShader", "ps_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                var geometryShaderByteCode = ShaderBytecode.Compile(gsFileName, "GS", "gs_4_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);


                //VertexShader = new VertexShader(device, vertexShaderByteCode);
                //PixelShader = new PixelShader(device, pixelShaderByteCode);










                VertexShader = new VertexShader(device, vertexShaderByteCode);
                PixelShader = new PixelShader(device, pixelShaderByteCode);
                GeometryShader = new GeometryShader(device, geometryShaderByteCode);


                InputElement[] inputElements = new InputElement[]
                {
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "TEXCOORD",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "COLOR",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL",
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 1,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 2,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 2,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 3,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 3,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION",
                        SemanticIndex = 4,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 4,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                };

                Layout = new InputLayout(device, ShaderSignature.GetInputSignature(vertexShaderByteCode), inputElements);

                vertexShaderByteCode.Dispose();
                pixelShaderByteCode.Dispose();
                geometryShaderByteCode.Dispose();


                BufferDescription matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DMatrixBuffer>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                ConstantMatrixBuffer = new SharpDX.Direct3D11.Buffer(device, matrixBufferDescription);

                SamplerStateDescription samplerDesc = new SamplerStateDescription()
                {
                    Filter = Filter.MinMagMipLinear,
                    AddressU = TextureAddressMode.Wrap,
                    AddressV = TextureAddressMode.Wrap,
                    AddressW = TextureAddressMode.Wrap,
                    MipLodBias = 0,
                    MaximumAnisotropy = 1,
                    ComparisonFunction = Comparison.Always,
                    BorderColor = new Color4(0, 0, 0, 0),
                    MinimumLod = 0,
                    MaximumLod = float.MaxValue
                };

                SamplerState = new SamplerState(device, samplerDesc);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void ShutDown()
        {
            ShutdownShader();
        }
        private void ShutdownShader()
        {
            SamplerState?.Dispose();
            SamplerState = null;
            ConstantMatrixBuffer?.Dispose();
            ConstantMatrixBuffer = null;
            Layout?.Dispose();
            Layout = null;
            PixelShader?.Dispose();
            PixelShader = null;
            VertexShader?.Dispose();
            VertexShader = null;
        }


        public bool Render(DeviceContext deviceContext, int VertexCount, int InstanceCount, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            if (!SetShaderParameters(deviceContext, worldMatrix, viewMatrix, projectionMatrix, texture, _cuber._WORLDMATRIXINSTANCES))
                return false;

            RenderShader(deviceContext, _DLightBuffer_, _cuber);

            return true;
        }

        int _switchOnce = 0;
        int bufferPositionNumber = 0;
        DMatrixBuffer matrixBuffer = new DMatrixBuffer();

        private bool SetShaderParameters(DeviceContext deviceContext, Matrix worldMatrix, Matrix viewMatrix, Matrix projectionMatrix, ShaderResourceView texture, Matrix[] worldMatrix_instances)
        {
            try
            {
                worldMatrix.Transpose();
                viewMatrix.Transpose();
                projectionMatrix.Transpose();

                deviceContext.MapSubresource(ConstantMatrixBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);

                matrixBuffer.world = worldMatrix;
                matrixBuffer.view = viewMatrix;
                matrixBuffer.projection = projectionMatrix;

                mappedResource.Write(matrixBuffer);

                deviceContext.UnmapSubresource(ConstantMatrixBuffer, 0);

                deviceContext.VertexShader.SetConstantBuffer(bufferPositionNumber, ConstantMatrixBuffer);
                deviceContext.PixelShader.SetShaderResource(0, texture);


                mappedResource.Dispose();

                return true;
            }
            catch
            {
                return false;
            }
        }

        private void RenderShader(DeviceContext deviceContext, sc_voxel.DLightBuffer[] _DLightBuffer_, sc_voxel _cuber)
        {
            deviceContext.MapSubresource(_cuber.InstanceBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(_cuber.instances, 0, _cuber.instances.Length);
            deviceContext.UnmapSubresource(_cuber.InstanceBuffer, 0);
            mappedResource.Dispose();

            deviceContext.MapSubresource(_cuber.InstanceRotationBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(_cuber.instancesDataForward, 0, _cuber.instancesDataForward.Length);
            deviceContext.UnmapSubresource(_cuber.InstanceRotationBuffer, 0);
            mappedResource.Dispose();

            deviceContext.MapSubresource(_cuber.InstanceRotationBufferRIGHT, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(_cuber.instancesDataRIGHT, 0, _cuber.instancesDataRIGHT.Length);
            deviceContext.UnmapSubresource(_cuber.InstanceRotationBufferRIGHT, 0);
            mappedResource.Dispose();

            deviceContext.MapSubresource(_cuber.InstanceRotationBufferUP, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
            mappedResource.WriteRange(_cuber.instancesDataUP, 0, _cuber.instancesDataUP.Length);
            deviceContext.UnmapSubresource(_cuber.InstanceRotationBufferUP, 0);
            mappedResource.Dispose();

            if (_switchOnce == 0)
            {
                try
                {
                    deviceContext.MapSubresource(_constantLightBuffer, MapMode.WriteDiscard, SharpDX.Direct3D11.MapFlags.None, out mappedResource);
                    mappedResource.WriteRange(_DLightBuffer_, 0, _DLightBuffer_.Length);
                    deviceContext.UnmapSubresource(_constantLightBuffer, 0);
                    mappedResource.Dispose();
                }
                catch (Exception ex)
                {
                    Program.MessageBox((IntPtr)0, ex.ToString() + "", "Oculus Error", 0);
                }
                //_switchOnce = 1;
            }

            deviceContext.InputAssembler.InputLayout = Layout;
            deviceContext.VertexShader.Set(VertexShader);
            deviceContext.PixelShader.Set(PixelShader);
            deviceContext.PixelShader.SetConstantBuffer(0, _constantLightBuffer);
            deviceContext.GeometryShader.Set(null);
            deviceContext.PixelShader.SetSampler(0, SamplerState);
            deviceContext.DrawIndexedInstanced(_cuber.IndexCount, _cuber.InstanceCount, 0, 0, 0);

        }
    }
}