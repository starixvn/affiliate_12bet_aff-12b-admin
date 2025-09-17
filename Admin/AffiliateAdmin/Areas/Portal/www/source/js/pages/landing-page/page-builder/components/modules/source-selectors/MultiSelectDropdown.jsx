import React, { useState, useCallback, useEffect, useRef } from 'react';
import { Input, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import ImageHoverPreview from './ImageHoverPreview';

const MultiSelectDropdown = ({ 
    moduleId,
    value,
    error,
    sources,
    isDisabled,
    onMultipleSelect,
    onSelectAll,
    bannerRootUrl
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

    // Filter sources based on search term
    const filteredSources = sources.filter(source => 
        source.MediaName && source.MediaName.toLowerCase().includes(searchTerm.toLowerCase())
    );

    // Check if all filtered sources are selected
    const allFilteredSourcesSelected = filteredSources.length > 0 && 
        filteredSources.every(source => Array.isArray(value) && value.includes(source.MediaId));

    // Get display text
    const getDisplayText = () => {
        if (isDisabled) return 'Please select module first';
        const selectedCount = Array.isArray(value) ? value.length : 0;
        if (selectedCount === 0) return 'Please select...';
        return `${selectedCount} selected`;
    };

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
                .dropdown-item {
                    color: #495057 !important;
                    font-size: 13px !important;
                }
                .dropdown-item:hover {
                    background-color: #f8f9fa !important;
                    color: #495057 !important;
                }
                .dropdown-item.active {
                    background-color: #007bff !important;
                    color: white !important;
                }
                .dropdown-item.active:hover {
                    background-color: #0056b3 !important;
                    color: white !important;
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
                    className={`form-control text-left ${error ? 'is-invalid' : ''}`}
                    style={{ backgroundColor: 'white', border: '1px solid #ced4da', color: '#495057' }}
                    disabled={isDisabled}
                >
                    {getDisplayText()}
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
                    
                    {/* Select All Checkbox */}
                    {filteredSources.length > 0 && (
                        <DropdownItem 
                            toggle={false}
                            onClick={(e) => {
                                e.stopPropagation();
                                onSelectAll();
                            }}
                            className="d-flex align-items-center"
                            style={{ backgroundColor: '#f8f9fa', cursor: 'pointer' }}
                        >
                            <Input
                                type="checkbox"
                                checked={allFilteredSourcesSelected}
                                className="me-2"
                            />
                            <strong>Select All ({filteredSources.length})</strong>
                        </DropdownItem>
                    )}
                    {filteredSources.length > 0 && <DropdownItem divider />}
                    
                    {/* Individual Source Checkboxes */}
                    {filteredSources.length > 0 ? (
                        filteredSources.map(source => {
                            const isChecked = Array.isArray(value) && value.includes(source.MediaId);
                            return (
                                <DropdownItem
                                    key={source.MediaId}
                                    tag="div"
                                    toggle={false}
                                    onClick={(e) => {
                                        e.stopPropagation();
                                        onMultipleSelect(source.MediaId, !isChecked);
                                    }}
                                    className="d-flex align-items-center"
                                    style={{ 
                                        overflow: 'hidden', // Prevent item content overflow
                                        maxWidth: '100%', // Ensure item doesn't exceed dropdown width
                                        cursor: 'pointer' // Show pointer cursor to indicate clickability
                                    }}
                                >
                                    <Input
                                        type="checkbox"
                                        checked={isChecked}
                                        className="me-2 flex-shrink-0"
                                    />
                                    {source.FileName ? (
                                        <ImageHoverPreview
                                            src={`${bannerRootUrl}/${source.FileName}`}
                                            alt={source.MediaName}
                                            maxPreviewSize={{ width: 400, height: 300 }}
                                            className="d-flex align-items-center flex-grow-1"
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
                            );
                        })
                    ) : (
                        <DropdownItem disabled>
                            {searchTerm ? 'No sources found' : 'No sources available'}
                        </DropdownItem>
                    )}
                </DropdownMenu>
            </Dropdown>
            {/* {error && <div className="invalid-feedback d-block">{error}</div>} */}
        </div>
    );
};

export default MultiSelectDropdown;
