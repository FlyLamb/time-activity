using System;

namespace TimeActivityFx {
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class ShaderNameAttribute : Attribute {
        public readonly string shaderParameter;

        public ShaderNameAttribute(string s) {
            shaderParameter = s;
        }
    }
}