using Delion.AssetManagement;
using OpenTK;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Delion.Graphics;
using OpenTK.Windowing.Common;
using Delion.Text;

namespace Delion
{
    public static class Game
    {
        public static Settings Settings { get; private set; }
        public static AssetManager AssetManager { get; private set; }
        public static PredefinedShaders PredefinedShaders { get; set; }
        private static GameWindow window { get; set; }

        /// <summary>
        /// call this before everything
        /// </summary>
        public static void InitAll()
        {
            Settings = new Settings();

            InitWindow();
            InitSubsystems();

            textRenderer = new TextRenderer();

            TTFontAsset font = (TTFontAsset)Game.AssetManager.Get("ComicSans");
            processedFont = new TTFont(font, 50);

            Vector2i[] ranges = new Vector2i[] { new Vector2i(0x410, 0x450), new Vector2i(0x20, 0x80) };

            textRenderer.createSymbolsAtlases(processedFont, ranges, FontMods.Usual);
            textRenderer.createSymbolsAtlases(processedFont, ranges, FontMods.Bold);
            textRenderer.createSymbolsAtlases(processedFont, ranges, FontMods.Italic);

            targetTexture = new TextureAsset(1024, 1024);
            targetTexture.InitStandartTexture(null);

        }

        private static void InitSubsystems()
        {

            AssetManager = new AssetManager("Assets", "Resources.json");

            PredefinedShaders = new PredefinedShaders();
        }
        public static void InitWindow()
        {
            NativeWindowSettings windowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(Settings.Params.XResolution, Settings.Params.YResolution),
                Title = Settings.Name,
            };
            window = new GameWindow(GameWindowSettings.Default, windowSettings);
            window.VSync = VSyncMode.On;

            GL.PixelStore(PixelStoreParameter.UnpackAlignment, 1);

            //connect Update and Render functions to the OpenTK loop
            window.RenderFrame += RenderFrame;
        }

        public static TextRenderer textRenderer;
        public static TTFont processedFont;
        public static TextureAsset targetTexture;

        public static void RenderFrame(FrameEventArgs args)
        {
            window.ProcessEvents();
            textRenderer.RenderStringToScreen(
                "|#ff2222ff|I can't believe |!#ff2222ff| but |italic| it finally |!italic||#11ff22ff||bold| works >d<|!bold||!#11ff22ff|", 
                new Utilities.Vector4b(255, 255, 255, 255), processedFont, new Vector4i(100,140,0,200));
            window.SwapBuffers();
        }

        public static void Loop()
        {
            window.Run();
        }

    }
}
