namespace SimpleBotScenario.Persistence.Configurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

internal class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.ToTable("messages");

        builder.HasKey(message => message.Id).HasName("PK_messages_id");

        builder.Property(message => message.Id).HasColumnName("id");
        builder.Property(message => message.ExternalId).HasColumnName("external_id");
        builder.Property(message => message.Text).HasColumnName("text").HasColumnType("text");
        builder.Property(message => message.DateTimeUtc).HasColumnName("date_time_utc");

        builder.HasIndex(message => message.ExternalId).HasDatabaseName("IX_messages_external_id");
    }
}