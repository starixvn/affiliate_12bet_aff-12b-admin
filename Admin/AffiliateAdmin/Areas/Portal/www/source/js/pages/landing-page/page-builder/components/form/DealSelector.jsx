import React, { useState, useCallback, useEffect, useRef } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, Dropdown, DropdownToggle, DropdownMenu, DropdownItem, Row, Col } from 'reactstrap';

import AddNewDealModal from '../modals/AddNewDealModal';

import { updateFormField } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const DealSelector = ({ value = [], error, campaignId, dealList, updateFormField, required = false }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [dropDirection, setDropDirection] = useState('down');
    const [isAddDealModalOpen, setIsAddDealModalOpen] = useState(false);
    const dropdownRef = useRef(null);

    const toggle = useCallback(() => {
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
    }, [isOpen]);

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

    const handleDealToggle = useCallback((dealId) => {
        const currentDeals = Array.isArray(value) ? value : [];
        const newDeals = currentDeals.includes(dealId)
            ? currentDeals.filter(id => id !== dealId)
            : [...currentDeals, dealId];
        
        updateFormField('dealIds', newDeals);
    }, [value, updateFormField]);

    const handleSearchChange = useCallback((e) => {
        setSearchTerm(e.target.value);
    }, []);

    const handleAddDeal = useCallback(() => {
        setIsOpen(false); // Close the dropdown first
        setIsAddDealModalOpen(true);
    }, []);

    const handleAddDealModalToggle = useCallback(() => {
        setIsAddDealModalOpen(prevState => !prevState);
    }, []);

    const handleNewDealAdded = useCallback((newDeal) => {
        // The deal is already added to Redux dealList by AddNewDealModal
        // Now also add it to the current form selection
        const currentDeals = Array.isArray(value) ? value : [];
        const newDealId = newDeal.GroupId;
        
        // Add the new deal to the selection if not already included
        if (!currentDeals.includes(newDealId)) {
            const updatedDeals = [...currentDeals, newDealId];
            updateFormField('dealIds', updatedDeals);
        }
    }, [value, updateFormField]);

    const handleSelectAll = useCallback(() => {
        const currentDeals = Array.isArray(value) ? value : [];
        const allDealIds = filteredDeals.map(deal => deal.key);
        
        const allSelected = allDealIds.every(dealId => currentDeals.includes(dealId));
        
        if (allSelected) {
            const newDeals = currentDeals.filter(dealId => !allDealIds.includes(dealId));
            updateFormField('dealIds', newDeals);
        } else {
            const newDeals = [...new Set([...currentDeals, ...allDealIds])];
            updateFormField('dealIds', newDeals);
        }
    }, [value, filteredDeals, updateFormField]);

    const filteredDeals = dealList.filter(deal => 
        deal.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

    const allFilteredSelected = filteredDeals.length > 0 && 
        filteredDeals.every(deal => value.includes(deal.key));

    const selectedDealsText = value.length > 0 
        ? `${value.length} deal(s) selected`
        : 'Please select...';

    return (
        <FormGroup>
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
                .deal-add-new-item {
                    background-color: #e3f2fd !important;
                    color: #1976d2 !important;
                    font-weight: 500 !important;
                    font-size: 13px !important;
                    border-top: 1px solid #dee2e6;
                }
                .deal-add-new-item:hover {
                    background-color: #bbdefb !important;
                    color: #1976d2 !important;
                }
                `}
            </style>
            <Row className='align-items-center'>
                <Label md={4} lg={3} for="dealSelector" className={required ? 'required' : ''}>
                    {pageBuilderLabels.deal}
                </Label>
                <Col md={8} lg={9} ref={dropdownRef}>
                    <Dropdown 
                        isOpen={isOpen} 
                        toggle={toggle} 
                        className="w-100"
                    >
                        <DropdownToggle 
                            caret 
                            id="dealSelector"
                            className={`form-control text-left ${error ? 'is-invalid border-danger' : ''}`}
                            style={{ backgroundColor: 'white', border: '1px solid #ced4da', color: '#495057' }}
                        >
                            {selectedDealsText}
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
                                    placeholder="Search deals..."
                                    value={searchTerm}
                                    onChange={handleSearchChange}
                                    onClick={(e) => e.stopPropagation()}
                                />
                            </DropdownItem>
                            <DropdownItem divider />
                            <DropdownItem toggle={false}>
                                <span
                                    className="w-100"
                                    onClick={handleAddDeal}
                                >
                                    <i className="fa fa-plus me-1"></i>
                                    Add New ...
                                </span>
                            </DropdownItem>
                            {filteredDeals.length > 0 && (
                                <DropdownItem 
                                    toggle={false}
                                    onClick={handleSelectAll}
                                    className="d-flex align-items-center"
                                    style={{ backgroundColor: '#f8f9fa' }}
                                >
                                    <Input
                                        type="checkbox"
                                        checked={allFilteredSelected}
                                        onChange={() => {}}
                                        className="me-2"
                                    />
                                    <strong>Select All ({filteredDeals.length})</strong>
                                </DropdownItem>
                            )}
                            {filteredDeals.length > 0 && <DropdownItem divider />}
                            {filteredDeals.length > 0 ? (
                                filteredDeals.map(deal => (
                                    <DropdownItem 
                                        key={deal.key} 
                                        toggle={false}
                                        onClick={() => handleDealToggle(deal.key)}
                                        className="d-flex align-items-center"
                                    >
                                        <Input
                                            type="checkbox"
                                            checked={value.includes(deal.key)}
                                            onChange={() => {}}
                                            className="me-2"
                                        />
                                        {deal.name}
                                    </DropdownItem>
                                ))
                            ) : (
                                <DropdownItem disabled>
                                    {campaignId ? 'No deals found' : 'Please select a campaign first'}
                                </DropdownItem>
                            )}
                        </DropdownMenu>
                    </Dropdown>
                    {/* {error && <div className="invalid-feedback d-block">{error}</div>} */}
                </Col>
            </Row>
            
            {/* Add New Deal Modal */}
            <AddNewDealModal 
                isOpen={isAddDealModalOpen}
                toggle={handleAddDealModalToggle}
                onAddDeal={handleNewDealAdded}
            />
        </FormGroup>
    );
};

function mapStateToProps({ dealList }) {
    return Object.assign({}, { dealList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(DealSelector));
