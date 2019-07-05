using UnityEngine;
using System.Collections.Generic;
using JAnalytics;

//#if UNITY_ANDROID
//#endif

//#if UNITY_IPHONE
//#endif

public class PluginsDemoGUI
{
   public static string str_unity = "";
	public static  int callbackId = 0;
    public static GameObject gameObject;
    // Use this for initialization
   public static void Start(GameObject gameObject)
    {
        gameObject.name = "Main Camera";
        PluginsDemoGUI.gameObject = gameObject;
    }

    public static void Update()
    {
        if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Home))
        {
            Application.Quit();
        }
    }

    public static void CallBack(string json)
    {
        str_unity = json;
        Debug.Log("CallBack----json-----" + json);
    }


    public static void OnGUI()
    {
        str_unity = GUILayout.TextField(str_unity, GUILayout.Width(Screen.width - 80),
        GUILayout.Height(100));
        if (GUILayout.Button("onPageStart", GUILayout.Height(80)))
        {
            JAnalyticsBinding.OnPageStart("testOnPage-1");
        }

        if (GUILayout.Button("onPageEnd", GUILayout.Height(80)))
        {
            JAnalyticsBinding.OnPageEnd("testOnPage-1");
        }

        if (GUILayout.Button("onPurchaseEvent", GUILayout.Height(80)))
        {
            string purchaseGoodsid= "goodsId";
            string purchaseGoodsName = "篮球";
            double purchasePrice = 300;
            bool purchaseSuccess = true;
            Currency purchaseCurrencyUnity = Currency.CNY;
            string purchaseGoodsType = "sport";
            int purchaseGoodsCount = 1;

            PurchaseEvent eEvent = new PurchaseEvent(purchaseGoodsid, purchaseGoodsName,
                purchasePrice, purchaseSuccess, purchaseCurrencyUnity, purchaseGoodsType, purchaseGoodsCount);
            eEvent.addKeyValue("1", "a");
            eEvent.addKeyValue("2", "b");
            JAnalyticsBinding.OnEvent(eEvent);
        }


        if (GUILayout.Button("onBrowseEvent", GUILayout.Height(80)))
        {
            string browseId = "browse_id";
            string browseName = "深圳热点新闻";
            string browseType = "news";
            long browseDuration = 30;

            BrowseEvent eEvent = new BrowseEvent(browseId, browseName,
                browseType, browseDuration);
            eEvent.addKeyValue("1", "a");
            eEvent.addKeyValue("2", "b");

            JAnalyticsBinding.OnEvent(eEvent);
        }

        if (GUILayout.Button("onRegisterEvent", GUILayout.Height(80)))
        {
            string registerMethod = "sina";
            bool registerSuccess = true;


            RegisterEvent eEvent = new RegisterEvent(registerMethod, registerSuccess);
            eEvent.addKeyValue("1", "a");
            eEvent.addKeyValue("2", "b");


            JAnalyticsBinding.OnEvent(eEvent);
        }

        if (GUILayout.Button("onLoginEvent", GUILayout.Height(80)))
        {
            string loginMethod = "qq";
            bool loginSuccess = true;

            LoginEvent eEvent = new LoginEvent(loginMethod, loginSuccess);
            eEvent.addKeyValue("1", "a");
            eEvent.addKeyValue("2", "b");

            JAnalyticsBinding.OnEvent(eEvent);
        }

        if (GUILayout.Button("onCalculateEvent", GUILayout.Height(80)))
        {
            string eventId = "onCalculateEvent_event_id";
            double eventValue = 10.1;

            CalculateEvent eEvent = new CalculateEvent(eventId, eventValue);
            eEvent.addKeyValue("1", "a");
            eEvent.addKeyValue("2", "b");

            JAnalyticsBinding.OnEvent(eEvent);
        }

        if (GUILayout.Button("onCountEvent", GUILayout.Height(80)))
        {
            string eventId = "onCountEvent_event_id";

            CountEvent eEvent = new CountEvent(eventId);
            eEvent.addKeyValue("1", "a");
            eEvent.addKeyValue("2", "b");

            JAnalyticsBinding.OnEvent(eEvent);
        }



        if (GUILayout.Button("identifyAccount", GUILayout.Height(80)))
        {
            Account account = new Account("AccountAccount");
            account.SetName("nnnnn");


            account.SetExtraAttr("k1", "v1");
            account.SetExtraAttr("k2", null);

            int sequence = callbackId++;
            string gameObjectNameBack = gameObject.name;
            string gameObjectMethodBack = "AccountCallBack";
            JAnalyticsCallBack callBack = new JAnalyticsCallBack(sequence, gameObjectNameBack, gameObjectMethodBack);
            JAnalyticsBinding.IdentifyAccount(account, callBack);
        }

        if (GUILayout.Button("detachAccount", GUILayout.Height(80)))
        {
            int sequence = callbackId++;
            string gameObjectNameBack = gameObject.name;
            string gameObjectMethodBack = "AccountCallBack";
            JAnalyticsCallBack callBack = new JAnalyticsCallBack(sequence, gameObjectNameBack, gameObjectMethodBack);

            JAnalyticsBinding.DetachAccount(callBack);
        }

        if (GUILayout.Button("setAnalyticsReportPeriod", GUILayout.Height(80)))
        {
            int period = 30;
            JAnalyticsBinding.SetAnalyticsReportPeriod(period);
        }

    }
}
