namespace Yarito.Domain.Core.DTOs.Images
{
    public class ImageStreamDto
    {
        public required Stream ImageStream { get; set; }
        public required string FileFormat { get; set; }
        public required string FileName { get; set; }
    }
}
