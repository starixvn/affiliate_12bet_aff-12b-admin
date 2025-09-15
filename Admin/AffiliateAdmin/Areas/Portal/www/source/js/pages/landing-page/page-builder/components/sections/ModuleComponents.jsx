import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Container, Row, Col, Card, CardBody, Button, Alert } from 'reactstrap';
import ModuleComponentsSection from '../modules/ModuleComponentsSection';

const ModuleComponents = ({ validation, moduleComponents, ui }) => {
    const moduleErrors = {};
    
    if (validation && validation.errors) {
        Object.entries(validation.errors).forEach(([key, error]) => {
            if (key === 'moduleComponents' || key.startsWith('module_') || key.startsWith('media_') || key.startsWith('required_module_')) {
                moduleErrors[key] = error;
            }
        });
    }
    
    const hasModuleErrors = Object.keys(moduleErrors).length > 0;
    
    const hasPreviewIssues = ui.hasPreviewedOrCreated && ui.previewUrl && 
                             (moduleComponents && moduleComponents.length > 0) &&
                             validation && validation.errors && 
                             Object.keys(validation.errors).some(key => 
                                 key.includes('preview') || key.includes('generation') || key.includes('configuration')
                             );
    
    const getModuleFieldDisplayName = (fieldKey) => {
        if (fieldKey === 'moduleComponents') {
            return 'Module Components';
        } else if (fieldKey.startsWith('module_')) {
            const moduleId = fieldKey.replace('module_', '');
            const moduleComponent = moduleComponents?.find(comp => comp.id == moduleId);
            const orderNumber = moduleComponent?.orderId || moduleId;
            return `Module Selection (Order ${orderNumber})`;
        } else if (fieldKey.startsWith('media_')) {
            const moduleId = fieldKey.replace('media_', '');
            const moduleComponent = moduleComponents?.find(comp => comp.id == moduleId);
            const orderNumber = moduleComponent?.orderId || moduleId;
            return `Source Selection (Order ${orderNumber})`;
        } else if (fieldKey.startsWith('required_module_')) {
            // For required_module_ errors, don't show the key, just show the error message directly
            return '';
        }
        return fieldKey;
    };
    

    return (
        <Row>
            <Col>
                <fieldset className='module-components'>
                    <legend><h6 className="font-weight-bold text-success">Module Components</h6></legend>
                    
                    {/* Show module validation errors */}
                    {hasModuleErrors && (
                        <Alert color="danger" className="mb-3">
                            <h6 className="alert-heading">
                                <i className="fa fa-exclamation-triangle me-2"></i>
                                Module Components Validation Errors
                            </h6>
                            <p className="mb-2">Please fix the following errors in the module components:</p>
                            <ul className="mb-0">
                                {Object.entries(moduleErrors).map(([key, error]) => {
                                    const fieldDisplayName = getModuleFieldDisplayName(key);
                                    const errorMessage = typeof error === 'string' ? error : String(error || 'Validation error');
                                    
                                    return (
                                        <li key={key} className="mb-1">
                                            {fieldDisplayName ? (
                                                <><strong>{fieldDisplayName}:</strong> {errorMessage}</>
                                            ) : (
                                                errorMessage
                                            )}
                                        </li>
                                    );
                                })}
                            </ul>
                        </Alert>
                    )}
                    
                    {/* Show configuration warnings */}
                    {hasPreviewIssues && (
                        <Alert color="warning" className="mb-3">
                            <h6 className="alert-heading">
                                <i className="fa fa-exclamation-triangle me-2"></i>
                                Module Configuration Warning
                            </h6>
                            <p className="mb-2">
                                There may be issues with the current module configuration that could affect preview generation:
                            </p>
                            <ul className="mb-2">
                                <li>Some modules may not have proper source configurations</li>
                                <li>Media sources might not be accessible or properly configured</li>
                                <li>Module combinations might cause display conflicts</li>
                            </ul>
                            <p className="mb-0">
                                <strong>Recommendation:</strong> Review your module selections and source configurations. 
                                If preview issues persist, try opening the preview in a new tab or contact support.
                            </p>
                        </Alert>
                    )}
                    
                    <ModuleComponentsSection />
                </fieldset>
            </Col>
        </Row>
    );
};

function mapStateToProps({ validation, moduleComponents, ui }) {
    return Object.assign({}, { validation, moduleComponents, ui });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({  }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(ModuleComponents));
