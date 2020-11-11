using System;

namespace WebApiService.Models
{
    public class Price
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public double RRP { get; set; }
        public double Sale { get; set; }
        public double Cost { get; set; }
    }
}