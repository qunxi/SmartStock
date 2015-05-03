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

        public Decimal Pe { get; set; } //PE

        public DateTime PublishDate { get; set; } //上市时间

        public Decimal TotalCapital { get; set; } //总股本

        public Decimal TotalShare { get; set; } //总股份

        public Decimal FloatingCapital { get; set; } //流通金额

        public Decimal FloatingShare { get; set; } //流通股份

        public Decimal Revenue { get; set; } //收入

        public Decimal Profit { get; set; } //利润

        public Decimal RevenueRatio { get; set; } //收入同比增长

        public Decimal ProfitRatio { get; set; } //利润同比增长


        public IEnumerable<TransactionStatus> DailyTransactionStatus { get; set; }  // 交易状态

     
        #region comment
        
        public override int GetHashCode()
        {
            return this.Code.GetHashCode() ^ this.Name.GetHashCode();
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
        #endregion comment
    }
}
