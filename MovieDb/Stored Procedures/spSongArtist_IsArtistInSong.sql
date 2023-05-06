CREATE PROCEDURE [dbo].[spSongArtist_IsArtistInSong]
    @songId int,
    @artistId int
AS
BEGIN
    SELECT COUNT(*)
    FROM SongArtist ma
    INNER JOIN Song m ON m.Id = ma.SongId
    INNER JOIN Artist ac ON ac.Id = ma.ArtistId
    WHERE m.Id = @songId AND ac.Id = @artistId
END