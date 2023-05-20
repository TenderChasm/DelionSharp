using System;
using System.Runtime.InteropServices;

namespace Delion.Utilities
{
    public class Win32NativeLoader
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryW", SetLastError = true)]
        private static extern IntPtr _LoadLibrary([MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        [DllImport("kernel32.dll", EntryPoint = "GetModuleHandleW", SetLastError = true)]
        private static extern IntPtr _GetModuleHandle([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);

        [DllImport("kernel32.dll", EntryPoint = "GetProcAddress", SetLastError = true)]
        private static extern IntPtr _GetProcAddress(IntPtr hModule, [MarshalAs(UnmanagedType.LPStr)] string lpProcName);

        public IntPtr GetModuleHandle(string name) =>
            _GetModuleHandle(name);

        public IntPtr LoadLibrary(string name) =>
            _LoadLibrary(name);

        public IntPtr GetProcAddress(IntPtr lib, string symbol) =>
            _GetProcAddress(lib, symbol);
    }
}
