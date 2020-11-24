using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.Models
{
    public class Image
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Url { get; set; }
    }
}
