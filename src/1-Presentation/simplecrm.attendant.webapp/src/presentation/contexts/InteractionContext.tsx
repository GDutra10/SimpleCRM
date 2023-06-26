import { createContext, ReactNode, useState } from "react";
import {InteractionRS} from "../../domain/models/api/responses/InteractionRS";

export const InteractionContext = createContext<InteractionContextType>({
    interactions: [],
    setInteractions: () => {}
});

export function InteractionProvider({ children }: Props){
    const [interactions, setInteractions] = useState<InteractionRS[]>([]);
    
    const interactionContext: InteractionContextType = {
        interactions: interactions,
        setInteractions: setInteractions
    }
    
    return <InteractionContext.Provider value={interactionContext}>
        {children}
    </InteractionContext.Provider>
}

type InteractionContextType = {
    interactions: InteractionRS[],
    setInteractions: React.Dispatch<React.SetStateAction<InteractionRS[]>>
}

type Props = {
    children: ReactNode
}