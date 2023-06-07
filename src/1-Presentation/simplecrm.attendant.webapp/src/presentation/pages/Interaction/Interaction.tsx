import {useNavigate, useParams } from "react-router-dom";
import {useInteractionContext} from "../../hooks/useInteraction";
import Control, {SelectOption} from "../../components/common/Control/Index";
import {useEffect, useState } from "react";
import {EnvironmentHelper} from "../../../domain/helpers/EnvironmentHelper";
import {InteractionState} from "../../../domain/models/api/InteractionState";

function Interaction(){
    const navigate = useNavigate();
    const params = useParams();
    const customerId = params.customerId;
    const { interaction, setInteraction } = useInteractionContext();
    const customer = interaction?.customer ?? null;
    let [name, setName] = useState<string>(customer?.name ?? "");
    let [email, setEmail] = useState<string>(customer?.email ?? "");
    let [telephone, setTelephone] = useState<string>(customer?.telephone ?? "");
    let [state, setState] = useState<string>(customer?.state ?? "");
    let shouldRedirect = false;
    
    useEffect(() => {
        if (shouldRedirect)
            navigate(-1);
    });
    
    if (!interaction || !customer){
        let message = "invalid interaction, redirecting to home..";

        alert(message);
        shouldRedirect = true;
        setInteraction(null);
        
        return <>message</>
    }
    
    // TODO: get products and set in table
    
    async function finish_onClick(event: React.MouseEvent<HTMLButtonElement>){
        // TODO: finish interaction
    }
    
    return <>
        <h1>Interaction</h1>
        <h2>{customer!.name}</h2>
        {EnvironmentHelper.isDev() ? customer!.id : <></>}
        <div className="form">
            <Control label="Name" value={name} onChange={async e => setName(e.target.value)}></Control>
            <Control label="Email" value={email} onChange={async e => setEmail(e.target.value)}></Control>
            <Control label="Telephone" value={telephone} onChange={async e => setTelephone(e.target.value)}></Control>
            <Control label="State" value={state} type="select" selectOptions={getOptions()} onChange={async e => setState(e.target.value)}></Control>
            <button className="btn btn-primary" onClick={async event => await finish_onClick(event)}>Finish</button>
        </div>
    </>;
}

function getOptions() : SelectOption[] {
    const options = new Array<SelectOption>();
    
    options.push({ key: 0, value: InteractionState.NotAvailable, label: InteractionState.NotAvailable});
    options.push({ key: 1, value: InteractionState.NotInterested, label: InteractionState.NotInterested});
    options.push({ key: 2, value: InteractionState.PreSale, label: InteractionState.PreSale});
        
    return options;
}

export default Interaction;