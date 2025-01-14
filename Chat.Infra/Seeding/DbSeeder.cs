using Chat.Domain.AggregateModels.ChatAggregate;
using Chat.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;

namespace Chat.Infra.Seeding;

public class DbSeeder
{
    private readonly ChatContext _context;

    public DbSeeder(ChatContext context)
    {
        _context = context;
    }

    public async Task SeedAsync()
    {
        // Only seed if the database is empty
        if (await _context.Users.AnyAsync())
        {
            return;
        }

        // Create test users
        var jobSeeker1 = new User("jobseeker1", "js1@example.com", "hashedPassword123", UserType.JobSeeker, "profile1.jpg");
        var jobSeeker2 = new User("jobseeker2", "js2@example.com", "hashedPassword123", UserType.JobSeeker, "profile2.jpg");
        var recruiter1 = new User("recruiter1", "rec1@company.com", "hashedPassword123", UserType.Recruiter, "profile3.jpg");
        var recruiter2 = new User("recruiter2", "rec2@company.com", "hashedPassword123", UserType.Recruiter, "profile4.jpg");

        await _context.Users.AddRangeAsync(jobSeeker1, jobSeeker2, recruiter1, recruiter2);
        await _context.SaveChangesAsync();

        // Create test chats
        var chat1 = new Domain.AggregateModels.ChatAggregate.Chat("Job Discussion 1");
        var chat2 = new Domain.AggregateModels.ChatAggregate.Chat("Job Discussion 2");

        await _context.Chats.AddRangeAsync(chat1, chat2);
        await _context.SaveChangesAsync();

        // Create test messages
        var messages = new List<Message>
        {
            new Message(chat1.Id, jobSeeker1.UserId, "Hi, I'm interested in the position"),
            new Message(chat1.Id, recruiter1.UserId, "Great! Let's discuss your experience"),
            new Message(chat1.Id, jobSeeker1.UserId, "I have 3 years of experience in software development"),
            new Message(chat2.Id, jobSeeker2.UserId, "Hello, I saw your job posting"),
            new Message(chat2.Id, recruiter2.UserId, "Thanks for reaching out! What's your background?"),
            new Message(chat2.Id, jobSeeker2.UserId, "I'm a full-stack developer with 5 years of experience")
        };

        await _context.Messages.AddRangeAsync(messages);
        await _context.SaveChangesAsync();
    }
}