# revision 13128 - "dashboard figurine" mini voxel human ik rig pointing at the mouse cursor.

i wanted to make the voxel human ik rig walk on the voxel virtual desktop surface but that is more complicated to code, and it had been a while that i hadn't touched inverse kinematics so i decided to practice by re-incorporating my ik development and tested for camera corners "dashboard figurines" and after a couple days of work i have a result.

But my development for trying to fix the virtual desktop screencapture image when using an NVIDIA video card didn't work. I tried playing around with the samplerstate from within the shader and outside of the shader, i tried padding completely my vertex binding in divisions of 4 by adding two more vertex bindings for an int of 4 bytes each and also did some other tests. In the end, it seems to be maybe a projection matrix/viewmatrix issues, maybe it's a translation of the texture that i am doing on the pixel shader that is accepted with AMD video cards but isn't accepted on NVIDIA video cards. It's maybe one of those things.

The voxel human ik rig arms are pointing where the mouse cursor is on the virtual desktop. This was a benchmark test and it was lagging with just that many rigs.

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-19%20194657.jpg" border="0">

Final Results. Only using 2 ik rig bottom left and right pointing their arms where the mouse cursor is. The right wrist of the ik character is clenching and the index is pointing when you press the left mouse button but the voxel ik rig is too small for you to notice unless maybe you have a big screen... but the hands are inverted so i will look into that later:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-22%20110431.jpg" border="0">

I will work on trying to incorporate a slider to increase the size of the voxel human ik rig "dashboard figurines" soon. In the screenshot below, the changes are discreet but it is visible that the voxel assets and voxel ik rig are bigger than the last screenshot:
<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-22%20130846.jpg" border="0">


Also, i have cleaned a little bit the console debug, and it is more precise to know now what is happening during the loading process:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-22%20122726.jpg" border="0">

I modified a little bit my perceptron code development using the broloff perceptron, in order to re-use my old code of vector2.dot instead of vector3.dot and also now the rotation of the stick only happens on the yaw and pitch as i never fixed it to work on 3 axis, but at least, when the user is standing in the front of the screen and moves backwards away from the screen, the stick/pencil doesn't go crazy in a rotation frenzy anymore.

Something didn't feel right with my vertex shader z position displacement as some rows were missing from the virtual desktop initial zero on the z axis, and we could see the missing rows way back as if they were sent to infinity... I think i fixed it but i am unsure as the last row at the bottom was also giving me issues until somehow it stopped after a few modifications and a solution rebuild. 





