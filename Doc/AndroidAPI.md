# Android SDK API

以下除事件监听之外的方法都基于 `JAnalyticsAndroid` 对象进行调用。

##SDK 初始化 API

+ ***Init()***
+ 接口说明：
+ 初始化接口。


+ ***InitCrashHandler()***
+ 接口说明：
+ 开启crashlog日志上报


+ ***StopCrashHandler()***
+ 接口说明：
+ 关闭crashlog日志上报

+ ***SetChannel(String channel)***
+ 接口说明：
+ 动态配置channel，优先级比AndroidManifest里配置的高
+ 参数说明：
+ channel:希望配置的channel，传null表示依然使用AndroidManifest里配置的channel
