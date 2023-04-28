#revision 13134

update - revision 13134 - full resolution - uncalibrated.

Hey everyone. My earlier resolution development / update revision 13133 was pure garbage as the only way possible to have a working resolution was to put the monitor at the resolution prior to selecting it inside of the program. I made changes so that at least 5 resolutions are available for each type of monitor resolutions. So currently my monitor goes from min 800w600h to max 1920w1080h so i could setup pretty much every resolutions inbetween including the min and max for a total of 15 resolutions that are on my monitor:

1920 x 1080

1760 x 990

1680 x 1050

1600 x 900

1440 x 900

1366 x 768

1280 x 1024

1280 x 960

1280 x 800

1280 x 720

1152 x 864

1128 x 634

1024 x 768

832 x 624

800 x 600

640 x 480 - WIP - i don't have a monitor yet accepting that resolution. so i will find a monitor test subject first and then i will be able to try and calibrate.

But with each monitor resolution you can select 5 different resolution, but that is not all, since i have also left enabled the "low voxel res" and "high voxel res" options, now named "half voxel res" and "full voxel res", so it's 15 monitor resolutions with 5 choices each (less voxels per resolution type and different ratios per resolution alternative from 1 to 5), and a full voxel and half voxel mode. That brings a total of 15*5*2 voxel resolutions, or 150 voxel resolutions total. But there is so much tweaking left to do, that probably only half of those resolutions are correctly-ish calibrated where the heightmaps values are fitting the screencapture image behind. So i will keep working on this.

I apologize for my last update as even the "low voxel res" and "high voxel res" weren't working anymore. so update 13133 wasn't that decent but at least it gave a little bit of resolutions.

Currently, it is not possible to have access to monitor resolutions other than the current monitor resolutions, so while you would be let's say in the monitor resolution 1920w 1080h, then the voxel resolutions available to you would be 5 * 2 = 10 resolutions possible.

steve c

Note: I bet building a program without the need for calibration is entirely possible. I just couldn't figure it all out during development and that's why i am stuck with having to manipulate by hand so many lines of code just to make it work for so many different resolutions. So please note that this is WIP, but a prelude to what is coming, which is "calibrated resolutions", where i will try and calibrate the resolutions the best that i can.
