import React, { useCallback, useMemo, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback, Row, Col } from 'reactstrap';

import { updateFormField } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const LanguageSelector = ({ value, error, languageList, targetCountryCode, updateFormField }) => {
    // Filter languages based on targetCountryCode
    const filteredLanguageList = useMemo(() => {
    if (!languageList || !targetCountryCode) {
            return languageList || [];
        }
        
        return languageList.filter(language => 
            language.TargetCountryCode === targetCountryCode
        );
    }, [languageList, targetCountryCode]);

    // Auto-select first language when target country changes
    // useEffect(() => {
    //     if (targetCountryCode && filteredLanguageList.length > 0) {
    //         const isCurrentLanguageAvailable = value && filteredLanguageList.some(
    //             language => language.LanguageCode === value
    //         );
            
    //         if (!isCurrentLanguageAvailable) {
    //             // Auto-select the first available language
    //             const firstLanguage = filteredLanguageList[0];
    //             updateFormField('languageCode', firstLanguage.LanguageCode);
    //         }
    //     } else if (!targetCountryCode) {
    //         // Clear language selection when no target country is selected
    //         updateFormField('languageCode', null);
    //     }


    // }, [value, targetCountryCode, filteredLanguageList, updateFormField]);

    const handleChange = useCallback((event) => {
        const languageCode = event.target.value || null;
        updateFormField('languageCode', languageCode);
    }, [updateFormField]);

    
    return (
        <FormGroup>
            <Row className='align-items-center'>
                <Label md={4} lg={3} for="languageSelector">
                    {pageBuilderLabels.language}
                </Label>
                <Col md={8} lg={9}>
                    <Input
                        type="select"
                        id="languageSelector"
                        key={`language-selector-${targetCountryCode || 'none'}`}
                        value={value || ''}
                        onChange={handleChange}
                        invalid={!!error}
                        disabled={!targetCountryCode || filteredLanguageList.length === 0}
                    >
                        {targetCountryCode && filteredLanguageList && filteredLanguageList.map(language => (
                            <option key={`language-selector-${targetCountryCode || 'none'}-${language.LanguageCode}`} value={language.LanguageCode}>
                                {language.LanguageName}
                            </option>
                        ))}
                    </Input>
                    {/* {error && <FormFeedback>{error}</FormFeedback>}
                    {targetCountryCode && filteredLanguageList.length === 0 && (
                        <small className="text-muted">No languages available for selected country</small>
                    )} */}
                </Col>
            </Row>
        </FormGroup>
    );
};

function mapStateToProps({ formData, languageList }) {
    return Object.assign({}, {
        languageList,
        targetCountryCode: formData ? formData.targetCountryCode : null
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(LanguageSelector));
