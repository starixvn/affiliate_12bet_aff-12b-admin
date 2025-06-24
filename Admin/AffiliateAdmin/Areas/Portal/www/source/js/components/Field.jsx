import React from 'react'
import { FormGroup, Label, Input } from 'reactstrap';

export default function Field({
    label,
    name,
    value,
    onChange,
    type = 'text',
    placeholder,
    maxLength,
    selectOptions,
}) {
  return (
    <>
        <Label htmlFor={label} className="mr-sm-2">{label}</Label>
        <Input 
            type={type} 
            name={name} 
            id={label} 
            maxLength={maxLength}
            onChange={onChange}
            value={value}
            placeholder={placeholder}
        >
            {selectOptions && selectOptions.map((option) => (
                <option key={option.value} value={option.value}>{option.label}</option>
            ))}
        </Input>
    </>
  )
}
