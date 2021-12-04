SET IDENTITY_INSERT dbo.Processors ON

INSERT INTO dbo.Processors
([ID],[Manufacturer],[Model],[Price],[FrequencyGHz],[Cores],[SerialNumber])
VALUES
(1,'AMD','Ryzen 3 2200G',12000,3.5,4,'A-R3-22G'),
(2,'AMD','Ryzen 3 3200G',12000,3.6,4,'A-R3-32G'),
(3,'AMD','Ryzen 3 3100',15000,3.6,4,'A-R3-31'),
(4,'AMD','Ryzen 3 PRO 4350G',20000,3.8,4,'A-R3P-435G'),
(5,'AMD','Ryzen 5 1600',15500,3.6,6,'A-R5-16'),
(6,'AMD','Ryzen 5 3500X',19000,3.6,6,'A-R5-35X'),
(7,'AMD','Ryzen 5 2600',20000,3.4,6,'A-R5-26'),
(8,'AMD','Ryzez 4 PRO 4650G',23000,3.7,6,'A-R4P-465G'),
(9,'AMD','Ryzen 5 2600X',20000,3.6,6,'A-R5-26X'),
(10,'AMD','Ryzen 5 3600',24000,3.6,6,'A-R5-36'),
(11,'AMD','Ryzen 5 3600X',26000,3.8,6,'A-R5-36X'),
(12,'AMD','Ryzen 5 3600XT',34000,3.8,6,'A-R5-36XT'),
(13,'AMD','Ryzen 5 5600X',38000,3.7,6,'A-R5-56X'),
(14,'AMD','Ryzen 7 2700',25000,3.2,8,'A-R7-27'),
(15,'AMD','Ryzen 7 1700',30000,3.0,8,'A-R7-17'),
(16,'AMD','Ryzen 7 2700X',30000,3.7,8,'A-R7-27X'),
(17,'AMD','Ryzen 7 3700X',34000,3.6,8,'A-R7-37X'),
(18,'AMD','Ryzen 7 3800X',39000,3.9,8,'A-R7-38X'),
(19,'AMD','Ryzen 7 PRO 4570G',42000,3.6,8,'A-R7P-457G'),
(20,'AMD','Ryzen 7 3800XT',45000,3.9,8,'A-R7-38XT'),
(21,'AMD','Ryzen 7 5800X',52000,3.8,8,'A-R7-58X'),
(22,'AMD','Ryzen 9 3900X',55000,3.6,12,'A-R9-39X'),
(23,'AMD','Ryzen 9 3900XT',60000,3.8,12,'A-R9-39XT'),
(24,'AMD','Ryzen 9 5900X',71000,3.7,12,'A-R9-59X'),
(25,'AMD','Ryzen 9 3950X',86000,3.5,16,'A-R9-395X'),
(26,'AMD','Threadripper 2990WX',200000,3.0,32,'A-TR-299WX'),
(27,'Intel','Core i3-10100F',14000,3.6,4,'I-3-101F'),
(28,'Intel','Core i3-9100F',14000,3.6,4,'I-3-91F'),
(29,'Intel','Core i3-10100',14000,3.6,4,'I-3-101'),
(30,'Intel','Core i3-8100',15000,3.6,4,'I-3-81'),
(31,'Intel','Core i3-9100',16000,3.6,4,'I-3-91'),
(32,'Intel','Core i3-8300',17000,3.7,4,'I-3-83'),
(33,'Intel','Core i5-7640X',19000,4.0,4,'I-5-76X'),
(34,'Intel','Core i3-10300',21000,3.7,4,'I-3-103'),
(35,'Intel','Core i5-9400F',16000,2.9,6,'I-5-94F'),
(36,'Intel','Core i5-10400F',17000,2.9,6,'I-5-104F'),
(37,'Intel','Core i5-8400',18000,2.8,6,'I-5-84'),
(38,'Intel','Core i5-9400',20000,2.9,6,'I-5-94'),
(39,'Intel','Core i5-10400',22000,2.9,6,'I-5-104'),
(40,'Intel','Core i5-9600KF',25000,3.7,6,'I-5-96KF'),
(41,'Intel','Core i5-9600',26000,3.1,6,'I-5-96'),
(42,'Intel','Core i5-9600K',26000,3.7,6,'I-5-96K'),
(43,'Intel','Core i5-10500',28000,3.1,6,'I-5-105'),
(44,'Intel','Core i5-10600KF',29000,4.1,6,'I-5-106KF'),
(45,'Intel','Core i5-10600',30000,3.3,6,'I-5-106'),
(46,'Intel','Core i5-10600K',34000,4.1,6,'I-5-106K'),
(47,'Intel','Core i5-8600',35000,3.1,6,'I-5-86'),
(48,'Intel','Core i7-6800K',45000,3.4,6,'I-7-68K'),
(49,'Intel','Core i7-9700KF',30000,3.6,8,'I-7-97KF'),
(50,'Intel','Core i7-9700',31000,3.0,8,'I-7-97'),
(51,'Intel','Core i7-9700F',32000,3.0,8,'I-7-97F'),
(52,'Intel','Core i7-9700KF',35000,3.6,8,'I-7-97KF'),
(53,'Intel','Core i7-10700F',36000,2.9,8,'I-7-107F'),
(54,'Intel','Core i9-9900',40000,3.1,8,'I-9-99'),
(55,'Intel','Core i7-10700',42000,2.9,8,'I-7-107'),
(56,'Intel','Core i9-9900KF',45000,3.6,8,'I-9-99KF'),
(57,'Intel','Core i7-1070KF',47000,3.8,8,'I-7-107KF'),
(58,'Intel','Core i7-10700K',50000,3.8,8,'I-7-107K'),
(59,'Intel','Core i9-9900KF',51000,3.6,8,'I-9-99KF'),
(60,'Intel','Core i9-10900',50000,2.8,10,'I-9-109'),
(61,'Intel','Core i9-10900F',54000,2.8,10,'I-9-109F'),
(62,'Intel','Core i9-10850K',58000,3.6,10,'I-9-1085K'),
(63,'Intel','Core i9-10900KF',60000,3.7,10,'I-9-109KF'),
(64,'Intel','Core i9-10900KF',73000,3.7,10,'I-9-109KF'),
(65,'Intel','Core i9-10900X',116000,3.7,10,'I-9-109X')

SET IDENTITY_INSERT dbo.Processors OFF

GO