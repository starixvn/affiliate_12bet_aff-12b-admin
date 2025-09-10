import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { updateFormField } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const TargetCountrySelector = ({ value, error, targetCountryList, updateFormField }) => {
    const handleChange = useCallback((event) => {
        const targetCountryId = event.target.value || null;
        updateFormField('targetCountryId', targetCountryId);
    }, [updateFormField]);

    return (
        <FormGroup>
            <Label for="targetCountrySelector">
                {pageBuilderLabels.targetCountry}
            </Label>
            <Input
                type="select"
                id="targetCountrySelector"
                value={value || ''}
                onChange={handleChange}
                invalid={!!error}
            >
                <option value="">Please select...</option>
                {targetCountryList && targetCountryList.map(country => (
                    <option key={country.value} value={country.value}>
                        {country.label}
                    </option>
                ))}
            </Input>
            {error && <FormFeedback>{error}</FormFeedback>}
        </FormGroup>
    );
};

function mapStateToProps({ targetCountryList }) {
    return Object.assign({}, { targetCountryList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(TargetCountrySelector));
