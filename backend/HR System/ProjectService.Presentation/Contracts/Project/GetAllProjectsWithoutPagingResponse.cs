namespace ProjectService.Presentation.Contracts.Project
{
    public class GetAllProjectsWithoutPagingResponse
    {
        public IEnumerable<Core.Entities.Project> Projects { get; set; }
    }
}
