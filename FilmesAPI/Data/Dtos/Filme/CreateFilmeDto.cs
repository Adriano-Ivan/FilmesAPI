using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos
{
    public class CreateFilmeDto
    {
        [Required(ErrorMessage = "Título é obrigatório.")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "Campo diretor é obrigatório.")]
        public string Diretor { get; set; }

        [StringLength(30, ErrorMessage = "O gênero deve ter no máximo 30 caracteres.")]
        public string Genero { get; set; }

        [Range(1, 600, ErrorMessage = "A duração deve estar entre 1 e 600.")]
        public int Duracao { get; set; }

        public int ClassificacaoEtaria { get; set; }
    }
}
