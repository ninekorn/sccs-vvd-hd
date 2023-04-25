# revision 13133

I added the standard resolutions from 640x480 to 1920x1080 for the voxel resolution as the correct heightmaps values for the correct bytes were only sent properly for resolutions of 1920x1080. Now it should work for everything between 640x480 to 1920x1080. But i had to shrink a little bit the size of the voxelized surface for the entirety of the heightmap values to be visible on the voxelized surface, otherwise it showed that some resolutions weren't showing at full resolution on the heightmaps values.
