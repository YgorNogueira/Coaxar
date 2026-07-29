using SQLite;

namespace CoaxarApp.Services;

public static class DatabaseConstants
{
    //Nome do arquivo do banco
    public const string DatabaseFilename = "coaxar.db3";

    //Flags de abertura do banco SQLite
    public const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    //Caminho do banco no diretório de dados do app
    public static string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}
