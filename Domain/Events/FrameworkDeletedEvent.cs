

using Domain.Common;

namespace Domain.Events
{
    public  class FrameworkDeletedEvent : BaseEvent
    {
        public FrameworkDeletedEvent(int id)
        {
            Id = id;
        }

        public int Id { get; }
    }
}
