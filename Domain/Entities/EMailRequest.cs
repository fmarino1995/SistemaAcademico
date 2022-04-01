using Microsoft.AspNetCore.Http;

namespace Domain.Entities
{
    public class EMailRequest
    {
        public string ToEmail { get; set; }
        public string UserName { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public List<IFormFile> Attachments { get; set; }
    }
}
