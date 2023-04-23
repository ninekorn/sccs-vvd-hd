#revision 13131

rev13131 - i added back the voxel ik head on the voxel ik human rigs. I made the dashboard figurines bigger and now the hands at least are aligned with the lower arm 
but the hand is not rotating yet towards the cursor location but the hand is clenching and the index is pointing the finger when left mouse clicking.

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-23%20071034.jpg" border="0">

EDIT-2023april21: AVIRA ANTIVIRUS DETECTION? why? where? what? so i sent my files to avira but will have to uninstall avira if i want to keep on programming. 
I advised them that i don't know where in the heck there could be a virus in my compiled exe, as this is what avira detected tonight around 20h56 21april2023 HEUR/APC.
Please be careful with my software. Otherwise, as i said before, it might be a binary signing files that i should do and that i don't do? So i restored the 
quarantined .exe back into my computer as whatever i am coding in my scripts isn't a virus and i need to keep on developing. maybe i should do a cleaning of the pinvoke 
functions "dllimport" user32.dll and the others of that kind, because people can use hooking functions maybe to hook to all of those functions that i don't use and are
remnants of code i had intended to use as reference and sometimes i just end up not even using those user32.dll calls in 
program.cs/form.cs/scgraphicssec.cs/scupdate.cs/scdirectx.cs . Many of those functions, i don't use them as i have no purpose for them. Maybe removing a 
couple of these functions could eliminate a false positive if this is what is causing those detections?...

In order to change the angle of the voxel virtual desktop at loadup, you have to modify the following lines in the script scgraphicssec.cs:

       Line2266   var pitch = (float)(Math.PI * (9) / 180.0f); //here i changed it to 9 degrees and didn't need to move the origin position of the camera 
       Line2267   var yaw = (float)(Math.PI * (0) / 180.0f);
       Line2268   var roll = (float)(Math.PI * (0) / 180.0f);

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-23%20192918.jpg" border="0">

Edit-2023april23: Same solution, no avira antivirus detection today? maybe their antivirus/malware database got updated after i sent them an email/message about the HEUR/APC detection?...
