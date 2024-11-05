namespace ProjectServicePresentation.Contracts;

public class UpdateClientRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Country { get; set; }
}