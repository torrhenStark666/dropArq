namespace UploadFile.WebApi.Models
{
    public class Document
    {
        public long Id { get; set; }
        public byte [] File { get; set; }
        public string FileName { get; set;}
        public long UserId { get; set; }
        public bool? Removed { get; set; } = false;
        // classe "virtual" quando utilizado include na query retorna o objeto usuario
        // relacionado
        public virtual User User { get; set; }
    }
}