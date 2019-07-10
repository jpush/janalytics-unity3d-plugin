#if UNITY_ANDROID
using JAnalyticsAndroid;
#endif

#if UNITY_IPHONE
using JAnalyticsIos;
#endif

namespace JAnalytics
{
    public class JAnalyticsBinding
    {


        /// <summary>
        /// 设置是否开启 Debug 模式。
        /// <para>Debug 模式将会输出更多的日志信息，建议在发布时关闭。建议在init前调用。</para>
        /// </summary>
        /// <param name="enable">true: 开启；false: 关闭。</param>
        public static void SetDebugMode(bool enable)
        {
#if UNITY_ANDROID
            JAnalyticsBindingAndroid.SetDebugMode(enable);
#endif

#if UNITY_IPHONE
            JAnalyticsBindingIos.SetDebugMode(enable);
#endif

        }

        /// <summary>
        /// 统计page开始.
        /// </summary>
        /// <param name="pageName">被统计页面的名字.</param>
        public static void OnPageStart(string pageName)
        {

#if UNITY_ANDROID
            JAnalyticsBindingAndroid.OnPageEnd(pageName);
#endif

#if UNITY_IPHONE
            JAnalyticsBindingIos.OnPageEnd(pageName);
#endif
        }

        /// <summary>
        /// 统计page结束.
        /// </summary>
        /// <param name="pageName">被统计页面的名字.</param>
        public static void OnPageEnd(string pageName)
        {

#if UNITY_ANDROID
            JAnalyticsBindingAndroid.OnPageEnd(pageName);
#endif

#if UNITY_IPHONE
            JAnalyticsBindingIos.OnPageEnd(pageName);
#endif
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

#if UNITY_ANDROID
            JAnalyticsBindingAndroid.IdentifyAccount(account, callBack);
#endif

#if UNITY_IPHONE
            JAnalyticsBindingIos.IdentifyAccount(account,callBack);
#endif
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

#if UNITY_ANDROID
            JAnalyticsBindingAndroid.DetachAccount(callBack);
#endif

#if UNITY_IPHONE
            JAnalyticsBindingIos.DetachAccount(callBack);
#endif
        }

        /// <summary>
        /// 设置统计上报的自动周期，未调用前默认即时上报.
        /// </summary>
        /// <param name="period">周期，单位秒，最小10秒，最大1天，超出范围会打印调用失败日志。传0表示统计数据即时上报.</param>
        public static void SetAnalyticsReportPeriod(int period)
        {

#if UNITY_ANDROID
            JAnalyticsBindingAndroid.SetAnalyticsReportPeriod(period);
#endif

#if UNITY_IPHONE
            JAnalyticsBindingIos.SetAnalyticsReportPeriod(period);
#endif
        }

        /// <summary>
        /// 自定义事件
        /// </summary>
        /// <param name="e">E.</param>
        public static void OnEvent(JAnalytics.Event e)
        {

#if UNITY_ANDROID
            JAnalyticsBindingAndroid.OnEvent(e);
#endif

#if UNITY_IPHONE
            JAnalyticsBindingIos.OnEvent(e);
#endif
        }


    }
}
