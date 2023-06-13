import {useState} from "react";
import Control from "../../components/common/Control/Index";
import CustomerAddModal from "../../components/CustomerAddModal";
import CustomerTable from "../../components/CustomerTable";
import {CustomerSearchRS} from "../../../domain/models/api/responses/CustomerSearchRS";
import {HttpMethod, SimpleCRMWebAPI} from "../../../infra/api/SimpleCRMWebAPI";
import {CustomerSearchRQ} from "../../../domain/models/api/requests/CustomerSearchRQ";
import {CustomerEndpoint} from "../../../domain/constants/EndpointConstants";

import './Index.css';

function Home(){
    let [name, setName] = useState<string>("");
    let [email, setEmail] = useState<string>("");
    let [telephone, setTelephone] = useState<string>("");
    let [customerSearchRS, setCustomerSearchRS] = useState<CustomerSearchRS | null>(null);
    const modalId = "modal" + Date.now().toString();
    
    async function searchCustomers(button: HTMLButtonElement){
        const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
        const customerSearchRQ : CustomerSearchRQ = {
            name: name,
            email: email,
            telephone: telephone,
            pageNumber: 0,
            pageSize: 50
        };
        const query = new URLSearchParams({...customerSearchRQ, pageNumber: customerSearchRQ.pageNumber.toString(), pageSize: customerSearchRQ.pageSize.toString()}).toString();
        const customerSearchRS = await api.executeAsync<CustomerSearchRS>(HttpMethod.Get, `${CustomerEndpoint.Customers}?${query}`, null, true);
        
        setCustomerSearchRS(customerSearchRS);
    }
    
    return (
        <>
            <h1>Customers</h1>
            <div>
                <div className="form">
                    <Control label="Name" value={name} onChange={async e => setName(e.target.value)}></Control>
                    <Control label="Email" value={email} onChange={async e => setEmail(e.target.value)}></Control>
                    <Control label="Telephone" value={telephone} onChange={async e => setTelephone(e.target.value)}></Control>
                    <button className="btn btn-primary" onClick={event => searchCustomers(event.target as HTMLButtonElement)}>Search</button>
                    <button className="btn btn-success" onClick={event => document.getElementById(modalId)!.style.display = "block"}>Add</button>
                </div>
                <div>
                    <CustomerTable  customerSearchRS={customerSearchRS}/>
                </div>
                <CustomerAddModal id={modalId}></CustomerAddModal>
            </div>
        </>
    );
}

export default Home;