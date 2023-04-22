//-------------------------------------------------------
// BasesGenerator
//
// extendable script to generate different types of bases:
// aliens-like, humans-like, etc
//



////////////////////////////INVASIONX MOD////////////////////////////
////////////////////////////INVASIONX MOD////////////////////////////
////////////////////////////INVASIONX MOD////////////////////////////

include(SC_Station_Tiles_Outpost_01.js);
include(SC_Station_Tiles_Military_01.js);
include(SC_Station_Tiles_Science_01.js);
include(SC_Station_Tiles_Business_01.js);
include(SC_Station_Tiles_Mining_01.js);


////////////////////////////INVASIONX MOD////////////////////////////
////////////////////////////INVASIONX MOD////////////////////////////
////////////////////////////INVASIONX MOD////////////////////////////





var BasesGenerator = {
    /*
     ===================================================================
     Adds solid human bases, which are indestructible
     
     Has two modes:
     1 - "random" (or null):
     In this mode random bases from types are taken in random order. For example,
     from
     {types,quantity}:[a,b,c],5
     it produce
     [a,a,a,a,a] or [a,b,c,c,b].
     2 - "strict":
     In this mode you need to specify all bases you need to be generated. Quantity parameters
     will be ignored. For example, from
     {types,quantity}:[a,b,c],5
     it can give
     [a,b,c] or [b,c,a] or [c,a,b]	
     ===================================================================
     */
    AddBasesOfTypes: function(args, types, quantity, mode, customTagClass)
    {
        var sys_id = args.sys_id;
        var inf = args.system_info;
        var bases = [];
        var distanceToJumpgate = 50;

        var strictBaseNum = MathExt.RandRange(0, types.length);
        if (mode == "strict")
        {
            quantity = types.length;
        }

        // add base next to one of jumpgates
        var current_jumpgate = 0;
        var jumpgates = args.jumpgates;

        // create a number of bases
        var tries = 1;
        for (var i = 0; i < quantity; i++)
        {
            var pickedIndex = 0;
            if (mode == "strict")
            {
                //pick at least one of each
                pickedIndex = strictBaseNum % types.length;
            }
            else //suppose it's "random" or null
            {
                pickedIndex = MathExt.RandRange(0, types.length);
            }

            var base_xml = types[pickedIndex];
            var tagClass = customTagClass == null ? "none" : customTagClass;
            var bas_coord = 0;

			if (customTagClass == null)
			{
				// assign class
				if (base_xml.indexOf("mining") >= 0)
				{
					tagClass = "mining";
				}
				else if (base_xml.indexOf("outpost") >= 0)
				{
					tagClass = "outpost";
				}
				else if (base_xml.indexOf("science") >= 0)
				{
					tagClass = "science";
				}
				else if (base_xml.indexOf("business") >= 0)
				{
					tagClass = "business";
				}
				else if (base_xml.indexOf("military") >= 0)
				{
					tagClass = "military";
				}
			}

            // based on class, define position
            if (tagClass == "outpost")
            {
                // try to positionate next to jumpgate
                if (current_jumpgate < jumpgates.length - 1)
                {
                    //console.Print("Base " + i + " outpost - trying to pick near jumpgate " + current_jumpgate);
                    var jag = jumpgates[current_jumpgate];
                    var ang = MathExt.RandDouble() * Math.PI - Math.PI / 2;
                    bas_coord = {
                        x: jag.coord.x + distanceToJumpgate * Math.cos(ang),
                        y: jag.coord.y + distanceToJumpgate * Math.sin(ang)
                    };

                    current_jumpgate++;
                }
            }

            if (bas_coord == 0)
            {
                //generic coordinates pick
                bas_coord = CSGen.GetRandomCoordWithinWorldBounds(args);
                //console.Print("Base " + i + " " + tagClass + " picking coordinates " + bas_coord.x + ";" + bas_coord.y);
            }

            //console.Print("Base " + i + " coordinates picked");

            // check if fits
            // we're checking distances to other bases and jumpgates
            var bFits = true;
            var dist = MathExt.Vector2Length(bas_coord);
            if (dist < CSGen.GetMinDistanceToSun())
            {
                bFits = false;
            } //to close to sun

            for (var k = 0; k < bases.length; k++)
            {
                dist = MathExt.Vector2Length(bases[k].coord, bas_coord);
                if (dist < 70)
                {
                    bFits = false;
                } //should be no closer then 70 to any other base
            }

            for (var k = 0; k < jumpgates.length; k++)
            {
                dist = MathExt.Vector2Length(jumpgates[k].coord, bas_coord);
                if (dist < distanceToJumpgate - 10)
                {
                    bFits = false;
                } //should be no closer then 30 to any jumpgate
            }

            if (!bFits)
            {
                i--;
                //this try doesn't count
                //console.Print("too close! another try (now " + tries + ")");
                tries++;
                if (tries > 100)
                {
                    console.PrintError("Cannot create bases. Tries of base creation are over 100, still cannot find room for it, restart generation process, or change generation parameters.");
                    console.WaitForUserInteraction();
                }
            }
            else
            {


                strictBaseNum++;
                tries = 1;

                var base_id = generator.AddBase(
                    sys_id,
                    bas_coord.x, bas_coord.y,
                    base_xml,
                    StationNameGenerator.GetName(),
                    { class: tagClass });
                relations.SetBaseFaction(base_id, inf.faction);

                bases.push({
                    id: base_id,
                    coord: bas_coord,
                    base_xml: base_xml
                });






                var arrayOfRefuelPlatforms =
                    [
                        "station_platform_refuel_0",
                        "station_platform_refuel_45",
                        "station_platform_refuel_90",
                        "station_platform_refuel_135",
                        "station_platform_refuel_180",
                        "station_platform_refuel_225",
                        "station_platform_refuel_270",
                        "station_platform_refuel_315"
                    ];

                var arrayOfRepairPlatforms =
                    [
                        "station_platform_repair_0",
                        "station_platform_repair_45",
                        "station_platform_repair_90",
                        "station_platform_repair_135",
                        "station_platform_repair_180",
                        "station_platform_repair_225",
                        "station_platform_repair_270",
                        "station_platform_repair_315"
                    ];
                var startSysSwtch = -1;
                var theBase = [];



                //console.PrintError("n:" + base_xml);

                //var someTester = "tester_01";
                //console.PrintError(someTester.substring(0,1));

                //var theBase = { id: base_id, coord: bas_coord, base_xml: base_xml, sys_id: sys_id };                   
                //storage.SetGlobal("system_" + sys_id + "_base_" + base_id + "_xmlStationType", theBase);


                ///BUILD TILES////////////////////////
                //////////////////////////////////////
                //SC_Station_Tiles_Outpost_01.buildTiles(theBase);
                //////////////////////////////////////
                //////////////////////////////////////

                ////////////////////////////INVASIONX MOD////////////////////////////
                ////////////////////////////INVASIONX MOD////////////////////////////
                ////////////////////////////INVASIONX MOD////////////////////////////
                var parsedAngle;

                if (tagClass == "outpost") //base_xml.indexOf("outpost") >= 0)
                {
                    var getSomeIndex = base_xml.substring(11, base_xml.length); //outpost_01_0

                    var index = indexOfStuff(getSomeIndex);

                    /*if (index == 0)
                    {
                        index = 0;
                    }
                    else if (index == 1)
                    {
                        index = 5;
                    }
                    else if (index == 2)
                    {
                        index = 6;
                    }
                    else if (index == 3)
                    {
                        index = 7;
                    }
                    else if (index == 4)
                    {
                        index = 4;
                    }
                    else if (index == 5)
                    {
                        index = 1;
                    }
                    else if (index == 6)
                    {
                        index = 2;
                    }
                    else if (index == 7)
                    {
                        index = 3;
                    }*/

                    parsedAngle = parseInt(getSomeIndex);

                    var tempCoordRefuelX = bas_coord.x - 3.5;
                    var tempCoordRefuelY = bas_coord.y - 23;
                    var tempCoordRefuel = { x: tempCoordRefuelX, y: tempCoordRefuelY };

                    var tempCoordRepairX = bas_coord.x + 3;
                    var tempCoordRepairY = bas_coord.y - 23;
                    var tempCoordRepair = { x: tempCoordRepairX, y: tempCoordRepairY };

                    var rotatedCoordRefuel = RotatePoint(tempCoordRefuel, bas_coord, parsedAngle);
                    var rotatedCoordRepair = RotatePoint(tempCoordRepair, bas_coord, parsedAngle);

                    var StationRefuelPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRefuel.x, rotatedCoordRefuel.y, arrayOfRefuelPlatforms[index], 0);
                    var StationRepairPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRepair.x, rotatedCoordRepair.y, arrayOfRepairPlatforms[index], 0);

                    //// add turrets
                    var turret_radius = 35;
                    var level = inf.danger_level + Math.round(MathExt.RandDouble() * 4 - 2);
                    level = utils.Clamp(level, 1, 100);
                    //var angle_offset = 0.0;

                    NpcGenerator.SpawnTurretsOfType(args, bas_coord, 4, level, turret_radius, "Turret", "special_human_turret", inf.faction, { class: "turret", non_talkable: true }, parsedAngle, base_xml, sys_id, base_id);
                    var theBase = { id: base_id, coord: bas_coord, base_xml: base_xml, sys_id: sys_id, rot: parsedAngle };// widthL: 21, widthR: 21, heightT: 19, heightB: 42, 
                    storage.SetGlobal("system_" + sys_id + "_base_" + base_id + "_xmlStationType", theBase);



                    var baseInteriorGlobalVariable = { id: base_id, x: bas_coord.x, y: bas_coord.y, };
                    storage.SetGlobal("stationproximitydata" + sys_id, baseInteriorGlobalVariable);



                    //if (switchForDebugTiles == -1)
                    {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {

                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                ////console.PrintError("player_starting_system_base_: " + base_id);
                            }

                            var stationData = SC_Station_Tiles_Outpost_01.buildTiles(theBase, theBase.id);
                            storage.SetGlobal("station_tiles" + theBase.id, stationData);

                            /*if (!storage.IsSetGlobal("player_starting_system_base_")) {

                                storage.SetGlobal("player_starting_system_base_", base_id);
                                //console.PrintError("setting player_starting_system_base_: " + base_id);
                            }*/
                        }
                        //switchForDebugTiles = 1;
                    }



                    /*baseInteriorGlobalVariable.id = base_id;
                    baseInteriorGlobalVariable.x = bas_coord.x;
                    baseInteriorGlobalVariable.y = bas_coord.y;
                    baseInteriorGlobalVariable.baseID = base_id;
                    storage.SetGlobal("stationINT" + sys_id, baseInteriorGlobalVariable);
                    */


                    //if(switchForDebugTiles == 1)
                    //{
                    //	SC_Station_Tiles_Outpost_01.buildTiles(theBase);
                    //}
                }
                else if (tagClass == "science") // (base_xml.indexOf("science") >= 0)
                {
                    base_id = generator.AddBase(
                        sys_id,
                        bas_coord.x, bas_coord.y,
                        base_xml,
                        StationNameGenerator.GetName(),
                        { class: tagClass });
                    relations.SetBaseFaction(base_id, inf.faction);

                    bases.push({
                        id: base_id,
                        coord: bas_coord,
                        base_xml: base_xml
                    });


                    var getSomeIndex = base_xml.substring(11, base_xml.length); //science_01_0
                    var index = indexOfStuff(getSomeIndex);

                    if (index == 0) {
                        index = 4;
                    }
                    else if (index == 1) {
                        index = 5;
                    }
                    else if (index == 2) {
                        index = 6;
                    }
                    else if (index == 3) {
                        index = 7;
                    }
                    else if (index == 4) {
                        index = 0;
                    }
                    else if (index == 5) {
                        index = 1;
                    }
                    else if (index == 6) {
                        index = 2;
                    }
                    else if (index == 7) {
                        index = 3;
                    }

                    parsedAngle = parseInt(getSomeIndex);

                    var tempCoordRefuelX = bas_coord.x + 3.5;
                    var tempCoordRefuelY = bas_coord.y + 14.5;
                    var tempCoordRefuel = { x: tempCoordRefuelX, y: tempCoordRefuelY };

                    var tempCoordRepairX = bas_coord.x - 3;
                    var tempCoordRepairY = bas_coord.y + 14.5;
                    var tempCoordRepair = { x: tempCoordRepairX, y: tempCoordRepairY };

                    var rotatedCoordRefuel = RotatePoint(tempCoordRefuel, bas_coord, parsedAngle);
                    var rotatedCoordRepair = RotatePoint(tempCoordRepair, bas_coord, parsedAngle);

                    var StationRefuelPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRefuel.x, rotatedCoordRefuel.y, arrayOfRefuelPlatforms[index], 0);
                    var StationRepairPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRepair.x, rotatedCoordRepair.y, arrayOfRepairPlatforms[index], 0);


                    var turret_radius = 35;
                    var level = inf.danger_level + Math.round(MathExt.RandDouble() * 4 - 2);
                    level = utils.Clamp(level, 1, 100);
                    //var angle_offset = 0.0;

                    NpcGenerator.SpawnTurretsOfType(args, bas_coord, 4, level, turret_radius, "Turret", "special_human_turret", inf.faction, { class: "turret", non_talkable: true }, parsedAngle, base_xml, sys_id, base_id);
                    var theBase = { id: base_id, coord: bas_coord, base_xml: base_xml, sys_id: sys_id, rot: parsedAngle };// widthL: 21, widthR: 21, heightT: 19, heightB: 42, 
                    storage.SetGlobal("system_" + sys_id + "_base_" + base_id + "_xmlStationType", theBase);


                    //var stationData = SC_Station_Tiles_Science_01.buildTiles(theBase, theBase.id);
                    //storage.SetGlobal("station_tiles" + theBase.id, stationData);

                    //if (switchForDebugTiles == -1) 
                    {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {

                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                ////console.PrintError("player_starting_system_base_: " + base_id);
                            }

                            var stationData = SC_Station_Tiles_Science_01.buildTiles(theBase, theBase.id);
                            storage.SetGlobal("station_tiles" + theBase.id, stationData);
                            /*if (!storage.IsSetGlobal("player_starting_system_base_")) {
                          
                                storage.SetGlobal("player_starting_system_base_", base_id);
                                //console.PrintError("setting player_starting_system_base_: " + base_id);
                            }*/
                        }
                        //switchForDebugTiles = 1;
                    }
                    var baseInteriorGlobalVariable = { id: base_id, x: bas_coord.x, y: bas_coord.y, };
                    storage.SetGlobal("stationproximitydata" + sys_id, baseInteriorGlobalVariable);
                    /*SC_Station_Tiles_Science_01.buildTiles(theBase);
 
                    if (switchForDebugTiles == -1) {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else
                            {
                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                //SC_Station_Tiles_Science_01.buildTiles(theBase);
                                //console.PrintError("player_starting_system_base_: " + scriptCounter);
                                storage.SetGlobal("player_starting_system_base_", theBase);
                            }
                        }
                    }*/


                }
                else if (tagClass == "military") //(base_xml.indexOf("military") >= 0)
                {
                    base_id = generator.AddBase(
                        sys_id,
                        bas_coord.x, bas_coord.y,
                        base_xml,
                        StationNameGenerator.GetName(),
                        { class: tagClass });
                    relations.SetBaseFaction(base_id, inf.faction);

                    bases.push({
                        id: base_id,
                        coord: bas_coord,
                        base_xml: base_xml
                    });


                    var getSomeIndex = base_xml.substring(12, base_xml.length); //military_01_0
                    var index = indexOfStuff(getSomeIndex);

                    if (index == 0) {
                        index = 4;
                    }
                    else if (index == 1) {
                        index = 5;
                    }
                    else if (index == 2) {
                        index = 6;
                    }
                    else if (index == 3) {
                        index = 7;
                    }
                    else if (index == 4) {
                        index = 0;
                    }
                    else if (index == 5) {
                        index = 1;
                    }
                    else if (index == 6) {
                        index = 2;
                    }
                    else if (index == 7) {
                        index = 3;
                    }

                    parsedAngle = parseInt(getSomeIndex);

                    var tempCoordRefuelX = bas_coord.x + 3.5;
                    var tempCoordRefuelY = bas_coord.y + 14.5;
                    var tempCoordRefuel = { x: tempCoordRefuelX, y: tempCoordRefuelY };

                    var tempCoordRepairX = bas_coord.x - 3;
                    var tempCoordRepairY = bas_coord.y + 14.5;
                    var tempCoordRepair = { x: tempCoordRepairX, y: tempCoordRepairY };

                    var rotatedCoordRefuel = RotatePoint(tempCoordRefuel, bas_coord, parsedAngle);
                    var rotatedCoordRepair = RotatePoint(tempCoordRepair, bas_coord, parsedAngle);

                    var StationRefuelPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRefuel.x, rotatedCoordRefuel.y, arrayOfRefuelPlatforms[index], 0);
                    var StationRepairPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRepair.x, rotatedCoordRepair.y, arrayOfRepairPlatforms[index], 0);

                    var turret_radius = 35;
                    var level = inf.danger_level + Math.round(MathExt.RandDouble() * 4 - 2);
                    level = utils.Clamp(level, 1, 100);
                    //var angle_offset = 0.0;

                    NpcGenerator.SpawnTurretsOfType(args, bas_coord, 4, level, turret_radius, "Turret", "special_human_turret", inf.faction, { class: "turret", non_talkable: true }, parsedAngle, base_xml, sys_id, base_id);
                    var theBase = { id: base_id, coord: bas_coord, base_xml: base_xml, sys_id: sys_id, rot: parsedAngle };// widthL: 21, widthR: 21, heightT: 19, heightB: 42, 
                    storage.SetGlobal("system_" + sys_id + "_base_" + base_id + "_xmlStationType", theBase);

                    //var stationData = SC_Station_Tiles_Military_01.buildTiles(theBase, theBase.id);
                    /// storage.SetGlobal("station_tiles" + theBase.id, stationData);

                    //if (switchForDebugTiles == -1)
                    {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {

                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                ////console.PrintError("player_starting_system_base_: " + base_id);
                            }

                            var stationData = SC_Station_Tiles_Military_01.buildTiles(theBase, theBase.id);
                            storage.SetGlobal("station_tiles" + theBase.id, stationData);

                            /*if (!storage.IsSetGlobal("player_starting_system_base_")) {

                               storage.SetGlobal("player_starting_system_base_", base_id);
                               //console.PrintError("setting player_starting_system_base_: " + base_id);
                           }*/
                        }
                        //switchForDebugTiles = 1;
                    }


                    var baseInteriorGlobalVariable = { id: base_id, x: bas_coord.x, y: bas_coord.y, };
                    storage.SetGlobal("stationproximitydata" + sys_id, baseInteriorGlobalVariable);
                    /*if (switchForDebugTiles == -1) {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {
                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                SC_Station_Tiles_Military_01.buildTiles(theBase);
                                //console.PrintError("player_starting_system_base_: " + scriptCounter);
                                storage.SetGlobal("player_starting_system_base_", theBase);
                            }
                        }
                    }*/


                    /*SC_Station_Tiles_Military_01.buildTiles(theBase);
 
                    if (switchForDebugTiles == -1) {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {
                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                //SC_Station_Tiles_Military_01.buildTiles(theBase);
                                //console.PrintError("player_starting_system_base_: " + scriptCounter);
                                storage.SetGlobal("player_starting_system_base_", theBase);
                            }
                        }
                    }*/
                }
                else if (tagClass == "business") //(base_xml.indexOf("business") >= 0)
                {
                    base_id = generator.AddBase(
                        sys_id,
                        bas_coord.x, bas_coord.y,
                        base_xml,
                        StationNameGenerator.GetName(),
                        { class: tagClass });
                    relations.SetBaseFaction(base_id, inf.faction);

                    bases.push({
                        id: base_id,
                        coord: bas_coord,
                        base_xml: base_xml
                    });


                    var getSomeIndex = base_xml.substring(12, base_xml.length); //business_01_0
                    var index = indexOfStuff(getSomeIndex);

                    /*if (index == 0)
                    {
                        index = 0;
                    }
                    else if (index == 1)
                    {
                        index = 5;
                    }
                    else if (index == 2)
                    {
                        index = 6;
                    }
                    else if (index == 3)
                    {
                        index = 7;
                    }
                    else if (index == 4)
                    {
                        index = 4;
                    }
                    else if (index == 5)
                    {
                        index = 1;
                    }
                    else if (index == 6)
                    {
                        index = 2;
                    }
                    else if (index == 7)
                    {
                        index = 3;
                    }*/

                    parsedAngle = parseInt(getSomeIndex);

                    var tempCoordRefuelX = bas_coord.x - 3.5;
                    var tempCoordRefuelY = bas_coord.y - 22;
                    var tempCoordRefuel = { x: tempCoordRefuelX, y: tempCoordRefuelY };

                    var tempCoordRepairX = bas_coord.x + 3;
                    var tempCoordRepairY = bas_coord.y - 22;
                    var tempCoordRepair = { x: tempCoordRepairX, y: tempCoordRepairY };

                    var rotatedCoordRefuel = RotatePoint(tempCoordRefuel, bas_coord, parsedAngle);
                    var rotatedCoordRepair = RotatePoint(tempCoordRepair, bas_coord, parsedAngle);

                    var StationRefuelPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRefuel.x, rotatedCoordRefuel.y, arrayOfRefuelPlatforms[index], 0);
                    var StationRepairPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRepair.x, rotatedCoordRepair.y, arrayOfRepairPlatforms[index], 0);

                    var turret_radius = 35;
                    var level = inf.danger_level + Math.round(MathExt.RandDouble() * 4 - 2);
                    level = utils.Clamp(level, 1, 100);
                    //var angle_offset = 0.0;

                    NpcGenerator.SpawnTurretsOfType(args, bas_coord, 4, level, turret_radius, "Turret", "special_human_turret", inf.faction, { class: "turret", non_talkable: true }, parsedAngle, base_xml, sys_id, base_id);
                    var theBase = { id: base_id, coord: bas_coord, base_xml: base_xml, sys_id: sys_id, rot: parsedAngle };// widthL: 21, widthR: 21, heightT: 19, heightB: 42, 
                    storage.SetGlobal("system_" + sys_id + "_base_" + base_id + "_xmlStationType", theBase);



                    //var stationData = SC_Station_Tiles_Business_01.buildTiles(theBase, theBase.id);
                    //storage.SetGlobal("station_tiles" + theBase.id, stationData);

                    //if (switchForDebugTiles == -1) 
                    {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {

                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                ////console.PrintError("player_starting_system_base_: " + base_id);
                            }

                            var stationData = SC_Station_Tiles_Business_01.buildTiles(theBase, theBase.id);
                            storage.SetGlobal("station_tiles" + theBase.id, stationData);

                            /*if (!storage.IsSetGlobal("player_starting_system_base_")) {

                              storage.SetGlobal("player_starting_system_base_", base_id);
                              //console.PrintError("setting player_starting_system_base_: " + base_id);
                          }*/
                        }
                        //switchForDebugTiles = 1;
                    }


                    var baseInteriorGlobalVariable = { id: base_id, x: bas_coord.x, y: bas_coord.y, };
                    storage.SetGlobal("stationproximitydata" + sys_id, baseInteriorGlobalVariable);
                    //if(switchForDebugTiles == 1)
                    //{
                    //	    SC_Station_Tiles_Business_01.buildTiles(theBase);
                    //}

                    /*SC_Station_Tiles_Business_01.buildTiles(theBase);
 
                    if (switchForDebugTiles == -1) {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {
                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                //SC_Station_Tiles_Business_01.buildTiles(theBase);
                                //console.PrintError("player_starting_system_base_: " + scriptCounter);
                                storage.SetGlobal("player_starting_system_base_", theBase);
                            }
                        }
                    }*/
                }

                else if (tagClass == "mining") //(base_xml.indexOf("mining") >= 0)
                {
                    base_id = generator.AddBase(
                        sys_id,
                        bas_coord.x, bas_coord.y,
                        base_xml,
                        StationNameGenerator.GetName(),
                        { class: tagClass });
                    relations.SetBaseFaction(base_id, inf.faction);

                    bases.push({
                        id: base_id,
                        coord: bas_coord,
                        base_xml: base_xml
                    });


                    var getSomeIndex = base_xml.substring(10, base_xml.length); //mining_01_0
                    var index = indexOfStuff(getSomeIndex);
                    parsedAngle = parseInt(getSomeIndex);

                    if (index == 0) {
                        index = 4;
                    }
                    else if (index == 1) {
                        index = 5;
                    }
                    else if (index == 2) {
                        index = 6;
                    }
                    else if (index == 3) {
                        index = 7;
                    }
                    else if (index == 4) {
                        index = 0;
                    }
                    else if (index == 5) {
                        index = 1;
                    }
                    else if (index == 6) {
                        index = 2;
                    }
                    else if (index == 7) {
                        index = 3;
                    }

                    var tempCoordRepairX = bas_coord.x + 14.75;
                    var tempCoordRepairY = bas_coord.y + 17;
                    var tempCoordRepair = { x: tempCoordRepairX, y: tempCoordRepairY };

                    var rotatedCoordRepair = RotatePoint(tempCoordRepair, bas_coord, parsedAngle);
                    var StationRepairPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRepair.x, rotatedCoordRepair.y, arrayOfRepairPlatforms[index], 0);


                    if (index == 0) {
                        index = 4;
                    }
                    else if (index == 1) {
                        index = 5;
                    }
                    else if (index == 2) {
                        index = 6;
                    }
                    else if (index == 3) {
                        index = 7;
                    }
                    else if (index == 4) {
                        index = 0;
                    }
                    else if (index == 5) {
                        index = 1;
                    }
                    else if (index == 6) {
                        index = 2;
                    }
                    else if (index == 7) {
                        index = 3;
                    }

                    var tempCoordRefuelX = bas_coord.x + 14.75;
                    var tempCoordRefuelY = bas_coord.y + 0;
                    var tempCoordRefuel = { x: tempCoordRefuelX, y: tempCoordRefuelY };

                    var rotatedCoordRefuel = RotatePoint(tempCoordRefuel, bas_coord, parsedAngle);
                    var StationRefuelPlatform = generator.AddSpecialObject(sys_id, rotatedCoordRefuel.x, rotatedCoordRefuel.y, arrayOfRefuelPlatforms[index], 0);

                    var turret_radius = 35;
                    var level = inf.danger_level + Math.round(MathExt.RandDouble() * 4 - 2);
                    level = utils.Clamp(level, 1, 100);
                    //var angle_offset = 0.0;

                    NpcGenerator.SpawnTurretsOfType(args, bas_coord, 4, level, turret_radius, "Turret", "special_human_turret", inf.faction, { class: "turret", non_talkable: true }, parsedAngle, base_xml, sys_id, base_id);
                    var theBase = { id: base_id, coord: bas_coord, base_xml: base_xml, sys_id: sys_id, rot: parsedAngle };// widthL: 21, widthR: 21, heightT: 19, heightB: 42, 
                    storage.SetGlobal("system_" + sys_id + "_base_" + base_id + "_xmlStationType", theBase);





                    // var stationData = SC_Station_Tiles_Mining_01.buildTiles(theBase, theBase.id);
                    //storage.SetGlobal("station_tiles" + theBase.id, stationData);

                    //if (switchForDebugTiles == -1)
                    {
                        if (theBase == null) {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else {
                            if (theBase.base_xml == null) {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {

                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                ////console.PrintError("player_starting_system_base_: " + base_id);
                            }

                            var stationData = SC_Station_Tiles_Mining_01.buildTiles(theBase, theBase.id);
                            storage.SetGlobal("station_tiles" + theBase.id, stationData);

                            /*if (!storage.IsSetGlobal("player_starting_system_base_")) {

                              storage.SetGlobal("player_starting_system_base_", base_id);
                              //console.PrintError("setting player_starting_system_base_: " + base_id);
                          }*/
                        }
                        //switchForDebugTiles = 1;
                    }

                    var baseInteriorGlobalVariable = { id: base_id, x: bas_coord.x, y: bas_coord.y, };
                    storage.SetGlobal("stationproximitydata" + sys_id, baseInteriorGlobalVariable);
                    /*SC_Station_Tiles_Mining_01.buildTiles(theBase);
 
                    if (switchForDebugTiles == -1)
                    {
                        if (theBase == null)
                        {
                            //console.PrintError("theBase null: " + scriptCounter);
                        }
                        else
                        {
                            if (theBase.base_xml == null)
                            {
                                //console.PrintError("theBase.base_xml null: " + scriptCounter);
                            }
                            else {
                                //console.PrintError("theBase.base_xml !null: " + theBase.base_xml + " id: " + theBase.id);
                                //SC_Station_Tiles_Mining_01.buildTiles(theBase);
                                //console.PrintError("player_starting_system_base_: " + scriptCounter);
                                storage.SetGlobal("player_starting_system_base_", theBase);
                            }
                        }
                    }*/
                }
                ////////////////////////////INVASIONX MOD////////////////////////////
                ////////////////////////////INVASIONX MOD////////////////////////////
                ////////////////////////////INVASIONX MOD////////////////////////////















                // add satellite
                /*if(base_xml == "outpost_01")
                 {
                 this.AddSatellite(args, bas_coord, "satellite_01");
                 }*/

                // add turrets
                /*var turret_radius = 20;
                var level = inf.danger_level + Math.round(MathExt.RandDouble() * 4 - 2);
                level = utils.Clamp(level, 1, 100);
                var angle_offset = 0.0;
                NpcGenerator.SpawnTurretsOfType(args, bas_coord, 4, level, turret_radius, "Turret", "special_human_turret", inf.faction, { class: "turret", non_talkable: true }, angle_offset);*/
            }
        }

        this.AddDecorations(
            args,
            [
                {
                    type: "satellite_01_rust",
                    count_from: 1,
                    count_to: 2,
                    rotation: this.GenSatelliteRotation(),
                    rotationSpeed: this.GenSatelliteRotationSpeed(),
                    distance_from: 45,
                    distance_to: 80,
                    scale_from: 0.7,
                    scale_to: 0.7,
                },
                {
                    type: "broken_asteroid_01",
                    count_from: 3,
                    count_to: 10,
                    rotation: "random",
                    rotationSpeed: { x: 0, y: 0, z: 0 },
                    distance_from: 45,
                    distance_to: 80,
                    scale_from: 2,
                    scale_to: 3,
                },
                {
                    type: "station_wreck_01",
                    count_from: 1,
                    count_to: 3,
                    rotation: "random",
                    rotationSpeed: { x: 0, y: 0, z: 0 },
                    distance_from: 45,
                    distance_to: 80,
                    scale_from: 2,
                    scale_to: 3,
                },
            ]);


        return bases;
    },
    //---------------------------------------------------
    // Adds alien hives of specified types (ship-like destructable alien bases)
    //
    //---------------------------------------------------
    AddAlienHives: function(args, types, quantity)
    {
        var system_id = args.sys_id;
        var inf = args.system_info;
        var bases = [];

        // add base
        var tries = 0;
        for (var i = 0; i < quantity; i++)
        {
            var ang = MathExt.RandDouble() * Math.PI * 2.0;
            var dist = MathExt.RandRangeDouble(150, 250);
            var coords = { x: dist * Math.cos(ang), y: dist * Math.sin(ang) };

            var is_fits = true;

            if (generator.AreThereAnyObjects(system_id, coords.x, coords.y, 25))
            {
                is_fits = false;
            }

            if (is_fits)
            {
                for (var k = 0; k < bases.length; k++)
                {
                    var dist = MathExt.Vector2Length(bases[k].coord, coords);
                    if (dist < 70)
                    {
                        is_fits = false;
                    } //should be no closer then 70 to any other base
                }
            }

            if (!is_fits)
            {
                i--;
                tries++;
                if (tries > 1500)
                {
                    console.PrintError("Add alien hives: cannot add " + quantity + " of bases.");
                }
            }
            else
            {
                var level = inf.danger_level + Math.round(MathExt.RandDouble() * 4 - 2);
                level = utils.Clamp(level, 1, 100);
                var base_xml = utils.SelectRandom(types);

                var id = generator.AddNPCShipToSystem($i0001, "Turret", level, base_xml, system_id, coords.x, coords.y, { class: "alien_hive", sex: "male" }); // Hive
                relations.SetShipFaction(id, "aliens");

                bases.push({
                    id: id,
                    coord: coords,
                });

                // add turrets	
                var turret_radius = 20;
                var angle_offset = 0.0;
                NpcGenerator.SpawnTurretsOfType(args, coords, 4, level, turret_radius, "Turret", "special_xengatarn_turret", "aliens", { class: "turret", non_talkable: true }, angle_offset);
            }
        }

        this.AddDecorations(args,
            [
                {
                    type: "satellite_01_rust",
                    count_from: 1,
                    count_to: 2,
                    rotation: this.GenSatelliteRotation(),
                    rotationSpeed: this.GenSatelliteRotationSpeed(),
                    distance_from: 45,
                    distance_to: 80,
                    scale_from: 0.7,
                    scale_to: 0.7,
                },
                {
                    type: "broken_asteroid_01",
                    count_from: 3,
                    count_to: 10,
                    rotation: "random",
                    rotationSpeed: { x: 0, y: 0, z: 0 },
                    distance_from: 45,
                    distance_to: 80,
                    scale_from: 2,
                    scale_to: 3,
                },
                {
                    type: "station_wreck_01",
                    count_from: 1,
                    count_to: 3,
                    rotation: "random",
                    rotationSpeed: { x: 0, y: 0, z: 0 },
                    distance_from: 45,
                    distance_to: 80,
                    scale_from: 2,
                    scale_to: 3,
                },
            ]);


        return bases;
    },
    /*
     ================================
     AddDecorations
     
     used to add abstract decorations
     
     type format:
     {
     type: string,
     count_from: number,
     count_to: number,
     rotation: vector3,
     rotationSpeed: vector3,
     distance_from: number,
     distance_to: number,
     scale_from: number
     scale_to: number
     }
     ================================
     */
    AddDecorations: function(args, types)
    {
        var points = [];

        for (var k = 0; k < types.length; k++)
        {
            var current = types[k];
            var count = MathExt.RandRange(current.count_from, current.count_to + 1);

            var pass;
            for (var i = 0; i < count;)
            {
                var pass = false;
                var coord = CSGen.GetRandomCoordWithinWorldBounds(args);
                for (var j = 0; j < points.length; j++)
                {
                    var dist = MathExt.Vector2Length(points, coord);
                    if (dist < 50)
                    {
                        pass = true;
                        break;
                    }
                }

                if (!pass)
                {
                    //generate
                    i++;

                    var distance = MathExt.RandRangeDouble(current.distance_from, current.distance_to);
                    var scale = MathExt.RandRangeDouble(current.scale_from, current.scale_to);
                    this.AddDecoration(
                        args,
                        coord,
                        current.type,
                        current.rotation,
                        current.rotationSpeed,
                        distance,
                        scale);
                }
            }
        }
    },
    AddDecoration: function(args, coords, type, rotation, rotationSpeed, distance, scale)
    {
        if (rotation == "random")
        {
            rotation = this.GenRandomRotation();
        }

        generator.AddDecoration(
            args.sys_id,
            type,
            coords,
            distance,
            rotation,
            rotationSpeed,
            scale);
    },
    GenSatelliteRotation: function()
    {
        return { X: (MathExt.RandDouble()) * 90, Y: (MathExt.RandDouble()) * 90, Z: 0.0 };
    },
    GenSatelliteRotationSpeed: function()
    {
        return { X: (MathExt.RandDouble() * 0.5 - 1.0) * 0.025 * 90, Y: 0.0, Z: 0.0 };
    },
    GenRandomRotation: function()
    {
        return {
            X: MathExt.RandDouble() * 360,
            Y: MathExt.RandDouble() * 360,
            Z: MathExt.RandDouble() * 360
        };
    },
    //-------------------------------
    // LEGACY METHODS

    /*
     AddSatellite : function(args, coords, type)
     {
     var rotation = { X: (MathExt.RandDouble()) * 90, Y: (MathExt.RandDouble()) * 90, Z: 0.0 };
     var rotationSpeed = { X: (MathExt.RandDouble() * 0.5 - 1.0) * 0.025 * 90, Y: 0.0, Z: 0.0 };
     var distance = MathExt.RandRangeDouble(45, 80);
     
     generator.AddDecoration(args.sys_id,
     type,
     coords,
     distance,
     rotation,
     rotationSpeed,
     1);
     },
     
     AddBrokenAsteroid : function(args, coords, type)
     {
     var rotation = { X: 0, Y: 0, Z: 0.0 };
     var rotationSpeed = { X: 0, Y: 0.0, Z: 0.0 };
     var distance = MathExt.RandRangeDouble(55, 80);
     
     generator.AddDecoration(args.sys_id,
     type,
     coords,
     distance,
     rotation,
     rotationSpeed,
     1);
     },*/

};


function indexOfStuff(someIndex) {
    if (someIndex == "0") {
        return 0;
    }
    if (someIndex == "45") {
        return 1;
    }
    else if (someIndex == "90") {
        return 2;
    }
    else if (someIndex == "135") {
        return 3;
    }
    else if (someIndex == "180") {
        return 4;
    }
    else if (someIndex == "225") {
        return 5;
    }
    else if (someIndex == "270") {
        return 6;
    }
    else if (someIndex == "315") {
        return 7;
    }
}


//https: //stackoverflow.com/questions/13695317/rotate-a-point-around-another-point
function RotatePoint(pointToRotate, centerPoint, angleInDegrees) {
    var angleInRadians = angleInDegrees * (Math.PI / 180);
    var cosTheta = Math.cos(angleInRadians);
    var sinTheta = Math.sin(angleInRadians);

    var newX = (cosTheta * (pointToRotate.x - centerPoint.x) - sinTheta * (pointToRotate.y - centerPoint.y) + centerPoint.x);
    var newY = (sinTheta * (pointToRotate.x - centerPoint.x) + cosTheta * (pointToRotate.y - centerPoint.y) + centerPoint.y);

    var newPos = { x: newX, y: newY };

    return newPos;
}
