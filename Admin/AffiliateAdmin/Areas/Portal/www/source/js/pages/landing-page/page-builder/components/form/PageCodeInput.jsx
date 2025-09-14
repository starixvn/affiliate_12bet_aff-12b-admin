import React, { useState, useEffect, useCallback, useRef } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { FormGroup, Label, Input, FormFeedback } from 'reactstrap';

import { updateFormField, validatePageCode, setValidationErrors } from '../../actions';
import { pageBuilderLabels, validationMessages, pageCodePattern } from '../../libs/constants';

const PageCodeInput = ({ value, error, required = false, validation, isEditMode, pageBuilderDetail, updateFormField, validatePageCode, setValidationErrors }) => {
    const [localValue, setLocalValue] = useState('');
    const debounceTimeoutRef = useRef(null);

    useEffect(() => {
        // Sync with external value changes
        if (value !== localValue) {
            setLocalValue(value || '');
            if (!pageCodePattern.test(value)) {
                setValidationErrors({
                    pageCode: validationMessages.pageCodeInvalid
                });
            }
        }
    }, [value]);

    // Validate page code when initialized from pageBuilderDetail (edit mode)
    useEffect(() => {
        if (isEditMode && pageBuilderDetail && pageBuilderDetail.PageCode && !validation.isValidating) {
            // Only validate if this is the initial load and we have a page code from pageBuilderDetail
            const pageCodeFromDetail = pageBuilderDetail.PageCode;
            
            // Validate the existing page code and show errors if invalid
            if (pageCodeFromDetail && pageCodeFromDetail.trim().length > 0) {
                // First check pattern
                if (!pageCodePattern.test(pageCodeFromDetail)) {
                    setValidationErrors({
                        pageCode: validationMessages.pageCodeInvalid
                    });
                    return;
                }
                
                // In edit mode, exclude the current page from uniqueness validation
                const currentPageId = pageBuilderDetail.PageId;
                validatePageCode(pageCodeFromDetail.trim(), currentPageId);
            }
        }
    }, [isEditMode, pageBuilderDetail, validation.isValidating, validatePageCode, setValidationErrors]);

    useEffect(() => {
        return () => {
            if (debounceTimeoutRef.current) {
                clearTimeout(debounceTimeoutRef.current);
            }
        };
    }, []);

    const validateCode = useCallback(async (code, excludePageId = null) => {
        if (code && code.trim().length > 0) {
            // First check pattern
            if (!pageCodePattern.test(code)) {
                setValidationErrors({
                    pageCode: validationMessages.pageCodeInvalid
                });
                return;
            }
            
            // Then check uniqueness, excluding current page in edit mode
            await validatePageCode(code.trim(), excludePageId);
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
            // In edit mode, exclude current page from uniqueness validation
            const excludePageId = isEditMode && pageBuilderDetail ? pageBuilderDetail.PageId : null;
            validateCode(newValue, excludePageId);
        }, 500);
    }, [updateFormField, setValidationErrors, validateCode, isEditMode, pageBuilderDetail]);

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
                placeholder="e.g.: LP-2025-NY"
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
            {/* <small className="text-muted">
                Page code will be used in URLs. Only letters, numbers, hyphens and underscores are allowed.
            </small> */}
        </FormGroup>
    );
};

function mapStateToProps({ validation, isEditMode, pageBuilderDetail }) {
    return Object.assign({}, { validation, isEditMode, pageBuilderDetail });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ updateFormField, validatePageCode, setValidationErrors }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(PageCodeInput));
