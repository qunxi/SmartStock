using System.Data.Entity.ModelConfiguration;
using Domain.Model;
using Domain.Model.Stocks;

namespace Domain.EntityFramework.Configurations
{
    public class StockConfiguration : EntityTypeConfiguration<Stock>
    {
    }
}
