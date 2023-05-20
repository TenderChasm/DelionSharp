using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Delion.AssetManagement;
using Delion.Utilities;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;
using Delion.Graphics;

namespace Delion.Text
{
    public enum FontMods
    {
        Usual,
        Italic,
        Bold,
        BoldItalic
    }

    /// <summary>
    /// Contains Atlases and Glyph Dictionaries for the each font modificator
    /// FontRenderData divides into 4 subparts.Each subpart contains atlases and glyphPositioning data
    /// for a certain fontModificator
    /// "Usual" is the standart choice
    /// </summary>
    public class FontRenderData
    {

        public Dictionary<FontMods, List<TextureAsset>> Atlases { get; private set; }

        public Dictionary<FontMods, Dictionary<char, GlyphInfo>> CharacterPositioning { get; private set; }

        public FontRenderData()
        {

            Atlases = new Dictionary<FontMods, List<TextureAsset>>();
            Atlases.Add(FontMods.Usual, new List<TextureAsset>());
            Atlases.Add(FontMods.Italic, new List<TextureAsset>());
            Atlases.Add(FontMods.Bold, new List<TextureAsset>());
            Atlases.Add(FontMods.BoldItalic, new List<TextureAsset>());

            CharacterPositioning = new Dictionary<FontMods, Dictionary<char, GlyphInfo>>();
            CharacterPositioning.Add(FontMods.Usual, new Dictionary<char, GlyphInfo>());
            CharacterPositioning.Add(FontMods.Italic, new Dictionary<char, GlyphInfo>());
            CharacterPositioning.Add(FontMods.Bold, new Dictionary<char, GlyphInfo>());
            CharacterPositioning.Add(FontMods.BoldItalic, new Dictionary<char, GlyphInfo>());
        }
    }

    /// <summary>
    /// TextRenderer maintain rendering text to the screen and textures
    /// It has the dictionary where the key is Font and the value is atlases with font glyphs and their character data(FontRenderData)
    /// When the TextRender sees a new font in the query he generates atlases and data for it and store it in dictionary
    /// Currently there are usual,italic,bold,bold-italic atlases holding glyphs of the font with different modificator
    /// Next time he will use a pregenerated solution
    /// </summary>
    public class TextRenderer
    {
        //hope is is enough
        public Vector2i AtlasDimensions { get; } = new Vector2i(1024, 1024);
        public Dictionary<TTFont, FontRenderData> FontData { get; private set; }

        private const int vertexFloatLength = 9;


        public TextRenderer()
        {
            FontData = new Dictionary<TTFont, FontRenderData>();
        }

        /// <summary>
        /// Creates array of atlases of certain Font and his FontModificator and fills them with symbols 
        /// of the given font and unicode ranges
        /// After generating atlases and characters data she adds them to the correspondent Atlas and CharacterPositioning
        /// properties in FontRenderData of TTFont in FontData dictionary
        /// </summary>
        public unsafe void createSymbolsAtlases(TTFont font, Vector2i[] unicodeRanges, FontMods mod)
        {
            if (!FontData.ContainsKey(font))
                FontData.Add(font, new FontRenderData());

            int currentAtlasIndex;
            TextureAsset makeshiftAtlas;

            //If font already have some atlases for mod, retreive last existing one and try to fill in it,otherwise create new one
            if (FontData[font].Atlases[mod].Count > 0)
            {
                currentAtlasIndex = FontData[font].Atlases[mod].Count - 1;
                makeshiftAtlas = FontData[font].Atlases[mod][^1];
            }
            else
            {
                makeshiftAtlas = new TextureAsset(AtlasDimensions.X, AtlasDimensions.Y);
                makeshiftAtlas.InitRedComponentAtlasTexture();
                currentAtlasIndex = 0;
            }

            AtlasPacker packer = new AtlasPacker(makeshiftAtlas);

            foreach (Vector2i range in unicodeRanges)
            {
                for(int charCode = range.X; charCode < range.Y; charCode++)
                {
                    byte* bitmap;

                    GlyphInfo glyph = font.GetCharacter(mod, charCode, &bitmap);

                    tryToPlaceGlyphAgainIfAtlasIsFull:

                    bool hasSpace = packer.PutGlyph(ref glyph, bitmap);

                    if(!hasSpace)
                    {
                        FontData[font].Atlases[mod].Add(makeshiftAtlas);

                        makeshiftAtlas = new TextureAsset(AtlasDimensions.X, AtlasDimensions.Y);
                        makeshiftAtlas.InitRedComponentAtlasTexture();
                        packer = new AtlasPacker(makeshiftAtlas);

                        goto tryToPlaceGlyphAgainIfAtlasIsFull;
                    }
                    else
                    {
                        glyph.AtlasID = currentAtlasIndex;
                        FontData[font].CharacterPositioning[mod].Add((char)charCode, glyph);
                    }
                }
            }

            FontData[font].Atlases[mod].Add(makeshiftAtlas);

        }

