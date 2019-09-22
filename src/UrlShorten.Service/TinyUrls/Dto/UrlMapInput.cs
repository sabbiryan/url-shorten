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
        [RegularExpression("^http(s)?://((www.)+)?([\\w-]+.)+[\\w-]+(/(#!/)?[\\w- ./?%&#!=])?$", ErrorMessage = "Please enter a valid url")]
        public string RawUrl { get; set; }

    }
}
