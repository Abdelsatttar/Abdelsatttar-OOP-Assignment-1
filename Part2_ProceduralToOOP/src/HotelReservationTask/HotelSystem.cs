namespace HotelReservationTask;

public class HotelSystem
{
    private readonly List<Guest> _guests = new();
    private readonly List<Room> _rooms = new();
    private readonly List<Reservation> _reservations = new();

    public IReadOnlyList<Guest> Guests => _guests.AsReadOnly();
    public IReadOnlyList<Room> Rooms => _rooms.AsReadOnly();
    public IReadOnlyList<Reservation> Reservations => _reservations.AsReadOnly();

    public void RegisterGuest(Guest guest)
    {
        ArgumentNullException.ThrowIfNull(guest);

        if (_guests.Any(existing => existing.GuestId == guest.GuestId))
            throw new InvalidOperationException($"Guest ID {guest.GuestId} already exists.");

        _guests.Add(guest);
    }

    public void AddRoom(Room room)
    {
        ArgumentNullException.ThrowIfNull(room);

        if (_rooms.Any(existing => existing.RoomNumber == room.RoomNumber))
            throw new InvalidOperationException($"Room number {room.RoomNumber} already exists.");

        _rooms.Add(room);
    }

    public Reservation CreateReservation(
        int reservationId,
        Guest guest,
        Room room,
        DateOnly checkInDate,
        DateOnly checkOutDate)
    {
        ArgumentNullException.ThrowIfNull(guest);
        ArgumentNullException.ThrowIfNull(room);

        if (_reservations.Any(existing => existing.ReservationId == reservationId))
            throw new InvalidOperationException($"Reservation ID {reservationId} already exists.");

        if (!_guests.Contains(guest))
            throw new InvalidOperationException("Guest is not registered in this hotel.");

        if (!_rooms.Contains(room))
            throw new InvalidOperationException("Room is not registered in this hotel.");

        var reservation = new Reservation(
            reservationId,
            guest,
            room,
            checkInDate,
            checkOutDate);

        guest.AddReservation(reservation);
        room.AddReservation(reservation);
        _reservations.Add(reservation);

        return reservation;
    }
}
