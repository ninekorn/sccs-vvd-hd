using(npc);
using(console);
using(player);
using(ship);
using(game);
using(generator);
using(storage);
using(station);
using(items);
using(timer);
using(server);

include(SC_AI_Drone_Manager_Utilities.js);

function OnAIInited() {

    var npcDecisionsPerSeconds = 19;
    npc.SetDecisionsPerSecond(SHIP_ID, npcDecisionsPerSeconds);
    var player_id = npc.GetTag(SHIP_ID, "ownerPlayerShipId");
    storage.SetGlobal("npcDecisionsPerSeconds" + player_id, npcDecisionsPerSeconds);

    //var player_id = player.GetShipOfPlayer(args.name);
    /*if (!storage.IsSetGlobal("npcDecisionsPerSeconds" + player_id))
    {

    }*/

    npc.SetBehavior(SHIP_ID, "avoid_asteroids", false);
    npc.SetBehavior(SHIP_ID, "avoid_ships", false);
    npc.SetBehavior(SHIP_ID, "avoid_bases", false);
    npc.SetBehavior(SHIP_ID, "avoid_debris", false);
    npc.StopEvasion(SHIP_ID);

    //NpcLib.SetupDefaultWeaponUsage(SHIP_ID);
    npc.SetWeaponUsage(SHIP_ID, "Ray", 0, 7, 0.001);
    npc.SetWeaponUsage(SHIP_ID, "BulletOrMissile", 2, 7, 0.001);
    npc.SetWeaponUsage(SHIP_ID, "DirectHit", 2, 7, 0.001);
}

var player_id;
var initStartingStuff;
var droneIndex;
var playerName;


var signs_GoSwitchSwitch = 0;

var goSignMainFrameCounter = 0;
var goSignMainFrameCounterMax = 5;

var stopSignMainFrameCounter = 0;
var stopSignMainFrameCounterMax = 1200; //1200 == 60 seconds for a stable thread and thats why i will use the timer class soon, to have a stable frame for some stuff in this mod. when approx setDecisionsPerSeconds is at 20 == 1200 frames for a 60 seconds wait time.


var DockingPointMainFrameCounter = 0;
var DockingPointMainFrameCounterMax = 0;



var baseID;

var mainFrameCounter = 0;

function Decision(args)
{
	player_id = npc.GetTag(SHIP_ID, "ownerPlayerShipId");

}

function OnDied(args)
{
    droneIndex = npc.GetTag(SHIP_ID, "droneIndex");
    currentObjective = npc.GetCurrentObjective(SHIP_ID);

    SC_AI_Drone_Manager_Utilities.setRemovedFromGlobalDroneArray(currentObjective.droneIndex, currentObjective.wepDistType, currentObjective.wepPropType,currentObjective.formation);
}
function OnTakeDamage(args) {
    //NpcLib.StandardOnTakeDamage(SHIP_ID, args);
}

function OnFrame(args) {

}

/*function OnTakeDamage(args)
{
    NpcLib.StandardOnTakeDamage(SHIP_ID, args);
}*/

function OnUpdateCache(args) {

}

/*function OnCalculateShipParameters(args)
{
    console.Print("OnCalculateShipParameters");
}

function OnCalculateInventoryCache(args) {
    console.Print("OnCalculateInventoryCache");
}

function OnCalculateSkillsCache(args) {
    console.Print("OnCalculateSkillsCache");
}

function OnCalculateBuffsCache(args) {
    console.Print("OnCalculateBuffsCache");
}
*/

/*function OnDied(args) {

}

function OnFrame(args)
{

}

function OnFinished(args) {

}

function OnCancel(args) {

}

function OnTakeDamage(args) {

}*/