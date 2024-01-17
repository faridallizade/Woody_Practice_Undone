using Woody.Models.Common;

namespace Woody.Models
{
    public class Service:BaseEntity
    {
        public string ImgUrl { get; set; }  
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
