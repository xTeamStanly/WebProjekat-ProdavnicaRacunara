import { fetchData, renderData } from "../../tools.js";

export default class Vendor {
    constructor(ID, JMBG, Name, MiddleName, Surname, Gender, Salary, BirthDate, Address, Contacts, VendorPurchases, Node) {
        this.ID = ID;
        this.JMBG = JMBG;
        this.Name = Name;
        this.MiddleName = MiddleName;
        this.Surname = Surname;
        this.Gender = Gender;
        this.Salary = Salary;
        this.BirthDate = BirthDate;
        this.Address = Address;
        this.Contacts = Contacts;
        this.VendorPurchases = VendorPurchases;
        this.Node = Node;
    }


    async fetchVendor() {
        if(!this.ID) { return; }

        const fetched = await fetchData(`https://localhost:5001/Vendor/GetVendor/ID/${this.ID}`);

        ['JMBG', 'Name', 'MiddleName', 'Surname', 'Gender', 'Salary', 'BirthDate', 'Address', 'Contacts'].forEach((i) => { this[i] = fetched[i.toLowerCase()]; });
    }

    draw() {

        let table = document.createElement('table');
        let tableHeaders = document.createElement('tr');
        ['Property', 'Value'].forEach((i) => {
            let kolona = document.createElement('th');
            kolona.innerText = i;
            table.appendChild(kolona);
        });
        table.appendChild(tableHeaders);

        ['JMBG', 'Name', 'MiddleName', 'Surname', 'Gender', 'Salary', 'BirthDate', 'Address', 'Contacts'].forEach((i) => {
            let red = document.createElement('tr');
            let left = document.createElement('td'); left.innerText = i;

            let rightData = this[i];
            if(rightData === undefined) { rightData = "/"; }

            let right = document.createElement('td');

            if(i === 'Contacts') {
                right.innerText = rightData.toString().split(',').join('\n');
            } else {
                right.innerText = rightData;
            }
            red.appendChild(left);
            red.appendChild(right);

            table.appendChild(red);
        });

        renderData(table, this.Node);

    }
}