import React from 'react'
import { Label, Input, Col, FormGroup } from 'reactstrap';

export default function RowField({
    label,
    name,
    value,
    onChange,
    type = 'text',
    placeholder,
    maxLength,
    selectOptions,
    labelProps = {},
    colProps = {},
    colComponent = null
}) {

    return (
        <FormGroup row> 
            <Label xs={2} htmlFor={label} className="mr-sm-2" {...labelProps}>{label}</Label>
            <Col xs={4} {...colProps}>
                {
                    colComponent ? colComponent : <Input
                        type={selectOptions ? 'select' : type}
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
                }
            </Col>
        </FormGroup>
    )
}
