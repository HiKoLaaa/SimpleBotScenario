namespace SimpleBotScenario.Persistence.Configurations;

using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class ChatParticipantConfiguration : IEntityTypeConfiguration<ChatParticipant>
{
    public void Configure(EntityTypeBuilder<ChatParticipant> builder)
    {
        builder.ToTable("chat_participants");

        builder.HasKey(chatParticipant => chatParticipant.Id).HasName("PK_chat_participants_id");
        builder
            .HasMany(chatParticipant => chatParticipant.Messages)
            .WithOne(message => message.ChatParticipant)
            .HasForeignKey("chat_participant_id")
            .HasConstraintName("FK_messages_chat_participants")
            .OnDelete(DeleteBehavior.NoAction);
    }
}