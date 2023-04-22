include(SystemsPresets.js);

using(generator);
using(relations);
using(spawn);
using(console);
using(storage);
using(game);


var NumOfSystems = 75;
var MinJumpsBetweenCapitals = 3;
var curNumOfSystems = 1; // number of systems created
var systems = [];



// Generate objects inside the system
function GenSystemObjects(sys_id, args)
{

    ////////////////////////////INVASIONX MOD////////////////////////////
    ////////////////////////////INVASIONX MOD////////////////////////////
    ////////////////////////////INVASIONX MOD////////////////////////////
    var startOnce = false;
    var newArray = [];

    if (startOnce == false)
    {
        //GALAXY MARKET//
        var globalShopPriceVariable = storage.SetGlobal("globalShopPrice", newArray);
        //GALAXY MARKET//

        var globIndex0 = storage.SetGlobal("maxDroneIndex0", -1);
        var globIndex1 = storage.SetGlobal("maxDroneIndex1", -1);
        var globIndex2 = storage.SetGlobal("maxDroneIndex2", -1);
        var globIndex3 = storage.SetGlobal("maxDroneIndex3", -1);
        var globIndex4 = storage.SetGlobal("maxDroneIndex4", -1);

        startOnce = true;
    }
    ////////////////////////////INVASIONX MOD////////////////////////////
    ////////////////////////////INVASIONX MOD////////////////////////////
    ////////////////////////////INVASIONX MOD////////////////////////////



    // setup some basic variables
    args.sys_id = sys_id;
    args.system_info = generator.GetSystemByID(sys_id);
    if (args.system_info.faction.length == 0)
    {
        args.system_info.faction = "none";
        console.PrintError("System with no faction occurred. Something is broken in GenerateGalaxy script.");
        console.WaitForUserInteraction();
    }



    /*
        type: "satellite_01_rust",
        count_from: 1,
        count_to: 2,
        rotation: this.GenSatelliteRotation(),
        rotationSpeed: this.GenSatelliteRotationSpeed(),
        distance_from: 45,
        distance_to: 80,
        scale_from: 0.7,
        scale_to: 0.7,*/


    //var rot = { X: (MathExt.RandDouble() * 0.5 - 1.0) * 0.025 * 90, Y: 0.0, Z: 0.0 };
    var rot = { X: 0.0, Y: 0.0, Z: 0.0 };
    var rotspeed = { X: 0.0, Y: 0.0, Z: 0.0 };
    var coords = {
        x: 0,
        y: 0
    };



    generator.AddDecoration(
        args.sys_id,
        "backgrounddeco",
        coords,
        1500,
        rot,
        rotspeed,
        1.0);





    //------------------------------------------------------
    // Generate JUMPGATES
    //

    // add jumpgates - one for each system
    args.jumpgates = AddJumpgates(args);

    //--------------------------------------------------------
    // P R E S E T - B A S E D
    //
    // generation system

    // go through function check	
    var chosen_presets = [];
    var total_chance = 0;
    for (var i in SystemsPresets)
    {
        var preset = SystemsPresets[i];
        if (preset.CheckIfFits(args)
            && preset.QuantityCheck(args))
        {
            chosen_presets.push(preset);
            total_chance += preset.Chance;
        }
    }

    // pick by chances
    var presetResult;
    var pickedIndex;
    if (chosen_presets.length == 0)
    {
        console.PrintError("No presets were chosen. Should never occur, fix SystemsPresets.");
        console.WaitForUserInteraction();
        return false;
    }
    else if (chosen_presets.length == 1)
    {
        presetResult = chosen_presets[0];
    }
    else
    {
        var currentChance = 0;
        var pickChance = MathExt.RandRange(1, total_chance + 1);
        for (var k = 0; k < chosen_presets.length; k++)
        {
            if (pickChance <= chosen_presets[k].Chance)
            {
                presetResult = chosen_presets[k];
                break;
            }
            pickChance -= chosen_presets[k].Chance;
        }
    }
    presetResult.Quantity++;
    generator.AddSystemTag(args.sys_id, "preset", presetResult.Name);

    console.Print("new system: " + presetResult.Name + " - " + args.system_info.name + " (" + args.system_info.faction + ")");

    // generation process
    args.bases = presetResult.CreateSystemBases(args);

    // create safe zone of radius 60 around every base
    args.safe_zones_centers_x = [];
    args.safe_zones_centers_y = [];
    args.safe_zones_radiuses = [];
    for (var i = 0; i < args.bases.length; i++)
    {
        var coord = args.bases[i].coord;
        args.safe_zones_centers_x.push(coord.x);
        args.safe_zones_centers_y.push(coord.y);
        args.safe_zones_radiuses.push(60);
    }

    // create save zone of radius 20 around every jumpgate
    for (var i = 0; i < args.jumpgates.length; i++)
    {
        var coord = args.jumpgates[i].coord;
        args.safe_zones_centers_x.push(coord.x);
        args.safe_zones_centers_y.push(coord.y);
        args.safe_zones_radiuses.push(20);
        // console.Print("Safe zone around jumpgate at x=" + coord.x + " and y=" + coord.y); 
    }

    storage.SetGlobal("system_" + args.sys_id + "_safe_zones_centers_x", args.safe_zones_centers_x); //store safe zones
    storage.SetGlobal("system_" + args.sys_id + "_safe_zones_centers_y", args.safe_zones_centers_y); //store safe zones
    storage.SetGlobal("system_" + args.sys_id + "_safe_zones_radiuses", args.safe_zones_radiuses); //store safe zones

    presetResult.CreateSystemObjects(args);

    return true;
}

