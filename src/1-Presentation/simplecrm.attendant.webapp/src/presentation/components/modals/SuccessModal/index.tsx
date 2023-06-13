import './index.css';
import {useModalContext} from "../../../hooks/useModalContext";
import {ModalConstants} from "../../../../domain/constants/ModalConstants";
import Modal from "../../common/Modal";

export function SuccessModal(){
    const { success } = useModalContext();
    
    return <Modal id={ModalConstants.Success}
                  title="Success"
                  body={<><span>{success}</span></>}
                  footer={<></>}
    >
        <></>
    </Modal>
}