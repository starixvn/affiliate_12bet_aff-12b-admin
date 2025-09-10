import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { updateFormField } from '../../actions';
import { pageBuilderLabels, statusOptions } from '../../libs/constants';

const StatusSelector = ({ value, error, updateFormField }) => {
    const handleChange = useCallback((event) => {
        const status = event.target.value;
        updateFormField('status', status);
    }, [updateFormField]);

    const getStatusDescription = useCallback((status) => {
        switch (status) {
            case 'Draft':
                return 'Draft landing pages will not be displayed on the user site';
            case 'Active':
                return 'Active landing pages will be displayed in the list of landing pages on the user site';
            case 'Inactive':
                return 'Inactive landing pages will not be displayed in the list of landing pages on the user site';
            default:
                return '';
        }
    }, []);

    return (
        <FormGroup>
            <Label for="statusSelector">
                {pageBuilderLabels.status}
            </Label>
            <Input
                type="select"
                id="statusSelector"
                value={value || 'Draft'}
                onChange={handleChange}
                invalid={!!error}
            >
                {statusOptions.map(option => (
                    <option key={option.value} value={option.value}>
                        {option.label}
                    </option>
                ))}
            </Input>
            {error && <FormFeedback>{error}</FormFeedback>}
            {value && (
                <small className="text-muted">
                    {getStatusDescription(value)}
                </small>
            )}
        </FormGroup>
    );
};

function mapStateToProps(state) {
    return {};
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(StatusSelector));
