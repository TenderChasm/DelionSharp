using Delion.AssetManagement;
using System;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;

namespace Delion.Text
{
    /// <summary>
    /// Packs glyphs in the atlas given by a contructor call
    /// The Algorithm is very simple:
    /// put glyphs in a horizontal line and after reaching the right border increment delimeter by the max glyph height of
    /// this row.After this start next row with a vertical offset equaling the delimeter.Glyphs overlap won't happen with a 
    /// such strategy but density is not very good.
    /// </summary>
    public class AtlasPacker
    {
        public TextureAsset Atlas { get; }

        private int delimeter { get; set; }
        private int xPosition { get; set; }

        private int maxGlyphHeight { get; set; }

        public AtlasPacker(TextureAsset atlas)
        {
            Atlas = atlas;
            delimeter = 0;
            xPosition = 0;
            maxGlyphHeight = 0;
        }

        /// <summary>
        /// Puts a glyph in the bitmap and returns false if the bitmap is full and he coudn't place a glyph.
        /// Otherwise returns true
        /// </summary>
        public unsafe bool PutGlyph(ref GlyphInfo glyph, byte* bitmap)
        {
            maxGlyphHeight = Math.Max(maxGlyphHeight, glyph.Size.Y);

            if (delimeter + glyph.Size.Y <= Atlas.Height)
            {
                if (xPosition + glyph.Size.X <= Atlas.Width)
                {
                    Atlas.PasteToTextureOnCPU(new Vector2i(xPosition, delimeter), glyph.Size,
                        PixelFormat.Red, PixelType.UnsignedByte, (IntPtr)bitmap);

                    glyph.AtlasPos = new Vector2i(xPosition, delimeter);

                    xPosition += glyph.Size.X + 1;
                }
                else
                {
                    xPosition = 0;
                    delimeter += maxGlyphHeight + 1;
                    maxGlyphHeight = 0;

                    return PutGlyph(ref glyph, bitmap);
                }
            }
            else
            {
                return false;
            }
            return true;
        }
    }
}
