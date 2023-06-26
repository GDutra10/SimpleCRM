import Control, {Size} from "../../common/Control";
import CustomerTable from "../../CustomerTable";
import CustomerAddModal from "../../CustomerAddModal";
import {CustomerSearchRS} from "../../../../domain/models/api/responses/CustomerSearchRS";
import {HttpMethod, SimpleCRMWebAPI} from "../../../../infra/api/SimpleCRMWebAPI";
import {CustomerSearchRQ} from "../../../../domain/models/api/requests/CustomerSearchRQ";
import {CustomerEndpoint} from "../../../../domain/constants/EndpointConstants";
import { useState } from "react";

export function CustomerSearchTab(props: Props){
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
    
    return <div id={props.id} className="tab-content active">
        <h1>Search Customers</h1>
        <div>
            <div className="form">
                <Control label="Name" value={name} size={Size.Size5} onChange={async e => setName(e.target.value)}></Control>
                <Control label="Email" value={email} size={Size.Size5} onChange={async e => setEmail(e.target.value)}></Control>
                <Control label="Telephone" value={telephone} size={Size.Size2} onChange={async e => setTelephone(e.target.value)}></Control>
            </div>
            <div className="row actions">
                <button className="btn btn-primary" onClick={event => searchCustomers(event.target as HTMLButtonElement)}>Search</button>
                <button className="btn btn-success" onClick={event => document.getElementById(modalId)!.style.display = "block"}>Add</button>
            </div>
            <div className="row">
                <CustomerTable  customerSearchRS={customerSearchRS}/>
            </div>
            <CustomerAddModal id={modalId}></CustomerAddModal>
        </div>
    </div>
}

type Props = {
    id: string
}