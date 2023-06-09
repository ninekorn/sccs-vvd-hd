﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpDX;

using SimplexNoise;

//from youtube Craig Perko.
using Math = System.Math;

using System.Drawing;
using System.Linq;
using System.Linq;
using System.Runtime.InteropServices;




namespace sccs.scgraphics
{

    public struct sc_chunk_node
    {
        public float _distance_parent_node_to_this;
        public float _distance_to_target;
        public Vector3 _position;
    }

    //https://www.c-sharpcorner.com/article/generating-random-number-and-string-in-C-Sharp/
    public class sccsvoxels
    {
        [DllImport("User32.dll", CharSet = CharSet.Unicode)]
        public static extern int MessageBox(IntPtr h, string m, string c, int type);

        private Bitmap bitmap;
        private System.Drawing.Color[,] colors;
        private Bitmap earthLookupBitmap;
        private System.Drawing.Color[] earthLookupTable;



        public int ChunkWidth_L = 50;
        public int ChunkWidth_R = 49;

        public int ChunkHeight_L = 50;
        public int ChunkHeight_R = 49;

        public int ChunkDepth_L = 50;
        public int ChunkDepth_R = 49;

        //public int ChunkWidth = -1;
        //public int ChunkHeight = -1;
        //public int ChunkDepth = -1;


        int _max;


        float _current_visual_distance_spike_glyco_protein_covid19_min = 0;
        float _current_visual_distance_spike_glyco_protein_covid19_max = 0;
        float _current_counter_for_adding_spike_glyco_protein_covid19 = 0;

        //public byte[] map;
        private int[] map;
        private float planeSize = 1;
        private int seed = 3420;

        private int block;

        private int counterVertexTop = 0;

        private int vertzIndex = 0;
        private int trigsIndex = 0;

        private int _detailScale = 10;
        private int _heightScale = 10;

        private Vector3 forward = new Vector3(0, 0, 1);
        private Vector3 back = new Vector3(0, 0, -1);
        private Vector3 right = new Vector3(1, 0, 0);
        private Vector3 left = new Vector3(-1, 0, 0);
        private Vector3 up = new Vector3(0, 1, 0);
        private Vector3 down = new Vector3(0, -1, 0);


        private List<SC_cube.DVertex> listOfVerts = new List<SC_cube.DVertex>();
        private List<int> listOfTriangleIndices = new List<int>();

        int randX = 3420;
        int randY = 3420;
        public static int countingArrayOfChunks = 0;


        float colorX = 0.75f;
        float colorY = 0.75f;
        float colorZ = 0.75f;
        float _tinyChunkHeightScale = 200;
        Vector3 _pos;



        int _swtch_spike_00 = 0;
        int _swtch_spike_01 = 0;
        int _swtch_spike_02 = 0;
        int _swtch_spike_03 = 0;
        int _swtch_spike_04 = 0;
        int _swtch_spike_05 = 0;
        int _swtch_spike_06 = 0;
        int _swtch_spike_07 = 0;


        /*
        //FROM CRAIG PERKO TUTORIALS
        public virtual float CalculateNoiseValue(Vector3 pos, Vector3 offset, float scale)
        {

            float noiseX = Math.Abs((pos.X + offset.X) * scale);
            float noiseY = Math.Abs((pos.Y + offset.Y) * scale);
            float noiseZ = Math.Abs((pos.Z + offset.Z) * scale);

            return Noise.Generate(noiseX, noiseY, noiseZ);

        }*/

        Random rand = new Random();



        Vector3 SphericalToCartesian(float radius, float polar, float elevation) //xyz
        {
            float a = (float)(radius * Math.Cos(elevation));
            float x = (float)(a * Math.Cos(polar));
            float y = (float)(radius * Math.Sin(elevation));
            float z = (float)(a * Math.Sin(polar));

            return new Vector3(x, y, z);
        }


        //https://pastebin.com/fAFp6NnN // Also found on the unity3D forums by an old response of user Aldonaletto but i think the answer was changed the last time i checked or the post isn't there anymore.
        public static Vector3 _getDirection(Vector3 value, SharpDX.Quaternion rotation)
        {
            Vector3 vector;
            double num12 = rotation.X + rotation.X;
            double num2 = rotation.Y + rotation.Y;
            double num = rotation.Z + rotation.Z;
            double num11 = rotation.W * num12;
            double num10 = rotation.W * num2;
            double num9 = rotation.W * num;
            double num8 = rotation.X * num12;
            double num7 = rotation.X * num2;
            double num6 = rotation.X * num;
            double num5 = rotation.Y * num2;
            double num4 = rotation.Y * num;
            double num3 = rotation.Z * num;
            double num15 = ((value.X * ((1f - num5) - num3)) + (value.Y * (num7 - num9))) + (value.Z * (num6 + num10));
            double num14 = ((value.X * (num7 + num9)) + (value.Y * ((1f - num8) - num3))) + (value.Z * (num4 - num11));
            double num13 = ((value.X * (num6 - num10)) + (value.Y * (num4 + num11))) + (value.Z * ((1f - num8) - num5));
            vector.X = (float)num15;
            vector.Y = (float)num14;
            vector.Z = (float)num13;
            return vector;
        }

        //http://james-ramsden.com/angle-between-two-vectors/
        double AngleBetween(Vector3 u, Vector3 v, bool returndegrees)
        {
            double toppart = 0;
            for (int d = 0; d < 3; d++) toppart += u[d] * v[d];

            double u2 = 0; //u squared
            double v2 = 0; //v squared
            for (int d = 0; d < 3; d++)
            {
                u2 += u[d] * u[d];
                v2 += v[d] * v[d];
            }

            double bottompart = 0;
            bottompart = Math.Sqrt(u2 * v2);


            double rtnval = Math.Acos(toppart / bottompart);
            if (returndegrees) rtnval *= 360.0 / (2 * Math.PI);
            return rtnval;
        }

        float randomX = 1;
        float randomY = 1;
        float randomZ = 1;
        /*private float npcCheckDistance (Vector3 nodeA, Vector3 nodeB)
        {
            var dstX = Math.Abs((nodeA.X) - (nodeB.X));
            var dstY = Math.Abs((nodeA.Y) - (nodeB.Y));
            var dstZ = Math.Abs((nodeA.Z) - (nodeB.Z));

            if (dstX > dstZ)
                return 14 * dstZ + 10 * (dstX - dstZ);
            return 14 * dstX + 10 * (dstZ - dstX);
        }*/





        //CURRENTLY THE MIX IS NOT HOMOGENOUS SO I CANNOT RANDOMLY CREATE SPIKES ALL AROUND AND HAVE THEM DISTANCED ENOUGH FROM EACH OTHER. I HAVE TO CREATE ONE SPIKE PER EIGTH OF THAT
        //SPHEROID CHUNK

        //DEVELOPED BY STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9, FIRST FOR TRYING TO DIG TUNNELS IN VOXELS AND THEN I JUST TRIED TO MAKE TUNNELS IN A SPHERE'S QUARTERS TO MAKE IT LOOK LIKE A COVID19 CELL.
        //DEVELOPED BY STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9, FIRST FOR TRYING TO DIG TUNNELS IN VOXELS AND THEN I JUST TRIED TO MAKE TUNNELS IN A SPHERE'S QUARTERS TO MAKE IT LOOK LIKE A COVID19 CELL.
        //DEVELOPED BY STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9, FIRST FOR TRYING TO DIG TUNNELS IN VOXELS AND THEN I JUST TRIED TO MAKE TUNNELS IN A SPHERE'S QUARTERS TO MAKE IT LOOK LIKE A COVID19 CELL.
        private void CreateSpikeGlycoProteinCOVID19(Vector3 center, int x, int y, int z) //, float min, float max //ref BlockData[,] blocks, TerrainType terrain
        {
            //MessageBox((IntPtr)0, "test", "Oculus error", 0);
            Vector3 current_target_pos = new Vector3(x, y, z);

            Vector3 _spike_direction = current_target_pos;

            float _spike_length = _spike_direction.Length();
            _spike_direction.Normalize();

            int _spike_max_length = (int)Math.Round(_spike_length - 1);// (int)Math.Round(_spike_length);// (int)Math.Round(_spike_length);// (int)(Math.Floor(rand.NextDouble() * (ChunkHeight - 1) + 0));

            Vector3? current_start_move_pos = center;

            //float xpos = x;
            //float ypos = y;
            //float zpos = z;

            float xpos = (int)Math.Round(current_target_pos.X);
            float ypos = (int)Math.Round(current_target_pos.Y);
            float zpos = (int)Math.Round(current_target_pos.Z);

            if (xpos < 0)
            {
                xpos *= -1;
                xpos = (ChunkWidth_R) + xpos;
            }

            if (ypos < 0)
            {
                ypos *= -1;
                ypos = (ChunkHeight_R) + ypos;
            }

            if (zpos < 0)
            {
                zpos *= -1;
                zpos = (ChunkDepth_R) + zpos;
            }

            var _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));



            /*if (_index >= 0 && _index < _max)
            {
                //MessageBox((IntPtr)0, "in of array length " + _max, "Oculus error", 0);
                map[_index] = 1;
            }
            else
            {
                MessageBox((IntPtr)0, _index+ " _ " + _max, "Oculus error", 0);
            }*/




