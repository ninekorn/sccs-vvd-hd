using(npc);
using(console);
using(player);
using(station);
using(ship);
using(game);
using(generator);
using(storage);

//This is needed for spawning turrets
include(NpcGenerator.js);

var counterToRepairTurrets = 0;
var counterToBuildTurrets = 0;

//var locationOfSpawnedTurretsToRepair = [];
//var locationOfMissingTurretsToRepair = [];
var currentIndex = 0;
//var locationOfMissingTurrets = [];

function OnCheckRequirements()
{
    var npc_id = topic.GetCurrentNpcShipId();

    if (npc.GetTag(npc_id, "class") == "station.commander")
    {
        return true;
    }
    return false;
}

function OnStart()
{

}

function OnDialogue()
{
    var input = topic.GetInput();
    var state = topic.GetState();
	var current_station_id = ship.GetCurrentBase(PLAYER_SHIP);

    if (input == NO_INPUT)
    {
        topic.AddPhrase($p0001); // ORIGINAL => Sure thing! We always welcome assistance and financial support for our station.
        topic.AddPhrase($p0002); // ORIGINAL => And given difficult times with the Xengatarn and pirates around we would never turn down such an offer.

        topic.AddChoice(10, $c0001); // ORIGINAL => Let's begin then!
        topic.AddChoice(999, $c0002); // ORIGINAL => Maybe next time...
    }
	else if (input == 10)
	{
        topic.AddPhrase($p0003); // ORIGINAL => So, what can you do for us today? Any help would be greatly appreciated!

        topic.AddChoice(100, $c0003); // ORIGINAL => I want to repair existing turrets.
        topic.AddChoice(200, $c0004); // ORIGINAL => I want to install standard T1 turrets.
		topic.AddChoice(300, $c0005); // ORIGINAL => I want to install improved T2 turrets.
		topic.AddChoice(400, $c0006); // ORIGINAL => I want to install the best T3 turrets.
        topic.AddChoice(999, $c0007); // ORIGINAL => Maybe next time...
	}
	else if (input == 20)
	{
        topic.AddPhrase($p0004); // ORIGINAL => Oh, well. You don't have to do that now. But if at any time you have some spare parts to aid our defenses you are always welcome.

        topic.AddChoice(10, $c0008); // ORIGINAL => There's another thing...
        topic.AddChoice(999, $c0009); // ORIGINAL => Alright, I'll be on my way then.
	}
    else if (input == 11)
    {
        topic.DialogueBreak();
    }

	//===================================
	//========== Repair ================= //IN THE ORIGINAL, REPAIRING A TURRET MEANT TO REPLACE IT WHEN IT WAS DESTROYED SO IT WASN'T REALLY A REPAIR and it still isn't with this current pay to full HP either. i prefer sending drones for repair very soon.
	//===================================

	else if (input == 100)
    {
        var station = generator.GetBaseByID(current_station_id);
        var system_id = station.system_id;

        var arrayOfTurretsAtStation = storage.GetGlobal("system_" + system_id + "_base_" + current_station_id + "_turrets");

        var counterTurretSpawnedDamaged = 0;
        var counterTurretSpawnedTotal = 0;
        var counterTurretNotSpawned = 0;

        var locationOfMissingTurrets = [];
        var locationOfSpawnedTurrets = [];
        var locationOfSpawnedTurretsToRepair = [];

        for (var i = 0; i < arrayOfTurretsAtStation.length; i++)
        {
            if (game.IsShipExists(arrayOfTurretsAtStation[i].id))//generator.ShipExists(arrayOfTurretsAtStation[i].id)) //game.IsShipExists(arrayOfTurretsAtStation[i].id))
            {
                arrayOfTurretsAtStation[i].spawned == 1;

                var currentTurretStructureValue = ship.GetCurrentValue(arrayOfTurretsAtStation[i].id, "structure");
                var turretAtFullHPvalue = ship.GetFinalCacheValue(arrayOfTurretsAtStation[i].id, "structure");

                if (turretAtFullHPvalue != currentTurretStructureValue)
                {
                    var price = GetPriceForAction(arrayOfTurretsAtStation[i].xmltype, 1);
                    var turretData = { id: arrayOfTurretsAtStation[i].id, loc: arrayOfTurretsAtStation[i].loc, index: i, xmltype: arrayOfTurretsAtStation[i].xmltype, hpcurrent: currentTurretStructureValue, hptotal: turretAtFullHPvalue, repairMaterialPrice: price };

                    locationOfSpawnedTurrets.push(turretData);
                    counterTurretSpawnedDamaged++;
                }
                counterTurretSpawnedTotal++;
            }
            else
            {
                arrayOfTurretsAtStation[i].spawned == 0;

                var turretData = { loc: arrayOfTurretsAtStation[i].loc, index: i, xmltype: arrayOfTurretsAtStation[i].xmltype };

                locationOfMissingTurrets.push(turretData);

                counterTurretNotSpawned++;
            }
        }

        if (counterTurretNotSpawned == arrayOfTurretsAtStation.length && counterTurretSpawnedDamaged == 0) // if there are no turrets spawned at all
        {
            //TO IMPLEMENT THE REQUEST FOR REPORT AND HAIL TO THE STATION BEFORE ARRIVING AND ASKING AN AUDIO REPORT OF WHATS GOING KINDA. SO I'VE GOTTA WORK ON THIS MICROPHONE INPUT. GIVE ME A SEC.
            //topic.AddPhrase($p0007); // ORIGINAL => Uhh... all of our turrets have been destroyed. There isn't much left to repair, I'm afraid...
            //topic.AddPhrase("");
            topic.AddPhrase($p0008); // ORIGINAL => But perhaps you'd like to help us install new turrets instead?
            topic.AddChoice(10, $c0012); // ORIGINAL => Let's see then...
            topic.AddChoice(999, $c0013); //ORIGINAL =>  Maybe next time...
        }
        else if (counterTurretSpawnedDamaged > 0) // if there are no turrets spawned at all
        {
            counterToRepairTurrets = 0;

            var ii = 0;

            for (ii = 0; ii < locationOfSpawnedTurrets.length; ii++)
            {
                //topic.AddPhrase($p0011, { str: locationOfSpawnedTurrets[i].repairMaterialPrice.components_structural, mech: locationOfSpawnedTurrets[i].repairMaterialPrice.components_mechanical, elec: locationOfSpawnedTurrets[i].repairMaterialPrice.components_electronic, comp: locationOfSpawnedTurrets[i].repairMaterialPrice.components_composites });

                var structuralString = locationOfSpawnedTurrets[ii].repairMaterialPrice.components_structural;
                var mechanicalString = locationOfSpawnedTurrets[ii].repairMaterialPrice.components_mechanical;
                var electronicString = locationOfSpawnedTurrets[ii].repairMaterialPrice.components_electronic;
                var compositesString = locationOfSpawnedTurrets[ii].repairMaterialPrice.components_composites;

                if (PlayerHasEnoughComponents(locationOfSpawnedTurrets[ii].repairMaterialPrice))
                {
                    //topic.AddPhrase("" + locationOfSpawnedTurrets[ii].loc);
                    topic.AddChoice(101 + ii, "Cost of repair is" + " str " + structuralString + " mech " + mechanicalString + " elec " + electronicString + " comp " + compositesString);
                    locationOfSpawnedTurretsToRepair.push(locationOfSpawnedTurrets[ii]);
                    counterToRepairTurrets++;
                }
                else
                {
                    //ORIGINAL => topic.AddPhrase("You do not have enough materials.");
                    //ORIGINAL => topic.AddChoice(20, $c0015); // That's a bit too expensive for me.
                }
            }
            if (counterToRepairTurrets == 0)
            {
                topic.AddPhrase("You do not have enough materials.");
            }
        }
        else if (counterTurretNotSpawned == 0 && counterTurretSpawnedTotal == arrayOfTurretsAtStation.length)
        {
            topic.AddPhrase("There is nothing to repair.");
        }
    }

    else if (input >= 101 && input < 101 + counterToRepairTurrets)
    {
        var indexOfTurret = input - 101;

        if (game.IsShipExists(locationOfSpawnedTurretsToRepair[indexOfTurret].id))//generator.ShipExists(arrayOfTurretsAtStation[i].id)) //game.IsShipExists(arrayOfTurretsAtStation[i].id))
        {
            RemoveComponents(locationOfSpawnedTurretsToRepair[indexOfTurret].repairMaterialPrice);
            topic.AddPhrase("" + locationOfSpawnedTurretsToRepair[indexOfTurret].loc);
            ship.SetCurrentValue(locationOfSpawnedTurretsToRepair[indexOfTurret].id, "structure", locationOfSpawnedTurretsToRepair[indexOfTurret].hptotal);
            topic.AddPhrase("The station will dispatch a drone for repairs shortly.");
        }
        else
        {
            topic.AddPhrase("The turret that you wanted to repair doesn't exist anymore.");
            topic.AddChoice(100, "Go back to the prior menu.");
            topic.AddChoice(11, "Disconnect");
        }
    }

	//===================================
	//========= Standard turrets ========
	//===================================

	else if (input == 200)
    {
        var current_station_id = ship.GetCurrentBase(PLAYER_SHIP);
        var station = generator.GetBaseByID(current_station_id);
        var system_id = station.system_id;

        var arrayOfTurretsAtStation = storage.GetGlobal("system_" + system_id + "_base_" + current_station_id + "_turrets");

        var counterTurretSpawnedDamaged = 0;
        var counterTurretSpawnedTotal = 0;
        var counterTurretNotSpawned = 0;

        var locationOfSpawnedTurrets = [];

        var locationOfSpawnedTurretsToRepair = [];
        var locationOfMissingTurretsToRepair = [];

        counterToBuildTurrets = 0;
        locationOfMissingTurrets = [];

        for (var i = 0; i < arrayOfTurretsAtStation.length; i++)
        {
            // && ship.GetCurrentValue(arrayOfTurretsAtStation[i].id, "structure") < 0
            if (!generator.ShipExists(arrayOfTurretsAtStation[i].id))//!game.IsShipExists(arrayOfTurretsAtStation[i].id))//generator.ShipExists(arrayOfTurretsAtStation[i].id))
            {
                arrayOfTurretsAtStation[i].spawned == 0;

                var price = GetPriceForAction(arrayOfTurretsAtStation[i].xmltype, 2);

                var turretData = { loc: arrayOfTurretsAtStation[i].loc, index: i, xmltype: arrayOfTurretsAtStation[i].xmltype, buildPrice: price, coordsX: arrayOfTurretsAtStation[i].coordsX, coordsY: arrayOfTurretsAtStation[i].coordsY, baseID: arrayOfTurretsAtStation[i].baseID };

                locationOfMissingTurrets.push(turretData);
                counterTurretNotSpawned++;
            }
            else
            { 

            }
        }

        if (counterTurretNotSpawned > 0) // if there are missing turrets
        {
            var ii = 0;

            for (ii = 0; ii < locationOfMissingTurrets.length; ii++)
            {
                //topic.AddPhrase($p0011, { str: locationOfMissingTurrets[i].repairMaterialPrice.components_structural, mech: locationOfMissingTurrets[i].repairMaterialPrice.components_mechanical, elec: locationOfMissingTurrets[i].repairMaterialPrice.components_electronic, comp: locationOfMissingTurrets[i].repairMaterialPrice.components_composites });

                var structuralString = locationOfMissingTurrets[ii].buildPrice.components_structural;
                var mechanicalString = locationOfMissingTurrets[ii].buildPrice.components_mechanical;
                var electronicString = locationOfMissingTurrets[ii].buildPrice.components_electronic;
                var compositesString = locationOfMissingTurrets[ii].buildPrice.components_composites;

                if (PlayerHasEnoughComponents(locationOfMissingTurrets[ii].buildPrice))
                {
                    //topic.AddPhrase("" + locationOfMissingTurrets[ii].loc);

                    topic.AddChoice(201 + ii, "Cost of building turret is" + " str " + structuralString + " mech " + mechanicalString + " elec " + electronicString + " comp " + compositesString);

                    //locationOfMissingTurretsToRepair.push(locationOfMissingTurrets[ii]);

                    counterToBuildTurrets++;
                }
                else
                {
                    //ORIGINAL => topic.AddPhrase("You do not have enough materials.");
                    //ORIGINAL => topic.AddChoice(20, $c0015); // That's a bit too expensive for me.
                }
            }

            currentIndex = ii;

            if (counterToBuildTurrets == 0)
            {
                topic.AddPhrase("You do not have enough materials.");
            }
        }
        else
        {
            topic.AddPhrase("There are no missing turrets.");
        }
    }
    
    else if (input >= 201 && input < 201 + currentIndex)
    {
        var current_station_id = ship.GetCurrentBase(PLAYER_SHIP);

        var station = generator.GetBaseByID(current_station_id);

        var system_id = station.system_id;

        var indexOfTurret = input - 201;

        RemoveComponents(locationOfMissingTurrets[indexOfTurret].buildPrice);

        var turretID = generator.AddNPCShipToSystem("", "Turret", 1, "special_human_turret", system_id, locationOfMissingTurrets[indexOfTurret].coordsX, locationOfMissingTurrets[indexOfTurret].coordsY, { class: "turret", non_talkable: true });

        var arrayOfTurretsAtStation = storage.GetGlobal("system_" + system_id + "_base_" + locationOfMissingTurrets[indexOfTurret].baseID + "_turrets");

        var arrayOfData = { loc: locationOfMissingTurrets[indexOfTurret].loc, id: turretID, sys_id: system_id, coordsX: locationOfMissingTurrets[indexOfTurret].coordsX, coordsY: locationOfMissingTurrets[indexOfTurret].coordsY, baseID: locationOfMissingTurrets[indexOfTurret].baseID, spawned: 1, xmltype: "special_human_turret" }; 

        arrayOfTurretsAtStation[locationOfMissingTurrets[indexOfTurret].index] = arrayOfData;

        storage.SetGlobal("system_" + system_id + "_base_" + locationOfMissingTurrets[indexOfTurret].baseID + "_turrets", arrayOfTurretsAtStation);
    }




    else if (input == 300)
    {
        var current_station_id = ship.GetCurrentBase(PLAYER_SHIP);
        var station = generator.GetBaseByID(current_station_id);
        var system_id = station.system_id;

        var arrayOfTurretsAtStation = storage.GetGlobal("system_" + system_id + "_base_" + current_station_id + "_turrets");

        var counterTurretSpawnedDamaged = 0;
        var counterTurretSpawnedTotal = 0;
        var counterTurretNotSpawned = 0;

        var locationOfSpawnedTurrets = [];

        locationOfSpawnedTurretsToRepair = [];
        locationOfMissingTurretsToRepair = [];

        counterToBuildTurrets = 0;
        locationOfMissingTurrets = [];

        for (var i = 0; i < arrayOfTurretsAtStation.length; i++) {
            // && ship.GetCurrentValue(arrayOfTurretsAtStation[i].id, "structure") < 0
            if (!generator.ShipExists(arrayOfTurretsAtStation[i].id))//!game.IsShipExists(arrayOfTurretsAtStation[i].id))//generator.ShipExists(arrayOfTurretsAtStation[i].id))
            {
                arrayOfTurretsAtStation[i].spawned == 0;

                var price = GetPriceForAction(arrayOfTurretsAtStation[i].xmltype, 4);

                var turretData = { loc: arrayOfTurretsAtStation[i].loc, index: i, xmltype: arrayOfTurretsAtStation[i].xmltype, buildPrice: price, coordsX: arrayOfTurretsAtStation[i].coordsX, coordsY: arrayOfTurretsAtStation[i].coordsY, baseID: arrayOfTurretsAtStation[i].baseID };

                locationOfMissingTurrets.push(turretData);
                counterTurretNotSpawned++;
            }
            else {

            }
        }

        if (counterTurretNotSpawned > 0) // if there are missing turrets
        {
            var ii = 0;

            for (ii = 0; ii < locationOfMissingTurrets.length; ii++) {
                //topic.AddPhrase($p0011, { str: locationOfMissingTurrets[i].repairMaterialPrice.components_structural, mech: locationOfMissingTurrets[i].repairMaterialPrice.components_mechanical, elec: locationOfMissingTurrets[i].repairMaterialPrice.components_electronic, comp: locationOfMissingTurrets[i].repairMaterialPrice.components_composites });

                var structuralString = locationOfMissingTurrets[ii].buildPrice.components_structural;
                var mechanicalString = locationOfMissingTurrets[ii].buildPrice.components_mechanical;
                var electronicString = locationOfMissingTurrets[ii].buildPrice.components_electronic;
                var compositesString = locationOfMissingTurrets[ii].buildPrice.components_composites;

                if (PlayerHasEnoughComponents(locationOfMissingTurrets[ii].buildPrice)) {
                    //topic.AddPhrase("" + locationOfMissingTurrets[ii].loc);

                    topic.AddChoice(301 + ii, "Cost of building turret is" + " str " + structuralString + " mech " + mechanicalString + " elec " + electronicString + " comp " + compositesString);

                    //locationOfMissingTurretsToRepair.push(locationOfMissingTurrets[ii]);

                    counterToBuildTurrets++;
                }
                else {
                    //ORIGINAL => topic.AddPhrase("You do not have enough materials.");
                    //ORIGINAL => topic.AddChoice(20, $c0015); // That's a bit too expensive for me.
                }
            }

            currentIndex = ii;

            if (counterToBuildTurrets == 0) {
                topic.AddPhrase("You do not have enough materials.");
            }
        }
        else {
            topic.AddPhrase("There are no missing turrets.");
        }
    }

    else if (input >= 301 && input < 301 + currentIndex) {
        var current_station_id = ship.GetCurrentBase(PLAYER_SHIP);

        var station = generator.GetBaseByID(current_station_id);

        var system_id = station.system_id;

        var indexOfTurret = input - 301;

        RemoveComponents(locationOfMissingTurrets[indexOfTurret].buildPrice);

        var turretID = generator.AddNPCShipToSystem("", "Turret", 1, "special_human_turret_mk2", system_id, locationOfMissingTurrets[indexOfTurret].coordsX, locationOfMissingTurrets[indexOfTurret].coordsY, { class: "turret", non_talkable: true });

        var arrayOfTurretsAtStation = storage.GetGlobal("system_" + system_id + "_base_" + locationOfMissingTurrets[indexOfTurret].baseID + "_turrets");

        var arrayOfData = { loc: locationOfMissingTurrets[indexOfTurret].loc, id: turretID, sys_id: system_id, coordsX: locationOfMissingTurrets[indexOfTurret].coordsX, coordsY: locationOfMissingTurrets[indexOfTurret].coordsY, baseID: locationOfMissingTurrets[indexOfTurret].baseID, spawned: 1, xmltype: "special_human_turret_mk2" };

        arrayOfTurretsAtStation[locationOfMissingTurrets[indexOfTurret].index] = arrayOfData;

        storage.SetGlobal("system_" + system_id + "_base_" + locationOfMissingTurrets[indexOfTurret].baseID + "_turrets", arrayOfTurretsAtStation);
    }



    else if (input == 400)
    {
        var current_station_id = ship.GetCurrentBase(PLAYER_SHIP);
        var station = generator.GetBaseByID(current_station_id);
        var system_id = station.system_id;

        var arrayOfTurretsAtStation = storage.GetGlobal("system_" + system_id + "_base_" + current_station_id + "_turrets");

        var counterTurretSpawnedDamaged = 0;
        var counterTurretSpawnedTotal = 0;
        var counterTurretNotSpawned = 0;

        var locationOfSpawnedTurrets = [];

        locationOfSpawnedTurretsToRepair = [];
        locationOfMissingTurretsToRepair = [];

        counterToBuildTurrets = 0;
        locationOfMissingTurrets = [];

        for (var i = 0; i < arrayOfTurretsAtStation.length; i++) {
            // && ship.GetCurrentValue(arrayOfTurretsAtStation[i].id, "structure") < 0
            if (!generator.ShipExists(arrayOfTurretsAtStation[i].id))//!game.IsShipExists(arrayOfTurretsAtStation[i].id))//generator.ShipExists(arrayOfTurretsAtStation[i].id))
            {
                arrayOfTurretsAtStation[i].spawned == 0;

                var price = GetPriceForAction(arrayOfTurretsAtStation[i].xmltype, 6);

                var turretData = { loc: arrayOfTurretsAtStation[i].loc, index: i, xmltype: arrayOfTurretsAtStation[i].xmltype, buildPrice: price, coordsX: arrayOfTurretsAtStation[i].coordsX, coordsY: arrayOfTurretsAtStation[i].coordsY, baseID: arrayOfTurretsAtStation[i].baseID };

                locationOfMissingTurrets.push(turretData);
                counterTurretNotSpawned++;
            }
            else {

            }
        }

        if (counterTurretNotSpawned > 0) // if there are missing turrets
        {
            var ii = 0;

            for (ii = 0; ii < locationOfMissingTurrets.length; ii++) {
                //topic.AddPhrase($p0011, { str: locationOfMissingTurrets[i].repairMaterialPrice.components_structural, mech: locationOfMissingTurrets[i].repairMaterialPrice.components_mechanical, elec: locationOfMissingTurrets[i].repairMaterialPrice.components_electronic, comp: locationOfMissingTurrets[i].repairMaterialPrice.components_composites });

                var structuralString = locationOfMissingTurrets[ii].buildPrice.components_structural;
                var mechanicalString = locationOfMissingTurrets[ii].buildPrice.components_mechanical;
                var electronicString = locationOfMissingTurrets[ii].buildPrice.components_electronic;
                var compositesString = locationOfMissingTurrets[ii].buildPrice.components_composites;

                if (PlayerHasEnoughComponents(locationOfMissingTurrets[ii].buildPrice)) {
                    //topic.AddPhrase("" + locationOfMissingTurrets[ii].loc);

                    topic.AddChoice(401 + ii, "Cost of building turret is" + " str " + structuralString + " mech " + mechanicalString + " elec " + electronicString + " comp " + compositesString);

                    //locationOfMissingTurretsToRepair.push(locationOfMissingTurrets[ii]);

                    counterToBuildTurrets++;
                }
                else {
                    //ORIGINAL => topic.AddPhrase("You do not have enough materials.");
                    //ORIGINAL => topic.AddChoice(20, $c0015); // That's a bit too expensive for me.
                }
            }

            currentIndex = ii;

            if (counterToBuildTurrets == 0) {
                topic.AddPhrase("You do not have enough materials.");
            }
        }
        else {
            topic.AddPhrase("There are no missing turrets.");
        }
    }

    else if (input >= 401 && input < 401 + currentIndex)
    {
        var current_station_id = ship.GetCurrentBase(PLAYER_SHIP);

        var station = generator.GetBaseByID(current_station_id);

        var system_id = station.system_id;

        var indexOfTurret = input - 401;

        RemoveComponents(locationOfMissingTurrets[indexOfTurret].buildPrice);

        var turretID = generator.AddNPCShipToSystem("", "Turret", 1, "special_human_turret_mk3", system_id, locationOfMissingTurrets[indexOfTurret].coordsX, locationOfMissingTurrets[indexOfTurret].coordsY, { class: "turret", non_talkable: true });

        var arrayOfTurretsAtStation = storage.GetGlobal("system_" + system_id + "_base_" + locationOfMissingTurrets[indexOfTurret].baseID + "_turrets");

        var arrayOfData = { loc: locationOfMissingTurrets[indexOfTurret].loc, id: turretID, sys_id: system_id, coordsX: locationOfMissingTurrets[indexOfTurret].coordsX, coordsY: locationOfMissingTurrets[indexOfTurret].coordsY, baseID: locationOfMissingTurrets[indexOfTurret].baseID, spawned: 1, xmltype: "special_human_turret_mk3" };

        arrayOfTurretsAtStation[locationOfMissingTurrets[indexOfTurret].index] = arrayOfData;

        storage.SetGlobal("system_" + system_id + "_base_" + locationOfMissingTurrets[indexOfTurret].baseID + "_turrets", arrayOfTurretsAtStation);
    }

























    /*
	else if (input == 210)
	{
		var price = GetPriceForAction("special_human_turret", 4);
		
		//remove necessary components
		RemoveComponents(price);
		
		//remove all turrets
		ClearTurretsAroundStation(current_station_id);
		
		//spawn new turrets of the given type
		SpawnTurretsAroundStation(current_station_id, "special_human_turret");
		
		topic.AddPhrase($p0018); // Great! We will start the upgrading process at once.
	}
	
	//===================================
	// ========= Improved turrets =======
	//===================================
	else if (input == 300)
	{
		var number = GetNumberOfTurretsAroundStation(current_station_id);
		var tier = GetStationTurretType(current_station_id);
		
		//If there are no turrets, or only shitty turrets, so we can replace them
        if (number == 0 || tier == "special_human_turret")
        {
			var price = GetPriceForAction("special_human_turret_mk2", 4);
			
			topic.AddPhrase($p0019); // Sure thing. We can alwayse use some help installing better defenses for the station.
			topic.AddPhrase($p0020, { str: price.components_structural, mech: price.components_mechanical, elec: price.components_electronic, comp: price.components_composites }); // We will need the following components to construct and deploy the new turrets: %str% structural components, %mech% mechanical components, %elec% electronic components and %comp% composite components.
			
            if (PlayerHasEnoughComponents(price))
            {
				topic.AddChoice(310, $c0021); // I have all the necessary components. Here.
            }

			topic.AddChoice(20, $c0022); // That's a bit too expensive for me.
			
			return;
		}
		
		//If the same turrets are installed already
        if (tier == "special_human_turret_mk2")
        {
            if (number == 4)
            {
				topic.AddPhrase($p0021); // We already have these turrets installed. But perhaps you want to install upgraded turrets instead?
            }
            else
            {
				topic.AddPhrase($p0022); // We already have these turrets installed. But perhaps you want to repair them instead? Some of the turrets were lost during the last battle, so we could certainly use some help with the repairs.
			}
			topic.AddChoice(10, $c0023); // Let's see then...
			topic.AddChoice(999, $c0024); // Maybe next time...
			
			return;
		}
		
		//If there are better turrets installed
		topic.AddPhrase($p0023); // I'm afraid I must decline. We already have better turrets installed currently.
		topic.AddChoice(10, $c0025); // There's another thing...
	}
	else if (input == 310)
	{
		var price = GetPriceForAction("special_human_turret_mk2", 4);
		
		//remove necessary components
		RemoveComponents(price);
		
		//remove all turrets
		ClearTurretsAroundStation(current_station_id);
		
		//spawn new turrets of the given type
		SpawnTurretsAroundStation(current_station_id, "special_human_turret_mk2");
		
		topic.AddPhrase($p0024); // Great! We will start the upgrading process at once.
	}
	
	//===================================
	// ========= Advanced turrets =======
	//===================================
	else if (input == 400)
	{
		var number = GetNumberOfTurretsAroundStation(current_station_id);
		var tier = GetStationTurretType(current_station_id);
		
		//If there are no turrets, or only shitty T1/T2 turrets, so we can replace them
		if (number == 0 || tier == "special_human_turret" || tier == "special_human_turret_mk2") {
			var price = GetPriceForAction("special_human_turret_mk3", 4);
			
			topic.AddPhrase($p0025); // Sure thing. We can alwayse use some help installing better defenses for the station.
			topic.AddPhrase($p0026, { str: price.components_structural, mech: price.components_mechanical, elec: price.components_electronic, comp: price.components_composites }); // We will need the following components to construct and deploy the new turrets: %str% structural components, %mech% mechanical components, %elec% electronic components and %comp% composite components.
			
			if (PlayerHasEnoughComponents(price)) {
				topic.AddChoice(410, $c0026); // I have all the necessary components. Here.
			}
			topic.AddChoice(20, $c0027); // That's a bit too expensive for me.
			
			return;
		}
		
		//If the same turrets are installed already
		if (number == 4) {
			topic.AddPhrase($p0027); // We already have these turrets installed. And all of them are currently operational.
		} else {
			topic.AddPhrase($p0028); // We already have these turrets installed. But perhaps you want to repair them instead? Some of the turrets were lost during the last battle, so we could certainly use some help with the repairs.
			
			topic.AddChoice(10, $c0028); // Let's see then...
		}
		topic.AddChoice(999, $c0029); // Alright then.
	}
	else if (input == 410)
	{
		var price = GetPriceForAction("special_human_turret_mk3", 4);
		
		//remove necessary components
		RemoveComponents(price);
		
		//remove all turrets
		ClearTurretsAroundStation(current_station_id);
		
		//spawn new turrets of the given type
		SpawnTurretsAroundStation(current_station_id, "special_human_turret_mk3");
		
		topic.AddPhrase($p0029); // Great! We will start the upgrading process at once.
	}*/
}

