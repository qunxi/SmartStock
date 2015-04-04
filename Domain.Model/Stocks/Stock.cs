using System;
using System.Collections.Generic;
using Infrastructure.Domain.Model;

namespace Domain.Model.Stocks
{
    public class Stock : BaseEntity, IAggregateRoot
    {
        public Stock(string name, string code)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Code = code;
            this.DailyTransactionStatus = new List<TransactionStatus>();
        }

        public string ShortcutName { get; set; } // 缩写名

        public string Name { get; set; } // 股票名字

        public string Code { get; set; } // 股票代码

        public IEnumerable<TransactionStatus> DailyTransactionStatus { get; set; }  // 交易状态

        //public void AddTransactionStatus(TransactionStatus status)
        //{
        //    this._dailyTransactionStatus.Add(status);
        //}

        #region comment
        /*public static bool operator ==(Stock left, Stock right)
        {
            return object.ReferenceEquals(left, null) ? object.ReferenceEquals(null, right) : left.Equals(right);
        }

        public static bool operator !=(Stock left, Stock right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Name != null ? this.Name.GetHashCode() : 0;
                hashCode = (hashCode * 397) ^ (this.Code != null ? this.Code.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(null, obj))
            {
                return false;
            }

            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }

            return obj.GetType() == this.GetType() && this.Equals((Stock)obj);
        }

        protected bool Equals(Stock other)
        {
            return string.Equals(this.Name, other.Name) && string.Equals(this.Code, other.Code);
        }*/
        #endregion comment
    }
}
