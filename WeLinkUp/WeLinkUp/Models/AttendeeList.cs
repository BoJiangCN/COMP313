using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WeLinkUp.Models
{
    [Keyless]
    public class AttendeeList
    {
        int EventId { get; set; }
        string UserId { get; set; }
        string Status { get; set; }
    }
}
