import { fetchData, formatError, renderData } from "../tools.js";
import GraphicsCard from "./parts/graphicsCard.js";
import Motherboard from "./parts/motherboard.js";
import Processor from "./parts/processor.js";
import RAM from "./parts/ram.js";
import Storage from "./parts/storage.js";

export default class Configuration {
    constructor(ID, Name, CPU, GPU, RAM, MB, STORAGE, Node) {
        this.ID = ID;
        this.Name = Name;
        this.CPU = CPU;
        this.GPU = GPU;
        this.RAM = RAM;
        this.MB = MB;
        this.STORAGE = STORAGE;

        this.Node = Node;
    }

    //! validacija preko ID-jeva
    static validacija = (name, cpu, gpu, ram, mb, storage) => {
        if(!name || !cpu || !gpu || !ram || !mb || !storage) { return false; }
        if(name.length > 64 || cpu < 0 || gpu < 0 || ram < 0 || mb < 0 || storage < 0) { return false; }

        return true;
    }

    static slikaKompanije = (kompanija) => {
        let kompanije = [ //moze staticki
            'amd',
            'asrock',
            'asus',
            'biostar',
            'corsair',
            'gigabyte',
            'intel',
            'kingston',
            'msi',
            'nvidia',
            'patriot',
            'samsung',
            'seagate',
            'toshiba',
            'westerndigital'
        ];

        kompanija = kompanija.toLowerCase();
        if(kompanija == 'western digital') { kompanija = 'westerndigital'; }

        let url = '';
        if(kompanije.includes(kompanija) == true) {
            url = `/Frontend/img/kompanije/${kompanija}.jpg`;
        } else {
            url = '/Frontend/img/kompanije/default.jpg';
        }
        return url;
    }

    async reload() {
        if(!this.Name) { return; }
        await this.fetchConfiguration();
        this.render();
    }

    async fetchConfiguration() {
        try {
            const data = await fetchData(`https://localhost:5001/Configuration/GetConfiguration/Name/${this.Name}`);

            ['ID', 'Name'].forEach((i) => { this[i] == data[i.toLowerCase()]; });

            this.CPU = new Processor(
                data.cpu.id,
                data.cpu.serialNumber,
                data.cpu.manufacturer,
                data.cpu.model,
                data.cpu.price,
                data.cpu.frequencyGHz,
                data.cpu.cores
            );

            this.GPU = new GraphicsCard(
                data.gpu.id,
                data.gpu.serialNumber,
                data.gpu.manufacturer,
                data.gpu.model,
                data.gpu.price,
                data.gpu.memoryGB
            );

            this.MB = new Motherboard(
                data.mb.id,
                data.mb.serialNumber,
                data.mb.manufacturer,
                data.mb.model,
                data.mb.price
            );

            this.RAM = new RAM(
                data.ram.id,
                data.ram.serialNumber,
                data.ram.manufacturer,
                data.ram.model,
                data.ram.price,
                data.ram.memoryGB,
                data.ram.frequencyMHz
            );

            this.STORAGE = new Storage(
                data.storage.id,
                data.storage.serialNumber,
                data.storage.manufacturer,
                data.storage.model,
                data.storage.price,
                data.storage.memoryGB
            );

        } catch(ex) { formatError(ex); }
    }

    udeoCene() {
        let cene = [];
        let ukupnaCena = this.CPU.Price + this.GPU.Price + this.MB.Price + this.STORAGE.Price + this.RAM.Price;

        cene.push({ komponenta: 'Procesor', udeo: this.CPU.Price / ukupnaCena });
        cene.push({ komponenta: 'Grafička', udeo: this.GPU.Price / ukupnaCena });
        cene.push({ komponenta: 'RAM', udeo: this.RAM.Price / ukupnaCena });
        cene.push({ komponenta: 'Matična', udeo: this.MB.Price / ukupnaCena });
        cene.push({ komponenta: 'Skladište', udeo: this.STORAGE.Price / ukupnaCena });

        return cene;
    }

    render() {
        if(!this.Node) { formatError('Null node!'); return; }

        let final = document.createElement('div'); final.className = 'platnoKonfiguracija';
        final.appendChild(document.createElement('br'));

        let naslovDiv = document.createElement('div'); naslovDiv.className = 'konfiguracijaNaslov';
        naslovDiv.innerText = `Konfiguracija: ${this.Name}`;
        final.appendChild(naslovDiv);
        final.appendChild(document.createElement('br'));

        let komponenteDiv = document.createElement('div'); komponenteDiv.className = 'flex_div';

        komponenteDiv.appendChild(this.CPU.returnRendered());
        komponenteDiv.appendChild(this.GPU.returnRendered());
        komponenteDiv.appendChild(this.RAM.returnRendered());
        komponenteDiv.appendChild(this.MB.returnRendered());
        komponenteDiv.appendChild(this.STORAGE.returnRendered());

        final.append(komponenteDiv);


        final.append(document.createElement('br'));
        let poredjenjeDiv = document.createElement('div'); poredjenjeDiv.className = 'poredjenjeCena';

        let naslovCene = document.createElement('div'); naslovCene.className = 'naslovCene'; naslovCene.innerText = 'Udeo cena';
        poredjenjeDiv.appendChild(naslovCene);

        let deloviCene = this.udeoCene();
        deloviCene.forEach((i) => {
            let udeoProcenat = Math.round(i.udeo * 100);

            let cenaDiv = document.createElement('div'); cenaDiv.className = 'cenaDiv';
            let procenatDiv = document.createElement('div'); procenatDiv.className = 'procenatDiv'; procenatDiv.innerText = `${i.komponenta} - ${udeoProcenat}%`;
            procenatDiv.setAttribute('style', `width: ${udeoProcenat}%;`);

            cenaDiv.appendChild(procenatDiv);

            poredjenjeDiv.append(cenaDiv);
            poredjenjeDiv.append(document.createElement('br'));
        });

        final.appendChild(poredjenjeDiv);

        renderData(final, this.Node);
    }
}