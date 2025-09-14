import React from 'react';
import { Input, FormFeedback, Label, FormGroup } from 'reactstrap';

const TextInputSelector = ({ 
    moduleId, 
    value, 
    error, 
    isDisabled, 
    onChange,
    placeholder = "",
    className = ""
}) => {
    return (
        <div className="source-selector">
            <FormGroup>
                <Input
                    id={`text-input-${moduleId}`}
                    key={`moduleId-text-${moduleId}`}
                    type="text"
                    placeholder={placeholder || "Please enter URL"}
                    value={value || ''}
                    onChange={onChange}
                    invalid={!!error}
                    disabled={isDisabled}
                    className={className}
                />
                {error && <FormFeedback>{error}</FormFeedback>}
            </FormGroup>
        </div>
    );
};

export default TextInputSelector;
