using System;

namespace Architecture.Domain.Core.Events
{
    public abstract class Message
    {
        public string MessageType { get; set; }
        public Guid AggregateId { get; set; }
        protected Message()
        {
            MessageType = GetType().Name;
        }
    }
}
