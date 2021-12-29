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



## 🧰 Rad
  1. Ovako izgleda aplikacija kada se pokrene. Postoje instance tri radnje, pa je potrebno odabrati
    posebno svaku radnju. Sve radnje se dinamički učitavaju iz baze i prikazuju u listi.
    ![Slika1](assets/slika1.png){:height="10px" width="10px"}