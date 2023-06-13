import { createContext, ReactNode, useState } from "react";
import {Validation} from "../../domain/models/api/Validation";
import {ModalConstants} from "../../domain/constants/ModalConstants";
import {Logger} from "../../infra/logger/Logger";

export const ModalContext = createContext<ModalContextType>({
    error: "",
    success: "",
    validations: null,
    setError: () => {},
    setSuccess: () => {},
    setValidations: () => {},
    showError: (error: string) => {},
    showSuccess: (error: string) => {},
    showValidations: (validations: Validation[]) => {}
});

export function ModalProvider({ children }: Props) {
    const [error, setError] = useState<string>("");
    const [success, setSuccess] = useState<string>("");
    const [validations, setValidations] = useState<Validation[] | null>(null);
    
    function showValidations(validations: Validation[]){
        Logger.logWarn(JSON.stringify(validations));
        setValidations(validations);
        document.getElementById(ModalConstants.Validations)!.style.display = "block";
    }
    
    function showError(error: string){
        Logger.logError(error);
        setError(error);
        document.getElementById(ModalConstants.Error)!.style.display = "block";
    }

    function showSuccess(success: string){
        Logger.logInfo(success);
        setSuccess(success);
        document.getElementById(ModalConstants.Success)!.style.display = "block";
    }
    
    const modalContextType: ModalContextType = {
        error: error,
        success: success,
        validations: validations,
        setError: setError,
        setSuccess: setSuccess,
        setValidations: setValidations,
        showError: showError,
        showValidations: showValidations,
        showSuccess:  showSuccess,
        
    }
    
    return <ModalContext.Provider value={modalContextType}>
        {children}
    </ModalContext.Provider>
}


type ModalContextType = {
    error: string,
    success: string,
    validations: Validation[] | null,
    setError: React.Dispatch<React.SetStateAction<string>>,
    setSuccess: React.Dispatch<React.SetStateAction<string>>,
    setValidations: React.Dispatch<React.SetStateAction<Validation[] | null>>,
    showError: (error: string) => void,
    showValidations: (validations: Validation[]) => void,
    showSuccess: (success: string) => void,
}

type Props = {
    children: ReactNode
}