//***************************************************************
// Functions 
//***************************************************************

/*
Tiers:
	special_human_turret - standard turrets
	special_human_turret_mk2 - t2 turrets
	special_human_turret_mk3 - t3 turrets
Number - how much is needed
*/
function GetPriceForAction(type, number) {
    switch (type) {
        case "special_human_turret":
            var price = {
                components_structural: 6 * number,
                components_mechanical: 3 * number,
                components_electronic: 2 * number,
                components_composites: 0 * number
            };
            break;
        case "special_human_turret_mk2":
            var price = {
                components_structural: 12 * number,
                components_mechanical: 6 * number,
                components_electronic: 3 * number,
                components_composites: 0 * number
            };
            break;
        case "special_human_turret_mk3":
            var price = {
                components_structural: 10 * number,
                components_mechanical: 8 * number,
                components_electronic: 5 * number,
                components_composites: 4 * number
            };
            break;
    }
    return price;
}

function PlayerHasEnoughComponents(price) 
{
	if (price.components_structural > 0) {
		var amount = ship.GetCargoAmount(PLAYER_SHIP, "components_structural");
		if (price.components_structural > amount)
			return false;
	}
	if (price.components_mechanical > 0) {
		var amount = ship.GetCargoAmount(PLAYER_SHIP, "components_mechanical");
		if (price.components_mechanical > amount)
			return false;
	}
	if (price.components_electronic > 0) {
		var amount = ship.GetCargoAmount(PLAYER_SHIP, "components_electronic");
		if (price.components_electronic > amount)
			return false;
	}
	if (price.components_composites > 0) {
		var amount = ship.GetCargoAmount(PLAYER_SHIP, "components_composites");
		if (price.components_composites > amount)
			return false;
	}
	return true;
}

