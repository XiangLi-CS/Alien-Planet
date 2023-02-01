using System.Text;
using Unity.Profiling;
using UnityEngine;

public class MemoryReader : MonoBehaviour
{
    /*The whole MemoryReader is using ProfileRecorder API to access the Memory Profiler module's counters
      System Used Memory can track total memory size of the application in the operating system's task manager
      Total Used Memory can track total value of memory that Unity uses*/

    string statsText;
    ProfilerRecorder systemUsedMemoryRecorder;
    ProfilerRecorder totalUsedMemoryRecorder;

    void OnEnable()
    {
        systemUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
        totalUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Used Memory");
    }

    void OnDisable()
    {
        systemUsedMemoryRecorder.Dispose();
        totalUsedMemoryRecorder.Dispose();
    }

    void Update()
    {
        var sb = new StringBuilder(500);
        if (systemUsedMemoryRecorder.Valid)
            sb.AppendLine($"System Used Memory: {systemUsedMemoryRecorder.LastValue / (1024 * 1024)} MB");
        if (totalUsedMemoryRecorder.Valid)
            sb.AppendLine($"Total Used Memory: {totalUsedMemoryRecorder.LastValue / (1024 * 1024)} MB");
        statsText = sb.ToString();
    }

    void OnGUI()
    {
        GUI.TextArea(new Rect(0, 30, 220, 35), statsText);
    }
}