            /*for (int xx = -1; xx <= 1; xx++)
            {
                for (int yy = -1; yy <= 1; yy++)
                {
                    for (int zz = -1; zz <= 1; zz++)
                    {

                        xpos = (int)Math.Round(current_target_pos.X + xx);
                        ypos = (int)Math.Round(current_target_pos.Y + yy);
                        zpos = (int)Math.Round(current_target_pos.Z + zz);

                        if (xpos < 0)
                        {
                            xpos *= -1;
                            xpos = (ChunkWidth_R) + xpos;
                        }

                        if (ypos < 0)
                        {
                            ypos *= -1;
                            ypos = (ChunkHeight_R) + ypos;
                        }

                        if (zpos < 0)
                        {
                            zpos *= -1;
                            zpos = (ChunkDepth_R) + zpos;
                        }

                        _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                        if (_index >= 0 && _index < _max)
                        {
                            map[_index] = 1;
                        }
                    }
                }
            }*/










            _index = 0;
            float sqrtX = 0;
            float sqrtY = 0;
            float sqrtZ = 0;
            float dist = 0;

            xpos = 0;
            ypos = 0;
            zpos = 0;
            Vector3 current_spike_neighboor_pos;
            int _end = 0;

            sc_chunk_node _sc_node;
            List<sc_chunk_node> _sc_node_list;
            Vector3 last_iteration_location = Vector3.Zero;


            for (float i = 0; i < _spike_max_length * 1.0f;) //_spike_max_length //(float)Math.Round(_spike_max_length * 0.75f)
            {
                if (current_start_move_pos == null)
                {
                    break;
                }
                _sc_node_list = new List<sc_chunk_node>();

                for (int xx = -1; xx <= 1; xx++)
                {
                    for (int yy = -1; yy <= 1; yy++)
                    {
                        for (int zz = -1; zz <= 1; zz++)
                        {

                            xpos = (int)Math.Round(current_start_move_pos.Value.X + xx);
                            ypos = (int)Math.Round(current_start_move_pos.Value.Y + yy);
                            zpos = (int)Math.Round(current_start_move_pos.Value.Z + zz);
                            current_spike_neighboor_pos = new Vector3(xpos, ypos, zpos);

                            if (xx == 0 && yy == 0 && zz == 0)
                            {
                                continue;
                            }



                            _sc_node = new sc_chunk_node();
                            _sc_node._position = current_spike_neighboor_pos;

                            sqrtX = ((current_target_pos.X - current_spike_neighboor_pos.X) * (current_target_pos.X - current_spike_neighboor_pos.X));
                            sqrtY = ((current_target_pos.Y - current_spike_neighboor_pos.Y) * (current_target_pos.Y - current_spike_neighboor_pos.Y));
                            sqrtZ = ((current_target_pos.Z - current_spike_neighboor_pos.Z) * (current_target_pos.Z - current_spike_neighboor_pos.Z));
                            dist = (float)Math.Sqrt(sqrtX + sqrtY + sqrtZ);
                            _sc_node._distance_to_target = dist;

                            sqrtX = ((current_start_move_pos.Value.X - current_spike_neighboor_pos.X) * (current_start_move_pos.Value.X - current_spike_neighboor_pos.X));
                            sqrtY = ((current_start_move_pos.Value.Y - current_spike_neighboor_pos.Y) * (current_start_move_pos.Value.Y - current_spike_neighboor_pos.Y));
                            sqrtZ = ((current_start_move_pos.Value.Z - current_spike_neighboor_pos.Z) * (current_start_move_pos.Value.Z - current_spike_neighboor_pos.Z));
                            dist = (float)Math.Sqrt(sqrtX + sqrtY + sqrtZ);
                            _sc_node._distance_parent_node_to_this = dist;

                            _sc_node_list.Add(_sc_node);

                            if (xpos < 0)
                            {
                                xpos *= -1;
                                xpos = (ChunkWidth_R) + xpos;
                            }

                            if (ypos < 0)
                            {
                                ypos *= -1;
                                ypos = (ChunkHeight_R) + ypos;
                            }

                            if (zpos < 0)
                            {
                                zpos *= -1;
                                zpos = (ChunkDepth_R) + zpos;
                            }

                            _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                            /*if (current_spike_neighboor_pos.X == current_target_pos.X && current_spike_neighboor_pos.Y == current_target_pos.Y && current_spike_neighboor_pos.Z == current_target_pos.Z ||
                               current_start_move_pos.Value.X == current_target_pos.X && current_start_move_pos.Value.Y == current_target_pos.Y && current_start_move_pos.Value.Z == current_target_pos.Z)
                            {
                                //_end = 1;
                                // continue;
                                break;
                            }*/
                        }
                    }
                }

                current_start_move_pos = null;

                if (_sc_node_list.Count > 0)
                {
                    _sc_node_list.Sort((s1, s2) => s1._distance_to_target.CompareTo(s2._distance_to_target));

                    xpos = (int)Math.Round(_sc_node_list[0]._position.X);
                    ypos = (int)Math.Round(_sc_node_list[0]._position.Y);
                    zpos = (int)Math.Round(_sc_node_list[0]._position.Z);

                    current_start_move_pos = new Vector3(xpos, ypos, zpos);

                    if (xpos < 0)
                    {
                        xpos *= -1;
                        xpos = (ChunkWidth_R) + xpos;
                    }

                    if (ypos < 0)
                    {
                        ypos *= -1;
                        ypos = (ChunkHeight_R) + ypos;
                    }

                    if (zpos < 0)
                    {
                        zpos *= -1;
                        zpos = (ChunkDepth_R) + zpos;
                    }

                    //_index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                    //if (_index >= 0 && _index < _max)
                    //{
                    //    map[_index] = 1;
                    //}

                    for (int xx = -1; xx <= 1; xx++)
                    {
                        for (int yy = -1; yy <= 1; yy++)
                        {
                            for (int zz = -1; zz <= 1; zz++)
                            {
                                xpos = (int)Math.Round(current_start_move_pos.Value.X + xx);
                                ypos = (int)Math.Round(current_start_move_pos.Value.Y + yy);
                                zpos = (int)Math.Round(current_start_move_pos.Value.Z + zz);

                                current_spike_neighboor_pos = new Vector3(xpos, ypos, zpos);

                                if (xx == 0 && yy == 0 && zz == 0)
                                {
                                    continue;
                                }

                                if (xpos < 0)
                                {
                                    xpos *= -1;
                                    xpos = (ChunkWidth_R) + xpos;
                                }

                                if (ypos < 0)
                                {
                                    ypos *= -1;
                                    ypos = (ChunkHeight_R) + ypos;
                                }

                                if (zpos < 0)
                                {
                                    zpos *= -1;
                                    zpos = (ChunkDepth_R) + zpos;
                                }

                                _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                                if (_index >= 0 && _index < _max)
                                {
                                    map[_index] = 1;
                                }

                                /*if (current_spike_neighboor_pos.X == current_target_pos.X && current_spike_neighboor_pos.Y == current_target_pos.Y && current_spike_neighboor_pos.Z == current_target_pos.Z ||
                                    current_start_move_pos.Value.X == current_target_pos.X && current_start_move_pos.Value.Y == current_target_pos.Y && current_start_move_pos.Value.Z == current_target_pos.Z)
                                {
                                    //_end = 1;
                                    // continue;
                                    break;
                                }*/
                            }
                        }
                    }

                    i += (float)Math.Ceiling(_sc_node_list[0]._distance_parent_node_to_this);
                }
                else
                {
                    i += 1;
                }

            }


