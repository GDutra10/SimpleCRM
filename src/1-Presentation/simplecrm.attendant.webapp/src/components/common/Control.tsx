﻿function Control(props: ControlProps){
    return <>
        <div className="form-control">
            <label className="text-control">{props.label}</label>
            <input className="input-control" type={props.type} onChange={event=> props.onChange(event) } value={props.value} required={props.required}/>
            <label className="error-control">{props.error}</label>
        </div>
    </>;
}

type ControlProps = {
    label: string;
    value: string;
    type: string;
    error: string;
    required: boolean;
    onChange: (event: React.ChangeEvent<HTMLInputElement>) => {};
}

Control.defaultProps = {
    type: "text",
    value: "",
    error: "",
    required: false,
}

export default Control;