# sccs-vvd-hd

Current revision1312:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-18%20131104.jpg" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-18%20131216.jpg" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-17%20224840.jpg" border="0">

In order to fix the objects appearing in the wrong layer in the scene through the window, it has to do with how i was sending the projection matrix to the shader. I was doing twice transpose on the voxel virtual desktop and once transpose on the ik rig and voxel objects so i fixed that:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-16%20170904.jpg" border="0">

The hands and fingers sizes are set with the alternate voxel size value in my sccsiklimbs.cs script but i will try and make it work as a whole in the future with one change of setting changes the size of the whole ik rig:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-17%20201824.jpg" border="0">





