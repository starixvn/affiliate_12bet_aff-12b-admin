import React, { useCallback } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { updateFormField } from '../../actions';
import { pageBuilderLabels } from '../../libs/constants';

const LanguageSelector = ({ value, error, languageList, updateFormField }) => {
    const handleChange = useCallback((event) => {
        const languageId = event.target.value || null;
        updateFormField('languageId', languageId);
    }, [updateFormField]);

    return (
        <FormGroup>
            <Label for="languageSelector">
                {pageBuilderLabels.language}
            </Label>
            <Input
                type="select"
                id="languageSelector"
                value={value || ''}
                onChange={handleChange}
                invalid={!!error}
            >
                <option value="">Please select...</option>
                {languageList && languageList.map(language => (
                    <option key={language.value} value={language.value}>
                        {language.label}
                    </option>
                ))}
            </Input>
            {error && <FormFeedback>{error}</FormFeedback>}
        </FormGroup>
    );
};

function mapStateToProps({ languageList }) {
    return Object.assign({}, { languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(LanguageSelector));
