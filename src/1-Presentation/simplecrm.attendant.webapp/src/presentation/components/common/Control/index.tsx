import "./index.css";

function Control(props: ControlProps){
    
    if (props.type === "select")
        return <>
            <div className={`control ${props.size}`}>
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
        <div className={`control ${props.size}`}>
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
    size: Size;
    selectOptions: SelectOption[]
    onChange: (event: React.ChangeEvent<HTMLInputElement> | React.ChangeEvent<HTMLSelectElement>) => {};
}

export type SelectOption = {
    key: number;
    label: string;
    value: string;
}

export enum Size{
    Size1 = "size-1",
    Size2 = "size-2",
    Size3 = "size-3",
    Size4 = "size-4",
    Size5 = "size-5",
    Size6 = "size-6",
    Size7 = "size-7",
    Size8 = "size-8",
    Size9 = "size-9",
    Size10 = "size-10",
}

Control.defaultProps = {
    type: "text",
    value: "",
    error: "",
    required: false,
    selectOptions: [],
    size: Size.Size10,
    onChange: () => {} 
}

export default Control;