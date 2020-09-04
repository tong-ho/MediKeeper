using MediKeeper.RestApi.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Threading.Tasks;

namespace MediKeeper.RestApi.Filters
{
    public class PaginationResultFilter : ResultFilterAttribute
    {
        public string PaginationKey { get; set; }
        public string QueryParametersKey { get; set; }

        /// <summary>
        /// Requires that the pagination metadata be stored in HttpContext.Items in the paginationMetadataKey and queryParametersKey
        /// </summary>
        /// <param name="paginationMetadataKey"></param>
        /// <param name="queryParametersKey"></param>
        public PaginationResultFilter(string paginationMetadataKey, string queryParametersKey)
        {
            PaginationKey = paginationMetadataKey;
            QueryParametersKey = queryParametersKey;
        }

        public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var resultFromAction = context.Result as ObjectResult;
            if (resultFromAction?.Value == null
                || resultFromAction.StatusCode < 200
                || resultFromAction.StatusCode >= 300)
            {
                await next();
                return;
            }

            if (context.HttpContext.Items[PaginationKey] is PaginationMetadata paginationMetadata &&
                context.HttpContext.Items[QueryParametersKey] is QueryParameters queryParameters)
            {
                paginationMetadata.PreviousPageLink = paginationMetadata.HasPrevious ?
                    CreateResourceUri(context.HttpContext, queryParameters, ResourceUriType.PreviousPage) :
                    null;

                paginationMetadata.NextPageLink = paginationMetadata.HasNext ?
                    CreateResourceUri(context.HttpContext, queryParameters, ResourceUriType.NextPage) :
                    null;

                context.HttpContext.Response.Headers.Add(PaginationKey, JsonConvert.SerializeObject(paginationMetadata,
                    new JsonSerializerSettings
                    {
                        ContractResolver = new CamelCasePropertyNamesContractResolver()
                    }));
            }

            await next();
        }

        private string CreateResourceUri(HttpContext httpContext, QueryParameters queryParameters, ResourceUriType resourceUriType)
        {
            LinkGenerator linkgenerator = (LinkGenerator)httpContext.RequestServices.GetService(typeof(LinkGenerator));
            switch (resourceUriType)
            {
                case ResourceUriType.PreviousPage:
                    return linkgenerator.GetUriByAction(
                        httpContext,
                        httpContext.GetRouteValue("action") as string,
                        httpContext.GetRouteValue("controller") as string,
                        values: new
                        {
                            sortBy = queryParameters.SortBy,
                            pageNumber = queryParameters.PageNumber - 1,
                            pageSize = queryParameters.PageSize
                        });
                case ResourceUriType.NextPage:
                    return linkgenerator.GetUriByAction(
                        httpContext,
                        httpContext.GetRouteValue("action") as string,
                        httpContext.GetRouteValue("controller") as string,
                        values: new
                        {
                            sortBy = queryParameters.SortBy,
                            pageNumber = queryParameters.PageNumber + 1,
                            pageSize = queryParameters.PageSize
                        });
                default:
                    return null;
            }
        }
    }
}
