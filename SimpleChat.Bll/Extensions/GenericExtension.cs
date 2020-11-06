using System;
using System.Reflection;

namespace SimpleChat.Bll.Extensions
{
    internal static class GenericExtension
    {
        internal static FinalModel MapToModel<FinalModel>(this object model) where FinalModel : class, new()
        {
            var modelType = model?.GetType();

            if (model == null || !modelType.IsClass)
                throw new ArgumentNullException();

            var finalModelprops = typeof(FinalModel).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var entity = new FinalModel();

            foreach (var modelProp in modelType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                foreach (var entityProp in finalModelprops)
                {
                    if(modelProp.PropertyType.Equals(entityProp.PropertyType) && modelProp.Name.Equals(entityProp.Name))
                    {
                        entityProp.SetValue(entity, modelProp.GetValue(model));
                    }
                }
            }

            return entity;
        }
    }
}
