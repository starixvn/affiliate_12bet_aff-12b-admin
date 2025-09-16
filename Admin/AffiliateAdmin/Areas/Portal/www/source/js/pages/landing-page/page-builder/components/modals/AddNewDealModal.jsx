import React, { useState, useCallback, useEffect, useRef } from 'react';
import { 
    Modal, 
    ModalHeader, 
    ModalBody, 
    ModalFooter, 
    Button, 
    FormGroup, 
    Label, 
    Dropdown, 
    DropdownToggle, 
    DropdownMenu, 
    DropdownItem,
    Input,
    Alert
} from 'reactstrap';

const AddNewDealModal = ({ isOpen, toggle, onAddDeal }) => {
    const [selectedDealType, setSelectedDealType] = useState('');
    const [isDropdownOpen, setIsDropdownOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [dropDirection, setDropDirection] = useState('down');
    const [error, setError] = useState('');
    const dropdownRef = useRef(null);

    // Available deal types based on the image
    const dealTypes = [
        { id: 'no-commission', name: 'No Commission' },
        { id: 'default-comm-group', name: 'Default Comm Group' },
        { id: '50-fix-commission', name: '50% Fix Commission' },
        { id: '40-fix-commission', name: '40% Fix Commission' },
        { id: '10-fix-commission', name: '10% Fix Commission' }
    ];

    const toggleDropdown = useCallback(() => {
        if (!isDropdownOpen && dropdownRef.current) {
            // Calculate available space when opening dropdown
            const rect = dropdownRef.current.getBoundingClientRect();
            const viewportHeight = window.innerHeight;
            const spaceBelow = viewportHeight - rect.bottom;
            const spaceAbove = rect.top;
            const dropdownHeight = 250; // maxHeight of dropdown
            
            // If not enough space below but more space above, drop up
            if (spaceBelow < dropdownHeight && spaceAbove > spaceBelow) {
                setDropDirection('up');
            } else {
                setDropDirection('down');
            }
        }
        setIsDropdownOpen(prevState => !prevState);
    }, [isDropdownOpen]);

    // Effect to handle window resize and recalculate dropdown direction
    useEffect(() => {
        const handleResize = () => {
            if (isDropdownOpen && dropdownRef.current) {
                const rect = dropdownRef.current.getBoundingClientRect();
                const viewportHeight = window.innerHeight;
                const spaceBelow = viewportHeight - rect.bottom;
                const spaceAbove = rect.top;
                const dropdownHeight = 250;
                
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
    }, [isDropdownOpen]);

    const handleSearchChange = useCallback((e) => {
        setSearchTerm(e.target.value);
    }, []);

    const handleDealTypeSelect = useCallback((dealType) => {
        setSelectedDealType(dealType);
        setIsDropdownOpen(false);
        setError('');
    }, []);

    // Filter deal types based on search term
    const filteredDealTypes = dealTypes.filter(dealType => 
        dealType.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

    // Get display text for selected deal type
    const selectedDealTypeObj = dealTypes.find(d => d.id === selectedDealType);
    const displayText = selectedDealTypeObj ? selectedDealTypeObj.name : 'Please select';

    const handleAdd = useCallback(() => {
        if (!selectedDealType) {
            setError('Please select a deal type.');
            return;
        }

        const selectedDealTypeObj = dealTypes.find(d => d.id === selectedDealType);
        if (onAddDeal) {
            onAddDeal(selectedDealTypeObj);
        }

        // Reset form and close modal
        setSelectedDealType('');
        setSearchTerm('');
        setError('');
        toggle();
    }, [selectedDealType, onAddDeal, toggle, dealTypes]);

    const handleCancel = useCallback(() => {
        // Reset form and close modal
        setSelectedDealType('');
        setSearchTerm('');
        setError('');
        toggle();
    }, [toggle]);

    // Reset form when modal is closed
    useEffect(() => {
        if (!isOpen) {
            setSelectedDealType('');
            setSearchTerm('');
            setError('');
            setIsDropdownOpen(false);
        }
    }, [isOpen]);

    return (
        <Modal isOpen={isOpen} toggle={toggle} size="md" centered>
            <ModalHeader toggle={toggle} className="bg-primary text-white">
                <i className="fa fa-plus me-2"></i>
                Add New Deal
            </ModalHeader>
            <ModalBody>
                {error && (
                    <Alert color="danger" className="mb-3">
                        <i className="fa fa-exclamation-triangle me-2"></i>
                        {error}
                    </Alert>
                )}
                
                <FormGroup>
                    <Label for="dealNameDropdown" className="required">
                        Deal Name
                    </Label>
                    <div className="deal-selector" ref={dropdownRef}>
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
                            isOpen={isDropdownOpen} 
                            toggle={toggleDropdown} 
                            className="w-100"
                        >
                            <DropdownToggle
                                caret
                                id="dealNameDropdown"
                                className={`form-control text-left ${error ? 'is-invalid border-danger' : ''}`}
                                style={{ backgroundColor: 'white', border: '1px solid #ced4da', color: 'black' }}
                            >
                                {displayText}
                            </DropdownToggle>
                            <DropdownMenu 
                                className={`w-100 ${dropDirection === 'up' ? 'dropdown-menu-up' : ''}`}
                                style={{ 
                                    maxHeight: '250px', 
                                    overflowY: 'auto',
                                    zIndex: 1060,
                                    position: 'absolute',
                                    width: '100%'
                                }}
                                flip={false}
                                right={dropDirection === 'up'}
                            >
                                <DropdownItem header>
                                    <Input
                                        type="text"
                                        placeholder="Search deal types..."
                                        value={searchTerm}
                                        onChange={handleSearchChange}
                                        onClick={(e) => e.stopPropagation()}
                                    />
                                </DropdownItem>
                                <DropdownItem divider />
                                {filteredDealTypes.length > 0 ? (
                                    filteredDealTypes.map(dealType => (
                                        <DropdownItem
                                            key={dealType.id}
                                            tag="button"
                                            type="button"
                                            onClick={(e) => {
                                                e.preventDefault();
                                                e.stopPropagation();
                                                handleDealTypeSelect(dealType.id);
                                            }}
                                            active={dealType.id === selectedDealType}
                                        >
                                            {dealType.name}
                                        </DropdownItem>
                                    ))
                                ) : (
                                    <DropdownItem disabled>
                                        No deal types found
                                    </DropdownItem>
                                )}
                            </DropdownMenu>
                        </Dropdown>
                    </div>
                </FormGroup>
            </ModalBody>
            <ModalFooter>
                <Button 
                    color="success" 
                    onClick={handleAdd}
                    className="me-2"
                >
                    <i className="fa fa-plus me-1"></i>
                    Add
                </Button>
                <Button 
                    color="secondary" 
                    onClick={handleCancel}
                >
                    <i className="fa fa-times me-1"></i>
                    Cancel
                </Button>
            </ModalFooter>
        </Modal>
    );
};

export default AddNewDealModal;
