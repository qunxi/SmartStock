using System;
using System.Collections.Generic;

namespace Domain.Model
{
    /*public class TransactionStatus
    {
        private const double TOLERANCE = 0.001;

        public TransactionStatus()
        {
        }

        public TransactionStatus(
            double open,
            double high,
            double realtime,
            double low,
            double volume,
            double turnover,
            DateTime date)
        {
            this.RealTime = realtime;
            this.Open = open;
            this.Close = realtime; // the realtime just close time
            this.High = high;
            this.Low = low;
            this.Volume = volume;
            this.Turnover = turnover;
            this.Date = date;
        }


        public double RealTime { get; set; } // 实时价

        public double Open { get; set; } // 开盘价

        public double Close { get; set; } // 收盘价

        public double High { get; set; } // 最高价

        public double Low { get; set; } // 最低价

        public double Volume { get; set; } // 成交量

        public double Turnover { get; set; } // 成交额

        public DateTime Date { get; set; } // 日期

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

            return obj.GetType() == this.GetType() && this.Equals((TransactionStatus)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.RealTime.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Open.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Close.GetHashCode();
                hashCode = (hashCode * 397) ^ this.High.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Low.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Volume.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Turnover.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Date.GetHashCode();
                return hashCode;
            }
        }

        protected bool Equals(TransactionStatus other)
        {
            return Math.Abs(this.RealTime - other.RealTime) < TOLERANCE && Math.Abs(this.Open - other.Open) < TOLERANCE
                   && Math.Abs(this.Close - other.Close) < TOLERANCE && Math.Abs(this.High - other.High) < TOLERANCE && Math.Abs(this.Low - other.Low) < TOLERANCE
                   && Math.Abs(this.Volume - other.Volume) < TOLERANCE && Math.Abs(this.Turnover - other.Turnover) < TOLERANCE && this.Date == other.Date;
        }
    }*/

    public class Stock : BaseEntity, IAggregateRoot
    {
        private readonly List<StockTransactionStatus> _transactionStatus;
 
        public Stock(string name, string code)
        {
            this.Name = name;
            this.Code = code;
            this._transactionStatus = new List<StockTransactionStatus>();
            this.Id = Guid.NewGuid();
        }

        public string ShortcutName { get; set; } // 缩写名

        public string Name { get; set; } // 股票名字

        public string Code { get; set; } // 股票代码

        public IEnumerable<StockTransactionStatus> TransactionStatus  // 交易状态
        {
            get
            {
                return this._transactionStatus;
            }
        }

        public void AddTransactionStatus(StockTransactionStatus status)
        {
            this._transactionStatus.Add(status);
        }


        public static bool operator ==(Stock left, Stock right)
        {
            return left != null ? left.Equals(right) : object.ReferenceEquals(null, right);
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
        }
    }
}
