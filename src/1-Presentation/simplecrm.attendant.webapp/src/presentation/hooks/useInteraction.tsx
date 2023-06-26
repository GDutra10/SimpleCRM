import { useContext } from "react";
import {InteractionContext} from "../contexts/InteractionContext";

export const useInteractionContext = () => useContext(InteractionContext);