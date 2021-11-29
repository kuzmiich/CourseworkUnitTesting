using System;

namespace WebAutopark.Core.Exceptions
{
    public static class RepositoryException
    {
        public static void IsIdValid(Guid id)
        {
            if (id.CompareTo(Guid.Empty) <= 0)// <= 0
            {
                throw new InvalidArgumentException(nameof(id));
            }
        }

        public static void IsEntityExists<TEntity>(TEntity entity, string entityName)
        {
            if (entity is null)
            {
                throw new ObjectNotFoundException(entityName);
            }
        }
    }
}