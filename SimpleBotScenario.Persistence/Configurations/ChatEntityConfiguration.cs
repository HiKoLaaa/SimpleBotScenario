namespace SimpleBotScenario.Persistence.Configurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class ChatEntityConfiguration : IEntityTypeConfiguration<Chat>
{
    public void Configure(EntityTypeBuilder<Chat> builder)
    {
        builder.ToTable("chats");

        builder.HasKey(chat => chat.Id).HasName("PK_chats_id");
        builder.Property(chat => chat.Id).HasColumnName("id");
        builder.Property(chat => chat.ExternalId).HasColumnName("external_id");

        builder
            .HasMany(chat => chat.Messages)
            .WithOne(message => message.Chat)
            .IsRequired()
            .HasForeignKey("chat_id")
            .HasConstraintName("FK_chats_messages")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(chat => chat.ExternalId).HasDatabaseName("IX_chats_external_id");
    }
}