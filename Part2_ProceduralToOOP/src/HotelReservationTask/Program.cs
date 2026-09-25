namespace HotelReservationTask;

internal class Program
{
    private static void Main()
    {
        var hotel = new HotelSystem();

        var guest = new Guest(1, "Ahmed Mohamed", "01012345678");
        var room = new Room(101, RoomType.Double, 1500m);

        hotel.RegisterGuest(guest);
        hotel.AddRoom(room);

        var reservation = hotel.CreateReservation(
            1001,
            guest,
            room,
            new DateOnly(2026, 10, 1),
            new DateOnly(2026, 10, 4));

        Console.WriteLine($"Guest: {guest.FullName}");
        Console.WriteLine($"Room: {reservation.Room.RoomNumber} ({reservation.Room.RoomType})");
        Console.WriteLine($"Status: {reservation.Status}");
        Console.WriteLine($"Nights: {reservation.NumberOfNights}");
        Console.WriteLine($"Total cost: {reservation.TotalCost:0.00}");

        reservation.Confirm();
        reservation.CheckIn();
        Console.WriteLine($"Status after check-in: {reservation.Status}");

        reservation.CheckOut();
        Console.WriteLine($"Status after check-out: {reservation.Status}");
        Console.WriteLine($"Guest reservation history: {guest.Reservations.Count}");

        Console.WriteLine("\nTesting invalid operations...");

        try
        {
            var badReservation = hotel.CreateReservation(
                1002,
                guest,
                room,
                new DateOnly(2026, 11, 10),
                new DateOnly(2026, 11, 9));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validation: {ex.Message}");
        }

        try
        {
            room.ChangeNightlyRate(0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validation: {ex.Message}");
        }

        room.StartMaintenance();

        try
        {
            var maintenanceReservation = hotel.CreateReservation(
                1003,
                guest,
                room,
                new DateOnly(2026, 12, 1),
                new DateOnly(2026, 12, 3));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Validation: {ex.Message}");
        }
    }
}