function AddJumpgates(args)
{
    var sys_id = args.sys_id;
    var inf = args.system_info;

    var system_vec = { x: inf.coord_x, y: inf.coord_y };
    var bounds = CSGen.GetWorldBounds();
    var linkedSystems = generator.GetLinkedSystems(sys_id);

    var res = [];

    for (var i = 0; i < linkedSystems.length; i++)
    {
        var curinf = generator.GetSystemByID(linkedSystems[i]);
        var angle = -MathExt.Vector2Angle(system_vec, { x: curinf.coord_x, y: curinf.coord_y });
        var radius = bounds.x * 0.75 * MathExt.RandRangeDouble(0.6, 1.2);
        var coord_x = radius * Math.cos(angle);
        var coord_y = radius * Math.sin(angle);

        var linkedSystem = linkedSystems[i];
        var jg_id = generator.AddJumpgate(
            sys_id,
            linkedSystem,
            coord_x,
            coord_y,
            angle,
            "jumpgate_01");

        var jumpgate_coords = { x: coord_x, y: coord_y };

        res.push({
            id: jg_id,
            coord: jumpgate_coords,
        });

        // add turrets	
        var turret_radius = 10;
        var level = inf.danger_level + Math.round(MathExt.RandDouble() * 4 - 2);
        level = utils.Clamp(level, 1, 100);
        var angle_offset = generator.GetJumpgateRotation(jg_id);

        if (inf.faction == "aliens")
        {
            // do not spawn turrets in aliens system
        }
        else
        {
            // do not spawn turrets in neutral system except starting system
            if (inf.faction != "none" || sys_id == spawn.GetSystemID())
            {
                NpcGenerator.SpawnJumpgateTurretsOfType(args, jumpgate_coords, 2, level, turret_radius, "Turret", "special_human_turret_gate", inf.faction, { class: "turret", non_talkable: true }, angle_offset);
            }
        }
    }

    return res;
}

