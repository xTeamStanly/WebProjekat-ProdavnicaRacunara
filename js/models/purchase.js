export default class Purchase {
    constructor(ID, Customer, Vendor, Configuration, Date, PaymentType) {
        this.ID = ID;
        this.Customer = Customer;
        this.Vendor = Vendor;
        this.Configuration = Configuration;
        this.Date = Date;
        this.PaymentType = PaymentType;
    }
}