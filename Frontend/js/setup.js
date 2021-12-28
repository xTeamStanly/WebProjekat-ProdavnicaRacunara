import { firstStore, secondStore, thirdStore } from "./data.js";
import { fetchData, formatError } from "./tools.js";

const setup = async () => {
    try {

        const prodavniceOpcije = await fetchData("https://localhost:5001/Store/GetStores");
        if(!prodavniceOpcije || prodavniceOpcije.length < 1) { formatError("Nema prodavnica!"); return; }

        //glavni div
        let glavniDiv = document.createElement('div'); glavniDiv.className = 'glavni';
        document.body.appendChild(glavniDiv);

        // === prva prodavnica ==
        //prvi div
        let prviDiv =  document.createElement('div'); prviDiv.className = 'kontejner';
        firstStore.Node = prviDiv;

        //prvi select
        let prviSelectDiv = document.createElement('div'); prviSelectDiv.className = 'select_div';
        prviDiv.appendChild(prviSelectDiv);

        let prviSelect = document.createElement('select'); prviSelectDiv.appendChild(prviSelect);

        let prviSelectPrvaOpcija = document.createElement('option');
        prviSelectPrvaOpcija.innerText = 'Odaberite prvu prodavnicu';
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

        // === linija ===
        let linija = document.createElement('hr'); linija.className = 'linija';
        glavniDiv.appendChild(linija);

        // === druga prodavnica ===
        //drugi div
        let drugiDiv = document.createElement('div'); drugiDiv.className = 'kontejner';
        secondStore.Node = drugiDiv;

        //drugi select
        let drugiSelectDiv = document.createElement('div'); drugiSelectDiv.className = 'select_div';
        drugiDiv.appendChild(drugiSelectDiv);

        let drugiSelect = document.createElement('select'); drugiSelectDiv.appendChild(drugiSelect);

        let drugiSelectPrvaOpcija = document.createElement('option');
        drugiSelectPrvaOpcija.innerText = 'Odaberite drugu prodavnicu';
        drugiSelect.options.add(drugiSelectPrvaOpcija);

        prodavniceOpcije.forEach((prodavnicaJson) => {
            let opcija = document.createElement('option');
            opcija.value = prodavnicaJson.id;
            opcija.innerText = prodavnicaJson.name;
            drugiSelect.options.add(opcija);
        });

        drugiSelect.onchange = async (ev) => {
            let ID = drugiSelect.options[drugiSelect.selectedIndex].value;
            if(ID < 1) { return; }

            drugiSelect.remove(); //posle odabira izbrisi select
            drugiSelectDiv.remove();

            secondStore.ID = ID;
            await secondStore.reload();
        }

        glavniDiv.appendChild(drugiDiv);

        // === linija ===
        let linija2 = document.createElement('hr'); linija2.className = 'linija';
        glavniDiv.appendChild(linija2);

        // === treca prodavnica ===
        //treci div
        let treciDiv = document.createElement('div'); treciDiv.className = 'kontejner';
        thirdStore.Node = treciDiv;

        //treci select
        let treciSelectDiv = document.createElement('div'); treciSelectDiv.className = 'select_div';
        treciDiv.appendChild(treciSelectDiv);

        let treciSelect = document.createElement('select'); treciSelectDiv.appendChild(treciSelect);

        let treciSelectPrvaOpcija = document.createElement('option');
        treciSelectPrvaOpcija.innerText = 'Odaberite treću prodavnicu';
        treciSelect.options.add(treciSelectPrvaOpcija);

        prodavniceOpcije.forEach((prodavnicaJson) => {
            let opcija = document.createElement('option');
            opcija.value = prodavnicaJson.id;
            opcija.innerText = prodavnicaJson.name;
            treciSelect.options.add(opcija);
        });

        treciSelect.onchange = async (ev) => {
            let ID = treciSelect.options[treciSelect.selectedIndex].value;
            if(ID < 1) { return; }

            treciSelect.remove(); //posle odabira izbrisi select
            treciSelectDiv.remove();

            thirdStore.ID = ID;
            await thirdStore.reload();
        }

        glavniDiv.appendChild(treciDiv);


    } catch(ex) {
        formatError(ex);
    }
};

export { setup }