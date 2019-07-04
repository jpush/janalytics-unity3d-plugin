package cn.jiguang.unity.analytics;

import android.app.Application;
import android.content.Context;

public class JAnalyticsApplication extends Application {
    public static Context mContext;
    @Override
    public void onCreate() {
        mContext = this;
        super.onCreate();
        JAnalyticsBridge.log("JAnalyticsApplication","onCreate");

    }
}