            //head of spike tree mushroom looking or brocoli looking.
            for (int xx = -6; xx <= 6; xx++)
            {
                for (int yy = -6; yy <= 6; yy++)
                {
                    for (int zz = -6; zz <= 6; zz++)
                    {
                        xpos = (int)Math.Round(current_start_move_pos.Value.X + xx);
                        ypos = (int)Math.Round(current_start_move_pos.Value.Y + yy);
                        zpos = (int)Math.Round(current_start_move_pos.Value.Z + zz);

                        float distance = Vector3.Distance(current_start_move_pos.Value, new Vector3(xpos, ypos, zpos));
                        if (distance < 5)
                        {
                            if (xx == 0 && yy == 0 && zz == 0)
                            {
                                continue;
                            }

                            if (xpos < 0)
                            {
                                xpos *= -1;
                                xpos = (ChunkWidth_R) + xpos;
                            }

                            if (ypos < 0)
                            {
                                ypos *= -1;
                                ypos = (ChunkHeight_R) + ypos;
                            }

                            if (zpos < 0)
                            {
                                zpos *= -1;
                                zpos = (ChunkDepth_R) + zpos;
                            }

                            _index = (int)Math.Round(xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos));

                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                            }
                        }
                    }
                }
            }
        }
        //DEVELOPED BY STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9, FIRST FOR TRYING TO DIG TUNNELS IN VOXELS AND THEN I JUST TRIED TO MAKE TUNNELS IN A SPHERE'S QUARTERS TO MAKE IT LOOK LIKE A COVID19 CELL.
        //DEVELOPED BY STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9, FIRST FOR TRYING TO DIG TUNNELS IN VOXELS AND THEN I JUST TRIED TO MAKE TUNNELS IN A SPHERE'S QUARTERS TO MAKE IT LOOK LIKE A COVID19 CELL.
        //DEVELOPED BY STEVE CHASSÉ AKA NINEKORN AKA NINE AKA 9, FIRST FOR TRYING TO DIG TUNNELS IN VOXELS AND THEN I JUST TRIED TO MAKE TUNNELS IN A SPHERE'S QUARTERS TO MAKE IT LOOK LIKE A COVID19 CELL.







        //shit. where did i find that.
        static internal int FastRand(int Seed, int MaxN)
        {
            Seed = (214013 * Seed + 2531011);
            return ((Seed >> 16) & 0x7FFF) % MaxN;
        }


        public List<Vector3> listofpickaxetipbytes = new List<Vector3>();


        float _vert_offset_x;
        float _vert_offset_y;
        float _vert_offset_z;

        //FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE AND HEAVILY MODIFIED BY STEVE CHASSÉ AKA NINEKORN/NINE/9
        //FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE AND HEAVILY MODIFIED BY STEVE CHASSÉ AKA NINEKORN/NINE/9
        //FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE AND HEAVILY MODIFIED BY STEVE CHASSÉ AKA NINEKORN/NINE/9
        public void startBuildingArray(Vector3 currentPosition, out SC_cube.DVertex[] vertexArray, out int[] triangleArray, out int[] mapper, float _planeSize, int minx, int miny, int minz, float diagmaxx, float diagmaxy, float diagmaxz, float diagminx, float diagminy, float diagminz, int _ChunkWidth_L, int _ChunkWidth_R, int _ChunkHeight_L, int _ChunkHeight_R, int _ChunkDepth_L, int _ChunkDepth_R, float maxDistance, float vert_offset_x, float vert_offset_y, float vert_offset_z, int voxel_type, int voxelbodyparttype, int somechunkpriminstanceikarmvoxelindex) //, out int vertexNum, out int indicesNum //Vector3 currentPosition, out Vector3[] vertexArray, out int[] indicesArray, 
        {
            float maxx = 2;
            float maxy = 8;
            float maxz = 2;

            _vert_offset_x = vert_offset_x;
            _vert_offset_y = vert_offset_y;
            _vert_offset_z = vert_offset_z;

            ChunkWidth_L = _ChunkWidth_L;
            ChunkWidth_R = _ChunkWidth_R;
            ChunkHeight_L = _ChunkHeight_L;
            ChunkHeight_R = _ChunkHeight_R;
            ChunkDepth_L = _ChunkDepth_L;
            ChunkDepth_R = _ChunkDepth_R;

            _pos = currentPosition;

            planeSize = _planeSize;

            //ChunkWidth = _width;
            //ChunkHeight = _height;
            //ChunkDepth = _depth;

            _max = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

            map = new int[(int)_max];

            int radius = 5;

            //Vector3 center = new Vector3(((ChunkWidth_L + ChunkWidth_R + 1)) * 0.5f, (ChunkHeight) * 0.5f, (ChunkDepth) * 0.5f);
            //var fastNoise = new FastNoise();

            seed = (int)Math.Floor(rand.NextDouble() * (currentPosition.X - 1) + 1);
            // not working as i need my other program called 1stVersion for instances to also incorporate different chunk seeds.                                                                               
            //and that other program might be long to incorporate.

            //create sphere
            for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x++)
            {
                for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y++)
                {
                    for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z++)
                    {
                        float posX = (x);
                        float posY = (y);
                        float posZ = (z);

                        var xx = x;
                        var yy = y;
                        var zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = (ChunkWidth_R) + xx;
                        }
                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = (ChunkHeight_R) + yy;
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = (ChunkDepth_R) + zz;
                        }

                        int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                        Vector3 position = new Vector3(posX, posY, posZ);

                        float distance = 0;
                        //float distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz); //11.31415926535f

                        if (voxel_type == 0)
                        {
                            distance = Vector3.Distance(position, currentPosition);
                            if (distance < ((ChunkWidth_L) * 0.25f)) // 0.35f
                            {
                                if (_index >= 0 && _index < _max)
                                {
                                    map[_index] = 1;
                                }
                            }

                        }
                        else if (voxel_type == 1)
                        {
                            //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                            distance = sc_maths.sc_check_distance_node_3d(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);
                            if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                            {
                                if (_index >= 0 && _index < _max)
                                {
                                    map[_index] = 1;
                                }
                            }
                        }
                        else if (voxel_type == 2)
                        {

                            //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                            distance = sc_maths.sc_check_distance_node_3d(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);
                            if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                            {
                                if (_index >= 0 && _index < _max)
                                {
                                    map[_index] = 1;
                                }
                            }

                            //OUT OF THE BOX - WRONG DISTANCE SO IT GOES FULL SIZE SO YOU CAN CHECK AND COMPARED WITH WHAT YOU'VE GOT IN minx/miny/minz/maxx/maxy/maxz/diagminx/diagminy/diagminz/diagmaxx/diagmaxy/diagmaxz/chunkwidthl/chunkwidthr/chunkheightl/chunkheighr/chunkdepthl/chunkdepthr
                            /*distance = Vector3.Distance(position, currentPosition);
                            if (distance < ((ChunkWidth_L) * maxDistance)) // 0.35f
                            {
                                if (_index >= 0 && _index < _max)
                                {
                                    map[_index] = 1;
                                }
                            }*/




                        }
                        else if (voxel_type == 3)
                        {
                            //distance = sc_maths.sc_check_distance_node_3d_geometry(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, maxx, maxy, maxz);
                            distance = sc_maths.sc_check_distance_node_3d(currentPosition, new Vector3(posX, posY, posZ), minx, miny, minz, diagmaxx, diagmaxy, diagmaxz, diagminx, diagminy, diagminz);

                            if (distance < ((ChunkWidth_L) * maxDistance) && x >= -maxx && x < maxx && z >= -maxz && z < maxz) // 0.35f
                            {
                                if (_index >= 0 && _index < _max)
                                {
                                    map[_index] = 1;
                                }
                            }

                            if (y >= -ChunkHeight_L && y < -ChunkHeight_L + maxy) // 0.35f // distance < ((ChunkHeight_R) * maxDistance) && 
                            {
                                if (_index >= 0 && _index < _max)
                                {

                                    map[_index] = 1;
                                }
                            }

                            if (y >= -ChunkHeight_L + maxy && y < maxy) // 0.35f // distance < ((ChunkHeight_R) * maxDistance) && 
                            {
                                if (_index >= 0 && _index < _max)
                                {
                                    if (y >= -ChunkHeight_L + maxy && y < -ChunkHeight_L + (maxy * 2) && z < -ChunkDepth_L + (maxz * 2))// 0.35f // distance < ((ChunkHeight_R) * maxDistance) && 
                                    {
                                        map[_index] = 2;

                                        listofpickaxetipbytes.Add(position);


                                        float _decimal_for_random = 1.0f;
                                        //float max_spike_length_for_random = 0.85f;

                                        float minX = 1;// ((ChunkWidth_L + ChunkWidth_R + 1) * 0.55f);
                                        float maxX = ((ChunkWidth_L) * 0.975f);

                                        /*if (posX < 0)
                                        {
                                            minX = minX * -1;
                                            maxX = maxX * -1;
                                        }*/

                                        float minY = 1;// ((ChunkHeight_L + ChunkHeight_R + 1) * 0.55f);
                                        float maxY = ((ChunkHeight_L) * 0.975f);
                                        /*if (posY < 0)
                                        {
                                            minY = minY * -1;
                                            maxY = maxY * -1;
                                        }*/

                                        float minZ = 1;// ((ChunkDepth_L + ChunkDepth_R + 1) * 0.55f);
                                        float maxZ = ((ChunkDepth_L) * 0.975f);

                                        /*if (posZ < 0)
                                        {
                                            minZ = minZ * -1;
                                            maxZ = maxZ * -1;
                                        }*/

                                        //int maxDist_width = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * max_spike_length_for_random));
                                        int randX = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minX), (int)Math.Round(maxX), _decimal_for_random, 0,0)));
                                        //int maxDist_height = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * max_spike_length_for_random));
                                        int randY = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minY), (int)Math.Round(maxY), _decimal_for_random, 0, 0)));
                                        //int maxDist_depth = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * max_spike_length_for_random));
                                        int randZ = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minZ), (int)Math.Round(maxZ), _decimal_for_random, 0, 0)));

                                        float _someMin = 0.35f;
                                        if (posX < 0)
                                        {
                                            randX *= -1;
                                            if ((randX) >= -(ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                            {
                                                randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin)) * -1;
                                            }
                                        }
                                        else
                                        {
                                            if ((randX) < (ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                            {
                                                randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin));
                                            }
                                        }

                                        if (posY < 0)
                                        {
                                            randY *= -1;
                                            if ((randY) >= -(ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                            {
                                                randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin)) * -1;
                                            }
                                        }
                                        else
                                        {
                                            if ((randY) < (ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                            {
                                                randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin));
                                            }
                                        }


                                        if (posZ < 0)
                                        {
                                            randZ *= -1;
                                            if ((randZ) >= -(ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                            {
                                                randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin)) * -1;
                                            }
                                        }
                                        else
                                        {
                                            if ((randZ) < (ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                            {
                                                randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin));
                                            }
                                        }

                                        /*if(randX < 0 || randY < 0|| randZ < 0)
                                        {
                                            Console.WriteLine(randX + " _ " + randY + "  " + randZ);
                                        }*/


                                        /*if (_swtch_spike_02 == 0)
                                        {
                                            CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);

                                            _swtch_spike_02 = 1;
                                        }*/

                                    }
                                }
                            }



                        }







                        //Vector3 position1 = currentPosition;
                        //float distance1 = Vector3.Distance(position1, center);
                        //_current_visual_distance_spike_glyco_protein_covid19_min = ((ChunkWidth_L + ChunkWidth_R) * 0.35f);
                        //_current_visual_distance_spike_glyco_protein_covid19_max = ((ChunkWidth_L + ChunkWidth_R) - 1); // increase size of chunk for longer glycoprotein spike


                        /*if (randX < 0)
                        {
                            randX -= 100;
                        }
                        else
                        {
                            randX += 100;
                        }


                        if (randY < 0)
                        {
                            randY -= 100;
                        }
                        else
                        {
                            randY += 100;
                        }

                        if (randZ < 0)
                        0   .


                        {
                            randZ -= 100;
                        }
                        else
                        {
                            randZ += 100;
                        }*/



                        if (distance >= ((ChunkWidth_L) * 0.35f) && distance < ((ChunkWidth_L) * 0.975f) && voxel_type == 0 || distance >= ((ChunkWidth_L) * 0.35f) && distance < ((ChunkWidth_L) * 0.975f) && voxel_type == 1) //0.35f 0.45f
                        {
                            float _decimal_for_random = 1.0f;
                            //float max_spike_length_for_random = 0.85f;

                            float minX = 1;// ((ChunkWidth_L + ChunkWidth_R + 1) * 0.55f);
                            float maxX = ((ChunkWidth_L) * 0.975f);

                            /*if (posX < 0)
                            {
                                minX = minX * -1;
                                maxX = maxX * -1;
                            }*/

                            float minY = 1;// ((ChunkHeight_L + ChunkHeight_R + 1) * 0.55f);
                            float maxY = ((ChunkHeight_L) * 0.975f);
                            /*if (posY < 0)
                            {
                                minY = minY * -1;
                                maxY = maxY * -1;
                            }*/

                            float minZ = 1;// ((ChunkDepth_L + ChunkDepth_R + 1) * 0.55f);
                            float maxZ = ((ChunkDepth_L) * 0.975f);

                            /*if (posZ < 0)
                            {
                                minZ = minZ * -1;
                                maxZ = maxZ * -1;
                            }*/

                            //int maxDist_width = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * max_spike_length_for_random));
                            int randX = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minX), (int)Math.Round(maxX), _decimal_for_random, 0, 0)));
                            //int maxDist_height = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * max_spike_length_for_random));
                            int randY = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minY), (int)Math.Round(maxY), _decimal_for_random, 0, 0)));
                            //int maxDist_depth = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * max_spike_length_for_random));
                            int randZ = (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minZ), (int)Math.Round(maxZ), _decimal_for_random, 0, 0)));

                            float _someMin = 0.35f;
                            if (posX < 0)
                            {
                                randX *= -1;
                                if ((randX) >= -(ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                {
                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randX) < (ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                {
                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin));
                                }
                            }

                            if (posY < 0)
                            {
                                randY *= -1;
                                if ((randY) >= -(ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                {
                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randY) < (ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                {
                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin));
                                }
                            }


                            if (posZ < 0)
                            {
                                randZ *= -1;
                                if ((randZ) >= -(ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                {
                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randZ) < (ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                {
                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin));
                                }
                            }

                            /*if(randX < 0 || randY < 0|| randZ < 0)
                            {
                                Console.WriteLine(randX + " _ " + randY + "  " + randZ);
                            }*/




                            if (voxel_type == 0)
                            {

                            }
                            else if (voxel_type == 1)
                            {

                                if (posY < 0 && posX < 0 && posZ < 0)
                                {
                                    if (_swtch_spike_00 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_00 = 1;
                                    }
                                }
                                else if (posY >= 0 && posX >= 0 && posZ >= 0)
                                {
                                    if (_swtch_spike_01 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_01 = 1;
                                    }
                                }
                                else if (posY >= 0 && posX < 0 && posZ >= 0)
                                {
                                    if (_swtch_spike_02 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_02 = 1;
                                    }
                                }
                                else if (posY >= 0 && posX >= 0 && posZ < 0)
                                {
                                    if (_swtch_spike_03 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_03 = 1;
                                    }
                                }
                                else if (posY >= 0 && posX < 0 && posZ < 0)
                                {
                                    if (_swtch_spike_04 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_04 = 1;
                                    }
                                }
                                else if (posY < 0 && posX >= 0 && posZ < 0)
                                {
                                    if (_swtch_spike_05 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_05 = 1;
                                    }

                                }
                                else if (posY < 0 && posX >= 0 && posZ >= 0)
                                {
                                    if (_swtch_spike_06 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_06 = 1;
                                    }
                                }
                                else if (posY < 0 && posX < 0 && posZ >= 0)
                                {
                                    if (_swtch_spike_07 == 0)
                                    {
                                        CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                        _swtch_spike_07 = 1;
                                    }
                                }
                            }
                            else if (voxel_type == 2)
                            {

                            }
                        }
                        else if (voxel_type == 3) //distance >= ((ChunkWidth_L) * 0.35f) && distance < ((ChunkWidth_L) * 0.975f)  && 
                        {
                            float _decimal_for_random = 1.0f;
                            //float max_spike_length_for_random = 0.85f;

                            float minX = 1;// ((ChunkWidth_L + ChunkWidth_R + 1) * 0.55f);
                            float maxX = ((ChunkWidth_L) * 0.975f);

                            /*if (posX < 0)
                            {
                                minX = minX * -1;
                                maxX = maxX * -1;
                            }*/

                            float minY = 1;// ((ChunkHeight_L + ChunkHeight_R + 1) * 0.55f);
                            float maxY = ((ChunkHeight_L) * 0.975f);
                            /*if (posY < 0)
                            {
                                minY = minY * -1;
                                maxY = maxY * -1;
                            }*/

                            float minZ = 1;// ((ChunkDepth_L + ChunkDepth_R + 1) * 0.55f);
                            float maxZ = ((ChunkDepth_L) * 0.975f);

                            /*if (posZ < 0)
                            {
                                minZ = minZ * -1;
                                maxZ = maxZ * -1;
                            }*/

                            //int maxDist_width = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * max_spike_length_for_random));
                            int randX = 0;// (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minX), (int)Math.Round(maxX), _decimal_for_random, 0)));
                            //int maxDist_height = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * max_spike_length_for_random));
                            int randY = 0;// (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minY), (int)Math.Round(maxY), _decimal_for_random, 0)));
                            //int maxDist_depth = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * max_spike_length_for_random));
                            int randZ = 0;// (int)(Math.Round(sc_maths.getSomeRandNumThousandDecimal((int)Math.Round(minZ), (int)Math.Round(maxZ), _decimal_for_random, 0)));

                            float _someMin = 0.35f;
                            /*if (posX < 0)
                            {
                                randX *= -1;
                                if ((randX) >= -(ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                {
                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randX) < (ChunkWidth_L + ChunkWidth_R + 1) * _someMin)
                                {
                                    randX = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * _someMin));
                                }
                            }

                            if (posY < 0)
                            {
                                randY *= -1;
                                if ((randY) >= -(ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                {
                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randY) < (ChunkHeight_L + ChunkHeight_R + 1) * _someMin)
                                {
                                    randY = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * _someMin));
                                }
                            }


                            if (posZ < 0)
                            {
                                randZ *= -1;
                                if ((randZ) >= -(ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                {
                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin)) * -1;
                                }
                            }
                            else
                            {
                                if ((randZ) < (ChunkDepth_L + ChunkDepth_R + 1) * _someMin)
                                {
                                    randZ = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * _someMin));
                                }
                            }*/

                            /*if(randX < 0 || randY < 0|| randZ < 0)
                            {
                                Console.WriteLine(randX + " _ " + randY + "  " + randZ);
                            }*/


                            if (posY < 0 && posX < 0 && posZ < 0)
                            {
                                if (_swtch_spike_00 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_00 = 1;
                                }
                            }
                            /*else if (posY >= 0 && posX >= 0 && posZ >= 0)
                            {
                                if (_swtch_spike_01 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_01 = 1;
                                }
                            }
                            else if (posY >= 0 && posX < 0 && posZ >= 0)
                            {
                                if (_swtch_spike_02 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_02 = 1;
                                }
                            }
                            else if (posY >= 0 && posX >= 0 && posZ < 0)
                            {
                                if (_swtch_spike_03 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_03 = 1;
                                }
                            }
                            else if (posY >= 0 && posX < 0 && posZ < 0)
                            {
                                if (_swtch_spike_04 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_04 = 1;
                                }
                            }
                            else if (posY < 0 && posX >= 0 && posZ < 0)
                            {
                                if (_swtch_spike_05 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_05 = 1;
                                }

                            }
                            else if (posY < 0 && posX >= 0 && posZ >= 0)
                            {
                                if (_swtch_spike_06 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_06 = 1;
                                }
                            }
                            else if (posY < 0 && posX < 0 && posZ >= 0)
                            {
                                if (_swtch_spike_07 == 0)
                                {
                                    CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                                    _swtch_spike_07 = 1;
                                }
                            }*/
                        }

                        /*if (_current_counter_for_adding_spike_glyco_protein_covid19 >= (_max) * 0.05f)
                        {
                            float _decimal = 1.0f;
                            float max_spike_length = 0.85f;

                            int maxDist_width = (int)(Math.Round((ChunkWidth_L + ChunkWidth_R + 1) * max_spike_length));
                            int randX = (int)(Math.Round(getSomeRandNumThousandDecimal(maxDist_width, _decimal)));

                            int maxDist_height = (int)(Math.Round((ChunkHeight_L + ChunkHeight_R + 1) * max_spike_length));
                            int randY = (int)(Math.Round(getSomeRandNumThousandDecimal(maxDist_height, _decimal)));

                            int maxDist_depth = (int)(Math.Round((ChunkDepth_L + ChunkDepth_R + 1) * max_spike_length));
                            int randZ = (int)(Math.Round(getSomeRandNumThousandDecimal(maxDist_depth, _decimal)));

                            CreateSpikeGlycoProteinCOVID19(currentPosition, randX, randY, randZ);
                            _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                        }*/

















                        /*randomX = 1;
                        randomY = 1;
                        randomZ = 1;

                        int isX = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                        //rand = new Random();
                        int isY = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                        //rand = new Random();
                        int isZ = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                        //rand = new Random();
                        //decide if possible or negative

                        if (isX == 0)
                        {
                            isX = 1;
                            randomX *= isX;
                        }
                        else //if (isX == 1)
                        {
                            isX = -1;
                            randomX *= isX;
                        }

                        if (isY == 0)
                        {
                            isY = 1;
                            randomY *= isY;
                        }
                        else //if (isY == 1)
                        {
                            isY = -1;
                            randomY *= isY;
                        }

                        if (isZ == 0)
                        {
                            isZ = 1;
                            randomZ *= isZ;
                        }
                        else //if (isZ == 1)
                        {
                            isZ = -1;
                            randomZ *= isZ;
                        }*/










                        /*var data_one_x = (int)(Math.Round(currentPosition.X - ((ChunkWidth_L + ChunkWidth_R + 1) * 0.35f)));
                        var data_two_x = (int)(Math.Round(currentPosition.X + ((ChunkWidth_L + ChunkWidth_R + 1) * 0.35f)));
                        //var randX = FastRand(seed, data_two_x);
                        var randX = rand.Next(data_one_x, data_two_x);

                        var data_one_y = (int)(Math.Round(currentPosition.Y - ((ChunkHeight_L + ChunkHeight_R + 1) * 0.35f)));
                        var data_two_y = (int)(Math.Round(currentPosition.Y + ((ChunkHeight_L + ChunkHeight_R + 1) * 0.35f)));
                        //var randY = FastRand(seed, data_two_y);
                        var randY = rand.Next(data_one_y, data_two_y);

                        var data_one_z = (int)(Math.Round(currentPosition.Z - ((ChunkDepth_L + ChunkDepth_R + 1) * 0.35f)));
                        var data_two_z = (int)(Math.Round(currentPosition.Z + ((ChunkDepth_L + ChunkDepth_R + 1) * 0.35f)));
                        //var randZ = FastRand(seed, data_two_z);
                        var randZ = rand.Next(data_one_z, data_two_z);
                        */

                        /*if (randX < 0)
                        {
                            randX *= -1;
                            randX = (ChunkWidth_R) + randX;
                        }
                        if (randY < 0)
                        {
                            randY *= -1;
                            randY = (ChunkHeight_R) + randY;
                        }
                        if (randZ < 0)
                        {
                            randZ *= -1;
                            randZ = (ChunkDepth_R) + randZ;
                        }

                        _index = randX + (ChunkWidth_L + ChunkWidth_R + 1) * (randY + (ChunkHeight_L + ChunkHeight_R + 1) * randZ);
                        if (_index >= 0 && _index < _max)
                        {
                            map[_index] = 1;
                        }*/

                        /*if (_current_counter_for_adding_spike_glyco_protein_covid19 >= (_max) * 0.05f)
                        {
                            _index = randX + (ChunkWidth_L + ChunkWidth_R + 1) * (randY + (ChunkHeight_L + ChunkHeight_R + 1) * randZ);

                            if (_index >= 0 && _index < _max)
                            {
                                CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);
                            }

                            //SC_pathfind_assets.sc_pathfind_grid grid = new SC_pathfind_assets.sc_pathfind_grid();
                            //grid.sc_init_pathfind_grid(ChunkWidth_L, ChunkWidth_R, ChunkHeight_L, ChunkHeight_R, ChunkDepth_L, ChunkDepth_R); // working but i didnt convert the rest yet.
                            //i just got to figure out how i planified the arrays in javascript. i dont know why i am so fucking stressed as this is so fucking easy to do.
                            /*if (_index >= 0 && _index < _max)
                            {
                                //MessageBox((IntPtr)0,  "test", "Oculus error", 0);
                                CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);
                            }
                            _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                        }
                        }*/





                        //int n = number.Next(0, 1000);


                        /*if (_current_counter_for_adding_spike_glyco_protein_covid19 >= (_max) * 0.05f)
                        {
                            _index = randX + (ChunkWidth_L + ChunkWidth_R + 1) * (randY + (ChunkHeight_L + ChunkHeight_R + 1) * randZ);

                            if (_index >= 0 && _index < _max)
                            {
                                map[_index] = 1;
                                //MessageBox((IntPtr)0,  "test", "Oculus error", 0);
                                //CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);
                            }

                            //SC_pathfind_assets.sc_pathfind_grid grid = new SC_pathfind_assets.sc_pathfind_grid();
                            //grid.sc_init_pathfind_grid(ChunkWidth_L, ChunkWidth_R, ChunkHeight_L, ChunkHeight_R, ChunkDepth_L, ChunkDepth_R); // working but i didnt convert the rest yet.
                            //i just got to figure out how i planified the arrays in javascript. i dont know why i am so fucking stressed as this is so fucking easy to do.
                            /*if (_index >= 0 && _index < _max)
                            {
                                //MessageBox((IntPtr)0,  "test", "Oculus error", 0);
                                CreateSpikeGlycoProteinCOVID19(currentPosition, x, y, z);
                            }
                            _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                        }*/


                        //_index = randX + (ChunkWidth_L + ChunkWidth_R + 1) * (randY + (ChunkHeight_L + ChunkHeight_R + 1) * randZ);






                        /*if (posY < 0 && posX < 0 && posZ < 0)
                        {
                            if (_swtch_spike_00 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_00 = 1;
                            }
                        }
                        else if (posY >= 0 && posX >= 0 && posZ >= 0)
                        {
                            if (_swtch_spike_01 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_01 = 1;
                            }
                        }
                        else if (posY >= 0 && posX < 0 && posZ >= 0)
                        {
                            if (_swtch_spike_02 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_02 = 1;
                            }
                        }
                        else if (posY >= 0 && posX >= 0 && posZ < 0)
                        {
                            if (_swtch_spike_03 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_03 = 1;
                            }
                        }
                        else if (posY >= 0 && posX < 0 && posZ < 0)
                        {
                            if (_swtch_spike_04 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_04 = 1;
                            }
                        }
                        else if (posY < 0 && posX >= 0 && posZ < 0)
                        {
                            if (_swtch_spike_05 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_05 = 1;
                            }

                        }
                        else if (posY < 0 && posX >= 0 && posZ >= 0)
                        {
                            if (_swtch_spike_06 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_06 = 1;
                            }
                        }
                        else if (posY < 0 && posX < 0 && posZ >= 0)
                        {
                            if (_swtch_spike_07 == 0)
                            {
                                CreateSpikeGlycoProteinCOVID19(center, randX, randY, randZ);
                                //CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ));
                                _swtch_spike_07 = 1;
                            }
                        }*/

                        /*else if (distance >= (ChunkWidth * 0.40f) && distance < (ChunkWidth * 0.425f))
                        {
                            //map[_index] = 1; // to see external sheet/membrane
                            //map[_index] = 0;
                        }
                        else if (distance >= (ChunkWidth * 0.425f) && distance < (ChunkWidth))
                        {
                            //map[_index] = 0;
                        }
                        else
                        { 
                           //map[_index] = 0;
                        }*/

                        _current_counter_for_adding_spike_glyco_protein_covid19++;
                    }
                }
            }




            /*
            for (int i = 0;i < 50;i++)
            {
                randomX = 1;
                randomY = 1;
                randomZ = 1;

                int isX = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                int isY = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                int isZ = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);

                //decide if possible or negative
                if (isX == 0)
                {

                }
                else if (isX == 1)
                {
                    isX = -1;
                    randomX *= isX;
                }

                if (isY == 0)
                {

                }
                else if (isY == 1)
                {
                    isY = -1;
                    randomY *= isY;
                }

                if (isZ == 0)
                {

                }
                else if (isZ == 1)
                {
                    isZ = -1;
                    randomZ *= isZ;
                }


                //randomX = randomX * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isX));
                //randomY = randomY * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isY));
                //randomZ = randomZ * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isZ));

                randomX = randomX * (_current_visual_distance_spike_glyco_protein_covid19_min);
                randomY = randomZ * (_current_visual_distance_spike_glyco_protein_covid19_min);
                randomZ = randomY * (_current_visual_distance_spike_glyco_protein_covid19_min);


                float posX = (randomX) + currentPosition.X;
                float posY = (randomY) + currentPosition.Y;
                float posZ = (randomZ) + currentPosition.Z;

                Vector3 position = new Vector3(posX, posY, posZ);

                float distance = Vector3.Distance(position, center);

                if (distance >= (ChunkWidth * 0.25f) && distance < (ChunkWidth))
                {
                    //if (_swtch == 0)
                    {
                        if (_current_counter_for_adding_spike_glyco_protein_covid19 >= 0)
                        {
                            CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ), _current_visual_distance_spike_glyco_protein_covid19_min, _current_visual_distance_spike_glyco_protein_covid19_max);
                            _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                            //_swtch = 1;
                        }
                    }
                }
                else
                {
                    //map[_index] = 0;
                    //_swtch = 0;
                }
            }*/
























            /*

            float max_spikes_percent = 0.1f;

            for (int i = 0;i < 1; i++) //(int)Math.Round(_max * max_spikes_percent)
            {
                int _loop_counter = 0;
            _loop:

                if (_loop_counter < 10)
                {
                    randomX = 1;
                    randomY = 1;
                    randomZ = 1;

                    int isX = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                    int isY = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);
                    int isZ = (int)Math.Floor(rand.NextDouble() * (2 - 0) + 0);

                    //decide if possible or negative
                    if (isX == 0)
                    {

                    }
                    else if (isX == 1)
                    {
                        isX = -1;
                        randomX *= isX;
                    }

                    if (isY == 0)
                    {

                    }
                    else if (isY == 1)
                    {
                        isY = -1;
                        randomY *= isY;
                    }

                    if (isZ == 0)
                    {

                    }
                    else if (isZ == 1)
                    {
                        isZ = -1;
                        randomZ *= isZ;
                    }
      

                    randomX = randomX * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isX));
                    randomY = randomY * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isY));
                    randomZ = randomZ * (int)(Math.Floor(rand.NextDouble() * (_current_visual_distance_spike_glyco_protein_covid19_max - 1) + _current_visual_distance_spike_glyco_protein_covid19_min + isZ));

                    float posX = (randomX * planeSize) + currentPosition.X;
                    float posY = (randomY * planeSize) + currentPosition.Y;
                    float posZ = (randomZ * planeSize) + currentPosition.Z;

                    Vector3 position = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(position, center);

                    if (distance > (ChunkWidth * 0.35f) * planeSize && distance < (ChunkWidth) * planeSize)
                    {
                        //if (_swtch == 0)
                        {
                            if (_current_counter_for_adding_spike_glyco_protein_covid19 >= 0) // maxframe is ChunkWidth *ChunkHeight * ChunkDepth
                            {
                                CreateSpikeGlycoProteinCOVID19(center, (int)Math.Round(randomX), (int)Math.Round(randomY), (int)Math.Round(randomZ), _current_visual_distance_spike_glyco_protein_covid19_min, _current_visual_distance_spike_glyco_protein_covid19_max);
                                _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                                //_swtch = 1;
                            }
                        }
                    }
                    else
                    {
                        //map[_index] = 0;
                        //_swtch = 0;
                        _loop_counter++;
                        goto _loop;
                    }
                }
            }*/








            /*
            //create protruding spikes
            for (int x = 0; x < ChunkWidth; x++)
            {
                float noiseX = Math.Abs(((float)(x * planeSize + currentPosition.X + seed) / _detailScale) * _heightScale);

                for (int y = 0; y < ChunkHeight; y++)
                {
                    float noiseY = Math.Abs(((float)(y * planeSize + currentPosition.Y + seed) / _detailScale) * _heightScale);

                    for (int z = 0; z < ChunkDepth; z++)
                    {
                        float noiseZ = Math.Abs(((float)(z * planeSize + currentPosition.Z + seed) / _detailScale) * _heightScale);

                        float posX = (x * planeSize) + currentPosition.X;
                        float posY = (y * planeSize) + currentPosition.Y;
                        float posZ = (z * planeSize) + currentPosition.Z;

                        Vector3 position = new Vector3(posX, posY, posZ);

                        float distance = Vector3.Distance(position, center);

                        //Vector3 position1 = currentPosition;
                        //float distance1 = Vector3.Distance(position1, center);

                        posX -= center.X;
                        posY -= center.Y;
                        posZ -= center.Z;

                        _current_visual_distance_spike_glyco_protein_covid19_min = (ChunkWidth * 0.35f) * planeSize;
                        _current_visual_distance_spike_glyco_protein_covid19_max = (ChunkWidth - 1) * planeSize; // increase size of chunk for longer glycoproteinthin

                        int _index = x + ChunkWidth * (y + ChunkHeight * z);

                        if (distance > (ChunkWidth * 0.35f) * planeSize && distance < (ChunkWidth) * planeSize)
                        {
                            if (_swtch == 0)
                            {
                                if (_current_counter_for_adding_spike_glyco_protein_covid19 >= 0) // maxframe is ChunkWidth *ChunkHeight * ChunkDepth
                                {
                                    CreateSpikeGlycoProteinCOVID19(center, x, y, z, _current_visual_distance_spike_glyco_protein_covid19_min, _current_visual_distance_spike_glyco_protein_covid19_max);

                                    _current_counter_for_adding_spike_glyco_protein_covid19 = 0;
                                    _swtch = 1;
                                }
                            }
                        }
                        else
                        {
                            //map[_index] = 0;
                        }
                        _current_counter_for_adding_spike_glyco_protein_covid19++;
                    }
                }
            }*/

            Regenerate(currentPosition);

            vertexArray = listOfVerts.ToArray();
            triangleArray = listOfTriangleIndices.ToArray();
            mapper = map;
        }


        //REGENERATE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE, IS THAT I LOOP IN NEGATIVES BUT MAKE THE INDEX POSITIVES AND I ONLY USE FLAT ARRAYS.
        //REGENERATE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE, IS THAT I LOOP IN NEGATIVES BUT MAKE THE INDEX POSITIVES AND I ONLY USE FLAT ARRAYS.
        //REGENERATE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE, IS THAT I LOOP IN NEGATIVES BUT MAKE THE INDEX POSITIVES AND I ONLY USE FLAT ARRAYS.
        public void Regenerate(Vector3 currentPosition)
        {
            for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x++)
            {
                for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y++)
                {
                    for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z++)
                    {
                        var xx = x;
                        var yy = y;
                        var zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = (ChunkWidth_R) + xx;
                        }
                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = (ChunkHeight_R) + yy;
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = (ChunkDepth_R) + zz;
                        }

                        int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                        block = map[_index];

                        if (block == 0) continue;
                        {
                            DrawBrick(x, y, z, xx, yy, zz);
                        }
                    }
                }
            }
        }
        //REGENERATE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE, IS THAT I LOOP IN NEGATIVES BUT MAKE THE INDEX POSITIVES AND I ONLY USE FLAT ARRAYS.
        //REGENERATE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE, IS THAT I LOOP IN NEGATIVES BUT MAKE THE INDEX POSITIVES AND I ONLY USE FLAT ARRAYS.
        //REGENERATE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE, IS THAT I LOOP IN NEGATIVES BUT MAKE THE INDEX POSITIVES AND I ONLY USE FLAT ARRAYS.






        //DRAWBRICK FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS ONLY THE PLANESIZE FOR VOXEL MINIATURIZATION AND THE VERTEX OFFSETS IN THIS VERSION OF IT BECAUSE I DID OTHER ONES TOO. BUT I ALSO ADD OTHER THINGS FOR LOD IN MY UNITY3D PROJECTS ALSO BASED ON THE SAME CRAIG PERKOS DRAWBRICK FUNCTION
        //DRAWBRICK FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS ONLY THE PLANESIZE FOR VOXEL MINIATURIZATION AND THE VERTEX OFFSETS IN THIS VERSION OF IT BECAUSE I DID OTHER ONES TOO. BUT I ALSO ADD OTHER THINGS FOR LOD IN MY UNITY3D PROJECTS ALSO BASED ON THE SAME CRAIG PERKOS DRAWBRICK FUNCTION
        //DRAWBRICK FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS ONLY THE PLANESIZE FOR VOXEL MINIATURIZATION AND THE VERTEX OFFSETS IN THIS VERSION OF IT BECAUSE I DID OTHER ONES TOO. BUT I ALSO ADD OTHER THINGS FOR LOD IN MY UNITY3D PROJECTS ALSO BASED ON THE SAME CRAIG PERKOS DRAWBRICK FUNCTION
        public void DrawBrick(int x, int y, int z, int xx, int yy, int zz)
        {

            Vector3 start = new Vector3((x + _vert_offset_x) * planeSize, (y + _vert_offset_y) * planeSize, (z + _vert_offset_z) * planeSize);
            //start.X -= ((ChunkWidth_L + ChunkWidth_R + 1) * planeSize) * 0.5f;
            //start.Y -= ((ChunkHeight_L + ChunkHeight_R + 1) * planeSize) * 0.5f;
            //start.Z -= ((ChunkDepth_L + ChunkDepth_R + 1) * planeSize) * 0.5f;

            Vector3 offset1, offset2;


            //TOPFACE
            if (IsTransparent(x, y + 1, z))
            {
                offset1 = forward * planeSize;
                offset2 = right * planeSize;
                createTopFace(start + up * planeSize, offset1, offset2);
                //MessageBox((IntPtr)0, "createTopFace", "Oculus error", 0);
            }
            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z))
            {
                offset1 = right * planeSize;
                offset2 = forward * planeSize;
                createBottomFace(start, offset1, offset2);
                //MessageBox((IntPtr)0, "createBottomFace", "Oculus error", 0);
            }

            //LEFTFACE
            if (IsTransparent(x - 1, y, z))
            {
                offset1 = back * planeSize;
                offset2 = down * planeSize;
                createleftFace(start + up * planeSize + forward * planeSize, offset1, offset2);
            }

            //RIGHTFACE
            if (IsTransparent(x + 1, y, z))
            {
                offset1 = up * planeSize;
                offset2 = forward * planeSize;
                createRightFace(start + right * planeSize, offset1, offset2);
            }
            //FRONTFACE
            if (IsTransparent(x, y, z - 1))
            {
                offset1 = left * planeSize;
                offset2 = up * planeSize;
                createFrontFace(start + right * planeSize, offset1, offset2);
            }
            //BACKFACE
            if (IsTransparent(x, y, z + 1))
            {
                offset1 = right * planeSize;
                offset2 = up * planeSize;
                createBackFace(start + forward * planeSize, offset1, offset2);
            }

        }
        //DRAWBRICK FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS ONLY THE PLANESIZE FOR VOXEL MINIATURIZATION AND THE VERTEX OFFSETS IN THIS VERSION OF IT BECAUSE I DID OTHER ONES TOO. BUT I ALSO ADD OTHER THINGS FOR LOD IN MY UNITY3D PROJECTS ALSO BASED ON THE SAME CRAIG PERKOS DRAWBRICK FUNCTION
        //DRAWBRICK FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS ONLY THE PLANESIZE FOR VOXEL MINIATURIZATION AND THE VERTEX OFFSETS IN THIS VERSION OF IT BECAUSE I DID OTHER ONES TOO. BUT I ALSO ADD OTHER THINGS FOR LOD IN MY UNITY3D PROJECTS ALSO BASED ON THE SAME CRAIG PERKOS DRAWBRICK FUNCTION
        //DRAWBRICK FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS ONLY THE PLANESIZE FOR VOXEL MINIATURIZATION AND THE VERTEX OFFSETS IN THIS VERSION OF IT BECAUSE I DID OTHER ONES TOO. BUT I ALSO ADD OTHER THINGS FOR LOD IN MY UNITY3D PROJECTS ALSO BASED ON THE SAME CRAIG PERKOS DRAWBRICK FUNCTION


        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        private void createTopFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, 0),

            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, 0),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, 0),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1f, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, 0),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.


        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        private void createBottomFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;
            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0f, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 1, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0f, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 1, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                normal = new Vector3(0, 1, -1),
                color = new Vector4(colorX, colorY, colorZ, 1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 1, -1),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.

        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        private void createFrontFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, 0),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, 0),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, 0),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, 0),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);

        }
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.


        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        private void createBackFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 0, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 0, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 0, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(0, 0, -1),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);

        }
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.




        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        private void createRightFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1f),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 0, -1),
            });

            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.


        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        private void createleftFace(Vector3 start, Vector3 offset1, Vector3 offset2)
        {
            int index = listOfVerts.Count;

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start,
                texture = new Vector2(0, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, -1),
            });

            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1,
                texture = new Vector2(0, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset2,
                texture = new Vector2(1, 0),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, -1),
            });


            listOfVerts.Add(new SC_cube.DVertex()
            {
                position = start + offset1 + offset2,
                texture = new Vector2(1, 1),
                color = new Vector4(colorX, colorY, colorZ, 1),
                normal = new Vector3(-1, 1, -1),
            });


            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 3);
            listOfTriangleIndices.Add(index + 2);
            listOfTriangleIndices.Add(index + 1);
            listOfTriangleIndices.Add(index + 0);
        }
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.
        //CREATE FACE FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT THE CLASS DVERTEX HOLDS THE VERTICES VERTEX BINDING DATA WHICH IS SET DIFFERENTLY IN THE UNITY 3D ENGINE.




        //ISTRANSPARENT FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT I HAVE NEGATIVE AND POSITIVE INDEXES AND I NEED TO MAKE THEM ALL POSITIVE BEFORE PROCEEDING AND ALSO I USE FLAT ARRAYS.
        //ISTRANSPARENT FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT I HAVE NEGATIVE AND POSITIVE INDEXES AND I NEED TO MAKE THEM ALL POSITIVE BEFORE PROCEEDING AND ALSO I USE FLAT ARRAYS.
        //ISTRANSPARENT FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT I HAVE NEGATIVE AND POSITIVE INDEXES AND I NEED TO MAKE THEM ALL POSITIVE BEFORE PROCEEDING AND ALSO I USE FLAT ARRAYS.
        public bool IsTransparent(int x, int y, int z)
        {
            if ((x < -ChunkWidth_L) || (y < -ChunkHeight_L) || (z < -ChunkDepth_L) || (x >= ChunkWidth_R + 1) || (y >= (ChunkHeight_R + 1)) || (z >= (ChunkDepth_R + 1)))
            {
                return true;
            }

            if (x < 0)
            {
                x *= -1;
                x = (ChunkWidth_R) + x;
            }
            if (y < 0)
            {
                y *= -1;
                y = (ChunkHeight_R) + y;
            }
            if (z < 0)
            {
                z *= -1;
                z = (ChunkDepth_R) + z;
            }

            int _index = x + (ChunkWidth_L + ChunkWidth_R + 1) * (y + (ChunkHeight_L + ChunkHeight_R + 1) * z);
            return map[_index] == 0;
        }
        //ISTRANSPARENT FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT I HAVE NEGATIVE AND POSITIVE INDEXES AND I NEED TO MAKE THEM ALL POSITIVE BEFORE PROCEEDING AND ALSO I USE FLAT ARRAYS.
        //ISTRANSPARENT FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT I HAVE NEGATIVE AND POSITIVE INDEXES AND I NEED TO MAKE THEM ALL POSITIVE BEFORE PROCEEDING AND ALSO I USE FLAT ARRAYS.
        //ISTRANSPARENT FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE. THE DIFFERENCE IN MINE HERE IS THAT I HAVE NEGATIVE AND POSITIVE INDEXES AND I NEED TO MAKE THEM ALL POSITIVE BEFORE PROCEEDING AND ALSO I USE FLAT ARRAYS.


        //ISTRANSPARENT FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE.
        public void SetBrick(int x, int y, int z, byte block, Vector3 currentposition)
        {

            /*x -= Math.Round(posX);
            y -= Math.Round(posY);
            z -= Math.Round(posZ);

            if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= width) || (z >= width))
            {
                return;
            }
            if (map[x, y, z] != block)
            {
                map[x, y, z] = block;
                Regenerate();
            }*/
        }
        //ISTRANSPARENT FUNCTION INSPIRED AND REFERENCED FROM CRAIG PERKO'S FIRST MINECRAFT TUTORIAL ON YOUTUBE.


    }
}























