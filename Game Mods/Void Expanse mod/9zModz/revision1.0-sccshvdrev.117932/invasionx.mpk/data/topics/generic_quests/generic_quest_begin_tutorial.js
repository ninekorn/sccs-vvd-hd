/*
 ======================================================================
 
 Introductory quest - part of introductory questline
 
 "Scurvy package" -> "Mining tutorial"
 
 ======================================================================
 */


using(relations);
using(npc);
using(console);
using(ship);
using(generator);
using(game);
using(items);
using(player);
using(storage);

// variables
var quest_system_id = 0;
var quest_base_id = 0;
var quest_scurvy_id = 0;

function OnCheckRequirements()
{
    var npc_id = topic.GetCurrentNpcShipId();

    if (npc.GetTag(npc_id, "unique_id") == "oneeyedjoe")
    {
        return true;
    }
    else if (npc_id == quest_scurvy_id)
    {
        return true;
    }

    return false;
}

function OnStart()
{
}

function OnGetTitle()
{
    var state = topic.GetState();
    var npc_id = topic.GetCurrentNpcShipId();

    if (npc.GetTag(npc_id, "unique_id") == "oneeyedjoe")
    {
        if (state == 0)
        {
            topic.SetTitle($t0001); // Hello! I'm new here.
        }
        else if (state == 100)
        {
            topic.SetTitle($t0002); // Get a better ship
        }
        else if (state == 200)
        {
            topic.SetTitle($t0003); // Get the ship
        }
        else if (state == 300)
        {
            topic.SetTitle($t0004); // Got the package
        }
        else if (state == 350)
        {
            topic.SetTitle($t0005); // Consumables
        }
        else if (state == 360)
        {
            topic.SetTitle($t0006); // Got some ore
        }
        else if (state == 380 || state == 390)
        {
            topic.SetTitle($t0010); // Final task
        }
        else if (state == 400)
        {
            topic.SetTitle($t0007); // What's next?
        }
    }
    else
    {
        if (state < 300)
        {
            topic.SetTitle($t0008); // Package
        }
        else
        {
            topic.SetTitle($t0009); // Fix your life
        }
    }
}

/*
 =====================================================================
 Dialogue with Joe
 =====================================================================
 */
