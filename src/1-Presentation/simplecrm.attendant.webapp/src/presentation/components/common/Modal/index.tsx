import './index.css';

export function Modal(props: Props){
    function close(){
        document.getElementById(props.id)!.style.display = "none";
    }
    
    return (
     <>
         <div className="modal" id={props.id}>
          <div className="modal-content">
           <div className="modal-header">
            <span className="close" onClick={event => close()}>&times;</span>
            <h2>{props.title}</h2>
           </div>
           <div className="modal-body">
            {props.body}
           </div>
           <div className="modal-footer">
            {props.footer}
           </div>
          </div>
         </div>
     </>
 );   
}

export default Modal;

type Props = {
    id: string,
    title: string,
    body: JSX.Element, 
    footer: JSX.Element,
    children: React.ReactNode
}