import { useParams } from "react-router-dom";

function Interaction(){
    const params = useParams();
    const idCustomer = params.idCustomer;
    
    return <>
        <h1>Interaction</h1>
        {idCustomer}
    </>;
}

export default Interaction;