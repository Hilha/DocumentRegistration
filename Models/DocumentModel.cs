using System.ComponentModel.DataAnnotations;
using System.Web;



namespace DocumentRegistration.Models
{
    public class DocumentModel
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Campo vazio")]
        [Display(Name = "Código")]
        public int Code { get; set; }

        [Required(ErrorMessage = "Campo vazio")]
        [Display(Name = "Título")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Campo vazio")]
        [Display(Name = "Categoria")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Campo vazio")]
        [Display(Name = "Processo")]
        public string Process { get; set; }

        [Display(Name = "Arquivo")]
        public string FilePath { get; set; }

        public HttpPostedFileBase File { get; set; }
    }
}
