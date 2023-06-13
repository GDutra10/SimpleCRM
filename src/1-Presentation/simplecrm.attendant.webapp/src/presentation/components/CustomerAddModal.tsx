import {useState} from "react";
import Control from "./common/Control/Index";
import Modal from "./common/Modal/Index";
import {HttpMethod, SimpleCRMWebAPI} from "../../infra/api/SimpleCRMWebAPI";
import {CustomerRegisterRQ} from "../../domain/models/api/requests/CustomerRegisterRQ";
import {CustomerRS} from "../../domain/models/api/responses/CustomerRS";
import {CustomerEndpoint} from "../../domain/constants/EndpointConstants";

function CustomerAddModal(props: Props){
    let [name, setName] = useState<string>("");
    let [email, setEmail] = useState<string>("");
    let [telephone, setTelephone] = useState<string>("");
    let [nameValidation, setNameValidation] = useState<string>("");
    let [emailValidation, setEmailValidation] = useState<string>("");
    let [telephoneValidation, setTelephoneValidation] = useState<string>("");
    let [globalValidation, setGlobalValidation] = useState<string>("");
    
    async function registerCustomer(button: HTMLButtonElement){
        clearValidations();
        button.disabled = true;
        const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
        const customerRegisterRQ : CustomerRegisterRQ = {
            name: name,
            email: email,
            telephone: telephone
        };
        const customerRS = await api.executeAsync<CustomerRS>(HttpMethod.Post, CustomerEndpoint.Customers, customerRegisterRQ, true);
        
        if (!customerRS){
            alert("something wrong, please try again later!");
            button.disabled = false;
            return;
        }
            
        if (customerRS.error){
            alert(customerRS.error);
            button.disabled = false;
            return;
        }
        
        if (customerRS.validations && customerRS.validations?.length > 0){
            customerRS.validations.forEach(v => {
                if (v.field === "Email")
                    setEmailValidation(v.message);
                else if (v.field === "Name")
                    setNameValidation(v.message);
                else if (v.field === "Telephone")
                    setTelephoneValidation(v.message);
                else
                    setGlobalValidation(v.message);
            });
            button.disabled = false;
            
            return;
        }
        
        alert(`Customer ${customerRS.name} registered successfully!`);
        setName("");
        setEmail("");
        setTelephone("");
        setGlobalValidation("");

        button.disabled = false;
    }
    
    function clearValidations(){
        setNameValidation("");
        setEmailValidation("");
        setTelephoneValidation("");
        setGlobalValidation("");
    }
    
    return (<>
        <Modal 
            id={props.id}
            title="Customer Register"
            body={<div className="form">
                <Control label="Name" value={name} error={nameValidation} onChange={async e => setName(e.target.value)}></Control>
                <Control label="Email" value={email} error={emailValidation} onChange={async e => setEmail(e.target.value)}></Control>
                <Control label="Telephone" value={telephone} error={telephoneValidation} onChange={async e => setTelephone(e.target.value)}></Control>
                <label className="validation">{globalValidation}</label>
            </div>}
            footer={<button className="btn btn-success" onClick={event => registerCustomer(event.target as HTMLButtonElement)}>Register</button>}>
            <></>
        </Modal>
    </>);
}

export default CustomerAddModal;

type Props = {
    id: string
}