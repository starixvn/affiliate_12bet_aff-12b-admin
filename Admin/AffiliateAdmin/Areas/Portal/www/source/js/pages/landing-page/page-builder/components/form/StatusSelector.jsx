import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, Row, Col } from 'reactstrap';

import { updateFormField } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';
import { showConfirmToast } from '../../../../common/components/Toast';

const StatusSelector = ({ value, error, updateFormField, statusList, isEditMode, pageBuilderDetail }) => {
    const handleChange = useCallback((event) => {
        const newStatusId = parseInt(event.target.value);
        
        // If in edit mode and pageId exists, show confirmation popup like page-management
        if (isEditMode && pageBuilderDetail && pageBuilderDetail.PageId) {
            showConfirmToast({
                title: "Confirm Status Change",
                content: [
                    'Only Active pages are available for link generation.',
                    'Inactive pages are not available for link generation. Existing links with an Inactive page will redirect users to the main join page.',
                    'Are you sure you want to change status of this landing page?'
                ],
                onConfirm: () => {
                    updateFormField('status', newStatusId);
                },
                onCancel: () => {
                    // Cancel callback - reset the select to original value
                    // Force re-render by temporarily setting to different value then back
                    const originalValue = value;
                    setTimeout(() => {
                        // Reset to original value if user cancels
                        const selectElement = document.getElementById('statusSelector');
                        if (selectElement) {
                            selectElement.value = originalValue;
                        }
                    }, 100);
                },
                cancelText: "Cancel",
                submitText: "Yes"
            });
        } else {
            // If not in edit mode or no pageId, just update the form field
            updateFormField('status', newStatusId);
        }
    }, [updateFormField, isEditMode, pageBuilderDetail, value]);

    return (
        <FormGroup>
            <Row className='align-items-center'>
                <Label xl={3} for="statusSelector">
                    {pageBuilderLabels.status}
                </Label>
                <Col xl={9}>
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
    return bindActionCreators({ updateFormField }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(StatusSelector));
