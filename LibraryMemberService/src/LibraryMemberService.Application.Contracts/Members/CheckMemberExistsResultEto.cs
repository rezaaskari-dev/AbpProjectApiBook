using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace LibraryMemberService.Members
{
    [EventName("LibraryMemberService.CheckMemberExistsResult")]
    public class CheckMemberExistsResultEto
    {
        public string CorrelationId { get; set; } = string.Empty;

        public Guid MemberId { get; set; }

        public bool Exists { get; set; }
    }
}
