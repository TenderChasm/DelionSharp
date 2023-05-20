using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Graphics.OpenGL;

namespace Delion.Graphics
{
    class ModelShader : Shader
    {
        //3 matrix variables in shader source must bear this names so that Locations can be obtained 
        private const string ModelMatrixVariableName = "ModelMatrix";
        private const string ViewMatrixVariableName = "ViewMatrix";
        private const string ProjectionMatrixVariableName = "ProjectionMatrix";

        public int ModelMatrixLocation { get; private set; }
        public int ViewMatrixLocation { get; private set; }
        public int ProjectionMatrixLocation { get; private set; }

        public ModelShader(string argVertexShaderSource, string argFragmentShaderSource) : 
            base(argVertexShaderSource, argFragmentShaderSource)
        {
            BindVariables();
        }

        private void BindVariables()
        {
            ModelMatrixLocation = GL.GetUniformLocation(ShaderProgram, ModelMatrixVariableName);
            ViewMatrixLocation = GL.GetUniformLocation(ShaderProgram, ViewMatrixVariableName);
            ProjectionMatrixLocation = GL.GetUniformLocation(ShaderProgram, ProjectionMatrixVariableName);
        }
    }
}
