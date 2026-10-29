using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Sequential)]

public struct Vector2D
{
    public float x;
    public float y;
}

public static class MyPlugin
{
    [DllImport("DLL_Unity_GED")]
    private static extern int GetID();

    [DllImport("DLL_Unity_GED")]
    private static extern void SetID(int id);

    [DllImport("DLL_Unity_GED")]
    private static extern Vector2D GetPosition();

    [DllImport("DLL_Unity_GED")]
    private static extern Vector2D SetPosition(float x, float y);

    [DllImport("DLL_Unity_GED")]
    private static extern void SetTimer(int milliseconds);

    [DllImport("DLL_Unity_GED")]
    private static extern bool IsTimerFinished();

    [DllImport("DLL_Unity_GED")]
    private static extern void ResetTimer();

    [DllImport("DLL_Unity_GED")]
    private static extern void AddTime(int milliseconds);

    [DllImport("DLL_Unity_GED")]
    private static extern int GetTimerDuration();

    public static int ID
    {
        get { return GetID(); }
        set { SetID(value); }
    }

    public static Vector2D Position
    {
        get { return GetPosition(); }
    }
}
