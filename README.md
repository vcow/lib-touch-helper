# Touch helper
This is the simple tool, which simulate the mobile device touchscreen touches in the Unity editor. For using that tool you simply need to get the touches from the <code>TouchHelper.GetTouches()</code> or from the <code>TouchHelper.GetTouch()</code> static methods, instead <code>UnityEngine.Input.touches</code>.   
In addition, the TouchHelper supports touch locking and two-tap pinch-to-zoom gesture simulation.
<br/>To simulate a zoom gesture, use RMB, the second touch will look like a mirror image of the mouse position relative to the middle of the screen.

See <a href="https://raw.githack.com/vcow/lib-touch-helper/master/docs/html/class_helpers_1_1_touch_helper_1_1_touch_helper.html">documentation</a> for details.

## How to install
Select one of the following methods:

1. From Unity package.<br/>Select latest release from the https://github.com/vcow/lib-touch-helper/releases and download __window-manager.unitypackage__ from Assets section.

2. From Git URL.<br/>Go to __Package Manager__, press __+__ in the top left of window and select __Install package from git URL__. Enter the URL below:
```
https://github.com/vcow/lib-touch-helper.git#upm
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;or
```
https://github.com/vcow/lib-touch-helper.git#2.1.0
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if you want to install exactly 2.1.0 version.

3. From OpenUPM.<br/>Go to __Edit -> Project Settings -> Package Manager__ and add next scoked registry:
* __Name__: package.openupm.com
* __URL__: https://package.openupm.com
* __Scope(s)__: com.vcow.touch-helper

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Press __Save__, then go to __Package Manager__ and install __Scene Select Tool__ from the __My Registries -> package.openupm.com__ section.

4. Add to the ```manifest.json```.<br/>Open ```mainfest.json``` and add next string to the ```dependencies``` section:
```
{
  "dependencies": {
    "com.vcow.touch-helper": "https://github.com/vcow/lib-touch-helper.git#upm",
    ...
  }
}
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;or
```
{
  "dependencies": {
    "com.vcow.touch-helper": "https://github.com/vcow/lib-touch-helper.git#2.1.0",
    ...
  }
}
```
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;if you want to install exactly 2.1.0 version.
