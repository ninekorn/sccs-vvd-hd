# sccs-vvd-hd

Current revision1312:

New sky/scene color picker button. New dirty rgb pixel area slider option but it is in development to help reduce the cpu cost of the heightmaps values being pasted on arrays and then the buffers writting the heightmap every frame is very costly so anything else that i can develop to help performance could be helpful for users with a cpu of less performance than my setup amd Ryzen 2600 and a Soyo 5700xt and 40gb of Ram, a 1500++ speed silicon m2280 drive, and an Asus 3D monitor that i never tried yet with my program to see the 3d voxels in action.

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-18%20131104.jpg" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-18%20131216.jpg" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-17%20224840.jpg" border="0">

In order to fix the objects appearing in the wrong layer in the scene through the window, it has to do with how i was sending the projection matrix to the shader. I was doing twice transpose on the voxel virtual desktop and once transpose on the ik rig and voxel objects so i fixed that:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-16%20170904.jpg" border="0">

The hands and fingers sizes are set with the alternate voxel size value in my sccsiklimbs.cs script but i will try and make it work as a whole in the future with one change of setting changes the size of the whole ik rig:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-17%20201824.jpg" border="0">





