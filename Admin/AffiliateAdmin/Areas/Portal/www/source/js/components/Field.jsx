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
<<<<<<< HEAD
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
=======
    <FormGroup className="my-sm-1 mr-sm-2">
        <Label htmlFor={label} className="mr-sm-2">{label}</Label>
        <Input type={type} name={name} id={label} maxLength={maxLength}
            onChange={onChange}
            value={value}
            placeholder={placeholder}
            >
                {selectOptions && selectOptions.map((option) => (
                    <option key={option.value} value={option.value}>{option.label}</option>
                ))}
            </Input>
    </FormGroup>
>>>>>>> 72e9e9483 (refactor code)
  )
}
