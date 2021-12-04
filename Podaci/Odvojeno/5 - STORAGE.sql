SET IDENTITY_INSERT dbo.Storages ON

INSERT INTO dbo.Storages
([ID],[Manufacturer],[Model],[Price],[MemoryGB],[SerialNumber])
VALUES
(1,'Western Digital','Gold',24000,4096,'WD-G4'),
(2,'Western Digital','Black',25000,4096,'WD-BK4'),
(3,'Western Digital','Red',24000,4096,'WD-R4'),
(4,'Western Digital','Purple',19000,4096,'WD-P4'),
(5,'Western Digital','Blue',15000,4096,'WD-B4'),
(6,'Western Digital','Gold',16000,2048,'WD-G2'),
(7,'Western Digital','Black',18000,2048,'WD-BK2'),
(8,'Western Digital','Blue',25000,2048,'WD-B2'),
(9,'Western Digital','Purple',11000,2048,'WD-P2'),
(10,'Western Digital','Red',15000,2048,'WD-R2'),
(11,'Western Digital','Red',34000,1024,'WD-R1'),
(12,'Western Digital','Gold',10000,1024,'WD-G1'),
(13,'Western Digital','Black',12000,1024,'WD-BK1'),
(14,'Western Digital','Purple',8000,1024,'WD-P1'),
(15,'Western Digital','Blue',6000,1024,'WD-B1'),
(16,'Samsung','970 EVO PLUS',10000,250,'S-97EP-Q'),
(17,'Samsung','970 EVO PLUS',14000,500,'S-97EP-H'),
(18,'Samsung','860 PRO',30000,1024,'S-86P-F'),
(19,'Samsung','970 EVO PLUS',20000,1024,'S-97EP-F'),
(20,'Samsung','970 EVO PLUS',46000,2048,'S-97EP-D'),
(21,'Intel','Optane',10000,32,'I-O32'),
(22,'Intel','660P',20000,1024,'I-66P'),
(23,'Intel','665P',15000,1024,'I-665P'),
(24,'Toshiba','SATA III',6000,1024,'T-S-3F'),
(25,'Toshiba','SATA3',5600,1024,'T-S3-F'),
(26,'Toshiba','SATA III',14000,2048,'T-S-3D'),
(27,'Toshiba','SATA3',9000,2048,'T-S3-D'),
(28,'Seagate','IronWolf',8000,1024,'SG-IW'),
(29,'Seagate','SATA3',19000,2048,'SG-S3'),
(30,'Seagate','IronWolf Pro',19000,4096,'SG-IWP')

SET IDENTITY_INSERT dbo.Storages OFF

GO
