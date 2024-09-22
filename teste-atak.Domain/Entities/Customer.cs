﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace teste_atak.Domain.Entities
{
    public class Customer
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string? ImageUrl { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Customer(string name, string email, string phone, string? imageUrl = null)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Email = email;
            Phone = phone;
            ImageUrl = imageUrl;
            CreatedAt = DateTime.UtcNow;
        }

        public Customer(string id, string name, string email, string phone, string? imageUrl, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            ImageUrl = imageUrl;
            CreatedAt = createdAt;
        }
    }
}