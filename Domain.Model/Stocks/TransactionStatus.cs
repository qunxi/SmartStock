using System;
using Infrastructure.Domain.Model;

namespace Domain.Model.Stocks
{
    public class TransactionStatus : BaseEntity, IAggregateRoot
    {
        // private const double TOLERANCE = 0.001;

        public TransactionStatus()
        {
            this.Id = Guid.NewGuid();
        }

        public TransactionStatus(
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
        

        #region comment

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
            return this.Name.GetHashCode() ^ this.Code.GetHashCode() ^ this.Date.GetHashCode();
        }

        protected bool Equals(TransactionStatus other)
        {
            return this.Name == other.Name && this.Code == other.Code && this.Date == other.Date;
        }

        #endregion comment

    }
}
