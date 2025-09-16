import React, { useState, useCallback, useEffect, useRef } from 'react';
import { Input, FormFeedback, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import ImageHoverPreview from './ImageHoverPreview';

const SingleSourceSelector = ({ 
    moduleId, 
    value, 
    error, 
    sources, 
    isDisabled, 
    onChange,
    bannerRootUrl,
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
    // Handle both string and number comparisons for MediaId
    const selectedSource = sources.find(s => s.MediaId == value);
    
    // Debug: selectedSource found successfully
    
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
                .dropdown-toggle-visible-text {
                    color: #212529 !important;
                    text-decoration: none !important;
                }
                .dropdown-toggle-visible-text:hover {
                    background-color: #f8f9fa !important;
                    color: #212529 !important;
                    text-decoration: none !important;
                }
                .dropdown-toggle-visible-text:focus {
                    background-color: #e9ecef !important;
                    color: #212529 !important;
                    text-decoration: none !important;
                    box-shadow: 0 0 0 0.2rem rgba(0,123,255,0.25) !important;
                }
                /* Fix active dropdown item text visibility */
                .dropdown-item.active {
                    background-color: #007bff !important;
                    color: white !important;
                }
                .dropdown-item.active:hover {
                    background-color: #0056b3 !important;
                    color: white !important;
                }
                .dropdown-item {
                    color: #212529 !important;
                }
                .dropdown-item:hover {
                    background-color: #f8f9fa !important;
                    color: #212529 !important;
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
                    className={`form-control text-left dropdown-toggle-visible-text ${error ? 'is-invalid border-danger' : ''} ${className}`}
                    style={{ 
                        backgroundColor: 'white',
                        border: '1px solid #ced4da', 
                        color: '#212529',
                        textDecoration: 'none'
                    }}
                    disabled={isDisabled}
                >
                    {displayText}
                </DropdownToggle>
                <DropdownMenu 
                    className={`w-100 ${dropDirection === 'up' ? 'dropdown-menu-up' : ''}`}
                    style={{ 
                        maxHeight: '300px', 
                        overflowY: 'auto',
                        overflowX: 'hidden', // Prevent horizontal scroll
                        zIndex: 1050,
                        position: 'absolute',
                        width: '100%',
                        minWidth: '250px' // Ensure minimum width for content
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
                        onClick={() => {
                            handleSourceSelect('');
                            toggle(); // Manually call the dropdown toggle function
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
                                onClick={() => {
                                    handleSourceSelect(source.MediaId);
                                    toggle(); // Manually call the dropdown toggle function
                                }}
                                active={source.MediaId === value}
                                className="d-flex align-items-center"
                                style={{ 
                                    cursor: 'pointer', 
                                    border: 'none', 
                                    backgroundColor: 'transparent',
                                    overflow: 'hidden', // Prevent item content overflow
                                    whiteSpace: 'nowrap', // Prevent text wrapping
                                    maxWidth: '100%', // Ensure item doesn't exceed dropdown width
                                    width: '100%', // Ensure button takes full width
                                    textAlign: 'left' // Align content to left
                                }}
                            >
                                {source.FileName ? (
                                    <ImageHoverPreview
                                        src={`${bannerRootUrl}/${source.FileName}`}
                                        alt={source.MediaName}
                                        maxPreviewSize={{ width: 400, height: 300 }}
                                        className="d-flex align-items-center w-100"
                                    >
                                        <span 
                                            className="me-auto text-truncate" 
                                            style={{ minWidth: 0, overflow: 'hidden', textOverflow: 'ellipsis' }}
                                            title={source.MediaName} // Show full name on hover
                                        >
                                            {source.MediaName}
                                        </span>
                                        <img 
                                            src={`${bannerRootUrl}/${source.FileName}`} 
                                            alt={source.MediaName}
                                            className="ml-2 flex-shrink-0"
                                            style={{ 
                                                maxWidth: '50px',
                                                maxHeight: '24px',
                                                objectFit: 'cover', 
                                                cursor: 'pointer',
                                                borderRadius: '3px',
                                                border: '1px solid #dee2e6'
                                            }}
                                        />
                                    </ImageHoverPreview>
                                ) : (
                                    <span 
                                        className="me-auto text-truncate" 
                                        style={{ minWidth: 0, overflow: 'hidden', textOverflow: 'ellipsis' }}
                                        title={source.MediaName} // Show full name on hover
                                    >
                                        {source.MediaName}
                                    </span>
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
