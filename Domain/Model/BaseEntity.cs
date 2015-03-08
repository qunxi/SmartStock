using System;

namespace Domain.Model
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }
    }
}
