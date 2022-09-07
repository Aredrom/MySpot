namespace MySpot.Api.Entities
{
    public class Reservation
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; private set; }
        public string LicensePlate { get; private set; }
        public DateTime Date { get; set; }

        public Reservation(Guid id, string employeeName, string licensePlate, DateTime date)
        {
            Id = id;
            EmployeeName = employeeName;
            LicensePlate = licensePlate;
            Date = date;
        }

        public void ChangeLicensePlate(string licensePlate)
            => LicensePlate = licensePlate;
    }
}
