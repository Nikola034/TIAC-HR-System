using Common.Exceptions;

namespace Core.Exceptions;

public class InvalidRefreshTokenException : BadRequestException
{
    public InvalidRefreshTokenException() : base("Provided refresh token is invalid or expired")
    {

    }

}
