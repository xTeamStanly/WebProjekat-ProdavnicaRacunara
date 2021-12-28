import { fetchData, formatError, renderData } from "../../tools.js";

export default class Customer {
    constructor(ID, JMBG, Name, MiddleName, Surname, Gender, Configurations, Contacts, Node) {
        this.ID = ID;
        this.JMBG = JMBG;
        this.Name = Name;
        this.MiddleName = MiddleName;
        this.Surname = Surname;
        this.Gender = Gender;
        this.Configurations = Configurations;
        this.Contacts = Contacts;

        this.Node = Node;
    }

    static validacija = (jmbg, ime, srednjeime, prezime, pol) => {
        if(!jmbg || !ime || !prezime || !pol) { return false; }

        let regexJmbg = new RegExp('^[1-9][0-9]{12}$'); //moze staticki posto je isti
        if(regexJmbg.test(jmbg) == false) { return false; };

        let regexPol = new RegExp('Ž|M'); //moze staticki
        if(regexPol.test(pol) == false) { return false; }

        if(ime.length > 32 || prezime.length > 32) { return false; }

        if(srednjeime && srednjeime.length > 2) { return false; }

        return true;
    }

    async reload() {
        if(!this.ID) { return; }
        await this.fetchCustomer();
        this.render();
    }

    async fetchCustomer() {
        try {
            const data = await fetchData(`https://localhost:5001/Customer/GetCustomer/ID/${this.ID}`);
            ['JMBG', 'Name', 'MiddleName', 'Surname', 'Gender', 'Address', 'Contacts'].forEach((i) => {
                if(i === 'MiddleName') {
                    this[i] = data['middleName'];
                } else {
                    this[i] = data[i.toLowerCase()];
                }
            });
        } catch(ex) { formatError(ex); }
    }

    render() {
        if(!this.Node) { return; }

        let final = document.createElement('div');
        final.appendChild(document.createElement('br'));
        let tabela = document.createElement('table');
        final.appendChild(tabela);
        tabela.className = 'prodavacTabela';

        // tabela sa hederima
        let tabelaHederi = document.createElement('tr');
        ['Svojstvo', 'Vrednost'].forEach((i) => {
            let kolona = document.createElement('th');
            kolona.innerText = i;
            tabelaHederi.appendChild(kolona);
        });
        tabela.append(tabelaHederi);

        let prevodi = ['JMBG', 'Ime', 'Srednje slovo', 'Prezime', 'Pol', 'Kontakt']; //moze staticko
        ['JMBG', 'Name', 'MiddleName', 'Surname', 'Gender', 'Contacts'].forEach((svojstvo, i) => {
            let red = document.createElement('tr');

            let naziv = document.createElement('td');
            naziv.innerText = prevodi[i];

            let vrednost = document.createElement('td');
            let vrednostPodatak = this[svojstvo] ?? '/';
            if(svojstvo === 'Contacts') {
                if(!vrednost || vrednost !== '/') {
                    vrednost.innerText = vrednostPodatak.join('\n');
                } else {
                    vrednost.innerText = vrednostPodatak;
                }
            } else {
                vrednost.innerText = vrednostPodatak;
            }

            red.appendChild(naziv);
            red.appendChild(vrednost);

            tabela.appendChild(red);
        });

        renderData(final, this.Node);
    }
}