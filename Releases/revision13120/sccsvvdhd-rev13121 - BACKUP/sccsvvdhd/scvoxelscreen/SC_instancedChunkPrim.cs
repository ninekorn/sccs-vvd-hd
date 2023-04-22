using System;
using SharpDX;
using System.Runtime.InteropServices;

using sccs.scgraphics;

using SharpDX.Direct3D;
using SharpDX.Direct3D11;

using System.Drawing;
using Jitter;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;

using Jitter.LinearMath;
using System.Collections;
using System.Collections.Generic;

using SharpDX.D3DCompiler;


namespace sccs
{
    public class SC_instancedChunkPrim
    {

        InputElement[] inputElements;

        public SharpDX.Direct3D11.Buffer ConstantTessellationBuffer;

        [StructLayout(LayoutKind.Sequential)]
        public struct DTessellationBufferType
        {
            public float tessellationAmount;
            public Vector3 padding;
        }

        public void ShutDown()
        {
            // Release the model texture.
            ReleaseTexture();
            // Release the vertex and index buffers.
            ShutdownBuffers();
        }
        private void ReleaseTexture()
        {
            // Release the texture object.
            //Texture?.ShutDown();
            //Texture = null;
        }
        private void ShutdownBuffers()
        {





            someovrdir = null;

            InstanceCount = 0;


            if (Layout != null)
            {
                Layout.Dispose();
                Layout = null;
            }

            if (samplerState != null)
            {
                samplerState.Dispose();
                samplerState = null;
            }






            lightBuffer = null;
            originalArrayOfVerts = null;
            originalArrayOfIndices = null;
            originalMap = null;
            arrayOfMatrixBuff = null;
            instancesLocationW = null;
            instancesLocationH = null;
            instancesLocationD = null;




            if (HullShader != null)
            {
                HullShader.Dispose();
                HullShader = null;
            }
            if (DomainShader != null)
            {
                DomainShader.Dispose();
                DomainShader = null;
            }
            if (VertexShader != null)
            {
                VertexShader.Dispose();
                VertexShader = null;
            }

            if (PixelShader != null)
            {
                PixelShader.Dispose();
                PixelShader = null;
            }

            if (GeometryShader != null)
            {
                GeometryShader.Dispose();
                GeometryShader = null;
            }






            /*public Matrix currentWorldMatrix;
            public Matrix worldmatofobj;
            public Vector3 somechunkpriminstancepos;*/

            chunkindex = 0;

            usegeometryshader = 0;

            numberOfObjectInWidth = 0;
            numberOfObjectInHeight = 0;
            numberOfObjectInDepth = 0;
            numberOfInstancesPerObjectInWidth = 0;
            numberOfInstancesPerObjectInHeight = 0;
            numberOfInstancesPerObjectInDepth = 0;
            planeSize = 0;

            tinyChunkWidth = 0;
            tinyChunkHeight = 0;
            tinyChunkDepth = 0;
            /*string vsFileNameByteArray = "";
            string psFileNameByteArray = "";
            string gsFileName = "";*/

            typeofbytemapobject = 0;
            fullface = 0;

            voxeltype = 0;

            // Return the index buffer.
            //IndexBuffer?.Dispose();
            //IndexBuffer = null;
            // Release the vertex buffer.
            //VertexBuffer?.Dispose();
            //VertexBuffer = null;
            //heightmapmatrix = null;
            instancesmatrix = null;
            instancesmatrixb = null;
            instancesmatrixc = null;
            instancesmatrixd = null;

            //arrayOfSomeMap = null;
            instancesDataFORWARD = null;
            instancesDataRIGHT = null;
            instancesDataUP = null;

            //arrayofzeromeshinstances = null;
            instances = null;
            //somechunk = null;
            //instancesIndex = null;
            instancesLocationW = null;
            instancesLocationH = null;



           
            if (contantBuffer!= null)
            {
                contantBuffer.Dispose();
                contantBuffer = null;
            }


            if (ConstantLightBuffer!=null)
            {
                ConstantLightBuffer.Dispose();
                ConstantLightBuffer = null;
            }



      

            instancesbytemaps = null;

            instancesccsbytemapxyz = null;
            heightmapmatrixbuff = null;
            instances = null;
            instancesmatrix = null;
            instancesmatrixb = null;
            instancesmatrixc = null;
            instancesmatrixd = null;
            instancesDataFORWARD = null;
            instancesDataRIGHT = null;
            instancesDataUP = null;
            instancesLocationW = null;
            instancesLocationH = null;
            lightBufferInstChunk = null;


            //VertexShader VertexShader;
            //PixelShader PixelShader;
            //GeometryShader GeometryShader;


            if (ConstantLightBuffer != null)
            {
                ConstantLightBuffer.Dispose();
                ConstantLightBuffer = null;
            }

            if (someoculusdirbuffer != null)
            {
                someoculusdirbuffer.Dispose();
                someoculusdirbuffer = null;
            }

            if (InstanceRotationBufferRIGHT != null)
            {
                InstanceRotationBufferRIGHT.Dispose();
                InstanceRotationBufferRIGHT = null;
            }

            if (InstanceRotationBufferUP != null)
            {
                InstanceRotationBufferUP.Dispose();
                InstanceRotationBufferUP = null;
            }

            if (InstanceRotationBufferFORWARD != null)
            {
                InstanceRotationBufferFORWARD.Dispose();
                InstanceRotationBufferFORWARD = null;
            }

            if (IndexBuffer != null)
            {
                IndexBuffer.Dispose();
                IndexBuffer = null;
            }

            if (instancesbytemapsbuffer != null)
            {
                instancesbytemapsbuffer.Dispose();
                instancesbytemapsbuffer = null;
            }
            if (IndicesBuffer != null)
            {
                IndicesBuffer.Dispose();
                IndicesBuffer = null;
            }
            if (VertexBuffer != null)
            {
                VertexBuffer.Dispose();
                VertexBuffer = null;
            }
            if (instancesmatrixbuffer != null)
            {
                instancesmatrixbuffer.Dispose();
                instancesmatrixbuffer = null;
            }

            if (instancesmatrixbufferb != null)
            {
                instancesmatrixbufferb.Dispose();
                instancesmatrixbufferb = null;
            }
            if (instancesmatrixbufferc != null)
            {
                instancesmatrixbufferc.Dispose();
                instancesmatrixbufferc = null;
            }
            if (instancesmatrixbufferd != null)
            {
                instancesmatrixbufferd.Dispose();
                instancesmatrixbufferd = null;
            }

            if (InstanceBuffer != null)
            {
                InstanceBuffer.Dispose();
                InstanceBuffer = null;
            }

            if (instanceBufferHeightmap != null)
            {
                instanceBufferHeightmap.Dispose();
                instanceBufferHeightmap = null;
            }


            if (InstanceBufferLocW != null)
            {
                InstanceBufferLocW.Dispose();
                InstanceBufferLocW = null;
            }


            if (instancesbytemapsmatrixbuffer != null)
            {
                instancesbytemapsmatrixbuffer.Dispose();
                instancesbytemapsmatrixbuffer = null;
            }


            if (InstanceBufferLocH != null)
            {
                InstanceBufferLocH.Dispose();
                InstanceBufferLocH = null;
            }


            



            if (shaderOfChunk != null)
            {
                shaderOfChunk.Dispose();
                shaderOfChunk = null;
            }

            if (sccstrigvertbuilder!= null)
            {
                sccstrigvertbuilder.Dispose();
                sccstrigvertbuilder = null;
            }


            for (int j = 0; j < arrayOfChunkData.Length; j++)
            {
                if (arrayOfChunkData[j] != null)
                {
                    if (arrayOfChunkData[j].constantLightBuffer != null)
                    {
                        arrayOfChunkData[j].constantLightBuffer.Dispose();
                        arrayOfChunkData[j].constantLightBuffer = null;
                    }

                    if (arrayOfChunkData[j].constantMatrixPosBuffer != null)
                    {
                        arrayOfChunkData[j].constantMatrixPosBuffer.Dispose();
                        arrayOfChunkData[j].constantMatrixPosBuffer = null;
                    }

                    if (arrayOfChunkData[j].ConstantTessellationBuffer != null)
                    {
                        arrayOfChunkData[j].ConstantTessellationBuffer.Dispose();
                        arrayOfChunkData[j].ConstantTessellationBuffer = null;
                    }

                    if (arrayOfChunkData[j].IndicesBuffer != null)
                    {
                        arrayOfChunkData[j].IndicesBuffer.Dispose();
                        arrayOfChunkData[j].IndicesBuffer = null;
                    }

                    if (arrayOfChunkData[j].instanceBuffer != null)
                    {
                        arrayOfChunkData[j].instanceBuffer.Dispose();
                        arrayOfChunkData[j].instanceBuffer = null;
                    }

                    if (arrayOfChunkData[j].instanceBufferHeightmap != null)
                    {
                        arrayOfChunkData[j].instanceBufferHeightmap.Dispose();
                        arrayOfChunkData[j].instanceBufferHeightmap = null;
                    }
                    if (arrayOfChunkData[j].InstanceBufferLocD != null)
                    {
                        arrayOfChunkData[j].InstanceBufferLocD.Dispose();
                        arrayOfChunkData[j].InstanceBufferLocD = null;
                    }
                    if (arrayOfChunkData[j].InstanceBufferLocH != null)
                    {
                        arrayOfChunkData[j].InstanceBufferLocH.Dispose();
                        arrayOfChunkData[j].InstanceBufferLocH = null;
                    }
                    if (arrayOfChunkData[j].InstanceBufferLocW != null)
                    {
                        arrayOfChunkData[j].InstanceBufferLocW.Dispose();
                        arrayOfChunkData[j].InstanceBufferLocW = null;
                    }

                    if (arrayOfChunkData[j].InstanceRotationBufferFORWARD != null)
                    {
                        arrayOfChunkData[j].InstanceRotationBufferFORWARD.Dispose();
                        arrayOfChunkData[j].InstanceRotationBufferFORWARD = null;
                    }
                    if (arrayOfChunkData[j].InstanceRotationBufferRIGHT != null)
                    {
                        arrayOfChunkData[j].InstanceRotationBufferRIGHT.Dispose();
                        arrayOfChunkData[j].InstanceRotationBufferRIGHT = null;
                    }
                    if (arrayOfChunkData[j].InstanceRotationBufferUP != null)
                    {
                        arrayOfChunkData[j].InstanceRotationBufferUP.Dispose();
                        arrayOfChunkData[j].InstanceRotationBufferUP = null;
                    }


                    if (arrayOfChunkData[j].mapBuffer != null)
                    {
                        arrayOfChunkData[j].mapBuffer.Dispose();
                        arrayOfChunkData[j].mapBuffer = null;
                    }

                    if (arrayOfChunkData[j].vertexBuffer != null)
                    {
                        arrayOfChunkData[j].vertexBuffer.Dispose();
                        arrayOfChunkData[j].vertexBuffer = null;
                    }

                    if (arrayOfChunkData[j].DomainShader != null)
                    {
                        arrayOfChunkData[j].DomainShader.Dispose();
                        arrayOfChunkData[j].DomainShader = null;
                    }

                    if (arrayOfChunkData[j].GeometryShader != null)
                    {
                        arrayOfChunkData[j].GeometryShader.Dispose();
                        arrayOfChunkData[j].GeometryShader = null;
                    }


                    if (arrayOfChunkData[j].HullShader != null)
                    {
                        arrayOfChunkData[j].HullShader.Dispose();
                        arrayOfChunkData[j].HullShader = null;
                    }


                    if (arrayOfChunkData[j].PixelShader != null)
                    {
                        arrayOfChunkData[j].PixelShader.Dispose();
                        arrayOfChunkData[j].PixelShader = null;
                    }


                    if (arrayOfChunkData[j].VertexShader != null)
                    {
                        arrayOfChunkData[j].VertexShader.Dispose();
                        arrayOfChunkData[j].VertexShader = null;
                    }



                    arrayOfChunkData[j].arrayOfDeVectorMapTemp = null;
                    arrayOfChunkData[j].arrayOfDeVectorMapTempTwo = null;
                    arrayOfChunkData[j].arrayOfSomeMap = null;
                    arrayOfChunkData[j].arrayOfVertex = null;
                    arrayOfChunkData[j].heightmapmatrix = null;
                    arrayOfChunkData[j].instancesbytemaps = null;
                    arrayOfChunkData[j].instancesccsbytemapxyz = null;
                    arrayOfChunkData[j].instancesIndex = null;
                    arrayOfChunkData[j].instancesLocationD = null;
                    arrayOfChunkData[j].instancesLocationH = null;
                    arrayOfChunkData[j].instancesLocationW = null;
                    arrayOfChunkData[j].instancesmatrix = null;
                    arrayOfChunkData[j].instancesmatrixb = null;
                    arrayOfChunkData[j].instancesmatrixc = null;
                    arrayOfChunkData[j].instancesmatrixd = null;




                    if (arrayOfChunkData[j].instancesmatrixbuffer != null)
                    {
                        arrayOfChunkData[j].instancesmatrixbuffer.Dispose();
                        arrayOfChunkData[j].instancesmatrixbuffer = null;
                    }


                    if (arrayOfChunkData[j].instancesmatrixbufferb != null)
                    {
                        arrayOfChunkData[j].instancesmatrixbufferb.Dispose();
                        arrayOfChunkData[j].instancesmatrixbufferb = null;
                    }

                    if (arrayOfChunkData[j].instancesmatrixbufferc != null)
                    {
                        arrayOfChunkData[j].instancesmatrixbufferc.Dispose();
                        arrayOfChunkData[j].instancesmatrixbufferc = null;
                    }

                    if (arrayOfChunkData[j].instancesmatrixbufferd != null)
                    {
                        arrayOfChunkData[j].instancesmatrixbufferd.Dispose();
                        arrayOfChunkData[j].instancesmatrixbufferd = null;
                    }


                    if (arrayOfChunkData[j].Layout != null)
                    {
                        arrayOfChunkData[j].Layout.Dispose();
                        arrayOfChunkData[j].Layout = null;
                    }

                    if (arrayOfChunkData[j].lightBuffer != null)
                    {

                        arrayOfChunkData[j].lightBuffer = null;
                    }

                    if (arrayOfChunkData[j].matrixBuffer != null)
                    {

                        arrayOfChunkData[j].matrixBuffer = null;
                    }


                    if (arrayOfChunkData[j].samplerState != null)
                    {
                        arrayOfChunkData[j].samplerState.Dispose();
                        arrayOfChunkData[j].samplerState = null;
                    }

                    if (arrayOfChunkData[j].someoculusdirbuffer != null)
                    {
                        arrayOfChunkData[j].someoculusdirbuffer.Dispose();
                        arrayOfChunkData[j].someoculusdirbuffer = null;
                    }

                    arrayOfChunkData[j].originalArrayOfIndices = null;

                    arrayOfChunkData[j].SC_instancedChunk_Instances = null;
                    arrayOfChunkData[j].SC_instancedChunk_InstancesFORWARD = null;
                    arrayOfChunkData[j].SC_instancedChunk_InstancesRIGHT = null;
                    arrayOfChunkData[j].SC_instancedChunk_InstancesUP = null;

                    arrayOfChunkData[j].someovrdir = null;
                    arrayOfChunkData[j].tessellationBuffer = null;
                    arrayOfChunkData[j].arrayOfVertex = null;

                    arrayOfChunkData[j] = null;

                }
            }

            for (int j = 0; j < arrayofindexzeromesh.Length; j++)
            {
                if (arrayofindexzeromesh[j] != null)
                {
                    arrayofindexzeromesh[j].ShutDown();
                    arrayofindexzeromesh[j] = null;
                }
            }


            if (somechunk!= null)
            {
                somechunk.Dispose();
                somechunk = null;
            }


            if (inputElements!= null)
            {
                inputElements = null;
            }


            /*
            SharpDX.Direct3D11.Buffer contantBuffer;
            //VertexShader VertexShader;
            //PixelShader PixelShader;
            //GeometryShader GeometryShader;
            SharpDX.Direct3D11.Buffer ConstantLightBuffer;

            SharpDX.Direct3D11.Buffer someoculusdirbuffer;

            SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT;
            SharpDX.Direct3D11.Buffer InstanceRotationBufferUP;
            SharpDX.Direct3D11.Buffer InstanceRotationBufferFORWARD;

            SharpDX.Direct3D11.Buffer IndexBuffer;
            SharpDX.Direct3D11.Buffer instancesbytemapsbuffer;

            SharpDX.Direct3D11.Buffer IndicesBuffer;
            SharpDX.Direct3D11.Buffer VertexBuffer;

            SharpDX.Direct3D11.Buffer instancesmatrixbufferd;
            SharpDX.Direct3D11.Buffer instancesmatrixbufferc;

            SharpDX.Direct3D11.Buffer InstanceBuffer;
            SharpDX.Direct3D11.Buffer instanceBufferHeightmap;
            SharpDX.Direct3D11.Buffer instancesmatrixbuffer;
            SharpDX.Direct3D11.Buffer instancesmatrixbufferb;

            SharpDX.Direct3D11.Buffer InstanceBufferLocW;

            SharpDX.Direct3D11.Buffer instancesbytemapsmatrixbuffer;
            SharpDX.Direct3D11.Buffer InstanceBufferLocH;

        public OVRDir[] someovrdir;*/
        }




