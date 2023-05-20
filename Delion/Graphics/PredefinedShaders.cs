using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delion.Graphics
{
    /// <summary>
    /// Ready to use shaders here
    /// </summary>
    public class PredefinedShaders
    {
        public TextShader TextShader;

        public PredefinedShaders()
        {
            TextShader = new TextShader();
        }
    }
}
