using OpenTK.Graphics.OpenGL;
using OpenTK;
using System;
using System.Drawing.Imaging;
using System.IO;
using OpenTK.Mathematics;

namespace Delion.AssetManagement
{
    //Texture asset class
    public class TextureAsset : Asset
    {
        //The Settings string for the TextureAsset deserializes into this enum
        private enum Settings
        {
            SimpleImage
        }

        public int Height { get; private set; }
        public int Width { get; private set; }
        public int TextureHandler { get; private set; }

        public TextureAsset(DatabaseAssetRecord assetInfo) : base(assetInfo)
        {
            LoadFromStreamStandart(Game.AssetManager.FullPath(assetInfo.Path), out int[] rawData, out int height, out int width);
            Height = height;
            Width = width;

            InitStandartTexture(rawData);
        }

        /// <summary>
        /// Create empty texture without alias
        /// After manually creating texture the init function should be called
        /// Init function determine how to initialize structure with different options OpenGl provides
        /// </summary>
        public TextureAsset(int width, int height) : base(null, AssetTypes.Texture)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// loads image from stream in 32 bit per pixel BGRA format
        /// ARGB = BGRA!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        private unsafe void LoadFromStreamStandart(string path, out int[] pixels, out int height, out int width)
        {
            using var stream = new FileStream(path, FileMode.Open);
            var bmp = new System.Drawing.Bitmap(stream);

            pixels = new int[bmp.Width * bmp.Height];
            var bmpData = bmp.LockBits(new System.Drawing.Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            var toCopy = bmp.Width * bmp.Height * 4;
            fixed (void* destPtr = pixels)
                System.Buffer.MemoryCopy((void*)bmpData.Scan0, destPtr, toCopy, toCopy);

            bmp.UnlockBits(bmpData);

            height = bmp.Height;
            width = bmp.Width;

        }

        /// <summary>
        /// Srgb8Alpha8
        /// </summary>
        public void InitStandartTexture(int[] pixels)
        {
            TextureHandler = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureHandler);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.Reduce);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.Reduce);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Srgb8Alpha8, Width, Height, 0,
                    OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, pixels);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        /// <summary>
        /// Used for creating the atlas texture that is filled with 8 bit grayscale bitmaps from FreeType
        /// This 8 bit component is stored in Red channel
        /// </summary>
        public void InitRedComponentAtlasTexture()
        {
            TextureHandler = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, TextureHandler);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)All.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.R8, Width, Height, 0,
                OpenTK.Graphics.OpenGL.PixelFormat.Red, PixelType.UnsignedByte, (IntPtr)null);
        }

        public unsafe void PasteToTextureOnCPU(Vector2i offset, Vector2i dims, OpenTK.Graphics.OpenGL.PixelFormat pixelFormat,
            PixelType pixelType, IntPtr data, int lodLevel = 0)
        {
            GL.TextureSubImage2D(TextureHandler, lodLevel, offset.X, offset.Y, dims.X, dims.Y, pixelFormat, pixelType, data);
        }

        /// <summary>
        /// Outputs bgra format
        /// </summary>
        public void GetPixels(IntPtr array)
        {
            GL.BindTexture(TextureTarget.Texture2D, TextureHandler);
            GL.GetTexImage(TextureTarget.Texture2D, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, array);
        }

        public Vector2 BitmapCoordsToUvCoords(Vector2i coords)
        {
            return new Vector2((Height - 1 - coords.Y) / (float)Height, coords.X / (float)Width);
        }

        public override void FreeForAssetManager()
        {
            GL.DeleteTexture(TextureHandler);
            GL.DeleteBuffer(TextureHandler);
        }
    }
}
