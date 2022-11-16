using MediatR;
using SistemaAcademicoApplication.Common.Responses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace SistemaAcademicoApplication.Common.Behaviours
{

    public class RequestDataAnnotationValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse> where TResponse : IResponse, new()
    {
        
        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            ValidationContext validationContext = new ValidationContext(request);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(request, validationContext, results, true);

            return isValid
                ? next()
                : Errors(results);
        }
        private static Task<TResponse> Errors(IEnumerable<ValidationResult> failures)
        {
            var response = new TResponse();

            foreach (var failure in failures)
            {
                var compositeValidationResult = (CompositeValidationResult)failure;
                foreach(var error in compositeValidationResult.Results)
                {
                    response.AddError(error.ErrorMessage);
                }
            }

            return Task.FromResult(response);
        }
    }
}
