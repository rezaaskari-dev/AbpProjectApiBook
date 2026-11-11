using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryMessaging.Contracts.Members
{
    public class CheckMemberExistsResponse
    {
        public string CorrelationId { get; set; } = string.Empty;

        public Guid MemberId { get; set; }

        public bool Exists { get; set; }
    }
}
