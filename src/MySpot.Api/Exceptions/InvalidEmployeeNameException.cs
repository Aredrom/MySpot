namespace MySpot.Api.Exceptions;

public class InvalidEmployeeNameException : CustomException
{
    public string EmployeeName { get; }

    public InvalidEmployeeNameException(string employeeName)
        : base($"Employee name: {employeeName} is invalid.")
    {
        EmployeeName = employeeName;
    }
}
