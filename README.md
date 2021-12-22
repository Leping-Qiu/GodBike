# 学堂路神车项目

学堂路神车是受到学堂路车神启发的，清华大学2021秋人机交互课AIoT设备创新交互项目。本项目重建了学堂路，使得用户可以在一条无限循环的学堂路上骑行自行车，查看骑行状态、身体状态、查看导航、查看信息等等。本项目面向 Oculus 平台。

作者：Leping Qiu

版本：1.0



## 使用

**环境**：Unity 2020.3.24

**场景**：路径 Assets > GodBike2.unity

**运行**：建议使用 Oculus Link 运行。设备需要支持手势识别以获得完整功能。

**核心功能**：

1. 在学堂路中骑行，查看骑行状态和身体状态（车把上方从左到右：车速，朝向，时间，热量，心跳）查看导航（车辆前方箭头）查看小组件（第一到第五：天气，日历，音乐，消息，远程桌面）
2. 使用左手手柄 thumb stick 上下控制车速
3. 使用右手手柄 thumb stick 左右切换小组件
4. 使用右手比划数字（1，2，3，4，5）切换小组件（注：3是OK手势）
5. 使用头动，面前方向，左向右歪头切换小组件
6. 使用键盘方向左右切换小组件（代码测试使用）



## 参数修改

### 骑行 

> **Player Object > BikeController.cs**

**修改巡航速度**：*Bike Controls > Movement Speed*

**修改骑行模式**：*Bike Controls > Ride Mode*

1. AutoPilotBreaks：自行车自动行驶
   1. 左手手柄 index trigger finger，刹车
   2. 左手手柄 hand trigger finger，加速
2. AutoPilotNoBreaks：自行车自动行驶，不能刹车
3. Controller：使用摇杆控制自行车行驶
   1. 左手手柄 thumb stick 向前，自行车向前
   2. 左手手柄 thumb stick 向后，自行车向后
4. Keyboard：使用键盘控制自行车行驶
   1. 上方向键，自行车向前
   2. 下方向键，自行车向后



### 交互（手柄）

同时只打开一项：

1. Controller Detector Single：不能连续切换
2. Controller Detector Multiple：可以连续切换

>**Detection Object > Controller Detection Object > ControllerDetectorMultiple.cs**

**连续切换时间间隔**：*Controller Parameters > Auto Change Time*



### 交互（手势）

>**Detection Object > Gesture Detection Object > GestureDetector.cs**

**手势识别阈值**：*Gesture Parameters > Detection Threshold*

**定义的手势列表**：*Gesture Objects > Gestures*

**添加新手势**：*Gesture Parameters > Add Gesture Mode*

1. 打开 Add Gesture Mode
2. 运行时用右手识别动作，按下空格添加
3. 运行时右键 Gestures 部分复制 values
4. 结束运行后右键粘贴 values



### 交互（头动）

同时只打开一项：

1. Controller Detector Single：不能连续切换
2. Controller Detector Multiple：可以连续切换

>**Detection Object > Head Detection Object > HeadDetectorSingle.cs**

**歪头识别阈值**：*Head Parameters > Detection Threshold*

> **Detection Object > Head Detection Object > HeadDetectorMultiple.cs**

**歪头识别阈值**：*Head Parameters > Detection Threshold*

**连续切换时间间隔**：*Head Parameters > Auto Change Time*