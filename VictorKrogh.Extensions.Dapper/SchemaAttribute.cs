namespace Dapper;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class SchemaAttribute : Attribute
{
    public SchemaAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
