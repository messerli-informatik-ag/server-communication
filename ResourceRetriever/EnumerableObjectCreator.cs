using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Utility.Extension;

namespace ResourceRetriever
{
    public class EnumerableObjectCreator : DefaultObjectCreator
    {
        public override object CreateInstance(Type type)
        {
            if (!type.IsEnumerable() || type == typeof(string))
            {
                return base.CreateInstance(type);
            }

            var innerType = type.GetInnerType();
            if (innerType is null || !type.IsEnumerable())
            {
                throw new ArgumentNullException(nameof(type));
            }

            return CreateInstance(type, new[] { CreateInstance(innerType) });
        }

        public override object CreateInstance(Type type, IEnumerable<object> parameters)
        {
            if (!type.IsEnumerable() || type == typeof(string))
            {
                return base.CreateInstance(type, parameters);
            }

            var innerType = type.GetInnerType();

            return parameters.Aggregate(
                (IList)Activator.CreateInstance(typeof(List<>).MakeGenericType(innerType)),
                (list, parameter) =>
                {
                    if (!(parameter is null) && !innerType.IsInstanceOfType(parameter))
                    {
                        throw new ArgumentException($"{parameter} is of type {parameter.GetType().Name}, but type {innerType.Name} is required by {type.Name}");
                    }

                    list.Add(parameter);

                    return list;
                });
        }
    }
}