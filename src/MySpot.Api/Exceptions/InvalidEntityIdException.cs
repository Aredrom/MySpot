namespace MySpot.Api.Exceptions;

public class InvalidEntityIdException : CustomException
{
    public Guid Id { get; }

    public InvalidEntityIdException(Guid id)
        : base($"Entity Id {id} is invalid.")
    {
        Id = id;
    }
}
