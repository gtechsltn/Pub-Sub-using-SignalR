using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SinglRPubSub.Models
{
    public class Message
    {
        public string User { get; set; } = string.Empty;
        public string Content { get; set; }
    }

    public class Data
    {
        public string User { get; set; } = string.Empty;
        public object Content { get; set; }
    }

}
