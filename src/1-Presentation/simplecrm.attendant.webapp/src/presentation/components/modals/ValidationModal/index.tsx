import './index.css';
import {useModalContext} from "../../../hooks/useModalContext";
import {ModalConstants} from "../../../../domain/constants/ModalConstants";
import Modal from "../../common/Modal";

export function ValidationModal(){
    const { validations} = useModalContext();
    
    return <Modal id={ModalConstants.Validations}
                  title="Validation"
                  body={<>
                      {validations && validations.length > 0
                          ? <>
                              <ul>
                                  {validations.map(v => {
                                      return <li key={new Date().toString()}>{v.message}</li>
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
}