function OnDialogue()
{    


    var input = topic.GetInput();
    var state = topic.GetState();

    var npc_id = topic.GetCurrentNpcShipId();

    //dialogue with scurvy
    var tagClass = npc.GetTag(npc_id, "class");
    var questtaker = npc.GetTag(npc_id, "questtaker");
    if (questtaker == PLAYER_SHIP && tagClass == "pirate")
    {
        OnDialogueWithScurvy(input, state, npc_id);
        return;
    }

    if (state == 0)
    {
        if (input == NO_INPUT)
        {   
            storage.Set("_system_common", PLAYER_SHIP + "_completed_tutorial", 1);
            game.SendGalaxyUpdate(PLAYER_SHIP); // now player is able to jump from the starting system
            game.SendNotification(PLAYER, $n0001, $n0002); // Access codes to the Jumpgate network received.

            topic.AddPhrase($p0001); // Hey, kid! You're from one of those farmers' planets down there right? What brought you here?

            topic.AddChoice(10, $c0001); // I've come to help fight aliens.
            topic.AddChoice(11, $c0002); // I've come looking for adventure.
            topic.AddChoice(12, $c0003); // I want to make a fortune.
        }
        else if (input == 10 || input == 11 || input == 12)
        {
            if (input == 10)
            {
                topic.AddPhrase($p0002); // Huh, stole your father's rusty piece of junk and think you're a fighter now? The Galaxy is a dangerous place, you know.
            }
            else if (input == 11)
            {
                topic.AddPhrase($p0003); // Well, with the Xengatarn around you're definitely gonna get yourself an adventure. The Galaxy is a dangerous place, you know.
            }
            else if (input == 12)
            {
                topic.AddPhrase($p0004); // Huh, you want to make money in a time like this?
            }
            topic.AddPhrase($p0005); // We're in the middle of a war. If you want to achieve anything, you should consider joining a faction - to have someone watch your back.

            topic.AddChoice(20, $c0004); // Join a faction?

            topic.AddTopic("generic_factions_factions");
        }
        else if (input == 20)
        {
            topic.AddPhrase($p0006); // Yes. See that guy over there? With green markings on his coat? He's an Order recruiter. If you wanna join the Order - he's your best shot. But he won't take you seriously unless you have a decent ship. I can help you with that.
            topic.AddPhrase($p0007); // Of course, there are other factions. Since you're new here, I can fill you in with the current situation if you want. But really, I think only the Order has any chance of ever winning this war.

            topic.AddChoice(30, $c0005); // Why are you helping me?

            topic.AddTopic("generic_races_alien_races");

            if (!topic.HasQuest("join_faction"))
            {
                topic.QuestStart("join_faction", $q0001); // Join a faction
                topic.QuestAddLog("join_faction", $q0004); // In order to achieve something in this world, I should join a faction.
            }
        }
        else if (input == 30)
        {
            topic.AddPhrase($p0008); // I don't really know. Maybe I see something in your eyes, that fire that I used to have...

            topic.SetState(100);

            topic.AddTopic("generic_personal_one_eyed_joe");
            topic.QuestSetState("startquest", QuestState.Finished);
            topic.RefreshTopics();

            topic.AddChoice(1, $c0006); // Okay, can you get me a better ship?
        }
    }
    else if (state == 100)
    {
        if (input == NO_INPUT || input == 1)
        {
            topic.AddPhrase($p0009); // Better ship? Hmmm... Okay, here's the deal. I've got a couple of old Banshee hulls here on this station. Fixed them up myself, you know. Flying like an eagle! At least, in theory.

            topic.AddPhrase($p0010); // But first, do me a favor, would you? I'm having some trouble with one of the local pirates. Name is Scurvy. He hijacked one of my caravans and stole a special container from me. Find him and convince him to give back what he stole.

            topic.AddChoice(2, $c0007); // And then what?
        }
        else if (input == 2)
        {
            topic.AddPhrase($p0011); // Bring the package back to me, and you'll get your new Banshee. Well, not completely new, but it will do.
            topic.AddPhrase($p0012); // Oh! And one very important thing! Don't forget to use your ship's cruise mode, usually activated by [C], unless you changed the controls. It will make long journeys much, much faster. Give it a try!

            // pick coordinates
            // and spawn scurvy
            var ship_id = topic.GetCurrentNpcShipId();
            var sys_id = ship.GetSystemID(ship_id);

            //set vars
            quest_system_id = sys_id;
            quest_base_id = ship.GetCurrentBase(ship_id);

            var ret = SpawnScurvy();

            topic.AddPhrase($p0013); // Anyway, here. I've got a tracer on this package. It will help you find Scurvy. Just take a look at your navigation map, and you will see him.

            topic.SetState(200);
            topic.RefreshTopics();
            topic.QuestStart(TOPIC_ID, $q0002); // Package for a Banshee
            topic.QuestAddMark(TOPIC_ID, sys_id);
            topic.QuestAddLocalMarkObject(TOPIC_ID, sys_id, ret.id);
            topic.QuestAddLog(TOPIC_ID, $q0005); // I should find the pirate named Scurvy and retrieve the package he stole from One-Eyed Joe. I should also try using the cruise mode of my ship. It will make the trip much faster.
        }
    }
    else if (state == 200)
    {
        if (input == NO_INPUT)
        {
            topic.AddPhrase($p0014); // I already told you, you'll get your ship no sooner than I get my package. Gotta stick to the rules of the game, you know.

            topic.AddChoice(999, $c0008); // Okay, I got it.
            topic.AddChoice(1, $c0009); // How do I do it again?
        }
        else if (input == 1)
        {
            topic.AddPhrase($p0015); // Just go out there and convince Scurvy to give back my package. You can use the tracker (yellow marker) on your radar to see where you need to go.
        }
    }
    else if (state == 300)
    {
        if (input == NO_INPUT)
        {
            topic.AddPhrase($p0016); // Ah, I see you got my package. Reasoned with Scurvy, did you? Good job. These pirates are getting worse by the hour. Damn Morgulis, he gives us a lot of trouble these days.

            topic.AddPhrase($p0017); // As I've promised, your new hull is waiting for you in your storage. As you can see, it's much better then yours, and it has two weapon mounts.

            topic.AddTopic("generic_advice_better_equipment");

            topic.AddChoice(1, $c0010); // Two weapon mounts?
        }
        else if (input == 1)
        {
            topic.AddPhrase($p0018); // Yes, it means you can mount two weapons at the same time.
            topic.AddPhrase($p0019); // And by the way, you can bind your weapons to different mouse buttons. There's a special control for that in your inventory next to the weapon slots.
            topic.AddPhrase($p0020); // It's incredibly important for you to learn how to assign weapons to different groups!

            topic.AddChoice(2, $c0011); // Mouse buttons? Joe, I think you should stop drinking.
        }
        else if (input == 2)
        {
            topic.AddPhrase($p0021); // Yeah, you're probably right, kid...

            // add next quest
            topic.AddPhrase($p0022); // Okay, you'll probably need some consumables to keep your ship together during the fight. Right?
            topic.SetState(350);
            topic.RefreshTopics();

            topic.QuestAddLog(TOPIC_ID, $q0006); // I successfully completed Joe's task. He gave me a new ship hull as promised.
            topic.QuestRemoveMarkers(TOPIC_ID);
            topic.QuestSetState(TOPIC_ID, QuestState.Finished);

            //removing scurvy
            ship.RemoveShip(quest_scurvy_id);

            player.AddQuestRewardExperience(PLAYER, 500);

            ship.RemoveItemByType(PLAYER_SHIP, "quest_object_joe_package");
            ship.AddItemToStorage(PLAYER_SHIP, "hull_banshee", 1);

            // destroy scurvy
            generator.RemoveShip(quest_scurvy_id);

            topic.AddChoice(150, $c0012); // Consumables?
        }
    }
    else if (state == 350)
    {
        if (input == NO_INPUT || input == 150)
        {
            topic.AddPhrase($p0023); // Yeah, it's a special container that you put into your ship's consumable slots. These allow you to quickly restore shields, repair your hull, replenish fuel and energy, and even restock ammunition.
            topic.AddPhrase($p0024); // These containers are rather expensive, but I can give you a couple of them in exchange for a little service.

            topic.AddChoice(1, $c0013); // What kind of service?
        }
        else if (input == 1)
        {
            topic.AddPhrase($p0025); // You see, here in the Far Colonies, there are a lot of really valuable minerals. That's the reason why the Federation tried to expand in this direction in the first place. Minerals from here can be used for almost anything: weapons crafting, station construction, medicinal devices, etc.
            topic.AddPhrase($p0026); // These minerals can be extracted from almost any asteroid around here with a special mining device.
            topic.AddPhrase($p0027); // I'm a little short on miners right now, so you can help me with that. Bring me 50 pieces of glepsite ore - and I'll give you, let's say, five consumables to mend your ship. Deal?

            topic.AddChoice(2, $c0014); // Sure, that sounds fair.

            // remove glepsite ore from store, otherwise player can cheat this task too easily
            var shop_container_id = items.GetStationShopContainerId(quest_base_id);
            var shop_glepsite_amount = items.GetCargoAmount(shop_container_id, "ore_glepsite");
            if (shop_glepsite_amount > 0)
            {
                items.RemoveCargo(shop_container_id, "ore_glepsite", shop_glepsite_amount);
            }
        }
        else if (input == 2)
        {
            topic.AddPhrase($p0028); // Then you'd better start mining before I change my mind!
            topic.AddPhrase($p0029); // And don't forget to buy and equip a Civilian Miner Device from the store.

            topic.QuestStart("tut_mining", $q0003); // Mining for consumables
            topic.QuestAddMark("tut_mining", quest_system_id);
            topic.QuestAddLocalMarkObject("tut_mining", quest_system_id, quest_base_id);
            topic.QuestAddLog("tut_mining", $q0007); // I should buy a mining device, equip it, and try to extract 50 pieces of glepsite ore from nearby asteroids. Then I should bring it back to One-eyed Joe and collect my reward.
            topic.SetState(360);
            topic.RefreshTopics();
        }
    }
    else if (state == 360)
    {
        var hasCargo = ship.HasCargoAmount(PLAYER_SHIP, "ore_glepsite", 50);
        if (hasCargo)
        {
            topic.AddPhrase($p0030); // You're already back? That's the spirit! Thanks for the ore. I'll put it to good use. Here are your consumables, as I promised! Don't forget to equip them, though.
            topic.AddPhrase($p0031); // Well, I think you're ready for the final task!

            ship.RemoveCargoByType(PLAYER_SHIP, "ore_glepsite", 50);
            ship.AddItem(PLAYER_SHIP, "consumable_structure", 5);

            topic.QuestAddLog("tut_mining", $q0008); // I've completed Joe's task, and he gave me my reward, just as we agreed.
            topic.QuestSetState("tut_mining", QuestState.Finished);
            player.AddQuestRewardExperience(PLAYER, 500);

            topic.SetState(380);
            topic.RefreshTopics();

            topic.QuestStart("tut_hacking", $q0012); // Final task
            topic.QuestAddMark("tut_hacking", quest_system_id);
            topic.QuestAddLocalMarkObject("tut_hacking", quest_system_id, quest_base_id);
            topic.QuestAddLog("tut_hacking", $q0013); // Joe said that he has a new task for me, which should be the last one. I need to ask him about it.

        }
        else
        {
            if (input == NO_INPUT)
            {
                topic.AddPhrase($p0032); // Got 50 pieces of glepsite ore? No? Well, take your time.

                topic.AddChoice(999, $c0015); // Okay.
                topic.AddChoice(1, $c0016); // How do I mine asteroids, again?
            }
            else if (input == 1)
            {
                topic.AddPhrase($p0033); // Okay, listen again. And pay attention this time. First you need to get a mining device. You can use a civilian version for now. It's available for free in the store on this station. Just grab one.
                topic.AddPhrase($p0034); // Once you have it, you need to equip it into your device slot. Then just undock from the station and find an asteroid rich in glepsite.
                topic.AddPhrase($p0035); // When you find an asteroid, make sure to stop your ship close to it and use the device on the asteroid. You'll see the mining beam activate. Do that several times until you get enough glepsite. Then return to me.
            }
        }
    }
    else if (state == 380)
    {
        if (input == NO_INPUT)
        {
            topic.AddPhrase($p0051); // Yes. There are gate access keys you need to leave this star system, ok? I'm pretty sure I know how you can get them!
            topic.AddPhrase($p0052); // I heard recently that pirates have been using one of the old terminals here, in this very system, to store their plunder.
            topic.AddPhrase($p0053); // You'll help me teach them a lesson by taking all their loot! And in the process you'll also get access to the gate network.

            if (IsTerminalHacked())
            {
                // already hacked
                topic.AddChoice(2, $c0030); // Wait, Joe... I recently hacked an old terminal in this system...
                topic.SetState(390);
            }
            else
            {
                topic.AddChoice(1, $c0029); // So, what do I need to do?
            }
        }
        else if (input == 1)
        {
            topic.AddPhrase($p0054); // Normally it'd be pretty difficult to break into one of these terminals.
            topic.AddPhrase($p0055); // But it seems they completely ruined the security system in the process of trying to hack it, so it should be a piece of cake for you.
            topic.AddPhrase($p0056); // I've marked the location on your system map. Now go! When you're done, come see me and I'll show you how to use those gate codes.

            var terminal_id = storage.GetGlobal("starting_system_ergrek_terminal_id");

            topic.QuestRemoveMarkers("tut_hacking");
            topic.QuestAddMark("tut_hacking", quest_system_id);
            topic.QuestAddLocalMarkObject("tut_hacking", quest_system_id, terminal_id); //marker to terminal
            topic.QuestAddLog("tut_hacking", $q0014); // I need to find an old terminal which is currently used by pirates as their secret stash location. Then I need to hack it and grab all the loot inside. This should give me the access codes to the gate network allowing me to leave this star system. I should report to Joe when I'm done with the hack.

            //binding "successful terminal hacking" trigger
            topic.Bind("onErgrekTerminalHacked", "ReturnToJoeAfterHacking", { obj_id: terminal_id });

            topic.SetState(390);
        }
    }
    else if (state == 390)
    {
        if (!IsTerminalHacked())
        {
            topic.AddPhrase($p0057); // Hmm? I already told you to go and hack the old terminal. Do you want the gate codes or not? Go!
        }
        else
        {
            topic.AddPhrase($p0058); // Good job. I had no doubt that you'd be able to hack that thing! Now about the gate codes.
            topic.AddPhrase($p0059); // These codes are entered into your ship's computer automatically. When you want to jump through a gate, simply activate the gate and the software will handle everything for you!
            topic.AddPhrase($p0060); // Good luck with your adventures, my friend!

            topic.QuestAddLog("tut_hacking", $q0015); // Joe explained to me how to use the access codes to travel through the gate network. Now I can finally leave this star system!
            topic.QuestRemoveMarkers("tut_hacking");
            topic.QuestSetState("tut_hacking", QuestState.Finished);

            //when completed all Joe's quests
            SetTutorialCompleted();
            game.GiveAchievement(PLAYER_SHIP, "complete_joe_quest");

            player.AddQuestRewardExperience(PLAYER, 500);

            topic.SetState(400);
			topic.RefreshTopics();
        }
    }
    else if (state == 400)
    {
        topic.AddPhrase($p0036); // Next? Well, it's up to you! As they say, you can be whatever you wanna be. Got it?
        topic.AddPhrase($p0037); // But I really think you should join a faction. You won't get far unless you join one of the big powers here. Order might be a good option for you.
    }
}

