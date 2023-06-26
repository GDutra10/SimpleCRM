import {useInteractionContext} from "../hooks/useInteraction";
import {HttpMethod, SimpleCRMWebAPI} from "../../infra/api/SimpleCRMWebAPI";
import {InteractionRS} from "../../domain/models/api/responses/InteractionRS";
import {CustomerSearchRS} from "../../domain/models/api/responses/CustomerSearchRS";
import {InteractionEndpoint} from "../../domain/constants/EndpointConstants";
import {useModalContext} from "../hooks/useModalContext";
import {Logger} from "../../infra/logger/Logger";

function CustomerTable(props: Props) {
    const modalContext = useModalContext();
    const interactionContext = useInteractionContext();
    
    if (!props.customerSearchRS)
        return <></>
    
    if (!props.customerSearchRS.records)
        return <>{props.customerSearchRS.error}</>
        
    if (props.customerSearchRS.records.length <= 0)
        return <>No data to show!</>

    async function startInteraction_Click(event: React.MouseEvent<HTMLButtonElement>, customerId: string){
        Logger.logInfo("starting interaction...")
        const button = event.target as HTMLButtonElement;    
        const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
        const query = new URLSearchParams({CustomerId: customerId}).toString();
        const interactionRS = await api.executeAsync<InteractionRS>(HttpMethod.Post, `${InteractionEndpoint.Start}?${query}`, null, true);

        button.disabled = true;
        
        if (interactionRS.error && interactionRS.error.length > 0){
            modalContext.showError(interactionRS.error);
            button.disabled = false;
            return;
        }

        if (interactionRS.validations && interactionRS.validations.length > 0){
            modalContext.showValidations(interactionRS.validations);
            button.disabled = false;
            return;
        }

        let interactions= [ ...interactionContext.interactions, interactionRS ];
        interactionContext.setInteractions(interactions);
        Logger.logDebug(JSON.stringify(interactionContext.interactions));
        button.disabled = false;
    }
    
    return <>
        <span>{"Total: " + props.customerSearchRS.records.length}</span>
        <table className="table">
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
    </>
}

export default CustomerTable;

type Props = {
    customerSearchRS: CustomerSearchRS | null 
}