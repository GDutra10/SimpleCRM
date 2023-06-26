import {useInteractionContext} from "../../../hooks/useInteraction";
import {InteractionTab} from "../../tabs/InteractionTab";
import {CustomerSearchTab} from "../../tabs/CustomerSearchTab";
import {HttpMethod, SimpleCRMWebAPI} from "../../../../infra/api/SimpleCRMWebAPI";
import {InteractionRS} from "../../../../domain/models/api/responses/InteractionRS";
import {InteractionEndpoint} from "../../../../domain/constants/EndpointConstants";
import {Logger} from "../../../../infra/logger/Logger";
import { useEffect } from "react";

export function MainNav(){
    const customerSearch = "customerSearch";
    const interactionContext = useInteractionContext();
    let idPanelLastActive = customerSearch;
    let idButtonLastActive = "btn" + customerSearch;

    useEffect(() => {
        async function getInteractionsAsync(){
            const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
            const interactionRSs = await api.executeAsync<InteractionRS[]>(HttpMethod.Get, InteractionEndpoint.Interaction, null, true);
            interactionContext.setInteractions(interactionRSs);
        }
        
        getInteractionsAsync()
            .then(_ => {
                Logger.logInfo("interactions loaded");
            })
            .catch(e => { Logger.logError("fail to get interactions!"); });

    }, []);
    
    function setActive_onClick(event: React.MouseEvent<HTMLButtonElement>, idToActive: string){
        const element = (event.target as HTMLElement);
        document.getElementById(idPanelLastActive)!.classList.remove("active");
        document.getElementById(idButtonLastActive)!.classList.remove("active");
        document.getElementById(idToActive)!.classList.add("active");
        element.classList.add("active");

        idPanelLastActive = idToActive;
        idButtonLastActive = element.id;
    }
    
    return <>
            <ul className="nav-tabs" id="mainNav">
                <li className="nav-item" key={"li_" + customerSearch}>
                    <button className="nav-link active" id={"btn" + customerSearch} onClick={e => setActive_onClick(e, customerSearch)}>Search</button>
                </li>
                {interactionContext.interactions.length > 0 
                    ? interactionContext.interactions.map(i => {
                            return <li className="nav-item" key={"li_"+i.id}>
                                <button className="nav-link" id={"btn" + i.id} onClick={e => setActive_onClick(e, i.id)}>{i.customer.name}</button>
                            </li>
                        }) 
                    : <></>
                }
            </ul>
            <div className="nav-content">
                <CustomerSearchTab id={customerSearch} ></CustomerSearchTab>
                {interactionContext.interactions.length > 0
                    ? interactionContext.interactions.map(i => {
                        return <div key={i.id}>
                            <InteractionTab interactionId={i.id}/>
                        </div>
                    })
                    : <></>
                }
            </div>
        </>
}