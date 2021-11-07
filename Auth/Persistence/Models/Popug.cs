using System;
using Microsoft.AspNetCore.Identity;

namespace Auth.Persistence.Models
{
    public class Popug : IdentityUser<ulong>
    {
        public Guid PublicId { get; set; }
    }
}