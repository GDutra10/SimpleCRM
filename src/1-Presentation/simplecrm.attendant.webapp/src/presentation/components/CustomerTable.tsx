import {useInteractionContext} from "../hooks/useInteraction";
import { useNavigate } from "react-router-dom";
import {HttpMethod, SimpleCRMWebAPI} from "../../infra/api/SimpleCRMWebAPI";
import {InteractionRS} from "../../domain/models/api/responses/InteractionRS";
import {CustomerSearchRS} from "../../domain/models/api/responses/CustomerSearchRS";
import {InteractionEndpoint} from "../../domain/constants/EndpointConstants";

function CustomerTable(props: Props) {
    const navigate = useNavigate();
    let { setInteraction} = useInteractionContext();
    
    if (!props.customerSearchRS)
        return <></>
    
    if (!props.customerSearchRS.records)
        return <>{props.customerSearchRS.error}</>
        
    if (props.customerSearchRS.records.length <= 0)
        return <>No data to show!</>

    async function startInteraction_Click(event: React.MouseEvent<HTMLButtonElement>, customerId: string){
        const button = event.target as HTMLButtonElement;    
        const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
        const query = new URLSearchParams({CustomerId: customerId}).toString();
        const interactionRS = await api.executeAsync<InteractionRS>(HttpMethod.Post, `${InteractionEndpoint.Start}?${query}`, null, true);

        button.disabled = true;
        
        if (interactionRS.error && interactionRS.error.length > 0){
            alert(interactionRS.error);
            button.disabled = false;
            return;
        }

        if (interactionRS.validations && interactionRS.validations.length > 0){
            alert(interactionRS.validations[0].message);
            button.disabled = false;
            return;
        }

        setInteraction(interactionRS);
        button.disabled = false;
        navigate(`/interaction/${interactionRS.customerId}`);
    }
    
    return <table className="table">
        <thead>
        <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Telephone</th>
            <th>State</th>
            <th>Creation Time</th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {props.customerSearchRS.records.map(customerRS => {
            return (
                <tr key={customerRS.id}>
                    <td>{customerRS.name}</td>
                    <td>{customerRS.email}</td>
                    <td>{customerRS.telephone}</td>
                    <td>{customerRS.state}</td>
                    <td>{customerRS.creationTime.toString()}</td>
                    <td>
                        <button className="btn btn-success" onClick={async event => startInteraction_Click(event, customerRS.id)}>Interact
                        </button>
                    </td>
                </tr>
            );
        })}
        </tbody>
    </table>
}

export default CustomerTable;

type Props = {
    customerSearchRS: CustomerSearchRS | null 
}