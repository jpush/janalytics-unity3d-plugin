using UnityEngine;

public class PluginsDemoIos : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        PluginsDemoIosGUI.Start(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        PluginsDemoIosGUI.Update();
    }

    void OnGUI()
    {
        PluginsDemoIosGUI.OnGUI();
    }

    // 开发者自定义的账号操作反回结果方法
    void AccountCallBack(string jsonStr)
    {
        Debug.Log("AccountCallBack----jsonStr-----" + jsonStr);
        PluginsDemoIosGUI.CallBack(jsonStr);

    }
}