function RemoveComponents(price)
{
    if (price.components_structural > 0)
    {
		ship.RemoveCargoByType(PLAYER_SHIP, "components_structural", price.components_structural);
    }

    if (price.components_mechanical > 0)
    {
		ship.RemoveCargoByType(PLAYER_SHIP, "components_mechanical", price.components_mechanical);
    }

    if (price.components_electronic > 0)
    {
		ship.RemoveCargoByType(PLAYER_SHIP, "components_electronic", price.components_electronic);
    }

    if (price.components_composites > 0)
    {
		ship.RemoveCargoByType(PLAYER_SHIP, "components_composites", price.components_composites);
	}
}
 
//Returns turret type for a given station, this is necessary to determine if turrets have been upgraded on a particular station or not
function GetStationTurretType(station_id)
{
	//station turrets id string to use in global storage
	var turret_id_string = "station.turrets." + station_id;

	if (storage.IsSetGlobal(turret_id_string))
	{
		//if such key exists, then get if from the storage
		var station_turret_type = storage.GetGlobal(turret_id_string);
    }
    else
    {
		//if it doesn't exists, then create new value
		var station_turret_type = "special_human_turret"; //the default turret xml id
    }

    return station_turret_type;
}

//Stores value to global storage with turret type for a given station, needed for ugrading/repairing logic
function SetStationTurretType(station_id, turret_type_xml_id)
{
	//station turrets id string to use in global storage
	var turret_id_string = "station.turrets." + station_id;
	//Save data to global storage
	storage.SetGlobal(turret_id_string, turret_type_xml_id);
}

