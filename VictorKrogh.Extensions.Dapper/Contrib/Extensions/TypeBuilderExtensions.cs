using System.Reflection;

namespace System
{
    public static class TypeExtensions
    {
        public static Type GetCorrectType(this Type type)
        {
            ArgumentNullException.ThrowIfNull(type);

            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                if (elementType is null)
                {
                    return type;
                }
                return elementType;
            }
            else if (type.IsGenericType)
            {
                var typeInfo = type.GetTypeInfo();

                var implementsGenericIEnumerableOrIsGenericIEnumerable =
                    typeInfo.ImplementedInterfaces.Any(ti => ti.IsGenericType && ti.GetGenericTypeDefinition() == typeof(IEnumerable<>)) ||
                    typeInfo.GetGenericTypeDefinition() == typeof(IEnumerable<>);

                if (implementsGenericIEnumerableOrIsGenericIEnumerable)
                {
                    return type.GetGenericArguments().First();
                }
            }

            return type;
        }
    }
}

namespace System.Reflection.Emit
{
    public static class TypeBuilderExtensions
    {
        public static void DefineMethodOverrideNullable(this TypeBuilder typeBuilder, MethodInfo? methodInfoBody, MethodInfo? methodInfoDeclaration)
        {
            ArgumentNullException.ThrowIfNull(methodInfoBody);
            ArgumentNullException.ThrowIfNull(methodInfoDeclaration);

            typeBuilder.DefineMethodOverride(methodInfoBody, methodInfoDeclaration);
        }
    }
}