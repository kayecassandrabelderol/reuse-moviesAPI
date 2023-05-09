CREATE PROCEDURE [dbo].[spGenre_GetAllBySongId]
    @songId int
AS
BEGIN
    SELECT g.*
    FROM Genre g
    INNER JOIN SongGenre sg ON sg.GenreId = g.Id
    INNER JOIN Song s ON s.Id = sg.SongId
    WHERE s.Id = @songId
END