using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AV.Set.Model;

namespace AV.Web.Set.Binders
{
    public class SetModelBinderProvider: IModelBinderProvider
    {
        /// <summary>
        /// Returns the model binder for the specified type.
        /// </summary>
        /// <returns>
        /// The model binder for the specified type.
        /// </returns>
        /// <param name="modelType">The type of the model.</param>
        public IModelBinder GetBinder(Type modelType)
        {
            if (modelType == typeof(Card))
                    return new TemplatedCardBinder();
            return null;
        }
    }
}