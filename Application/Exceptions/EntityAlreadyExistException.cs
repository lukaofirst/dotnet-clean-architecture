namespace Application.Exceptions;

public class EntityAlreadyExistException(string message) : Exception(message)
{
}
