import React, { useState, useEffect } from 'react';
import { MODULE_TYPES, NAVIGATION_TARGETS, messages, validationRules } from '../libs/constants';

const ModuleForm = ({ 
    isVisible, 
    modalType, 
    selectedModule, 
    moduleTypes, 
    navigationTargets, 
    onClose, 
    onSubmit 
}) => {
    const [formData, setFormData] = useState({
        moduleId: 0,
        moduleName: '',
        moduleType: MODULE_TYPES.IMAGE,
        remark: '',
        navigateTo: NAVIGATION_TARGETS.NONE,
        isMultipleUpload: false,
        isRequired: false,
        isUnique: false
    });

    const [errors, setErrors] = useState({});
    const [isSubmitting, setIsSubmitting] = useState(false);

    useEffect(() => {
        if (selectedModule && modalType === 'edit') {
            setFormData({
                moduleId: selectedModule.ModuleId,
                moduleName: selectedModule.ModuleName || '',
                moduleType: selectedModule.ModuleType || MODULE_TYPES.IMAGE,
                remark: selectedModule.Remark || '',
                navigateTo: selectedModule.NavigateTo || NAVIGATION_TARGETS.NONE,
                isMultipleUpload: selectedModule.IsMultipleUpload || false,
                isRequired: selectedModule.IsRequired || false,
                isUnique: selectedModule.IsUnique || false
            });
        } else {
            // Reset form for create mode
            setFormData({
                moduleId: 0,
                moduleName: '',
                moduleType: MODULE_TYPES.IMAGE,
                remark: '',
                navigateTo: NAVIGATION_TARGETS.NONE,
                isMultipleUpload: false,
                isRequired: false,
                isUnique: false
            });
        }
        setErrors({});
        setIsSubmitting(false);
    }, [selectedModule, modalType, isVisible]);

    const handleInputChange = (field, value) => {
        setFormData(prev => ({ ...prev, [field]: value }));
        
        // Clear error for this field
        if (errors[field]) {
            setErrors(prev => ({ ...prev, [field]: null }));
        }
    };

    const validateForm = () => {
        const newErrors = {};

        // Module name validation
        if (!formData.moduleName.trim()) {
            newErrors.moduleName = messages.ModuleNameRequired;
        } else if (formData.moduleName.length > validationRules.moduleNameMaxLength) {
            newErrors.moduleName = messages.ModuleNameTooLong;
        } else if (validationRules.restrictedCharacters.test(formData.moduleName)) {
            newErrors.moduleName = 'Module name contains restricted characters: < > " \' &';
        }

        // Module type validation
        if (formData.moduleType < validationRules.moduleTypeMin || formData.moduleType > validationRules.moduleTypeMax) {
            newErrors.moduleType = messages.InvalidModuleType;
        }

        // Remark validation
        if (formData.remark && formData.remark.length > validationRules.remarkMaxLength) {
            newErrors.remark = messages.RemarkTooLong;
        } else if (formData.remark && validationRules.restrictedCharacters.test(formData.remark)) {
            newErrors.remark = 'Remark contains restricted characters: < > " \' &';
        }

        setErrors(newErrors);
        return Object.keys(newErrors).length === 0;
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        
        if (!validateForm()) {
            return;
        }

        setIsSubmitting(true);

        try {
            await onSubmit(formData);
        } catch (error) {
            console.error('Form submission error:', error);
        } finally {
            setIsSubmitting(false);
        }
    };

    const handleClose = () => {
        setFormData({
            moduleId: 0,
            moduleName: '',
            moduleType: MODULE_TYPES.IMAGE,
            remark: '',
            navigateTo: NAVIGATION_TARGETS.NONE,
            isMultipleUpload: false,
            isRequired: false,
            isUnique: false
        });
        setErrors({});
        setIsSubmitting(false);
        onClose();
    };

    if (!isVisible) return null;

    const isEditMode = modalType === 'edit';
    const modalTitle = isEditMode ? 'Edit Module' : 'Create New Module';

    return (
        <div className="modal fade show d-block" tabIndex="-1" role="dialog" style={{ backgroundColor: 'rgba(0,0,0,0.5)' }}>
            <div className="modal-dialog modal-lg" role="document">
                <div className="modal-content">
                    <div className="modal-header">
                        <h5 className="modal-title">
                            <i className={`fas ${isEditMode ? 'fa-edit' : 'fa-plus'} me-2`}></i>
                            {modalTitle}
                        </h5>
                        <button type="button" className="btn-close" onClick={handleClose}></button>
                    </div>
                    
                    <form onSubmit={handleSubmit}>
                        <div className="modal-body">
                            <div className="row">
                                <div className="col-md-8">
                                    <div className="mb-3">
                                        <label htmlFor="moduleName" className="form-label">
                                            Module Name <span className="text-danger">*</span>
                                        </label>
                                        <input
                                            type="text"
                                            id="moduleName"
                                            className={`form-control ${errors.moduleName ? 'is-invalid' : ''}`}
                                            value={formData.moduleName}
                                            onChange={(e) => handleInputChange('moduleName', e.target.value)}
                                            maxLength={validationRules.moduleNameMaxLength}
                                            placeholder="Enter module name"
                                        />
                                        {errors.moduleName && (
                                            <div className="invalid-feedback">{errors.moduleName}</div>
                                        )}
                                    </div>
                                </div>
                                
                                <div className="col-md-4">
                                    <div className="mb-3">
                                        <label htmlFor="moduleType" className="form-label">
                                            Module Type <span className="text-danger">*</span>
                                        </label>
                                        <select
                                            id="moduleType"
                                            className={`form-select ${errors.moduleType ? 'is-invalid' : ''}`}
                                            value={formData.moduleType}
                                            onChange={(e) => handleInputChange('moduleType', parseInt(e.target.value))}
                                        >
                                            {moduleTypes.map(type => (
                                                <option key={type.Id} value={type.Id}>
                                                    {type.Name} - {type.Description}
                                                </option>
                                            ))}
                                        </select>
                                        {errors.moduleType && (
                                            <div className="invalid-feedback">{errors.moduleType}</div>
                                        )}
                                    </div>
                                </div>
                            </div>

                            <div className="row">
                                <div className="col-md-8">
                                    <div className="mb-3">
                                        <label htmlFor="remark" className="form-label">Description</label>
                                        <textarea
                                            id="remark"
                                            className={`form-control ${errors.remark ? 'is-invalid' : ''}`}
                                            rows="3"
                                            value={formData.remark}
                                            onChange={(e) => handleInputChange('remark', e.target.value)}
                                            maxLength={validationRules.remarkMaxLength}
                                            placeholder="Enter module description (optional)"
                                        />
                                        {errors.remark && (
                                            <div className="invalid-feedback">{errors.remark}</div>
                                        )}
                                        <div className="form-text">
                                            {formData.remark.length}/{validationRules.remarkMaxLength} characters
                                        </div>
                                    </div>
                                </div>
                                
                                <div className="col-md-4">
                                    <div className="mb-3">
                                        <label htmlFor="navigateTo" className="form-label">Navigation Target</label>
                                        <select
                                            id="navigateTo"
                                            className="form-select"
                                            value={formData.navigateTo}
                                            onChange={(e) => handleInputChange('navigateTo', parseInt(e.target.value))}
                                        >
                                            {navigationTargets.map(target => (
                                                <option key={target.Id} value={target.Id}>
                                                    {target.Name}
                                                </option>
                                            ))}
                                        </select>
                                        <div className="form-text">
                                            Where should this module navigate when clicked
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div className="row">
                                <div className="col-12">
                                    <div className="mb-3">
                                        <label className="form-label">Module Settings</label>
                                        <div className="form-check-group">
                                            <div className="form-check">
                                                <input
                                                    type="checkbox"
                                                    id="isRequired"
                                                    className="form-check-input"
                                                    checked={formData.isRequired}
                                                    onChange={(e) => handleInputChange('isRequired', e.target.checked)}
                                                />
                                                <label htmlFor="isRequired" className="form-check-label">
                                                    <strong>Required Module</strong>
                                                    <small className="text-muted d-block">This module must be included in every landing page</small>
                                                </label>
                                            </div>
                                            
                                            <div className="form-check">
                                                <input
                                                    type="checkbox"
                                                    id="isUnique"
                                                    className="form-check-input"
                                                    checked={formData.isUnique}
                                                    onChange={(e) => handleInputChange('isUnique', e.target.checked)}
                                                />
                                                <label htmlFor="isUnique" className="form-check-label">
                                                    <strong>Unique Module</strong>
                                                    <small className="text-muted d-block">Only one instance of this module can be used per landing page</small>
                                                </label>
                                            </div>
                                            
                                            <div className="form-check">
                                                <input
                                                    type="checkbox"
                                                    id="isMultipleUpload"
                                                    className="form-check-input"
                                                    checked={formData.isMultipleUpload}
                                                    onChange={(e) => handleInputChange('isMultipleUpload', e.target.checked)}
                                                />
                                                <label htmlFor="isMultipleUpload" className="form-check-label">
                                                    <strong>Multiple File Upload</strong>
                                                    <small className="text-muted d-block">Allow multiple files to be uploaded for this module</small>
                                                </label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        
                        <div className="modal-footer">
                            <button
                                type="button"
                                className="btn btn-secondary"
                                onClick={handleClose}
                                disabled={isSubmitting}
                            >
                                Cancel
                            </button>
                            <button
                                type="submit"
                                className={`btn ${isEditMode ? 'btn-primary' : 'btn-success'}`}
                                disabled={isSubmitting}
                            >
                                {isSubmitting && <i className="fas fa-spinner fa-spin me-2"></i>}
                                {isEditMode ? 'Update Module' : 'Create Module'}
                            </button>
                        </div>
                    </form>
                </div>
            </div>
        </div>
    );
};

export default ModuleForm;