        /// <summary>
        /// Renders string to the screen with the given padding and returns number of rendered chars
        /// Padding order: top, right, bottom, left
        /// </summary>
        public unsafe int RenderStringToScreen(string text, Vector4b color, TTFont font, Vector4i padding)
        {
            // 0 fbo means screen FBO
            return RenderStringToFbo(text, color, font, padding, 0,
                new Vector2i(Game.Settings.Params.XResolution, Game.Settings.Params.YResolution));
        }


        /// <summary>
        /// Renders string to the texture(creating FBO from it) with the given padding and returns number of rendered chars
        /// Padding order: top, right, bottom, left
        /// </summary>
        public unsafe int RenderStringToTexture(string text, Vector4b color, TTFont font, Vector4i padding, TextureAsset texture)
        {
            int framebuffer = GL.GenFramebuffer();
            GL.BindFramebuffer(FramebufferTarget.Framebuffer, framebuffer);
            GL.FramebufferTexture(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, texture.TextureHandler, 0);

            int count = RenderStringToFbo(text, color, font, padding, framebuffer, new Vector2i(texture.Width, texture.Height));

            GL.DeleteFramebuffer(framebuffer);

            return count;
        }


        /// <summary>
        /// Renders string to the FBO with the given padding and returns number of rendered chars
        /// Padding order: top, right, bottom, left
        /// </summary>
        public unsafe int RenderStringToFbo(string text, Vector4b color, TTFont font, Vector4i padding,
            int framebuffer, Vector2i framebufferDims)
        {
            //transform glyphs data to the vertex array
            Span<float> vertices = stackalloc float[text.Length * vertexFloatLength * 4];

            Vector2i glyphBaselinePositionCursor = new Vector2i(padding.W, padding.X + font.OptimalBaselineSpacing);

            //parse markdowned string and get chars and their modificators
            MarkdownParser parser = new MarkdownParser();
            Span<MarkdownSymbolData> stringToDraw = parser.Parse(text).Span;

            //We can pass only one atlas at a time to the shader,but we may need several,
            //because different chars in a string can relay on a different atlases
            //So we create the atlas-keyed dict and fill its value lists in a loop with VBOs of characters
            //from a string those relay on a specific atlas
            Dictionary<TextureAsset, List<float>> charsVBOsPerAtlas = new Dictionary<TextureAsset, List<float>>();

            //fill VBO with vertex data from each symbol
            int symbolNumber = 0;
            foreach (MarkdownSymbolData symbolData in stringToDraw)
            {
                char symbol = symbolData.symbol;

                GlyphInfo glyphData;
                bool res = FontData[font].CharacterPositioning[symbolData.Style].TryGetValue(symbol, out glyphData);

                //if there is no such character then get glyph data of the 0 char,symbol that can't be rendered.
                //Its bitmap is a tofu.So we show a tofu when don't know what to render
                if(!res)
                {
                    glyphData = FontData[font].CharacterPositioning[symbolData.Style][(char)0];
                }

                if (!charsVBOsPerAtlas.ContainsKey(FontData[font].Atlases[symbolData.Style][glyphData.AtlasID]))
                    charsVBOsPerAtlas.Add(FontData[font].Atlases[symbolData.Style][glyphData.AtlasID], new List<float>());

                //check if the glyph won't fit in the line and if so then move cursor a line down and to the left
                //Break loop if still can't fit
                if(glyphBaselinePositionCursor.X + glyphData.Advance > framebufferDims.X - padding.Y)
                {
                    glyphBaselinePositionCursor.X = padding.W;
                    glyphBaselinePositionCursor.Y += font.OptimalBaselineSpacing;

                    if (glyphBaselinePositionCursor.Y > framebufferDims.Y - padding.Z)
                        break;
                }

                //get vertices positions for glyph corners in the canvas and his texture in the atlas
                Vector2 topLeftVertex = new Vector2(glyphBaselinePositionCursor.X + glyphData.Bearing.X,
                        glyphBaselinePositionCursor.Y - glyphData.Bearing.Y);
                Vector2 bottomLeftVertex = new Vector2(glyphBaselinePositionCursor.X + glyphData.Bearing.X,
                        glyphBaselinePositionCursor.Y + (glyphData.Size.Y - glyphData.Bearing.Y));
                Vector2 topRightVertex = new Vector2(glyphBaselinePositionCursor.X + glyphData.Bearing.X + glyphData.Size.X,
                        glyphBaselinePositionCursor.Y - glyphData.Bearing.Y);
                Vector2 bottomRightVertex = new Vector2(glyphBaselinePositionCursor.X + glyphData.Bearing.X + glyphData.Size.X,
                        glyphBaselinePositionCursor.Y + (glyphData.Size.Y - glyphData.Bearing.Y));

                Vector2 PixelToUV(int coordX, int coordY)
                {
                    return new Vector2((float)coordX / AtlasDimensions.X, 1 - ((float)coordY / AtlasDimensions.Y));
                }

                Vector2 topLeftUV = PixelToUV(glyphData.AtlasPos.X, glyphData.AtlasPos.Y);
                Vector2 bottomLeftUV = PixelToUV(glyphData.AtlasPos.X, glyphData.AtlasPos.Y + glyphData.Size.Y);
                Vector2 topRightUV = PixelToUV(glyphData.AtlasPos.X + glyphData.Size.X, glyphData.AtlasPos.Y);
                Vector2 bottomRightUV = PixelToUV(glyphData.AtlasPos.X + glyphData.Size.X, glyphData.AtlasPos.Y + glyphData.Size.Y);

                float id = 0;

                Vector4b symbolColor = symbolData.Color ?? color;

                float NormalizeColor(byte channel) => (float)channel / 255;

                //fill VBO
                TextureAsset relatedAtlas = FontData[font].Atlases[symbolData.Style][glyphData.AtlasID];

                charsVBOsPerAtlas[relatedAtlas].Add(topLeftVertex.X);
                charsVBOsPerAtlas[relatedAtlas].Add(topLeftVertex.Y);
                charsVBOsPerAtlas[relatedAtlas].Add(topLeftUV.X);
                charsVBOsPerAtlas[relatedAtlas].Add(topLeftUV.Y);
                charsVBOsPerAtlas[relatedAtlas].Add(id);
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.R));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.G));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.B));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.A));

                charsVBOsPerAtlas[relatedAtlas].Add(bottomLeftVertex.X);
                charsVBOsPerAtlas[relatedAtlas].Add(bottomLeftVertex.Y);
                charsVBOsPerAtlas[relatedAtlas].Add(bottomLeftUV.X);
                charsVBOsPerAtlas[relatedAtlas].Add(bottomLeftUV.Y);
                charsVBOsPerAtlas[relatedAtlas].Add(id);
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.R));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.G));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.B));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.A));

                charsVBOsPerAtlas[relatedAtlas].Add(bottomRightVertex.X);
                charsVBOsPerAtlas[relatedAtlas].Add(bottomRightVertex.Y);
                charsVBOsPerAtlas[relatedAtlas].Add(bottomRightUV.X);
                charsVBOsPerAtlas[relatedAtlas].Add(bottomRightUV.Y);
                charsVBOsPerAtlas[relatedAtlas].Add(id);
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.R));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.G));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.B));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.A));

                charsVBOsPerAtlas[relatedAtlas].Add(topRightVertex.X);
                charsVBOsPerAtlas[relatedAtlas].Add(topRightVertex.Y);
                charsVBOsPerAtlas[relatedAtlas].Add(topRightUV.X);
                charsVBOsPerAtlas[relatedAtlas].Add(topRightUV.Y);
                charsVBOsPerAtlas[relatedAtlas].Add(id);
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.R));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.G));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.B));
                charsVBOsPerAtlas[relatedAtlas].Add(NormalizeColor(symbolColor.A));

                glyphBaselinePositionCursor.X += glyphData.Advance;
            }

            //Now we iterate through the dict and do a drawcall for each atlas,passing it to the shader,
            //also passing and rendering all related to it characters VBOs
            foreach(KeyValuePair<TextureAsset, List<float>> atlas in charsVBOsPerAtlas)
            {
                int verticesCount = atlas.Value.Count / vertexFloatLength;
                int symbolInVboCount = verticesCount / 4;

                //GL.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
                InitVaoAndVbo(MemoryMarshal.Cast<float, byte>(CollectionsMarshal.AsSpan(atlas.Value)), out int vao, out int vbo);

                Game.PredefinedShaders.TextShader.Use();

                GL.BindVertexArray(vao);
                GL.BindTexture(TextureTarget.Texture2D, atlas.Key.TextureHandler);
                GL.Uniform2(Game.PredefinedShaders.TextShader.ResolutionLocation, framebufferDims);

                //We use rendering with Triangles so must represent each quad as 2 triangles
                //A triangle pair share 2 vertices and with intention to not duplicate them in VBO
                //we create EBO and arrange indices of quads vertices in it in order to form triangles 
                int ebo = GL.GenBuffer();
                GL.BindBuffer(BufferTarget.ElementArrayBuffer, ebo);
                Span<int> indices = stackalloc int[verticesCount / 2 * 3];
                int indicesSize = verticesCount / 2 * 3;
                for (int i = 0; i < symbolInVboCount; i++)
                {
                    indices[0 + i * 6] = 0 + i * 4;
                    indices[1 + i * 6] = 1 + i * 4;
                    indices[2 + i * 6] = 2 + i * 4;
                    indices[3 + i * 6] = 0 + i * 4;
                    indices[4 + i * 6] = 2 + i * 4;
                    indices[5 + i * 6] = 3 + i * 4;
                }

                fixed(void* indicesPtr = indices)
                GL.BufferData(BufferTarget.ElementArrayBuffer,indicesSize * sizeof(int),
                    (IntPtr)indicesPtr, BufferUsageHint.DynamicDraw);

                GL.DrawElements(PrimitiveType.Triangles, indicesSize, DrawElementsType.UnsignedInt, 0);

                GL.DeleteVertexArray(vao);
                GL.DeleteBuffer(vbo);
                GL.DeleteBuffer(ebo);
            }
            return symbolNumber;

        }



        private unsafe void InitVaoAndVbo(Span<byte> vertices, out int vao, out int vbo)
        {
            //don't forget to change vertexFloatLength!!!
            //create VAO and VBO for a glyph vertex. VAO layout: X, Y, UV.X, UV.Y, ID, R, G, B, A
            vao = GL.GenVertexArray();
            GL.BindVertexArray(vao);

            vbo = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, vbo);
            fixed(void* data = vertices)
                GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length, (IntPtr)data, BufferUsageHint.StaticDraw);

            GL.VertexAttribPointer(0, 2, VertexAttribPointerType.Float, false, vertexFloatLength * sizeof(float), 0);
            GL.EnableVertexAttribArray(0);
            // color attribute
            GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, vertexFloatLength * sizeof(float), 2 * sizeof(float));
            GL.EnableVertexAttribArray(1);

            GL.VertexAttribPointer(2, 1, VertexAttribPointerType.Float, false, vertexFloatLength * sizeof(float), 4 * sizeof(float));
            GL.EnableVertexAttribArray(2);

            GL.VertexAttribPointer(3, 4, VertexAttribPointerType.Float, false, vertexFloatLength * sizeof(float), 5 * sizeof(float));
            GL.EnableVertexAttribArray(3);
        }

    }
}
