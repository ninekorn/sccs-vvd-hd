# revision 13132

pressing the menu F9 to go fullscreen was making the captured application go to 1920w x 1080h, regardless of the resolution of the desktop monitor captured. So i added some code in program.cs that uses the device of scdirectx.cs to get the desktopbounds right when scgraphicssec.cs is created, and that way, inside of my script Form.cs, i was able to use program.cs desktopboundswidth and desktopboundsheight in order to properly make the captured program be at the current resolution of the monitor captured. The rest is pretty much the same as my revision 13131.

steve chassé 

NOTE: i pushed my last update under revision 13133 but it was a mistake on the revision number as i was pushing through for 13132. the correct revision for the push at 09h13am eastern time was for revision 13132. 