function IsTerminalHacked()
{
    var terminal_id = storage.GetGlobal("starting_system_ergrek_terminal_id");
    if (terminal_id != undefined)
    {
        var db_key = "terminal_hacked_id" + terminal_id + "_shipId_" + PLAYER_SHIP;
        return "1" == storage.GetGlobal(db_key);
    }

    console.Print("Warning: the terminal is not available in the starting system (playing in the world created with v1.2.9 or previous?). We will assume the terminal is hacked by the player - it's best what we can do now.")
    return true;
}

function ReturnToJoeAfterHacking()
{
    //hacking complete
    //new quest markers
    topic.QuestRemoveMarkers("tut_hacking");
    topic.QuestAddMark("tut_hacking", quest_system_id);
    topic.QuestAddLocalMarkObject("tut_hacking", quest_system_id, quest_base_id);
    topic.QuestAddLog("tut_hacking", $q0016); // I hacked the terminal successfully. Now I should report to Joe, so he can explain to me how to use the gate network.

    //exp for hacking
    player.AddQuestRewardExperience(PLAYER, 100);
}

// open jumpgates for the player
function SetTutorialCompleted()
{
    // add topics (in case of fast-tutorial completition)
    topic.AddTopic("generic_factions_factions");
    topic.AddTopic("generic_races_alien_races");
    topic.AddTopic("generic_personal_one_eyed_joe");
    topic.AddTopic("generic_advice_better_equipment");
    storage.Set("_system_common", PLAYER_SHIP + "_completed_tutorial", 1);
    game.SendGalaxyUpdate(PLAYER_SHIP); // now player is able to jump from the starting system
    game.SendNotification(PLAYER, $n0001, $n0002); // Access codes to the Jumpgate network received.
}

