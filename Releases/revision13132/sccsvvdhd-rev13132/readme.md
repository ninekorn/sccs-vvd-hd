# revision 13132

pressing the menu F9 to go fullscreen was making the captured application go to 1920w x 1080h, regardless of the resolution of the desktop monitor captured. So i added some code in program.cs that uses the device of scdirectx.cs to get the desktopbounds right when scgraphicssec.cs is created, and that way,
inside of my script Form.cs, i was able to use program.cs desktopboundswidth and desktopboundsheight in order to properly make the captured program be at the current resolution of the monitor captured.

steve chassé 
