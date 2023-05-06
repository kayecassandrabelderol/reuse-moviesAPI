CREATE PROCEDURE [dbo].[spSong_GetSongDetails]
	@Id int
AS
BEGIN
	SELECT m.*, g.*, ac.*, aw.*
    FROM Song m
    LEFT JOIN SongGenre mg ON mg.SongId = m.Id
    LEFT JOIN Genre g ON g.Id = mg.GenreId
    LEFT JOIN SongArtist ma ON ma.SongId = m.Id
    LEFT JOIN Artist ac ON ac.Id = ma.ArtistId
    LEFT JOIN Award aw ON aw.SongId = m.Id
    WHERE m.Id = @Id
END