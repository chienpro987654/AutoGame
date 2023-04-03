# AutoGame
An automated application for the game called Epic Seven.

This application helps player gains arena point in arena by getting defeated continuously. The points get from streak battling and reward for the lost result.

That kind of tactic is obviously not sufficient as building a team and then gaining victory normally, but it helps you to reduce the time for the game, 
and it is also a good choice for a lazy person like me who doesn't want to think too much on the things supposed to be an entertainment activity :v >.<

### How this code works

Using adb shell to send commands from PC, such as take a screenshot, click (using coordinates).
Using Aforge Imaging Framework to compare an image that has been cut out from the screenshot to avoid unexpected behavior.

### Demo

https://youtu.be/txatGTTrUSw

### Requirements

#### Step 1: Install Adb - Allow ADB Debugging.
Install adb shell on your PC and allow ADB Debugging on your cellphone. See the guide from [here](https://www.xda-developers.com/install-adb-windows-macos-linux/).

#### Step 2: Configure the code
Clone or download the code.
The coordinates in this code are used for Xiaomi Redmi 5 Pro (2160 x 1080). 
If your cellphone has a different resolution, you need to change those coordinates to make it work. Or just change the resolution of the emulator, it also works fine
(tested with LD Player emulator).
Unless skip this step.

**Warning**: If you do not have the same resolution as mine, the steps below require IDE and programming knowledge, if you don't have any ideas about it,
I suggest giving up from now on!

##### Step 2.1 Change coordinates

To change those coordinates, first start adb using this command: `adb devices`.

Then, take a screenshot using this command: `adb exec-out screencap -p > <your_path>`. For example: `adb exec-out screencap -p > D:\images\screenshot.png`.

Then, use Paint or another application that can help you to get the coordinates of the clicks.
The coordinates are stored in `clicks[]` array and be used as parameters of the `clickXY()` function.

Explanation: `clickXY(1155, 1320, 700, 740)` -> click the coordinate that has X between 1155 and 1320, Y between 700 and 740.

Open Form1.cs, go to `clickEvent()` function, and change those coordinates in `click[]` array.

##### Step 2.2 Change template images

Template images are used as sources, which will be used whenever the application takes a screenshot and take a part of this screenshot to compare to it.

First, take a screenshot using the command has been shown in step 2.1. Then copy it to folder `images`.

Second, open the screenshot using Paint and then get a rectangle with a coordinate(X,Y) 
and width, height (from X to <width> pixel to the right, from Y to <height> pixel to the bottom).

Third, open Form1.cs, go to `cropBtn_Click()` function, change rectangles attributes in line 219 and the name of the image in line 223.

Fourth, open the project using Visual studio code/Visual Studio Community and click `Crop` button.

Do the same thing with the rest images.

#### Step 3: Find Retry button

This code works only after you get defeated 2 times to appear point suspension alert. After that, you need to find the retry button 
due to the random position of the retry button. Of course, if you have the same resolution as mine, just skip this step.

In `findRetry()` function, you need to care about 3 things:

First, the fight buttons and the retry button have the same X (coordinate), so you just need to change it one time in line 149.

Second, `pixels[]` array stored the Y (coordinate) of these buttons, so you need to change it to correspond to each button.

Finally, this function works based on the color difference between the buttons, so you should choose the coordinate exactly below the "Fight" or "Retry" line
to get the best result.

#### Step 4: Run Program

After running successfully it in visual studio code/visual studio community, the next time you can just open `AutoEpicSeven.exe` in `Debug` folder,
click `Test` button until `label1` changes to 
> Adb starts successfully!

And then change the number of flags, click `Run`, and enjoy!

