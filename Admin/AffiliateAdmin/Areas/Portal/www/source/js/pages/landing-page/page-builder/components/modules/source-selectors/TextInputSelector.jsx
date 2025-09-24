import React, { useState } from 'react';
import { Input, FormFeedback, FormGroup } from 'reactstrap';
import { urlFormatExampleImage } from '../../../libs/urlFormatExampleImage';

const TextInputSelector = ({ 
    moduleId, 
    value, 
    error, 
    isDisabled, 
    onChange,
    placeholder = "",
    className = ""
}) => {
    const [showTooltip, setShowTooltip] = useState(false);
    
    return (
        <div className="source-selector">
            <FormGroup>
                <div className="d-flex align-items-center" style={{ width: '100%' }}>
                    <Input
                        id={`text-input-${moduleId}`}
                        key={`moduleId-text-${moduleId}`}
                        type="text"
                        placeholder={placeholder || "Please enter URL"}
                        value={value || ''}
                        onChange={onChange}
                        invalid={!!error}
                        disabled={isDisabled}
                        className={`${className} flex-grow-1`}
                    />
                    <div 
                        className="position-relative rounded-circle note-info"
                        onMouseEnter={() => setShowTooltip(true)}
                        onMouseLeave={() => setShowTooltip(false)}
                        style={{ cursor: 'pointer', marginLeft: '8px', flexShrink: 0 }}
                    >
                        <span class="oi oi-info"></span>
                        {showTooltip && (
                            <div 
                                className="position-absolute bg-white border rounded shadow-lg p-2"
                                style={{
                                    top: '-200px',
                                    right: '25px',
                                    zIndex: 1050,
                                    minWidth: '500px'
                                }}
                            >
                                <div className="text-center">
                                    <img 
                                        src={urlFormatExampleImage}
                                        alt="URL Format Example"
                                        className="img-fluid rounded"
                                        style={{ maxHeight: '300px', width: 'auto' }}
                                    />
                                </div>
                            </div>
                        )}
                    </div>
                </div>
            </FormGroup>
        </div>
    );
};

export default TextInputSelector;
