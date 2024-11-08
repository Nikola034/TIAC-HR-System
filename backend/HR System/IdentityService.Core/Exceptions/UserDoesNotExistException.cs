using Common.Exceptions;

namespace Core.Exceptions;

public class UserDoesNotExistException :BadRequestException
{
    public UserDoesNotExistException() : base("User with given email does not exist")
    {
        
    }
}