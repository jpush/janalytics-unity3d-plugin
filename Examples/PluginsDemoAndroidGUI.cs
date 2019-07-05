using UnityEngine;
using JAnalytics;
using JAnalyticsAndroid;

public static class PluginsDemoAndroidGUI
{
    //Use this for initialization
    public static void Start(GameObject gameObject)
    {
        PluginsDemoGUI.Start(gameObject);
        JAnalyticsBinding.SetDebugMode(true);
        JAnalyticsBindingAndroid.Init();
    }

    // Update is called once per frame
    public static void Update()
    {
        PluginsDemoGUI.Update();
    }

    public static void CallBack(string json)
    {

        Debug.Log("AndroidCallBack----json-----" + json);
        PluginsDemoGUI.CallBack(json);
    }

    public static void OnGUI()
    {

        GUILayout.Button("DemoAndroid", GUILayout.Height(80));
        PluginsDemoGUI.OnGUI();

        if (GUILayout.Button("initCrashHandler", GUILayout.Height(80)))
        {
            JAnalyticsBindingAndroid.InitCrashHandler();
        }


        if (GUILayout.Button("stopCrashHandler", GUILayout.Height(80)))
        {
            JAnalyticsBindingAndroid.StopCrashHandler();
        }

        if (GUILayout.Button("setChannel", GUILayout.Height(80)))
        {
            //string channel = null;
            string channel = "test_channel";
            JAnalyticsBindingAndroid.SetChannel(channel);
        }

    }
}
