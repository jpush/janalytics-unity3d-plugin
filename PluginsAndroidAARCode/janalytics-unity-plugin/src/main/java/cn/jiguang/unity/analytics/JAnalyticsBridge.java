package cn.jiguang.unity.analytics;

import android.app.Activity;
import android.content.Context;
import android.util.Log;

import com.unity3d.player.UnityPlayer;

import java.util.Arrays;
import java.util.Map;
import java.util.Set;

import cn.jiguang.analytics.android.api.Account;
import cn.jiguang.analytics.android.api.BrowseEvent;
import cn.jiguang.analytics.android.api.CalculateEvent;
import cn.jiguang.analytics.android.api.CountEvent;
import cn.jiguang.analytics.android.api.Currency;
import cn.jiguang.analytics.android.api.Event;
import cn.jiguang.analytics.android.api.JAnalyticsInterface;
import cn.jiguang.analytics.android.api.LoginEvent;
import cn.jiguang.analytics.android.api.PurchaseEvent;
import cn.jiguang.analytics.android.api.RegisterEvent;


public class JAnalyticsBridge {
    private static final String TAG = "JAnalyticsBridge";

    private static JAnalyticsBridge jAnalyticsBridge;

    private Context mContext;
//    static String gameObject;

    private static final int CNY = 0;
    private static final int USD = 1;


    public static JAnalyticsBridge getInstance() {
        JAnalyticsBridge.log(TAG, "getInstance()");
        if (jAnalyticsBridge == null) {
            jAnalyticsBridge = new JAnalyticsBridge();
        }
        return jAnalyticsBridge;
    }

    public Context getContext() {
        if (null == mContext) {
            Activity currentActivity = UnityPlayer.currentActivity;
            if (null != currentActivity) {
                mContext = currentActivity.getApplicationContext();
            } else {
                Log.e(TAG, "UnityPlayer.currentActivity == null");
                mContext = JAnalyticsApplication.mContext;
                if (null == mContext) {
                    Log.e(TAG, "JAnalyticsApplication.mContext == null");
                }
            }
        }
        return mContext;
    }


    public void setDebugMode(boolean enable) {
        JAnalyticsBridge.log(TAG, "setDebugMode:", enable);
        JAnalyticsInterface.setDebugMode(enable);
    }

    public void init() {
        JAnalyticsBridge.log(TAG, "init");
//        JAnalyticsBridge.gameObject = gameObject;
        JAnalyticsInterface.init(getContext());
    }

    /**
     * 统计page开始
     *
     * @param pageName 被统计页面的名字
     */
    public void onPageStart(String pageName) {
        JAnalyticsBridge.log(TAG, "onPageStart:", pageName);
        JAnalyticsInterface.onPageStart(getContext(), pageName);
    }

    /**
     * 统计page结束
     *
     * @param pageName 被统计页面的名字
     */
    public void onPageEnd(String pageName) {
        JAnalyticsBridge.log(TAG, "onPageEnd:", pageName);
        JAnalyticsInterface.onPageEnd(getContext(), pageName);
    }

    /**
     * 自定义统计 类型由event决定
     * PurchaseEvent 购买事件模型
     * BrowseEvent 浏览事件模型
     * RegisterEvent 注册事件模型
     * LoginEvent 登录事件模型
     * CalculateEvent 计算事件模型
     * CountEvent 计数事件模型
     */
    public void onPurchaseEvent(String purchaseGoodsid, String purchaseGoodsName, double purchasePrice,
                                boolean purchaseSuccess, int purchaseCurrencyUnity, String purchaseGoodsType, int purchaseGoodsCount,
                                String extMap) {
        JAnalyticsBridge.log(TAG, "onPurchaseEvent:", purchaseGoodsid, purchaseGoodsName, purchasePrice,
                purchaseSuccess, purchaseCurrencyUnity, purchaseGoodsType, purchaseGoodsCount, extMap);
        Currency purchaseCurrency;
        switch (purchaseCurrencyUnity) {
            case CNY:
                purchaseCurrency = Currency.CNY;
                break;
            case USD:
                purchaseCurrency = Currency.USD;
                break;
            default:
                purchaseCurrency = Currency.CNY;
                break;

        }
        PurchaseEvent event = new PurchaseEvent(purchaseGoodsid, purchaseGoodsName, purchasePrice,
                purchaseSuccess, purchaseCurrency, purchaseGoodsType, purchaseGoodsCount);
        onEvent(extMap, event);
    }

    public void onBrowseEvent(String browseId, String browseName, String browseType, long browseDuration, String extMap) {

        JAnalyticsBridge.log(TAG, "onBrowseEvent:", browseId, browseName, browseType, browseDuration, extMap);
        BrowseEvent event = new BrowseEvent(browseId, browseName, browseType, browseDuration);
        onEvent(extMap, event);
    }

    public void onRegisterEvent(String registerMethod, boolean registerSuccess, String extMap) {
        JAnalyticsBridge.log(TAG, "onRegisterEvent:", registerMethod, registerSuccess, extMap);
        RegisterEvent event = new RegisterEvent(registerMethod, registerSuccess);
        onEvent(extMap, event);
    }

    public void onLoginEvent(String loginMethod, boolean loginSuccess, String extMap) {
        JAnalyticsBridge.log(TAG, "onLoginEvent:", loginMethod, loginSuccess, extMap);
        LoginEvent event = new LoginEvent(loginMethod, loginSuccess);
        onEvent(extMap, event);
    }

