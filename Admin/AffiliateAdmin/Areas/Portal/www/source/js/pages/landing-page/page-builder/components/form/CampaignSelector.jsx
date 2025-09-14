import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { updateFormField, fetchDeals, clearValidationErrors } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const CampaignSelector = ({ value, error, required = false, campaignList, updateFormField, fetchDeals, clearValidationErrors }) => {
    const handleChange = useCallback((event) => {
        const campaignId = event.target.value || null;

        if (campaignId === 'add-new') {
            alert('Add Campaign functionality is not yet implemented. Please contact the administrator to add new campaigns.');
            return;
        }
        
        updateFormField('campaignId', campaignId);
        
        // Clear validation error when user makes a selection
        if (error && campaignId) {
            clearValidationErrors();
        }
        
        // Fetch deals when campaign is selected
        // if (campaignId) {
        //     fetchDeals(campaignId);
        // }
    }, [updateFormField, error, clearValidationErrors, fetchDeals]);

    return (
        <FormGroup>
            <Label for="campaignSelector" className={required ? 'required' : ''}>
                {pageBuilderLabels.campaignName}
            </Label>
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
            {error && <FormFeedback>{error}</FormFeedback>}
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
