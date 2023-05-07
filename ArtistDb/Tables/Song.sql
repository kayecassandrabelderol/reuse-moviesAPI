CREATE TABLE [dbo].[Song]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Title] NVARCHAR(50) NOT NULL, 
    [Duration] INT NOT NULL, 
    [ReleaseDate] DATETIME NOT NULL
)
