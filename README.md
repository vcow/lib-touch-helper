# Touch helper
This is the simple tool, which simulate the mobile device touchscreen touches in the Unity editor. For using that tool you simply need to get the touches from the <code>TouchHelper.GetTouches()</code> or from the <code>TouchHelper.GetTouch()</code> static methods, instead <code>UnityEngine.Input.touches</code>.   
In addition, the TouchHelper supports touch locking and two-tap pinch-to-zoom gesture simulation.
<br/>To simulate a zoom gesture, use RMB, the second touch will look like a mirror image of the mouse position relative to the middle of the screen.

See <a href="https://raw.githack.com/vcow/lib-touch-helper/master/docs/html/class_helpers_1_1_touch_helper_1_1_touch_helper.html">documentation</a> for details.

## Applying
You can download and install <code>touch-helper.unitypackage</code> from this repository. You can also add TouchHelper as dependency from __Github__ or directly include them in your Git project as __subtree__.

### Github
Go to the <code>manifest.json</code> and in the section <code>dependencies</code> add next dependency:
```
  "dependencies": {
    "vcow.helpers.touch-helper": "https://github.com/vcow/lib-touch-helper.git?path=/Assets/Scripts/Helpers/TouchHelper#2.0.1",
    ...
  }
```

### Subtree
From the root of your Git project launch next Git command:
```
git subtree add --prefix Assets/Scripts/Helpers/TouchHelper --squash git@github.com:vcow/lib-touch-helper.git sub-2.0.1
```
That adds TouchHelper from this repository to your project as a subtree at the locstion specified in the <code>--prefix</code> section relative to the root of your project.