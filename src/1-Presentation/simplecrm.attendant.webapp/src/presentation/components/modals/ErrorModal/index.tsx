import './index.css';
import {useModalContext} from "../../../hooks/useModalContext";
import {ModalConstants} from "../../../../domain/constants/ModalConstants";
import Modal from "../../common/Modal";

export function ErrorModal(){
    const { error } = useModalContext();
    
    return <Modal id={ModalConstants.Error}
                  title="Error"
                  body={<><span>{error}</span></>}
                  footer={<></>}
    >
        <></>
    </Modal>
}