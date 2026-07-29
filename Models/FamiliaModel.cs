using SQLite;

namespace CoaxarApp.Models;

[Table("Familias")]
public class FamiliaModel
{
    //Identificador da família no banco
    [PrimaryKey]
    public int Id { get; set; }

    //Nome da família
    [Indexed, NotNull]
    public string Nome { get; set; } = string.Empty;

    //Caminho da imagem de capa da família
    public string ImagemPath { get; set; } = string.Empty;
}
