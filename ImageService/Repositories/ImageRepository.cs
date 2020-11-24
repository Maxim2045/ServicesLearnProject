using System;

namespace ImageService.Repositories
{
    public class ImageRepository
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Url { get; set; }

        public string FullPathOnDisk { get; set; }

        public DateTime CreatedDate { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime LastSavedDate { get; set; }

        public Guid LastSavedBy { get; set; }

        public bool IsDeleted { get; set; }
    }
}