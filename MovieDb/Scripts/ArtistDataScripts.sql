
--insert to Artist
insert into Artist (Name, Gender, Birthday) values ('Merwin Rizziello', 'Male', '1981-08-12');
insert into Artist (Name, Gender, Birthday) values ('Charles Sopp', 'Male', '1989-12-25');
insert into Artist (Name, Gender, Birthday) values ('Sarina Williment', 'Female', '1985-08-26');
insert into Artist (Name, Gender, Birthday) values ('Clarabelle Molyneaux', 'Female', '1986-01-31');
insert into Artist (Name, Gender, Birthday) values ('Cesar Matitiaho', 'Male', '1996-11-11');
insert into Artist (Name, Gender, Birthday) values ('Mellie O'' Hern', 'Female', '1982-11-01');
insert into Artist (Name, Gender, Birthday) values ('Gregoor MacParland', 'Male', '1994-09-03');
insert into Artist (Name, Gender, Birthday) values ('Ulrich Stronough', 'Male', '1996-12-23');
insert into Artist (Name, Gender, Birthday) values ('Bartram Stannus', 'Male', '1996-06-30');
insert into Artist (Name, Gender, Birthday) values ('Mellisa Willerstone', 'Female', '1999-07-13');
GO

--insert to Genre
insert into Genre (Name)
values
('Action'),
('Comedy'),
('Drama')
GO

--insert to Song
insert into Song (Title, Duration, ReleaseDate) values ('SAMPLE2', 255, '2016-11-20');
insert into Song (Title, Duration, ReleaseDate) values ('SAMPLE1', 255, '2023-11-20');
GO

/* --insert mocks to Award
insert into Award (Name, Year, SongId) values ('cras pellentesque volutpat dui', 2020, 10);
insert into Award (Name, Year, SongId) values ('in hac habitasse platea', 2015, 10);
insert into Award (Name, Year, SongId) values ('habitasse platea dictumst maecenas ut', 2010, 3);
insert into Award (Name, Year, SongId) values ('diam erat fermentum', 2004, 10);
insert into Award (Name, Year, SongId) values ('quis libero nullam sit amet', 2002, 7);
GO */

/*--SongArtists
insert into SongArtist(SongId, ArtistId)
values
(1,1), (1,2),
(2,2), (2,3),
(3,3), (3,4),
(4,4), (4,5),
(5,5), (5,6),
(6,6), (6,7),
(7,7), (7,8),
(8,8), (8,9),
(9,9)
GO

--SongGenres
insert into SongGenre (SongId, GenreId)
values
(1,1),
(2,1), (2,2),
(3,2),
(4,2), (4,3),
(5,1),
(6,1), (6,2),
(7,2),
(8,2), (8,3),
(9,3)*/