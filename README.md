# JAnalytics Unity Plugin

[![release](https://img.shields.io/badge/release-1.0.0-blue.svg)](https://github.com/jpush/janalytics-unity3d-plugin/releases)
[![platforms](https://img.shields.io/badge/platforms-iOS%7CAndroid-green.svg)](https://github.com/jpush/janalytics-unity3d-plugin)

极光官方支持的 JAnalytics Unity 插件（Android &amp; iOS）。

## 集成

把Plugins文件夹里的文件合并到您自己的项目Assets/Plugins文件夹下面

### Android

1. 替换 AndroidManifest.xml 里的包名。
2. 将 AndroidManifest.xml 中的 JPUSH_APPKEY 值替换成极光控制台应用详情中的 AppKey 值。
3. 配置项目里的包名：在 Unity 中选择 *File---Build Settings---Player Settings*，将 *Identification* 选项下的 *Bundle Identifier* 设置为应用的包名。

### iOS

1. 生成 iOS 工程，并打开该工程。
2. 添加必要的框架：

    - UIKit
    - SystemConfiguration
    - CoreTelephony
    - CoreGraphics
    - Security
    - Foundation
    - CoreLocation
    - CoreFoundation
    - CFNetwork
    - libz.tbd
    - libresolv.tbd
    - libsqlite3.tbd（v2.0.0及以上版本需要）


3. 在 UnityAppController.mm 中添加头文件 `JANALYTICSService.h`  。

    ```Objective-C
    #import "JANALYTICSService.h"

    ```

4. 在 UnityAppController.mm 的下列方法中添加以下代码：

    ```Objective-C
    - (BOOL)application:(UIApplication *)application didFinishLaunchingWithOptions:(NSDictionary *)launchOptions {

      

      /*
        参数说明：
            appKey: 极光官网控制台应用标识。
            channel: 频道，暂无可填任意。
      */
      JANALYTICSLaunchConfig * config = [[JANALYTICSLaunchConfig alloc] init];
      config.appKey = @"替换成你自己的 Appkey";
      config.channel = @"";
      [JANALYTICSService setupWithConfig:config];


      return YES;
    }

    ```

## API 说明

Android 与 iOS [通用 API](/Doc/CommonAPI.md)。

### Android

[Android API](/Doc/AndroidAPI.md)

> ./PluginsAndroidAARCode/janalytics-unity-plugin 为插件的 Android 项目，可以使用 Android Studio 打开并进行修改，再 build 为 .aar 替换已有的 janalytics-unity-plugin-release.aar。

### iOS

[iOS API](/Doc/iOSAPI.md)

## 更多

- [JAnalytics 官网文档](https://docs.jiguang.cn)
- 有问题可访问[极光社区](http://community.jpush.cn/)搜索和提问。
