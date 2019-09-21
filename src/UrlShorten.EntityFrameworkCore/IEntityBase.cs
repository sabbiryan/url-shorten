using System;

namespace UrlShorten.EntityFrameworkCore
{
    public interface IEntityBase<TKey>
    {
        TKey Id { get; set; }


        string CreatedBy { get; set; }
        DateTime CreationTime { get; set; }

        string ModifiedBy { get; set; }
        DateTime? ModificationTime { get; set; }


        bool IsDeleted { get; set; }
        string DeletedBy { get; set; }
        DateTime? DeletionTime { get; set; }

        string IpAddress { get; set; }
        string DeviceInfo { get; set; }
    }
}