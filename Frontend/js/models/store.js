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

        let obrisiRadnika = document.createElement('button');
        obrisiRadnika.innerText = 'Obriši globalnog radnika';
        obrisiRadnika.onclick = async (ev) => { await this.renderObrisiRadnika(); }
        radnikDiv.appendChild(obrisiRadnika);

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
        infoKupacKonfig.innerText = 'Informacije o kupcu i njegovim kupovinama';
        infoKupacKonfig.onclick = async (ev) => { /*TODO: samo prikazi kad je bila kupovina koji je naziv konfiguracije i koji clan je prodao i kad*/ console.log('info + config'); }
        kupacDiv.appendChild(infoKupacKonfig);

        //todo dodaj jos dugmica (info (alert JMBG) i kupovine (alert JMBG))

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
        infoKonfiguracija.onclick = async (ev) => { await this.renderInfoKonfiguracuja(); }
        konfiguracijaDiv.appendChild(infoKonfiguracija);

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
        this.Node.innerHtml = "";

        this.renderInfo();
        await this.renderKontrole();

        // === canvas setup ===
        let platnoDiv = this.Node.querySelector('.platno');
        if(!platnoDiv) {
            platnoDiv = document.createElement('div');
            platnoDiv.className = 'platno';

            let dobrodosliDiv = document.createElement('div');
            dobrodosliDiv.className = 'dobrodosli';
            dobrodosliDiv.innerText = '😀 Dobrodošli 😀';
            platnoDiv.appendChild(dobrodosliDiv);


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


    //todo dodaj delete and fire radnika

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

            let obj = { jmbg, name, middle, surname, gender, salary, birthDate, address, contacts: null, vendorPurchases: null }

            if(Vendor.validacija(jmbg, name, middle, surname, gender, address) == false) { formatError("Validacija neuspešna"); return; }

            try {
                let res1 = await fetch('https://localhost:5001/Vendor/AddVendor', {
                    method: 'post',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(obj)
                });
                if(!res1.ok) { await formatErrorResponse(res); return; }


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
            let address = inputAdresa.value;

            let obj = { jmbg, name, middle, surname, gender, address, configurations: null, contacts: null }

            if(Customer.validacija(jmbg, name, middle, surname, gender, address) == false) { formatError('Validacija neuspešna!'); return; }

            try {
                let res1 = await fetch('https://localhost:5001/Customer/AddCustomer', {
                    method: 'post',
                    headers: {'Content-Type': 'application/json'},
                    body: JSON.stringify(obj)
                });
                if(!res1.ok) { await formatErrorResponse(res); return; }

                alert("Uspešno!");
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

    //todo render forma za dodavanje konfiguracije
    async renderDodajKonfiguraciju() {

    }

    async renderInfoKonfiguracuja() {
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



}