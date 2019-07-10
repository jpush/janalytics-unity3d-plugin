# iOS SDK API

以下除事件监听之外的方法都基于 `JAnalyticsIos` 对象进行调用。

##SDK 地理位置统计
+ ***SetLatitude(double latitude, double longitude)***
	+ 接口说明：
		+ 上报LBS信息

	+ 参数说明：
		+ latitude：纬度
		+ longitude： 经度


##SDK 崩溃日志统计
+ ***CrashLogON()***
	+ 接口说明：
		+ 开启crash日志收集，默认是关闭状态
