//MADE BY STEVE CHASS� AKA NINEKORN AKA 9


cbuffer MatrixBuffer :register(b0)
{
	float4x4 world;
	float4x4 view;
	float4x4 proj;
};


cbuffer OVRDir :register(b2)
{
	float4 ovrdirf;
	float4 ovrdiru;
	float4 ovrdirr;
	float4 ovrpos;
	
};

cbuffer LightBuffer //:register(b1)
{
	float4 ambientColor;
	float4 diffuseColor;
	float4 specularColor;
	float4 lightDirection;
	float4 lightPosition;
	float4 lightextras; // z for grid types // 
	float4 gridcolor;
	float4 cursorcolor;

	//float padding0;
	//float3 lightPosition;
	//float lightextras.y;
};


//cbuffer MatrixBuffer :register(b1)
//{
//	int mapper[][]
//};

struct VertexInputType
{   
	float4 position : POSITION0;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth

	float4 instancePosition : POSITION1;
	float4 instanceRadRotFORWARD : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;

	float4 mapmatrix0 : POSITION5;
	float4 mapmatrix1 : POSITION6;
	float4 mapmatrix2 : POSITION7;
	float4 mapmatrix3 : POSITION8;
	float4 mapmatrix4 : POSITION9;
	float4 mapmatrix5 : POSITION10;
	float4 mapmatrix6 : POSITION11;
	float4 mapmatrix7 : POSITION12;
	float4 mapmatrix8 : POSITION13;
	float4 mapmatrix9 : POSITION14;
	float4 mapmatrix10 : POSITION15;
	float4 mapmatrix11 : POSITION16;
	float4 mapmatrix12 : POSITION17;
	float4 mapmatrix13 : POSITION18;
	float4 mapmatrix14 : POSITION19;
	float4 mapmatrix15 : POSITION20;

	int xindex : PSIZE3;	
	int yindex : PSIZE4;
	int padding2 : PSIZE5;	
	int padding3 : PSIZE6;
};

struct PixelInputType
{
    float4 position : SV_POSITION;
	float4 color : COLOR0; //byte map index xyz and w for typeofface 0 to 5
	float3 normal : NORMAL0;
	float paddingvert0 : PSIZE0;	//instance width
	float2 tex : TEXCOORD0;
	float paddingvert1 : PSIZE1;	//instance height
	float paddingvert2 : PSIZE2;	//instance depth

	float4 instancePosition : POSITION1;
	float4 instanceRadRotFORWARD : POSITION2;
	float4 instanceRadRotRIGHT : POSITION3;
	float4 instanceRadRotUP : POSITION4;

	float4 mapmatrix0 : POSITION5;
	float4 mapmatrix1 : POSITION6;
	float4 mapmatrix2 : POSITION7;
	float4 mapmatrix3 : POSITION8;
	float4 mapmatrix4 : POSITION9;
	float4 mapmatrix5 : POSITION10;
	float4 mapmatrix6 : POSITION11;
	float4 mapmatrix7 : POSITION12;
	float4 mapmatrix8 : POSITION13;
	float4 mapmatrix9 : POSITION14;
	float4 mapmatrix10 : POSITION15;
	float4 mapmatrix11 : POSITION16;
	float4 mapmatrix12 : POSITION17;
	float4 mapmatrix13 : POSITION18;
	float4 mapmatrix14 : POSITION19;
	float4 mapmatrix15 : POSITION20;

	int xindex : PSIZE3;	
	int yindex : PSIZE4;
	int padding2 : PSIZE5;	
	int padding3 : PSIZE6;
};

int lastbit(int number)
{
    number = number - (number >> 1 << 1);
    return number;
}

int nthbit(int number,int position)
{
    return lastbit(number >> position);
}


static int heightmapw = 4;
static int heightmaph = 4;
static int heightmapd = 1;

float planeSize = 0.05f;
//static int mapWidth = 4;
//static int mapHeight = 4;
//static int mapDepth = 4;

