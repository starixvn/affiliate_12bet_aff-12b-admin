import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Col} from 'reactstrap';
import { fetchCampaigns } from '../actions';

import CreateNewLandingPage from './sections/CreateNewLandingPage';
import ModuleComponents from './sections/ModuleComponents';
import LandingPagePreview from './sections/LandingPagePreview';

const LandingPageForm = ({ fetchCampaigns }) => {
    useEffect(() => {
        // Initialize data on component mount
        //fetchCampaigns();
    }, [fetchCampaigns]);

    return (
        <Container fluid className="landing-page-format">
            <Row>
                <Col lg={9}>
                    <CreateNewLandingPage />
                    <ModuleComponents />
                </Col>
                <Col lg={3}>
                    <LandingPagePreview />
                </Col>
            </Row>
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
