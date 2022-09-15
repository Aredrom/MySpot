namespace MySpot.Api.Exceptions;

public class InvalidParkingSpotNameException : CustomException
{
    public string ParkingSpotName { get; }

    public InvalidParkingSpotNameException(string parkingSpotName)
        : base($"Parking spot name: {parkingSpotName} is invalid.")
    {
        ParkingSpotName = parkingSpotName;
    }
}
