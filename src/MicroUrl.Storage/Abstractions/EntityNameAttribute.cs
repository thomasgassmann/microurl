namespace MicroUrl.Storage.Abstractions
{
    using System;

    public class EntityNameAttribute : Attribute
    {
        public EntityNameAttribute(string name) =>
            Name = name;

        public string Name { get; }
    }
}