    public void onCalculateEvent(String eventId, double eventValue, String extMap) {
        JAnalyticsBridge.log(TAG, "onCalculateEvent:", eventId, eventValue, extMap);
        CalculateEvent event = new CalculateEvent(eventId, eventValue);
        onEvent(extMap, event);
    }

    public void onCountEvent(String eventId, String extMap) {
        JAnalyticsBridge.log(TAG, "onCountEvent:", eventId, extMap);
        CountEvent event = new CountEvent(eventId);
        onEvent(extMap, event);
    }

    private void onEvent(String extMap, Event event) {
        Map<String, String> map = JsonUtil.unityJsonToMap(extMap);
        event.addExtMap(map);
        JAnalyticsInterface.onEvent(getContext(), event);
    }

    /**
     * 开启crashlog上报
     */
    public void initCrashHandler() {
        JAnalyticsBridge.log(TAG, "initCrashHandler");
        JAnalyticsInterface.initCrashHandler(getContext());
    }

    /**
     * 关闭crashlog上报
     *
     */
    public void stopCrashHandler() {
        JAnalyticsBridge.log(TAG, "stopCrashHandler");
        JAnalyticsInterface.stopCrashHandler(getContext());
    }


    private static final String accountID_account_key = "accountID";
    private static final String creationTime_account_key = "creationTime";
    private static final String name_account_key = "name";
    private static final String sex_account_key = "sex";
    private static final String paid_account_key = "paid";
    private static final String birthdate_account_key = "birthdate";
    private static final String phone_account_key = "phone";
    private static final String email_account_key = "email";
    private static final String weiboID_account_key = "weiboID";
    private static final String wechatID_account_key = "wechatID";
    private static final String qqID_account_key = "qqID";

    /**
     * 开发者可以为用户增加账户信息，使统计数据可以以账户维度做统计分析 现开发的属性有：
     */
    public void identifyAccount(String accountMapJson, String extraMapJson,
                                int sequence, String gameObjectNameBack, String gameObjectMethodBack) {

        JAnalyticsBridge.log(TAG, "identifyAccount:", accountMapJson, extraMapJson,
                sequence, gameObjectNameBack, gameObjectMethodBack);
        Map<String, String> accountMap = JsonUtil.unityJsonToMap(accountMapJson);

        String accountID = accountMap.get(accountID_account_key);
        Account account = new Account(accountID);

        String creationTimeStirng = accountMap.get(creationTime_account_key);
        if (null != creationTimeStirng) {
            account.setCreationTime(Long.parseLong(creationTimeStirng));
        }

        String name = accountMap.get(name_account_key);
        if (null != name) {
            account.setName(name);
        }

        String sexString = accountMap.get(sex_account_key);
        if (null != sexString) {
            account.setSex(Integer.parseInt(sexString));
        }

        String paidString = accountMap.get(paid_account_key);
        if (null != paidString) {
            account.setPaid(Integer.parseInt(paidString));
        }

        String birthdate = accountMap.get(birthdate_account_key);
        if (null != birthdate) {
            account.setBirthdate(birthdate);
        }

        String phone = accountMap.get(phone_account_key);
        if (null != phone) {
            account.setPhone(phone);
        }

        String email = accountMap.get(email_account_key);
        if (null != email) {
            account.setEmail(email);
        }

        String weiboID = accountMap.get(weiboID_account_key);
        if (null != weiboID) {
            account.setWeiboId(weiboID);
        }

        String wechatID = accountMap.get(wechatID_account_key);
        if (null != wechatID) {
            account.setWechatId(wechatID);
        }

        String qqID = accountMap.get(qqID_account_key);
        if (null != qqID) {
            account.setQqId(qqID);
        }

        Map<String, String> extraMap = JsonUtil.unityJsonToMap(extraMapJson);

        if (!extraMap.isEmpty()) {
            Set<Map.Entry<String, String>> entries = extraMap.entrySet();
            for (Map.Entry<String, String> entry :
                    entries) {
                String key = entry.getKey();
                String value = entry.getValue();
                account.setExtraAttr(key, value);
            }
        }

        JAnalyticsInterface.identifyAccount(getContext(), account, new UnityAccountCallback(gameObjectNameBack, gameObjectMethodBack, sequence));
    }

    /**
     * 如果要解绑当前用户信息，调用JAnalyticsInterface.detachAccount(context, callback);
     */

    public void detachAccount(int sequence, String gameObjectNameBack, String gameObjectMethodBack) {

        JAnalyticsBridge.log(TAG, "detachAccount:",
                sequence, gameObjectNameBack, gameObjectMethodBack);
        JAnalyticsInterface.detachAccount(getContext(), new UnityAccountCallback(gameObjectNameBack, gameObjectMethodBack, sequence));
    }

    /**
     * 设置统计上报的自动周期，未调用前默认即时上报
     *
     * @param period 周期，单位秒，最小10秒，最大1天，超出范围会打印调用失败日志。传0表示统计数据即时上报
     */
    public void setAnalyticsReportPeriod(int period) {
        JAnalyticsBridge.log(TAG, "setAnalyticsReportPeriod:",period);
        JAnalyticsInterface.setAnalyticsReportPeriod(getContext(), period);
    }


    /**
     * 设置channel
     * 希望配置的channel，传null表示依然使用AndroidManifest里配置的channel.
     */
    public void setChannel(String channel) {
        JAnalyticsBridge.log(TAG, "setChannel:",channel);
        JAnalyticsInterface.setChannel(getContext(), channel);
    }

    public static void log(String tag, Object... obj) {
//        tag = "Unity_"+tag;
//        if (null == obj) {
//            Log.d(tag, "null");
//        } else {
//            Log.d(tag, Arrays.toString(obj));
//        }
    }

}
