import Configuration from "../configuration.js";

export default class Motherboard {
    constructor(ID, SerialNumber, Manufacturer, Model, Price) {
        this.ID = ID;
        this.SerialNumber = SerialNumber;
        this.Manufacturer = Manufacturer;
        this.Model = Model;
        this.Price = Price;
    }

    returnRendered() {

        let final = document.createElement('div');
        let tabela = document.createElement('table');
        final.appendChild(tabela);
        tabela.className = 'prodavacTabela';

        //komponenta heder
        let heder = document.createElement('tr');
        let hederCelija = document.createElement('th'); hederCelija.colSpan = 2;
        hederCelija.innerText = 'Matična';
        heder.appendChild(hederCelija);
        tabela.appendChild(heder);

        // tabela sa hederima
        let tabelaHederi = document.createElement('tr');
        ['Svojstvo', 'Vrednost'].forEach((i) => {
            let kolona = document.createElement('th');
            kolona.innerText = i;
            tabelaHederi.appendChild(kolona);
        });
        tabela.append(tabelaHederi);

        let prevodi = ['Serijski broj', 'Proizvođač', 'Model', 'Cena'];
        ['SerialNumber', 'Manufacturer', 'Model', 'Price'].forEach((svojstvo, i) => {
            let red = document.createElement('tr');

            let naziv = document.createElement('td');
            naziv.innerText = prevodi[i];

            let vrednost = document.createElement('td');
            let vrednostPodatak = this[svojstvo] ?? '/';
            vrednost.innerText = vrednostPodatak;

            red.appendChild(naziv);
            red.appendChild(vrednost);

            tabela.appendChild(red);
        });

        let slikaRed = document.createElement('tr');
        let nazivSlike = document.createElement('td'); nazivSlike.innerText = 'Slika';
        let slikaCelija = document.createElement('td');
        let slika = document.createElement('img');
        slikaCelija.appendChild(slika);
        slika.src = Configuration.slikaKompanije(this.Manufacturer);

        slikaRed.appendChild(nazivSlike);
        slikaRed.appendChild(slikaCelija);

        tabela.append(slikaRed);

        return final;
    }
}