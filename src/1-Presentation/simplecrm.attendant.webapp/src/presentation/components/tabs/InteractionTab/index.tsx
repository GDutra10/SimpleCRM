import './index.css';

import {useEffect, useState} from "react";
import {HttpMethod, SimpleCRMWebAPI} from "../../../../infra/api/SimpleCRMWebAPI";
import {useModalContext} from "../../../hooks/useModalContext";
import {useInteractionContext} from "../../../hooks/useInteraction";
import {ProductSearchRS} from "../../../../domain/models/api/responses/ProductSearchRS";
import {OrderRS} from "../../../../domain/models/api/responses/OrderRS";
import {Logger} from "../../../../infra/logger/Logger";
import {ProductSearchRQ} from "../../../../domain/models/api/requests/ProductSearchRQ";
import {InteractionEndpoint, ProductEndpoint} from "../../../../domain/constants/EndpointConstants";
import {InteractionFinishRQ} from "../../../../domain/models/api/requests/InteractionFinishRQ";
import {InteractionState} from "../../../../domain/models/api/InteractionState";
import {InteractionRS} from "../../../../domain/models/api/responses/InteractionRS";
import {OrderItemAddRQ} from "../../../../domain/models/api/requests/OrderItemAddRQ";
import {OrderItemDeleteRQ} from "../../../../domain/models/api/requests/OrderItemDeleteRQ";
import {EnvironmentHelper} from "../../../../domain/helpers/EnvironmentHelper";
import Control, {SelectOption, Size} from "../../common/Control";
import {DateHelper} from "../../../../domain/helpers/DateHelper";

export function InteractionTab(props: Props){
    const api : SimpleCRMWebAPI = new SimpleCRMWebAPI();
    const modalContext = useModalContext();
    const interactionContext = useInteractionContext();
    const interaction = interactionContext.interactions.find(i => i.id === props.interactionId);
    const customer = interaction?.customer ?? null;
    let [name, setName] = useState<string>(customer?.name ?? "");
    let [email, setEmail] = useState<string>(customer?.email ?? "");
    let [telephone, setTelephone] = useState<string>(customer?.telephone ?? "");
    let [state, setState] = useState<string>(customer?.state ?? "");
    let [productSearchRS, setProductSearchRS] = useState<ProductSearchRS | null>(null);
    let [orderRS, setOrderRS] = useState<OrderRS | null>(null);
    let shouldEndInteraction = false;
    let productLoaded = false;

    useEffect(() => {
        loadProducts_onLoad()
            .then(productSearchRS => {
                Logger.logInfo("product loaded");
                setProductSearchRS(productSearchRS);
            })
            .catch(e => { Logger.logError("fail to get products!"); });

        if (shouldEndInteraction){
            endInteraction();
        }
            
    }, []);

    if (!interaction || !customer){
        let message = "invalid interaction, redirecting to home..";

        Logger.logWarn(message);
        shouldEndInteraction = true;

        return <>{message}</>
    }

    function endInteraction(){
        Logger.logDebug("InteractionId: " + props.interactionId);
        const interactions = interactionContext.interactions.filter(i => i.id !== props.interactionId);

        Logger.logDebug("Before remove: " + JSON.stringify(interactionContext.interactions));
        Logger.logDebug("After remove: " + JSON.stringify(interactions));
        
        interactionContext.setInteractions(interactions);
    }
    
    async function loadProducts_onLoad(){
        const productSearchRQ : ProductSearchRQ = { onlyActive: true, pageSize: 50, pageNumber: 0};
        const query = new URLSearchParams({onlyActive: ""+productSearchRQ.onlyActive, pageNumber: productSearchRQ.pageNumber.toString(), pageSize: productSearchRQ.pageSize.toString()}).toString();

        return await api.executeAsync<ProductSearchRS>(HttpMethod.Get, `${ProductEndpoint.Products}?${query}`, null, true);
    }

    async function finish_onClick(event: React.MouseEvent<HTMLButtonElement>){
        Logger.logInfo("try finishing interaction");

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
            modalContext.showError(interactionRS.error);
        } else if (interactionRS.validations && interactionRS.validations.length > 0){
            modalContext.showValidations(interactionRS.validations);
        } else {
            endInteraction();
        }

        button.disabled = false;
    }

    async function addOrder_onClick(event: React.MouseEvent<HTMLButtonElement>, productId: string){
        Logger.logInfo(`adding order ProductId: ${productId}`);

        const button = event.target as HTMLButtonElement;
        button.disabled = true;

        const orderItemAddRQ: OrderItemAddRQ = {
            interactionId: interaction!.id,
            productId: productId
        };
        const orderRS = await api.executeAsync<OrderRS>(HttpMethod.Post, InteractionEndpoint.Order, orderItemAddRQ, true);

        if (orderRS.error){
            modalContext.showError(orderRS.error);
        } else if (orderRS.validations && orderRS.validations.length > 0){
            modalContext.showValidations(orderRS.validations);
        } else {
            setOrderRS(orderRS);
        }

        button.disabled = false;
    }

    async function removeOrderItem_onClick(event: React.MouseEvent<HTMLButtonElement>, orderItemId: string){
        Logger.logInfo(`removing order item: ${orderItemId}`);

        const button = event.target as HTMLButtonElement;
        button.disabled = true;

        const orderItemDeleteRQ: OrderItemDeleteRQ = {
            interactionId: interaction!.id,
            orderItemId: orderItemId
        };
        const orderRS = await api.executeAsync<OrderRS>(HttpMethod.Delete, InteractionEndpoint.Order, orderItemDeleteRQ, true);

        if (orderRS.error){
            modalContext.showError(orderRS.error);
        } else if (orderRS.validations && orderRS.validations.length > 0){
            modalContext.showValidations(orderRS.validations);
        } else {
            setOrderRS(orderRS);
        }

        button.disabled = false;
    }
    
    const creationTime = new Date(interaction.creationTime);
    
    return <div id={props.interactionId} className="tab-content">
        <h1>{customer!.name}</h1>
        <span>{"Started at: " + DateHelper.ToString(creationTime)}</span>
        {EnvironmentHelper.isDev() ? <p>CustomerID: {customer!.id}</p> : <></>}
        {EnvironmentHelper.isDev() ? <p>InteractionID: {interaction!.id}<br/></p> : <></>}
        <div className="form">
            <div className="row">
                <Control label="Name" value={name} size={Size.Size5} onChange={async e => setName(e.target.value)}></Control>
                <Control label="Email" value={email} size={Size.Size5} onChange={async e => setEmail(e.target.value)}></Control>
                <Control label="Telephone" value={telephone} size={Size.Size2} onChange={async e => setTelephone(e.target.value)}></Control>
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
                <Control label="State" value={state} size={Size.Size2} type="select" selectOptions={getOptions()} onChange={async e => setState(e.target.value)}></Control>
                <button className="btn btn-primary" onClick={async event => await finish_onClick(event)}>Finish</button>
            </div>
        </div>
    </div>
}

function getOptions() : SelectOption[] {
    const options = new Array<SelectOption>();

    options.push({ key: 0, value: InteractionState.NotAvailable, label: InteractionState.NotAvailable});
    options.push({ key: 1, value: InteractionState.NotInterested, label: InteractionState.NotInterested});
    options.push({ key: 2, value: InteractionState.PreSale, label: InteractionState.PreSale});

    return options;
}

type Props = {
    interactionId: string
}