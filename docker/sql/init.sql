IF DB_ID(N'BookStoreAbp') IS NULL
BEGIN
    CREATE DATABASE [BookStoreAbp];
END
GO

IF DB_ID(N'LibraryMemberServiceDb') IS NULL
BEGIN
    CREATE DATABASE [LibraryMemberServiceDb];
END
GO