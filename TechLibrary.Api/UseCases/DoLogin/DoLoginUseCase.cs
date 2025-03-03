using TechLibrary.Api.Infraestructure;
using TechLibrary.Api.Infraestructure.Security.Cryptography;
using TechLibrary.Api.Infraestructure.Security.Tokens.Access;
using TechLibrary.Communication.Requests;
using TechLibrary.Communication.Responses;
using TechLibrary.Exception;

namespace TechLibrary.Api.UseCases.DoLogin;

public class DoLoginUseCase
{
    public ResponseRegisteredUserJson Execute(RequestLoginJson request)
    {
        var dbContext = new TechLibraryDbContext();

        var user = dbContext.Users.FirstOrDefault(user => user.Email.Equals(request.Email));
        if (user is null)
        {
            throw new InvalidLoginExecption();
        }

        var cryptography = new BCryptAlgorithm();
        var passwordIsValid = cryptography.Verify(request.Password, user);
        if (passwordIsValid == false)
            throw new InvalidLoginExecption();

        var tokenGenerator = new JwtTokenGenerator();
        
        return new ResponseRegisteredUserJson
        {
            Name = user.Name,
            AccessToken = tokenGenerator.Generate(user)
        };
    }
}