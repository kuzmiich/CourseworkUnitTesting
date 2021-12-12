using System;

namespace WebAutopark.Core.Exceptions
{
    public static class RepositoryException
    {
        public static void IsIdValid(int id)
        {
            if (id <= 0)
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