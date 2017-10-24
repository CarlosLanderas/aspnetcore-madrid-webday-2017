using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PlainConcepts.WebDay.API.Test.Request;
using PlainConcepts.WebDay.Model;

namespace PlainConcepts.WebDay.Infrastructure.Binders
{
    public class PaginationRequestBinder : IModelBinder
    {

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var userInboundRequest = new InboundRequest();
            
            userInboundRequest.Page = GetFromQuery(nameof(InboundRequest.Page), bindingContext);
            userInboundRequest.Sorting = GetFromQuery(nameof(InboundRequest.Sorting), bindingContext);
            userInboundRequest.Language = GetHeader(nameof(InboundRequest.Language), bindingContext);
            userInboundRequest.ClientId = GetHeader(nameof(InboundRequest.ClientId), bindingContext);

            userInboundRequest.Query = bindingContext.HttpContext.Request.QueryString;

            bindingContext.Result = ModelBindingResult.Success(userInboundRequest);
            return Task.CompletedTask;
        }

        private string GetHeader(string headerName, ModelBindingContext context)
        {
            return context.HttpContext.Request.Headers[headerName].FirstOrDefault();
        }

        private string GetFromQuery(string parameter, ModelBindingContext context)
        {
            return context.HttpContext.Request.Query[parameter].FirstOrDefault() ?? default(string);
        }
    }
}
