import {useNavigate, useParams} from "react-router-dom";
import {useInteractionContext} from "../../hooks/useInteraction";
import Control, {SelectOption} from "../../components/common/Control/Index";
import {useEffect, useState} from "react";
import {EnvironmentHelper} from "../../../domain/helpers/EnvironmentHelper";
import {InteractionState} from "../../../domain/models/api/InteractionState";
import {HttpMethod, SimpleCRMWebAPI} from "../../../infra/api/SimpleCRMWebAPI";
import {ProductSearchRS} from "../../../domain/models/api/responses/ProductSearchRS";
import {ProductSearchRQ} from "../../../domain/models/api/requests/ProductSearchRQ";
import {InteractionEndpoint, ProductEndpoint} from "../../../domain/constants/EndpointConstants";
import {InteractionRS} from "../../../domain/models/api/responses/InteractionRS";
import {InteractionFinishRQ} from "../../../domain/models/api/requests/InteractionFinishRQ";

function Interaction(){
    const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
    const navigate = useNavigate();
    const params = useParams();
    const customerId = params.customerId;
    const { interaction, setInteraction } = useInteractionContext();
    const customer = interaction?.customer ?? null;
    let [name, setName] = useState<string>(customer?.name ?? "");
    let [email, setEmail] = useState<string>(customer?.email ?? "");
    let [telephone, setTelephone] = useState<string>(customer?.telephone ?? "");
    let [state, setState] = useState<string>(customer?.state ?? "");
    let [productSearchRS, setProductSearchRS] = useState<ProductSearchRS | null>(null);
    let shouldRedirect = false;
    let productLoaded = false;
    
    useEffect(() => {
        loadProducts_onLoad()
            .then(productSearchRS => { 
                console.log("product loaded");
                setProductSearchRS(productSearchRS);
            });
        
        if (shouldRedirect)
            navigate(-1);
    }, []);
    
    if (!interaction || !customer){
        let message = "invalid interaction, redirecting to home..";

        alert(message);
        shouldRedirect = true;
        setInteraction(null);
        
        return <>{message}</>
    }
    
    async function loadProducts_onLoad(){
        const productSearchRQ : ProductSearchRQ = { onlyActive: true, pageSize: 50, pageNumber: 0};
        const query = new URLSearchParams({onlyActive: ""+productSearchRQ.onlyActive, pageNumber: productSearchRQ.pageNumber.toString(), pageSize: productSearchRQ.pageSize.toString()}).toString();
        
        return await api.executeAsync<ProductSearchRS>(HttpMethod.Get, `${ProductEndpoint.Products}?${query}`, null, true);
    }

    async function finish_onClick(event: React.MouseEvent<HTMLButtonElement>){
        const button = event.target as HTMLButtonElement;
        let shouldRedirect = false;
        button.disabled = true;
        
        const interactionFinishRQ: InteractionFinishRQ = { 
            interactionId: interaction!.id,
            state: InteractionState[state as keyof typeof InteractionState],
            customerProps: {
                name: name,
                email: email,
                telephone: telephone
            }
        };
        const interactionRS = await api.executeAsync<InteractionRS>(HttpMethod.Put, InteractionEndpoint.Finish, interactionFinishRQ, true);
        
        if (interactionRS.error){
            alert(interactionRS.error);
        } else if (interactionRS.validations && interactionRS.validations.length > 0){
            // TODO: show in modal
            alert(interactionRS.validations[0].message);
        } else {
            shouldRedirect = true;
        }
    
        button.disabled = false;
        
        if (shouldRedirect){
            navigate("/");
            setInteraction(null);    
        }
    }
    
    async function addOrder_onChecked(event: React.MouseEvent<HTMLButtonElement>, productId: string){
        // TODO: add products
        alert(`add order item with ProductId: "${productId}"!`);
    }
    
    return <>
        <h1>Interaction</h1>
        <h2>{customer!.name}</h2>
        {EnvironmentHelper.isDev() ? <>CustomerID: {customer!.id}<br/></> : <></>}
        {EnvironmentHelper.isDev() ? <>InteractionID: {interaction!.id}<br/></> : <></>}
        <div className="form">
            <Control label="Name" value={name} onChange={async e => setName(e.target.value)}></Control>
            <Control label="Email" value={email} onChange={async e => setEmail(e.target.value)}></Control>
            <Control label="Telephone" value={telephone} onChange={async e => setTelephone(e.target.value)}></Control>
            <Control label="State" value={state} type="select" selectOptions={getOptions()} onChange={async e => setState(e.target.value)}></Control>

            <div className="products">
                <h3>Products</h3>
                {productSearchRS === null || productSearchRS === undefined ? <label>loading products...</label> 
                    : productSearchRS.error && productSearchRS.error !== "" ? <>{productSearchRS.error}</> 
                        : productSearchRS.validations && productSearchRS.validations.length > 0 ? <>{productSearchRS.validations[0].message}</> 
                            :<>
                                <table>
                                    <thead>
                                    <tr>
                                        <th>Product</th>
                                        <th></th>
                                    </tr>
                                    </thead>
                                    <tbody>
                                    {productSearchRS.records.map(product => {
                                        return <tr key={product.id}>
                                            <td>{product.name}</td>
                                            <td><button className="btn btn-success" onClick={async e => await addOrder_onChecked(e, product.id)}>+</button></td>
                                        </tr>})
                                    }
                                    </tbody>
                                </table>
                            </>
                }
            </div>
            <div className="order-items">
                <h4>Order Items</h4>
                TODO: add order items
            </div>
            <div className="actions">
                <button className="btn btn-primary" onClick={async event => await finish_onClick(event)}>Finish</button>
            </div>
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