//-----------------------------------------------------------
// GenerateGalaxy
// entry point of this script
//-----------------------------------------------------------	
function GenerateGalaxy(args)
{
    //set seed of random
    MathExt.RandSeedStr(args.seed);

    NumOfSystems = Math.floor(50 + 50 * (args.galaxy_size / 255));
    args.NumOfSystems = NumOfSystems;
    console.Print("Creating galaxy with " + NumOfSystems + " systems. Seed is: " + args.seed);

    ResetPresetsQuantity();

    // generate systems
    generator.ReportLoadingPhase($x0001); // Generating systems
    GenerateSystems(args);

    // generate links
    generator.ReportLoadingPhase($x0002); // Generating links
    GenerateLinks(args);

    //--------------------------
    // O R I G I N (pick)
    //
    var angle = MathExt.RandRangeDouble(0, Math.PI * 2);

    var start_coord = { x: 50 + 50 * Math.cos(angle), y: 50 + 50 * Math.sin(angle) };
    var start_system_id = generator.GetClosestSystemToPoint(start_coord.x, start_coord.y);
    var start_system = generator.GetSystemByID(start_system_id);
    start_coord.x = start_system.coord_x;
    start_coord.y = start_system.coord_y;
    args.start_system_id = start_system_id;
    spawn.SetSystemID(start_system_id);
    //console.Print("start system: " + start_system_id);

    // generate factions
    generator.ReportLoadingPhase($x0003); // Generating factions
    if (!GenerateFactions(args, start_coord, start_system_id))
    {
        // failed to generate galaxy
        return false;
    }

    // fix some wrongly generated systems
    FixFactions(args, start_coord, start_system_id);

    // set tech and danger levels
    generator.ReportLoadingPhase($x0004); // Calculating levels
    GenerateSystemLevels(args, start_coord, start_system_id);

    // misc
    generator.ReportLoadingPhase($x0005); // Generating system objects
    if (!GenerateSystemObjects(args))
    {
        return false;
    }

    generator.ReportLoadingPhase($x0006); // Final preparations
    generator.ReportLoadingProgress(1);

    return true;
}

function GenerateSystems(args)
{
    var center = { x: 50, y: 50 };
    var tries = 20; //max attempts to create one system
    var minDistanceBetweenStars = 3;

    curNumOfSystems = 0;

    SystemNameGenerator.Clear();
    StationNameGenerator.Clear();

    while (curNumOfSystems < NumOfSystems)
    {
        generator.ReportLoadingProgress(curNumOfSystems / NumOfSystems);

        for (var i = 0; i < tries; i++)
        {
            var coord = { x: MathExt.RandRangeDouble(5, 95), y: MathExt.RandRangeDouble(5, 95) };
            var dist = MathExt.Vector2Length(coord, center);

            if (dist > 50) //test if coordinate in galaxy circle
            {
                i--;
            }
            else
            {
                var mindist = generator.GetMinDistanceToSystem(coord.x, coord.y);
                if (mindist > minDistanceBetweenStars)
                {
                    var sys_id = generator.AddSystem(coord.x, coord.y,
                        SystemNameGenerator.GetName(),
                        MathExt.RandInt());
                    curNumOfSystems++;
                    break;
                }
            }
        }
        if (i == tries)
        {
            console.Print("Couldn't find a place for star in " + tries + " tries.");
            break;
        }
    }
}


//--------------------------------------------------------------------------
// Name:
// Desc: function generates links between created systems
//--------------------------------------------------------------------------
function GenerateLinks(args)
{
    var GenerationSteps = 5;
    var AverageJumpsFromSystemToSystem = 4;

    var ByDist = {};
    var Systems = generator.GetAllSystems();

    for (var i = 0; i < GenerationSteps; i++)
    {
        generator.ReportLoadingProgress(i / GenerationSteps);

        for (var j = 0; j < Systems.length; j++)
        {
            // allocate if none
            var curSysID = Systems[j];
            if (typeof (ByDist[curSysID]) == "undefined")
            {
                ByDist[curSysID] = generator.GetSystemsByDistanceTo(curSysID);
            }

            // try to connect with current iteration
            var conWithID = ByDist[curSysID][i + 1];

            if (generator.GetJumpsBetweenSystems(curSysID, conWithID) > AverageJumpsFromSystemToSystem)
            {
                generator.AddSystemsLink(curSysID, conWithID);
            }
        }
    }

    // make system whole
    generator.FixConnectivity();
}

