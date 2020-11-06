using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace SimpleChat.Bll.Extensions
{
    internal static class GenericExtension
    {
        internal static FinalModel MapToModel<BaseModel, FinalModel>(this BaseModel model) where FinalModel : class, new() where BaseModel : class, new()
        {
            if (model == null)
                throw new ArgumentNullException();

            var entity = new FinalModel();

            foreach (var modelProp in typeof(BaseModel).GetProperties(BindingFlags.Public | BindingFlags.Instance))
            {
                foreach (var entityProp in typeof(FinalModel).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    if(modelProp.PropertyType.Equals(entityProp.PropertyType) && modelProp.Name == entityProp.Name)
                    {
                        entity.GetType().GetProperty(entityProp.Name).SetValue(entity, modelProp.GetValue(model));
                    }
                }
            }
            return entity;
        }
    }
}
