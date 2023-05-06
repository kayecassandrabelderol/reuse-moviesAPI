
--insert to Artist
insert into Artist (Name, Gender, Birthday) values ('IU', 'Female', '1981-08-12');
insert into Artist (Name, Gender, Birthday) values ('Sunmi', 'Female', '1989-12-25');
insert into Artist (Name, Gender, Birthday) values ('Taemin', 'Male', '1985-08-26');
insert into Artist (Name, Gender, Birthday) values ('PSY', 'Male', '1986-01-31');
insert into Artist (Name, Gender, Birthday) values ('Jackson Wang', 'Male', '1982-11-01');
GO

--insert to Genre
insert into Genre (Name)
values
('Genre Sample1'),
('Genre Sample2'),
('Genre Sample3'),
('Genre Sample4'),
('Genre Sample5')
GO

--insert to Song
insert into Song (Title, Duration, ReleaseDate) values ('sample song 1', 255, '2016-11-20');
insert into Song (Title, Duration, ReleaseDate) values ('sample song 2', 255, '2023-11-20');
insert into Song (Title, Duration, ReleaseDate) values ('sample song 3', 255, '2023-11-20');
insert into Song (Title, Duration, ReleaseDate) values ('sample song 4', 255, '2023-11-20');
insert into Song (Title, Duration, ReleaseDate) values ('sample song 5', 255, '2023-11-20');
GO

--insert to Award
insert into Award (Name, Year, SongId) values ('sample award 1', 2021, 1);
insert into Award (Name, Year, SongId) values ('sample award 2', 2022, 2);
insert into Award (Name, Year, SongId) values ('sample award 3', 2023, 3);
insert into Award (Name, Year, SongId) values ('sample award 4', 2024, 4);
insert into Award (Name, Year, SongId) values ('sample award 5', 2025, 5);
GO

--SongArtists
insert into SongArtist(SongId, ArtistId)
values
(1,1),
(2,2),
(3,3),
(4,4),
(5,5)
GO

--SongGenres
insert into SongGenre (SongId, GenreId)
values
(1,1),
(2,2),
(3,3),
(4,4),
(5,5)
GO