CREATE DATABASE World_Countries;

use World_Countries;

CREATE TABLE Countries(
   [Name] VARCHAR(45) not null,
   [Capital]  VARCHAR(45) not null,
   [Population] BIGINT null,
   [Area] FLOAT not null,
   [Language] VARCHAR(45) null,
   [GDP_USD] DECIMAL null,
)

INSERT INTO Countries( [Name],[Capital],[Population],[Area],[Language],[GDP_USD])
VALUES 
('Russia', 'Moscow', 146000000, 17098246.5, 'Russian', 1699870000000),
('Germany', 'Berlin', 83000000, 357386.75, 'German', 3845630000000),
('Brazil', 'Brasília', 211000000, 8515767.25, 'Portuguese', 1839750000000),
('Japan', 'Tokyo', 126000000, 377975.4, 'Japanese', 5064870000000),
('Canada', 'Ottawa', 37590000, 9984670.1, 'English, French', 1736420000000),
('Australia', 'Canberra', 25000000, 7692024.9, 'English', 1392680000000),
('India', 'New Delhi', 1380004385, 3287263.55, 'Hindi, English', 2875140000000),
('China', 'Beijing', 1439323776, 9596961.8, 'Mandarin', 14722731000000),
('France', 'Paris', 67000000, 643801.33, 'French', 2715510000000),
('United Kingdom', 'London', 67000000, 243610.44, 'English', 2826440000000),
('Italy', 'Rome', 60000000, 301340.21, 'Italian', 2001170000000),
('South Africa', 'Pretoria', 58000000, 1221037.66, '11 official languages', 351432000000),
('Mexico', 'Mexico City', 126000000, 1964375.82, 'Spanish', 1220710000000),
('Argentina', 'Buenos Aires', 45000000, 2780400.7, 'Spanish', 449663000000),
('South Korea', 'Seoul', 51700000, 100210.55, 'Korean', 1647120000000),
('Saudi Arabia', 'Riyadh', 34000000, 2149690.88, 'Arabic', 793969000000),
('Egypt', 'Cairo', 102000000, 1002450.91, 'Arabic', 303176000000),
('Turkey', 'Ankara', 82000000, 783562.3, 'Turkish', 761425000000),
('Indonesia', 'Jakarta', 273000000, 1904569.44, 'Indonesian', 1119190000000),
('Thailand', 'Bangkok', 70000000, 513120.68, 'Thai', 505280000000),
('Spain', 'Madrid', 47000000, 505990.12, 'Spanish', 1394230000000),
('Netherlands', 'Amsterdam', 17000000, 41543.76, 'Dutch', 902355000000),
('Sweden', 'Stockholm', 10300000, 450295.21, 'Swedish', 551520000000),
('Norway', 'Oslo', 5400000, 385207.54, 'Norwegian', 434760000000),
('Poland', 'Warsaw', 38000000, 312696.8, 'Polish', 595861000000),
('Belgium', 'Brussels', 11500000, 30528.22, 'Dutch, French, German', 529615000000),
('Chile', 'Santiago', 19000000, 756102.37, 'Spanish', 282318000000),
('Colombia', 'Bogotá', 50000000, 1141748.6, 'Spanish', 323802000000),
('Nigeria', 'Abuja', 206000000, 923768.44, 'English', 448121000000),
('Vietnam', 'Hanoi', 97000000, 331212.55, 'Vietnamese', 262580000000)

select * from Countries