using System.ComponentModel.DataAnnotations;

namespace dropArquivo.Models
{
    public class File
    {

        [Key]
        public long Id { get; set; }
        public byte [] FileByte { get; set; }
        public string FileName { get; set;}
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}