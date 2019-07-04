package cn.jiguang.unity.analytics;

import android.os.Bundle;

import com.unity3d.player.UnityPlayerActivity;

public class UnityPluginActivity extends UnityPlayerActivity {

    @Override
    protected void onCreate(Bundle arg0) {
        super.onCreate(arg0);
        JAnalyticsBridge.log("UnityPluginActivity","onCreate");
    }

}