// Generate tech and danger levels of systems regardless of factions
// (factions influence will be taken into account later)
function GenerateSystemLevels(args, start_coord, start_system_id)
{
    var system,
        coord,
        dist,
        fraction,
        fraction2;
    var tech_level,
        danger_level;
    var factionInfo,
        capital_id,
        capital_info;

    // set systems default level according to how far it is from origin
    var systems = generator.GetAllSystems();
    for (var i = 0; i < systems.length; i++)
    {
        var system_id = systems[i];

        generator.ReportLoadingProgress(i / systems.length);
        system = generator.GetSystemByID(system_id);
        coord = { x: system.coord_x, y: system.coord_y };

        dist = MathExt.Vector2Length(coord, start_coord);
        fraction = dist / 100;

        tech_level = 5 + fraction * 50 + (MathExt.RandDouble() - 0.5) * fraction * 50;
        danger_level = 1 + Math.round(fraction * 70);

        // take faction into account
        if (typeof (system.faction) != "undefined" && system.faction != "none" && system.faction.length > 0)
        {
            factionInfo = relations.GetFactionInfoByID(system.faction);
            capital_id = relations.GetFactionCapital(system.faction);
            capital_info = generator.GetSystemByID(capital_id);

            dist = MathExt.Vector2Length(coord, { x: capital_info.coord_x, y: capital_info.coord_y });
            fraction2 = 1 - dist / factionInfo.capital_area_of_influence;
            if (fraction2 > 1)
            {
                fraction2 = 1;
            }
            else if (fraction2 <= 0)
            {
                fraction2 = 0;
            }

            tech_level += (factionInfo.capital_tech_level - tech_level) * fraction2;
            danger_level += (factionInfo.capital_danger_level - danger_level) * fraction2;
        }

        tech_level = utils.Clamp(tech_level, 1, 100);
        danger_level = utils.Clamp(danger_level, 3, 100);

        generator.SetSystemTechLevel(system_id, Math.round(tech_level));
        generator.SetSystemDangerLevel(system_id, Math.round(danger_level));
    }
}

