using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using FluentValidation;
using DocumentRegistration.DAL;
using System.Linq;
using System.Web.Mvc;

namespace DocumentRegistration.Models
{
    [Table("Document")]
    public partial class DocumentModel
    {
        [Key]
        public long Id { get; set; }

        [Index(IsUnique = true)]
        [Remote(action: "VerifyCode", controller: "Document", AdditionalFields = "Id", ErrorMessage = "Este codigo ja existe")]
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

        [NotMapped]
        public HttpPostedFileBase File { get; set; }
    }
}
