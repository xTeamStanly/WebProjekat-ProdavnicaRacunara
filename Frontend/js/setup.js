import { firstStore } from "./data.js";
import { fetchData, formatError } from "./tools.js";

const setup = async () => {
    try {

        const prodavniceOpcije = await fetchData("https://localhost:5001/Store/GetStores");
        if(!prodavniceOpcije || prodavniceOpcije.length < 1) { formatError("Nema prodavnica!"); return; }

        //glavni div
        let glavniDiv = document.createElement('div'); glavniDiv.className = 'glavni';
        document.body.appendChild(glavniDiv);

        //prvi div
        let prviDiv =  document.createElement('div'); prviDiv.className = 'kontejner';
        firstStore.Node = prviDiv;

        //prvi select
        let prviSelectDiv = document.createElement('div'); prviSelectDiv.className = 'select_div';
        prviDiv.appendChild(prviSelectDiv);

        let prviSelect = document.createElement('select'); prviSelectDiv.appendChild(prviSelect);

        let prviSelectPrvaOpcija = document.createElement('option');
        prviSelectPrvaOpcija.innerText = 'Odaberite prodavnicu';
        prviSelect.options.add(prviSelectPrvaOpcija);

        prodavniceOpcije.forEach((prodavnicaJson) => {
            let opcija = document.createElement('option');
            opcija.value = prodavnicaJson.id;
            opcija.innerText = prodavnicaJson.name;
            prviSelect.options.add(opcija);
        });

        prviSelect.onchange = async (ev) => {
            let ID = prviSelect.options[prviSelect.selectedIndex].value;
            if(ID < 1) { return; }

            prviSelect.remove(); //posle odabira izbrisi select
            prviSelectDiv.remove();

            firstStore.ID = ID;
            await firstStore.reload();
        }

        glavniDiv.appendChild(prviDiv);

        //todo dodaj drugu prodavnicu

    } catch(ex) {
        formatError(ex);
    }
};

export { setup }