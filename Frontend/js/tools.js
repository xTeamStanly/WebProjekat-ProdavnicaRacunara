import { firstStore, secondStore } from "./data.js";

//fetchuje i jsonifikuje podatke
const fetchData = async (url) => {
    let data = await fetch(url);
    data = await data.json();
    return data;
}

//crtamo 'data' na 'node', brise sadrzaj node-a
const renderData = (nodeData, nodeCanvas) => {
    nodeCanvas.innerHTML = "";
    nodeCanvas.appendChild(nodeData);
}



const firstRender = async () => {
    //resetPage();
    const opcijeZaSelect = await fetchData("https://localhost:5001/Store/GetStores");

    //main div
    let glavniDiv = document.createElement('div'); glavniDiv.id = 'main'; document.body.appendChild(glavniDiv);

    //prvi div
    let prviDiv = document.createElement('div'); prviDiv.className = 'container';
    firstStore.Node = prviDiv;

    //prvi select
    let prviSelect = document.createElement('select');
    let naslovOpcija = document.createElement('option');
    naslovOpcija.innerText = "Odaberite prodavnicu";
    prviSelect.options.add(naslovOpcija);
    prviDiv.appendChild(prviSelect);

    //prvi div dodaci
    let prviDataDiv = document.createElement('div'); prviDataDiv.className = 'list';
    let prviInfoDiv = document.createElement('div'); prviInfoDiv.className = 'info';
    prviDiv.appendChild(prviDataDiv);
    prviDiv.appendChild(prviInfoDiv);

    //drugi div
    let drugiDiv = document.createElement('div'); drugiDiv.className = 'container';
    secondStore.Node = drugiDiv;

    //drugi select
    let drugiSelect = document.createElement('select');
    let naslovOpcija2 = document.createElement('option');
    naslovOpcija2.innerText = "Odaberite prodavnicu";
    drugiSelect.options.add(naslovOpcija2);
    drugiDiv.appendChild(drugiSelect);

    //drugi div dodaci
    let drugiDataDiv = document.createElement('div'); drugiDataDiv.className = 'list';
    let drugiInfoDiv = document.createElement('div'); drugiInfoDiv.className = 'info';
    drugiDiv.appendChild(drugiDataDiv);
    drugiDiv.appendChild(drugiInfoDiv);

    //popunjavanje opcija za selektovanje
    opcijeZaSelect.forEach((jsonOpcija) => {
        let opcija = document.createElement('option');
        opcija.value = jsonOpcija.id;
        opcija.innerHTML = jsonOpcija.name;
        prviSelect.options.add(opcija);

        let opcija2 = document.createElement('option');
        opcija2.value = jsonOpcija.id;
        opcija2.innerHTML = jsonOpcija.name;
        drugiSelect.options.add(opcija2);
    });

    //prvi select event
    prviSelect.onchange = async (ev) => {
        let ID = prviSelect.options[prviSelect.selectedIndex].value;
        if(ID < 0) { return; }

        prviSelect.remove(); //izbrisi select posle prvog klika

        firstStore.ID = ID;
        await firstStore.reload();
    }

    //drugi select event
    drugiSelect.onchange = async (ev) => {
        let ID = drugiSelect.options[drugiSelect.selectedIndex].value;
        if(ID < 0) { return; }

        drugiSelect.remove(); //izbrisi select posle prvog klika

        secondStore.ID = ID;
        await secondStore.reload();
    }

    glavniDiv.appendChild(prviDiv);
    glavniDiv.appendChild(drugiDiv);
}

const createTag = (tag, tekst, klasa, id) => {
    if(!tag) { return null; }
    let elem = document.createElement(tag);

    if(tekst) { elem.innerHTML = tekst; }
    if(klasa) { elem.className = klasa; }
    if(id) { elem.id = id; }

    return elem;
}

const resetPage = () => {
    let divNodes = document.body.querySelectorAll('div');
    divNodes.forEach((node) => { node.remove(); })
}

export { fetchData, firstRender, renderData }