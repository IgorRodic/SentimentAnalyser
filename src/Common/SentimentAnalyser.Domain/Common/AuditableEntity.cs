using System;
using System.Collections.Generic;
using System.Text;

namespace SentimentAnalyser.Domain.Common
{
    public abstract class AuditableEntity
    {
        public string Creator { get; set; }

        public DateTime CreateDate { get; set; }

        public string Modifier { get; set; }

        public DateTime? ModifyDate { get; set; }

    }
}
