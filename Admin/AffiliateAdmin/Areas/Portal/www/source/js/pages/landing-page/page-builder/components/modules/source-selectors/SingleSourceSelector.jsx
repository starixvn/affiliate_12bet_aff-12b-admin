import React from 'react';
import { Input, FormFeedback } from 'reactstrap';

const SingleSourceSelector = ({ 
    moduleId, 
    value, 
    error, 
    sources, 
    isDisabled, 
    onChange,
    className = ""
}) => {
    return (
        <Input
            id={`source-${moduleId}`}
            key={`moduleId-source-${moduleId}`}
            type="select"
            value={value || ''}
            onChange={onChange}
            invalid={!!error}
            disabled={isDisabled}
            className={className}
        >
            <option value="">
                {isDisabled ? 'Please select module first' : 'Please select...'}
            </option>
            {sources && sources.map(source => (
                <option key={source.MediaId} value={source.MediaId}>
                    {source.MediaName}
                </option>
            ))}
        </Input>
    );
};

export default SingleSourceSelector;
