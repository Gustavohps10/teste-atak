using System;

namespace teste_atak.Domain.Entities
{
    public class User
    {
        public string Id { get; private set; }
        public string Email { get; private set; }
        public string PasswordHash { get; private set; }
        public string? VerificationToken { get; private set; }
        public DateTime? TokenExpiration { get; private set; }
        public bool IsEmailVerified { get; private set; }
        public string Name { get; private set; }
        public string? AvatarUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public User(string email, string passwordHash, string name, string? avatarUrl = null)
        {
            Id = Guid.NewGuid().ToString(); 
            Email = email;
            PasswordHash = passwordHash;
            Name = name;
            AvatarUrl = avatarUrl;
            IsEmailVerified = false;
            CreatedAt = DateTime.UtcNow;
        }

        public User(string id, string email, string passwordHash, string name, bool isEmailVerified, DateTime createdAt, string? verificationToken, DateTime? tokenExpiration,  string? avatarUrl = null)
        {
            Id = id;
            Email = email;
            PasswordHash = passwordHash;
            VerificationToken = verificationToken;
            TokenExpiration = tokenExpiration;
            IsEmailVerified = isEmailVerified;
            Name = name;
            AvatarUrl = avatarUrl;
            CreatedAt = createdAt;
        }
    }
}
