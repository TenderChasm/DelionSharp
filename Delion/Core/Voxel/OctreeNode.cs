using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Delion.Core.Voxel
{
    [StructLayout(LayoutKind.Explicit)]
    unsafe public struct OctreeNode
    {
        [FieldOffset(0)]
        private int parentIndex;
        [FieldOffset(4)]
        private fixed int childrenIndex[8];
        [FieldOffset(4)]
        private fixed int childrenMaterials[8];

        public int ChildrenIndexGet(int index) => childrenIndex[index];

    }


    [StructLayout(LayoutKind.Explicit)]
    unsafe public struct OctreeLeaf
    {
        [FieldOffset(0)]
        public ushort materialID;
    }
}
