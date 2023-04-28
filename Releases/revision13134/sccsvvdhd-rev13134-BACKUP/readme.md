#revision 13134

update - revision 13134 - full resolution - uncalibrated.

Hey everyone. My earlier resolution development / update revision 13133 was pure garbage as the only way possible to have a working resolution was to put the monitor at the resolution prior to selecting it inside of the program. I made changes so that at least 5 resolutions are available for each type of monitor resolutions. So currently my monitor goes from min 800w600h to max 1920w1080h so i was able to develop some lines of code to adapt to different monitor resolutions, including the min and max of my monitor for a total of 15 resolutions that are on my monitor.

Next to the resolutions of my monitor below are the new "resolution options" info on how i have scripted new resolution options, where 5 choices of resolution are available for the user to choose from (apart from the "half voxel res" option and "full voxel res" option). The resolutions below are also the resolutions that my program is now able to detect and assign an appropriate number of voxel instances and voxel bytes that matches the resolution of the monitor. (there is calibration/tweaking left to do).

ratio 16/9 - of a 1920 x 1080 resolution <= voxel resolution #1

ratio //// of a 1760 x 990 resolution 

ratio //// - of a 1680 x 1050 resolution 

ratio 16/9 - of a 1600 x 900 resolution <= voxel resolution #2 

ratio 8/5 - of a 1440 x 900 resolution <= voxel resolution #3

ratio //// - of a 1366 x 768 resolution 

ratio //// - of a 1280 x 1024 resolution 

ratio //// - of a 1280 x 960 resolution 

ratio //// - of a 1280 x 800 resolution 

ratio 16/9 - of a 1280 x 720 resolution  <= voxel resolution #4 

ratio //// - of a 1152 x 864 resolution 

ratio //// - of a 1128 x 634 resolution 

ratio //// - of a 1024 x 768 resolution 

ratio //// - of a 832 x 624 resolution 

ratio 4/3 - of a 800 x 600 resolution  <= voxel resolution #5 (i'm not sure here, i think i based it off 640x480)

ratio //// - of a 640 x 480 resolution  - WIP - i don't have a monitor yet accepting that resolution. so i will find a monitor test subject first and then i will be able to try and calibrate.

Note: 

1. I use the monitor resolution as the base for reducing/dividing it to obtain an approximate comparable number of voxel instances in width and height to cover the area of voxel bytes for the entire voxel virtual desktop image). "Keep in mind more the ratio of those monitors not just the resolution".
2. The resolutions options #1 to #5 are based of a resolution in width of 1920 by 1080 in height.

But with each monitor resolution you can select 5 different resolution, but that is not all, since i have also left enabled the "low voxel res" and "high voxel res" options, now named "half voxel res" and "full voxel res", so it's 15 monitor resolutions with 5 choices each (less voxels per resolution type and different ratios per resolution alternative from 1 to 5), and a full voxel and half voxel mode. That brings a total of 15*5*2 voxel resolutions, or 150 voxel resolutions total. But there is so much tweaking left to do, that probably only half of those resolutions are correctly-ish calibrated where the heightmaps values are fitting the screencapture image behind. So i will keep working on this.

I apologize for my last update as even the "low voxel res" and "high voxel res" weren't working anymore. so update 13133 wasn't that decent but at least it gave a little bit of resolutions.

Currently, it is not possible to have access to monitor resolutions other than the current monitor resolutions, so while you would be let's say in the monitor resolution 1920w 1080h, then the voxel resolutions available to you would be 5 * 2 = 10 resolutions possible.

steve c

P.S. I bet building a program without the need for calibration is entirely possible. I just couldn't figure it all out during development and that's why i am stuck with having to manipulate by hand so many lines of code just to make it work for so many different resolutions. So please note that this is WIP, but a prelude to what is coming, which is "calibrated resolutions", where i will try and calibrate the resolutions the best that i can.
