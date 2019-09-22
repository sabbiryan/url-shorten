using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace UrlShorten.EntityFrameworkCore
{
    public class UrlMap: EntityBase<string>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        //[NotMapped]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Identity { get; set; }

        public string ShortenUrl { get; set; }

        public string RawUrl { get; set; }


        public int HitCount { get; set; }

        public virtual ICollection<HitLog> HitLogs { get; set; }
    }
}
