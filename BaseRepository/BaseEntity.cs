using System;

namespace BaseRepositories
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public float Cost { get; set; }
        public float Sell { get; set; }
        public float RRP { get; set; }
        public byte IsLast { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid CreatedBy { get; set; }
        public DateTime LastSavedDate { get; set; }
        public Guid LastSavedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