/*
 =====================================================================
 Dialogue with scurvy
 =====================================================================
 */
function OnDialogueWithScurvy(input, state, shipId)
{
    if (state < 300)
    {
        if (input == NO_INPUT)
        {
            topic.AddPhrase($p0038); // What package? I don't know anything about your package... This is an insult! I'm an honest tradesman!

            topic.AddChoice(1, $c0017); // Give me the package you stole, now!
            topic.AddChoice(2, $c0018); // You're lying! And you'll die for that!
        }
        else if (input == 2)
        {
            //turn aggressive
            relations.SetShipFaction(shipId, "pirates");
            topic.DialogueBreak();
        }
        else if (input == 3)
        {
            //peaceful - 500d
            var money = player.GetMoney(PLAYER);
            if (money >= 500)
            {
                var res = TryToAddPackage();
                if (res)
                {
                    player.RemoveMoney(PLAYER, 500);
                }
            }
            else
            {
                topic.AddPhrase($p0039); // Are you trying to trick me? You don't have enough money!
            }
        }
        else if (input == 1)
        {
            topic.AddPhrase($p0040); // Oh, THAT package! Well, hypothetically, if I had it, I could give it to you... but not for free. I have to feed my family. I'd give it over for... let's say... 500d. Deal?

            topic.AddChoice(3, $c0019); // Deal.
            topic.AddChoice(4, $c0020); // Tell me about your family.
        }
        else if (input == 4)
        {
            topic.AddPhrase($p0041); // My family? Eh, okay...
            topic.AddPhrase($p0042); // I have a wife... her name is... eh... Molly!
            topic.AddPhrase($p0043); // And I have two sons... ehm... Tango and Cash.

            topic.AddChoice(3, $c0021); // Ah, okay. I'll give you 500d.
            topic.AddChoice(5, $c0022); // How old are your sons?
        }
        else if (input == 5)
        {
            topic.AddPhrase($p0044); // How old?.. Five.

            topic.AddChoice(3, $c0023); // Ah, okay. I'll give you 500d.
            topic.AddChoice(6, $c0024); // Both of them?
        }
        else if (input == 6)
        {
            topic.AddPhrase($p0045); // Eh... Yeah. They're twins. Yeah, that's right.
            topic.AddPhrase($p0046); // My wife has a lot of trouble with them, mischievous kids! Ha-ha!

            topic.AddChoice(3, $c0025); // Ah, okay. I'll give you 500d.
            topic.AddChoice(7, $c0026); // What's your wife's name again?
        }
        else if (input == 7)
        {
            topic.AddPhrase($p0047); // Wife's name you say? Ehmmm... Melanie! That's right, Melanie!

            topic.AddChoice(3, $c0027); // Ah, okay. I'll give you 500d.
            topic.AddChoice(8, $c0028); // But you just said her name was Molly!
        }
        else if (input == 8)
        {
            topic.AddPhrase($p0048); // Ah, damn... Okay, you got me! Take this package and leave me alone!
            topic.AddPhrase($p0049); // What a tenacious bastard...
            TryToAddPackage();
        }
    }
    else
    {
        topic.AddPhrase($p0050); // Mind your own business. You got your package. Now get lost.
    }
}


