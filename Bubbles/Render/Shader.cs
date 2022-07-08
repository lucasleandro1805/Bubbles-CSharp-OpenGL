using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

namespace Bubbles.Render
{
    internal class Shader
    {
        private int handle;
        private string vertex;
        private string fragment;
        private bool rendering;

        public Shader(string vertexCode, string fragmentCode)
        {
            // GL.CreateShader will create an empty shader (obviously). The ShaderType enum denotes which type of shader will be created.
            var vertexShader = GL.CreateShader(ShaderType.VertexShader);

            // Now, bind the GLSL source code
            GL.ShaderSource(vertexShader, vertexCode);

            // And then compile
            CompileShader(vertexShader);

            var fragmentShader = GL.CreateShader(ShaderType.FragmentShader);
            GL.ShaderSource(fragmentShader, fragmentCode);
            CompileShader(fragmentShader);

            // These two shaders must then be merged into a shader program, which can then be used by OpenGL.
            // To do this, create a program...
            handle = GL.CreateProgram();

            // Attach both shaders...
            GL.AttachShader(handle, vertexShader);
            GL.AttachShader(handle, fragmentShader);

            // And then link them together.
            LinkProgram(handle);

            // When the shader program is linked, it no longer needs the individual shaders attached to it; the compiled code is copied into the shader program.
            // Detach them, and then delete them.
            GL.DetachShader(handle, vertexShader);
            GL.DetachShader(handle, fragmentShader);
            GL.DeleteShader(fragmentShader);
            GL.DeleteShader(vertexShader);

            // The shader is now ready to go, but first, we're going to cache all the shader uniform locations.
            // Querying this from the shader is very slow, so we do it once on initialization and reuse those values
            // later.

            // First, we have to get the number of active uniforms in the shader.
            GL.GetProgram(handle, GetProgramParameterName.ActiveUniforms, out var numberOfUniforms);
        }

        private static void CompileShader(int shader)
        {
            // Try to compile the shader
            GL.CompileShader(shader);

            // Check for compilation errors
            GL.GetShader(shader, ShaderParameter.CompileStatus, out var code);
            if (code != (int)All.True)
            {
                // We can use `GL.GetShaderInfoLog(shader)` to get information about the error.
                var infoLog = GL.GetShaderInfoLog(shader);
                throw new Exception($"Error occurred whilst compiling Shader({shader}).\n\n{infoLog}");
            }
        }

        private static void LinkProgram(int program)
        {
            // We link the program
            GL.LinkProgram(program);

            // Check for linking errors
            GL.GetProgram(program, GetProgramParameterName.LinkStatus, out var code);
            if (code != (int)All.True)
            {
                // We can use `GL.GetProgramInfoLog(program)` to get information about the error.
                throw new Exception($"Error occurred whilst linking Program({program})");
            }
        }

        // A wrapper function that enables the shader program.
        public bool BeginRender()
        {
            if (rendering)
            {
                throw new Exception("shader already running");
            }
            rendering = true;
            GL.UseProgram(handle);
            return true;
        }

        public void StopRender()
        {
            if (!rendering)
            {
                throw new Exception("shader is not rendering");
            }
            rendering = false;
        }

        // The shader sources provided with this project use hardcoded layout(location)-s. If you want to do it dynamically,
        // you can omit the layout(location=X) lines in the vertex shader, and use this in VertexAttribPointer instead of the hardcoded values.
        public int GetAttribLocation(string attribName)
        {
            return GL.GetAttribLocation(handle, attribName);
        }
        public int GetUniformlocation(string name)
        {
            return GL.GetUniformLocation(handle, name);
        }

    }
}
