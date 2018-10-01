using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace WillemseFranceMP.Controllers.Helpers
{
    public static class MvcHtmlHelpers
    {
        public static MvcHtmlString DescriptionFor<TModel, TValue>(this HtmlHelper<TModel> self, Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, self.ViewData);
            var description = metadata.Description;

         //   return MvcHtmlString.Create(string.Format(@"<span class >{0}</span>", description));
         return MvcHtmlString.Create(
           //  string.Format("<a style=\"color: orange;font-size:20px;font-weight: bold\" href =\"#\" data-toggle=\"tooltip\" data-placement=\"right\" title=\"{0}\">i</a>", description));

            string.Format("<span class=\"badge pull-left\"  title=\"{0}\">i</span>", description));



        }
    }


}