/*
 ============================================================
 TRIGGERS SECTION
 ============================================================
 */
function SpawnScurvy()
{
    var radius = MathExt.RandRangeDouble(380, 420);

    // spawn scurvy outside the system, next to the space station
    var base_inf = generator.GetBaseByID(quest_base_id);
    var coord = { x: base_inf.coord_x, y: base_inf.coord_y };
    var ang = Math.atan2(coord.y, coord.x) + MathExt.RandRangeDouble(-1.576, 1.576);
    var coords = { x: radius * Math.cos(ang), y: radius * Math.sin(ang) };


    var scurvyId = generator.AddNPCShipToSystem($i0001, "PirateInWaiting", 1, "generic_pirates_fang_01", quest_system_id, coords.x, coords.y, { class: "pirate", questtaker: PLAYER_SHIP, sex: "male" }); // Scurvy
    relations.SetShipFaction(scurvyId, "none");
    generator.ClearDropLists(scurvyId);
    generator.SetNPCAvatarImg(scurvyId, "avatars/unique/scurvy.png");

    quest_scurvy_id = scurvyId;

    topic.Bind("onNpcDie", "OnScurvyKilled", { ship_id: scurvyId });

    return {
        coords: coords,
        id: scurvyId
    };
}

function TryToAddPackage()
{
    //add item to player's inventory
    if (ship.HasItemOfType(PLAYER_SHIP, "quest_object_joe_package"))
    {
        return true;
    }

    var res = ship.AddItem(PLAYER_SHIP, "quest_object_joe_package", 1);
    if (res.quantity == 1)
    {
        player.AddQuestRewardExperience(PLAYER, 250);

        topic.SetState(300);

        topic.QuestAddLog(TOPIC_ID, $q0009); // I've got the package. Now I need to take it back to One-Eyed Joe.
        topic.QuestRemoveLocalMarkers(TOPIC_ID);
        topic.QuestAddLocalMarkObject(TOPIC_ID, quest_system_id, quest_base_id);

        topic.RefreshTopics();
        return true;
    }
    else
    {
        game.SendNotificationError(PLAYER, $n0003, $n0004); // Inventory is full: You cannot take the package because your inventory is full.
        return false;
    }
}