function ClearTurretsAroundStation(station_id)
{
	//Get info about the station and system
	var station = generator.GetBaseByID(station_id);
	var system_id = station.system_id;
	
	//Get all turrets in a system
	var allTurretIds = game.GetSystemShipsOfClass(system_id, "turret");
	
	//foreach through all turrets that we found, if any
	for (var index in allTurretIds)
	{
		var turretId = allTurretIds[index];
		
		//Checking how far the object from the station, if it's higher than the desired radius, then - ignore it.
		//The reason it could be higher - if the turrets belong to another station, or to a jumpgate
		var distance = npc.GetDistanceToObj(turretId, station_id);
		if (distance > 25)
		{
			continue;
		}
		
		//So, then we have a turret that's right at the given station? If so - delete it.
		ship.RemoveShip(turretId);
	}
}

function SpawnTurretsAroundStation(station_id, npc_xml_id)
{
	//Get info about the station
	var station = generator.GetBaseByID(station_id);
	
	//Get info about the system
	var system_id = station.system_id;
	var system_info = generator.GetSystemByID(system_id);
	
	//Prepare some values
	var turret_level = utils.Clamp(system_info.danger_level, 5, 100); //simply danger level of the system, but no less than 5
	var args = { sys_id: system_id }; //we have to do this bs, since the scripting is written ass backwards
	var station_coords = { x: station.coord_x, y: station.coord_y }; //again, some high level bs
	var faction = system_info.faction; //faction of the system
	
	//Now add the turrets
	NpcGenerator.SpawnTurretsOfType(
		args,
		station_coords, //coordinates of the station
		4, //number of turrets, we don't need to change that, since the default is 4
		turret_level, //level of the turret
		20, //radius at which turrets will be spawned around station, the default value is 20
		"Turret", //This is AI behaviour definition, musn't change that
		npc_xml_id, //This is the npc xml id, basically what xml preset to use, the default for the game is "special_human_turret"
		faction, //Specifies to which faction the turrets will belong
		{ class: "turret", non_talkable: true }, //defines that this is a turret, rather than normal npc
		0 //angle offset, we don't need it
	);
}

