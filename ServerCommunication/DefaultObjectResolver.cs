﻿using Messerli.Utility.Extension;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Messerli.ServerCommunication
{
    public class DefaultObjectResolver : IObjectResolver
    {
        public virtual T Resolve<T>(T current)
        {
            return (T)Resolve(typeof(T), current);
        }

        public virtual object Resolve(Type type, object current)
        {
            return Resolve(new ResolveObject(type, current, null));
        }

        public virtual object Resolve(ResolveObject resolveObject)
        {
            var type = resolveObject.CurrentType;
            var current = resolveObject.Current;

            if (!type.IsEnumerable() || type == typeof(string))
            {
                if (current is null || type.IsEnum || type.IsPrimitive || type == typeof(string))
                {
                    return current is null && !(type.GetDefault() is null)
                        ? throw new ArgumentException($"{nameof(current)} is null, but {type.Name} is not nullable")
                        : current;
                }

                var constructor = type.GetConstructors()?.First();

                if (constructor is null)
                {
                    return current;
                }

                var resolvedParameters = GetParameterValues(constructor, resolveObject);

                return constructor.Invoke(resolvedParameters.ToArray());
            }

            var innerType = type.GetInnerType();
            var list = (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(innerType));

            foreach (var element in (IEnumerable)current)
            {
                list.Add(Resolve(innerType, element));
            }

            return list;
        }

        private IEnumerable<object> GetParameterValues(ConstructorInfo constructor, ResolveObject resolveObject)
        {
            var type = resolveObject.CurrentType;
            var current = resolveObject.Current;

            return GetParameterTypes(constructor)
                .Zip(type.GetPropertyValues(current), Tuple.Create)
                .Select(t => Resolve(new ResolveObject(t.Item1, t.Item2, resolveObject)));
        }

        internal static IEnumerable<Type> GetParameterTypes(ConstructorInfo constructor)
        {
            return constructor.GetParameters().Select(param => param.ParameterType);
        }

    }
}