//Vector3 current_start_move_pos = new Vector3(x,y,z);
//float distance = Vector3.Distance(current_target_pos, center);

/*if(_spike_max_length < 50)
{
    _spike_max_length = 50;
}*/





/*int xpos = (int)Math.Round(current_start_move_pos.X);
int ypos = (int)Math.Round(current_start_move_pos.Y);
int zpos = (int)Math.Round(current_start_move_pos.Z);

if (xpos < 0)
{
    xpos *= -1;
    xpos = (ChunkWidth_R) + xpos;
}
if (ypos < 0)
{
    ypos *= -1;
    ypos = (ChunkHeight_R) + ypos;
}
if (zpos < 0)
{
    zpos *= -1;
    zpos = (ChunkDepth_R) + zpos;
}

var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/



//float distFromGoalTarget =





//List<KeyValuePair<Vector3, float>> _dictionary = new List<KeyValuePair<Vector3, float>>();


//KeyValuePair<Vector3, float> _keyvaluepair = new KeyValuePair<Vector3, float>(current_spike_neighboor_pos, dist);
//_dictionary.Add(_keyvaluepair);
/*_dictionary.Sort(
    delegate (KeyValuePair<Vector3, float> pair1,
    KeyValuePair<Vector3, float> pair2)
    {
        return pair1.Value.CompareTo(pair2.Value);
    }
);

var key = _dictionary.FirstOrDefault();
current_target_pos = key.Key;*/







