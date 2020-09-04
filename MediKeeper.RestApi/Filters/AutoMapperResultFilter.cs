using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;

namespace MediKeeper.RestApi.Filters
{
    public class AutoMapperResultFilter : ResultFilterAttribute
    {
        public Type SourceType { get; set; }
        public Type DestinationType { get; set; }

        /// <summary>
        /// Will map the properties from sourceType to destinationType as defined in the AutoMapper profile.
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="destinationType"></param>
        public AutoMapperResultFilter(Type sourceType, Type destinationType)
        {
            this.SourceType = sourceType;
            this.DestinationType = destinationType;
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

            IMapper mapper = (IMapper)context.HttpContext.RequestServices.GetService(typeof(IMapper));
            var results = mapper.Map(resultFromAction.Value, SourceType, DestinationType);
            resultFromAction.Value = results;

            await next();
        }
    }
}
