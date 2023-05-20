using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.GL;
using OpenTK.Mathematics;

namespace Delion.Text
{
    //struct represents a character to render in font
    public struct GlyphInfo
    {
        public int AtlasID;
        public Vector2i AtlasPos;
        public Vector2i Size;       // Size of glyph
        public Vector2i Bearing;    // Offset from baseline to left/top of glyph
        public int Advance;
    }
}
