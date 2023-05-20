using OpenTK.Graphics.OpenGL;
using System;

namespace Delion.Graphics
{
    //OpenGl shader joint stuff
    public class Shader
    {
        private string vertexShaderSource { get; set; }
        private string fragmentShaderSource { get; set; }
        public int ShaderProgram { get; private set; }
        
        public Shader(string argVertexShaderSource, string argFragmentShaderSource)
        {
            vertexShaderSource = argVertexShaderSource;
            fragmentShaderSource = argFragmentShaderSource;
            CompileShaderProgram();
        }

        public void Use()
        {
            GL.UseProgram(ShaderProgram);
        }

        private void CompileShaderProgram()
        {
            int vertexShader = GL.CreateShader(ShaderType.VertexShader);
            GL.ShaderSource(vertexShader, vertexShaderSource);
            GL.CompileShader(vertexShader);
            
            string infoLogVert = GL.GetShaderInfoLog(vertexShader);
            if (infoLogVert != string.Empty)
                Console.WriteLine(infoLogVert);

            int fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentShaderSource);
            GL.CompileShader(fragmentShader);
            
            string infoLogFrag = GL.GetShaderInfoLog(fragmentShader);
            if (infoLogFrag != string.Empty)
                Console.WriteLine(infoLogFrag);
            int status;
            GL.GetShader(fragmentShader, ShaderParameter.CompileStatus, out status);
            Console.WriteLine(status);

            ShaderProgram = GL.CreateProgram();
            GL.AttachShader(ShaderProgram, vertexShader);
            GL.AttachShader(ShaderProgram, fragmentShader);
            GL.LinkProgram(ShaderProgram);

            string infoLogProgram = GL.GetProgramInfoLog(ShaderProgram);
            if (infoLogProgram != string.Empty)
                Console.WriteLine(infoLogProgram);

            GL.DeleteShader(vertexShader);
            GL.DeleteShader(fragmentShader);
        }   
    }
}
