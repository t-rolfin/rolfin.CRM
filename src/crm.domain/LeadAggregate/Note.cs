﻿using System;

namespace crm.domain.LeadAggregate
{
    public class Note : Entity<Guid>
    {
        public Note() : base() { }

        public Note(string content)
        {
            Content = content;
        }

        public Guid LeadId { get; init; }
        public string Content { get; set; }
    }
}