/*xpos = (int)Math.Round(current_start_move_pos.X);
ypos = (int)Math.Round(current_start_move_pos.Y);
zpos = (int)Math.Round(current_start_move_pos.Z);

if (xpos < 0)
{
    xpos *= -1;
    xpos = (ChunkWidth_R) + xpos;
}

if (ypos < 0)
{
    ypos *= -1;
    ypos = (ChunkHeight_R) + ypos;
}

if (zpos < 0)
{
    zpos *= -1;
    zpos = (ChunkDepth_R) + zpos;
}

_index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}*/


/*current_start_move_pos = current_start_move_pos + (_spike_direction * 1);

int xpos = (int)Math.Round(current_start_move_pos.X);
int ypos = (int)Math.Round(current_start_move_pos.Y);
int zpos = (int)Math.Round(current_start_move_pos.Z);

if (current_target_pos.X == xpos && current_target_pos.Y == ypos && current_target_pos.Z == zpos)
{
    break;
}

if (xpos < 0)
{
    xpos *= -1;
    xpos = (ChunkWidth_R) + xpos;
}

if (ypos < 0)
{
    ypos *= -1;
    ypos = (ChunkHeight_R) + ypos;
}

if (zpos < 0)
{
    zpos *= -1;
    zpos = (ChunkDepth_R) + zpos;
}

var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}*/



