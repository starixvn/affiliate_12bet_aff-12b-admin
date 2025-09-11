import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Col, Card, CardBody, Button } from 'reactstrap';

import CampaignSelector from './form/CampaignSelector';
import TargetCountrySelector from './form/TargetCountrySelector';
import LanguageSelector from './form/LanguageSelector';
import DealSelector from './form/DealSelector';
import PageNameInput from './form/PageNameInput';
import PageCodeInput from './form/PageCodeInput';
import StatusSelector from './form/StatusSelector';
import ModuleComponentsSection from './modules/ModuleComponentsSection';
import ActionButtons from './actions/ActionButtons';
import PreviewModal from './actions/PreviewModal';
import CopyLinkModal from './actions/CopyLinkModal';

import { pageBuilderLabels } from '../libs/constants';
import { fetchCampaigns } from '../actions';

const LandingPageForm = ({ formData, validation, ui, fetchCampaigns }) => {
    useEffect(() => {
        // Initialize data on component mount
        fetchCampaigns();
    }, [fetchCampaigns]);

    return (
        <Container fluid className="landing-page-format">
            <Row>
                <Col md={8}>
                    <Row>
                        <Col md={10}>
                            <fieldset className='create-new-landing-page'>
                                <legend><h6 className="font-weight-bold text-success">Create New Landing Page</h6></legend>
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
                                                value={formData.targetCountryId}
                                                error={validation.errors.targetCountryId}
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
                                                value={formData.languageId}
                                                error={validation.errors.languageId}
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
                            <Button color="primary">Review</Button>
                        </Col>
                    </Row>
                    
                    <fieldset className='module-components'>
                        <legend><h6 className="font-weight-bold text-success">Module Components</h6>
                        </legend>
                    </fieldset>
                </Col>
                <Col md={4}>
                    <fieldset className='landing-page-preview'>
                        <legend><h6 className="font-weight-bold text-success">Landing Page Preview</h6></legend>
                    </fieldset>
                </Col>
            </Row>        
            {/* <Row>
                <Col md={12}>
                    <Card>

                        <CardBody>
                            <hr className="my-4" />
                            <ModuleComponentsSection />

                            <hr className="my-4" />
                            <ActionButtons />
                        </CardBody>
                    </Card>
                </Col>
            </Row> */}

            {/* Modals */}
            {/* <PreviewModal 
                isOpen={ui.isPreviewOpen}
                formData={formData}
            />
            
            <CopyLinkModal 
                isOpen={ui.isCopyLinkOpen}
            /> */}
        </Container>
    );
};

function mapStateToProps({ formData, validation, ui }) {
    return Object.assign({}, { formData, validation, ui });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchCampaigns }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(LandingPageForm));
