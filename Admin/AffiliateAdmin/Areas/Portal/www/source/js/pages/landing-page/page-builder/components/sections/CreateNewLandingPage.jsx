import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Row, Col, Button } from 'reactstrap';

import CampaignSelector from '../form/CampaignSelector';
import TargetCountrySelector from '../form/TargetCountrySelector';
import LanguageSelector from '../form/LanguageSelector';
import DealSelector from '../form/DealSelector';
import PageNameInput from '../form/PageNameInput';
import PageCodeInput from '../form/PageCodeInput';
import StatusSelector from '../form/StatusSelector';
import ActionButtons from '../actions/ActionButtons';

const CreateNewLandingPage = ({ formData, validation, isEditMode, pageBuilderDetail }) => {
    const getClonedPageName = (name) => {
        if (!name) return '';
        // Remove " (Clone)" at the end of the name if present
        return name.replace(/\s*\(Clone\)$/, '');
    };
    const title = isEditMode
        ? `Update Landing Page ${pageBuilderDetail.PageName}`
        : (pageBuilderDetail
            ? `Create New Landing Page from ${getClonedPageName(pageBuilderDetail.PageName)}`
            : 'Create New Landing Page');
    return (
        <Row>
            <Col md={10}>
                <fieldset className='create-new-landing-page'>
                    <legend><h6 className="font-weight-bold text-success">
                        {title}
                    </h6></legend>
                        <Row>
                            <Col md={12}>
                                <CampaignSelector 
                                    value={formData.campaignId}
                                    error={validation.errors.campaignId}
                                    required={true}
                                />
                            </Col>
                        </Row>
                        <Row>
                            <Col md={6}>
                                <TargetCountrySelector 
                                    value={formData.targetCountryCode}
                                    error={validation.errors.targetCountryCode}
                                />
                            </Col>
                            <Col md={6}>
                                <PageNameInput 
                                    value={formData.pageName}
                                    error={validation.errors.pageName}
                                    required={true}
                                />
                            </Col>
                        </Row>
                        <Row>
                            <Col md={6}>
                                <LanguageSelector 
                                    value={formData.languageCode}
                                    error={validation.errors.languageCode}
                                />
                            </Col>
                            <Col md={6}>
                                <PageCodeInput 
                                    value={formData.pageCode}
                                    error={validation.errors.pageCode}
                                    required={true}
                                />
                            </Col>
                        </Row>
                        <Row>
                            <Col md={6}>
                                <DealSelector 
                                    value={formData.dealIds}
                                    error={validation.errors.dealIds}
                                    campaignId={formData.campaignId}
                                />
                            </Col>
                            <Col md={6}>
                                <StatusSelector 
                                    value={formData.status}
                                    error={validation.errors.status}
                                />
                            </Col>
                        </Row>
                </fieldset>
            </Col>
            <Col md={2}>
                {/* <Button color="primary">Review</Button> */}
                <ActionButtons />
            </Col>
        </Row>
    );
};

function mapStateToProps({ formData, validation, isEditMode, pageBuilderDetail }) {
    return Object.assign({}, { formData, validation, isEditMode, pageBuilderDetail });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(CreateNewLandingPage));