/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = xpos + xx;
            ypos = ypos + yy;
            zpos = zpos + zz;

            if (xpos < 0)
            {
                xpos *= -1;
                xpos = (ChunkWidth_R) + xpos;
            }
            if (ypos < 0)
            {
                ypos *= -1;
                ypos = (ChunkHeight_R) + ypos;
            }
            if (zpos < 0)
            {
                zpos *= -1;
                zpos = (ChunkDepth_R) + zpos;
            }

            int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                map[_index] = 1;
            }
        }
    }
}*/





//List<float> _list = new List<float>();
//Dictionary<Vector3,float> _dictionary = new Dictionary<Vector3, float>();
//var total = (1 + 1) * (1 + 1) * (1 + 1);

/*int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/

//current_start_move_pos = current_start_move_pos + (_spike_direction * 1.4f);

/*int xpos = (int)Math.Round(current_start_move_pos.X);
int ypos = (int)Math.Round(current_start_move_pos.Y);
int zpos = (int)Math.Round(current_start_move_pos.Z);

if (current_target_pos.X == xpos && current_target_pos.Y == ypos && current_target_pos.Z == zpos ||
    Vector3.Distance(new Vector3(xpos,ypos,zpos), center) > _spike_max_length-1)
{
    break;
}

List<KeyValuePair<Vector3, float>> _dictionary = new List<KeyValuePair<Vector3, float>>();

for (int xx = -1; xx <= 1; xx++)
{
    for (int yy = -1; yy <= 1; yy++)
    {
        for (int zz = -1; zz <= 1; zz++)
        {
            xpos = xpos + xx;
            ypos = ypos + yy;
            zpos = zpos + zz;

            var current_spike_neighboor_pos = new Vector3(xpos, ypos, zpos);
            float dist = _check_node_distance(current_target_pos, current_spike_neighboor_pos);
            KeyValuePair<Vector3, float> _keyvaluepair = new KeyValuePair<Vector3, float>(current_spike_neighboor_pos, dist);
            _dictionary.Add(_keyvaluepair);

            if (current_target_pos.X == xpos && current_target_pos.Y == ypos && current_target_pos.Z == zpos)
            {
                break;
            }
        }
    }
}

_dictionary.Sort(
    delegate (KeyValuePair<Vector3, float> pair1,
    KeyValuePair<Vector3, float> pair2)
    {
        return pair1.Value.CompareTo(pair2.Value);
    }
);

var key = _dictionary.FirstOrDefault();
current_start_move_pos = key.Key;

xpos = (int)Math.Round(key.Key.X);
ypos = (int)Math.Round(key.Key.Y);
zpos = (int)Math.Round(key.Key.Z);

if (xpos < 0)
{
    xpos *= -1;
    xpos = (ChunkWidth_R) + xpos;
}
if (ypos < 0)
{
    ypos *= -1;
    ypos = (ChunkHeight_R) + ypos;
}
if (zpos < 0)
{
    zpos *= -1;
    zpos = (ChunkDepth_R) + zpos;
}

var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/



/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            //xpos = xpos + xx;
            //ypos = ypos + yy;
            //zpos = zpos + zz;
            xpos = (int)Math.Round(key.Key.X + xx);
            ypos = (int)Math.Round(key.Key.Y + yy);
            zpos = (int)Math.Round(key.Key.Z + zz);

            if (xpos < 0)
            {
                xpos *= -1;
                xpos = (ChunkWidth_R) + xpos;
            }
            if (ypos < 0)
            {
                ypos *= -1;
                ypos = (ChunkHeight_R) + ypos;
            }
            if (zpos < 0)
            {
                zpos *= -1;
                zpos = (ChunkDepth_R) + zpos;
            }

            var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/









/*

for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            //xpos = xpos + xx;
            //ypos = ypos + yy;
            //zpos = zpos + zz;
            xpos = (int)Math.Round(key.Key.X + xx);
            ypos = (int)Math.Round(key.Key.Y + yy);
            zpos = (int)Math.Round(key.Key.Z + zz);

            if (xpos < 0)
            {
                xpos *= -1;
                xpos = (ChunkWidth_R) + xpos;
            }
            if (ypos < 0)
            {
                ypos *= -1;
                ypos = (ChunkHeight_R) + ypos;
            }
            if (zpos < 0)
            {
                zpos *= -1;
                zpos = (ChunkDepth_R) + zpos;
            }

            var _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/















//current_start_move_pos = current_start_move_pos + (_spike_direction * 1.4f);

//why did i even need that now? keeping this for later :)
//_check_node_distance();


/*int xpos = (int)Math.Round(current_spike_position.X);
int ypos = (int)Math.Round(current_spike_position.Y);
int zpos = (int)Math.Round(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}*/



