# revision 13128 - dashboard voxel human ik rig

i wanted to make the voxel human ik rig walk on the voxel virtual desktop surface but that is more complicated to code, and it had been a while that i hadn't touched inverse kinematics so i decided to practice by re-incorporating my ik development and tested for camera corners "dashboard figurines" and after a couple days of work i have a result.

But my development for trying to fix the virtual desktop screencapture image when using an NVIDIA video card didn't work. I tried playing around with the samplerstate from within the shader and outside of the shader, i tried padding completely my vertex binding in divisions of 4 by adding two more vertex bindings for an int of 4 bytes each and also did some other tests. In the end, it seems to be maybe a projection matrix/viewmatrix issues, maybe it's a translation of the texture that i am doing on the pixel shader that is accepted with AMD video cards but isn't accepted on NVIDIA video cards. It's maybe one of those things.

The voxel human ik rig arms are pointing where the mouse cursor is on the virtual desktop. This was a benchmark test and it was lagging with just that many rigs.

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-22%20110431.jpg" border="0">

Final Results. Only using 2 ik rig bottom left and right:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-19%20194657.jpg" border="0">

