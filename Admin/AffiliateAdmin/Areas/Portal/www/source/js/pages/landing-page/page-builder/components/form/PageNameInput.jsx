import React, { useState, useEffect, useCallback, useRef } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { updateFormField, validatePageName } from '../../actions';
import { pageBuilderLabels, validationMessages } from '../../libs/constants';

const PageNameInput = ({ value, error, required = false, validation, updateFormField, validatePageName }) => {
    const [localValue, setLocalValue] = useState(value || '');
    const debounceTimeoutRef = useRef(null);

    useEffect(() => {
        // Sync with external value changes
        if (value !== localValue) {
            setLocalValue(value || '');
        }
    }, [value]);

    useEffect(() => {
        return () => {
            if (debounceTimeoutRef.current) {
                clearTimeout(debounceTimeoutRef.current);
            }
        };
    }, []);

    const validateName = useCallback(async (name) => {
        if (name && name.trim().length > 0) {
            await validatePageName(name.trim());
        }
    }, [validatePageName]);

    const handleChange = useCallback((event) => {
        const newValue = event.target.value;
        setLocalValue(newValue);
        updateFormField('pageName', newValue);
        
        // Debounced validation
        if (debounceTimeoutRef.current) {
            clearTimeout(debounceTimeoutRef.current);
        }
        
        debounceTimeoutRef.current = setTimeout(() => {
            validateName(newValue);
        }, 500);
    }, [updateFormField, validateName]);

    const getValidationState = useCallback(() => {
        const { isValidating } = validation;
        
        if (isValidating) return null;
        if (error) return false;
        if (localValue && !error) return true;
        return null;
    }, [validation, error, localValue]);

    const { isValidating } = validation;

    return (
        <FormGroup>
            <Label for="pageNameInput" className={required ? 'required' : ''}>
                {pageBuilderLabels.pageName}
            </Label>
            <Input
                type="text"
                id="pageNameInput"
                value={localValue}
                onChange={handleChange}
                invalid={!!error}
                valid={getValidationState() === true}
                placeholder="e.g.: New Year Promotion Page"
                maxLength={100}
            />
            {error && <FormFeedback>{error}</FormFeedback>}
            {isValidating && (
                <small className="text-muted">
                    <i className="fa fa-spinner fa-spin me-1"></i>
                    Checking availability...
                </small>
            )}
            {!error && localValue && !isValidating && (
                <FormFeedback valid>
                    Page name is available
                </FormFeedback>
            )}
        </FormGroup>
    );
};

function mapStateToProps({ validation }) {
    return Object.assign({}, { validation });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, validatePageName }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(PageNameInput));