        [StructLayout(LayoutKind.Explicit)]
        public struct OVRDir
        {
            [FieldOffset(0)]
            public Vector4 ovrdirf;
            [FieldOffset(16)]
            public Vector4 ovrdirr;
            [FieldOffset(32)]
            public Vector4 ovrdiru;
            [FieldOffset(48)]
            public Vector4 ovrpos;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DMatrixBuffer
        {
            [FieldOffset(0)]
            public Matrix world;
            [FieldOffset(64)]
            public Matrix view;
            [FieldOffset(128)]
            public Matrix proj;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBuffer
        {
            [FieldOffset(0)]
            public Vector4 ambientColor;
            [FieldOffset(16)]
            public Vector4 diffuseColor;
            [FieldOffset(32)]
            public Vector3 lightDirection;
            [FieldOffset(44)]
            public float padding; // Added extra padding so structure is a multiple of 16.

        }
        public class chunkData
        {

            public int pixelmovedbufferregiona = 0;
            public int pixelmovedbufferregionb = 0;
            public int pixelmovedbufferregionc = 0;
            public int pixelmovedbufferregiond = 0;

            public SC_instancedChunk.DInstanceMatrix[] lastinstancesmatrixa;
            public SC_instancedChunk.DInstanceMatrix[] lastinstancesmatrixb;
            public SC_instancedChunk.DInstanceMatrix[] lastinstancesmatrixc;
            public SC_instancedChunk.DInstanceMatrix[] lastinstancesmatrixd;

            public VertexShader VertexShader;//= new VertexShader(D3D.device, vertexShaderByteCode);
            public PixelShader PixelShader;// = new PixelShader(D3D.device, pixelShaderByteCode);
            public GeometryShader GeometryShader;// = new GeometryShader(D3D.device, geometryShaderByteCode);
            public HullShader HullShader;
            public DomainShader DomainShader;

            public SharpDX.Direct3D11.Buffer ConstantTessellationBuffer;

            public int[] originalArrayOfIndices;

            public InputLayout Layout;

            public DTessellationBufferType[] tessellationBuffer;

            public SC_instancedChunk.DInstanceMatrix[] instancesmatrix;
            public SharpDX.Direct3D11.Buffer instancesmatrixbuffer;

            public SC_instancedChunk.DInstanceMatrix[] instancesmatrixb;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferb;

            public SC_instancedChunk.DInstanceMatrix[] instancesmatrixc;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferc;

            public SC_instancedChunk.DInstanceMatrix[] instancesmatrixd;
            public SharpDX.Direct3D11.Buffer instancesmatrixbufferd;


            public SharpDX.Direct3D11.Buffer someoculusdirbuffer;

            public OVRDir[] someovrdir;
            public SC_instancedChunk.DInstancesccsbytemapxyz[] instancesccsbytemapxyz;
            public SharpDX.Direct3D11.Buffer instancesbytemapsmatrixbuffer;

            public SC_instancedChunk.heightmapinstance[] heightmapmatrix;
            //public SC_instancedChunk.DInstanceType[] indexArray;
            public SC_instancedChunk.DInstancesByteMap[] instancesbytemaps;
            public SC_instancedChunk.DInstanceType[] SC_instancedChunk_Instances;
            public SC_instancedChunk.DInstanceShipData[] SC_instancedChunk_InstancesFORWARD;
            public SC_instancedChunk.DInstanceShipData[] SC_instancedChunk_InstancesRIGHT;
            public SC_instancedChunk.DInstanceShipData[] SC_instancedChunk_InstancesUP;

            public Matrix worldMatrix;
            public Matrix viewMatrix;
            public Matrix projectionMatrix;
            // public SC_instancedChunk_shader_final chunkShader;
            public DMatrixBuffer[] matrixBuffer;

            public DLightBufferEr[] lightBuffer;
            public int switchForRender;
            public SC_instancedChunk.DInstanceType[] instancesIndex;
            public SC_instancedChunk.DInstanceType[] arrayOfDeVectorMapTemp;
            public SC_instancedChunk.DInstanceShipData[] arrayOfDeVectorMapTempTwo;

            public SharpDX.Direct3D11.Buffer instanceBufferHeightmap;
            public SharpDX.Direct3D11.Buffer instanceBuffer;
            public SharpDX.Direct3D11.Buffer constantLightBuffer;
            public SharpDX.Direct3D11.Buffer vertexBuffer;
            public SharpDX.Direct3D11.Buffer IndicesBuffer;

            public SharpDX.Direct3D11.Buffer constantMatrixPosBuffer;
            public int[][] arrayOfSomeMap;
            public SharpDX.Direct3D11.Buffer mapBuffer;

            public SC_instancedChunk.DInstanceTypeLocW[] instancesLocationW;
            public SC_instancedChunk.DInstanceTypeLocH[] instancesLocationH;
            public SC_instancedChunk.DInstanceTypeLocD[] instancesLocationD;

            public SharpDX.Direct3D11.Buffer InstanceBufferLocW;
            public SharpDX.Direct3D11.Buffer InstanceBufferLocH;
            public SharpDX.Direct3D11.Buffer InstanceBufferLocD;
            public SharpDX.Direct3D11.Buffer instancesbytemapsbuffer;

            public SharpDX.Direct3D11.Buffer InstanceRotationBufferFORWARD;
            public SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT;
            public SharpDX.Direct3D11.Buffer InstanceRotationBufferUP;

            public SC_instancedChunk.DVertex[] arrayOfVertex;

            public int copytobuffer;

            public int numberOfObjectInWidth;
            public int numberOfObjectInHeight;
            public int numberOfObjectInDepth;
            public int numberOfInstancesPerObjectInWidth;
            public int numberOfInstancesPerObjectInHeight;
            public int numberOfInstancesPerObjectInDepth;
            public float planeSize;

            public SC_instancedChunk_shader_final shaderOfChunk;

            public SamplerState samplerState;

            public int candrawswtc;
        }



        [StructLayout(LayoutKind.Explicit)]
        public struct DLightBufferEr
        {

            [FieldOffset(0)]
            public Vector4 ambientColor;
            [FieldOffset(16)]
            public Vector4 diffuseColor;
            [FieldOffset(32)]
            public Vector4 specularcolor;
            [FieldOffset(48)]
            public Vector4 lightDirection;
            [FieldOffset(64)]
            public Vector4 lightPosition;
            [FieldOffset(80)]
            public Vector4 lightextras;
            [FieldOffset(96)]
            public Vector4 gridcolor;
            [FieldOffset(112)]
            public Vector4 cursorcolor;

            /*
            [FieldOffset(0)]
            public Vector4 ambientColor;
            [FieldOffset(16)]
            public Vector4 diffuseColor;
            [FieldOffset(32)]
            public Vector4 specularcolor;
            [FieldOffset(48)]
            public Vector4 lightDirection;
            [FieldOffset(64)]
            public Vector4 lightPosition;
            [FieldOffset(80)]
            public Vector4 lightextras;*/
            /*
            [FieldOffset(60)]
            public float padding0;
            [FieldOffset(64)]
            public Vector3 lightPosition;
            [FieldOffset(76)]
            public float padding1;*/
        }






        chunkscreen somechunk;

        SharpDX.Direct3D11.Buffer contantBuffer;
        //VertexShader VertexShader;
        //PixelShader PixelShader;
        //GeometryShader GeometryShader;
        SharpDX.Direct3D11.Buffer ConstantLightBuffer;

        SharpDX.Direct3D11.Buffer someoculusdirbuffer;

        SharpDX.Direct3D11.Buffer InstanceRotationBufferRIGHT;
        SharpDX.Direct3D11.Buffer InstanceRotationBufferUP;
        SharpDX.Direct3D11.Buffer InstanceRotationBufferFORWARD;

        SharpDX.Direct3D11.Buffer IndexBuffer;
        SharpDX.Direct3D11.Buffer instancesbytemapsbuffer;

        SharpDX.Direct3D11.Buffer IndicesBuffer;
        SharpDX.Direct3D11.Buffer VertexBuffer;

        SharpDX.Direct3D11.Buffer instancesmatrixbufferd;
        SharpDX.Direct3D11.Buffer instancesmatrixbufferc;

        SharpDX.Direct3D11.Buffer InstanceBuffer;
        SharpDX.Direct3D11.Buffer instanceBufferHeightmap;
        SharpDX.Direct3D11.Buffer instancesmatrixbuffer;
        SharpDX.Direct3D11.Buffer instancesmatrixbufferb;

        SharpDX.Direct3D11.Buffer InstanceBufferLocW;

        SharpDX.Direct3D11.Buffer instancesbytemapsmatrixbuffer;
        SharpDX.Direct3D11.Buffer InstanceBufferLocH;

        public OVRDir[] someovrdir;
        //public SC_instancedChunk.DVertex[] indexArray;

        //CREATING MY VR DESKTOP INSTANCED SCREEN WITH MY ADDED BYTE TO SHADER CHUNK FROM MY PROGRAM TEST CALLED 1ST VERSION. 
        //CREATING MY VR DESKTOP INSTANCED SCREEN WITH MY ADDED BYTE TO SHADER CHUNK FROM MY PROGRAM TEST CALLED 1ST VERSION. 
        //CREATING MY VR DESKTOP INSTANCED SCREEN WITH MY ADDED BYTE TO SHADER CHUNK FROM MY PROGRAM TEST CALLED 1ST VERSION. 

        public chunkData[] arrayOfChunkData;
        public SC_instancedChunk[] arrayofindexzeromesh;

        int InstanceCount = 0;

        InputLayout Layout;
        VertexShader VertexShader;
        PixelShader PixelShader;

        public SamplerState samplerState;
        /*[StructLayout(LayoutKind.Explicit, Size = 48)] //, Pack = 48
        public struct DInstanceType
        {
            [FieldOffset(0)]
            public int one;
            [FieldOffset(4)]
            public int two;
            [FieldOffset(8)]
            public int three;
            [FieldOffset(12)]
            public int four;
            [FieldOffset(16)]
            public int oneTwo;
            [FieldOffset(20)]
            public int twoTwo;
            [FieldOffset(24)]
            public int threeTwo;
            [FieldOffset(28)]
            public int fourTwo;
            [FieldOffset(32)]
            public Vector4 instancePos;
        };*/

        /*[StructLayout(LayoutKind.Sequential, Pack = 16)]
        public struct DInstanceTypeTwo
        {
            public Matrix matrix;
        };*/

        DLightBuffer[] lightBuffer = new DLightBuffer[1];
        public SC_instancedChunk.DVertex[] originalArrayOfVerts;
        public int[] originalArrayOfIndices;
        int[] originalMap;

        public SC_instancedChunk_shader_final shaderOfChunk;

        sccstrigvertbuilderscreen sccstrigvertbuilder;
        sccstrigvertbuilderscreenreduced sccstrigvertbuilderreduced;
        sccstrigvertbuilderscreenmorescreens sccstrigvertbuildermorescreen;



        public DMatrixBuffer[] arrayOfMatrixBuff = new DMatrixBuffer[1];
        public SC_instancedChunk.DInstanceTypeLocW[] instancesLocationW { get; set; }
        public SC_instancedChunk.DInstanceTypeLocH[] instancesLocationH { get; set; }
        public SC_instancedChunk.DInstanceTypeLocD[] instancesLocationD { get; set; }

        GeometryShader GeometryShader;






        public DLightBufferEr[] lightBufferInstChunk = new DLightBufferEr[1];

        public SC_instancedChunk.DInstancesccsbytemapxyz[] instancesccsbytemapxyz;
        public SC_instancedChunk.DInstancesByteMap[] instancesbytemaps;

        public SC_instancedChunk.DInstanceType[] instances;
        public SC_instancedChunk.DInstanceShipData[] instancesDataFORWARD;
        public SC_instancedChunk.DInstanceShipData[] instancesDataRIGHT;
        public SC_instancedChunk.DInstanceShipData[] instancesDataUP;

        public SC_instancedChunk.DInstanceMatrix[] instancesmatrix;
        public SC_instancedChunk.DInstanceMatrix[] instancesmatrixb;
        public SC_instancedChunk.DInstanceMatrix[] instancesmatrixc;
        public SC_instancedChunk.DInstanceMatrix[] instancesmatrixd;

        public SC_instancedChunk.DInstanceMatrix[] lastinstancesmatrixa;
        public SC_instancedChunk.DInstanceMatrix[] lastinstancesmatrixb;
        public SC_instancedChunk.DInstanceMatrix[] lastinstancesmatrixc;
        public SC_instancedChunk.DInstanceMatrix[] lastinstancesmatrixd;
        




        public HullShader HullShader { get; set; }
        public DomainShader DomainShader { get; set; }

        //SC_AI_Start[][] perceptronRotationRL;
        //SC_AI_Start[][] perceptronRotationTB;
        //SC_AI_Start[][] perceptronRotationFB;

        //SC_AI.data_input dataForPerceptronRL;
        //SC_AI.data_input dataForPerceptronTB;
        //SC_AI.data_input dataForPerceptronFB;

       
        SC_instancedChunk.heightmapinstance[] heightmapmatrixbuff;

        public Matrix currentWorldMatrix;
        public Matrix worldmatofobj;
        public Vector3 somechunkpriminstancepos;
        public int chunkindex;

        int usegeometryshader;

        int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

        int tinyChunkWidth; int tinyChunkHeight; int tinyChunkDepth;
        string vsFileNameByteArray = "";
        string psFileNameByteArray = "";
        string gsFileName = "";

        int typeofbytemapobject;
        int fullface;

        public int voxeltype;


        [StructLayout(LayoutKind.Sequential)]
        public struct DInstanceData
        {
            public Vector4 rotation;
        }



        public int hasalreadycreated5faces = 0;


        SharpDX.Direct3D11.Buffer IndicesBuffer5faces;
        SharpDX.Direct3D11.Buffer VertexBuffer5faces;
        List<SC_instancedChunk.DVertex> somevertlist;
        List<int> sometriglist;


        List<SC_instancedChunk.DVertex> somevertlistoriginal;
        List<int> sometriglistoriginal;


        public void createdifferentvirtualdesktop(sccs.scgraphics.scdirectx D3D, int fullface_, int chunkprimindex)
        {



            /*
            sccstrigvertbuilder = new sccstrigvertbuilderscreen();
            //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
            sccstrigvertbuilder.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface_, voxeltype); //,surfaceWidth,surfaceHeight

            /*
            if (voxeltype == 0 || voxeltype == 1 || voxeltype == 2 || voxeltype == 3)
            {
                sccstrigvertbuilder = new sccstrigvertbuilderscreen();
                //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                sccstrigvertbuilder.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface_, voxeltype); //,surfaceWidth,surfaceHeight

            }

            VertexBuffer5faces = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, originalArrayOfVerts);
            IndicesBuffer5faces = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, originalArrayOfIndices);

            somevertlist = sccstrigvertbuilder.vertexlist;
            sometriglist = sccstrigvertbuilder.listOfTriangleIndices;

            arrayOfChunkData[chunkprimindex].IndicesBuffer = IndicesBuffer5faces;
            arrayOfChunkData[chunkprimindex].vertexBuffer = VertexBuffer5faces;
            arrayOfChunkData[chunkprimindex].arrayOfVertex = somevertlist.ToArray();//.ToArray();
            arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sometriglist.ToArray();//.ToArray();
            */


            

            if (hasalreadycreated5faces == 0 && fullface_ == 1)
            {
                //VertexBuffer?.Dispose();
                //VertexBuffer = null;

                //IndicesBuffer?.Dispose();
                //IndicesBuffer = null;
                

                if (voxeltype == 0 || voxeltype == 1 || voxeltype == 2 || voxeltype == 3)
                {
                    sccstrigvertbuilder = new sccstrigvertbuilderscreen();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuilder.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface_, voxeltype); //,surfaceWidth,surfaceHeight
                    
                }

                VertexBuffer5faces = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, originalArrayOfVerts);
                IndicesBuffer5faces = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, originalArrayOfIndices);

