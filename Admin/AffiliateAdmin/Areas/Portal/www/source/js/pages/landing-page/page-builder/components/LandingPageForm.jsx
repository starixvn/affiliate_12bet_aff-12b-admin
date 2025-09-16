import React, { useEffect, useRef } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Col} from 'reactstrap';
import { fetchCampaigns, reloadAllModuleSources, validateAndResetInvalidSources } from '../actions';

import CreateNewLandingPage from './sections/CreateNewLandingPage';
import ModuleComponents from './sections/ModuleComponents';
import LandingPagePreview from './sections/LandingPagePreview';

const LandingPageForm = ({ 
    formData, 
    moduleComponents, 
    sources, 
    fetchCampaigns, 
    reloadAllModuleSources, 
    validateAndResetInvalidSources 
}) => {
    const previousCountryCode = useRef(null);
    const previousLanguageCode = useRef(null);

    useEffect(() => {
        // Initialize data on component mount
        //fetchCampaigns();
    }, [fetchCampaigns]);

    // Watch for changes in targetCountryCode or languageCode
    useEffect(() => {
        const currentCountryCode = formData?.targetCountryCode;
        const currentLanguageCode = formData?.languageCode;
        
        // Skip initial load when values are null/undefined
        if (!currentCountryCode && !currentLanguageCode) {
            previousCountryCode.current = currentCountryCode;
            previousLanguageCode.current = currentLanguageCode;
            return;
        }

        // Check if country or language has changed
        const countryChanged = previousCountryCode.current !== null && 
                              previousCountryCode.current !== currentCountryCode;
        const languageChanged = previousLanguageCode.current !== null && 
                               previousLanguageCode.current !== currentLanguageCode;

        if (countryChanged || languageChanged) {
            // Reload sources for all modules with new country/language parameters
            if (moduleComponents && moduleComponents.length > 0) {
                reloadAllModuleSources(moduleComponents, currentCountryCode, currentLanguageCode)
                    .then(() => {
                        // After sources are reloaded, validate and reset invalid selections
                        setTimeout(() => {
                            validateAndResetInvalidSources(moduleComponents, sources);
                        }, 500); // Small delay to ensure sources are updated in Redux
                    });
            }
        }

        // Update previous values
        previousCountryCode.current = currentCountryCode;
        previousLanguageCode.current = currentLanguageCode;
    }, [formData?.targetCountryCode, formData?.languageCode, moduleComponents, sources, reloadAllModuleSources, validateAndResetInvalidSources]);

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

function mapStateToProps({ formData, validation, ui, moduleComponents, sources }) {
    return Object.assign({}, { formData, validation, ui, moduleComponents, sources });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ 
        fetchCampaigns, 
        reloadAllModuleSources, 
        validateAndResetInvalidSources 
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(LandingPageForm));
