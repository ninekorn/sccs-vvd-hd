# revision 13137 - pixel voxel light

<img src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-05-07%20201627.jpg" border="0">

https://youtu.be/DoTWQmmm_-o

i added a per voxel light effect on the light. Gotta go quite close to the voxel virtual desktop to notice or have a big screen.

Now if closing a captured program, the voxel virtual desktop image will also disappear and return to charcoal also losing the heightmaps value. It turns into the idle state that i would call it.

The orthogonal/perspective projection are back and working again.

The grids are back and working correctly again. minus that the colors of the grid are more pale when there aren't any captured program.

The screencapture brightness slider is back and working correctly again.

The light slider is back and working correctly again.

The light color picker and grid color picker options are back and working again.

I added a 4rth voxel type, being 5 faces cubes where the vertices don't extend. The current voxel types options are:

    0 => front face only
    1 => 5 faces with the front vertex that extend when heightmaps values are positive otherwise cubic voxels when heightmap values are negative.
    2 => the standard virtual desktop of a simple cube with 6 faces and 1 texture. This isn't a voxel virtual desktop.
    3 => the cubic voxels when heightmaps values are positive and negative.

I have work left to do on the dirty instances rgb option and slider so it isn't working correctly currently anymore.

Also i didn't work on the "high voxel resolution" options yet, i didn't calibrate those yet, except only the 1920x1080 works in that mode with the 1 to 5 resolutions available.

steve chassé aka ninekorn
