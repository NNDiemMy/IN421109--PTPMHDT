using System;

namespace EduHub.Domain.Entities
{
    public class Member
    {
        public Guid Id { get; }
        public string Email { get; }

        private Member(Guid id, string email)
        {
            Id = id;
            Email = email;
        }

        public static Member Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email không được để trống", nameof(email));
            if (!email.Contains("@"))
                throw new ArgumentException("Email phải chứa ký tự @", nameof(email));
            
            return new Member(Guid.NewGuid(), email);
        }
    }
}
