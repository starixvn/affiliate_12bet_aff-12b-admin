import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback, Row, Col } from 'reactstrap';

import { updateFormField } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const StatusSelector = ({ value, error, updateFormField, statusList }) => {
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
            <Row className='align-items-center'>
                <Label md={4} lg={3} for="statusSelector">
                    {pageBuilderLabels.status}
                </Label>
                <Col md={8} lg={9}>
                    <Input
                        type="select"
                        id="statusSelector"
                        value={value || 'Draft'}
                        onChange={handleChange}
                        invalid={!!error}
                    >
                        {statusList.map(item => (
                            <option key={item.StatusId} value={item.StatusId}>
                                {item.Status}
                            </option>
                        ))}
                    </Input>
                    {/* {error && <FormFeedback>{error}</FormFeedback>} */}
                    {/* {value && (
                        <small className="text-muted">
                            {getStatusDescription(value)}
                        </small>
                    )} */}
                </Col>
            </Row>
        </FormGroup>
    );
};

function mapStateToProps({ statusList }) {
    return {
        statusList
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(StatusSelector));