                somevertlist = sccstrigvertbuilder.vertexlist;
                sometriglist = sccstrigvertbuilder.listOfTriangleIndices;

                arrayOfChunkData[chunkprimindex].IndicesBuffer = IndicesBuffer5faces;
                arrayOfChunkData[chunkprimindex].vertexBuffer = VertexBuffer5faces;
                arrayOfChunkData[chunkprimindex].arrayOfVertex = somevertlist.ToArray();//.ToArray();
                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sometriglist.ToArray();//.ToArray();

                hasalreadycreated5faces = 1;

            }
            else if (hasalreadycreated5faces == 1 && fullface_ == 1)
            {

                arrayOfChunkData[chunkprimindex].IndicesBuffer = IndicesBuffer5faces;
                arrayOfChunkData[chunkprimindex].vertexBuffer = VertexBuffer5faces;
                arrayOfChunkData[chunkprimindex].arrayOfVertex = somevertlist.ToArray();//.ToArray();
                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sometriglist.ToArray();//.ToArray();
            }
            else if (fullface_ == 0)
            {

                arrayOfChunkData[chunkprimindex].IndicesBuffer = IndicesBuffer;
                arrayOfChunkData[chunkprimindex].vertexBuffer = VertexBuffer;
                arrayOfChunkData[chunkprimindex].arrayOfVertex = somevertlistoriginal.ToArray();//.ToArray();
                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sometriglistoriginal.ToArray();//.ToArray();
            }
            


        }
        public void createChunk(sccs.scgraphics.scdirectx D3D, float surfaceWidth, float surfaceHeight, Vector3 lightpos, Vector3 dirLight, Vector3 somechunkpriminstancepos_, int chunkindex_, int _addtoworld, float _mass, World _the_world, bool _is_static, sccs.scgraphics.scdirectx.BodyTag _tag, int numberofmainobjectx, int numberofmainobjecty, int numberofmainobjectz, int numberOfObjectInWidth_, int numberOfObjectInHeight_, int numberOfObjectInDepth_, int numberOfInstancesPerObjectInWidth_, int numberOfInstancesPerObjectInHeight_, int numberOfInstancesPerObjectInDepth_, int tinyChunkWidth_, int tinyChunkHeight_, int tinyChunkDepth_, float planeSize_, int shaderswtch, float offsetinstance, int fullface_, int usegeometryshader_, int somechunkkeyboardpriminstanceindex_, int voxeltype_, Matrix worldmatofobj_, int typeofbytemapobject_, int mainix, int mainiy, int mainiz)
        {
            try
            {
                typeofbytemapobject = typeofbytemapobject_;
                worldmatofobj = worldmatofobj_;

                /* Quaternion somedirquat;
                Quaternion.RotationMatrix(ref worldmatofobj, out somedirquat);

                var dirDroneInstanceRight = -sc_maths._newgetdirleft(somedirquat);
                var dirDroneInstanceUp = sc_maths._newgetdirup(somedirquat);
                var dirDroneInstanceForward = sc_maths._newgetdirforward(somedirquat);

                //position = position - (new Vector4(dirDroneInstanceRight.X, dirDroneInstanceRight.Y, dirDroneInstanceRight.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                //position = position - (new Vector4(dirDroneInstanceUp.X, dirDroneInstanceUp.Y, dirDroneInstanceUp.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));
                //position = position + (new Vector4(dirDroneInstanceForward.X, dirDroneInstanceForward.Y, dirDroneInstanceForward.Z, 1.0f) * (tinyChunkWidth * planeSize * 0.5f));

                Vector4 somevecx = new Vector4(dirDroneInstanceRight.X, dirDroneInstanceRight.Y, dirDroneInstanceRight.Z, 1.0f);
                Vector4 somevecy = new Vector4(dirDroneInstanceUp.X, dirDroneInstanceUp.Y, dirDroneInstanceUp.Z, 1.0f);
                Vector4 somevecz = new Vector4(dirDroneInstanceForward.X, dirDroneInstanceForward.Y, dirDroneInstanceForward.Z, 1.0f);

                Vector4 somemainpos = new Vector4(worldmatofobj.M41, worldmatofobj.M42, worldmatofobj.M43, 1.0f);
                Vector4 someinstancepostest = somemainpos;// Vector4.Zero;

                someinstancepostest = someinstancepostest + (somevecx * (x * tinyChunkWidth * planeSize));
                someinstancepostest = someinstancepostest + (somevecy * (y * tinyChunkWidth * planeSize));
                someinstancepostest = someinstancepostest + (somevecz * (z * tinyChunkWidth * planeSize));*/

                someovrdir = new OVRDir[1];
                someovrdir[0].ovrdirr = new Vector4(0, 0, 0, 1);
                someovrdir[0].ovrdirf = new Vector4(0, 0, 0, 1);
                someovrdir[0].ovrdiru = new Vector4(0, 0, 0, 1);
                someovrdir[0].ovrpos = new Vector4(0, 0, 0, 1);

                voxeltype = voxeltype_;

                usegeometryshader = usegeometryshader_;

                fullface = fullface_;

                tinyChunkWidth = tinyChunkWidth_;
                tinyChunkHeight = tinyChunkHeight_;
                tinyChunkDepth = tinyChunkDepth_;

                numberOfObjectInWidth = numberOfObjectInWidth_;
                numberOfObjectInHeight = numberOfObjectInHeight_;
                numberOfObjectInDepth = numberOfObjectInDepth_;
                numberOfInstancesPerObjectInWidth = numberOfInstancesPerObjectInWidth_;
                numberOfInstancesPerObjectInHeight = numberOfInstancesPerObjectInHeight_;
                numberOfInstancesPerObjectInDepth = numberOfInstancesPerObjectInDepth_;
                planeSize = planeSize_;

                chunkindex = chunkindex_;

                somechunkpriminstancepos = somechunkpriminstancepos_;

                //arrayofindexzeromesh = new SC_instancedChunk[numberOfObjectInWidth_ * numberOfObjectInHeight_ * numberOfObjectInDepth_];
                //arrayOfChunkData = new chunkData[numberOfObjectInWidth_ * numberOfObjectInHeight_ * numberOfObjectInDepth_];
                arrayofindexzeromesh = new SC_instancedChunk[numberOfObjectInWidth_ * numberOfObjectInHeight_ * numberOfObjectInDepth_];
                arrayOfChunkData = new chunkData[numberOfObjectInWidth_ * numberOfObjectInHeight_ * numberOfObjectInDepth_];

                //SC_Globals.numberOfInstancesPerObjectInWidth_* numberOfInstancesPerObjectInHeight_* numberOfInstancesPerObjectInDepth_
                if (voxeltype_ == 0 || voxeltype_ == 1 || voxeltype_ == 2 || voxeltype_ == 3)
                {
                    sccstrigvertbuilder = new sccstrigvertbuilderscreen();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuilder.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype); //,surfaceWidth,surfaceHeight

                    somevertlistoriginal = sccstrigvertbuilder.vertexlist;
                    sometriglistoriginal = sccstrigvertbuilder.listOfTriangleIndices;
                }
                /*else if (voxeltype_ == 2)
                {
                    sccstrigvertbuilderreduced = new sccstrigvertbuilderscreenreduced();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuilderreduced.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype); //,surfaceWidth,surfaceHeight
                }*/
                /*else if (voxeltype_ == 3)
                {
                    sccstrigvertbuildermorescreen = new sccstrigvertbuilderscreenmorescreens();
                    //chunkTerrain.startBuildingArray(new Vector4(0, 0, 0, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, this);
                    sccstrigvertbuildermorescreen.startBuildingArray(new Vector4(somechunkpriminstancepos.X, somechunkpriminstancepos.Y, somechunkpriminstancepos.Z, 1), out originalArrayOfVerts, out originalArrayOfIndices, out originalMap, numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, this, null, null, fullface, voxeltype, mainix, mainiy, mainiz); //,surfaceWidth,surfaceHeight
                }*/





                InstanceCount = numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_;
                instancesbytemaps = new SC_instancedChunk.DInstancesByteMap[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                instancesccsbytemapxyz = new SC_instancedChunk.DInstancesccsbytemapxyz[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                instances = new SC_instancedChunk.DInstanceType[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                heightmapmatrixbuff = new SC_instancedChunk.heightmapinstance[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                instancesmatrix = new SC_instancedChunk.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                instancesmatrixb = new SC_instancedChunk.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                instancesmatrixc = new SC_instancedChunk.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                instancesmatrixd = new SC_instancedChunk.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];


                lastinstancesmatrixa = new SC_instancedChunk.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                lastinstancesmatrixb = new SC_instancedChunk.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                lastinstancesmatrixc = new SC_instancedChunk.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                lastinstancesmatrixd = new SC_instancedChunk.DInstanceMatrix[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];






                instancesDataFORWARD = new SC_instancedChunk.DInstanceShipData[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                instancesDataRIGHT = new SC_instancedChunk.DInstanceShipData[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                instancesDataUP = new SC_instancedChunk.DInstanceShipData[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                instancesLocationW = new SC_instancedChunk.DInstanceTypeLocW[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                instancesLocationH = new SC_instancedChunk.DInstanceTypeLocH[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];
                //instancesLocationD = new DInstanceTypeLocD[numberOfInstancesPerObjectInWidth_ * numberOfInstancesPerObjectInHeight_ * numberOfInstancesPerObjectInDepth_];

                //var vsFileNameByteArray = sccs.Properties.Resources.textureTrigVS; //for cubes use shader textureTrigChunkVSMOD //textureTrigChunkVS
                //var psFileNameByteArray = sccs.Properties.Resources.textureTrigPS; //textureTrigChunkPS
                
                if (shaderswtch == 0 && Program._useOculusRift == 1)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkVS;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkPS;
                    //gsFileName = sccs.Properties.Resources.HLSLchunkkeyboard;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 1 && Program._useOculusRift == 1)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.textureTrigChunkVS;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.textureTrigChunkPS;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 2 && Program._useOculusRift == 1)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapVS;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapPS;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 3 && Program._useOculusRift == 1)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapVSeight;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapPSeight;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 0 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkVSdx;//textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkPSdx;
                    //gsFileName = sccs.Properties.Resources.HLSLchunkkeyboard;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 1 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.textureTrigChunkVSdx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.textureTrigChunkPSdx;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 2 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapVSdx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapPSdx;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 3 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapVSeightdx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapPSeightdx;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
                else if (shaderswtch == 4 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapVSsixteendx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapPSsixteendx;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }

                else if (shaderswtch == 5 && Program._useOculusRift == 1)
                {
                    
                    vsFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapVSdx; //textureTrigChunkVSMOD
                    psFileNameByteArray = sccsvvdhd.Properties.Resources.sccsvertbindchunkheightmapPSdx;
                    gsFileName = sccsvvdhd.Properties.Resources.HLSL;
                }
               





                var hsFileName = sccsvvdhd.Properties.Resources.colorhull; //textureTrigChunkVSMOD
                var dsFileName = sccsvvdhd.Properties.Resources.colordom;



                // Compile the Hull shader code.
                ShaderBytecode hullShaderByteCode = ShaderBytecode.Compile(hsFileName, "ColorHullShader", "hs_5_0", ShaderFlags.None, EffectFlags.None);
                // Compile the Domain shader code.
                ShaderBytecode domainShaderByteCode = ShaderBytecode.Compile(dsFileName, "ColorDomainShader", "ds_5_0", ShaderFlags.None, EffectFlags.None);
            


                //var vsFileNameByteArray = sccs.Properties.Resources.texture1; //for cubes use shader textureTrigChunkVSMOD //textureTrigChunkVS
                //var psFileNameByteArray = sccs.Properties.Resources.texture; //textureTrigChunkPS


                ShaderBytecode vertexShaderByteCode = ShaderBytecode.Compile(vsFileNameByteArray, "TextureVertexShader", "vs_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                ShaderBytecode pixelShaderByteCode = ShaderBytecode.Compile(psFileNameByteArray, "TexturePixelShader", "ps_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);
                ShaderBytecode geometryShaderByteCode = ShaderBytecode.Compile(gsFileName, "GS", "gs_5_0", ShaderFlags.None, SharpDX.D3DCompiler.EffectFlags.None);

                VertexShader = new VertexShader(D3D.device, vertexShaderByteCode);
                PixelShader = new PixelShader(D3D.device, pixelShaderByteCode);
                GeometryShader = new GeometryShader(D3D.device, geometryShaderByteCode);
                // Create the vertex shader from the buffer.
                HullShader = new HullShader(D3D.device, hullShaderByteCode);
                // Create the vertex shader from the buffer.
                DomainShader = new DomainShader(D3D.device, domainShaderByteCode);

                

                /*
                if (shaderswtch == 0 && Program._useOculusRift == 1)
                {
                    vsFileNameByteArray = "sccsvertbindchunkVS";//textureTrigChunkVSMOD
                    psFileNameByteArray = "sccsvertbindchunkPS";
                    //gsFileName = sccs.Properties.Resources.HLSLchunkkeyboard;
                    gsFileName = "HLSL";
                }
                else if (shaderswtch == 1 && Program._useOculusRift == 1)
                {
                    vsFileNameByteArray = "textureTrigChunkVS";//textureTrigChunkVSMOD
                    psFileNameByteArray = "textureTrigChunkPS";
                    gsFileName = "HLSL";
                }
                else if (shaderswtch == 2 && Program._useOculusRift == 1)
                {
                    vsFileNameByteArray = "sccsvertbindchunkheightmapVS";//textureTrigChunkVSMOD
                    psFileNameByteArray = "sccsvertbindchunkheightmapPS";
                    gsFileName = "HLSL";
                }
                else if (shaderswtch == 3 && Program._useOculusRift == 1)
                {
                    vsFileNameByteArray = "sccsvertbindchunkheightmapVSeight";//textureTrigChunkVSMOD
                    psFileNameByteArray = "sccsvertbindchunkheightmapPSeight";
                    gsFileName = "HLSL";
                }
                else if (shaderswtch == 0 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = "sccsvertbindchunkVSdx";//textureTrigChunkVSMOD
                    psFileNameByteArray = "sccsvertbindchunkPSdx";
                    //gsFileName = sccs.Properties.Resources.HLSLchunkkeyboard;
                    gsFileName = "HLSL";
                }
                else if (shaderswtch == 1 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = "textureTrigChunkVSdx"; //textureTrigChunkVSMOD
                    psFileNameByteArray = "textureTrigChunkPSdx";
                    gsFileName = "HLSL";
                }
                else if (shaderswtch == 2 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = "sccsvertbindchunkheightmapVSdx"; //textureTrigChunkVSMOD
                    psFileNameByteArray = "sccsvertbindchunkheightmapPSdx";
                    gsFileName = "HLSL";
                }
                else if (shaderswtch == 3 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = "sccsvertbindchunkheightmapVSeightdx"; //textureTrigChunkVSMOD
                    psFileNameByteArray = "sccsvertbindchunkheightmapPSeightdx";
                    gsFileName = "HLSL";
                }
                else if (shaderswtch == 4 && Program._useOculusRift == 0)
                {
                    vsFileNameByteArray = "sccsvertbindchunkheightmapVSsixteendx"; //textureTrigChunkVSMOD
                    psFileNameByteArray = "sccsvertbindchunkheightmapPSsixteendx";
                    gsFileName = "HLSL";
                }

                else if (shaderswtch == 5 && Program._useOculusRift == 0)
                {

                    vsFileNameByteArray = "sccsvertbindchunkheightmapVSeightbreakdx"; //textureTrigChunkVSMOD
                    psFileNameByteArray = "sccsvertbindchunkheightmapPSeightbreakdx";
                    gsFileName = "HLSL";
                }
                
                var hsFileName = "colorhull"; //textureTrigChunkVSMOD
                var dsFileName = "colordom";

                












                /*
                ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile("RandomShader.hlsl", "TextureVertexShader", "vs_5_0");
                ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile("RandomShader.hlsl", "TexturePixelShader", "ps_5_0");
                


















                

                // Compile the Hull shader code.
                ShaderBytecode hullShaderByteCode = ShaderBytecode.CompileFromFile(hsFileName + ".hs", "ColorHullShader", "hs_5_0", ShaderFlags.None, EffectFlags.None);
                // Compile the Domain shader code.
                ShaderBytecode domainShaderByteCode = ShaderBytecode.CompileFromFile(dsFileName + ".ds", "ColorDomainShader", "ds_5_0", ShaderFlags.None, EffectFlags.None);
                


                //var vsFileNameByteArray = sccs.Properties.Resources.texture1; //for cubes use shader textureTrigChunkVSMOD //textureTrigChunkVS
                //var psFileNameByteArray = sccs.Properties.Resources.texture; //textureTrigChunkPS


                ShaderBytecode vertexShaderByteCode = ShaderBytecode.CompileFromFile(vsFileNameByteArray + ".vs", "TextureVertexShader", "vs_5_0", ShaderFlags.None, EffectFlags.None);
                ShaderBytecode pixelShaderByteCode = ShaderBytecode.CompileFromFile(psFileNameByteArray + ".ps", "TexturePixelShader", "ps_5_0", ShaderFlags.None, EffectFlags.None);
                ShaderBytecode geometryShaderByteCode = ShaderBytecode.CompileFromFile(gsFileName + ".gs", "GS", "gs_5_0", ShaderFlags.None, EffectFlags.None);

                VertexShader = new VertexShader(D3D.Device, vertexShaderByteCode);
                PixelShader = new PixelShader(D3D.Device, pixelShaderByteCode);
                GeometryShader = new GeometryShader(D3D.Device, geometryShaderByteCode);
                // Create the vertex shader from the buffer.
                HullShader = new HullShader(D3D.Device, hullShaderByteCode);
                // Create the vertex shader from the buffer.
                DomainShader = new DomainShader(D3D.Device, domainShaderByteCode);
                */


                inputElements = new InputElement[]
                {
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "COLOR",// 16
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 0,
                        AlignedByteOffset =InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "NORMAL", // 12
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32B32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement() // 2.2f
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "TEXCOORD", // 8
                        SemanticIndex = 0,
                        Format = SharpDX.DXGI.Format.R32G32_Float,
                        Slot = 0,
                        AlignedByteOffset =InputElement.AppendAligned,
                        Classification =InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 2,
                        Format = SharpDX.DXGI.Format.R32_Float,
                        Slot = 0,
                        AlignedByteOffset = InputElement.AppendAligned,
                        Classification = InputClassification.PerVertexData,
                        InstanceDataStepRate = 0
                    },












                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 1,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 1,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 2,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 2,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 3,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 3,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 4,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 4,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },



                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 5,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 6,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 7,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 8,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },







                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 9,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 10,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 11,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 12,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 6,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },










                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 13,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 7,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 14,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 7,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 15,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 7,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 16,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 7,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },





                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 17,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 8,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 18,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 8,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 19,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 8,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },

                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 20,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 8,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },


                    /*
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 21,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =  0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 22,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 23,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    //TOO MANY BINDINGS
                    new InputElement()
                    {
                        SemanticName = "POSITION", // 16
                        SemanticIndex = 24,
                        Format = SharpDX.DXGI.Format.R32G32B32A32_Float,
                        Slot = 5,
                        AlignedByteOffset =   InputElement.AppendAligned,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    */






                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 3,
                        Format = SharpDX.DXGI.Format.R32_SInt,
                        Slot = 9,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                    new InputElement()
                    {
                        SemanticName = "PSIZE", // 4
                        SemanticIndex = 4,
                        Format = SharpDX.DXGI.Format.R32_SInt,
                        Slot = 10,
                        AlignedByteOffset = 0,
                        Classification = InputClassification.PerInstanceData,
                        InstanceDataStepRate = 1
                    },
                };










                Layout = new InputLayout(D3D.device, ShaderSignature.GetInputSignature(vertexShaderByteCode), inputElements);

                contantBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, Utilities.SizeOf<DMatrixBuffer>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);


