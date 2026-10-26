using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace LibraryMemberService.Members
{
    [EventName("LibraryMemberService.CheckMemberExists")]
    public class CheckMemberExistsEto
    {
        public Guid MemberId { get; set; }

        public string CorrelationId { get; set; } = string.Empty;
    }
}