// Function generates factions in the galaxy
function GenerateFactions(args, start_coord, start_system_id)
{
    //----------------------------------------------
    // C A P I T A L S  of factions
    //

    // grab info about factions (from xmls)
    var factions = {};
    var factions_ids = relations.GetFactions();
    for (var i = 0; i < factions_ids.length; i++)
    {
		var factionName = factions_ids[i];
		if (generator.IsFactionExcludedFromGeneration(factionName))	
		{
			continue;
		}
		
        var fact = relations.GetFactionInfoByID(factions_ids[i]);
        if (fact.stars_count_value != 0 || fact.stars_count_percent != 0)
        {
            fact.total_num_of_stars = Math.ceil(fact.stars_count_value + NumOfSystems * (fact.stars_count_percent / 100));
            factions[factions_ids[i]] = fact;
            console.Print("Faction " + factions_ids[i] + " must have num of stars " + fact.total_num_of_stars);
        }
    }

    var total_stars_needed_to_be_created = 8;
    for (var fctt in factions)
    {
        total_stars_needed_to_be_created += factions[fctt].total_num_of_stars;
    }

    if (NumOfSystems < total_stars_needed_to_be_created)
    {
        console.PrintError("Error: total amount of stars (" + NumOfSystems + ") is not enough to fit factions. Fix factions' xmls, or increase total amount of stars (" + total_stars_needed_to_be_created + " at least needed)");
        console.WaitForUserInteraction();
        return;
    }

    var result;
    var tries = 500;
    var capitalPickFails = 0;
    for (var i = 0; i < tries; i++)
    {
        console.Print("GenerateFactions: Try " + i);
        // pick capitals
        result = GenerateFactions_PickCapitals(factions, start_system_id, start_coord);
        if (!result)
        {
            capitalPickFails++;
            if (capitalPickFails > 50)
            {
                MinJumpsBetweenCapitals--;
                capitalPickFails = 0;
                console.Print("GenerateFactions: failed to pick capitals, decreasing requirement of minimum jumps count between capitals to " + MinJumpsBetweenCapitals + ". Start over...");
            }
            else
            {
                console.Print("GenerateFactions: failed to pick capitals, start over");
            }

            relations.ClearAllInfoOnSystems();
        }
        else
        {
            // reinit capital pick fails
            capitalPickFails = 0;

            for (var k in factions)
            {
                relations.SetFactionCapital(k, factions[k].capital);
            }

            // expand factions area
            result = GenerateFactions_CreateFactionsAreas(factions, start_system_id);
            if (!result)
            {
                console.Print("GenerateFactions: Failed to create factions areas, start over");
                relations.ClearAllInfoOnSystems();
            }
            else
            {
                console.Print("GenerateFactions: Try " + i + " was successful");
                GenerateFactions_FactionsPostprocessing();
                break; // generated successfully
            }
        }
    }

    if (i == tries)
    {
        console.Print("GenerateFactions: Error: cannot generate factions areas with specified parameters. Try changing Xmls.");
        return false;
    }

    return true;
}

function FixFactions(args, start_coord, start_system_id)
{
    // step 1
    // for all order, freedom and fanatics systems remove all adjacent xengatarn systems
    /*var arr = ["order", "fanatics", "freedom"];
     for(var i = 0; i < arr.length; i++)
     {
     var systems = relations.GetFactionSystems(arr[i]);
     for(var j = 0; j < systems.length; j++)
     {
     var linked = generator.GetLinkedSystems(systems[j]);
     for(var k = 0; k < linked.length; k++)
     {
     var faction = relations.GetSystemFaction(linked[k]);
     if(faction == "aliens")
     {
     //remove faction no matter what!
     relations.SetSystemFaction(linked[k], "none");
     }
     //console.PrintError(linked[k] + " system " + faction);
     }
     }
     }*/

    // step 2
    // ensure, that passage between the start system and all non-alien systems is available
    var systems = generator.GetAllSystems();
    var startSystemID = spawn.GetSystemID(); //get starting system

    console.Print("Start opening blocked by alies systems");

    for (var i = 0; i < systems.length; i++)
    {
        var systemId = systems[i];
        if (systemId == startSystemID)
        {
            continue;
        }

        var system_faction = relations.GetSystemFaction(systemId);
        if (system_faction == "aliens")
        {
            continue;
        }

        var route = generator.GetRoute(startSystemID, systemId);
        for (var k = 0; k < route.length; k++)
        {
            var faction = relations.GetSystemFaction(route[k]);
            if (faction == "aliens")
            {
                //remove faction no matter what!
                relations.SetSystemFaction(route[k], "none");

                var inf = generator.GetSystemByID(route[k]);
                console.Print("Set system " + inf.name + " faction to none");
            }
        }
    }

    console.Print("End opening blocked by alies systems");
}