//List<float> _list = new List<float>();
//Dictionary<Vector3,float> _dictionary = new Dictionary<Vector3, float>();
//List<KeyValuePair<Vector3, float>> _dictionary = new List<KeyValuePair<Vector3, float>>();
//var total = (1 + 1) * (1 + 1) * (1 + 1);

/*int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/

/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = xpos + xx;
            ypos = ypos + yy;
            zpos = zpos + zz;

            var current_spike_neighboor_pos = new Vector3(xpos, ypos, zpos);
            float dist = _check_node_distance(center, current_spike_neighboor_pos);
            KeyValuePair<Vector3, float> _keyvaluepair = new KeyValuePair<Vector3, float>(current_spike_neighboor_pos, dist);
            //_keyvaluepair.Key = current_spike_neighboor_pos;
            //_keyvaluepair.Value = current_spike_neighboor_pos;
            //_dictionary.Add(_keyvaluepair);          


            int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/


//List<KeyValuePair<Vector3, float>> myList = _dictionary.ToList();
/*_dictionary.Sort(
    delegate (KeyValuePair<Vector3, float> pair1,
    KeyValuePair<Vector3, float> pair2)
    {
        return pair1.Value.CompareTo(pair2.Value);
    }
);

foreach (var val in _dictionary)
{
    //Console.WriteLine(val);
}
var key = _dictionary.Where(pair => pair.Value == 0).Select(pair => pair.Key).FirstOrDefault();
*/
/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = (int)Math.Round(key.X + xx);
            ypos = (int)Math.Round(key.Y + yy);
            zpos = (int)Math.Round(key.Z + zz);

            int _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/

