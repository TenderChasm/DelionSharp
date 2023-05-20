using System.Drawing;
using OpenTK;
using OpenTK.Mathematics;

namespace Delion
{
    public struct BColor
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }
    public class Voxel
    {
        public short x;
        public short y;
        public short z;
        public Color w;
    }
}