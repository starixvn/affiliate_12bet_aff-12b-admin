import React, { useState, useCallback, useEffect, useRef } from 'react';
import { Input, FormFeedback, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

const SingleSourceSelector = ({ 
    moduleId, 
    value, 
    error, 
    sources, 
    isDisabled, 
    onChange,
    className = ""
}) => {
    const [isOpen, setIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [dropDirection, setDropDirection] = useState('down');
    const dropdownRef = useRef(null);

    const toggle = useCallback(() => {
        if (!isDisabled) {
            if (!isOpen && dropdownRef.current) {
                // Calculate available space when opening dropdown
                const rect = dropdownRef.current.getBoundingClientRect();
                const viewportHeight = window.innerHeight;
                const spaceBelow = viewportHeight - rect.bottom;
                const spaceAbove = rect.top;
                const dropdownHeight = 300; // maxHeight of dropdown
                
                // If not enough space below but more space above, drop up
                if (spaceBelow < dropdownHeight && spaceAbove > spaceBelow) {
                    setDropDirection('up');
                } else {
                    setDropDirection('down');
                }
            }
            setIsOpen(prevState => !prevState);
        }
    }, [isDisabled, isOpen]);

    // Effect to handle window resize and recalculate dropdown direction
    useEffect(() => {
        const handleResize = () => {
            if (isOpen && dropdownRef.current) {
                const rect = dropdownRef.current.getBoundingClientRect();
                const viewportHeight = window.innerHeight;
                const spaceBelow = viewportHeight - rect.bottom;
                const spaceAbove = rect.top;
                const dropdownHeight = 300;
                
                if (spaceBelow < dropdownHeight && spaceAbove > spaceBelow) {
                    setDropDirection('up');
                } else {
                    setDropDirection('down');
                }
            }
        };

        window.addEventListener('resize', handleResize);
        window.addEventListener('scroll', handleResize);
        
        return () => {
            window.removeEventListener('resize', handleResize);
            window.removeEventListener('scroll', handleResize);
        };
    }, [isOpen]);

    const handleSearchChange = useCallback((e) => {
        setSearchTerm(e.target.value);
    }, []);

    const handleSourceSelect = useCallback((sourceId) => {
        // Create synthetic event object to match expected onChange signature
        const syntheticEvent = {
            target: { value: sourceId }
        };
        onChange(syntheticEvent);
        setIsOpen(false);
    }, [onChange]);

    // Filter sources based on search term
    const filteredSources = sources.filter(source => 
        source.MediaName && source.MediaName.toLowerCase().includes(searchTerm.toLowerCase())
    );

    // Get display text for selected source
    const selectedSource = sources.find(s => s.MediaId === value);
    const displayText = isDisabled 
        ? 'Please select module first' 
        : (selectedSource ? selectedSource.MediaName : 'Please select...');

    return (
        <div className="source-selector" ref={dropdownRef}>
            <style>
                {`
                .dropdown-menu-up {
                    bottom: 100% !important;
                    top: auto !important;
                    transform: translateY(-2px) !important;
                    margin-bottom: 2px !important;
                }
                `}
            </style>
            <Dropdown 
                isOpen={isOpen} 
                toggle={toggle} 
                className="w-100"
            >
                <DropdownToggle
                    caret
                    className={`form-control text-left ${error ? 'is-invalid border-danger' : ''} ${className}`}
                    style={{ backgroundColor: 'white', border: '1px solid #ced4da', color: 'black' }}
                    disabled={isDisabled}
                >
                    {displayText}
                </DropdownToggle>
                <DropdownMenu 
                    className={`w-100 ${dropDirection === 'up' ? 'dropdown-menu-up' : ''}`}
                    style={{ 
                        maxHeight: '300px', 
                        overflowY: 'auto',
                        zIndex: 1050,
                        position: 'absolute',
                        width: '100%'
                    }}
                    flip={false}
                    right={dropDirection === 'up'}
                >
                    <DropdownItem header>
                        <Input
                            type="text"
                            placeholder="Search sources..."
                            value={searchTerm}
                            onChange={handleSearchChange}
                            onClick={(e) => e.stopPropagation()}
                        />
                    </DropdownItem>
                    <DropdownItem divider />
                    <DropdownItem 
                        tag="button"
                        type="button"
                        hidden
                        onClick={(e) => {
                            e.preventDefault();
                            e.stopPropagation();
                            handleSourceSelect('');
                        }}
                        active={!value}
                    >
                        Please select...
                    </DropdownItem>
                    {filteredSources.length > 0 ? (
                        filteredSources.map(source => (
                            <DropdownItem
                                key={source.MediaId}
                                tag="button"
                                type="button"
                                onClick={(e) => {
                                    e.preventDefault();
                                    e.stopPropagation();
                                    handleSourceSelect(source.MediaId);
                                }}
                                active={source.MediaId === value}
                                className="d-flex align-items-center"
                            >
                                <span className="me-auto">{source.MediaName}</span>
                                {source.MediaPreview && (
                                    <img 
                                        src={source.MediaPreview} 
                                        alt={source.MediaName}
                                        className="ms-2"
                                        style={{ width: '40px', height: '30px', objectFit: 'cover' }}
                                    />
                                )}
                            </DropdownItem>
                        ))
                    ) : (
                        <DropdownItem disabled>
                            No sources found
                        </DropdownItem>
                    )}
                </DropdownMenu>
            </Dropdown>
        </div>
    );
};

export default SingleSourceSelector;
