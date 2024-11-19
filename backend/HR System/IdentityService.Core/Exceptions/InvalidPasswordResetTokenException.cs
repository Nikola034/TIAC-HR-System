using Common.Exceptions;

namespace Core.Exceptions;

public class InvalidPasswordResetTokenException : BadRequestException
{
    public InvalidPasswordResetTokenException() : base("Provided reset password token is invalid or expired")
    {

    }

}