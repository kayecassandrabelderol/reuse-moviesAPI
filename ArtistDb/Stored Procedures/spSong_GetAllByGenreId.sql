CREATE PROCEDURE [dbo].[spSong_GetAllByGenreId]
	@genreId int
AS
BEGIN
	SELECT m.*
    FROM Song m
    INNER JOIN SongGenre mg ON mg.SongId = m.Id
    INNER JOIN Genre g ON g.Id = mg.GenreId
    WHERE g.Id = @genreId
END
