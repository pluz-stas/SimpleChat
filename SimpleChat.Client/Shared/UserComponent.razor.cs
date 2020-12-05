using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace SimpleChat.Client.Shared
{
    public partial class UserComponent
    {
        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> InputAttributes { get; set; }
    }
}
