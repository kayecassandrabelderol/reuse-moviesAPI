CREATE PROCEDURE [dbo].[spArtist_GetAllBySongId]
    @songId int
AS
BEGIN
    SELECT ar.*
    FROM Artist ar
    INNER JOIN SongArtist sa ON sa.ArtistId = ar.Id
    INNER JOIN Song s ON s.Id = sa.SongId
    WHERE s.Id = @songId
END