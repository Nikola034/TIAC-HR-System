using Common.Exceptions;

namespace Core.Exceptions;

public class ProjectDoesNotExistException :BadRequestException
{
    public ProjectDoesNotExistException() : base("Project does not exist")
    {
        
    }
}