function GetNumberOfTurretsAroundStation(station_id)
{
	//Get info about the station
	var station = generator.GetBaseByID(station_id);
	var system_id = station.system_id;
	
	//Get all turrets in a system
	var allTurretIds = game.GetSystemShipsOfClass(system_id, "turret");
	
	var count = 0;
	
	//foreach through all turrets that we found, if any
	for (var index in allTurretIds)
	{
		var turretId = allTurretIds[index];
		
		var distance = npc.GetDistanceToObj(turretId, station_id);
		if (distance > 25)
		{
			continue;
		}
		
		count++;
	}
	
	return count;
}

/*
var station = generator.GetBaseByID(station_id);
Returns:
	public double coord_x;
	public double coord_y;
	public string faction;
	public uint id;
	public string name;
	public int tech_level;
	public uint system_id;
	public string xml_type_id;

var system_info = generator.GetSystemByID(system_id);
Returns:
	public double coord_x;
	public double coord_y;
	public int danger_level;
	public string faction;
	public string name;
	public int seed;
	public int tech_level;

SetGlobal(string keyName, object value):void
Set(string tableName, string keyName, object value):void
RemoveGlobal(string keyName):void
Remove(string tableName, string keyName):void
IsSetGlobal(string keyName):bool
IsSet(string tableName, string keyName):bool
GetGlobal(string keyName):object
Get(string tableName, string keyName):object
*/













