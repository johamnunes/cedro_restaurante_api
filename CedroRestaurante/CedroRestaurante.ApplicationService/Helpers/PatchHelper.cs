using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace CedroRestaurante.ApplicationService.Helpers
{
    public static class PatchHelper
    {
        public static ConcurrentDictionary<Type, PropertyInfo[]> TypePropertiesCache =
        new ConcurrentDictionary<Type, PropertyInfo[]>();

        /// <summary>
        /// Ajuste de dados em objetos já existentes
        /// </summary>
        /// <typeparam name="TPatch">Objeto com dados a serem incluidos</typeparam>
        /// <typeparam name="TEntity">Objeto a ser ajustado</typeparam>
        /// <param name="entity"></param>
        /// <param name="patch"></param>
        public static void Patch<TPatch, TEntity>(TEntity entity, TPatch patch)
            where TPatch : class
            where TEntity : class
        {
            PropertyInfo[] properties = TypePropertiesCache.GetOrAdd(
                patch.GetType(),
                (type) => type.GetProperties(BindingFlags.Instance | BindingFlags.Public));

            foreach (PropertyInfo prop in properties)
            {
                if (prop.Name == "CriadoEm" || prop.Name == "AtualizadoEm" || prop.Name == "Id")
                {
                }
                else
                {
                    PropertyInfo orjProp = entity.GetType().GetProperty(prop.Name);
                    object value = prop.GetValue(patch);
                    if (value != null)
                    {
                        orjProp.SetValue(entity, value);
                    }
                }
            }
        }
    }
}
