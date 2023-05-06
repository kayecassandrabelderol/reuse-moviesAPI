CREATE PROCEDURE [dbo].[spAward_GetAllBySongId]
    @songId int
AS
BEGIN
    SELECT aw.*
    FROM Award aw
    INNER JOIN Song s ON s.Id = aw.SongId
    WHERE s.Id = @songId
END