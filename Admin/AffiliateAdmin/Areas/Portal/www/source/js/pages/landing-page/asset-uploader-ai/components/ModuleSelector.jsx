import React, { useState, useEffect } from 'react';
import { Form, FormGroup, Label, Input, Alert } from 'reactstrap';
import { post } from '../../../../modules/ajaxRequest';
import { globalResource } from '../../../../modules/globalResource';

const ModuleSelector = ({ modules = [], selectedModuleId, onModuleChange }) => {
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    const handleModuleChange = (event) => {
        const moduleId = parseInt(event.target.value);
        onModuleChange(moduleId);
    };

    const getModuleTypeLabel = (moduleType) => {
        switch (moduleType) {
            case 1: return 'Image';
            case 2: return 'Contact';
            case 3: return 'Form';
            case 4: return 'Link';
            default: return 'Unknown';
        }
    };

    if (loading) {
        return (
            <div className="text-center">
                <div className="spinner-border spinner-border-sm" role="status">
                    <span className="sr-only">Loading...</span>
                </div>
                <p className="small text-muted mt-2">Loading modules...</p>
            </div>
        );
    }

    if (error) {
        return (
            <Alert color="danger" className="small">
                {error}
                <button 
                    className="btn btn-sm btn-outline-danger float-right"
                    onClick={loadModules}
                    style={{ padding: '2px 8px', fontSize: '12px' }}
                >
                    Retry
                </button>
            </Alert>
        );
    }

    return (
        <div className="module-selector">
            <Form>
                <FormGroup>
                    <Label for="moduleSelect" className="small font-weight-bold">
                        Select Module <span className="text-danger">*</span>
                    </Label>
                    <Input 
                        type="select" 
                        name="moduleSelect" 
                        id="moduleSelect"
                        value={selectedModuleId || ''}
                        onChange={handleModuleChange}
                        className="form-control-sm"
                    >
                        <option value="">{globalResource.PleaseSelect || 'Please Select'}</option>
                        {modules.map(module => (
                            <option 
                                key={module.ModuleId} 
                                value={module.ModuleId}
                            >
                                {module.ModuleName} ({getModuleTypeLabel(module.ModuleType)})
                            </option>
                        ))}
                    </Input>
                </FormGroup>
            </Form>

            {selectedModuleId && modules.find(m => m.ModuleId === selectedModuleId) && (
                <div className="selected-module-info mt-3 p-2 bg-light rounded">
                    {(() => {
                        const selectedModule = modules.find(m => m.ModuleId === selectedModuleId);
                        return (
                            <div className="small">
                                <strong>Selected Module:</strong>
                                <div className="mt-1">
                                    <div><strong>Name:</strong> {selectedModule.ModuleName}</div>
                                    <div><strong>Type:</strong> {getModuleTypeLabel(selectedModule.ModuleType)}</div>
                                    {selectedModule.Remark && (
                                        <div><strong>Description:</strong> {selectedModule.Remark}</div>
                                    )}
                                    <div className="mt-2">
                                        <span className={`badge ${selectedModule.IsRequired ? 'badge-warning' : 'badge-secondary'}`}>
                                            {selectedModule.IsRequired ? 'Required' : 'Optional'}
                                        </span>
                                        {selectedModule.IsMultipleUpload && (
                                            <span className="badge badge-info ml-1">Multiple Upload</span>
                                        )}
                                        {selectedModule.IsUnique && (
                                            <span className="badge badge-success ml-1">Unique</span>
                                        )}
                                    </div>
                                </div>
                            </div>
                        );
                    })()}
                </div>
            )}

            {modules.length === 0 && !loading && (
                <Alert color="info" className="small text-center">
                    <i className="oi oi-info"></i> No modules available
                </Alert>
            )}
        </div>
    );
};

export default React.memo(ModuleSelector);
