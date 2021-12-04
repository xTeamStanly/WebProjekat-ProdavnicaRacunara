SET IDENTITY_INSERT dbo.Vendors ON
INSERT INTO dbo.Vendors
([ID],[Name],[MiddleName],[Surname],[Address],[Gender],[Birthdate],[Salary],[JMBG],[StoreID])
VALUES
(1,'Nika',NULL,'Milojević','Vladete Kovačevića 30','Ž','27-Dec-1969',118098,3109815156693, 1),
(2,'Sonja','G','Filipović','Mitropolita Petra 6','Ž','23-Sep-1997',135928,4836695560738, 1),
(3,'Ana',NULL,'Ćirić','Mokroluška 5','Ž','30-Jun-1953',133327,4546166489591, 1),
(4,'Elena',NULL,'Filipović','Vojislava Vučkovića 18','Ž','30-Dec-1976',124387,3026137165637, 1),
(5,'Aleksandra',NULL,'Jakovljević','Zaplanjska 27','Ž','3-Nov-1954',99237,3728979796134, 1),
(6,'Julija',NULL,'Veličković','Vladimira Nazora 4','Ž','25-Feb-1999',127401,8351954924945, 1),
(7,'Elena',NULL,'Paunović','Treći Bulevar 3','Ž','11-Nov-1954',104063,1020673498113, 1),
(8,'Višnja',NULL,'Živković','Đure Strugara 7','Ž','27-Apr-1957',105174,7322277739472, 1),
(9,'Jana',NULL,'Jeremić','Ljubićka 33','Ž','31-Mar-1963',71456,3131817922852, 1),
(10,'Nina',NULL,'Stojanović','Paraćinska 10','Ž','1-Feb-1974',58082,1039474154168, 1),
(11,'Srna',NULL,'Kovačević','Veslina Masleše 17','Ž','9-Jun-1953',107554,1539927239303, 1),
(12,'Ivona',NULL,'Ivanović','Ulica 7. 22','Ž','1-Apr-1956',138261,4314042880765, 1),
(13,'Drina',NULL,'Božić','Vojvode Stepe 34','Ž','19-Jun-1950',119468,6142571752128, 1),
(14,'Teodora',NULL,'Jevtić','Bačka 41','Ž','20-Jun-1969',111297,7621268386697, 1),
(15,'Kalina','K','Lazić','Vladimira Gačinovića 10','Ž','5-Jul-1980',77012,3575458790123, 1),
(16,'Emilija',NULL,'Tasić','Jerusalimska 9','Ž','28-Apr-1974',137818,7760609473521, 1),
(17,'Viktorija',NULL,'Tasić','Živote Stepanovića 20','Ž','4-Aug-1970',91414,8474366815961, 1),
(18,'Minja',NULL,'Gajić','Ulica 1. 50','Ž','12-Dec-1980',126972,4396941743492, 1),
(19,'Katja',NULL,'Antić','Save Kovačevića 17','Ž','26-Mar-1972',119542,7333228701536, 1),
(20,'Drina',NULL,'Antić','Alekse Dundića 22','Ž','18-Aug-1991',101774,8173781709008, 1),
(21,'Natalija',NULL,'Petrović','Lazara Savatića 47','Ž','10-Apr-1961',132361,3756543546386, 1),
(22,'Aleksandra',NULL,'Savić','Hajima Davića 33','Ž','8-Dec-1971',89488,2909886309068, 1),
(23,'Kristina','I','Aleksić','Straška Pindžura 20','Ž','11-Aug-1985',115051,8066366386496, 1),
(24,'Olga',NULL,'Bogdanović','Dobrinovićeva 38','Ž','1-Jan-1961',143941,7777872040432, 1),
(25,'Anđelija',NULL,'Ivković','Pajsijeva 49','Ž','11-Apr-1973',83364,9180677630183, 1),
(26,'Miona','B','Jevtić','Trešnja 23','Ž','19-Apr-1992',52822,8711960941135, 1),
(27,'Hana','Š','Bogdanović','Rade Vranešević 46','Ž','6-Jun-1993',65359,6424336934758, 1),
(28,'Isidora',NULL,'Stefanović','Petrinjska 25','Ž','3-Sep-1987',109060,5546975176032, 1),
(29,'Lena','I','Ivković','Đorđa Nešića 32','Ž','1-Jul-1980',86706,4228796329706, 1),
(30,'Una',NULL,'Radulović','Sibinjanin Janka 42','Ž','24-Oct-1978',131921,1150689059313, 1),
(31,'Jelisaveta',NULL,'Stamenković','Vinogradska 37','Ž','31-Mar-1979',103929,7438263606438, 1),
(32,'Angelina','Z','Radović','Džordža Vašingtona 33','Ž','8-Apr-1965',132668,1093439835190, 1),
(33,'Tara','R','Milenković','Prvomajska 50','Ž','8-Jun-1973',83741,1031818807512, 1),
(34,'Ružica',NULL,'Jeremić','Ljermontova 1','Ž','3-Apr-1978',140326,3644460589709, 1),
(35,'Minja',NULL,'Radosavljević','Orlovska 30','Ž','11-Jun-1982',108572,7461041012631, 1),
(36,'Ljubica','O','Pavlović','Kraljevačkih Žrtava 4','Ž','13-Jun-1964',51076,9534829898998, 1),
(37,'Eva',NULL,'Bogdanović','Ljube Kovačevića 1','Ž','21-Dec-1964',108646,5992816451887, 1),
(38,'Tatjana',NULL,'Milić','Miška Jovanovića 28','Ž','5-May-1960',131325,2360864078086, 1),
(39,'Nika','J','Stanković','Čunarska 15','Ž','23-Sep-1981',116271,8810904509164, 1),
(40,'Julija',NULL,'Stanojević','Sime Šolaje 31','Ž','1-Aug-1984',61724,1061575159356, 2),
(41,'Iris',NULL,'Aleksić','Ane Ahmatove 42','Ž','5-Sep-2002',143330,5968554149048, 2),
(42,'Kruna',NULL,'Marinković','Ladne Vode 45','Ž','26-Jan-1994',89858,8605874890826, 2),
(43,'Martina',NULL,'Tomić','Lovačka 19','Ž','20-Jul-1987',102808,1079200753922, 2),
(44,'Jelisaveta','Š','Pavlović','Vrnjačka 35','Ž','8-Jun-1980',149339,3802204488063, 2),
(45,'Olga',NULL,'Vasiljević','Dragoljuba Ristića 8','Ž','27-May-1978',137829,1017117755787, 2),
(46,'Petra','Ž','Radovanović','Svete Simića 27','Ž','14-Sep-1959',148324,2309469402500, 2),
(47,'Kristina','Š','Ivković','Paje Jovanovića 27','Ž','23-Jun-1975',107336,8877235426087, 2),
(48,'Ružica',NULL,'Jakovljević','Slaviše Vajnera 10','Ž','9-Mar-1962',72494,6574642897562, 2),
(49,'Simona',NULL,'Tasić','Četvrtog Sremskog Bataljona 43','Ž','7-Mar-1978',136698,2174534973924, 2),
(50,'Danica',NULL,'Krstić','Kružna 46','Ž','15-Jul-1973',64205,2546363491559, 2),
(51,'Miona','Nj','Trajković','Grahovska 30','Ž','28-Apr-1961',75438,8892091216322, 2),
(52,'Simona',NULL,'Ivković','Marka Pola 6','Ž','22-Nov-1978',133135,2924329832483, 2),
(53,'Kruna',NULL,'Cvetković','Unska 12','Ž','31-Jul-1996',82312,1042183857071, 2),
(54,'Lana',NULL,'Jeremić','Karlovačka 32','Ž','8-Apr-1952',135135,4746743945438, 2),
(55,'Kristina',NULL,'Jakovljević','Ibrahima Babovića 48','Ž','28-Dec-1952',99338,7598380832931, 2),
(56,'Anastasija',NULL,'Anđelković','Ćukovačka 50','Ž','24-Jan-2001',65661,6479802467335, 2),
(57,'Anika',NULL,'Stefanović','Filipa Višnjića 28','Ž','22-Nov-1953',56945,7212044635978, 2),
(58,'Nataša',NULL,'Jevtić','Marijane Gregoran Prolaz 1. 48','Ž','1-Dec-1987',57525,3120945017744, 2),
(59,'Petra',NULL,'Tasić','Kadinjačka 11','Ž','20-Jul-1975',90432,9134045253729, 2),
(60,'Ana',NULL,'Petrović','Toplička 44','Ž','5-Oct-1952',116355,6373831316266, 2),
(61,'Andrijana',NULL,'Milojević','Mickijevićeva 7','Ž','8-Feb-1955',127519,3043113028178, 2),
(62,'Irena',NULL,'Petrović','Uralska 1','Ž','10-Nov-1962',99989,1049908054872, 2),
(63,'Tea',NULL,'Obradović','Braće Grim 28','Ž','22-Sep-1952',80393,3874831626987, 2),
(64,'Olga',NULL,'Lazić','Bregovita 32','Ž','11-Jan-1966',139728,5047394828667, 2),
(65,'Nika',NULL,'Kostić','Dr Jovana Danića 45','Ž','23-Aug-1968',143854,7540054722745, 2),
(66,'Nina',NULL,'Stojanović','Marka Čelebonovića 34','Ž','27-Jan-1955',50281,1633567351790, 2),
(67,'Dorotea',NULL,'Milić','Vodovodska 4. Deo 37','Ž','29-Apr-1962',88929,3207598163934, 2),
(68,'Iskra',NULL,'Milić','Grmečka 20','Ž','16-Sep-1982',52096,3233318172361, 2),
(69,'Jovana',NULL,'Grujić','Čarli Čaplina 21','Ž','21-Feb-1969',145123,4180722374288, 2),
(70,'Andrea',NULL,'Milutinović','Milorada Petrovića 15','Ž','1-Oct-1994',81651,9425299823016, 2),
(71,'Lara',NULL,'Marjanović','Cara Dušana 36','Ž','17-Dec-1968',58642,6710495807818, 2),
(72,'Drina',NULL,'Jović','Milice Milojković 16','Ž','4-Feb-1974',70782,7097296297508, 2),
(73,'Eva',NULL,'Mitrović','Vrbnička 20','Ž','25-Aug-1963',113856,2171003672593, 2),
(74,'Vera','Đ','Marjanović','Sopoćanska 26','Ž','6-Feb-1997',148060,5814302897178, 2),
(75,'Teodora',NULL,'Ristić','Danilova 21','Ž','30-Oct-1953',132372,9112103535931, 2),
(76,'Vida',NULL,'Milovanović','Pavla Papa 9','Ž','15-Apr-1960',81942,1339631718341, 2),
(77,'Miona','Ž','Milutinović','Kozara 17','Ž','6-Jun-1970',73037,5580866929026, 2),
(78,'Iva',NULL,'Petković','Stojana Ralice 2','Ž','3-Oct-2001',90766,5864854819579, 2),
(79,'Darija','M','Vučković','Pohorska 17','Ž','14-Jun-1957',139473,1052015821485, 2),
(80,'Jelena','J','Ivković','Alekse Bačvanskog 2','Ž','12-Jul-1955',120300,4848071244359, 3),
(81,'Simona',NULL,'Vasić','Izletnička 18','Ž','19-Jan-1962',88272,2712401248515, 3),
(82,'Ivana',NULL,'Pavlović','Postojnska 39','Ž','27-Feb-1956',52242,1246055494507, 3),
(83,'Iskra',NULL,'Trifunović','Krečanska 11','Ž','31-Mar-1992',63371,2896458999660, 3),
(84,'Sara','V','Tomić','Vojvođanska 42','Ž','13-Aug-1958',119585,8567632158661, 3),
(85,'Ivana',NULL,'Marinković','Marka Ristića 31','Ž','15-Nov-1999',62227,5685294063645, 3),
(86,'Katja',NULL,'Gajić','Stanislava Sremčevića 11','Ž','10-May-1988',116850,8463154835938, 3),
(87,'Martina',NULL,'Novaković','Sestara Strain 16','Ž','14-May-1980',84544,9641193014509, 3),
(88,'Anđelija',NULL,'Aleksić','Suvopoljska 24','Ž','4-Oct-1968',69990,2911185173474, 3),
(89,'Natalija',NULL,'Ilić','Milana Egića 25','Ž','27-Dec-1977',143229,1088841838833, 3),
(90,'Nora',NULL,'Živanović','Žitomislićka 6','Ž','25-May-1965',146726,5196700275723, 3),
(91,'Ružica',NULL,'Mladenović','Stara 11','Ž','17-Jul-1960',115079,8464234634478, 3),
(92,'Marija',NULL,'Rakić','Provalijska 1','Ž','7-Aug-1983',62069,2842831351525, 3),
(93,'Srna',NULL,'Cvetković','Sloga 29','Ž','22-Dec-1982',121394,7211853098338, 3),
(94,'Dunja',NULL,'Jevtić','Šumatovačka 35','Ž','26-Mar-1986',146511,4662586127909, 3),
(95,'Emilija','Đ','Milošević','Skojevska 25','Ž','7-Oct-1962',111183,3584975366073, 3),
(96,'Andrea',NULL,'Vučković','Majke Angeline 6','Ž','1-Aug-1952',55705,7539168025062, 3),
(97,'Valentina',NULL,'Ivković','Milana Gligorijevića 30','Ž','30-Mar-1962',133131,6379508049165, 3),
(98,'Mia','Lj','Jović','Joze Šćurle 1','Ž','4-Apr-1967',98665,5541396324390, 3),
(99,'Mia','B','Đokić','Gramšijeva 3','Ž','22-Jul-2000',114620,1064234716443, 3),
(100,'Irina',NULL,'Simić','Valentina Vodnika 18','Ž','4-Sep-1960',95112,6700499881212, 3),
(101,'Balša',NULL,'Todorović','Dr Miloša Radojčića 1','M','27-Nov-1987',114243,6341841096709, 3),
(102,'Nenad',NULL,'Marinković','Grobljanska 35','M','11-Mar-1951',97444,7616373883145, 3),
(103,'Ivan',NULL,'Milanović','Jasenička 11','M','1-Jul-1986',63764,7046849169904, 3),
(104,'Nikola','P','Vasić','Dr Dragoslava Popovića 49','M','21-Jul-1971',51945,5183265383959, 3),
(105,'Nikola','Đ','Perić','Zrenjaninski Put 15','M','30-May-1951',141057,6631556967005, 3),
(106,'Vukašin',NULL,'Aleksić','Pribojske Stepenice 21','M','3-Jul-2002',147091,9046351926193, 3),
(107,'Đorđe',NULL,'Milutinović','Ruvarčeva 45','M','6-Sep-1982',104345,7753348043220, 3),
(108,'Đurađ',NULL,'Ivković','Livanjska 12','M','23-Mar-1953',83830,3873362491677, 3),
(109,'Jasin',NULL,'Ristić','Save Jovanovića 47','M','16-Sep-1962',134662,1969653119075, 3),
(110,'Despot',NULL,'Dimitrijević','Garibaldijeva 24','M','15-Aug-1996',58045,2777716264444, 3),
(111,'Vladimir',NULL,'Stojković','Kotorska 3','M','17-Jan-1988',137648,4440807374525, 3),
(112,'Đurađ','K','Trifunović','Pančina 21','M','23-Aug-1950',58698,4714168269703, 3),
(113,'Vojin',NULL,'Pavlović','Oktobarska 37','M','8-Jan-1959',128703,7538598092225, 3),
(114,'Matea',NULL,'Tomić','Mažuranićeva 31','M','30-Sep-1999',86082,4344642733744, 3),
(115,'Arsenije',NULL,'Milenković','Dr Riharda Burijana 46','M','3-Apr-1962',53821,5615480326252, 3),
(116,'Lazar',NULL,'Živanović','Kvarnerska 7','M','9-Nov-1957',90203,3637013644972, 3),
(117,'Dragan','A','Simić','Stanoja Glavaša 6','M','19-Sep-1983',139483,5468891955013, 3),
(118,'Boris',NULL,'Đurđević','Breza 24','M','7-Feb-1969',119066,3702606268165, 3),
(119,'Aljoša',NULL,'Božić','Gastona Gravijea 23','M','25-Apr-1998',87760,6823938410598, 3),
(120,'Emir',NULL,'Gajić','Vlade Zečevića 34','M','13-Dec-1963',62714,8894718768705, 4),
(121,'Adam',NULL,'Mihajlović','Porečka 5','M','23-Mar-1982',113336,5757976712441, 4),
(122,'Danilo','A','Mitić','Kosmajska 40','M','10-Dec-1958',64537,3619971981909, 4),
(123,'Arsenije','P','Gavrilović','Gusinjska 14','M','20-Feb-1952',128798,3367545513592, 4),
(124,'Momčilo',NULL,'Vasić','Ranka Tajsića 10','M','14-May-1955',111609,8733416233390, 4),
(125,'Đorđe',NULL,'Stevanović','Nemirović Dančenka , 4),22','M','23-Oct-1970',59059,9960038553972, 4),
(126,'Relja',NULL,'Marinković','Svetozara Miletića 11','M','22-Sep-1988',132474,8864535943592, 4),
(127,'Tadej',NULL,'Radović','Bojadžijska 43','M','31-Jul-1980',76077,5849335328054, 4),
(128,'Zoran',NULL,'Đurić','Milana Bogdanovića 28','M','1-Nov-1961',57146,8329487869633, 4),
(129,'Igor',NULL,'Nešić','Simonidina 8','M','3-Aug-2001',103897,7935493303644, 4),
(130,'Simon','Z','Miljković','Draže Pavlovića 46','M','12-Feb-1968',106048,7823700240368, 4),
(131,'Relja',NULL,'Đurđević','Zage Malivuk 31','M','17-May-1970',114140,8158965656549, 4),
(132,'Oleg','U','Đurđević','Prvomajska 37','M','2-Oct-2001',142855,4532970266433, 4),
(133,'Jovan',NULL,'Živanović','Sremska 13','M','28-Dec-1992',107991,5477097539497, 4),
(134,'Tadej',NULL,'Dimitrijević','Nušićeva 27','M','1-Oct-1988',88964,2920142293883, 4),
(135,'Uglješa','A','Stanojević','Vladete Popovića Pineckog 6','M','10-Sep-1971',81078,9259112317245, 4),
(136,'Vukan',NULL,'Bošković','Koste Jovanovića 24','M','11-Dec-1964',149540,1911205956270, 4),
(137,'Adam',NULL,'Lazarević','Čakorska 12','M','20-Jan-1978',134874,4888208454971, 4),
(138,'Leon',NULL,'Tasić','Kružni Put 6','M','25-Oct-1958',102368,1040026259885, 4),
(139,'Božidar',NULL,'Kostić','Velizara Kosanovića 4','M','25-Mar-1972',145512,7562454427767, 4),
(140,'Vukan',NULL,'Anđelković','Ulica 5. 45','M','31-Jul-1966',90166,7360682268512, 4),
(141,'Andrej',NULL,'Simić','Braće Ribnikar 16','M','30-Jan-1969',144321,3488968645290, 4),
(142,'Božidar',NULL,'Trifunović','Gavrila Principa 26','M','12-Mar-1957',121483,5821829675109, 4),
(143,'Gavrilo',NULL,'Popović','Milana Kuča 32','M','19-Nov-1998',88953,5872561956200, 4),
(144,'Filip',NULL,'Stefanović','Ulica 88. 48','M','29-Jul-1951',69116,1007969348009, 4),
(145,'Stefan','P','Đukić','Mira Popare 17','M','16-Apr-1955',129995,9393957923081, 4),
(146,'Božidar',NULL,'Urošević','Nikole Đurkovića 21','M','9-Jul-1992',119555,9800726979358, 4),
(147,'Sava','J','Stefanović','Listopadna 34','M','13-Aug-1962',97839,7697275176948, 4),
(148,'Boris',NULL,'Stanojević','Janka Jankovića 8','M','15-Aug-1993',63080,1040265265544, 4),
(149,'Mijat','Nj','Janković','Đoke Krstića 10','M','26-Feb-1999',71216,9981775930889, 4),
(150,'Konstantin',NULL,'Ristić','Timočka 37','M','27-Jun-1961',149406,6125038132113, 4),
(151,'Vujadin',NULL,'Tomić','Velje Miljković 26','M','4-Nov-1990',137782,7515813768268, 4),
(152,'Konstantin',NULL,'Nedeljković','Kralja Bodina 49','M','15-May-1965',81485,5137686845239, 4),
(153,'Luka',NULL,'Nikolić','Mornarska 45','M','8-Dec-1990',86714,3008788568456, 4),
(154,'Tadija',NULL,'Milić','Stevana Lilića 15','M','8-Nov-1978',76596,8425351015237, 4),
(155,'Martin',NULL,'Milenković','Ruže Jovanović 23','M','10-Sep-1994',106135,6201796331969, 4),
(156,'Pavle','S','Radovanović','Jurice Ribara 43','M','15-Apr-2002',67339,6650802457634, 4),
(157,'Relja',NULL,'Mitić','Carice Milice 17','M','13-Sep-1972',91903,9205136989967, 4),
(158,'Stefan',NULL,'Petković','Milana Miličevića 46','M','30-Aug-1978',71570,3372724405030, 4),
(159,'Filip',NULL,'Miljković','Brankova 35','M','24-Jan-1962',89776,1034546948204, 5),
(160,'Srđan',NULL,'Milenković','Raška Dimitrijevića 21','M','2-Jun-1987',101705,8532698006065, 5),
(161,'Adam',NULL,'Jović','Dobračina 10','M','2-Mar-1972',146675,9007993562659, 5),
(162,'Đorđe',NULL,'Jakovljević','Vladimira Radovanovića 48','M','12-Sep-1977',112252,7196362466854, 5),
(163,'Dragan',NULL,'Savić','Vidaka Markovića 12','M','13-Jun-1993',149062,1045776299455, 5),
(164,'Đorđe',NULL,'Đurđević','Krsturska 22','M','5-Dec-1991',72789,9706420298481, 5),
(165,'Adam',NULL,'Savić','Lokrumska 36','M','10-Apr-1986',149931,2009876975554, 5),
(166,'Vojin',NULL,'Blagojević','Belog Bagrema 17','M','2-Mar-1970',60288,1029446359529, 5),
(167,'Tadija',NULL,'Milić','Barajevska 1','M','9-Oct-1975',81329,6583112811570, 5),
(168,'Saša',NULL,'Ivanović','Ulica 7. 13','M','31-Aug-1984',60302,7059277020456, 5),
(169,'Momčilo','C','Lukić','Bate Vukanović 39','M','12-Mar-1953',133002,6810220843681, 5),
(170,'Aleksandar',NULL,'Zdravković','Nova 501 1','M','15-Jun-1968',61418,6159859115745, 5),
(171,'Ognjen',NULL,'Matić','Vrčinska 47','M','10-Jul-1996',103687,1497212540667, 5),
(172,'Aleksandar',NULL,'Milenković','Sv. Prohora Pčinjskog 35','M','20-Jan-1971',88827,8828474346421, 5),
(173,'Mijat',NULL,'Ivanović','Jadranska 5','M','21-Jul-1957',139999,3480329255000, 5),
(174,'Tadej',NULL,'Jović','Milutina Bojića 1','M','28-Aug-1955',129087,1078257423952, 5),
(175,'Slobodan','D','Radovanović','Dr Huga Klajna 7','M','20-Feb-1985',65512,8186328707760, 5),
(176,'Strahinja',NULL,'Miletić','Narodnog Fronta 32','M','17-Sep-1986',75733,1013587050730, 5),
(177,'Nikolaj',NULL,'Jović','Crnomorska 6','M','9-Jun-1951',67169,5491129130505, 5),
(178,'Uglješa','J','Jakovljević','Jevrema Grujića 36','M','7-Jun-1954',87645,3398227036402, 5),
(179,'Vasilije',NULL,'Popović','Kičevska 12','M','2-Feb-1984',135439,5749919427651, 5),
(180,'Veljko',NULL,'Aleksić','Maksima Gorkog 40','M','9-Mar-2000',110344,5116692755471, 5),
(181,'Mihajlo',NULL,'Gavrilović','Darosavačka 21','M','15-Sep-1970',84463,4833159743131, 5),
(182,'Nikola',NULL,'Milovanović','Slobodana Aligrudića 24','M','7-Jul-1952',120176,3991731501338, 5),
(183,'Mijat',NULL,'Đurđević','Rusijanova 30','M','4-Nov-1956',56507,7622722778644, 5),
(184,'Ignjat',NULL,'Stanojević','Dimitrija Davidovića 3','M','20-Aug-1993',109596,8780385176624, 5),
(185,'Jovan',NULL,'Božić','Lipetska 31','M','24-May-1961',110794,4242527533532, 5),
(186,'Mijat',NULL,'Todorović','Nikole Čupića 4','M','14-Apr-1957',62399,1836958494125, 5),
(187,'Miloš',NULL,'Jakovljević','Husinskih Rudara 9','M','14-Nov-1951',128964,4844398604426, 5),
(188,'Nemanja',NULL,'Đokić','Vojna Pošta 2','M','17-Jul-1957',108084,5302475569479, 5),
(189,'Lav','K','Blagojević','Miloša Savkovića 43','M','19-Jul-2000',103678,3994643752019, 5),
(190,'Aljoša',NULL,'Lukić','Igmanska 32','M','26-Apr-1983',64168,1141182099766, 5),
(191,'Stefan',NULL,'Stefanović','Slavka Rodića 4','M','14-Jan-1994',94265,1921265299572, 5),
(192,'Ilija',NULL,'Trifunović','Ruska 1','M','8-Nov-1955',78213,8406429841977, 5),
(193,'Gavrilo',NULL,'Krstić','Batutova 50','M','19-Mar-1961',107749,9047825563865, 5),
(194,'Vujadin',NULL,'Stamenković','Dr Miloša Pantića 47','M','26-Dec-1967',147033,1003922984834, 5),
(195,'Aljoša','Č','Miljković','Camblakova 10','M','16-Dec-1972',58592,9589085277639, 5),
(196,'Mladen','R','Novaković','Branka Miljkovića 8','M','13-Jan-1952',57649,5928098035429, 5),
(197,'Mijat','Nj','Bogdanović','Vladimira Popovića 28','M','8-Oct-1985',73839,9649244870216, 5),
(198,'Despot','M','Bošković','Rastka Petrovića 34','M','3-Sep-1984',143413,7748297286731, 5),
(199,'Vasilije',NULL,'Božić','Vajara Đoke Jovanovića 36','M','9-May-1985',95532,5972280333176, 5),
(200,'Novak',NULL,'Nikolić','Vuka Vrčevića 30','M','4-Jan-1952',99546,8395411053894, 5)
SET IDENTITY_INSERT dbo.Vendors OFF

GO