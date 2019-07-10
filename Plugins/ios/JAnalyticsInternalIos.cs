using UnityEngine;
using System.Runtime.InteropServices;

namespace JAnalyticsIos
{
    public class JAnalyticsInternalIos : MonoBehaviour
    {




        /// <summary>
        /// 地理位置上报
        /// </summary>
        /// <param name="latitude">纬度.</param>
        /// <param name="longitude">经度.</param>
        [DllImport("__Internal")]
        internal extern static void _setLatitude(double latitude, double longitude);


        /// <summary>
        /// 开启Crash日志收集,默认是关闭状态.
        /// </summary>
        [DllImport("__Internal")]
        internal extern static void _crashLogON();


        /// <summary>
        /// 设置是否开启 Debug 模式。
        /// <para>Debug 模式将会输出更多的日志信息，建议在发布时关闭。建议在init前调用。</para>
        /// </summary>
        /// <param name="enable">true: 开启；false: 关闭。</param>
        [DllImport("__Internal")]
        internal extern static void _setDebug(bool enable);




        /// <summary>
        /// 统计page开始.
        /// </summary>
        /// <param name="pageName">被统计页面的名字.</param>
        [DllImport("__Internal")]
        internal extern static void _startLogPageView(string pageName);

        /// <summary>
        /// 统计page结束.
        /// </summary>
        /// <param name="pageName">被统计页面的名字.</param>
        [DllImport("__Internal")]
        internal extern static void _stopLogPageView(string pageName);






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
        [DllImport("__Internal")]
        internal extern static void _onPurchaseEvent(string purchaseGoodsid, string purchaseGoodsName, double purchasePrice,
                                bool purchaseSuccess, int purchaseCurrencyUnity, string purchaseGoodsType, int purchaseGoodsCount,
                                string extMap);

        /// <summary>
        /// 浏览事件上报.
        /// </summary>
        /// <param name="browseId">浏览内容id.</param>
        /// <param name="browseName">内容名称(非空).</param>
        /// <param name="browseType">内容类型.</param>
        /// <param name="browseDuration">浏览时长，单位秒.</param>
        /// <param name="extDic">扩展参数.</param>
        [DllImport("__Internal")]
        internal extern static void _onBrowseEvent(string browseId, string browseName, string browseType, long browseDuration,
                                string extMap);

        /// <summary>
        /// 注册事件上报.
        /// </summary>
        /// <param name="registerMethod">注册方式(非空).</param>
        /// <param name="registerSuccess">注册是否成功(非空).</param>
        /// <param name="extDic">扩展参数.</param>
        [DllImport("__Internal")]
        internal extern static void _onRegisterEvent(string registerMethod, bool registerSuccess,
                               string extMap);

        /// <summary>
        /// 登录事件上报.
        /// </summary>
        /// <param name="loginMethod">登录方式(非空).</param>
        /// <param name="loginSuccess">登录是否成功(非空).</param>
        /// <param name="extDic">扩展参数.</param>
        [DllImport("__Internal")]
        internal extern static void _onLoginEvent(string loginMethod, bool loginSuccess,
                               string extMap);

        /// <summary>
        /// 计算事件上报.
        /// </summary>
        /// <param name="eventId">事件Id(非空).</param>
        /// <param name="eventValues">事件的值(非空).</param>
        /// <param name="extDic">扩展参数.</param>
        [DllImport("__Internal")]
        internal extern static void _onCalculateEvent(string eventId, double eventValues,
                               string extMap);

        /// <summary>
        /// 计数事件上报.
        /// </summary>
        /// <param name="eventId">事件Id(非空).</param>
        /// <param name="extDic">扩展参数.</param>
        [DllImport("__Internal")]
        internal extern static void _onCountEvent(string eventId,
                               string extMap);

        /// <summary>
        /// 开发者可以为用户增加账户信息，使统计数据可以以账户维度做统计分析.
        /// </summary>
        /// <param name="account">账号信息.</param>
        /// <param name="sequence">唯一请求识别ID，和结果一起反回.</param>
        /// <param name="gameObjectNameBack">游戏对象名 用于反回结果.</param>
        /// <param name="gameObjectMethodBack">游戏对象中的方法名，此方法只能有一个string 参数， 用于反回结果.</param>
        [DllImport("__Internal")]
        internal extern static void _identifyAccount(string accountMapJson, string extMapJson,
                                 int sequence, string gameObjectNameBack, string gameObjectMethodBack);

        /// <summary>
        /// 解绑当前用账户信息
        /// </summary>
        /// <param name="sequence">唯一请求识别ID，和结果一起反回.</param>
        /// <param name="gameObjectNameBack">游戏对象名 用于反回结果.</param>
        /// <param name="gameObjectMethodBack">游戏对象中的方法名，此方法只能有一个string 参数， 用于反回结果.</param>
        [DllImport("__Internal")]
        internal extern static void _detachAccount(
                                 int sequence, string gameObjectNameBack, string gameObjectMethodBack);

        /// <summary>
        /// 设置统计上报的自动周期，未调用前默认即时上报.
        /// </summary>
        /// <param name="period">周期，单位秒，最小10秒，最大1天，超出范围会打印调用失败日志。传0表示统计数据即时上报.</param>
        [DllImport("__Internal")]
        internal extern static void _setFrequency(int period);


    }
}
