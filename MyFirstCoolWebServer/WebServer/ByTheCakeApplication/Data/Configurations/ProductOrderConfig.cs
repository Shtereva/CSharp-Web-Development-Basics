using HTTPServer.ByTheCakeApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HTTPServer.ByTheCakeApplication.Data.Configurations
{
    public class ProductOrderConfig : IEntityTypeConfiguration<ProductOrder>
    {
        public void Configure(EntityTypeBuilder<ProductOrder> builder)
        {
            builder.HasKey(po => new
            {
                po.ProductId,
                po.OrderId
            });
        }
    }
}
