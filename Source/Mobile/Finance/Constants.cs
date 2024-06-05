﻿namespace Finance;

internal static class Constants {
    public const string DatabaseFilename = "Finance.db3";
    public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | SQLite.SQLiteOpenFlags.Create | SQLite.SQLiteOpenFlags.SharedCache;
    public static string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
}