using Delion.AssetManagement;
using Delion.Text;
using System;
using System.Drawing;
using OpenTK.Mathematics;
using OpenTK.Graphics.OpenGL;
using System.Runtime.InteropServices;

namespace Delion
{
    
    class Program
    {
        public static bool slabs(Vector3 p0, Vector3 p1, Vector3 rayOrigin, Vector3 rayEnd)
        {
            Vector3 invRaydir = -1 * (rayEnd - rayOrigin).Normalized();
            Vector3 t0 = (p0 - rayOrigin) * invRaydir;
            Vector3 t1 = (p1 - rayOrigin) * invRaydir;
            Vector3 tmin = min(t0, t1), tmax = max(t0, t1);
            return max_component(tmin) <= min_component(tmax);
        }

        public static Vector3 min(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Min(a.X, b.X), Math.Min(a.Y, b.Y), Math.Min(a.Z, b.Z));
        }

        public static float  min_component(Vector3 a)
        {
            return Math.Min(Math.Min(a.X, a.Y), a.Z);
        }

        public static Vector3 max(Vector3 a, Vector3 b)
        {
            return new Vector3(Math.Max(a.X, b.X), Math.Max(a.Y, b.Y), Math.Max(a.Z, b.Z));
        }

        public static float max_component(Vector3 a)
        {
            return Math.Max(Math.Max(a.X, a.Y), a.Z);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        unsafe static void Main(string[] args)
        {
            //AllocConsole();
            Console.WriteLine("Test");
            Console.WriteLine(slabs(new Vector3(1, 9, 0), new Vector3(6, 14, 5), new Vector3(1, 1, (float)2), new Vector3(2, 5, (float)2)));
            Game.InitAll();
            //GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);

            /* MarkdownParser parser = new MarkdownParser();
             ReadOnlySpan<char> testString = "hi |italic| kitten |#aa00ffff| rope |!#aa00ffff| |!italic| bye";
             var kek = parser.Parse(testString);


            TextRenderer textRenderer = new TextRenderer();

            TTFontAsset font = (TTFontAsset)Game.AssetManager.Get("ComicSans");
            TTFont processedFont = new TTFont(font, 50);

            Vector2i[] ranges = new Vector2i[]{ new Vector2i(33, 125) };
            
            textRenderer.createSymbolsAtlases(processedFont, ranges, FontMods.Usual);

            TextureAsset targetTexture = new TextureAsset(1024, 1024);
            targetTexture.InitStandartTexture(null);

            textRenderer.RenderStringToTexture("|#ffccccff|kasatochka|!#ffccccff|", new Utilities.Vector4b(255, 255, 255, 255), processedFont, new Vector4i(), targetTexture);

            //TextureAsset texture = textRenderer.FontData[processedFont].Atlases[FontMods.BoldItalic][0];
            TextureAsset texture = targetTexture;

            byte[] output = new byte[texture.Height * texture.Width  * 4];

            fixed(byte* arr = output)
                texture.GetPixels((IntPtr)arr);

            Bitmap map;
            fixed (byte* arr = output)
                map = new Bitmap(texture.Width, texture.Height, texture.Width * 4, System.Drawing.Imaging.PixelFormat.Format32bppArgb, (IntPtr)arr);

            map.Save("output.png", System.Drawing.Imaging.ImageFormat.Png);*/

            Game.Loop();
        }
    }
}