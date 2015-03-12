﻿using System;

namespace Infrastructure.Domain.Model
{
    public abstract class BaseEntity : IEntity
    {
        public Guid Id { get; set; }

        #region Equality
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (object.ReferenceEquals(this, obj))
                return true;

            BaseEntity entity = obj as BaseEntity;
            return entity != null && this.Id.Equals(entity.Id);
        }

        public static bool operator ==(BaseEntity left, BaseEntity right)
        {
            return !ReferenceEquals(left, null) && left.Equals(right);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        #endregion
    }
}
