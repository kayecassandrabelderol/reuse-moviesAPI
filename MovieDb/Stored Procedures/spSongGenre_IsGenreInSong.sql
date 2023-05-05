CREATE PROCEDURE [dbo].[spSongGenre_IsGenreInSong]
	@songId int,
	@genreId int
AS
BEGIN
	SELECT COUNT(*)
    FROM SongGenre mg
    INNER JOIN Song m ON m.Id = mg.SongId
    INNER JOIN Genre g ON g.Id = mg.GenreId
    WHERE m.Id = @songId AND g.Id = @genreId
END