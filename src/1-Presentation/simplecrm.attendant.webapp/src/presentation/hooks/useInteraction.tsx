import { useContext } from "react";
import {InteractionRSContext} from "../contexts/InteractionRSContext";

export const useInteractionContext = () => useContext(InteractionRSContext);