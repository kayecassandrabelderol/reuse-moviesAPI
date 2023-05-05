CREATE PROCEDURE [dbo].[spSong_GetAllByArtistId]
	@actorId int
AS
BEGIN
	SELECT m.*
    FROM Song m
    INNER JOIN SongArtist ma ON ma.SongId = m.Id
    INNER JOIN Artist a ON a.Id = ma.ArtistId
    WHERE a.Id = @actorId
END