// Special function to pick capitals according to xml parameters
function GenerateFactions_PickCapitals(factions, start_system_id, start_coord)
{
    // variables
    var i;
    var capitals = [];
	capitals.push(start_system_id);
    var maxAttempts = 100;

    for (i in factions)
    {
        for (var attempt = 0; attempt < maxAttempts; attempt++)
        {
            // pick by distance from origin (in percents)
            var systemId = generator.GetStarSystemByDistanceFromStarSystem(
                start_system_id,
                factions[i].distance_from_origin_min,
                factions[i].distance_from_origin_max);

            if (systemId == 0)
            {
                attempt++;
                continue;
            }

            var isOk = true;

            for (var j = 0; j < capitals.length; j++)
            {
				var anotherCapitalId = capitals[j];
                if (systemId == anotherCapitalId)
				{ 
					console.Print("The same system ID is already used as capital system for another faction");
					isOk = false;					
					break;
				}
				
				if (MinJumpsBetweenCapitals
					> generator.GetJumpsBetweenSystems(anotherCapitalId, systemId))
				{
					console.Print("Too close to another capital system");
					isOk = false;					
					break;	
				}
            }

            if (isOk)
            {
                capitals.push(systemId);
                factions[i].capital = systemId;
                break;
            }
            else
            {
                attempt++;
            }
        }

        if (attempt == maxAttempts)
        {
            console.Print("PickCapitals error: Something went wrong (Capital too close to another?), couldn't pick a capital for " + i);
            // total repick!
            return false;
        }
    }

    return true;
}

// Special function to expand factions areas around picked capitals
function GenerateFactions_CreateFactionsAreas(factions, start_system_id)
{
    var bGenerated;
    var bNeeded;
    var sti;
    var cursysfac;
    var i;
    var stars;

    // fill first system
    for (i in factions)
    {
        factions[i].stars = [factions[i].capital];
        relations.SetSystemFaction(factions[i].capital, i);
    }

    console.Print("At the beginning all factions has their capitals: ");
    for (var br in factions)
    {
        console.Print(br + " capital is " + factions[br].capital);
    }

    while (true)
    {
        bGenerated = false;
        bNeeded = false;

        // try to expand every faction by 1 unit (1 link)
        for (i in factions)
        {
            // lack of stars - needs to be generated
            if (factions[i].stars.length < factions[i].total_num_of_stars)
            {
                bNeeded = true;

                //console.Print("trying to expand");
                stars = generator.ExpandArea(factions[i].stars);

                //console.Print("expanded to: " + stars.length);
                for (sti = 0; sti < factions[i].total_num_of_stars && sti < stars.length; sti++)
                {
                    cursysfac = relations.GetSystemFaction(stars[sti]);
                    if (cursysfac.length == 0 && stars[sti] != start_system_id)
                    {
                        relations.SetSystemFaction(stars[sti], i);
                        bGenerated = true;
                        //console.Print(stars[sti] + " faction set to " + i);
                    }
                    else if (cursysfac != i || stars[sti] == start_system_id)
                    {
                        //console.Print(stars[sti] + " already taken");
                        stars.splice(sti, 1);
                        sti--;
                    }
                }
                for (sti = stars.length; sti > factions[i].total_num_of_stars; sti--)
                {
                    stars.pop();
                } //remove all extra stars

                factions[i].stars = stars;
            }
        }

        // if nothing was generated, it means either all systems properly generated,
        // or error occured, and nothing can be generated
        if (!bGenerated)
        {
            if (bNeeded)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}


// Function sets faction of unfactioned systems to "none" - default faction
function GenerateFactions_FactionsPostprocessing()
{
    // fix systems with no faction
    var systems = generator.GetAllSystems();
    for (var i = 0; i < systems.length; i++)
    {
        var inf = generator.GetSystemByID(systems[i]);

        // if no faction - set faction to "none"
        if (inf.faction.length == 0)
        {
            relations.SetSystemFaction(systems[i], "none");
        }
    }
}

// Function, initiating process of creating objects for each system
function GenerateSystemObjects(args)
{
    var systems = generator.GetAllSystems();
    for (var i = 0; i < systems.length; i++)
    {
        generator.ReportLoadingProgress(i / systems.length);
        if (!GenSystemObjects(systems[i], args))
        {
            return false;
        }
    }

    return true;
}

function ResetPresetsQuantity()
{
    for (var i in SystemsPresets)
    {
        var preset = SystemsPresets[i];
        preset.Quantity = 0;
    }
}
