namespace HotelReservationTask;

public class Guest
{
    private readonly List<Reservation> _reservations = new();

    public int GuestId { get; }
    public string FullName { get; }
    public string PhoneNumber { get; }

    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public Guest(int guestId, string fullName, string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(fullName))
            throw new ArgumentException("Guest full name cannot be empty.", nameof(fullName));

        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new ArgumentException("Guest phone number cannot be empty.", nameof(phoneNumber));

        GuestId = guestId;
        FullName = fullName;
        PhoneNumber = phoneNumber;
    }

    public void AddReservation(Reservation reservation)
    {
        ArgumentNullException.ThrowIfNull(reservation);

        if (reservation.Guest != this)
            throw new InvalidOperationException("The reservation does not belong to this guest.");

        if (_reservations.Contains(reservation))
            throw new InvalidOperationException("This reservation is already in the guest history.");

        _reservations.Add(reservation);
    }
}
