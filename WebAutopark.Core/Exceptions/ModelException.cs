using System;

namespace WebAutopark.Core.Exceptions
{
    public static class ModelException
    {
        public static void IsNotNull<T>(T model, string modelName)
        {
            if (model is null)
            {
                throw new ArgumentNullException(modelName);
            }
        }
    }
}