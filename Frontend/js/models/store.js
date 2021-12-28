import { fetchData, formatError, formatErrorResponse, pickRandomFromArray, renderData } from "../tools.js";
import Configuration from "./configuration.js";
import Customer from "./customer/customer.js";
import GraphicsCard from "./parts/graphicsCard.js";
import Motherboard from "./parts/motherboard.js";
import Processor from "./parts/processor.js";
import RAM from "./parts/ram.js";
import Storage from "./parts/storage.js";
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
        await this.render();
    }

    async reloadData() {
        const data = await fetchData(`https://localhost:5001/Store/GetStore/ID/${this.ID}`);
        this.Name = data.name;
        this.Address = data.address;
        this.Vendors = data.employees;

        let platnoDiv = this.Node.querySelector('.platno');
        if(!platnoDiv) {
            platnoDiv = document.createElement('div');
            platnoDiv.className = 'platno';
            this.Node.appendChild(platnoDiv);
        }

        let oldVendors = [];
        this.Vendors.forEach((vendor) => { oldVendors.push(vendor); })
        this.Vendors = [];
        oldVendors.forEach(async (oldVendor) => {
            let vendor = new Vendor();
            vendor.ID = oldVendor.id;
            vendor.Node = platnoDiv;
            vendor.StoreID = this.ID;
            await vendor.fetchVendor();
            this.Vendors.push(vendor);
        });
    }

    renderInfo() {
        let informacijeDiv = document.createElement('div'); informacijeDiv.className = 'informacije'; informacijeDiv.id = this.ID;
        let naslov = document.createElement('div'); naslov.className = 'naslov'; naslov.innerText = this.Name;
        let adresa = document.createElement('div'); adresa.className = 'adresa'; adresa.innerText = ` ━━━┃ ${this.Address} ┃━━━`;

        informacijeDiv.appendChild(naslov);
        informacijeDiv.appendChild(adresa);

        this.Node.appendChild(informacijeDiv);
    }

    async renderKontrole() {
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
        dodajRadnika.innerText = "Dodaj i zaposli radnika";
        dodajRadnika.onclick = async (ev) => { await this.renderDodajRadnika(); }
        radnikDiv.appendChild(dodajRadnika);

        let nasumicanRadnik = document.createElement('button');
        nasumicanRadnik.innerText = "Nasumičan radnik u prodavnici";
        nasumicanRadnik.onclick = (ev) => { pickRandomFromArray(this.Vendors).render(); }
        radnikDiv.appendChild(nasumicanRadnik);

        let infoRadnik = document.createElement('button');
        infoRadnik.innerText = 'Informacije o globalnom radniku';
        infoRadnik.onclick = async (ev) => { await this.renderInfoRadnik(); }
        radnikDiv.appendChild(infoRadnik);

        let urediRadnika = document.createElement('button');
        urediRadnika.innerText = 'Uredi globalnog radnika';
        urediRadnika.onclick = async (ev) => { await this.renderUrediRadnika(); }
        radnikDiv.appendChild(urediRadnika);

        let obrisiRadnika = document.createElement('button');
        obrisiRadnika.innerText = 'Obriši globalnog radnika';
        obrisiRadnika.onclick = async (ev) => { await this.renderObrisiRadnika(); }
        radnikDiv.appendChild(obrisiRadnika);

        let radnikDodajKontakt = document.createElement('button');
        radnikDodajKontakt.innerText = 'Dodaj kontakt globalnom radniku';
        radnikDodajKontakt.onclick = async (ev) => { await this.renderDodajKontakt('radnik'); }
        radnikDiv.appendChild(radnikDodajKontakt);

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
        dodajKupca.onclick = async (ev) => { await this.renderDodajKupca(); }
        kupacDiv.appendChild(dodajKupca);

        let infoKupac = document.createElement('button');
        infoKupac.innerText = 'Informacije o kupcu';
        infoKupac.onclick = async (ev) => { await this.renderInfoKupac(); }
        kupacDiv.appendChild(infoKupac);

        let infoKupacKonfig = document.createElement('button');
        infoKupacKonfig.innerText = 'Informacije o kupovinama kupca';
        infoKupacKonfig.onclick = async (ev) => { await this.renderKupovineKupac(); }
        kupacDiv.appendChild(infoKupacKonfig);

        let urediKupca = document.createElement('button');
        urediKupca.innerText = 'Uredi kupca';
        urediKupca.onclick = async (ev) => { await this.renderUrediKupca(); }
        kupacDiv.appendChild(urediKupca);

        let obrisiKupca = document.createElement('button');
        obrisiKupca.innerText = 'Obriši kupca';
        obrisiKupca.onclick = async (ev) => { await this.renderObrisiKupca(); }
        kupacDiv.appendChild(obrisiKupca);

        let kupacDodajKontakt = document.createElement('button');
        kupacDodajKontakt.innerText = 'Dodaj kontakt kupcu';
        kupacDodajKontakt.onclick = async (ev) => { await this.renderDodajKontakt('kupac'); }
        kupacDiv.appendChild(kupacDodajKontakt);

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
        dodajKonfiguraciju.onclick = async (ev) => { await this.renderDodajKonfiguraciju(); }
        konfiguracijaDiv.appendChild(dodajKonfiguraciju);

        let infoKonfiguracija = document.createElement('button');
        infoKonfiguracija.innerText = 'Informacije o konfiguraciji';
        infoKonfiguracija.onclick = async (ev) => { await this.renderInfoKonfiguraciju(); }
        konfiguracijaDiv.appendChild(infoKonfiguracija);

        let obrisiKonfiguraciju = document.createElement('button');
        obrisiKonfiguraciju.innerText = 'Obriši konfiguraciju';
        obrisiKonfiguraciju.onclick = async (ev) => { await this.renderObrisiKonfiguraciju(); }
        konfiguracijaDiv.appendChild(obrisiKonfiguraciju);

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

    returnDobrodosli() {
        let dobrodosliDiv = document.createElement('div');
        dobrodosliDiv.className = 'dobrodosli';
        dobrodosliDiv.innerText = '😀 Dobrodošli 😀';
        return dobrodosliDiv;
    }

    async render() {
        if(!this.Node) { return; }
        this.Node.innerHtml = "";

        this.renderInfo();
        await this.renderKontrole();

        // === canvas setup ===
        let platnoDiv = this.Node.querySelector('.platno');
        if(!platnoDiv) {
            platnoDiv = document.createElement('div');
            platnoDiv.className = 'platno';

            platnoDiv.appendChild(this.returnDobrodosli());

            this.Node.appendChild(platnoDiv);
        }

        // === pretvaranje json u vendor objekat ===
        let oldVendors = [];
        this.Vendors.forEach((vendor) => { oldVendors.push(vendor); })
        this.Vendors = [];
        oldVendors.forEach(async (oldVendor) => {
            let vendor = new Vendor();
            vendor.ID = oldVendor.id;
            vendor.Node = platnoDiv;
            vendor.StoreID = this.ID;
            await vendor.fetchVendor();
            this.Vendors.push(vendor);
        });



    }

    // === forma za dodavanje radnika ===
    async renderDodajRadnika() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.method = 'post';
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let name = inputIme.value;
            let middle = inputSrednjeSlovo.value;
            let surname = inputPrezime.value;
            let gender = inputPol.value;
            let salary = Number.parseFloat(inputPlata.value);
            let birthDate = inputRodj.valueAsDate.toISOString();
            let address = inputAdresa.value;

            let obj = { jmbg, name, middleName: middle, surname, gender, salary, birthDate, address, contacts: null, vendorPurchases: null }

            if(Vendor.validacija(jmbg, name, middle, surname, gender, address) == false || salary < 50000) { formatError("Validacija neuspešna"); return; }

            try {
                let res1 = await fetch('https://localhost:5001/Vendor/AddVendor', {
                    method: 'post',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(obj)
                });
                if(!res1.ok) { await formatErrorResponse(res1); return; }


                let res2 = await fetch(`https://localhost:5001/Vendor/GetVendor/JMBG/${jmbg}`);
                if(!res2.ok) { await formatErrorResponse(res2); return; }
                res2 = await res2.json();

                let res3 = await fetch(`https://localhost:5001/Store/HireVendor/${this.ID}/${res2.id}`, {
                    method: 'post',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify({})
                });
                if(!res3.ok) { await formatErrorResponse(res3); return; }

                alert("Uspešno!");

                await this.reloadData();
                renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));
            } catch(ex) { formatError(ex); }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = 'JMBG';
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        inputJMBG.name = 'jmbg';
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));

        let labelaIme = document.createElement('label'); labelaIme.innerText = 'Ime';
        let inputIme = document.createElement('input');
        inputIme.maxLength = 32;
        inputIme.name = 'name';
        forma.appendChild(labelaIme);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputIme);
        forma.appendChild(document.createElement('br'));

        let labelaSrednjeSlovo = document.createElement('label'); labelaSrednjeSlovo.innerText = 'Srednje slovo';
        let inputSrednjeSlovo = document.createElement('input');
        inputSrednjeSlovo.maxLength = 2;
        inputSrednjeSlovo.name = 'middleName';
        forma.appendChild(labelaSrednjeSlovo);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputSrednjeSlovo);
        forma.appendChild(document.createElement('br'));

        let labelaPrezime = document.createElement('label'); labelaPrezime.innerText = 'Prezime';
        let inputPrezime = document.createElement('input');
        inputPrezime.maxLength = 32;
        inputPrezime.name = 'surname';
        forma.appendChild(labelaPrezime);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPrezime);
        forma.appendChild(document.createElement('br'));

        let labelaPol = document.createElement('label'); labelaPol.innerText = 'Pol';
        let inputPol = document.createElement('select'); inputPol.name = 'gender';
        let opcija1 = document.createElement('option'); opcija1.innerText = 'Odaberite pol'; opcija1.value = 'invalid';
        let opcija2 = document.createElement('option'); opcija2.innerText = 'M'; opcija2.value = 'M';
        let opcija3 = document.createElement('option'); opcija3.innerText = 'Ž'; opcija3.value = 'Ž';
        inputPol.appendChild(opcija1); inputPol.appendChild(opcija2); inputPol.appendChild(opcija3);
        forma.appendChild(labelaPol);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPol);
        forma.appendChild(document.createElement('br'));

        let labelaPlata = document.createElement('label'); labelaPlata.innerText = 'Plata';
        let inputPlata = document.createElement('input'); inputPlata.name = 'salary';
        forma.appendChild(labelaPlata);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPlata);
        forma.appendChild(document.createElement('br'));

        let labelaRodj = document.createElement('label'); labelaRodj.innerText = 'Datum rođenja';
        let inputRodj = document.createElement('input'); inputRodj.type = 'date'; inputRodj.name = 'birthDate';
        forma.appendChild(labelaRodj);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputRodj);
        forma.appendChild(document.createElement('br'));

        let labelaAdresa = document.createElement('label'); labelaAdresa.innerText = 'Adresa';
        let inputAdresa = document.createElement('input'); inputAdresa.name = 'address';
        forma.appendChild(labelaAdresa);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputAdresa);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za infomacije o radniku ===
    async renderInfoRadnik() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let regexJmbg = new RegExp('^[1-9][0-9]{12}$'); //moze staticki posto je isti
            if(regexJmbg.test(jmbg) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Vendor/GetVendor/JMBG/${jmbg}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                let infoRadnik = new Vendor(
                    res.id,
                    res.jmbg,
                    res.name,
                    res.middleName,
                    res.surname,
                    res.gender,
                    res.salary,
                    res.birthDate,
                    res.address,
                    res.contacts,
                    res.vendorPurchases,
                    this.Node.querySelector('.platno')
                );

                infoRadnik.render();

            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = "JMBG";
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za uredjivanje radnika ===
    async renderUrediRadnika() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let regexJmbg = new RegExp('^[1-9][0-9]{12}$'); //moze staticki posto je isti
            if(regexJmbg.test(jmbg) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Vendor/GetVendor/JMBG/${jmbg}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                let infoRadnik = new Vendor(
                    res.id,
                    res.jmbg,
                    res.name,
                    res.middleName,
                    res.surname,
                    res.gender,
                    res.salary,
                    res.birthDate,
                    res.address,
                    res.contacts,
                    res.vendorPurchases,
                    this.Node.querySelector('.platno')
                );

                //pozovi drugi deo i prosledi objekat radnika
                await this.renderUrediRadnikaDrugiDeo(infoRadnik);
            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = "JMBG";
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }
    async renderUrediRadnikaDrugiDeo(radnik) {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.method = 'post';
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let name = inputIme.value;
            let middle = inputSrednjeSlovo.value;
            let surname = inputPrezime.value;
            let gender = inputPol.value;
            let salary = Number.parseFloat(inputPlata.value);
            let birthDate = inputRodj.valueAsDate.toISOString();
            let address = inputAdresa.value;

            let obj = { id: radnik.ID, jmbg, name, middleName: middle, surname, gender, salary, birthDate, address, contacts: null, vendorPurchases: null }

            if(Vendor.validacija(jmbg, name, middle, surname, gender, address) == false || salary < 50000) { formatError("Validacija neuspešna"); return; }

            try {
                let res1 = await fetch('https://localhost:5001/Vendor/UpdateVendor', {
                    method: 'put',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(obj)
                });
                if(!res1.ok) { await formatErrorResponse(res1); return; }

                alert("Uspešno!");

                await this.reloadData();
                renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));
            } catch(ex) { formatError(ex); }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = 'JMBG';
        let inputJMBG = document.createElement('input'); inputJMBG.value = radnik.JMBG; inputJMBG.disabled = true;
        inputJMBG.maxLength = 13;
        inputJMBG.name = 'jmbg';
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));

        let labelaIme = document.createElement('label'); labelaIme.innerText = 'Ime';
        let inputIme = document.createElement('input'); inputIme.value = radnik.Name;
        inputIme.maxLength = 32;
        inputIme.name = 'name';
        forma.appendChild(labelaIme);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputIme);
        forma.appendChild(document.createElement('br'));

        let labelaSrednjeSlovo = document.createElement('label'); labelaSrednjeSlovo.innerText = 'Srednje slovo';
        let inputSrednjeSlovo = document.createElement('input'); if(radnik.MiddleName) { inputSrednjeSlovo.value = radnik.MiddleName; }
        inputSrednjeSlovo.maxLength = 2;
        inputSrednjeSlovo.name = 'middleName';
        forma.appendChild(labelaSrednjeSlovo);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputSrednjeSlovo);
        forma.appendChild(document.createElement('br'));

        let labelaPrezime = document.createElement('label'); labelaPrezime.innerText = 'Prezime';
        let inputPrezime = document.createElement('input'); inputPrezime.value = radnik.Surname;
        inputPrezime.maxLength = 32;
        inputPrezime.name = 'surname';
        forma.appendChild(labelaPrezime);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPrezime);
        forma.appendChild(document.createElement('br'));

        let labelaPol = document.createElement('label'); labelaPol.innerText = 'Pol';
        let inputPol = document.createElement('select'); inputPol.name = 'gender';
        let opcija1 = document.createElement('option'); opcija1.innerText = 'Odaberite pol'; opcija1.value = 'invalid';
        let opcija2 = document.createElement('option'); opcija2.innerText = 'M'; opcija2.value = 'M';
        let opcija3 = document.createElement('option'); opcija3.innerText = 'Ž'; opcija3.value = 'Ž';
        inputPol.appendChild(opcija1); inputPol.appendChild(opcija2); inputPol.appendChild(opcija3);
        forma.appendChild(labelaPol);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPol);
        forma.appendChild(document.createElement('br'));

        inputPol.selectedIndex = (radnik.Gender == "M") ? 1 : 2;


        let labelaPlata = document.createElement('label'); labelaPlata.innerText = 'Plata';
        let inputPlata = document.createElement('input'); inputPlata.name = 'salary'; inputPlata.value = radnik.Salary;
        forma.appendChild(labelaPlata);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPlata);
        forma.appendChild(document.createElement('br'));

        let labelaRodj = document.createElement('label'); labelaRodj.innerText = 'Datum rođenja';
        let inputRodj = document.createElement('input'); inputRodj.type = 'date'; inputRodj.name = 'birthDate'; inputRodj.valueAsDate = radnik.BirthDate;
        forma.appendChild(labelaRodj);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputRodj);
        forma.appendChild(document.createElement('br'));

        let labelaAdresa = document.createElement('label'); labelaAdresa.innerText = 'Adresa';
        let inputAdresa = document.createElement('input'); inputAdresa.name = 'address'; inputAdresa.value = radnik.Address;
        forma.appendChild(labelaAdresa);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputAdresa);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za brisanje radnika ===
    async renderObrisiRadnika() {
        if(!this.Node) { formatError('Null node!'); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let regexJmbg = new RegExp('^[1-9][0-9]{12}$'); //moze staticki posto je isti
            if(regexJmbg.test(jmbg) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Vendor/GetVendor/JMBG/${jmbg}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                let vendorID = res.id;

                res = await fetch(`https://localhost:5001/Vendor/DeleteVendor/ID/${vendorID}`, {
                    method: 'delete'
                });

                if(!res.ok) { await formatErrorResponse(res); return; }

                alert("Uspešno!");
                renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));
            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = "JMBG";
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za dodavanje kupca ===
    async renderDodajKupca() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.method = 'post';
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let name = inputIme.value;
            let middle = inputSrednjeSlovo.value;
            let surname = inputPrezime.value;
            let gender = inputPol.value;

            let obj = { jmbg, name, middleName: middle, surname, gender }

            if(Customer.validacija(jmbg, name, middle, surname, gender, address) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res1 = await fetch('https://localhost:5001/Customer/AddCustomer', {
                    method: 'post',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(obj)
                });
                if(!res1.ok) { await formatErrorResponse(res1); return; }

                alert("Uspešno!");
                renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));
            } catch(ex) { formatError(ex); }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = 'JMBG';
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        inputJMBG.name = 'jmbg';
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));

        let labelaIme = document.createElement('label'); labelaIme.innerText = 'Ime';
        let inputIme = document.createElement('input');
        inputIme.maxLength = 32;
        inputIme.name = 'name';
        forma.appendChild(labelaIme);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputIme);
        forma.appendChild(document.createElement('br'));

        let labelaSrednjeSlovo = document.createElement('label'); labelaSrednjeSlovo.innerText = 'Srednje slovo';
        let inputSrednjeSlovo = document.createElement('input');
        inputSrednjeSlovo.maxLength = 2;
        inputSrednjeSlovo.name = 'middleName';
        forma.appendChild(labelaSrednjeSlovo);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputSrednjeSlovo);
        forma.appendChild(document.createElement('br'));

        let labelaPrezime = document.createElement('label'); labelaPrezime.innerText = 'Prezime';
        let inputPrezime = document.createElement('input');
        inputPrezime.maxLength = 32;
        inputPrezime.name = 'surname';
        forma.appendChild(labelaPrezime);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPrezime);
        forma.appendChild(document.createElement('br'));

        let labelaPol = document.createElement('label'); labelaPol.innerText = 'Pol';
        let inputPol = document.createElement('select'); inputPol.name = 'gender';
        let opcija1 = document.createElement('option'); opcija1.innerText = 'Odaberite pol'; opcija1.value = 'invalid';
        let opcija2 = document.createElement('option'); opcija2.innerText = 'M'; opcija2.value = 'M';
        let opcija3 = document.createElement('option'); opcija3.innerText = 'Ž'; opcija3.value = 'Ž';
        inputPol.appendChild(opcija1); inputPol.appendChild(opcija2); inputPol.appendChild(opcija3);
        forma.appendChild(labelaPol);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPol);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za informacije o kupcu ===
    async renderInfoKupac() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let regexJmbg = new RegExp('^[1-9][0-9]{12}$'); //moze staticki posto je isti

            if(regexJmbg.test(jmbg) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Customer/GetCustomer/JMBG/${jmbg}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                let infoKupac = new Customer(
                    res.id,
                    res.jmbg,
                    res.name,
                    res.middleName,
                    res.surname,
                    res.gender,
                    res.configurations,
                    res.contacts,
                    this.Node.querySelector('.platno')
                );

                infoKupac.render();

            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = "JMBG";
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za prikazivanje kupovina nekog kupca ===
    async renderKupovineKupac() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let regexJmbg = new RegExp('^[1-9][0-9]{12}$'); //moze staticki posto je isti

            if(regexJmbg.test(jmbg) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Customer/GetCustomer/JMBG/${jmbg}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                let kupacJMBG = res.jmbg;
                let kupacIme = res.name;
                let kupacSrednjeSlovo = (!res.middleName) ? "" : (res.middleName + " ");
                let kupacPrezime = res.surname;

                let kupacID = res.id;
                res = await fetch(`https://localhost:5001/Purchase/GetPurchases/customer/${kupacID}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                if(!res || res.length < 1) { formatError('Nema kupovina'); return; }

                await this.renderKupovineKupacDrugiDeo(res, { jmbg: kupacJMBG, name: kupacIme, middleName: kupacSrednjeSlovo, surname: kupacPrezime });
            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = "JMBG";
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }
    async renderKupovineKupacDrugiDeo(podaci, kupac) {
        let final = document.createElement('div');
        final.appendChild(document.createElement('br'));
        let tabela = document.createElement('table');
        final.appendChild(tabela);
        tabela.className = 'prodavacTabela';

        let kupacHeder = document.createElement('th'); kupacHeder.colSpan = 5;
        kupacHeder.innerText = `${kupac.jmbg} - ${kupac.name} ${kupac.middleName}${kupac.surname}`;
        tabela.appendChild(kupacHeder);

        // tabela sa hederima
        let tabelaHederi = document.createElement('tr');
        ['Matični broj prodavca', 'Naziv konfiguracije', 'Cena', 'Datum', 'Način plaćanja'].forEach((i) => {
            let kolona = document.createElement('th');
            kolona.innerText = i;
            tabelaHederi.appendChild(kolona);
        });
        tabela.append(tabelaHederi);

        podaci.forEach(async (kupovina) => {
            let red = document.createElement('tr');

            let celijaMaticni = document.createElement('td');
            let fetched = await fetch(`https://localhost:5001/Vendor/GetVendor/ID/${kupovina.vendorID}`);
            if(!fetched.ok) { formatErrorResponse(fetched); return; }
            fetched = await fetched.json();
            celijaMaticni.innerText = fetched.jmbg;
            red.appendChild(celijaMaticni);

            let celijaKonfiguracija = document.createElement('td');
            fetched = await fetch(`https://localhost:5001/Configuration/GetConfiguration/ID/${kupovina.configurationID}`);
            if(!fetched.ok) { formatErrorResponse(fetched); return; }
            fetched = await fetched.json();
            celijaKonfiguracija.innerText = fetched.name;
            red.appendChild(celijaKonfiguracija);

            let cena = Configuration.cenaKonfiguracijeJson(fetched);
            let celijaCena = document.createElement('td');
            celijaCena.innerHTML = cena;
            red.appendChild(celijaCena);

            let celijaDatum = document.createElement('td');
            if(!kupovina.date) { celijaDatum.innerText = '/'; } else { celijaDatum.innerText = (new Date(kupovina.date)).toUTCString(); }
            red.appendChild(celijaDatum);

            let celijaNacinPlacanja = document.createElement('td');
            if(!kupovina.paymentType) {celijaNacinPlacanja.innerText = '/'; } else { celijaNacinPlacanja.innerText = kupovina.paymentType; }
            red.appendChild(celijaNacinPlacanja);

            tabela.appendChild(red);
        });

        renderData(final, this.Node.querySelector('.platno'));
    }

    // === forma za brisanje kupca ===
    async renderObrisiKupca() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let regexJmbg = new RegExp('^[1-9][0-9]{12}$'); //moze staticki posto je isti

            if(regexJmbg.test(jmbg) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Customer/GetCustomer/JMBG/${jmbg}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();


                res = await fetch(`https://localhost:5001/Customer/DeleteCustomer/ID/${res.id}`, {
                    method: 'delete'
                });
                if(!res.ok) { await formatErrorResponse(res); return; }

                alert("Uspešno!");
                renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));

            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = "JMBG";
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    napraviSelectOpciju(sadrzaj, vrednost) {
        let opcija = document.createElement('option');
        opcija.innerText = sadrzaj;
        opcija.value = (!vrednost) ? -1 : vrednost;
        return opcija;
    }

    // === forma za dodavanje konfiguracije ===
    async renderDodajKonfiguraciju() {
        if(!this.Node) { formatError('Null node!'); return; }
        try {
            let opcijeZaKontole = await fetch('https://localhost:5001/AllParts/GetAllParts');
            if(!opcijeZaKontole.ok) { await formatErrorResponse(opcijeZaKontole); }
            opcijeZaKontole = await opcijeZaKontole.json();

            //validacija da li ima dovoljno komponenti u bazi
            if(
                !opcijeZaKontole.cpu || opcijeZaKontole.cpu.length < 1 ||
                !opcijeZaKontole.gpu || opcijeZaKontole.gpu.length < 1 ||
                !opcijeZaKontole.ram || opcijeZaKontole.ram.length < 1 ||
                !opcijeZaKontole.mb || opcijeZaKontole.mb.length < 1 ||
                !opcijeZaKontole.storage || opcijeZaKontole.storage.length < 1
            ) { formatError("Nema dovoljno komponenti"); return; }

            // === popunjavanje selektova ===
            let selectProcesor = document.createElement('select');
            selectProcesor.appendChild(this.napraviSelectOpciju("Odaberite procesor"));
            opcijeZaKontole.cpu.forEach(i => { selectProcesor.appendChild(this.napraviSelectOpciju(i.name, i.id)); });

            let selectGraficka = document.createElement('select');
            selectGraficka.appendChild(this.napraviSelectOpciju("Odaberite grafičku"));
            opcijeZaKontole.gpu.forEach(i => { selectGraficka.appendChild(this.napraviSelectOpciju(i.name, i.id)); });

            let selectRam = document.createElement('select');
            selectRam.appendChild(this.napraviSelectOpciju("Odaberite RAM"));
            opcijeZaKontole.ram.forEach(i => { selectRam.appendChild(this.napraviSelectOpciju(i.name, i.id)); })

            let selectMaticna = document.createElement('select');
            selectMaticna.appendChild(this.napraviSelectOpciju("Odaberite matičnu"));
            opcijeZaKontole.mb.forEach(i => { selectMaticna.appendChild(this.napraviSelectOpciju(i.name, i.id)); });

            let selectSkladiste = document.createElement('select');
            selectSkladiste.appendChild(this.napraviSelectOpciju("Odaberite skladište"));
            opcijeZaKontole.storage.forEach(i => { selectSkladiste.appendChild(this.napraviSelectOpciju(i.name, i.id)); });

            let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
            let forma = document.createElement('form');
            forma.appendChild(document.createElement('br'));
            forma.method = 'post';
            forma.onsubmit = async (ev) => {
                ev.preventDefault();

                let ime = inputIme.value;
                let cpuID = Number.parseInt(selectProcesor.value);
                let gpuID = Number.parseInt(selectGraficka.value);
                let ramID = Number.parseInt(selectRam.value);
                let mbID = Number.parseInt(selectMaticna.value);
                let storageID = Number.parseInt(selectSkladiste.value);

                if(Configuration.validacija(ime, cpuID, gpuID, ramID, mbID, storageID) == false) { formatError("Validacija neuspešna"); return; }

                try {
                    let res = await fetch(`https://localhost:5001/Configuration/AddConfiguration/${ime}/CPU/${cpuID}/GPU/${gpuID}/RAM/${ramID}/MB/${mbID}/STORAGE/${storageID}`, {
                        method: 'post',
                        headers: {'Content-Type': 'application/json'},
                        body: JSON.stringify({})
                    });
                    if(!res.ok) { await formatErrorResponse(res); return; }

                    alert("Uspešno!");
                    renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));

                } catch(ex) {
                    formatError(ex);
                }
            }

            let labelaIme = document.createElement('label'); labelaIme.innerText = "Naziv";
            let inputIme = document.createElement('input'); inputIme.maxLength = 64;
            forma.appendChild(labelaIme);
            forma.appendChild(document.createElement('br'));
            forma.appendChild(inputIme);
            forma.appendChild(document.createElement('br'));

            let labelaProcesor = document.createElement('label'); labelaProcesor.innerText = "Procesor";
            forma.appendChild(labelaProcesor);
            forma.appendChild(document.createElement('br'));
            forma.appendChild(selectProcesor);
            forma.appendChild(document.createElement('br'));

            let labelaGraficka = document.createElement('label'); labelaGraficka.innerText = "Grafička";
            forma.appendChild(labelaGraficka);
            forma.appendChild(document.createElement('br'));
            forma.appendChild(selectGraficka);
            forma.appendChild(document.createElement('br'));

            let labelaRam = document.createElement('label'); labelaRam.innerText = "RAM";
            forma.appendChild(labelaRam);
            forma.appendChild(document.createElement('br'));
            forma.appendChild(selectRam);
            forma.appendChild(document.createElement('br'));

            let labelaMaticna = document.createElement('label'); labelaMaticna.innerText = "Matična";
            forma.appendChild(labelaMaticna);
            forma.appendChild(document.createElement('br'));
            forma.appendChild(selectMaticna);
            forma.appendChild(document.createElement('br'));

            let labelaSkladiste = document.createElement('label'); labelaSkladiste.innerText = "Skladište";
            forma.appendChild(labelaSkladiste);
            forma.appendChild(document.createElement('br'));
            forma.appendChild(selectSkladiste);
            forma.appendChild(document.createElement('br'));
            forma.appendChild(document.createElement('br'));

            let submit = document.createElement('input');
            submit.type = 'submit'; submit.className = 'submitDugme';
            submit.value = 'Pošalji';
            forma.appendChild(submit);

            formaDiv.appendChild(forma);
            renderData(formaDiv, this.Node.querySelector('.platno'));

        } catch(ex) { formatError(ex); }

    }

    // === forma za informacije o konfiguraciji ===
    async renderInfoKonfiguraciju() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let ime = inputIme.value;
            if(!ime) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Configuration/GetConfiguration/Name/${ime}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                let procesor = new Processor(
                    res.cpu.id,
                    res.cpu.serialNumber,
                    res.cpu.manufacturer,
                    res.cpu.model,
                    res.cpu.price,
                    res.cpu.frequencyGHz,
                    res.cpu.cores
                );

                let graficka = new GraphicsCard(
                    res.gpu.id,
                    res.gpu.serialNumber,
                    res.gpu.manufacturer,
                    res.gpu.model,
                    res.gpu.price,
                    res.gpu.memoryGB
                );

                let maticna = new Motherboard(
                    res.mb.id,
                    res.mb.serialNumber,
                    res.mb.manufacturer,
                    res.mb.model,
                    res.mb.price
                );

                let ram = new RAM(
                    res.ram.id,
                    res.ram.serialNumber,
                    res.ram.manufacturer,
                    res.ram.model,
                    res.ram.price,
                    res.ram.memoryGB,
                    res.ram.frequencyMHz
                );

                let skladiste = new Storage(
                    res.storage.id,
                    res.storage.serialNumber,
                    res.storage.manufacturer,
                    res.storage.model,
                    res.storage.price,
                    res.storage.memoryGB
                );

                let infoKonfig = new Configuration(
                    res.id,
                    res.name,
                    procesor,
                    graficka,
                    ram,
                    maticna,
                    skladiste,
                    this.Node.querySelector('.platno')
                );

                infoKonfig.render();
            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaIme = document.createElement('label'); labelaIme.innerText = "Naziv konfiguracije";
        let inputIme = document.createElement('input');
        inputIme.maxLength = 64;
        forma.appendChild(labelaIme);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputIme);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za uredjivanje kupca ===
    async renderUrediKupca() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let regexJmbg = new RegExp('^[1-9][0-9]{12}$'); //moze staticki posto je isti
            if(regexJmbg.test(jmbg) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Customer/GetCustomer/JMBG/${jmbg}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                let infoKupac = new Customer(
                    res.id,
                    res.jmbg,
                    res.name,
                    res.middleName,
                    res.surname,
                    res.gender,
                    res.configurations,
                    res.contacts,
                    this.Node.querySelector('.platno')
                );
                console.log(infoKupac);

                await this.renderUrediKupcaDrugiDeo(infoKupac);
            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = "JMBG";
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }
    async renderUrediKupcaDrugiDeo(kupac) {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.method = 'post';
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let name = inputIme.value;
            let middle = inputSrednjeSlovo.value;
            let surname = inputPrezime.value;
            let gender = inputPol.value;

            let obj = { id: kupac.ID, jmbg, name, middleName: middle, surname, gender }

            if(Customer.validacija(jmbg, name, middle, surname, gender) == false) { formatError("Validacija neuspešna"); return; }

            try {
                let res1 = await fetch('https://localhost:5001/Customer/UpdateCustomer', {
                    method: 'put',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(obj)
                });
                if(!res1.ok) { await formatErrorResponse(res1); return; }

                alert("Uspešno!");

                await this.reloadData();
                renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));
            } catch(ex) { formatError(ex); }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = 'JMBG';
        let inputJMBG = document.createElement('input'); inputJMBG.value = kupac.JMBG; inputJMBG.disabled = true;
        inputJMBG.maxLength = 13;
        inputJMBG.name = 'jmbg';
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));

        let labelaIme = document.createElement('label'); labelaIme.innerText = 'Ime';
        let inputIme = document.createElement('input'); inputIme.value = kupac.Name;
        inputIme.maxLength = 32;
        inputIme.name = 'name';
        forma.appendChild(labelaIme);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputIme);
        forma.appendChild(document.createElement('br'));

        let labelaSrednjeSlovo = document.createElement('label'); labelaSrednjeSlovo.innerText = 'Srednje slovo';
        let inputSrednjeSlovo = document.createElement('input'); if(kupac.MiddleName) { inputSrednjeSlovo.value = kupac.MiddleName; }
        inputSrednjeSlovo.maxLength = 2;
        inputSrednjeSlovo.name = 'middleName';
        forma.appendChild(labelaSrednjeSlovo);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputSrednjeSlovo);
        forma.appendChild(document.createElement('br'));

        let labelaPrezime = document.createElement('label'); labelaPrezime.innerText = 'Prezime';
        let inputPrezime = document.createElement('input'); inputPrezime.value = kupac.Surname;
        inputPrezime.maxLength = 32;
        inputPrezime.name = 'surname';
        forma.appendChild(labelaPrezime);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPrezime);
        forma.appendChild(document.createElement('br'));

        let labelaPol = document.createElement('label'); labelaPol.innerText = 'Pol';
        let inputPol = document.createElement('select'); inputPol.name = 'gender';
        let opcija1 = document.createElement('option'); opcija1.innerText = 'Odaberite pol'; opcija1.value = 'invalid';
        let opcija2 = document.createElement('option'); opcija2.innerText = 'M'; opcija2.value = 'M';
        let opcija3 = document.createElement('option'); opcija3.innerText = 'Ž'; opcija3.value = 'Ž';
        inputPol.appendChild(opcija1); inputPol.appendChild(opcija2); inputPol.appendChild(opcija3);
        forma.appendChild(labelaPol);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputPol);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        inputPol.selectedIndex = (kupac.Gender == "M") ? 1 : 2;

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za brisanje konfiguracije ===
    async renderObrisiKonfiguraciju() {
        if(!this.Node) { formatError("Null node!"); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let ime = inputIme.value;
            if(!ime) { formatError('Validacija neuspešna!'); return; }

            try {
                let res = await fetch(`https://localhost:5001/Configuration/GetConfiguration/Name/${ime}`);
                if(!res.ok) { await formatErrorResponse(res); return; }
                res = await res.json();

                res = await fetch(`https://localhost:5001/Configuration/DeleteConfiguration/ID/${res.id}`, {
                    method: 'delete'
                });
                if(!res.ok) { await formatErrorResponse(res); return; }

                alert("Uspešno!");
                renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));
            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaIme = document.createElement('label'); labelaIme.innerText = "Naziv konfiguracije";
        let inputIme = document.createElement('input');
        inputIme.maxLength = 64;
        forma.appendChild(labelaIme);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputIme);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }

    // === forma za dodavanje kontakta radniku/kupcu ===
    async renderDodajKontakt(tip) {
        if(!this.Node) { formatError('Null node!'); return; }

        let formaDiv = document.createElement('div'); formaDiv.className = 'forma';
        let forma = document.createElement('form');
        forma.appendChild(document.createElement('br'));
        forma.onsubmit = async (ev) => {
            ev.preventDefault();

            let jmbg = inputJMBG.value;
            let kontakt = inputKontakt.value;
            let regexJmbg = new RegExp('^[1-9][0-9]{12}$');
            if(regexJmbg.test(jmbg) == false && kontakt && kontakt.length < 65) { formatError('Validacija neuspešna!'); return; }

            try {

                if(tip === 'radnik') {

                    let res = await fetch(`https://localhost:5001/Vendor/GetVendor/JMBG/${jmbg}`);
                    if(!res.ok) { await formatErrorResponse(res); return; }
                    res = await res.json();

                    let vendorID = res.id;
                    res = await fetch(`https://localhost:5001/VendorContacts/AddVendorContact/${vendorID}/${kontakt}`, {
                        method: 'post',
                        body: JSON.stringify({})
                    });

                    if(!res.ok) { await formatErrorResponse(res); return; }

                } else if(tip === 'kupac') {

                    let res = await fetch(`https://localhost:5001/Customer/GetCustomer/JMBG/${jmbg}`);
                    if(!res.ok) { await formatErrorResponse(res); return; }
                    res = await res.json();

                    let customerID = res.id;
                    res = await fetch(`https://localhost:5001/CustomerContacts/AddCustomerContact/${customerID}/${kontakt}`, {
                        method: 'post',
                        body: JSON.stringify({})
                    });

                    if(!res.ok) { await formatErrorResponse(res); return; }

                } else { formatError("Nepoznat tip"); return; }

                alert("Uspešno!");
                renderData(this.returnDobrodosli(), this.Node.querySelector('.platno'));
            } catch(ex) {
                formatError(ex);
            }
        }

        let labelaJMBG = document.createElement('label'); labelaJMBG.innerText = "JMBG";
        let inputJMBG = document.createElement('input');
        inputJMBG.maxLength = 13;
        forma.appendChild(labelaJMBG);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputJMBG);
        forma.appendChild(document.createElement('br'));

        let labelaKontakt = document.createElement('label'); labelaKontakt.innerText = "Kontakt";
        let inputKontakt = document.createElement('input');
        inputKontakt.maxLength = 64;
        forma.appendChild(labelaKontakt);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(inputKontakt);
        forma.appendChild(document.createElement('br'));
        forma.appendChild(document.createElement('br'));

        let submit = document.createElement('input');
        submit.type = 'submit'; submit.className = 'submitDugme';
        submit.value = 'Pošalji';
        forma.appendChild(submit);

        formaDiv.appendChild(forma);
        renderData(formaDiv, this.Node.querySelector('.platno'));
    }
}