                vertexShaderByteCode.Dispose();
                //domainShaderByteCode.Dispose();
                //hullShaderByteCode.Dispose();
                pixelShaderByteCode.Dispose();
                geometryShaderByteCode.Dispose();


                Vector4 ambientColor = new Vector4(0.15f, 0.15f, 0.15f, 1.0f);
                Vector4 diffuseColour = new Vector4(1, 1, 1, 1);
                Vector4 lightDirection = new Vector4(1, 0, 0,1.0f);
                Vector4 lightpos0 = new Vector4(1, 0, 0, 1.0f);
                Vector4 dirLight0 = new Vector4(1, 0, 0, 1.0f);

                lightBufferInstChunk[0] = new DLightBufferEr()
                {
                    ambientColor = ambientColor,
                    diffuseColor = diffuseColour,
                    lightDirection = dirLight0,
                    specularcolor = lightpos0,
                    //padding0 = 35,
                    lightPosition = lightpos0,
                    lightextras = lightpos0,
                    //padding1 = 100
                };


                BufferDescription lightBufferDesc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DLightBufferEr>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };


                ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, lightBufferDesc);
                //var ConstantLightBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, Utilities.SizeOf<DMatrixBuffer>(), ResourceUsage.Dynamic, BindFlags.ConstantBuffer, CpuAccessFlags.Write, ResourceOptionFlags.None, 0);






                /*SC_instancedChunk.DIndexType[] indexArray = new SC_instancedChunk.DIndexType[SC_Globals.tinyChunkWidth* SC_Globals.tinyChunkHeight* SC_Globals.tinyChunkDepth];
                for (int i = 0; i < SC_Globals.tinyChunkWidth * SC_Globals.tinyChunkHeight * SC_Globals.tinyChunkDepth; i++)
                {
                    indexArray[i] = new SC_instancedChunk.DIndexType()
                    {
                        indexPos = originalMap[i],
                    };
                }*/



                var matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceType)) * instances.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);



                var matrixBufferDescriptionVertexcNF = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.heightmapinstance)) * heightmapmatrixbuff.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instanceBufferHeightmap = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNF);





                var matrixBufferDescriptionVertexcNFm = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)) * instancesmatrix.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesmatrixbuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNFm);



                var matrixBufferDescriptionVertexcNFmb = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)) * instancesmatrixb.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesmatrixbufferb = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNFmb);


                var matrixBufferDescriptionVertexcNFmc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)) * instancesmatrixc.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesmatrixbufferc = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNFmc);



                var matrixBufferDescriptionVertexcNFmd = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceMatrix)) * instancesmatrixd.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesmatrixbufferd = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexcNFmd);




                var matrixBufferDescriptionVertexx = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstancesByteMap)) * instancesbytemaps.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesbytemapsbuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertexx);


                IndexBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);



                matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)) * instancesDataFORWARD.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBufferFORWARD = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);

                //var InstanceRotationBufferFORWARD = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instances);

                matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)) * instancesDataRIGHT.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBufferRIGHT = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);

                //var InstanceRotationBufferRIGHT = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instances);

                matrixBufferDescriptionVertex00 = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstanceShipData)) * instancesDataUP.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceRotationBufferUP = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionVertex00);
                //var InstanceRotationBufferUP = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instances);
                //InstanceRotationMatrixBuffer = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instancesDataForward);



                VertexBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.VertexBuffer, originalArrayOfVerts);
                IndicesBuffer = SharpDX.Direct3D11.Buffer.Create(D3D.device, BindFlags.IndexBuffer, originalArrayOfIndices);







                var matrixBufferDescriptionbytemaparray = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Marshal.SizeOf(typeof(SC_instancedChunk.DInstancesccsbytemapxyz)) * instancesccsbytemapxyz.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                instancesbytemapsmatrixbuffer = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescriptionbytemaparray);


                BufferDescription matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<SC_instancedChunk.DInstanceTypeLocW>() * instancesLocationW.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceBufferLocW = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);

                matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<SC_instancedChunk.DInstanceTypeLocW>() * instancesLocationH.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                InstanceBufferLocH = new SharpDX.Direct3D11.Buffer(D3D.device, matrixBufferDescription);






                // Setup the description of the dynamic matrix constant buffer that is in the vertex shader.
                BufferDescription someoculusheadsetdirection = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<OVRDir>(),
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the vertex shader constant buffer from within this class.
                someoculusdirbuffer = new SharpDX.Direct3D11.Buffer(D3D.device, someoculusheadsetdirection);



                // Setup the description of the dynamic tessellation constant buffer that is in the hull shader.
                BufferDescription tessellationBufferDesc = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DTessellationBufferType>(), // was Matrix
                    BindFlags = BindFlags.ConstantBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                // Create the constant buffer pointer so we can access the hull shader constant buffer from within this class.
                ConstantTessellationBuffer = new SharpDX.Direct3D11.Buffer(D3D.device, tessellationBufferDesc);








                // Create a texture sampler state description.
                SamplerStateDescription samplerDesc = new SamplerStateDescription()
                {
                    Filter = Filter.MinMagMipLinear,
                    AddressU = TextureAddressMode.Wrap,
                    AddressV = TextureAddressMode.Wrap,
                    AddressW = TextureAddressMode.Wrap,
                    MipLodBias = 0,
                    MaximumAnisotropy = 1,
                    ComparisonFunction = Comparison.Always,
                    BorderColor = new Color4(0, 0, 0, 0),  // Black Border.
                    MinimumLod = 0,
                    MaximumLod = float.MaxValue
                };

                // Create the texture sampler state.
                samplerState = new SamplerState(D3D.device, samplerDesc);







                /*matrixBufferDescription = new BufferDescription()
                {
                    Usage = ResourceUsage.Dynamic,
                    SizeInBytes = Utilities.SizeOf<DInstanceTypeLocD>() * instancesLocationD.Length,
                    BindFlags = BindFlags.VertexBuffer,
                    CpuAccessFlags = CpuAccessFlags.Write,
                    OptionFlags = ResourceOptionFlags.None,
                    StructureByteStride = 0
                };

                SharpDX.Direct3D11.Buffer InstanceBufferLocD = new SharpDX.Direct3D11.Buffer(SC_console_directx.D3D.device, matrixBufferDescription);
                */

                /*for (int x = 0; x < numberOfObjectInWidth_; x++)
                {
                    for (int y = 0; y < numberOfObjectInHeight_; y++)
                    {
                        for (int z = 0; z < numberOfObjectInDepth_; z++)
                        {
                            var chunkprimindex = x + numberOfObjectInWidth_ * (y + numberOfObjectInHeight_ * z);

                            arrayOfChunkData[chunkprimindex].InstanceBufferLocW = InstanceBufferLocW;
                            arrayOfChunkData[chunkprimindex].InstanceBufferLocH = InstanceBufferLocH;
                            //arrayOfChunkData[chunkprimindex].InstanceBufferLocD = InstanceBufferLocD;
                        }
                    }
                }*/





                //Console.WriteLine(perceptronRotationRL.Length);
                //Vector3 chunkscreenoriginposition = new Vector3((numberOfObjectInDepth_ * numberOfInstancesPerObjectInDepth_ * planeSize_) * 2, 0, -(numberOfObjectInDepth_ * numberOfInstancesPerObjectInDepth_ * SC_Globals.tinyChunkDepth * planeSize_) * 2);
                //Vector3 chunkscreenoriginposition = new Vector3(10f, 0, -(numberOfObjectInDepth_ * numberOfInstancesPerObjectInDepth_ * numberOfObjectInDepth_ * planeSize_) / 2);

                Matrix worldMat = Matrix.Identity;


                somechunk = new chunkscreen();
                for (int x = 0; x < numberOfObjectInWidth_; x++)
                {
                    for (int y = 0; y < numberOfObjectInHeight_; y++)
                    {
                        for (int z = 0; z < numberOfObjectInDepth_; z++)
                        {
                            var chunkprimindex = x + numberOfObjectInWidth_ * (y + numberOfObjectInHeight_ * z);

                            Vector3 chunkPos = new Vector3(x, y, z); //4
                            //Vector3 chunkPos = new Vector3(x, y, z);

                            //chunkPos.X += ((SC_Globals.tinyChunkWidth) * 1 * planeSize_);
                            //chunkPos.Y += ((SC_Globals.tinyChunkHeight) * 1 * planeSize_);
                            //chunkPos.Z += ((SC_Globals.tinyChunkDepth) * 1 * planeSize_);

                            /*instancesLocationW[chunkprimindex] = new DInstanceTypeLocW()
                            {
                                index = x // numberOfObjectInWidth_ - 1 - x
                            };

                            instancesLocationH[chunkprimindex] = new DInstanceTypeLocH()
                            {
                                index = numberOfInstancesPerObjectInHeight_ - 1 - y //y//y //numberOfObjectInHeight_ - 1 - y //
                            };*/
                            /*
                            instancesLocationD[chunkprimindex] = new DInstanceTypeLocD()
                            {
                                index = z//y //numberOfObjectInHeight_ - 1 - y //
                            };*/

                            //sccs ORIGINAL
                            /*chunkPos.X *= (numberOfInstancesPerObjectInWidth * tinyChunkWidth);
                            chunkPos.Y *= (numberOfInstancesPerObjectInHeight * tinyChunkHeight);
                            chunkPos.Z *= (numberOfInstancesPerObjectInDepth * tinyChunkDepth);*/
                            //sccs ORIGINAL

                            //  int numberOfObjectInWidth; int numberOfObjectInHeight; int numberOfObjectInDepth; int numberOfInstancesPerObjectInWidth; int numberOfInstancesPerObjectInHeight; int numberOfInstancesPerObjectInDepth; float planeSize;

                            chunkPos.X *= numberOfInstancesPerObjectInWidth * tinyChunkWidth * planeSize;
                            chunkPos.Y *= numberOfInstancesPerObjectInHeight * tinyChunkHeight * planeSize;
                            chunkPos.Z *= numberOfInstancesPerObjectInDepth * tinyChunkDepth * planeSize;


                            //chunkPos.X *= SC_GlobalsChunkKeyboard.numberOfInstancesPerObjectInWidth * SC_GlobalsChunkKeyboard.numberOfObjectInWidth * SC_GlobalsChunkKeyboard.tinyChunkWidth * SC_GlobalsChunkKeyboard.planeSize;
                            //chunkPos.Y *= SC_GlobalsChunkKeyboard.numberOfInstancesPerObjectInWidth * SC_GlobalsChunkKeyboard.numberOfObjectInWidth * SC_GlobalsChunkKeyboard.tinyChunkWidth * SC_GlobalsChunkKeyboard.planeSize;
                            //chunkPos.Z *= SC_GlobalsChunkKeyboard.numberOfInstancesPerObjectInWidth * SC_GlobalsChunkKeyboard.numberOfObjectInWidth * SC_GlobalsChunkKeyboard.tinyChunkWidth * SC_GlobalsChunkKeyboard.planeSize;



                            //4tiny * 10tinyinst * tinyinstmeshclass

                            /*
                            chunkPos.X *= (numberOfObjectInWidth_ * planeSize_);
                            chunkPos.Y *= (numberOfObjectInHeight_ * planeSize_);
                            chunkPos.Z *= ( numberOfObjectInDepth_ * planeSize_);
                            */

                            //chunkPos.X*= planeSize_;
                            //chunkPos.Y *= planeSize_;
                            //chunkPos.Z *= planeSize_;

                            chunkPos += somechunkpriminstancepos;

                            Matrix mainObjectInstance1stCubeMatrix = worldmatofobj;// Matrix.Identity;
                            mainObjectInstance1stCubeMatrix.M41 = chunkPos.X;
                            mainObjectInstance1stCubeMatrix.M42 = chunkPos.Y;
                            mainObjectInstance1stCubeMatrix.M43 = chunkPos.Z;

                            double m11 = 0;
                            double m12 = 0;
                            double m13 = 0;
                            double m14 = 0;
                            double m21 = 0;
                            double m22 = 0;
                            double m23 = 0;
                            double m24 = 0;
                            double m31 = 0;
                            double m32 = 0;
                            double m33 = 0;
                            double m34 = 0;
                            double m41 = 0;
                            double m42 = 0;
                            double m43 = 0;
                            double m44 = 0;


                            double m11b = 0;
                            double m12b = 0;
                            double m13b = 0;
                            double m14b = 0;
                            double m21b = 0;
                            double m22b = 0;
                            double m23b = 0;
                            double m24b = 0;
                            double m31b = 0;
                            double m32b = 0;
                            double m33b = 0;
                            double m34b = 0;
                            double m41b = 0;
                            double m42b = 0;
                            double m43b = 0;
                            double m44b = 0;






                            double m11c = 0;
                            double m12c = 0;
                            double m13c = 0;
                            double m14c = 0;
                            double m21c = 0;
                            double m22c = 0;
                            double m23c = 0;
                            double m24c = 0;
                            double m31c = 0;
                            double m32c = 0;
                            double m33c = 0;
                            double m34c = 0;
                            double m41c = 0;
                            double m42c = 0;
                            double m43c = 0;
                            double m44c = 0;


                            double m11d = 0;
                            double m12d = 0;
                            double m13d = 0;
                            double m14d = 0;
                            double m21d = 0;
                            double m22d = 0;
                            double m23d = 0;
                            double m24d = 0;
                            double m31d = 0;
                            double m32d = 0;
                            double m33d = 0;
                            double m34d = 0;
                            double m41d = 0;
                            double m42d = 0;
                            double m43d = 0;
                            double m44d = 0;

                            int[] theMap;

                            //var somechunk = new chunkscreen();
                            //somechunk.startBuildingArray(new Vector4(chunkPos.X, chunkPos.Y, chunkPos.Z, 1), out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, out theMap, null, -1, null, numberofmainobjectx, numberofmainobjecty, numberofmainobjectz, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, voxeltype, typeofbytemapobject, mainix, mainiy, mainiz, x, y, z, 0, 0, 0);

                            somechunk.Dispose();
                            somechunk.startBuildingArray(new Vector4(chunkPos.X, chunkPos.Y, chunkPos.Z, 1), out m11, out m12, out m13, out m14, out m21, out m22, out m23, out m24, out m31, out m32, out m33, out m34, out m41, out m42, out m43, out m44, out theMap, null, -1, null, numberofmainobjectx, numberofmainobjecty, numberofmainobjectz, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, voxeltype, typeofbytemapobject, mainix, mainiy, mainiz, x, y, z, 0, 0, 0
                                   , out m11b, out m12b, out m13b, out m14b, out m21b, out m22b, out m23b, out m24b, out m31b, out m32b, out m33b, out m34b, out m41b, out m42b, out m43b, out m44b,
                            out m11c, out m12c, out m13c, out m14c, out m21c, out m22c, out m23c, out m24c, out m31c, out m32c, out m33c, out m34c, out m41c, out m42c, out m43c, out m44c,
                            out m11d, out m12d, out m13d, out m14d, out m21d, out m22d, out m23d, out m24d, out m31d, out m32d, out m33d, out m34d, out m41d, out m42d, out m43d, out m44d, -1);





                            arrayofindexzeromesh[chunkprimindex] = new SC_instancedChunk(1, 1, 1, new Vector4(0.1f, 0.1f, 0.1f, 1), numberOfInstancesPerObjectInWidth_, numberOfInstancesPerObjectInHeight_, numberOfInstancesPerObjectInDepth_, chunkPos, mainObjectInstance1stCubeMatrix, somechunk, _addtoworld, _mass, _the_world, _is_static, _tag, numberofmainobjectx, numberofmainobjecty, numberofmainobjectz, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, offsetinstance, instancesbytemaps, somechunkkeyboardpriminstanceindex_, chunkprimindex, voxeltype, typeofbytemapobject, mainix, mainiy, mainiz, x, y, z); //, instances[chunkprimindex]





                            /*for ()
                            {

                            }
                            arrayofindexzeromesh[chunkprimindex].arrayofzeromeshinstances[]*/



                            arrayOfChunkData[chunkprimindex] = new chunkData();



                            arrayOfChunkData[chunkprimindex].VertexShader = VertexShader;
                            arrayOfChunkData[chunkprimindex].PixelShader = PixelShader;
                            arrayOfChunkData[chunkprimindex].GeometryShader = GeometryShader;



                            arrayOfChunkData[chunkprimindex].Layout = Layout;

                            arrayOfChunkData[chunkprimindex].InstanceBufferLocW = InstanceBufferLocW;
                            arrayOfChunkData[chunkprimindex].InstanceBufferLocH = InstanceBufferLocH;


                            arrayOfChunkData[chunkprimindex].numberOfObjectInWidth = numberOfObjectInWidth;
                            arrayOfChunkData[chunkprimindex].numberOfObjectInHeight = numberOfObjectInHeight;
                            arrayOfChunkData[chunkprimindex].numberOfObjectInDepth = numberOfObjectInDepth;

                            arrayOfChunkData[chunkprimindex].numberOfInstancesPerObjectInWidth = numberOfInstancesPerObjectInWidth;
                            arrayOfChunkData[chunkprimindex].numberOfInstancesPerObjectInHeight = numberOfInstancesPerObjectInHeight;
                            arrayOfChunkData[chunkprimindex].numberOfInstancesPerObjectInDepth = numberOfInstancesPerObjectInDepth;

                            arrayOfChunkData[chunkprimindex].planeSize = planeSize;



                            arrayOfChunkData[chunkprimindex].samplerState = samplerState;





                            arrayOfChunkData[chunkprimindex].candrawswtc = 0;


                            if (voxeltype == 0 || voxeltype == 1 || voxeltype == 2 || voxeltype == 3)
                            {
                                arrayOfChunkData[chunkprimindex].arrayOfVertex = sccstrigvertbuilder.vertexlist.ToArray();//.ToArray();
                                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sccstrigvertbuilder.listOfTriangleIndices.ToArray();//.ToArray();


                            }
                            /*else if (voxeltype == 2)
                            {
                                arrayOfChunkData[chunkprimindex].arrayOfVertex = sccstrigvertbuilder.vertexlist.ToArray();//.ToArray();
                                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sccstrigvertbuilder.listOfTriangleIndices.ToArray();//.ToArray();

                                //arrayOfChunkData[chunkprimindex].arrayOfVertex = sccstrigvertbuilderreduced.vertexlist.ToArray();//.ToArray();
                                //arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sccstrigvertbuilderreduced.listOfTriangleIndices.ToArray();//.ToArray();

                                //voxeltype = 0;
                            }*/
                            /*else if (voxeltype == 3)
                            {
                                arrayOfChunkData[chunkprimindex].arrayOfVertex = sccstrigvertbuildermorescreen.vertexlist.ToArray();//.ToArray();
                                arrayOfChunkData[chunkprimindex].originalArrayOfIndices = sccstrigvertbuildermorescreen.listOfTriangleIndices.ToArray();//.ToArray();

                                //voxeltype = 0;
                            }*/


                            arrayOfChunkData[chunkprimindex].someoculusdirbuffer = someoculusdirbuffer;

                            arrayOfChunkData[chunkprimindex].someovrdir = someovrdir;

                            arrayOfChunkData[chunkprimindex].instancesmatrix = arrayofindexzeromesh[chunkprimindex].instancesmatrix;
                            arrayOfChunkData[chunkprimindex].instancesmatrixbuffer = instancesmatrixbuffer;
                            arrayOfChunkData[chunkprimindex].instancesmatrixb = arrayofindexzeromesh[chunkprimindex].instancesmatrixb;
                            arrayOfChunkData[chunkprimindex].instancesmatrixbufferb = instancesmatrixbufferb;
                            arrayOfChunkData[chunkprimindex].instancesmatrixc = arrayofindexzeromesh[chunkprimindex].instancesmatrixc;
                            arrayOfChunkData[chunkprimindex].instancesmatrixbufferc = instancesmatrixbufferc;
                            arrayOfChunkData[chunkprimindex].instancesmatrixd = arrayofindexzeromesh[chunkprimindex].instancesmatrixd;
                            arrayOfChunkData[chunkprimindex].instancesmatrixbufferd = instancesmatrixbufferd;


                            arrayOfChunkData[chunkprimindex].lastinstancesmatrixa = arrayofindexzeromesh[chunkprimindex].lastinstancesmatrixa;
                            arrayOfChunkData[chunkprimindex].lastinstancesmatrixb = arrayofindexzeromesh[chunkprimindex].lastinstancesmatrixb;
                            arrayOfChunkData[chunkprimindex].lastinstancesmatrixc = arrayofindexzeromesh[chunkprimindex].lastinstancesmatrixc;
                            arrayOfChunkData[chunkprimindex].lastinstancesmatrixd = arrayofindexzeromesh[chunkprimindex].lastinstancesmatrixd;


                            //UPDATE OR NOT ALL THE LASTINSTANCEMATRIXABCD VALUES INITIALLY FROM M11 TO M44 OR NOT, TO BE DIFFERENT VALUES FROM THE INSTANCEMATRIXABCD. TO BE THOUGHT ABOUT.
                            /*
                            for (int i = 0; i < arrayOfChunkData[chunkprimindex].lastinstancesmatrixa.Length;i++ )
                            {
                                arrayOfChunkData[chunkprimindex].lastinstancesmatrixa[i].instancematrix.M11 = -1;
                            }
                            */






                            arrayOfChunkData[chunkprimindex].ConstantTessellationBuffer = ConstantTessellationBuffer;
                            arrayOfChunkData[chunkprimindex].tessellationBuffer = arrayofindexzeromesh[chunkprimindex].tessellationBuffer;



                            arrayOfChunkData[chunkprimindex].heightmapmatrix = arrayofindexzeromesh[chunkprimindex].heightmapmatrix;
                            arrayOfChunkData[chunkprimindex].instanceBufferHeightmap = instanceBufferHeightmap;






                            arrayOfChunkData[chunkprimindex].instancesbytemaps = arrayofindexzeromesh[chunkprimindex].instancesbytemaps;
                            arrayOfChunkData[chunkprimindex].instancesbytemapsbuffer = instancesbytemapsbuffer;


                            arrayOfChunkData[chunkprimindex].instancesccsbytemapxyz = arrayofindexzeromesh[chunkprimindex].instancesccsbytemapxyz;
                            arrayOfChunkData[chunkprimindex].instancesbytemapsmatrixbuffer = instancesbytemapsmatrixbuffer;






                            arrayOfChunkData[chunkprimindex].copytobuffer = 1;

                            arrayOfChunkData[chunkprimindex].switchForRender = 1;
                            arrayOfChunkData[chunkprimindex].instanceBuffer = InstanceBuffer;
                            arrayOfChunkData[chunkprimindex].SC_instancedChunk_Instances = arrayofindexzeromesh[chunkprimindex].instances;




                            arrayOfChunkData[chunkprimindex].InstanceRotationBufferFORWARD = InstanceRotationBufferFORWARD;
                            arrayOfChunkData[chunkprimindex].InstanceRotationBufferRIGHT = InstanceRotationBufferRIGHT;
                            arrayOfChunkData[chunkprimindex].InstanceRotationBufferUP = InstanceRotationBufferUP;



                            arrayOfChunkData[chunkprimindex].SC_instancedChunk_InstancesFORWARD = arrayofindexzeromesh[chunkprimindex].instancesDataFORWARD;
                            arrayOfChunkData[chunkprimindex].SC_instancedChunk_InstancesRIGHT = arrayofindexzeromesh[chunkprimindex].instancesDataRIGHT;
                            arrayOfChunkData[chunkprimindex].SC_instancedChunk_InstancesUP = arrayofindexzeromesh[chunkprimindex].instancesDataUP;

                            //arrayOfChunkData[chunkprimindex].InstanceRotationBufferFORWARD = arrayofindexzeromesh[chunkprimindex].buffer;
                            //arrayOfChunkData[chunkprimindex].InstanceRotationBufferRIGHT = arrayofindexzeromesh[chunkprimindex].instancesDataRIGHT;
                            //arrayOfChunkData[chunkprimindex].InstanceRotationBufferUP = arrayofindexzeromesh[chunkprimindex].instancesDataUP;




                            arrayOfChunkData[chunkprimindex].IndicesBuffer = IndicesBuffer;
                            arrayOfChunkData[chunkprimindex].vertexBuffer = VertexBuffer;


                            arrayOfChunkData[chunkprimindex].constantLightBuffer = ConstantLightBuffer;

                            arrayOfChunkData[chunkprimindex].constantMatrixPosBuffer = contantBuffer;
                            arrayOfChunkData[chunkprimindex].lightBuffer = lightBufferInstChunk;
                            //arrayOfChunkData[chunkprimindex].instancesLocationW = instancesLocationW;
                            //arrayOfChunkData[chunkprimindex].instancesLocationH = instancesLocationH;

                            ///arrayofindexzeromesh[chunkprimindex].instancesLocationW = instancesLocationW;
                            //arrayofindexzeromesh[chunkprimindex].instancesLocationH = instancesLocationH;



                            arrayOfChunkData[chunkprimindex].instancesLocationW = arrayofindexzeromesh[chunkprimindex].instancesLocationW;
                            arrayOfChunkData[chunkprimindex].instancesLocationH = arrayofindexzeromesh[chunkprimindex].instancesLocationH;



                            //arrayOfChunkData[chunkprimindex].instancesLocationD = instancesLocationD;

                            //worldMat.M41 = chunkPos.X;
                            //worldMat.M42 = chunkPos.Y;
                            //worldMat.M43 = chunkPos.Z;

                            arrayOfChunkData[chunkprimindex].worldMatrix = mainObjectInstance1stCubeMatrix; //mainObjectInstance1stCubeMatrix

                            //arrayOfChunkData[chunkprimindex].shaderOfChunk = new SC_instancedChunk_shader_final(D3D.device);
                        }
                    }
                }





                /*SC_instancedChunk.DIndexType[] indexArray = new SC_instancedChunk.DIndexType[SC_Globals.tinyChunkWidth * SC_Globals.tinyChunkHeight * SC_Globals.tinyChunkDepth];
                for (int i = 0; i < SC_Globals.tinyChunkWidth * SC_Globals.tinyChunkHeight * SC_Globals.tinyChunkDepth; i++)
                {
                    indexArray[i] = new SC_instancedChunk.DIndexType()
                    {
                        indexPos = originalMap[i],
                    };
                }*/



                /*
                indexArray = new SC_instancedChunk.DVertex[originalArrayOfIndices.Length];
                for (int i = 0; i < originalArrayOfIndices.Length; i++)
                {
                    indexArray[i] = new SC_instancedChunk.DVertex()
                    {
                        indexPos = originalArrayOfIndices[i],
                    };
                }
                */




                //SharpDX.Direct3D11.Buffer InstanceBufferLocW = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instancesLocationW);
                //SharpDX.Direct3D11.Buffer InstanceBufferLocH = SharpDX.Direct3D11.Buffer.Create(SC_console_directx.D3D.device, BindFlags.VertexBuffer, instancesLocationH);





                //shaderOfChunk = new SC_instancedChunk_shader_final(D3D.device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer, originalArrayOfVerts, indexArray, VertexBuffer, IndicesBuffer, IndexBuffer, instancesbytemapsbuffer);
                ///shaderOfChunk = new SC_instancedChunk_shader_final(D3D.device, contantBuffer, Layout, VertexShader, PixelShader, GeometryShader, InstanceBuffer, ConstantLightBuffer, originalArrayOfVerts, originalArrayOfIndices, VertexBuffer, IndicesBuffer, IndexBuffer, instancesbytemapsbuffer, instanceBufferHeightmap, numberOfObjectInWidth, numberOfObjectInHeight, numberOfObjectInDepth, numberOfInstancesPerObjectInWidth, numberOfInstancesPerObjectInHeight, numberOfInstancesPerObjectInDepth, tinyChunkWidth, tinyChunkHeight, tinyChunkDepth, planeSize, usegeometryshader);

                shaderOfChunk = new SC_instancedChunk_shader_final(D3D.device);

            }
            catch (Exception ex)
            {
                Program.MessageBox((IntPtr)0, ex.ToString(), "sccs", 0);
            }
        }
    }
}
