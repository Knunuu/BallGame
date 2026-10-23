using UnityEngine;

public class PluginExample : MonoBehaviour
{
    void Start()
    {
        MyPlugin.ID = 10;
        Debug.Log(MyPlugin.ID);
    }
}
