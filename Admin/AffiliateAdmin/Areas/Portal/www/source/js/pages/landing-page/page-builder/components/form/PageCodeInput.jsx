import React, { useState, useEffect, useCallback, useRef } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { updateFormField, validatePageCode, setValidationErrors } from '../../actions';
import { pageBuilderLabels, validationMessages, pageCodePattern } from '../../libs/constants';

const PageCodeInput = ({ value, error, required = false, validation, updateFormField, validatePageCode, setValidationErrors }) => {
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

    const validateCode = useCallback(async (code) => {
        if (code && code.trim().length > 0) {
            // First check pattern
            if (!pageCodePattern.test(code)) {
                setValidationErrors({
                    pageCode: validationMessages.pageCodeInvalid
                });
                return;
            }
            
            // Then check uniqueness
            await validatePageCode(code.trim());
        }
    }, [validatePageCode, setValidationErrors]);

    const handleChange = useCallback((event) => {
        const newValue = event.target.value;
        setLocalValue(newValue);
        updateFormField('pageCode', newValue);
        
        // Validate pattern immediately
        if (newValue && !pageCodePattern.test(newValue)) {
            setValidationErrors({
                pageCode: validationMessages.pageCodeInvalid
            });
            return;
        }
        
        // Debounced validation for uniqueness
        if (debounceTimeoutRef.current) {
            clearTimeout(debounceTimeoutRef.current);
        }
        
        debounceTimeoutRef.current = setTimeout(() => {
            validateCode(newValue);
        }, 500);
    }, [updateFormField, setValidationErrors, validateCode]);

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
            <Label for="pageCodeInput" className={required ? 'required' : ''}>
                {pageBuilderLabels.pageCode}
            </Label>
            <Input
                type="text"
                id="pageCodeInput"
                value={localValue}
                onChange={handleChange}
                invalid={!!error}
                valid={getValidationState() === true}
                placeholder="Enter page code (letters, numbers, hyphens, underscores only)..."
                maxLength={50}
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
                    Page code is available
                </FormFeedback>
            )}
            <small className="text-muted">
                Page code will be used in URLs. Only letters, numbers, hyphens and underscores are allowed.
            </small>
        </FormGroup>
    );
};

function mapStateToProps({ validation }) {
    return Object.assign({}, { validation });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, validatePageCode, setValidationErrors }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(PageCodeInput));
