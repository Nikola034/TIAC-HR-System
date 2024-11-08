using Common.Exceptions;

namespace Core.Exceptions;

public class InvalidPasswordResetTokenException : BadRequestException
{
    public InvalidPasswordResetTokenException() : base("Provided refresh token is invalid or expired")
    {

    }

}