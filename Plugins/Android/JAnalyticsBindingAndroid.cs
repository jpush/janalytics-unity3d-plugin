using UnityEngine;
using System.Collections.Generic;
using JAnalytics;

namespace JAnalyticsAndroid
{
    public class JAnalyticsBindingAndroid : MonoBehaviour
    {


        private static AndroidJavaObject _plugin;


        static JAnalyticsBindingAndroid()
        {
            using (AndroidJavaClass jpushClass = new AndroidJavaClass("cn.jiguang.unity.analytics.JAnalyticsBridge"))
            {
                _plugin = jpushClass.CallStatic<AndroidJavaObject>("getInstance");
            }
        }

        /// <summary>
        /// 设置是否开启 Debug 模式。
        /// <para>Debug 模式将会输出更多的日志信息，建议在发布时关闭。建议在init前调用。</para>
        /// </summary>
        /// <param name="enable">true: 开启；false: 关闭。</param>
        public static void SetDebugMode(bool enable)
        {
            _plugin.Call("setDebugMode", enable);

        }

        /// <summary>
        /// 初始化 JAnalytics。
        /// </summary>
        public static void Init()
        {
            _plugin.Call("init");

        }

        /// <summary>
        /// 统计page开始.
        /// </summary>
        /// <param name="pageName">被统计页面的名字.</param>
        public static void OnPageStart(string pageName)
        {
            _plugin.Call("onPageStart", pageName);

        }

        /// <summary>
        /// 统计page结束.
        /// </summary>
        /// <param name="pageName">被统计页面的名字.</param>
        public static void OnPageEnd(string pageName)
        {
            _plugin.Call("onPageEnd", pageName);

        }


        /// <summary>
        /// 开启crashlog上报.
        /// </summary>
        public static void InitCrashHandler()
        {
            _plugin.Call("initCrashHandler");

        }
        /// <summary>
        /// 关闭crashlog上报.
        /// </summary>
        public static void StopCrashHandler()
        {
            _plugin.Call("stopCrashHandler");

        }

        /// <summary>
        /// 开发者可以为用户增加账户信息，使统计数据可以以账户维度做统计分析.
        /// </summary>
        /// <param name="account">账号信息.</param>
        /// <param name="sequence">唯一请求识别ID，和结果一起反回.</param>
        /// <param name="gameObjectNameBack">游戏对象名 用于反回结果.</param>
        /// <param name="gameObjectMethodBack">游戏对象中的方法名，此方法只能有一个string 参数， 用于反回结果.</param>
        public static void IdentifyAccount(Account account,JAnalyticsCallBack callBack)
        {
            string accountMapJson = JsonHelper.ToJson(account.GetAccountDic());
            string extMapJson = JsonHelper.ToJson(account.GetExtraDic());

            int sequence = 0;
            string gameObjectNameBack = null;
            string gameObjectMethodBack = null;
            if (null != callBack)
            {
                sequence = callBack.getSequence();
                gameObjectNameBack = callBack.getGameObjectName();
                gameObjectMethodBack = callBack.getGameObjectMethode();
            }

            _plugin.Call("identifyAccount", accountMapJson, extMapJson, sequence, gameObjectNameBack, gameObjectMethodBack);

        }

        /// <summary>
        /// 解绑当前用账户信息
        /// </summary>
        /// <param name="sequence">唯一请求识别ID，和结果一起反回.</param>
        /// <param name="gameObjectNameBack">游戏对象名 用于反回结果.</param>
        /// <param name="gameObjectMethodBack">游戏对象中的方法名，此方法只能有一个string 参数， 用于反回结果.</param>
        public static void DetachAccount(
                                 JAnalyticsCallBack callBack)
        {

            int sequence = 0;
            string gameObjectNameBack = null;
            string gameObjectMethodBack = null;
            if (null != callBack)
            {
                sequence = callBack.getSequence();
                gameObjectNameBack = callBack.getGameObjectName();
                gameObjectMethodBack = callBack.getGameObjectMethode();
            }
            _plugin.Call("detachAccount", sequence, gameObjectNameBack, gameObjectMethodBack);

        }

        /// <summary>
        /// 设置统计上报的自动周期，未调用前默认即时上报.
        /// </summary>
        /// <param name="period">周期，单位秒，最小10秒，最大1天，超出范围会打印调用失败日志。传0表示统计数据即时上报.</param>
        public static void SetAnalyticsReportPeriod(int period)
        {
            _plugin.Call("setAnalyticsReportPeriod", period);

        }

        /// <summary>
        /// 设置channel.
        /// 动态配置channel，优先级比AndroidManifest里配置的高
        /// </summary>
        /// <param name="channel">希望配置的channel，传null表示依然使用AndroidManifest里配置的channel.</param>
        public static void SetChannel(string channel)
        {
            _plugin.Call("setChannel", channel);

        }


