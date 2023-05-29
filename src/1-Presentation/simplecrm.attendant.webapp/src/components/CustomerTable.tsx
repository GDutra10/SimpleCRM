import {CustomerSearchRS} from "../models/api/responses/CustomerSearchRS";

function CustomerTable(props: Props) {
    
    if (!props.customerSearchRS)
        return <></>
    
    if (!props.customerSearchRS.records)
        return <>{props.customerSearchRS.error}</>
        
    if (props.customerSearchRS.records.length <= 0)
        return <>No data to show!</>

    return <table className="table">
        <thead>
        <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Telephone</th>
            <th>State</th>
            <th>Creation Time</th>
            <th></th>
        </tr>
        </thead>
        <tbody>
        {props.customerSearchRS.records.map(customerRS => {
            return (
                <tr>
                    <td>{customerRS.name}</td>
                    <td>{customerRS.email}</td>
                    <td>{customerRS.telephone}</td>
                    <td>{customerRS.state}</td>
                    <td>{customerRS.creationTime.toString()}</td>
                    <td>
                        <button className="btn btn-success" onClick={event => alert("start event")}>Interact
                        </button>
                    </td>
                </tr>
            );
        })}
        </tbody>
    </table>
}

export default CustomerTable;

type Props = {
    customerSearchRS: CustomerSearchRS | null 
}