import React, { useState, useEffect, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import { updateFormField, fetchDeals } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const DealSelector = ({ value = [], error, campaignId, deals, updateFormField, fetchDeals }) => {
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

    const filteredDeals = deals.filter(deal => 
        deal.label.toLowerCase().includes(searchTerm.toLowerCase())
    );

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
                    style={{ backgroundColor: 'white', border: '1px solid #ced4da' }}
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
                    {filteredDeals.length > 0 ? (
                        filteredDeals.map(deal => (
                            <DropdownItem 
                                key={deal.value} 
                                toggle={false}
                                onClick={() => handleDealToggle(deal.value)}
                                className="d-flex align-items-center"
                            >
                                <Input
                                    type="checkbox"
                                    checked={value.includes(deal.value)}
                                    onChange={() => {}} // Handled by onClick
                                    className="me-2"
                                />
                                {deal.label}
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

function mapStateToProps({ deals }) {
    return Object.assign({}, { deals });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, fetchDeals }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(DealSelector));