/*else if (input == 110)
{
	var tier = GetStationTurretType(current_station_id);
	var price = GetPriceForAction(tier, 4 - number);
		
	//pay for the repairs (remove necessary components)
	RemoveComponents(price);
		
	//remove all turrets
	ClearTurretsAroundStation(current_station_id);
		
	//spawn new turrets of the same turret type as it was before
	SpawnTurretsAroundStation(current_station_id, tier);
		
	topic.AddPhrase($p0012); // Great! We will start repairs at once!
}*/













        /*
        if (arrayOfTurretsAtStation.length > 0)
        {

        }
        else
        {
            topic.AddPhrase($p0007); // Uhh... all of our turrets have been destroyed. There isn't much left to repair, I'm afraid...
            topic.AddPhrase($p0008); // But perhaps you'd like to help us install new turrets instead?

            topic.AddChoice(10, $c0012); // Let's see then...
            topic.AddChoice(999, $c0013); // Maybe next time...
        }*/


        /*//if (counterSpawned != arrayOfTurretsAtStation.length)
        {
            //some turrets are destroyed. the rest might be damaged or not.
            //for (var i = 0; i < locationOfSpawnedTurrets.length; i++)
            //{
            //
            //}
            //generator.ShipExists(ceData.ts) || ship.GetCurrentValue(ceData.ts, "structure") <= 0

            /*
            var tier = GetStationTurretType(current_station_id);
            var price = GetPriceForAction(tier, 4 - number);

            topic.AddPhrase($p0009); // Sure thing. We can always use some help maintaining the defenses of our station.
            topic.AddPhrase($p0010); // According to our current turret type and the number of still operational turrets...
            topic.AddPhrase($p0011, { str: price.components_structural, mech: price.components_mechanical, elec: price.components_electronic, comp: price.components_composites }); // We will need the following components to undertake the repairs: %str% structural components, %mech% mechanical components, %elec% electronic components and %comp% composite components.

            if (PlayerHasEnoughComponents(price))
            {
                topic.AddChoice(110, $c0014); // I have all the necessary components. Here.
            }

            topic.AddChoice(20, $c0015); // That's a bit too expensive for me.
        }
        else if (counterSpawned == 0)
        {
            topic.AddPhrase($p0007); // Uhh... all of our turrets have been destroyed. There isn't much left to repair, I'm afraid...
            topic.AddPhrase($p0008); // But perhaps you'd like to help us install new turrets instead?

            topic.AddChoice(10, $c0012); // Let's see then...
            topic.AddChoice(999, $c0013); // Maybe next time...
        }
        else
        {
            topic.AddPhrase($p0005); // I certainly appreciate the gesture, but it looks like all of our turrets are still in place. So, there is no need for repairing.
            topic.AddPhrase($p0006); // But perhaps you'd like to help us install new turrets instead?

            topic.AddChoice(10, $c0010); // Let's see then...
            topic.AddChoice(999, $c0011); // Maybe next time...
        }*/











		/*var number = GetNumberOfTurretsAroundStation(current_station_id);
		
        if (number = 4)
        {

			topic.AddPhrase($p0005); // I certainly appreciate the gesture, but it looks like all of our turrets are still in place. So, there is no need for repairing.
			topic.AddPhrase($p0006); // But perhaps you'd like to help us install new turrets instead?
			
			topic.AddChoice(10, $c0010); // Let's see then...
            topic.AddChoice(999, $c0011); // Maybe next time...

        }
        else if (number == 0)
        {

			topic.AddPhrase($p0007); // Uhh... all of our turrets have been destroyed. There isn't much left to repair, I'm afraid...
			topic.AddPhrase($p0008); // But perhaps you'd like to help us install new turrets instead?
			
			topic.AddChoice(10, $c0012); // Let's see then...
            topic.AddChoice(999, $c0013); // Maybe next time...

        }
        else
        {

			var tier = GetStationTurretType(current_station_id);
			var price = GetPriceForAction(tier, 4 - number);
			
			topic.AddPhrase($p0009); // Sure thing. We can alwayse use some help maintaining the defenses of our station.
			topic.AddPhrase($p0010); // According to our current turret type and the number of still operational turrets...
			topic.AddPhrase($p0011, { str: price.components_structural, mech: price.components_mechanical, elec: price.components_electronic, comp: price.components_composites }); // We will need the following components to undertake the repairs: %str% structural components, %mech% mechanical components, %elec% electronic components and %comp% composite components.
			
            if (PlayerHasEnoughComponents(price))
            {
				topic.AddChoice(110, $c0014); // I have all the necessary components. Here.
            }

            topic.AddChoice(20, $c0015); // That's a bit too expensive for me.

        }*/




        /*var number = GetNumberOfTurretsAroundStation(current_station_id);
		var tier = GetStationTurretType(current_station_id);
		
		if (number > 0) {
			if (tier == "special_human_turret") {
				if (number == 4) {
					topic.AddPhrase($p0013); // We already have these turrets installed. But perhaps you want to install upgraded turrets instead?
				} else {
					topic.AddPhrase($p0014); // We already have these turrets installed. But perhaps you want to repair them instead? Some of the turrets were lost during the last battle, so we could certainly use some help with the repairs.
				}
				topic.AddChoice(10, $c0016); // Let's see then...
				topic.AddChoice(999, $c0017); // Maybe next time...
			} else {
				topic.AddPhrase($p0015); // I'm afraid I must decline. We already have better turrets installed currently.
				
				topic.AddChoice(10, $c0018); // There's another thing...
			}
		} else {
			var price = GetPriceForAction("special_human_turret", 4);
			
			topic.AddPhrase($p0016); // Sure thing. We can alwayse use some help improving the defenses of our station.
			topic.AddPhrase($p0017, { str: price.components_structural, mech: price.components_mechanical, elec: price.components_electronic, comp: price.components_composites }); // We will need the following components to construct and deploy the new turrets: %str% structural components, %mech% mechanical components, %elec% electronic components and %comp% composite components.
			
			if (PlayerHasEnoughComponents(price)) {
				topic.AddChoice(210, $c0019); // I have all the necessary components. Here.
			}
			topic.AddChoice(20, $c0020); // That's a bit too expensive for me.
		}*/