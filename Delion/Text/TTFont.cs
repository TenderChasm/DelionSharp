using Delion.AssetManagement;
using Delion.Bindings.FreeType;
using System;
using System.Text;
using OpenTK.Mathematics;

namespace Delion.Text
{
    //font object wrapper on the FreeType face
    public unsafe class TTFont
    {
        public TTFontAsset AttachedFont { get; }

        //there are no typedefs in C# so FT_Face == FT_FaceRec_*
        private FT_FaceRec_* face;

        private IntPtr stroker;

        public IntPtr Face => (IntPtr)face;
        public int Size { get; }
        public int PixelWidth { get; }
        public int MaxHeightFromBaseline { get; }
        public int OptimalBaselineSpacing { get; }

        public TTFont(TTFontAsset font, int size, int pixelWidth = 0)
        {
            PixelWidth = pixelWidth;
            Size = size;
            AttachedFont = font;

            //create face by obtaing ptrs to the Face and the FullPath string
            fixed(FT_FaceRec_** facePtr = &face)
                fixed(byte* path = Encoding.ASCII.GetBytes(AttachedFont.FullPath))
                    Ft.NewFace(Ft.Instance, (sbyte*)path, 0, facePtr);

            Ft.SetPixelSizes(face, (uint)PixelWidth, (uint)Size);
            Ft.SelectCharmap(face, Ft.ENCODING_UNICODE);

            MaxHeightFromBaseline = face->Ascender / 64;
            OptimalBaselineSpacing = face->Height / 64;
        }

        /// <summary>
        /// Return GlyphInfo with character metrics and a grayscale bitmap as the out parameter;
        /// </summary>
        /// <param name="charCode"></param>
        /// <returns></returns>
        public GlyphInfo GetCharacter(FontMods mod, int charCode, byte** bitmap)
        {
            Ft.LoadChar(face, (uint)charCode, Ft.LOAD_DEFAULT);

            FT_GlyphRec_* glyphFT;
            Ft.GetGlyph(face->Glyph, &glyphFT);

            if (mod == FontMods.Italic || mod == FontMods.BoldItalic)
            {
                var matrix = new FT_Matrix_
                {
                    Xx = 1 << 16,
                    Xy = (int)(0.2F * 0xFFFF),
                    Yx = 0,
                    Yy = 1 << 16
                };

                Ft.GlyphTransform(glyphFT, &matrix, (FT_Vector_*)0);
            }

            if(mod == FontMods.Bold || mod == FontMods.BoldItalic)
            {
                CreateStroker();

                Ft.StrokerSet(stroker, face->Height / 40, Ft.STROKER_LINECAP_SQUARE, Ft.STROKER_LINECAP_SQUARE, 0);

                Ft.GlyphStrokeBorder(&glyphFT, stroker, 0, 1);
            }

            Ft.GlyphToBitmap(&glyphFT, Ft.RENDER_MODE_NORMAL, (FT_Vector_*)0, 1);

            GlyphInfo glyph = new GlyphInfo();

            glyph.Size.X = (int)((FT_BitmapGlyphRec_*)glyphFT)->Bitmap.Width;

            glyph.Size.Y = (int)((FT_BitmapGlyphRec_*)glyphFT)->Bitmap.Rows;

            glyph.Bearing = new Vector2i((int)face->Glyph->BitmapLeft, (int)face->Glyph->BitmapTop);
            glyph.Advance = face->Glyph->Advance.X / 64;

            *bitmap = ((FT_BitmapGlyphRec_*)glyphFT)->Bitmap.Buffer;

            return glyph;
        }

        private void CreateStroker()
        {
            if (stroker != IntPtr.Zero)
                return;

            fixed (void* strokerPtr = &stroker)
                Ft.StrokerNew(Ft.Instance, (IntPtr)strokerPtr);
        }
    }
}
