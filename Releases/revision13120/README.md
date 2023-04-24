# sccs-vvd-hd


########RELEASE-REV1312#######
########RELEASE-REV1312#######
########RELEASE-REV1312#######

EDIT2023AVRIL17 - I released/uploaded my solution REV1312. Now i am naming my solutions sccsvvdhd instead of sccsr14forms or sccsr15-16-17 etc... but naming my folders with revision numbers will happen again when i make backups of my solution and release/upload them as i did in this current repository with my first release. I wanted to remake from scratch my solutions WinRT.GraphicsCapture and Win32.Shared and Win32.DWMSharedSurface before releasing and so i did. I freshly "rewrote the scripts from c# by using my sccsscriptrewrite utility solution", where i don't even know if rewritting the scripts from c# is giving errors if a stenography virus or bullshit is attached so instead of using File.Copy, i instead retrieve the string, and soon i will try and write a .txt file and save and then reopen the file and retrieve the string again and then rewrite to .cs extension, as i want to see if i can clean viruses/spywares/malwares/adwares/steganography if any, was attached to any of my code. I also remade my main solution and rewrote in c# the scripts of my main solution. While remaking my solution using visual studio 2022, things to note, you have to create it in netframework 4.7.2 and edit the csproj project file and add UseWPF true and LangVersion 10.0 and I rebuilt those 3 screencapture solutions from Mika's sandbox using visual studio 2022 and the framework 4.7.2 for those as using the framework 4.8 or net6.0 wasn't working.

########RELEASE-REV1312#######
########RELEASE-REV1312#######
########RELEASE-REV1312#######

Current revision1312 info:

New sky/scene color picker button. New dirty rgb pixel area slider option but it is in development to help reduce the cpu cost of the heightmaps values being pasted on arrays and then the buffers writting the heightmap every frame is very costly so anything else that i can develop to help performance could be helpful for users with a cpu of less performance than my setup amd Ryzen 2600 and a Soyo 5700xt and 40gb of Ram, a 1500++ speed silicon m2280 drive, and an Asus 3D monitor that i never tried yet with my program to see the 3d voxels in action. You have now the ability to see the "fps" in the form of milliseconds for the "mu" that is the "milliseconds update timer" and the "mw" for the "milliseconds work timer" and "mr" for the milliseconds rendering and presenting to directx (to reverify as right now i think i am only using the presenting for the "mr").

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-18%20131104.jpg" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-18%20131216.jpg" border="0"><img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-17%20224840.jpg" border="0">

In order to fix the objects appearing in the wrong layer in the scene through the window, it has to do with how i was sending the projection matrix to the shader. I was doing twice transpose on the voxel virtual desktop and once transpose on the ik rig and voxel objects so i fixed that:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-16%20170904.jpg" border="0">

The hands and fingers sizes are set with the alternate voxel size value in my sccsikvoxellimbs.cs script but i will try and make it work as a whole in the future with one change of setting changes the size of the whole ik rig:

<img WIDTH=250 src="https://github.com/ninekorn/gif-resources/blob/main/Capture%20d%E2%80%99%C3%A9cran%202023-04-17%20201824.jpg" border="0">


rev130: https://www.microsoft.com/en-us/wdsi/submission/70925aec-07f1-4f36-b73e-99b9a829edd4

rev132: https://www.microsoft.com/en-us/wdsi/submission/ba49e49d-4c97-4265-b858-4daf4fea9636

rev132: https://www.microsoft.com/en-us/wdsi/submission/930977cd-8c0e-48b1-bfcf-572caf1cb567 //Client Detection none - Cloud detection Trojan:Win32/Phonzy.A!ml - uncompiled






