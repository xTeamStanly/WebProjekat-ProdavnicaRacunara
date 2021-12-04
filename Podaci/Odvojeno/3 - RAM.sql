SET IDENTITY_INSERT dbo.RAMs ON

INSERT INTO dbo.RAMs
([ID],[Manufacturer],[Model],[Price],[FrequencyMHz],[MemoryGB],[SerialNumber])
VALUES
(1,'Kingston','HyperX Fury',7700,3200,8,'K-X-F8'),
(2,'Kingston','HyperX XMP Predator',8000,3200,8,'K-X-XP8'),
(3,'Kingston','HyperX Fury RGB',7300,3200,8,'K-X-FRGB8'),
(4,'Kingston','HyperX Fury Black',6400,3200,8,'K-X-FB8'),
(5,'Kingston','HyperX Fury XMP',15000,3200,16,'K-X-FX16'),
(6,'Kingston','HyperX XMP Predator BLACK',16000,3200,16,'K-X-XPB16'),
(7,'Kingston','HyperX Fury RGB',14000,3200,16,'K-X-FRBG16'),
(8,'Kingston','HyperX Fury Black',13000,3200,16,'K-X-FB16'),
(9,'Patriot','Viper Steel RGB',24000,3200,32,'P-V-SRGB32'),
(10,'Patriot','Viper Steel',24000,3600,32,'P-V-S32'),
(11,'Patriot','Viper 4',21000,3200,32,'P-V4-32'),
(12,'Patriot','Viper Steel',20000,3000,32,'P-V-S3K32'),
(13,'Corsair','Vengeance RGB',16000,3600,16,'C-V-RGB16'),
(14,'Corsair','Vengeance LPX',14000,3600,16,'C-V-LPX16'),
(15,'Corsair','Vengeance LPX RED',13000,3200,16,'C-V-LPXR16')

SET IDENTITY_INSERT dbo.RAMs OFF

GO