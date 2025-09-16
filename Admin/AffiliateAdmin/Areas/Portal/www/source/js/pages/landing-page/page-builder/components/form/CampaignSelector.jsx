import React, { useState, useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback, Row, Col } from 'reactstrap';

import AddNewCampaignModal from '../modals/AddNewCampaignModal';

import { updateFormField, fetchDeals, clearValidationErrors } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const CampaignSelector = ({ value, error, required = false, campaignList, updateFormField, fetchDeals, clearValidationErrors }) => {
    const [isAddCampaignModalOpen, setIsAddCampaignModalOpen] = useState(false);

    const handleChange = useCallback((event) => {
        const campaignId = event.target.value || null;

        if (campaignId === 'add-new') {
            setIsAddCampaignModalOpen(true);
            return;
        }
        
        updateFormField('campaignId', campaignId);
        
        if (error && campaignId) {
            clearValidationErrors();
        }
    }, [updateFormField, error, clearValidationErrors]);

    const handleAddCampaignModalToggle = useCallback(() => {
        setIsAddCampaignModalOpen(prevState => !prevState);
    }, []);

    const handleNewCampaignAdded = useCallback((newCampaign) => {
        updateFormField('campaignId', newCampaign.CampaignId);
    }, [updateFormField]);

    return (
        <FormGroup>
            <Row className='align-items-center'>
                <Label md={4} lg={3} for="campaignSelector" className={required ? 'required' : ''}>
                    {pageBuilderLabels.campaignName}
                </Label>
                <Col md={8} lg={9}>
                    <Input
                        type="select"
                        id="campaignSelector"
                        value={value || ''}
                        onChange={handleChange}
                        invalid={!!error}
                    >
                        <option className='d-none' value="" disabled>Select campaign name</option>
                        <option value="add-new">Add New...</option>
                        {campaignList && campaignList.map(campaign => (
                            <option key={campaign.CampaignId} value={campaign.CampaignId}>
                                {campaign.CampaignName}
                            </option>
                        ))}
                    </Input>
                    {/* {error && <FormFeedback>{error}</FormFeedback>} */}
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

function mapStateToProps({ campaignList }) {
    return Object.assign({}, { campaignList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, fetchDeals, clearValidationErrors }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(CampaignSelector));
