using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.Users.Register;

public class RegisterUserUseCase
{
    public ResponseRegisteredUserJson Execute(RequestUserJson request)
    {

        Validator(request);
        
        return new ResponseRegisteredUserJson
        {

        };
    }

    private void Validator(RequestUserJson request)
    {
        var validator = new RegisterUserValidator();
        
        var result = validator.Validate(request);

        if (!result.IsValid == false)
        {
            var errorMessages = result.Errors.Select(error => error.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }
}