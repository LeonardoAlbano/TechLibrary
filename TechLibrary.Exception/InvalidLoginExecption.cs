using System.Net;

namespace TechLibrary.Exception;

public class InvalidLoginExecption : TechLibraryException
{
    public override List<string> GetErrorMessage() => ["Email e/ou senha invalidos."];

    
    public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
}