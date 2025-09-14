import React, { useState, useCallback } from 'react';
import { Input, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

const MultiSelectDropdown = ({ 
    moduleId,
    value,
    error,
    sources,
    isDisabled,
    onMultipleSelect,
    onSelectAll
}) => {
    const [isOpen, setIsOpen] = useState(false);

    const toggle = useCallback(() => {
        if (!isDisabled) {
            setIsOpen(prevState => !prevState);
        }
    }, [isDisabled]);

    // Check if all sources are selected
    const allSourcesSelected = sources.length > 0 && 
        sources.every(source => Array.isArray(value) && value.includes(source.MediaId));

    // Get display text
    const getDisplayText = () => {
        if (isDisabled) return 'Please select module first';
        const selectedCount = Array.isArray(value) ? value.length : 0;
        if (selectedCount === 0) return 'Please select...';
        return `${selectedCount} selected`;
    };

    return (
        <div className="source-selector">
            <Dropdown isOpen={isOpen} toggle={toggle} className="w-100">
                <DropdownToggle
                    caret
                    className={`form-control text-left ${error ? 'is-invalid' : ''}`}
                    style={{ backgroundColor: 'white', border: '1px solid #ced4da', color: 'black' }}
                    disabled={isDisabled}
                >
                    {getDisplayText()}
                </DropdownToggle>
                <DropdownMenu className="w-100" style={{ maxHeight: '300px', overflowY: 'auto' }}>
                    <DropdownItem header>
                        <strong>Select multiple sources:</strong>
                    </DropdownItem>
                    <DropdownItem divider />
                    
                    {/* Select All Checkbox */}
                    {sources && sources.length > 0 && (
                        <DropdownItem 
                            toggle={false}
                            onClick={onSelectAll}
                            className="d-flex align-items-center"
                            style={{ backgroundColor: '#f8f9fa' }}
                        >
                            <Input
                                type="checkbox"
                                checked={allSourcesSelected}
                                onChange={() => {}} // Handled by onClick
                                className="me-2"
                            />
                            <strong>Select All ({sources.length})</strong>
                        </DropdownItem>
                    )}
                    {sources && sources.length > 0 && <DropdownItem divider />}
                    
                    {/* Individual Source Checkboxes */}
                    {sources && sources.length > 0 ? (
                        sources.map(source => {
                            const isChecked = Array.isArray(value) && value.includes(source.MediaId);
                            return (
                                <DropdownItem
                                    key={source.MediaId}
                                    tag="div"
                                    onClick={(e) => e.stopPropagation()}
                                    className="d-flex align-items-center"
                                >
                                    <Input
                                        type="checkbox"
                                        checked={isChecked}
                                        onChange={(e) => onMultipleSelect(source.MediaId, e.target.checked)}
                                        className="me-2"
                                    />
                                    <span>{source.MediaName}</span>
                                    {source.MediaPreview && (
                                        <img 
                                            src={source.MediaPreview} 
                                            alt={source.MediaName}
                                            className="ms-auto"
                                            style={{ width: '40px', height: '30px', objectFit: 'cover' }}
                                        />
                                    )}
                                </DropdownItem>
                            );
                        })
                    ) : (
                        <DropdownItem disabled>
                            No sources available
                        </DropdownItem>
                    )}
                </DropdownMenu>
            </Dropdown>
            {error && <div className="invalid-feedback d-block">{error}</div>}
        </div>
    );
};

export default MultiSelectDropdown;
