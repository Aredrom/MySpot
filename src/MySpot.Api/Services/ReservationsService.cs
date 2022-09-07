using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;

namespace MySpot.Api.Services;

public sealed class ReservationsService
{
        private static WeeklyParkingSpot[] _weeklyParkingSpots =
        {
            new (Guid.NewGuid(), DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(5), "P1"),
            new (Guid.NewGuid(), DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(5), "P2"),
            new (Guid.NewGuid(), DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(5), "P3"),
            new (Guid.NewGuid(), DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(5), "P4"),
            new (Guid.NewGuid(), DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(5), "P5"),
        };

        public IEnumerable<ReservationDto> GetAllWeekly()
            => _weeklyParkingSpots
                .SelectMany(x => x.Reservations)
                .Select(x => new ReservationDto
                {
                    Id = x.Id,
                    EmployeeName = x.EmployeeName,
                    Date = x.Date
                });
        public ReservationDto Get(Guid id)
            => GetAllWeekly().SingleOrDefault(x => x.Id == id);

        public Guid? Create(CreateReservation command)
        {
            var (parkingSpotId, reservationId, employeeName, licensePlate, date) = command;

            var weeklyParkingSpot = _weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);

            if (weeklyParkingSpot is null)
            {
                return default;
            }

            var reservation = new Reservation(reservationId, employeeName, licensePlate, date);

            weeklyParkingSpot.AddReservation(reservation);
            return reservation.Id;
        }

        public bool Update(ChangeReservationLicensePlate command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if(weeklyParkingSpot is null)
            {
                return false;
            }

            var reservation = weeklyParkingSpot.Reservations
                .SingleOrDefault(x => x.Id == command.ReservationId);

            if (reservation is null)
            {
                return false;
            }

            reservation.ChangeLicensePlate(command.LicensePlate);
            return true;
        }

        public bool Delete(DeleteReservation command)
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if(weeklyParkingSpot is null)
            {
                return false;
            }

            weeklyParkingSpot.RemoveReservation(command.ReservationId);
            return true;
        }

        private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(Guid id)
            => _weeklyParkingSpots
                .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
}

