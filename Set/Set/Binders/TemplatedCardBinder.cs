using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using AV.Set.Model;
using AV.Web.Set.Models;

namespace AV.Web.Set.Binders
{
    public class TemplatedCardBinder: IModelBinder
    {
        /// <summary>
        /// Binds the model to a value by using the specified controller context and binding context.
        /// </summary>
        /// <returns>
        /// The bound value.
        /// </returns>
        /// <param name="controllerContext">The controller context.</param><param name="bindingContext">The binding context.</param>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var form = controllerContext.HttpContext.Request.Form;            
            var card = new Card()
                           {
                               Color = GetA(form, bindingContext, "Color", colorConverter),
                               Filling = GetA(form, bindingContext, "Filling", fillingConverter),
                               Shape = GetA(form, bindingContext, "Shape", shapeConverter),
                               ShapeCount = GetA(form, bindingContext, "ShapeCount", countConverter)
                           };
            return card;
        }

        CardColor colorConverter(string color)
        {
            CardColor result;
            CardColor.TryParse(color, true, out result);
            return result;
        }

        Shape shapeConverter(string shape)
        {
            Shape result;
            Shape.TryParse(shape, true, out result);
            return result;
        }

        byte countConverter(string count)
        {
            return byte.Parse(count);
        }

        Filling fillingConverter(string filling)
        {
            switch(filling.ToLowerInvariant())
            {
                case "url(#pattern_strips_red)":
                case "url(#pattern_strips_blue)":
                case "url(#pattern_strips_green)":
                            return Filling.Strip;
                case "none": return Filling.Empty;
                default: return Filling.Solid;
            }
        }

        private T GetA<T>(NameValueCollection form, ModelBindingContext bindingContext, string key, Func<string, T> convert) where T : struct
        {
            if (String.IsNullOrEmpty(key)) return convert(null);
            var value = form[bindingContext.ModelName + "[" + key + "]"];
            return convert(value);
        }

        private static string GetColorPresentation(CardColor color)
        {
            return color.ToString();
        }

        private static string GetFillingPresentation(Filling filling, CardColor color)
        {
            switch (filling)
            {
                case Filling.Strip:
                    return "url(#pattern_strips_" + color.ToString() + ")";
                case Filling.Solid:
                    return GetColorPresentation(color);
                case Filling.Empty:
                    return "none";
            }
            return "black";
        }

        static public CardViewModel ToViewModel(Card card)
        {
            return new CardViewModel()
                       {
                                       Shape = card.Shape.ToString(),
                                       ShapeCount = card.ShapeCount,
                                       Filling = GetFillingPresentation(card.Filling, card.Color),
                                       Color = GetColorPresentation(card.Color),
                                       Coordinates = GetCoordinates(card.ShapeCount)
                       };
        }

        private static IEnumerable<int> GetCoordinates(byte shapesCount)
        {
            var step = 150;
            var startPoint = 0;
            switch (shapesCount)
            {
                case 1:
                    startPoint = step;
                    break;
                case 2:
                    startPoint = step / 2;
                    break;
            }
            for (int shapeIndex = 0; shapeIndex < shapesCount; shapeIndex++)
            {
                yield return startPoint;
                startPoint += step;
            }
        }
    }
}