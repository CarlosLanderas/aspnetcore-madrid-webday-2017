using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PlainConcepts.WebDay.Model;

namespace PlainConcepts.WebDay.Infrastructure.Binders
{
    public class UserInfoBinder : IModelBinder
    {
        private HashSet<string> targetHeaders = new HashSet<string>()
        {
            "user",
            "age",
            "language"
        };
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var result = new ExpandoObject() as IDictionary<string, Object>;
            var request = bindingContext.HttpContext.Request;
            foreach (var header in targetHeaders)
            {
                result.Add(header, request.Headers[header].FirstOrDefault());
            }

            result.Add("Query", request.QueryString);
            bindingContext.Result = ModelBindingResult.Success(result);
            return Task.CompletedTask;
        }
    }
}
