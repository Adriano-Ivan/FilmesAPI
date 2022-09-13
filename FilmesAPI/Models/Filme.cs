using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage ="Título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage ="Campo diretor é obrigatório.")]
        public string Diretor { get; set; }

        [StringLength(30, ErrorMessage ="O gênero deve ter no máximo 30 caracteres.")]
        public string Genero { get; set; }

        [Range(1,600,ErrorMessage ="A duração deve estar entre 1 e 600.")]
        public int Duracao { get; set; }

        public int ClassificacaoEtaria { get; set; }

        [JsonIgnore]
        public virtual List<Sessao> Sessoes { get; set; }
    }
}
