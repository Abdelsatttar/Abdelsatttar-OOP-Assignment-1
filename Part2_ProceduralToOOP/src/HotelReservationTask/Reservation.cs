namespace HotelReservationTask;

public class Reservation
{
    public int ReservationId { get; }
    public DateOnly CheckInDate { get; }
    public DateOnly CheckOutDate { get; }
    public Room Room { get; }
    public Guest Guest { get; }
    public ReservationStatus Status { get; private set; }

    public int NumberOfNights => CheckOutDate.DayNumber - CheckInDate.DayNumber;
    public decimal TotalCost => NumberOfNights * Room.NightlyRate;

    public Reservation(
        int reservationId,
        Guest guest,
        Room room,
        DateOnly checkInDate,
        DateOnly checkOutDate)
    {
        ArgumentNullException.ThrowIfNull(guest);
        ArgumentNullException.ThrowIfNull(room);

        if (checkOutDate <= checkInDate)
            throw new ArgumentException("Check-out date must be strictly after check-in date.");

        room.EnsureCanBeBooked(checkInDate, checkOutDate);

        ReservationId = reservationId;
        Guest = guest;
        Room = room;
        CheckInDate = checkInDate;
        CheckOutDate = checkOutDate;
        Status = ReservationStatus.Pending;
    }

    public void Confirm()
    {
        EnsureStatus(ReservationStatus.Pending);
        Status = ReservationStatus.Confirmed;
    }

    public void CheckIn()
    {
        EnsureStatus(ReservationStatus.Confirmed);
        Status = ReservationStatus.CheckedIn;
    }

    public void CheckOut()
    {
        EnsureStatus(ReservationStatus.CheckedIn);
        Status = ReservationStatus.CheckedOut;
    }

    public void Cancel()
    {
        if (Status is not (ReservationStatus.Pending or ReservationStatus.Confirmed))
            throw new InvalidOperationException("Only pending or confirmed reservations can be cancelled.");

        Status = ReservationStatus.Cancelled;
    }

    private void EnsureStatus(ReservationStatus expectedStatus)
    {
        if (Status != expectedStatus)
            throw new InvalidOperationException(
                $"Invalid status transition: {Status} -> {expectedStatus}.");
    }
}
