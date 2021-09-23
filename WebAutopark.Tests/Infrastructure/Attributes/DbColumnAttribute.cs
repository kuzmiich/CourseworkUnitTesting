using System;

namespace WebAutopark.Tests.Infrastructure.Attributes
{
    public abstract class DbColumnAttribute : Attribute
    {
        public string Name { get; set; }

        public DbColumnAttribute(string name)
        {
            Name = name;
        }
    }
}