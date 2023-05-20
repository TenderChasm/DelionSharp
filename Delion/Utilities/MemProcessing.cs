using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Delion.Utilities
{
    /// <summary>
    /// C-like functions for memory processing
    /// Use only in the very core
    /// </summary>
    unsafe static class MemProcessing
    {
        public static void* malloc(int size)
        {
            return (void*)Marshal.AllocHGlobal(size);
        }

        public static void free(void* ptr)
        {
            Marshal.FreeHGlobal((IntPtr)ptr);
        }

        public static void memcpy(void* dest, void* src, int n)
        {
            Unsafe.CopyBlock(dest, src, (uint)n);
        }
    }
}
