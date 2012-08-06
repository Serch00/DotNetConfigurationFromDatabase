using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DotNetConfigurationFromDatabase
{
    public static class Private
    {
        public static MethodInfo Method<T>(string methodName)
        {
            return typeof(T).GetMethod(methodName, BindingFlags.Instance | BindingFlags.NonPublic);
        }
    }
}
