import { fetchData, renderData } from "../tools.js";
import Vendor from "./vendor/vendor.js";

export default class Store {
    constructor(ID, Name, Address, Vendors, Node) {
        this.ID = ID;
        this.Name = Name;
        this.Address = Address;
        this.Vendors = Vendors;
        this.Node = Node;
    }

    async reload() {
        const data = await fetchData(`https://localhost:5001/Store/GetStore/ID/${this.ID}`);
        this.Name = data.name;
        this.Address = data.address;
        this.Vendors = data.employees;
        this.render();
    }

    renderInfo() {
        let informacijeDiv = document.createElement('div'); informacijeDiv.className = 'informacije';
        let naslov = document.createElement('div'); naslov.className = 'naslov'; naslov.innerText = this.Name;
        let adresa = document.createElement('div'); adresa.className = 'adresa'; adresa.innerText = ` ━━━┃ ${this.Address} ┃━━━`;

        informacijeDiv.appendChild(naslov);
        informacijeDiv.appendChild(adresa);

        this.Node.appendChild(informacijeDiv);
    }

    renderKontrole() {
        let kontrole = document.createElement('div');
        kontrole.className = 'kontrole';

        // === radnik kontrola ===
        let radnikDiv = document.createElement('div');
        radnikDiv.className = 'kontrola';
        let radnikDivNaslov = document.createElement('div');
        radnikDivNaslov.className = 'kontrolaNaslov';
        radnikDivNaslov.innerText = 'Radnik';
        radnikDiv.appendChild(radnikDivNaslov);

        let dodajRadnika = document.createElement('button');
        dodajRadnika.innerText = "Dodaj radnika";
        dodajRadnika.onclick = async (ev) => {
            console.log('dodaj radnika');
            //todo render forma za dodavanje radnika
        }

        //todo dodaj jos dugmica (ukloni/listaj)

        radnikDiv.appendChild(dodajRadnika);
        kontrole.appendChild(radnikDiv);



        // === kupac kontrola ===
        let kupacDiv = document.createElement('div');
        kupacDiv.className = 'kontrola';
        let kupacDivNaslov = document.createElement('div');
        kupacDivNaslov.className = 'kontrolaNaslov';
        kupacDivNaslov.innerText = 'Kupac';
        kupacDiv.appendChild(kupacDivNaslov);

        let dodajKupca = document.createElement('button');
        dodajKupca.innerText = "Dodaj kupca";
        dodajKupca.onclick = async (ev) => {
            console.log('dodaj kupca');
            //todo render forma za dodavanje kupca
        }

        //todo dodaj jos dugmica (info (alert JMBG) i kupovine (alert JMBG))

        kupacDiv.appendChild(dodajKupca);
        kontrole.appendChild(kupacDiv);



        // === konfiguracija ===
        let konfiguracijaDiv = document.createElement('div');
        konfiguracijaDiv.className = 'kontrola';
        let konfiguracijaDivNaslov = document.createElement('div');
        konfiguracijaDivNaslov.className = 'kontrolaNaslov';
        konfiguracijaDivNaslov.innerText = 'Konfiguracija';
        konfiguracijaDiv.appendChild(konfiguracijaDivNaslov);

        let dodajKonfiguraciju = document.createElement('button');
        dodajKonfiguraciju.innerText = 'Dodaj konfiguraciju';
        dodajKonfiguraciju.onclick = async (ev) => {
            console.log('dodaj konfiguraciju');
            //todo render forma za dodavanje konfiguracije
        }

        konfiguracijaDiv.appendChild(dodajKonfiguraciju);
        kontrole.appendChild(konfiguracijaDiv);



        // === kupovina ===
        let kupovinaDiv = document.createElement('div');
        kupovinaDiv.className = 'kontrola';
        let kupovinaDivNaslov = document.createElement('div');
        kupovinaDivNaslov.className = 'kontrolaNaslov';
        kupovinaDivNaslov.innerText = 'Kupovina';
        kupovinaDiv.appendChild(kupovinaDivNaslov);

        let dodajKupovinu = document.createElement('button');
        dodajKupovinu.innerText = "Dodaj kupovinu";
        dodajKupovinu.onclick = async (ev) => {
            console.log('dodaj kupovinu');
            //todo render forma za dodavanje kupovine
        }

        //todo dodaj jos dugmica za kupovinu (filter dugme - forma)

        kupovinaDiv.appendChild(dodajKupovinu);
        kontrole.appendChild(kupovinaDiv);

        this.Node.appendChild(kontrole);
    }

    async render() {
        if(!this.Node) { return; }

        this.renderInfo();
        this.renderKontrole();
        return;


        this.renderKontrola('radnik', kontrole);
        this.renderKontrola('kupac', kontrole);
        this.renderKontrola('konfiguracija', kontrole);
        this.renderKontrola('kupovina', kontrole);




        let divLista = this.Node.querySelector('.list');

        let opis = document.createElement('div');
        opis.classList = 'description';

        let naziv = document.createElement('p');
        naziv.className = 'title';
        naziv.innerText = this.Name;
        opis.appendChild(naziv);

        let adresa = document.createElement('p');
        adresa.className = 'address';
        adresa.innerText = this.Address;
        opis.appendChild(adresa);

        divLista.appendChild(opis);

        let addVendorButton = document.createElement('button');
        addVendorButton.innerText = "Dodaj radnika";
        addVendorButton.onclick = async (ev) => {

            let canvas = this.Node.querySelector('.info');

            let naslov = document.createElement('div');
            naslov.innerHTML = "Dodaj radnika";


            let vendorForm = document.createElement('form'); vendorForm.method = 'post';

            let jmbgLabel = document.createElement('label'); jmbgLabel.innerText = "JMBG: ";
            vendorForm.appendChild(jmbgLabel);

            let inputJMBG = document.createElement('input');
            vendorForm.append(inputJMBG);

            let br1 = document.createElement('br');
            vendorForm.append(br1);


            canvas.appendChild(vendorForm);
            renderData(vendorForm, canvas);
        }


        divLista.appendChild(addVendorButton);





        let table = document.createElement('table');

        let tableTitle = document.createElement('tr');
        let tableTitleCell = document.createElement('th');
        tableTitleCell.innerText = "Zaposleni";
        tableTitleCell.colSpan = 4;
        tableTitle.appendChild(tableTitleCell);
        table.appendChild(tableTitle);

        //header tabele
        let tableHeader = document.createElement('tr');
        ['Ime', 'Prezime', 'Plata', 'Info'].forEach((i) => {
            let kolona = document.createElement('th');
            kolona.innerText = i;
            tableHeader.appendChild(kolona);
        });
        table.appendChild(tableHeader)

        let infoDiv = this.Node.querySelector('.info');

        //redovi => tabela
        this.Vendors.forEach(async (vendor) => {

            let vendorObject = new Vendor();
            vendorObject.ID = vendor.id;
            vendorObject.Node = infoDiv;
            await vendorObject.fetchVendor();



            let red = document.createElement('tr');
            ['name', 'surname', 'salary'].forEach((i) => {
                let celija = document.createElement('td');
                celija.innerText = vendor[i];
                red.appendChild(celija);
            });

            let buttonRow = document.createElement('tr');
            let button = document.createElement('button'); button.innerText = "Info";

            button.onclick = (ev) => {

                vendorObject.draw();
            }

            buttonRow.appendChild(button);
            red.appendChild(buttonRow);

            table.appendChild(red);
        });


        divLista.appendChild(table);


    }
}