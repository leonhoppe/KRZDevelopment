using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterKlon.Logic.Sessions.DTOs
{
    public class RefreshToken
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
