SET IDENTITY_INSERT dbo.Stores ON

INSERT INTO dbo.Stores
([ID], [Name], [Address])
VALUES
(1, 'NanoGen Computing', 'Vizantijski Bulevar 1'),
(2, 'Microtech Emporium', 'Vardarska 20'),
(3, 'Mr. Computer', 'Starca Vujadina 3'),
(4, 'Technology Solutions', 'Aleksandra Medvedeva BB'),
(5, 'Desktop Innovations', 'Bulevar Šabana Bajramovića 22')

SET IDENTITY_INSERT dbo.Stores OFF

GO