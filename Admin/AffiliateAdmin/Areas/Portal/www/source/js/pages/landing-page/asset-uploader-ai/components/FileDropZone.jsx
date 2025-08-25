import React, { useRef, useState } from 'react';
import { Button, Alert } from 'reactstrap';
import { globalResource } from '../../../../modules/globalResource';
import uploadService from '../libs/uploadService';

const FileDropZone = ({ onFilesSelected, selectedModule, isUploading, dragOver }) => {
    const fileInputRef = useRef(null);
    const [validationErrors, setValidationErrors] = useState([]);

    const handleFileSelect = (files) => {
        if (!selectedModule) {
            setValidationErrors(['Please select a module first']);
            return;
        }

        const fileArray = Array.from(files);
        const errors = [];

        // Client-side validation
        fileArray.forEach(file => {
            const validation = uploadService.validateFileClient(file, selectedModule.ModuleType);
            if (!validation.isValid) {
                errors.push(`${file.name}: ${validation.errors.join(', ')}`);
            }
        });

        if (errors.length > 0) {
            setValidationErrors(errors);
            return;
        }

        setValidationErrors([]);
        onFilesSelected(files);
    };

    const handleDrop = (e) => {
        e.preventDefault();
        const files = e.dataTransfer.files;
        if (files.length > 0) {
            handleFileSelect(files);
        }
    };

    const handleDragOver = (e) => {
        e.preventDefault();
    };

    const handleFileInputChange = (e) => {
        const files = e.target.files;
        if (files.length > 0) {
            handleFileSelect(files);
        }
        // Reset input to allow same file selection
        e.target.value = '';
    };

    const handleBrowseClick = () => {
        fileInputRef.current?.click();
    };

    const getDropZoneClass = () => {
        let classes = 'file-drop-zone text-center p-4 border-2 border-dashed rounded';
        
        if (dragOver) {
            classes += ' border-primary bg-light';
        } else if (selectedModule) {
            classes += ' border-secondary';
        } else {
            classes += ' border-muted';
        }
        
        if (isUploading) {
            classes += ' uploading';
        }
        
        return classes;
    };

    const getMaxFileInfo = () => {
        if (!selectedModule) return '';
        
        const moduleType = selectedModule.ModuleType;
        const maxSize = '5MB';
        const allowedTypes = '.jpg, .jpeg, .png, .gif';
        
        switch (moduleType) {
            case 1: // Image modules
                return `Max ${maxSize} • ${allowedTypes}`;
            case 2: // Contact modules  
            case 3: // Form modules
                return `Max ${maxSize} • ${allowedTypes}`;
            case 4: // Link modules
                return 'File uploads not supported for Link modules';
            default:
                return `Max ${maxSize} • ${allowedTypes}`;
        }
    };

    const isUploadDisabled = !selectedModule || isUploading || selectedModule.ModuleType === 4;

    return (
        <div className="file-drop-zone-container">
            {/* Hidden file input */}
            <input
                ref={fileInputRef}
                type="file"
                multiple
                accept=".jpg,.jpeg,.png,.gif"
                onChange={handleFileInputChange}
                style={{ display: 'none' }}
            />

            {/* Drop zone */}
            <div
                className={getDropZoneClass()}
                onDrop={handleDrop}
                onDragOver={handleDragOver}
                style={{ minHeight: '200px', display: 'flex', alignItems: 'center', justifyContent: 'center' }}
            >
                <div>
                    {isUploading ? (
                        <>
                            <div className="spinner-border text-primary mb-3" role="status">
                                <span className="sr-only">Uploading...</span>
                            </div>
                            <h5 className="text-primary">Uploading Files...</h5>
                            <p className="text-muted">Please wait while your files are being uploaded</p>
                        </>
                    ) : dragOver ? (
                        <>
                            <i className="fas fa-cloud-upload-alt fa-3x text-primary mb-3"></i>
                            <h5 className="text-primary">Drop files here</h5>
                            <p className="text-muted">Release to upload files</p>
                        </>
                    ) : selectedModule ? (
                        selectedModule.ModuleType === 4 ? (
                            <>
                                <i className="fas fa-ban fa-3x text-muted mb-3"></i>
                                <h5 className="text-muted">Upload Not Available</h5>
                                <p className="text-muted">Link modules do not support file uploads</p>
                            </>
                        ) : (
                            <>
                                <i className="fas fa-cloud-upload-alt fa-3x text-secondary mb-3"></i>
                                <h5>Upload Files for {selectedModule.ModuleName}</h5>
                                <p className="text-muted mb-3">
                                    Drag & drop files here or click to browse
                                </p>
                                <Button 
                                    color="primary" 
                                    onClick={handleBrowseClick}
                                    disabled={isUploadDisabled}
                                >
                                    <i className="oi oi-folder"></i> Browse Files
                                </Button>
                                <div className="small text-muted mt-3">
                                    {getMaxFileInfo()}
                                </div>
                            </>
                        )
                    ) : (
                        <>
                            <i className="fas fa-folder-open fa-3x text-muted mb-3"></i>
                            <h5 className="text-muted">Select a Module First</h5>
                            <p className="text-muted">Choose a module from the left panel to start uploading</p>
                        </>
                    )}
                </div>
            </div>

            {/* Validation Errors */}
            {validationErrors.length > 0 && (
                <Alert color="danger" className="mt-3">
                    <strong>Validation Errors:</strong>
                    <ul className="mb-0 mt-2">
                        {validationErrors.map((error, index) => (
                            <li key={index}>{error}</li>
                        ))}
                    </ul>
                    <button 
                        className="btn btn-sm btn-outline-danger float-right"
                        onClick={() => setValidationErrors([])}
                        style={{ marginTop: '-5px' }}
                    >
                        ×
                    </button>
                </Alert>
            )}

            {/* Upload Instructions */}
            {selectedModule && selectedModule.ModuleType !== 4 && !isUploading && (
                <div className="upload-instructions mt-3 p-3 bg-light rounded">
                    <div className="row">
                        <div className="col-md-6">
                            <h6><i className="oi oi-info"></i> Upload Guidelines</h6>
                            <ul className="small mb-0">
                                <li>Maximum file size: 5MB</li>
                                <li>Supported formats: JPG, PNG, GIF</li>
                                <li>Multiple files can be uploaded at once</li>
                            </ul>
                        </div>
                        <div className="col-md-6">
                            <h6><i className="oi oi-target"></i> Module Requirements</h6>
                            <ul className="small mb-0">
                                {selectedModule.ModuleName.toLowerCase().includes('background') && (
                                    <li>Recommended size: 1920×1080px</li>
                                )}
                                {selectedModule.ModuleName.toLowerCase().includes('hero') && (
                                    <li>Recommended size: 1200×600px</li>
                                )}
                                {selectedModule.ModuleName.toLowerCase().includes('logo') && (
                                    <li>Recommended size: 400×200px</li>
                                )}
                                {selectedModule.ModuleName.toLowerCase().includes('button') && (
                                    <li>Recommended size: 300×100px</li>
                                )}
                                <li>Images will be auto-optimized if needed</li>
                            </ul>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default React.memo(FileDropZone);