/*int _index = (int)Math.Round(key.X + (ChunkWidth_L + ChunkWidth_R + 1) * (key.Y + (ChunkHeight_L + ChunkHeight_R + 1) * key.Z));

if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/









//IEnumerator _enum = _dictionary.GetEnumerator();
//var _current = (KeyValuePair<Vector3, float>)_enum.Current;

//var lowestorhighest = _list[0];








/*_dictionary = _dictionary.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

IEnumerator _enum = _dictionary.GetEnumerator();

var _current = (KeyValuePair<Vector3, float>)_enum.Current;
for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = (int)Math.Round(_current.Key.X + xx);
            ypos = (int)Math.Round(_current.Key.Y + yy);
            zpos = (int)Math.Round(_current.Key.Z + zz);

            _index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/



/*
List<KeyValuePair<Vector3, float>> myList = _dictionary.ToList();
myList.Sort(
    delegate (KeyValuePair<Vector3, float> pair1,
    KeyValuePair<Vector3, float> pair2)
    {
        return pair1.Value.CompareTo(pair2.Value);
    }
);


var lowestorhighest =_list[0];*/







//_list.Sort();







/*
_index = xpos + (ChunkWidth_L + ChunkWidth_R + 1) * (ypos + (ChunkHeight_L + ChunkHeight_R + 1) * zpos);
if (_index >= 0 && _index < _max)
{
    //Console.WriteLine("test");
    map[_index] = 1;
}*/


//_spike_direction = current_spike_position - center;
//_spike_direction.Normalize();
//var length = Math.Sqrt((current_spike_position.X * current_spike_position.X) + (current_spike_position.Y * current_spike_position.Y) + (current_spike_position.Z * current_spike_position.Z));
//var _side_z = ((ChunkWidth * ChunkWidth) + (ChunkHeight * ChunkHeight)) * planeSize;
//var _side_z = (current_spike_position.X * current_spike_position.X) + (current_spike_position.Y* current_spike_position.Y);

/*current_spike_position = current_spike_position + (_spike_direction * 1.4f);

int xpos = (int)Math.Round(current_spike_position.X);
int ypos = (int)Math.Round(current_spike_position.Y);
int zpos = (int)Math.Round(current_spike_position.Z);
_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}*/



/*int xpos = (int)Math.Round(current_spike_position.X);
int ypos = (int)Math.Round(current_spike_position.Y);
int zpos = (int)Math.Round(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}

xpos = (int)Math.Floor(current_spike_position.X);
ypos = (int)Math.Floor(current_spike_position.Y);
zpos = (int)Math.Floor(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}

xpos = (int)Math.Ceiling(current_spike_position.X);
ypos = (int)Math.Ceiling(current_spike_position.Y);
zpos = (int)Math.Ceiling(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

if (_index >= 0 && _index < _max)
{
    map[_index] = 1;
}
_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);*/

/*int xpos = (int)Math.Round(current_spike_position.X);
int ypos = (int)Math.Round(current_spike_position.Y);
int zpos = (int)Math.Round(current_spike_position.Z);

_index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

*/
/*for (int xx = -1; xx < 1; xx++)
{
    for (int yy = -1; yy < 1; yy++)
    {
        for (int zz = -1; zz < 1; zz++)
        {
            xpos = xpos + xx;
            ypos = ypos + yy;
            zpos = zpos + zz;

            _index = xpos + (ChunkWidth) * (ypos + (ChunkHeight) * zpos);

            if (_index >= 0 && _index < _max)
            {
                //Console.WriteLine("test");
                map[_index] = 1;
            }
        }
    }
}*/

/*
if (_index >= 0 && _index < _max)
{


}
else
{
    //break;
}*/


/*if (_spike_max_length < _max * 0.475f) //depending on where the spike is started.
{
    _spike_max_length = (int)Math.Round(_max * 0.475f);// (int)Math.Round(max - min + 2);
}*/

//Console.WriteLine(_spike_max_length);




/*public float sc_check_node(Vector3 nodeA, Vector3 nodeB)
{

    var dstX = Math.Abs((nodeA.X) - (nodeB.X));
    var dstY = Math.Abs((nodeA.Y) - (nodeB.Y));
    var dstZ = Math.Abs((nodeA.Z) - (nodeB.Z));

    if (dstX >= dstY && dstX >= dstZ)
    {
        return 14 * dstY + 14 * dstZ + (10 * ((dstX - dstZ) + (dstX - dstY)));
    }
    else if (dstX >= dstY && dstX < dstZ)
    {
        return 14 * dstY + 14 * dstZ + (10 * ((dstX - dstZ) + (dstX - dstY)));
    }



    var dstX = Math.Abs((nodeA.X) - (nodeB.X));
    var dstZ = Math.Abs((nodeA.Y) - (nodeB.Y));

    if (dstX > dstZ)
    {
        return 14 * dstZ + 10 * (dstX - dstZ);
    }
    return 14 * dstX + 10 * (dstZ - dstX);
}*/
