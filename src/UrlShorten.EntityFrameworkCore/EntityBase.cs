using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace UrlShorten.EntityFrameworkCore
{
    public class EntityBase<TKey>: IEntityBase<TKey>
    {
        [Key]
        public TKey Id { get; set; }


        public string CreatedBy { get; set; }
        public DateTime CreationTime { get; set; } = DateTime.UtcNow;

        public string ModifiedBy { get; set; }
        public DateTime? ModificationTime { get; set; }


        public bool IsDeleted { get; set; }
        public string DeletedBy { get; set; }
        public DateTime? DeletionTime { get; set; }

        public string IpAddress { get; set; }
        public string DeviceInfo { get; set; }
    }
}