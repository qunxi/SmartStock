using System;

namespace Domain.Model
{
    public class StockTransactionStatus : BaseEntity, IAggregateRoot
    {
        private const double TOLERANCE = 0.001;

        public StockTransactionStatus()
        {
            this.Id = Guid.NewGuid();
        }

        public StockTransactionStatus(
            string name,
            string code,
            double open,
            double high,
            double realtime,
            double low,
            double volume,
            double turnover,
            DateTime date)
        {
            this.Id = Guid.NewGuid();
            this.Code = code;
            this.Name = name;
            this.RealTime = realtime;
            this.Open = open;
            this.Close = realtime; // the realtime just close time
            this.High = high;
            this.Low = low;
            this.Volume = volume;
            this.Turnover = turnover;
            this.Date = date;
        }

        public string Name { get; set; } // 股票名称

        public string Code { get; set; } // 股票代码

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

            return obj.GetType() == this.GetType() && this.Equals((StockTransactionStatus)obj);
        }

        public static bool operator ==(StockTransactionStatus left, StockTransactionStatus right)
        {
            return object.ReferenceEquals(left, null) ? object.ReferenceEquals(null, right) : left.Equals(right);
        }

        public static bool operator !=(StockTransactionStatus left, StockTransactionStatus right)
        {
            return !(left == right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Name.GetHashCode();
                hashCode = (hashCode * 397) ^ this.Code.GetHashCode();
                hashCode = (hashCode * 397) ^ this.RealTime.GetHashCode();
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

        protected bool Equals(StockTransactionStatus other)
        {
            return this.Name == other.Name && this.Code == other.Code 
                   && Math.Abs(this.RealTime - other.RealTime) < TOLERANCE && Math.Abs(this.Open - other.Open) < TOLERANCE
                   && Math.Abs(this.Close - other.Close) < TOLERANCE && Math.Abs(this.High - other.High) < TOLERANCE && Math.Abs(this.Low - other.Low) < TOLERANCE
                   && Math.Abs(this.Volume - other.Volume) < TOLERANCE && Math.Abs(this.Turnover - other.Turnover) < TOLERANCE && this.Date == other.Date;
        }
      
    }
}