static int tinyChunkWidth = 8; // 4 // 8
static int tinyChunkHeight = 8; // 4 // 5
static int tinyChunkDepth = 1; // 4 // 8 // 1
static const int maxfloatbytemaparraylength = 8; //4 // 8 
static const int maxfloatbytemaparraylengthfull = 9; //5 // 9
static float arrayOfDigits[maxfloatbytemaparraylength];
static float arrayOfDigitsFull[maxfloatbytemaparraylengthfull];

//int iVar[3] = {1,2,3};
//int someoptions[1] = {0};
static int somebytemap[1008];//  = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};
static int somebytemapswtc[1008];// = {0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0};

//21 * 6 * 8 = 1008

static float initialmap = 51111.0;

static float4 mod_input_vertex_pos;

static float3 forwardDir;
static float3 rightDir;
static float3 upDir;

static float3 MOVINGPOINT;
static float3 vertPos;
static float diffX;
static float diffY;
static float diffZ;



static const float PI = 3.1415926535897932384626433832795f;
float DegreeToRadian(float angle)
{
   return PI * angle / 180.0f;
}

float RadianToDegree(float angle)
{
  return angle * (180.0f / PI);
}

//stackoverflow 14607640

float3 rotateveczaxis (float x, float y, float z,float angle)
{
	angle = DegreeToRadian(angle);
	float somenewx = (x * cos(angle)) - (y * sin(angle));
	float somenewy = (x * sin(angle)) + (y * cos(angle));
	float somenewz = z;

	return float3(somenewx,somenewy,somenewz);
}


float3 rotatevecyaxis (float x, float y, float z,float angle)
{	
	angle = DegreeToRadian(angle);
	float somenewx = (x * cos(angle)) + (z * sin(angle));
	float somenewy = y;
	float somenewz = (-x * sin(angle)) + (z * cos(angle));

	return float3(somenewx,somenewy,somenewz);
}



float3 rotatevecxaxis (float x, float y, float z,float angle)
{	
	angle = DegreeToRadian(angle);
	float somenewx = x;
	float somenewy = (y * cos(angle)) - (z * sin(angle));
	float somenewz = (y * sin(angle)) + (z * cos(angle));

	return float3(somenewx,somenewy,somenewz);
}





//public int IsTransparent(int x, int y, int z)
//{
//    if ((x < 0) || (y < 0) || (z < 0) || (x >= tinyChunkWidth) || (y >= tinyChunkHeight) || (z >= tinyChunkDepth)) return 1;
//    {
//		if(map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] == 0)
//		{
//
//		}
//        return map[x + tinyChunkWidth * (y + tinyChunkHeight * z)] == 0;
//    }
//}

static int facetype;
static float currentByte = 0.0;
static int currentMapData;
static int currentIndex;
static int someOtherIndex;
static int somecountermul;

