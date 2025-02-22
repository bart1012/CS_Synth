using System.Runtime.InteropServices;

namespace AudioAppService
{
    [ComImport]
    [Guid("A95664D2-9614-4F35-A746-DE8DB63617E6")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    public interface IMMDeviceEnumerator
    {
        int GetDefaultAudioEndpoint(int dataFlow, int role, out IMMDevice ppEndpoint);
        int EnumAudioEndpoints(int dataFlow, int dwStateMask, out IntPtr ppDevices);
    }
}
