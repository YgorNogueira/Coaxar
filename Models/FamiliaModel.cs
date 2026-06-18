using SQLite;

namespace CoaxarApp.Models;

[Table("Familias")]
public class FamiliaModel
{
    [PrimaryKey]
    public int Id { get; set; }

    [Indexed, NotNull]
    public string Nome { get; set; } = string.Empty;

    public string ImagemPath { get; set; } = string.Empty;
}
