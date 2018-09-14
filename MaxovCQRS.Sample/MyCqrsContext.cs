using MaxovCQRS.Common;

namespace MaxovCQRS.Sample
{
    public class MyCqrsContext : ICqrsContext
    {
        public int CurrentUserId { get; private set; }

        public MyCqrsContext(int currentUserId)
        {
            CurrentUserId = currentUserId;
        }
    }
}