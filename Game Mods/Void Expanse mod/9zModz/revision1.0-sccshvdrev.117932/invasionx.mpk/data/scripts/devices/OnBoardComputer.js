
/*
	Script can contain following functions:
		OnUpdateCache - called when device is equipped or initialized, or some values are changed
		OnStart - called when device is activated (for all)
		OnFrame - called every frame (only for "per frame" mode)
		OnFinished - called when device effect should be applied (only for "per frame" and "on complete")
*/
include(pickByChance.js);
using(ship);
using(console);
using(game);
using(generator);
using(relations);
using(player);
//using(actions);
using(storage);


function OnAIInited()
{
	
}

function OnInit()
{
}

function OnUpdateCache(args)
{

}

function OnEquipped(args)
{

    
}

var droneReleaseCounter = 0;
var numberOfDrones = "numberOfDrones";
//var numberOfDronesReleased = ;
var numberOfDronesInCargo = 5;
//var listOfDrones = [];
//var reReleaser = 0;

function OnStart(args)
{
	var shipID = args.ship_id;
	var name = game.GetShipOwner(shipID);

	//var coords = ship.GetCoordinates(shipID);
	//var sys_id = ship.GetSystemID(shipID);

	//console.Print(name);

    player.StartDialogue(name,"OnBoardComputer"); //mmh. the drone manager... where the hell do i spawn him again... do i spawn him in every system or just in one system?... do i spawn him in every stations of every systems?
}

function OnFrame(args)
{

}

function OnFinished(args)
{

}

function OnCancel(args)
{

}







































/*
 Script can contain following functions:
 OnUpdateCache - called when device is equipped or initialized, or some values are changed
 OnStart - called when device is activated (for all)
 OnFrame - called every frame (only for "per frame" mode)
 OnFinished - called when device effect should be applied (only for "per frame" and "on complete")
 

using(ship);
using(console);
using(generator);
using(game);
using(visual);
using(actions);



function OnUpdateCache(args)
{
	var inf = generator.GetSystemByID(args.sys_id);
    //var id = generator.AddContainer(1, 0, 0, "Container01", "DropList01");
	//var ship_id = player.GetShipOfPlayer(args);
	//console.Print(is_player);
	console.Print(inf);

}







function OnStart(args)
{
	//var ship_id = player.GetShipOfPlayer(ship);
	//var name = game.GetShipOwner(args);
	//var is_player = game.IsShipPlayerControlled(args);
	//var coords = args.GetCoordinates(args);

	var inf = generator.GetSystemByID(args.sys_id);
    //var id = generator.AddContainer(1, 0, 0, "Container01", "DropList01");
	//var ship_id = player.GetShipOfPlayer(args);
	//console.Print(is_player);
	console.Print(inf);
	//console.Print(coords);
	//console.Print(ship_id);



}

function OnFrame(args)
{
	var inf = generator.GetSystemByID(args.sys_id);
    //var id = generator.AddContainer(1, 0, 0, "Container01", "DropList01");
	//var ship_id = player.GetShipOfPlayer(args);
	//console.Print(is_player);
	console.Print(inf);
}

function OnFinished(args)
{

}

function OnCancel(args)
{

}*/