import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, Row, Col } from 'reactstrap';

import { updateFormField, setNotification } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const StatusSelector = ({ value, error, updateFormField, statusList, isEditMode, pageBuilderDetail, setNotification }) => {
    const handleChange = useCallback((event) => {
        const newStatusId = parseInt(event.target.value);
        
        // If in edit mode and pageId exists, show confirmation popup like page-management
        if (isEditMode && pageBuilderDetail && pageBuilderDetail.PageId) {
            setNotification({
                message: `<p>Only Active pages are available for link generation.</p>
                    <p>Inactive pages are not available for link generation. Existing links with an Inactive page will redirect users to the main join page.</p>
                    <span>Are you sure you want to change status of this landing page?</span>`,
                isConfirmPopup: true,
                onConfirm: async () => {
                    updateFormField('status', newStatusId);
                }
            });
        } else {
            // If not in edit mode or no pageId, just update the form field
            updateFormField('status', newStatusId);
        }
    }, [updateFormField, isEditMode, pageBuilderDetail, setNotification]);

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

function mapStateToProps({ statusList, isEditMode, pageBuilderDetail }) {
    return {
        statusList,
        isEditMode,
        pageBuilderDetail
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, setNotification }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(StatusSelector));
