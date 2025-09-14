import React, { useState, useEffect, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback, Dropdown, DropdownToggle, DropdownMenu, DropdownItem, Button } from 'reactstrap';

import { updateFormField, fetchDeals } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const DealSelector = ({ value = [], error, campaignId, dealList, updateFormField, fetchDeals }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        if (campaignId) {
            fetchDeals(campaignId);
        }
    }, [campaignId, fetchDeals]);

    const toggle = useCallback(() => {
        setIsOpen(prevState => !prevState);
    }, []);

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
        alert('Add Deal functionality is not yet implemented. Please contact the administrator to add new deals.');
    }, []);

    const handleSelectAll = useCallback(() => {
        const currentDeals = Array.isArray(value) ? value : [];
        const allDealIds = filteredDeals.map(deal => deal.key);
        
        // If all filtered deals are already selected, unselect all
        const allSelected = allDealIds.every(dealId => currentDeals.includes(dealId));
        
        if (allSelected) {
            // Remove all filtered deals from selection
            const newDeals = currentDeals.filter(dealId => !allDealIds.includes(dealId));
            updateFormField('dealIds', newDeals);
        } else {
            // Add all filtered deals to selection (avoid duplicates)
            const newDeals = [...new Set([...currentDeals, ...allDealIds])];
            updateFormField('dealIds', newDeals);
        }
    }, [value, filteredDeals, updateFormField]);

    const filteredDeals = dealList.filter(deal => 
        deal.name.toLowerCase().includes(searchTerm.toLowerCase())
    );

    // Check if all filtered deals are selected
    const allFilteredSelected = filteredDeals.length > 0 && 
        filteredDeals.every(deal => value.includes(deal.key));

    const selectedDealsText = value.length > 0 
        ? `${value.length} deal(s) selected`
        : 'Please select...';

    return (
        <FormGroup>
            <Label for="dealSelector">
                {pageBuilderLabels.deal}
            </Label>
            <Dropdown isOpen={isOpen} toggle={toggle} className="w-100">
                <DropdownToggle 
                    caret 
                    className={`form-control text-left ${error ? 'is-invalid' : ''}`}
                    style={{ backgroundColor: 'white', border: '1px solid #ced4da', color: 'black' }}
                >
                    {selectedDealsText}
                </DropdownToggle>
                <DropdownMenu className="w-100" style={{ maxHeight: '300px', overflowY: 'auto' }}>
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
                            onClick={(e) => {
                                e.stopPropagation();
                                handleAddDeal();
                            }}
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
                                onChange={() => {}} // Handled by onClick
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
                                    onChange={() => {}} // Handled by onClick
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
            {error && <div className="invalid-feedback d-block">{error}</div>}
        </FormGroup>
    );
};

function mapStateToProps({ dealList }) {
    return Object.assign({}, { dealList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, fetchDeals }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(DealSelector));
