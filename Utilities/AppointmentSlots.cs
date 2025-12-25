namespace QwiikTechnicalTest.Utilities
{
    public static class AppointmentSlots
    {
        public static readonly TimeOnly Start = new(8, 0);
        public static readonly TimeOnly End = new(16, 0);
        public static readonly TimeSpan SlotDuration = TimeSpan.FromHours(1);

        public static bool IsValidSlot(TimeOnly time)
        {
            return time >= Start && time < End
                && ((time.Hour - Start.Hour) % 1 == 0);
        }
    }
}
