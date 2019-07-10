package cn.jiguang.unity.analytics;

import com.unity3d.player.UnityPlayer;

import java.util.HashMap;

import cn.jiguang.analytics.android.api.AccountCallback;

public class UnityAccountCallback implements AccountCallback {
    private String gameObjectNameBack;
    private String gameObjectMethodBack;
    private int sequence;

    public UnityAccountCallback(String gameObjectNameBack,String gameObjectMethodBack,int sequence){
        this.gameObjectNameBack = gameObjectNameBack;
        this.gameObjectMethodBack = gameObjectMethodBack;
        this.sequence = sequence;
    }
    @Override
    public void callback(int i, String s) {
        JAnalyticsBridge.log("UnityAccountCallback","callback:",i,s);
        if (null == gameObjectNameBack || null == gameObjectMethodBack) {
            return;
        }
        HashMap<String, String> jsonMap = new HashMap<String, String>();
        jsonMap.put("sequence", String.valueOf(sequence));
        jsonMap.put("code", String.valueOf(i));
        jsonMap.put("msg", s);
        String json = JsonUtil.toJson(jsonMap);
        UnityPlayer.UnitySendMessage(gameObjectNameBack, gameObjectMethodBack, json);
    }
}
