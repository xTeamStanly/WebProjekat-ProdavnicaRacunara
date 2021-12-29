# Web Projekat - Prodavnica Računara

<!-- bedzevi -->
![C#](https://img.shields.io/badge/c%23-%23239120.svg?logo=c-sharp&logoColor=white)
![.Net](https://img.shields.io/badge/.NET-5C2D91?logo=.net&logoColor=white)
![HTML5](https://img.shields.io/badge/html5-%23E34F26.svg?logo=html5&logoColor=white)
![CSS3](https://img.shields.io/badge/css3-%231572B6.svg?logo=css3&logoColor=white)
![JavaScript](https://img.shields.io/badge/javascript-%23323330.svg?logo=javascript&logoColor=%23F7DF1E)

Ovo je projekat za potrebe premeta "Web Programiranje" na trećoj godini Elektronskog Fakulteta u Nišu. Tema projekta je **prodavnica računara**,
odnosno nešto što podseća na **kontrolnu tablu** neke prodavnice računara. Omogućava kreiranje kupaca, prodavaca,
konfiguracija, kupovina, kontakata kupaca i kontakata prodavca. Takođe mogu se brisati konfiguracije, kupovine,
prodavci i kupci. Kada je reč o menjanju, mogu se menjati kupac i prodavac. Kupovine se mogu filtrirati po odabranom kriterijumu.
Mogu se prikazati informacije o kupcu, radniku i konfiguraciji. Ništa od podataka se ne čuva lokalno, osim prodavnica
i njihovih lokalnih (zaposlenih) radnika, tako da se svi potrebni podaci uzimaju iz baze dinamički.

## 📝 Obaveštenja
- Pre pokretanja dodati sve podatke u bazu: [`0 - SVE ZAJEDNO.sql`](https://github.com/xTeamStanly/WebProjekat-ProdavnicaRacunara/blob/main/Podaci/0%20-%20SVE%20ZAJEDNO.sql)
  - Ovaj SQL upit je pravljen specifično za [Azure Data Studio](https://github.com/microsoft/azuredatastudio) i neće raditi na ostalim platformama
  - Zato je podeljen na delove od po maksimum 1000 _INSERT INTO_ operacija
  - Korišćen _connection string_ pokazuje na server `ProdavnicaRacunaraDB`, a baza se zove `ProdavnicaRacunara`
- Backend
  - .NET Core 5
  - [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore/5.0.12) (v5.0.12)
    - [Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/5.0.12)
    - [SqlServer](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.SqlServer/5.0.12)

## 📖 O implementaciji
  - [Backend](https://github.com/xTeamStanly/WebProjekat-ProdavnicaRacunara/tree/main/Backend)
    > Za svaku klasu u [Models](https://github.com/xTeamStanly/WebProjekat-ProdavnicaRacunara/tree/main/Backend/Models) folderu
    > napravljen je po jedan kontroler. Svaki kontroler implementira sve *CRUD* operacije. Za komplikovanije *LINQ* upite
    > korišćen je `Eager Loading` (`Include` i `ThenInclude`). Jedini kontroler koji nije pravljen specifično za neku klasu je
    > [`AllPartsController.cs`](https://github.com/xTeamStanly/WebProjekat-ProdavnicaRacunara/blob/main/Backend/Controllers/Parts/AllPartsController.cs).
    > On služi da vrati sve potrebne podatke, samo ID i Naziv, za sve komponente kako bi se učitale u određenu formu da korisnik može da izabere koju želi.
    >

  - [Frontend](https://github.com/xTeamStanly/WebProjekat-ProdavnicaRacunara/tree/main/Frontend)
    > Podaci o instancama prodavnica se nalaze u posebnom [`data.js`](https://github.com/xTeamStanly/WebProjekat-ProdavnicaRacunara/blob/main/Frontend/js/data.js) fajlu. Svaka prodavnica se nalazi u posebnom kontejner bloku.
    > Svaki kontejner block mozemo da podelimo na 3 dela: ***deo za informacije***, ***deo za kontrole*** (meni) i ***platno*** gde se iscrtavaju
    > odabrane forme. Skoro svaka klasa koja treba da se crta sadrži svojstvo `Node` ili `this.Node` koje ukazuje na mesto gde treba da se iscrta,
    > uglavnom `Node` treba da ukazuje na `platno`, osim ako nije reč o nekom redu u tabeli ili delu tabele. Klasa `Store` sadrži svu potrebnu logiku
    > za crtanje različitih formi za unos, uređivanje, brisanje, kao i za prikazivanje informacija. Korisniku se nikada ne prikazuju ID-jevi, već mu se
    > prikazuju drugačije jedinstvene identifikacione vrednosti, primer: matični broj radnika ili kupca, serijski broj neke komponente ili naziv konfiguracije.

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
