using Email_data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Email_data.Configs
{
    public class EmailInteractionConfig : IEntityTypeConfiguration<EmailInteraction>
    {
        public void Configure(EntityTypeBuilder<EmailInteraction> builder)
        {
            //builder.Property(e => e.Id).IsRequired();
            //builder.Property(e => e.Status);
            //builder.HasKey(e => e.Id);
            //builder.Property(e => e.Status);    
            //builder.Property(e => e.Direction);
            //builder.Property(e => e.ThreadId);            
        }
    }

}
