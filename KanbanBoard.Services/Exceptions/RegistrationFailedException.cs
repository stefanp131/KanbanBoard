using System;

namespace KanbanBoard.Services.Exceptions;

public class RegistrationFailedException : Exception
{
    public RegistrationFailedException()
    {
    }

    public RegistrationFailedException(string message) : base(message)
    {
    }
}