function OnSpecialObjectActivatedHandler(args)
{
    //add item to player's inventory
    if (ship.HasItemOfType(PLAYER_SHIP, "quest_object_joe_package"))
    {
        return;
    }

    var res = ship.AddItem(PLAYER_SHIP, "quest_object_joe_package", 1);
    if (res.quantity == 1)
    {
        topic.SetState(300);

        topic.Unbind("onSpecialObjectActivated", "OnSpecialObjectActivatedHandler");
        generator.RemoveSpecialObject(args.special_object_id);

        topic.QuestAddLog(TOPIC_ID, $q0010); // I've got the package. Now I need to take it back to One-Eyed Joe.
        topic.QuestRemoveLocalMarkers(TOPIC_ID);
        topic.QuestAddLocalMarkObject(TOPIC_ID, quest_system_id, quest_base_id);
    }
    else
    {
        game.SendNotificationError(PLAYER, $n0005, $n0006); // Inventory is full: You cannot take the package because your inventory is full.
    }
}

function OnScurvyKilled(args)
{
    topic.Unbind("onNpcDie", "OnScurvyKilled");

    if (npc.GetTag(args.ship_id, "questtaker") != PLAYER_SHIP)
    {
        return;
    }

    if (ship.HasItemOfType(PLAYER_SHIP, "quest_object_joe_package"))
    {
        return;
    }

    //add remains, visible only to person, who took the quest
    var coord = ship.GetCoordinates(args.ship_id);

    var spobj_id = generator.AddSpecialObject(quest_system_id, coord.x, coord.y, "scurvy_remains", PLAYER_SHIP);
    topic.Bind("onSpecialObjectActivated", "OnSpecialObjectActivatedHandler", { special_object_id: spobj_id });

    topic.QuestRemoveLocalMarkers(TOPIC_ID);
    topic.QuestAddLocalMarkObject(TOPIC_ID, quest_system_id, spobj_id);
    topic.QuestAddLog(TOPIC_ID, $q0011); // Scurvy is dead. I should search for a package in his wreck.
}