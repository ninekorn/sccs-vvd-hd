# revision 13135 - calibration update - halfway there

hey everyone, i was able to calibrate approx 75 resolutions on 150. It's all of the low voxel definition resolutions that i worked on but out the 75 resolutions i calibrated, i will review 13 of those as i am unsure, and wrote next to the resolutions "to review" in the script scupdate.cs (correction as i had written scgraphicssec.cs in my push notes). So currently, you should only use the "half voxel res" option, and it is set as default, as i didn't calibrate the "full voxel res" yet, where the voxel HD resolutions are (1 voxel byte per 1 pixel). So currently, the engine runs smooth on multiple resolutions where the screencapture and the voxels are "calibrated" correctly positioned meaning the values of the heightmaps are where the color values RGB are on the displayed image, but my calibration isn't casted in concrete yet meaning that i am undone and just tried the best i could to have the voxels correctly fractioned and sometimes it just isn't good enough (13 resolutions to review on 75). 

I am unsure if i will start the "full voxel resolution" calibration today the 30th of april 2023, as it really was a marathon just to calibrate the first 75 resolutions in two days. I thought, this revision 13135 would be adding a tons of monitor "compatibility", for low voxel resolution so i thought to release it now. My last revision 13134 was only calibrated for monitors of resolutions 1920 by 1080 and to prepare for the calibration for the rest of the resolutions of this revision 13135, for the low voxel resolution option. If you want HD voxel resolutions again, you will have to wait for the next calibration update, or you can try and calibrate trial and error like i am doing mostly in the scripts scgraphicssec.cs and scupdate.cs.

Oh, and sometimes i couldn't make the same ratios comparable from resolution to resolution, as i just wasn't inputting the correct values to calibrate and i had to modify the ratio of number of instances in width and height in order to at least have 5 "low voxel" resolutions for each monitor resolution.

Also keep in mind that, this current revision 13135 gives you 5 voxel resolutions which means the performance also is affected so the program might run better for you depending on your computer specs as, going full voxel mode (which isn't working yet for any resolutions except the 1920*1080 where 4 are setup the rest i will work on it soon). So this revision brings in some sort "performance adjustments" where you can choose a different resolution of 16/9 if your windows resolution is a 1920*1080. If your current windows resolution is lower than 1920*1080, than only the lower voxel resolution mode will work, the high voxel resolution isn't calibrated yet for those resolutions.

As i explained in my revision 13134 readme file, the current resolutions that i worked on, are the following: 

ratio 16/9 - of a 1920 x 1080 resolution <= voxel resolution #1

ratio //// of a 1760 x 990 resolution

ratio //// - of a 1680 x 1050 resolution

ratio 16/9 - of a 1600 x 900 resolution <= voxel resolution #2

ratio 8/5 - of a 1440 x 900 resolution <= voxel resolution #3

ratio //// - of a 1366 x 768 resolution

ratio //// - of a 1280 x 1024 resolution

ratio //// - of a 1280 x 960 resolution

ratio //// - of a 1280 x 800 resolution

ratio 16/9 - of a 1280 x 720 resolution <= voxel resolution #4

ratio //// - of a 1152 x 864 resolution

ratio //// - of a 1128 x 634 resolution

ratio //// - of a 1024 x 768 resolution

ratio //// - of a 832 x 624 resolution

ratio 4/3 - of a 800 x 600 resolution <= voxel resolution #5 (i'm not sure here, i think i based it off 640x480)

I tried to follow the desktop monitor resolution ratio for increasing/decreasing the amount of instances in width and height but i wasn't always successful for every desktop monitor resolution. 

steve chassé 

**Here are the screenshots of the results for a monitor resolution of 1920x1080 in the low voxel mode option:**

128 instances in width * 72 instances in height * 8 voxel bytes per instances in width * 8 voxel bytes per instances in height:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/voxelres1-19201080.jpg" border="0">

113 instances in width * 57 instances in height * 8 voxel bytes per instances in width * 8 voxel bytes per instances in height:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/voxelres2-19201080.jpg" border="0">

101 instances in width * 57 instances in height * 8 voxel bytes per instances in width * 8 voxel bytes per instances in height:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/voxelres3-19201080.jpg" border="0">

80 instances in width * 47 instances in height * 8 voxel bytes per instances in width * 8 voxel bytes per instances in height:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/voxelres4-19201080.jpg" border="0">

40 instances in width * 30 instances in height * 8 voxel bytes per instances in width * 8 voxel bytes per instances in height:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/voxelres5-19201080.jpg" border="0">

Not sure that my ratios are correctly fitting the resolution, but at least i was able to calibrate most of the 75 low voxel resolutions. 


