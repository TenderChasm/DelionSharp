using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Delion.Graphics
{
    public class TextShader : Shader
    {
        private const string TextSampler2DName = "text";
        private const string ResolutionName = "resolution";

        public int TextSampler2DLocation { get; private set; }
        public int ResolutionLocation { get; private set; }

        private const string vertexCode =
            @"#version 330 core
            layout (location = 0) in vec2 aPosInTexture;
            layout (location = 1) in vec2 aPosInAtlasUV;
            layout (location = 2) in float aAtlasID;
            layout (location = 3) in vec4 aColor;

            out vec3 Color;
            out vec2 TexCoord;

            uniform vec2 resolution;

            void main()
            {
                //calculate glyph vertex position in NDC form
                float xPos = (aPosInTexture.x / resolution.x) * 2 - 1;
                float yPos = (1 - (aPosInTexture.y / resolution.y)) * 2 - 1;
                gl_Position = vec4(xPos, yPos, 0, 1.0);
                TexCoord = aPosInAtlasUV;
                Color = vec3(aColor);
            }";

        private const string fragmentCode =
            @"#version 330 core
            in vec3 Color;
            in vec2 TexCoord;
            out vec4 color;

            uniform sampler2D text;

            void main()
            {    
                //We get the alpha value from a texture where it is stored as red component and create colorVector with
                //this alpha
                vec4 sampled = vec4(1.0, 1.0, 1.0, texture(text, vec2(TexCoord.x,1 - TexCoord.y)).r);
                //multiply alpha and desired color vectors to get final color
                color = vec4(Color, 1.0) * sampled.w;
            }";

        public TextShader() : base(vertexCode, fragmentCode)
        {
            BindVariables();
        }

        private void BindVariables()
        {
            TextSampler2DLocation = GL.GetUniformLocation(ShaderProgram, TextSampler2DName);
            ResolutionLocation = GL.GetUniformLocation(ShaderProgram, ResolutionName);
        }

    }
}
