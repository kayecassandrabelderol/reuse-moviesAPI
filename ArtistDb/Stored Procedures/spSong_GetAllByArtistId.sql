CREATE PROCEDURE [dbo].[spSong_GetAllByArtistId]
	@artistId int
AS
BEGIN
	SELECT s.*
    FROM Song s
    INNER JOIN SongArtist sa ON sa.SongId = s.Id
    INNER JOIN Artist a ON a.Id = sa.ArtistId
    WHERE a.Id = @artistId
END