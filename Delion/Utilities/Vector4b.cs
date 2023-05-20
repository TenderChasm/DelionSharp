using System.Runtime.InteropServices;


namespace Delion.Utilities
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Vector4b
    {
        [FieldOffset(0)] public byte X;
        [FieldOffset(1)] public byte Y;
        [FieldOffset(2)] public byte Z;
        [FieldOffset(3)] public byte W;

        [FieldOffset(0)] public byte R;
        [FieldOffset(1)] public byte G;
        [FieldOffset(2)] public byte B;
        [FieldOffset(3)] public byte A;

        public Vector4b(byte x = 0, byte y = 0, byte z = 0, byte w = 0)
        {
            R = 0;
            G = 0;
            B = 0;
            A = 0;

            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public static bool operator ==(Vector4b vec1, Vector4b vec2)
        {
            return vec1.Equals(vec2);
        }

        public static bool operator !=(Vector4b vec1, Vector4b vec2)
        {
            return !(vec1 == vec2);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
            {
                return false;
            }

            Vector4b VecObj = (Vector4b)obj;
            if (X == VecObj.X && Y == VecObj.Y && Z == VecObj.Z && W == VecObj.W)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
