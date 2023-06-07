import { createContext, ReactNode, useState } from "react";
import {InteractionRS} from "../models/api/responses/InteractionRS";

export const InteractionRSContext = createContext<InteractionRSContextType>({
    interaction: null,
    setInteraction: () => {}
});

export function InteractionRSProvider({ children }: Props){
    const [interaction, setInteraction] = useState<InteractionRS | null>(null);
    
    return <InteractionRSContext.Provider value={{interaction: interaction, setInteraction: setInteraction}}>
        {children}
    </InteractionRSContext.Provider>
}


type InteractionRSContextType = {
    interaction: InteractionRS | null,
    setInteraction: React.Dispatch<React.SetStateAction<InteractionRS | null>> 
}

type Props = {
    children: ReactNode
}