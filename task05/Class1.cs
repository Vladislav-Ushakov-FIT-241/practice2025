using System;
using System.Reflection;
using System.Collections.Generic;

namespace task05
{
    public class ClassAnalyzer
    {
        private Type _type;

        public ClassAnalyzer(Type type)
        {
            _type = type;
        }

        public IEnumerable<string> GetPublicMethods()
        {
            var methods = _type.GetMethods().Select(x => x.Name).ToList();
            return methods;
        }

        public IEnumerable<string> GetMethodParams(string methodName)
        {
            var parameters = _type.GetMethod(methodName).GetParameters().Select(x => x.Name).ToList();
            return parameters;
        }

        public IEnumerable<string> GetAllFields()
        {
            var fields = _type.GetRuntimeFields().Select(x => x.Name).ToList();
            return fields;
        }

        public IEnumerable<string> GetProperties()
        {
            var properties = _type.GetProperties().Select(x => x.Name).ToList();
            return properties;
        }

        public bool HasAttribute<T>() where T : Attribute
        {
            return _type.IsDefined(typeof(T));
        }
    }
}
