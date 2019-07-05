using UnityEngine;

public class PluginsDemoAndroid: MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        PluginsDemoAndroidGUI.Start(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        PluginsDemoAndroidGUI.Update();
    }

    void OnGUI()
    {
        PluginsDemoAndroidGUI.OnGUI();
    }

    // 开发者自定义的账号操作反回结果方法
    void AccountCallBack(string jsonStr)
    {
        Debug.Log("AccountCallBack----jsonStr-----" + jsonStr);
        PluginsDemoAndroidGUI.CallBack(jsonStr);

    }
}
