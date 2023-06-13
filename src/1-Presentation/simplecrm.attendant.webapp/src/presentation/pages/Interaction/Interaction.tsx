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
import {OrderItemAddRQ} from "../../../domain/models/api/requests/OrderItemAddRQ";
import {OrderRS} from "../../../domain/models/api/responses/OrderRS";

import './Interaction.css';
import {OrderItemDeleteRQ} from "../../../domain/models/api/requests/OrderItemDeleteRQ";
import Modal from "../../components/common/Modal/Index";
import {Validation} from "../../../domain/models/api/Validation";

function Interaction(){
    const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
    const validationModalId = "validationModalId";
    const errorModalId = "errorModalId";
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
    let [orderRS, setOrderRS] = useState<OrderRS | null>(null);
    let [validations, setValidations] = useState<Validation[] | null>(null);
    let [error, setError] = useState<string>("");
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
            showError(interactionRS.error);
        } else if (interactionRS.validations && interactionRS.validations.length > 0){
            showValidations(interactionRS.validations);
        } else {
            shouldRedirect = true;
        }
    
        button.disabled = false;
        
        if (shouldRedirect){
            navigate("/");
            setInteraction(null);    
        }
    }
    
    async function addOrder_onClick(event: React.MouseEvent<HTMLButtonElement>, productId: string){
        const button = event.target as HTMLButtonElement;
        button.disabled = true;
        
        const orderItemAddRQ: OrderItemAddRQ = { 
            interactionId: interaction!.id,
            productId: productId
        };
        const orderRS = await api.executeAsync<OrderRS>(HttpMethod.Post, InteractionEndpoint.Order, orderItemAddRQ, true);

        if (orderRS.error){
            showError(orderRS.error);
        } else if (orderRS.validations && orderRS.validations.length > 0){
            showValidations(orderRS.validations);
        } else {
            setOrderRS(orderRS);
        }

        button.disabled = false;
    }
    
    async function removeOrderItem_onClick(event: React.MouseEvent<HTMLButtonElement>, orderItemId: string){
        const button = event.target as HTMLButtonElement;
        button.disabled = true;

        const orderItemDeleteRQ: OrderItemDeleteRQ = {
            interactionId: interaction!.id,
            orderItemId: orderItemId
        };
        const orderRS = await api.executeAsync<OrderRS>(HttpMethod.Delete, InteractionEndpoint.Order, orderItemDeleteRQ, true);

        if (orderRS.error){
            showError(orderRS.error);
        } else if (orderRS.validations && orderRS.validations.length > 0){
            showValidations(orderRS.validations);
        } else {
            setOrderRS(orderRS);
        }
        
        button.disabled = false;
    }
    
    function showError(error: string){
        setError(error);
        document.getElementById(errorModalId)!.style.display = "block"
    }
    
    function showValidations(validations: Validation[]){
        setValidations(validations);
        document.getElementById(validationModalId)!.style.display = "block"
    }
    
    return <>
        <h1>Interaction</h1>
        <h2>{customer!.name}</h2>
        {EnvironmentHelper.isDev() ? <>CustomerID: {customer!.id}<br/></> : <></>}
        {EnvironmentHelper.isDev() ? <>InteractionID: {interaction!.id}<br/></> : <></>}
        <div className="form">
            <div className="row">
                <Control label="Name" value={name} onChange={async e => setName(e.target.value)}></Control>
                <Control label="Email" value={email} onChange={async e => setEmail(e.target.value)}></Control>
                <Control label="Telephone" value={telephone} onChange={async e => setTelephone(e.target.value)}></Control>
            </div>
            
            <div className="row">
                <div className="products">
                    <h3>Products</h3>
                    {productSearchRS === null || productSearchRS === undefined ? <label>loading products...</label> 
                        : productSearchRS.error && productSearchRS.error !== "" ? <>{productSearchRS.error}</> 
                            : productSearchRS.validations && productSearchRS.validations.length > 0 ? <>{productSearchRS.validations[0].message}</> 
                                :<>
                                    <table className="table">
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
                                                <td><button className="btn btn-success" onClick={async e => await addOrder_onClick(e, product.id)}>+</button></td>
                                            </tr>})
                                        }
                                        </tbody>
                                    </table>
                                </>
                    }
                </div>
                <div className="order-items">
                    <h4>Order Items</h4>
                    {orderRS === null || orderRS === undefined ? <label>no order items</label>
                        : orderRS.error && orderRS.error !== "" ? <>{orderRS.error}</>
                            : orderRS.validations && orderRS.validations.length > 0 ? <>{orderRS.validations[0].message}</>
                                : <>
                                    <table className="table">
                                        <thead>
                                        <tr>
                                            <th>Product</th>
                                            <th></th>
                                        </tr>
                                        </thead>
                                        <tbody>
                                        {orderRS.orderItems.map(orderItem => {
                                            return <tr key={orderItem.id}>
                                                <td>{orderItem.product.name}</td>
                                                <td><button className="btn btn-danger" onClick={async e => await removeOrderItem_onClick(e, orderItem.id)}>-</button></td>
                                            </tr>})
                                        }
                                        </tbody>
                                    </table>
                                </>
                    }
                </div>
            </div>
            <div className="actions">
                <Control label="State" value={state} type="select" selectOptions={getOptions()} onChange={async e => setState(e.target.value)}></Control>
                <button className="btn btn-primary" onClick={async event => await finish_onClick(event)}>Finish</button>
            </div>
        </div>
        
        <Modal id={validationModalId} 
               title="Validation"
               body={<>
               {validations && validations.length > 0 
                   ? <>
                       <ul>
                       {validations.map(v => {
                           return <>
                               <li>{v.message}</li>    
                               </>
                           })}
                       </ul>
                   </>
                   : <>no validations to show</>
               }
               </>}
               footer={<></>}
        >
            <></>
        </Modal>

        <Modal id={errorModalId}
               title="Error"
               body={<><span>{error}</span></>}
               footer={<></>}
        >
            <></>
        </Modal>
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