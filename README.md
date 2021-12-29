# Web Projekat - Prodavnica Računara

<!-- bedzevi -->
![C#](https://img.shields.io/badge/c%23-%23239120.svg?logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white)
![HTML5](https://img.shields.io/badge/html5-%23E34F26.svg?logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/css3-%231572B6.svg?logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/javascript-%23323330.svg?logo=javascript&logoColor=%23F7DF1E)

Ovo je projekat za potrebe premeta "Web Programiranje" na trećoj godini Elektronskog Fakulteta u Nišu. Tema projekta je **prodavnica računara**,
odnosno nešto što podseća na **kontrolnu tablu** neke prodavnice računara.

## 📝 Obaveštenja
- Pre pokretanja dodati sve podatke u bazu: [`0 - SVE ZAJEDNO.sql`](https://github.com/xTeamStanly/WebProjekat-ProdavnicaRacunara/blob/main/Podaci/0%20-%20SVE%20ZAJEDNO.sql)
  - Ovaj sql upit je pravljen specifično za [Azure Data Studio](https://github.com/microsoft/azuredatastudio)
  - Zato je podeljen na delove od po maksimum 1000 _INSERT INTO_ operacija
  - Korišćen _connection string_ pokazuje na server `ProdavnicaRacunaraDB`, a baza se zove `ProdavnicaRacunara`
- Backend
  - .NET Core 5
  - [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/5.0.12) (v5.0.12)
    - [Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/5.0.12)
    - [SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/5.0.12)

## 📖 O implementaciji



## 🧰 Primer rada (11 slika - neke slike su prilično velike)
  1. Ovako izgleda aplikacija kada se pokrene. Postoje instance tri radnje, pa je potrebno odabrati
     posebno svaku radnju. Sve prodavnice, odnosno njihova imena, se dinamički učitavaju iz baze i
     prikazuju u padajućoj listi.<br><br>
     ![Slika1](assets/slika1.png)

  2. Izbor prodavnice preko padajuće liste. Iz baze se pribavljaju informacije o odabranoj prodavnici i
     pamte se u lokalnoj promenljivoj unutar [`data.js`](https://github.com/xTeamStanly/WebProjekat-ProdavnicaRacunara/blob/main/Frontend/js/data.js) fajla. U njemu se nalaze promenljive za sve tri instance prodavnice.<br><br>
     ![Slika2](assets/slika2.png)

  3. Izgled glavnog menija aplikacije. Odavde možete odabrati bilo koju od opcija vezanih za
     prvu, drugu ili treću instancu prodavnice. Svaka instanca prodavnice ima drugačiju boju
     pozadine i odvojena je isprekidanom linijom kako bi se lakše razlikovale. U gornjem delu
     prostora za prodavnicu prikazane su informacije o istoj.<br><br>
     ![Slika3](assets/slika3.png)

  4. Zarad demonstracije, odabrana je opcija `Nasumičan radnik u prodavnici`, rezultat te operacije
     je prikazan ispod menija. Taj odeljak za prikaz se zove `platno`.<br><br>
     ![Slika4](assets/slika4.png)

  5. Možemo odabrati drugu prodavnicu iz padajućeg menija namenjenog
     drugoj instanci prodavnice.<br><br>
     ![Slika5](assets/slika5.png)

  6. Pojavljuje se meni za drugu instancu prodavnice. Zarad primera, odabrana je
     opcija `Dodaj kupca`. Posle odabira na `platnu` se iscrtava forma za dodavanje
     novog kupca u bazu podataka.<br><br>
     ![Slika6](assets/slika6.png)

  7. Možemo odabrati treću prodavnicu iz padajućeg menija namenjenog
     trećoj instanci prodavnice.<br><br>
     ![Slika7](assets/slika7.png)

  8. Pojavljuje se meni za treću instancu prodavnice. Zarad primera, odabrana je
     opcija `Dodaj konfiguraciju`. Posle odabira na `platnu` se iscrtava forma za dodavanje
     nove konfiguracije u bazu podataka.<br><br>
     ![Slika8](assets/slika8.png)

  9. Forma za dodavanje nove konfiguracije dinamički uzima moguće delove iz baze podataka
     i popunjava odgovarajuće padajuće liste tim delovima.<br><br>
     ![Slika9](assets/slika9.png)

  10. Ovako izgleda aplikacija kada joj se dodeli veći, odnosno širi, prozor.<br><br>
      ![Slika10](assets/slika10.png)

  11. Ovako izgleda aplikacija kada se nalazi u nekom užem prozoru, na primer
      mobilni telefon. Meni aplikacije je fleksibilan.<br><br>
      ![Slika11](assets/slika11.png)

  12. Takođe prikaz delova konfiguracije je isto fleksibilan, ovako izgleda kada je razvučen.<br><br>
      ![Slika12](assets/slika12.png)

  13. Ovako izgleda prikaz delova konfiguracije kada je skupljen.<br><br>
      ![Slika13](assets/slika13.png)
