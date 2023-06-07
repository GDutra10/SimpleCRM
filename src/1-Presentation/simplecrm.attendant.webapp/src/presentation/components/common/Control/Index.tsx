import "./Index.css";

function Control(props: ControlProps){
    
    if (props.type === "select")
        return <>
            <div className="control">
                <label className="text-control">{props.label}</label>
                <select className="input-control" onChange={event=> props.onChange(event) } value={props.value} required={props.required}>
                    <option value=""></option>
                    {props.selectOptions.map(s => {
                        return <option value={s.value} key={s.key}>{s.label}</option>
                    })}
                </select>
                <label className="validation-control">{props.error}</label>
            </div>
        </>;
    
    return <>
        <div className="control">
            <label className="text-control">{props.label}</label>
            <input className="input-control" type={props.type} onChange={event=> props.onChange(event) } value={props.value} required={props.required}/>
            <label className="validation-control">{props.error}</label>
        </div>
    </>;
}

type ControlProps = {
    label: string;
    value: string;
    type: string;
    error: string;
    required: boolean;
    selectOptions: SelectOption[]
    onChange: (event: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLSelectElement>) => {};
}

export type SelectOption = {
    key: number;
    label: string;
    value: string;
}

Control.defaultProps = {
    type: "text",
    value: "",
    error: "",
    required: false,
    selectOptions: [],
    onChange: () => {} 
}

export default Control;