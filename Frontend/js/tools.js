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

//nasumicno iz nekog niza
const pickRandomFromArray = (array) => {
    if(!array) { return; }
    return array[Math.floor(Math.random() * array.length)];
}

//http response => err message
const formatErrorResponse = async (res) => {
    alert(`Kod: ${res.status}\nPoruka: ${await res.text()}`);
}

//err message
const formatError = (errorMessage) => {
    alert(`Greska: ${errorMessage}`);
}

export {
    fetchData,
    renderData,
    pickRandomFromArray,
    formatError,
    formatErrorResponse
}