﻿CREATE TABLE [dbo].[SongArtist]
(
    [SongId] INT NOT NULL, 
    [ArtistId] INT NOT NULL, 
    CONSTRAINT [FK_SongArtist_Song] FOREIGN KEY ([SongId]) REFERENCES [Song]([Id]) ON DELETE CASCADE ON UPDATE CASCADE, 
    CONSTRAINT [FK_SongArtist_Artist] FOREIGN KEY ([ArtistId]) REFERENCES [Artist]([Id]) ON DELETE CASCADE ON UPDATE CASCADE, 
    CONSTRAINT [PK_SongArtist] PRIMARY KEY CLUSTERED ([SongId], [ArtistId])   
)
