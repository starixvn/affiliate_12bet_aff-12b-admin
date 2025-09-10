import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { updateFormField, fetchDeals, clearValidationErrors } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const CampaignSelector = ({ value, error, required = false, campaigns, updateFormField, fetchDeals, clearValidationErrors }) => {
    const handleChange = useCallback((event) => {
        const campaignId = event.target.value || null;
        
        updateFormField('campaignId', campaignId);
        
        // Clear validation error when user makes a selection
        if (error && campaignId) {
            clearValidationErrors();
        }
        
        // Fetch deals when campaign is selected
        if (campaignId) {
            fetchDeals(campaignId);
        }
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
                <option value="">Please select...</option>
                {campaigns && campaigns.map(campaign => (
                    <option key={campaign.value} value={campaign.value}>
                        {campaign.label}
                    </option>
                ))}
            </Input>
            {error && <FormFeedback>{error}</FormFeedback>}
        </FormGroup>
    );
};

function mapStateToProps({ campaigns }) {
    return Object.assign({}, { campaigns });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, fetchDeals, clearValidationErrors }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(CampaignSelector));
