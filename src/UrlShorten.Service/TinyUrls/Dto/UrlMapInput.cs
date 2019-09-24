using System;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace UrlShorten.Service.TinyUrls.Dto
{
    public class UrlMapInput
    {

        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        

        [Required(ErrorMessage = "Please enter a url")]
        [RegularExpression("^(http:\\/\\/www\\.|https:\\/\\/www\\.|http:\\/\\/|https:\\/\\/)?[a-z0-9]+([\\-\\.]{1}[a-z0-9]+)*\\.[a-z]{2,5}(:[0-9]{1,5})?(\\/.*)?$", ErrorMessage = "Please enter a valid url")]
        public string RawUrl { get; set; }

    }
}
