import React, { useState, useCallback, useEffect, useRef } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, Row, Col, Dropdown, DropdownToggle, DropdownMenu, DropdownItem } from 'reactstrap';

import AddNewCampaignModal from '../modals/AddNewCampaignModal';

import { updateFormField, fetchDeals, clearValidationErrors } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';
import { claimResource } from '../../../../../modules/claim';

const CampaignSelector = ({ value, error, required = false, campaignList, updateFormField, clearValidationErrors, isModifyCampaign }) => {
    const [isAddCampaignModalOpen, setIsAddCampaignModalOpen] = useState(false);
    const [isOpen, setIsOpen] = useState(false);
    const [searchTerm, setSearchTerm] = useState('');
    const [dropDirection, setDropDirection] = useState('down');
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

    const handleSearchChange = useCallback((e) => {
        setSearchTerm(e.target.value);
    }, []);

    // Filter campaigns based on search term
    const filteredCampaigns = campaignList ? campaignList.filter(campaign => 
        campaign.CampaignName && (campaign.CampaignName.toLowerCase().includes(searchTerm.toLowerCase()) || campaign.CampaignCode.toLowerCase().includes(searchTerm.toLowerCase()))
    ) : [];

    const handleCampaignSelect = useCallback((campaignId) => {
        if (campaignId === 'add-new') {
            setIsAddCampaignModalOpen(true);
            setIsOpen(false); // Keep this for modal case
            return;
        }
        
        updateFormField('campaignId', campaignId);
        
        if (error && campaignId) {
            clearValidationErrors();
        }
        
        setIsOpen(false);
        setSearchTerm(''); // Reset search after selection
    }, [updateFormField, error, clearValidationErrors]);

    const handleAddCampaignModalToggle = useCallback(() => {
        setIsAddCampaignModalOpen(prevState => !prevState);
    }, []);

    const handleNewCampaignAdded = useCallback(() => {
        // Campaign is automatically selected by fetchCampaigns(true) in AddNewCampaignModal
        // Just close the modal after successful addition
        setIsAddCampaignModalOpen(false);
    }, []);

    // Get display text for selected campaign
    const getDisplayText = () => {
        if (!value) return 'Select campaign name';
        const selectedCampaign = campaignList?.find(campaign => campaign.CampaignId === value);
        return selectedCampaign ? selectedCampaign.CampaignCode + ' | ' + selectedCampaign.CampaignName : 'Select campaign name';
    };

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
                .dropdown-toggle-visible-text {
                    color: #495057 !important;
                    text-decoration: none !important;
                }
                .dropdown-toggle-visible-text:hover {
                    background-color: #f8f9fa !important;
                    color: #495057 !important;
                    text-decoration: none !important;
                }
                .dropdown-toggle-visible-text:focus {
                    background-color: #e9ecef !important;
                    color: #495057 !important;
                    text-decoration: none !important;
                    box-shadow: 0 0 0 0.2rem rgba(0,123,255,0.25) !important;
                }
                .dropdown-item.active {
                    background-color: #007bff !important;
                    color: white !important;
                }
                .dropdown-item.active:hover {
                    background-color: #0056b3 !important;
                    color: white !important;
                }
                .dropdown-item {
                    color: #495057 !important;
                    font-size: 13px !important;
                }
                .dropdown-item:hover {
                    background-color: #f8f9fa !important;
                    color: #495057 !important;
                }
                .campaign-add-new-item {
                    background-color: #e3f2fd !important;
                    color: #1976d2 !important;
                    font-weight: 500 !important;
                    font-size: 13px !important;
                    border-top: 1px solid #dee2e6;
                }
                .campaign-add-new-item:hover {
                    background-color: #bbdefb !important;
                    color: #1976d2 !important;
                }
                `}
            </style>
            <Row className='align-items-center'>
                <Label md={4} lg={3} for="campaignSelector" className={required ? 'required' : ''}>
                    {pageBuilderLabels.campaignName}
                </Label>
                <Col md={8} lg={9}>
                    <div ref={dropdownRef}>
                        <Dropdown 
                            isOpen={isOpen} 
                            toggle={toggle} 
                            className="w-100"
                        >
                            <DropdownToggle
                                caret
                                className={`form-control text-truncate text-left dropdown-toggle-visible-text ${error ? 'is-invalid border-danger' : ''}`}
                                style={{ 
                                    backgroundColor: 'white',
                                    border: '1px solid #ced4da', 
                                    color: '#495057',
                                    textDecoration: 'none'
                                }}
                                title={getDisplayText()}
                            >
                                {getDisplayText()}
                            </DropdownToggle>
                            <DropdownMenu 
                                className={`w-100 ${dropDirection === 'up' ? 'dropdown-menu-up' : ''}`}
                                style={{ 
                                    maxHeight: '300px', 
                                    overflowY: 'auto',
                                    overflowX: 'hidden',
                                    zIndex: 1050,
                                    position: 'absolute',
                                    width: '100%',
                                    minWidth: '250px'
                                }}
                                flip={false}
                                right={dropDirection === 'up'}
                            >
                                <DropdownItem header>
                                    <Input
                                        type="text"
                                        placeholder="Search campaigns..."
                                        value={searchTerm}
                                        onChange={handleSearchChange}
                                        onClick={(e) => e.stopPropagation()}
                                    />
                                </DropdownItem>
                                <DropdownItem divider />
                                
                                {/* Add New Option */}
                                {
                                    isModifyCampaign && (
                                        <DropdownItem
                                            tag="button"
                                            type="button"
                                            onClick={() => handleCampaignSelect('add-new')}
                                            className="campaign-add-new-item"
                                        >
                                            <i className="fa fa-plus me-2"></i>
                                            Add New Campaign...
                                        </DropdownItem>
                                    )
                                }
                                
                                {/* Campaign List */}
                                {filteredCampaigns.length > 0 ? (
                                    filteredCampaigns.map(campaign => (
                                        <DropdownItem
                                            key={campaign.CampaignId}
                                            onClick={() => {
                                                handleCampaignSelect(campaign.CampaignId);
                                                toggle(); // Manually call the dropdown toggle function
                                            }}
                                            active={campaign.CampaignId === value}
                                            style={{ 
                                                cursor: 'pointer', 
                                                textOverflow: 'ellipsis',
                                                overflow: 'hidden',
                                                whiteSpace: 'nowrap',
                                                maxWidth: '100%',
                                                width: '100%',
                                                textAlign: 'left'
                                            }}
                                        >
                                            <span 
                                                className="text-truncate" 
                                                style={{ minWidth: 0 }}
                                                title={campaign.CampaignCode + ' | ' + campaign.CampaignName}
                                            >
                                                {campaign.CampaignCode + ' | ' + campaign.CampaignName}
                                            </span>
                                        </DropdownItem>
                                    ))
                                ) : (
                                    campaignList && campaignList.length > 0 && searchTerm && (
                                        <DropdownItem disabled>
                                            <em>No campaigns found</em>
                                        </DropdownItem>
                                    )
                                )}
                                
                                {(!campaignList || campaignList.length === 0) && !searchTerm && (
                                    <DropdownItem disabled>
                                        <em>No campaigns available</em>
                                    </DropdownItem>
                                )}
                            </DropdownMenu>
                        </Dropdown>
                    </div>
                    {error && <div className="invalid-feedback d-block">{error}</div>}
                </Col>
            </Row>
            
            {/* Add New Campaign Modal */}
            <AddNewCampaignModal 
                isOpen={isAddCampaignModalOpen}
                toggle={handleAddCampaignModalToggle}
                onAddCampaign={handleNewCampaignAdded}
            />
        </FormGroup>
    );
};

function mapStateToProps({ campaignList, claimsPrincipal }) {
    const isModifyCampaign = claimsPrincipal.hasWriteClaim(claimResource.AllCampaigns);

    return Object.assign({}, { campaignList, isModifyCampaign });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, fetchDeals, clearValidationErrors }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(CampaignSelector));
