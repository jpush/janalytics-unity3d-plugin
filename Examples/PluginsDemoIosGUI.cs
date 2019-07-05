using UnityEngine;
using JAnalytics;
using JAnalyticsIos;

public static class PluginsDemoIosGUI
{
    // Use this for initialization
   public static void Start(GameObject gameObject)
    {
        PluginsDemoGUI.Start(gameObject);
        JAnalyticsBinding.SetDebugMode(true);
    }

    // Update is called once per frame
    public static void Update()
    {
        PluginsDemoGUI.Update();
    }

    public static void CallBack(string json)
    {

        Debug.Log("IosCallBack----json-----" + json);
        PluginsDemoGUI.CallBack(json);
    }

    public static void OnGUI()
    {

        GUILayout.Button("DemoIos", GUILayout.Height(80));
        PluginsDemoGUI.OnGUI();

        if (GUILayout.Button("CrashLogON", GUILayout.Height(80)))
        {
            JAnalyticsBindingIos.CrashLogON();
        }

        if (GUILayout.Button("setLatitude", GUILayout.Height(80)))
        {
            double latitude = 100.1;
            double longitude = 200.1;
            JAnalyticsBindingIos.SetLatitude(latitude, longitude);
        }

    }
}
