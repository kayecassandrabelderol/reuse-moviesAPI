
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
('R&B'),
('Electronic'),
('Korean Pop'),
('EDM'),
('Dance Pop')
GO

--insert to Song
insert into Song (Title, Duration, ReleaseDate) values ('Good Day', 3.53, '2010-12-09');
insert into Song (Title, Duration, ReleaseDate) values ('Gashina', 3.00, '2017-08-22');
insert into Song (Title, Duration, ReleaseDate) values ('Criminal', 3.31, '2020-09-07');
insert into Song (Title, Duration, ReleaseDate) values ('Gangnam Style', 3.39, '2012-07-15');
insert into Song (Title, Duration, ReleaseDate) values ('Pretty Please', 5.26, '2020-09-04');
GO

--insert to Award
insert into Award (Name, Year, SongId) values ('MAMA - Song of the Year', 2015, 1);
insert into Award (Name, Year, SongId) values ('MAMA - Song of the Year', 2016, 2);
insert into Award (Name, Year, SongId) values ('MAMA - Song of the Year', 2017, 3);
insert into Award (Name, Year, SongId) values ('MAMA - Song of the Year', 2018, 4);
insert into Award (Name, Year, SongId) values ('MAMA - Song of the Year', 2019, 5);
insert into Award (Name, Year, SongId) values ('MAMA - Song of the Year', 2020, 6);
insert into Award (Name, Year, SongId) values ('MAMA - Song of the Year', 2021, 7);
insert into Award (Name, Year, SongId) values ('MAMA - Song of the Year', 2022, 8);
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