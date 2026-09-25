namespace HotelReservationTask;

public class Room
{
    private readonly List<Reservation> _reservations = new();

    public int RoomNumber { get; }
    public RoomType RoomType { get; }
    public decimal NightlyRate { get; private set; }
    public bool IsUnderMaintenance { get; private set; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public Room(int roomNumber, RoomType roomType, decimal nightlyRate)
    {
        if (nightlyRate <= 0)
            throw new ArgumentException("Nightly rate must be positive.", nameof(nightlyRate));

        RoomNumber = roomNumber;
        RoomType = roomType;
        NightlyRate = nightlyRate;
    }

    public void ChangeNightlyRate(decimal newRate)
    {
        if (newRate <= 0)
            throw new ArgumentException("Nightly rate must be positive.", nameof(newRate));

        NightlyRate = newRate;
    }

    public void StartMaintenance()
    {
        IsUnderMaintenance = true;
    }

    public void EndMaintenance()
    {
        IsUnderMaintenance = false;
    }

    public bool HasOverlappingActiveReservation(DateOnly checkInDate, DateOnly checkOutDate)
    {
        foreach (var reservation in _reservations)
        {
            if (!IsActive(reservation.Status))
                continue;

            bool overlaps = checkInDate < reservation.CheckOutDate &&
                            checkOutDate > reservation.CheckInDate;

            if (overlaps)
                return true;
        }

        return false;
    }

    public void EnsureCanBeBooked(DateOnly checkInDate, DateOnly checkOutDate)
    {
        if (IsUnderMaintenance)
            throw new InvalidOperationException($"Room #{RoomNumber} is under maintenance.");

        if (HasOverlappingActiveReservation(checkInDate, checkOutDate))
            throw new InvalidOperationException($"Room #{RoomNumber} is already booked for an overlapping period.");
    }

    internal void AddReservation(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);

        if (reservation.Room != this)
            throw new InvalidOperationException("The reservation does not belong to this room.");

        if (_reservations.Contains(reservation))
            throw new InvalidOperationException("This reservation is already linked to the room.");

        _reservations.Add(reservation);
    }

    private static bool IsActive(ReservationStatus status)
    {
        return status is ReservationStatus.Pending
            or ReservationStatus.Confirmed
            or ReservationStatus.CheckedIn;
    }
}
