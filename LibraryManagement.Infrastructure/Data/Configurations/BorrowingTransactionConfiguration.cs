using LibraryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibraryManagement.Infrastructure.Data.Configurations;

public class BorrowingTransactionConfiguration : IEntityTypeConfiguration<BorrowingTransaction>
{
    public void Configure(EntityTypeBuilder<BorrowingTransaction> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Status)
            .HasConversion<string>();

        builder.HasOne(t => t.Book)
            .WithMany(b => b.BorrowingTransactions)
            .HasForeignKey(t => t.BookId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Member)
            .WithMany(m => m.BorrowingTransactions)
            .HasForeignKey(t => t.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.SystemUser)
            .WithMany()
            .HasForeignKey(t => t.SystemUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}