//[maxvertexcount(96)] 
PixelInputType TextureVertexShader(VertexInputType input)
{ 
	PixelInputType output;
    input.position.w = 1.0f;

	

	//IVE SET THE VERTEX UNINSTANCED COLOR TO HOLD THE INDEX LOCATION OF THE BYTE
	int x = int(input.color.x); 
	int y = int(input.color.y); 
	int z = int(input.color.z);
	//IVE SET THE VERTEX UNINSTANCED COLOR TO HOLD THE INDEX LOCATION OF THE BYTE

	facetype = int(input.color.w);

	int isfrontvertex = trunc(round((input.color.w - round(input.color.w)) * 10.0f));
	
	//4*4*4 = 64 voxels per chunk max
	//8*8*8 = 512 voxels per chunk max

	currentIndex = x + tinyChunkWidth * (y + tinyChunkHeight * z); // x + tinyChunkWidth * (y + (tinyChunkHeight * z));
	someOtherIndex = currentIndex;

    somecountermul = 0;
	
	somecountermul = (currentIndex); //17.25f

    
	if(somecountermul == 0)
	{  
		currentMapData = input.mapmatrix0.x;
	}
	else if(somecountermul == 1)
	{
		currentMapData =input.mapmatrix0.y;
	}    
    else if(somecountermul == 2)
	{
		currentMapData =input.mapmatrix0.z;		
	}
	else if(somecountermul == 3)
	{
		currentMapData =input.mapmatrix0.w;
	}
	else if(somecountermul == 4)
	{
		currentMapData =input.mapmatrix1.x;
	}
	else if(somecountermul == 5)
	{
		currentMapData =input.mapmatrix1.y;
	     
	}
	else if(somecountermul == 6)
	{
		currentMapData =input.mapmatrix1.z;
	     
	}
	else if(somecountermul == 7)
	{
		currentMapData =input.mapmatrix1.w;
	     
	}
	else if(somecountermul == 8)
	{
		currentMapData =input.mapmatrix2.x;
	     
	}
	else if(somecountermul == 9)
	{
		currentMapData =input.mapmatrix2.y;
	}
	else if(somecountermul == 10)
	{
		currentMapData =input.mapmatrix2.z;

	}
	else if(somecountermul == 11)
	{
		currentMapData =input.mapmatrix2.w;
	}
	else if(somecountermul == 12)
	{
		currentMapData =input.mapmatrix3.x;

	}
	else if(somecountermul == 13)
	{
		currentMapData =input.mapmatrix3.y;

	}
	else if(somecountermul == 14)
	{
		currentMapData =input.mapmatrix3.z;
	
	}
	else if(somecountermul == 15)
	{
		currentMapData =input.mapmatrix3.w;

	}
	else if(somecountermul == 16)
	{  
		currentMapData =input.mapmatrix4.x;
	}
	else if(somecountermul == 17)
	{
		currentMapData =input.mapmatrix4.y;
	}
	else if(somecountermul == 18)
	{
		currentMapData =input.mapmatrix4.z;
	}
	else if(somecountermul == 19)
	{
		currentMapData =input.mapmatrix4.w;
	}
	else if(somecountermul == 20)
	{
		currentMapData =input.mapmatrix5.x;
	}
	else if(somecountermul == 21)
	{
		currentMapData =input.mapmatrix5.y;
	}
	else if(somecountermul == 22)
	{
		currentMapData =input.mapmatrix5.z;
	}
	else if(somecountermul == 23)
	{
		currentMapData =input.mapmatrix5.w;
	}
	else if(somecountermul == 24)
	{
		currentMapData =input.mapmatrix6.x;
	}
	else if(somecountermul == 25)
	{
		currentMapData =input.mapmatrix6.y;
	}
	else if(somecountermul == 26)
	{
		currentMapData =input.mapmatrix6.z;
	}
	else if(somecountermul == 27)
	{
		currentMapData =input.mapmatrix6.w;
	}
	else if(somecountermul == 28)
	{
		currentMapData =input.mapmatrix7.x;
	}
	else if(somecountermul == 29)
	{
		currentMapData =input.mapmatrix7.y;
	}
	else if(somecountermul == 30)
	{
		currentMapData =input.mapmatrix7.z;
	}
	else if(somecountermul == 31)
	{
		currentMapData =input.mapmatrix7.w;
	}
	else if(somecountermul == 32)
	{  
		currentMapData =input.mapmatrix8.x;
	}
	else if(somecountermul == 33)
	{
		currentMapData =input.mapmatrix8.y;
	}
	else if(somecountermul == 34)
	{
		currentMapData =input.mapmatrix8.z;
	}
	else if(somecountermul == 35)
	{
		currentMapData =input.mapmatrix8.w;
	}
	else if(somecountermul == 36)
	{
		currentMapData =input.mapmatrix9.x;
	}
	else if(somecountermul == 37)
	{
		currentMapData =input.mapmatrix9.y;
	}
	else if(somecountermul == 38)
	{
		currentMapData =input.mapmatrix9.z;
	}
	else if(somecountermul == 39)
	{
		currentMapData =input.mapmatrix9.w;
	}
	else if(somecountermul == 40)
	{
		currentMapData =input.mapmatrix10.x;
	}
	else if(somecountermul == 41)
	{
		currentMapData =input.mapmatrix10.y;
	}
	else if(somecountermul == 42)
	{
		currentMapData =input.mapmatrix10.z;
	}
	else if(somecountermul == 43)
	{
		currentMapData =input.mapmatrix10.w;
	}
	else if(somecountermul == 44)
	{
		currentMapData =input.mapmatrix11.x;
	}
	else if(somecountermul == 45)
	{
		currentMapData =input.mapmatrix11.y;
	}
	else if(somecountermul == 46)
	{
		currentMapData =input.mapmatrix11.z;
	}
	else if(somecountermul == 47)
	{
		currentMapData =input.mapmatrix11.w;
	}
	else if(somecountermul == 48)
	{  
		currentMapData =input.mapmatrix12.x;
	}
	else if(somecountermul == 49)
	{
		currentMapData =input.mapmatrix12.y;
	}
	else if(somecountermul == 50)
	{
		currentMapData =input.mapmatrix12.z;
	}
	else if(somecountermul == 51)
	{
		currentMapData =input.mapmatrix12.w;
	}
	else if(somecountermul == 52)
	{
		currentMapData =input.mapmatrix13.x;
	}
	else if(somecountermul == 53)
	{
		currentMapData =input.mapmatrix13.y;
	}
	else if(somecountermul == 54)
	{
		currentMapData =input.mapmatrix13.z;
	}
	else if(somecountermul == 55)
	{
		currentMapData =input.mapmatrix13.w;
	}
	else if(somecountermul == 56)
	{
		currentMapData =input.mapmatrix14.x;
	}
	else if(somecountermul == 57)
	{
		currentMapData =input.mapmatrix14.y;
	}
	else if(somecountermul == 58)
	{
		currentMapData =input.mapmatrix14.z;
	}
	else if(somecountermul == 59)
	{
		currentMapData =input.mapmatrix14.w;
	}
	else if(somecountermul == 60)
	{
		currentMapData =input.mapmatrix15.x;
	}
	else if(somecountermul == 61)
	{
		currentMapData =input.mapmatrix15.y;
	}
	else if(somecountermul == 62)
	{
		currentMapData =input.mapmatrix15.z;
	}
	else if(somecountermul == 63) // 
	{
		currentMapData = input.mapmatrix15.w;
	}






	//0-4-1-5-2-6-3-7
	//8-12-9-13-10-14-11-15
	//16-20-17-21-18-22-19-23
	//24-28-25-29-26-30-27-31
	//32-36-33-37-34-38-35-39
	//40-44-41-45-42-46-43-47
	//48-52-49-53-50-54-51-55
	//56-60-57-61-58-62-59-63	

	//0-1-2-3-4-5-6-7
	//indexplacement 0 => 1 + (3 - 0) * 2 = 7
	//indexplacement 1 => 1 + (3 - 1) * 2 = 5
	//indexplacement 2 => 1 + (3 - 2) * 2 = 3
	//indexplacement 3 => 1 + (3 - 3) * 2 = 1

	//indexplacement 4 => 0 + (7 - 4) * 2 = 6
	//indexplacement 5 => 0 + (7 - 5) * 2 = 4
	//indexplacement 6 => 0 + (7 - 6) * 2 = 2
	//indexplacement 7 => 0 + (7 - 7) * 2 = 0







	
	input.position.w = 1.0f;

	mod_input_vertex_pos = input.position;

	mod_input_vertex_pos.x += input.instancePosition.x;
	mod_input_vertex_pos.y += input.instancePosition.y;
	mod_input_vertex_pos.z += input.instancePosition.z;
	mod_input_vertex_pos.w = 1.0f;

	forwardDir = float3(input.instanceRadRotFORWARD.x, input.instanceRadRotFORWARD.y, input.instanceRadRotFORWARD.z);
	rightDir = float3(input.instanceRadRotRIGHT.x, input.instanceRadRotRIGHT.y, input.instanceRadRotRIGHT.z); 
	upDir = float3(input.instanceRadRotUP.x, input.instanceRadRotUP.y, input.instanceRadRotUP.z);
	


	//mod_input_vertex_pos.xyz =  rotatevecxaxis(mod_input_vertex_pos.x,mod_input_vertex_pos.y,mod_input_vertex_pos.z,25); //45
	MOVINGPOINT = float3(input.instancePosition.x, input.instancePosition.y, input.instancePosition.z);
	


	float multowardsrift = 10.5f;


	float3 dirtorift = float3(0,0,-1);//ovrpos.xyz - MOVINGPOINT.xyz;
	//dirtorift = normalize(dirtorift.xyz);

	float somemul = currentMapData * 0.0001f;
	//float somemul = currentMapData;
	multowardsrift = somemul * 1.0f;








	
	int projectionsetting = 0;

	float3 somecamerapos = float3(0,0,0);
	float4x4 worldswap = world;

	if(round(worldswap._44) == -1.0f) //_m00 to _m33
	{
		somecamerapos = float3(0,0, 1.0f);

		projectionsetting = 1;
	
		//world = worldswap;
	}
	else if(round(worldswap._44) == 1.0f) 
	{
		somecamerapos = ovrpos;
		//somecamerapos = float3(0,0, 1.0f);

		projectionsetting = 0;
	}

	worldswap._44 = 1.0f;



	vertPos = float3(mod_input_vertex_pos.x, mod_input_vertex_pos.y, mod_input_vertex_pos.z);	

	diffX = (vertPos.x - (input.instancePosition.x));
	diffY = (vertPos.y - (input.instancePosition.y));
	diffZ = (vertPos.z - (input.instancePosition.z));
		

		
	MOVINGPOINT = MOVINGPOINT + (-rightDir * diffX);
	MOVINGPOINT = MOVINGPOINT + (upDir * diffY);
	MOVINGPOINT = MOVINGPOINT + (forwardDir * diffZ);






	multowardsrift = somemul * -1.0f;
	float effectmul = 1.0f;

	//EFFECT ROTATE TOWARDS PLAYER POSITION.
	if(facetype == 0) //frontface
	{	

		//float roundeddecimal = round(floor(input.color.z) * 10.0f) / 10.0f;
		if(isfrontvertex == 1)
		{
			float3 otherdir = somecamerapos.xyz - (MOVINGPOINT.xyz *-1);  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 

			float somelength = length(otherdir);
			otherdir /= somelength;

			MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
			MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
			MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
		}
		else
		{
			
		}
	}

	else if(facetype == 1) //top
	{
		if(isfrontvertex == 1)
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 
			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{

			}
		}
		else
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz;

			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{

				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{

				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
		}
	}
	else if(facetype == 2) //left
	{
		if(isfrontvertex == 1)
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 
			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{

				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{

			}
		}
		else
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 
			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{

				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{

				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
		}
	}
	else if(facetype == 3) //right
	{
		if(isfrontvertex == 1)
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 
			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{

				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{

			}
		}
		else
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 
			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
		}
	}
	else if(facetype == 4) //back
	{
		if(isfrontvertex == 1)
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 
			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{

			}
		}
		else
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 
			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
		}
	}
	else if(facetype == 5) //bottom
	{
		if(isfrontvertex == 1)
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz *-1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 
			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{

			}
		}
		else
		{
			float3 otherdir = somecamerapos.xyz - MOVINGPOINT.xyz * -1;  //ovrdirf.xyz * -1;//somecamerapos.xyz - MOVINGPOINT.xyz; 

			float somelength = length(otherdir);
			otherdir /= somelength;
			if(multowardsrift < 0)
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
			else
			{
				MOVINGPOINT.x = MOVINGPOINT.x + (otherdir.x * (-multowardsrift) * 0.25f);
				MOVINGPOINT.y = MOVINGPOINT.y + (otherdir.y * (-multowardsrift) * 0.25f);
				MOVINGPOINT.z = MOVINGPOINT.z + (otherdir.z * (-multowardsrift) * 0.25f);
			}
		}
	}









	input.position.x = MOVINGPOINT.x;
	input.position.y = MOVINGPOINT.y;
	input.position.z = MOVINGPOINT.z;


	//output.position = mul(mod_input_vertex_pos, world);
	output.position = mul(input.position, worldswap);
	output.position = mul(output.position, view);
	output.position = mul(output.position, proj);



	//float4x4 worldviewmatrix = mul(worldswap,view);
	//float3 positionvs = input.position.xyz + float3(worldviewmatrix._41,worldviewmatrix._42,worldviewmatrix._43);
	//float3 finalpos = mul(float4(positionvs,1.0), proj);
	//output.position = float4(finalpos,1.0);





	output.instancePosition.x = input.instancePosition.x;
	output.instancePosition.y = input.instancePosition.y;
	output.instancePosition.z = input.instancePosition.z;


	/*output.instanceRadRotFORWARD.x = input.instanceRadRotFORWARD.x;
	output.instanceRadRotFORWARD.y = input.instanceRadRotFORWARD.y;
	output.instanceRadRotFORWARD.z = input.instanceRadRotFORWARD.z;

	output.instanceRadRotRIGHT.x = input.instanceRadRotRIGHT.x;
	output.instanceRadRotRIGHT.y = input.instanceRadRotRIGHT.y;
	output.instanceRadRotRIGHT.z = input.instanceRadRotRIGHT.z;

	output.instanceRadRotUP.x = input.instanceRadRotUP.x;
	output.instanceRadRotUP.y = input.instanceRadRotUP.y;
	output.instanceRadRotUP.z = input.instanceRadRotUP.z;*/

	output.color = input.color;
	

	



	output.paddingvert0 = input.paddingvert0;
	output.paddingvert1 = input.paddingvert1;
	output.paddingvert2 = input.paddingvert2;
	

	//output.tex = input.tex;


	output.normal = input.normal;
	//output.normal = mul(input.normal, worldswap);
	//output.normal = normalize(output.normal);

	output.xindex = input.xindex;
	output.yindex = input.yindex;
	//output.zindex = input.zindex;




	
	//option 
	int sccsvvdhdgrayscaleorcolored = int(round(lightextras.w));
	//option

	//instances in width
	int instancesw = int(input.paddingvert0); // 256 // 240 // 320 // 480 // 128 //120

	//instances in height
	int instancesh = int(input.paddingvert1); // 128 // 135 // 180 // 270 // 72 //68

	//projection setting
	int instancesd = int(input.paddingvert2); 
	
	//4 or 8 multiplier
	int oriscreenx = instancesw * 8; //80 when using 20 width instances in SC_GlobalsChunkKeyboard //// 192 <=> 768 
 	int oriscreeny = instancesh * 8; //40 when using 10 height instances in SC_GlobalsChunkKeyboard //// 108 <=> 432
	//*2 when more screens in scgraphicssec.cs
	
	float xdivisionnormalizedtoproportionsofonemax = 1/oriscreenx;
	
	if(tinyChunkWidth == 8)
	{
		if(sccsvvdhdgrayscaleorcolored == 0)
		{
			/*
			if(facetype == 0)
			{
				//MY ORIGINAL
				//MY ORIGINAL
				float2 inputtexture = (input.tex/ float2(oriscreenx,oriscreeny));
				inputtexture.x = (inputtexture.x + (xdivisionnormalizedtoproportionsofonemax * (input.xindex)));
				inputtexture.x = ((inputtexture.x) + (((1.0f/float(oriscreenx))) * ((input.xindex * tinyChunkWidth) + (x))));
				inputtexture.y = ((inputtexture.y) + (((1.0f/float(oriscreeny))) * ((input.yindex * tinyChunkHeight) + (tinyChunkHeight-1- y))));
				//inputtexture.x *=-1;
				output.tex = inputtexture;
				//textureColor = shaderTexture.Sample(SampleType, input.tex);
				//MY ORIGINAL
				//MY ORIGINAL
			}
			else
			{
				output.tex = input.tex;
			}*/





			/*
			else if(facetype == 1)
			{
				
			}
			else if(facetype == 2)
			{
				
			}
			else if(facetype == 3) 
			{
				
			}
			else if(facetype == 4) 
			{
				
			}
			else if(facetype == 5) 
			{
				
			}*/


		}
	}




	output.tex = input.tex;












	return output;
}
