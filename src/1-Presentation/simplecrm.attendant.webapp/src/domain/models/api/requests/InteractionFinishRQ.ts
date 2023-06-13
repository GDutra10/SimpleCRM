import {InteractionState} from "../InteractionState";
import {CustomerProps} from "../CustomerProps";

export interface InteractionFinishRQ{
    interactionId: string;
    state: InteractionState;
    customerProps: CustomerProps;
}