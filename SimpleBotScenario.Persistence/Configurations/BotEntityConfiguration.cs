namespace SimpleBotScenario.Persistence.Configurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class BotEntityConfiguration : IEntityTypeConfiguration<Bot>
{
    public void Configure(EntityTypeBuilder<Bot> builder)
    {
        builder.ToTable("bots");

        builder.HasKey(bot => bot.Id).HasName("PK_bots_id");
        builder.Property(bot => bot.Id).HasColumnName("id");
        builder.Property(bot => bot.Token).HasColumnName("token").HasColumnType("varchar(100)");

        builder
            .HasMany(bot => bot.Chats)
            .WithOne(chat => chat.Bot)
            .IsRequired()
            .HasForeignKey("bot_id")
            .HasConstraintName("FK_chats_messages")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(bot => bot.Token).IsUnique().HasDatabaseName("UX_bots_token");
    }
}