        public static void OnEvent(JAnalytics.Event e)
        {
           int type =  e.getType();
            switch (type)
            {
                case JAnalytics.Event.BrowseEventType:
                    BrowseEvent browseEven = (BrowseEvent)e;
                    string browseId = browseEven.getBrowseId();
                    string browseName = browseEven.getBrowseName();
                    string browseType = browseEven.getBrowseType();
                    long browseDuration = browseEven.getBrowseDuration();
                    OnBrowseEvent( browseId,  browseName,  browseType,  browseDuration,
                                 browseEven.getExtraDic());
                    break;
                case JAnalytics.Event.CalculateEventType:
                    CalculateEvent calculateEvent = (CalculateEvent)e;
                    string eventId = calculateEvent.getEventId();
                    double eventValues = calculateEvent.getEventValued();
                    OnCalculateEvent( eventId,  eventValues,
                                calculateEvent.getExtraDic());
                    break;
                case JAnalytics.Event.CountEventType:
                    CountEvent countEvent = (CountEvent)e;
                    OnCountEvent(countEvent.getEventId(),
                                countEvent.getExtraDic());
                    break;
                case JAnalytics.Event.LoginEventType:
                    LoginEvent loginEvent = (LoginEvent)e;
                    OnLoginEvent(loginEvent.getLoginMethod(), loginEvent.geLoginSuccess(),
                               loginEvent.getExtraDic());
                    break;
                case JAnalytics.Event.PurchaseEventType:
                    PurchaseEvent purchaseEvent = (PurchaseEvent)e;
                    string purchaseGoodsid = purchaseEvent.getPurchaseGoodsid();
                    string purchaseGoodsName = purchaseEvent.getPurchaseGoodsName();
                    double purchasePrice = purchaseEvent.getPurchasePrice();
                    bool purchaseSuccess = purchaseEvent.getPurchaseSuccess();
                    int purchaseCurrencyUnity = purchaseEvent.getPurchaseCurrencyInt();
                    string purchaseGoodsType = purchaseEvent.getPurchaseGoodsType();
                    int purchaseGoodsCount = purchaseEvent.getPurchaseGoodsCount();
                    OnPurchaseEvent( purchaseGoodsid,  purchaseGoodsName,  purchasePrice,
                                 purchaseSuccess,  purchaseCurrencyUnity,  purchaseGoodsType,  purchaseGoodsCount,
                                purchaseEvent.getExtraDic());
                    break;
                case JAnalytics.Event.RegisterEventType:
                    RegisterEvent registerEvent = (RegisterEvent)e;
                    OnRegisterEvent(registerEvent.getRegisterMethod(), registerEvent.getRegisterSuccess(), registerEvent.getExtraDic());
                    break;
            }
        }


        /// <summary>
        /// 购买事件上报
        /// </summary>
        /// <param name="purchaseGoodsid">商品id.</param>
        /// <param name="purchaseGoodsName">商品名称.</param>
        /// <param name="purchasePrice">购买价格(非空)e.</param>
        /// <param name="purchaseSuccess">购买是否成功(非空).</param>
        /// <param name="purchaseCurrencyUnity">货币类型，Currency.CNY 或 Currency.USD.</param>
        /// <param name="purchaseGoodsType">商品类型.</param>
        /// <param name="purchaseGoodsCount">商品数量.</param>
        /// <param name="extDic">扩展参数.</param>
        private static void OnPurchaseEvent(string purchaseGoodsid, string purchaseGoodsName, double purchasePrice,
                                bool purchaseSuccess, int purchaseCurrencyUnity, string purchaseGoodsType, int purchaseGoodsCount,
                                Dictionary<string,string> extDic)
        {
            string extMap = JsonHelper.ToJson(extDic);
            _plugin.Call("onPurchaseEvent", purchaseGoodsid, purchaseGoodsName, purchasePrice,
                purchaseSuccess, purchaseCurrencyUnity, purchaseGoodsType, purchaseGoodsCount, extMap);

        }

        /// <summary>
        /// 浏览事件上报.
        /// </summary>
        /// <param name="browseId">浏览内容id.</param>
        /// <param name="browseName">内容名称(非空).</param>
        /// <param name="browseType">内容类型.</param>
        /// <param name="browseDuration">浏览时长，单位秒.</param>
        /// <param name="extDic">扩展参数.</param>
        private static void OnBrowseEvent(string browseId, string browseName, string browseType, long browseDuration, 
                                Dictionary<string, string> extDic)
        {
            string extMap = JsonHelper.ToJson(extDic);
            _plugin.Call("onBrowseEvent", browseId, browseName, browseType,
                browseDuration, extMap);

        }

        /// <summary>
        /// 注册事件上报.
        /// </summary>
        /// <param name="registerMethod">注册方式(非空).</param>
        /// <param name="registerSuccess">注册是否成功(非空).</param>
        /// <param name="extDic">扩展参数.</param>
        private static void OnRegisterEvent(string registerMethod, bool registerSuccess,
                               Dictionary<string, string> extDic)
        {
            string extMap = JsonHelper.ToJson(extDic);
            _plugin.Call("onRegisterEvent", registerMethod, registerSuccess, extMap);

        }

        /// <summary>
        /// 登录事件上报.
        /// </summary>
        /// <param name="loginMethod">登录方式(非空).</param>
        /// <param name="loginSuccess">登录是否成功(非空).</param>
        /// <param name="extDic">扩展参数.</param>
        private static void OnLoginEvent(string loginMethod, bool loginSuccess,
                               Dictionary<string, string> extDic)
        {
            string extMap = JsonHelper.ToJson(extDic);
            _plugin.Call("onLoginEvent", loginMethod, loginSuccess, extMap);

        }

        /// <summary>
        /// 计算事件上报.
        /// </summary>
        /// <param name="eventId">事件Id(非空).</param>
        /// <param name="eventValues">事件的值(非空).</param>
        /// <param name="extDic">扩展参数.</param>
        private static void OnCalculateEvent(string eventId, double eventValues,
                               Dictionary<string, string> extDic)
        {
            string extMap = JsonHelper.ToJson(extDic);
            _plugin.Call("onCalculateEvent", eventId, eventValues, extMap);

        }

        /// <summary>
        /// 计数事件上报.
        /// </summary>
        /// <param name="eventId">事件Id(非空).</param>
        /// <param name="extDic">扩展参数.</param>
        private static void OnCountEvent(string eventId, 
                               Dictionary<string, string> extDic)
        {
            string extMap = JsonHelper.ToJson(extDic);
            _plugin.Call("onCountEvent", eventId, extMap);

        }


    }
}
