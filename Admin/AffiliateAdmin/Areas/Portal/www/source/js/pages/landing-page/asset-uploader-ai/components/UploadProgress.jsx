import React from 'react';
import { Progress, Alert, Button } from 'reactstrap';

const UploadProgress = ({ progress, isUploading }) => {
    if (!isUploading && Object.keys(progress).length === 0) {
        return null;
    }

    const progressEntries = Object.entries(progress);
    const hasProgress = progressEntries.length > 0;

    if (!hasProgress && isUploading) {
        return (
            <Alert color="info" className="mb-0">
                <div className="d-flex align-items-center">
                    <div className="spinner-border spinner-border-sm mr-2" role="status">
                        <span className="sr-only">Uploading...</span>
                    </div>
                    <div>
                        <strong>Preparing Upload...</strong>
                        <div className="small text-muted">Validating files and preparing for upload</div>
                    </div>
                </div>
            </Alert>
        );
    }

    return (
        <div className="upload-progress">
            <div className="d-flex justify-content-between align-items-center mb-2">
                <h6 className="mb-0">
                    <i className="oi oi-cloud-upload"></i> Upload Progress
                </h6>
                <span className="text-muted small">
                    {progressEntries.length} file(s) uploading
                </span>
            </div>

            {progressEntries.map(([uploadId, progressInfo]) => {
                const { percent, files } = progressInfo;
                const fileName = files && files.length > 0 ? files[0].name : 'Unknown file';
                const isComplete = percent >= 100;
                const progressColor = isComplete ? 'success' : percent > 50 ? 'info' : 'primary';

                return (
                    <div key={uploadId} className="upload-item mb-3">
                        <div className="d-flex justify-content-between align-items-center mb-1">
                            <div className="file-info flex-grow-1">
                                <div className="file-name font-weight-medium text-truncate" title={fileName}>
                                    <i className="oi oi-document mr-1"></i>
                                    {fileName}
                                </div>
                                {files && files.length > 1 && (
                                    <div className="small text-muted">
                                        +{files.length - 1} more file(s)
                                    </div>
                                )}
                            </div>
                            <div className="progress-info ml-2">
                                <span className={`badge badge-${progressColor}`}>
                                    {Math.round(percent)}%
                                </span>
                            </div>
                        </div>
                        
                        <Progress 
                            value={percent} 
                            color={progressColor}
                            className="mb-1"
                            style={{ height: '6px' }}
                        />
                        
                        <div className="d-flex justify-content-between align-items-center">
                            <div className="small text-muted">
                                {isComplete ? (
                                    <span className="text-success">
                                        <i className="oi oi-check"></i> Upload complete
                                    </span>
                                ) : (
                                    <span>
                                        <i className="oi oi-data-transfer-upload"></i> Uploading...
                                    </span>
                                )}
                            </div>
                            
                            {!isComplete && (
                                <Button 
                                    size="sm" 
                                    color="outline-danger" 
                                    className="btn-sm"
                                    onClick={() => {
                                        // TODO: Implement cancel functionality
                                        console.log('Cancel upload:', uploadId);
                                    }}
                                    title="Cancel Upload"
                                >
                                    <i className="oi oi-x"></i>
                                </Button>
                            )}
                        </div>
                    </div>
                );
            })}

            {/* Overall Progress Summary */}
            {progressEntries.length > 1 && (
                <div className="overall-progress mt-3 pt-3 border-top">
                    <div className="d-flex justify-content-between align-items-center mb-2">
                        <span className="small font-weight-medium">Overall Progress</span>
                        <span className="small text-muted">
                            {progressEntries.filter(([, info]) => info.percent >= 100).length} of {progressEntries.length} completed
                        </span>
                    </div>
                    
                    <Progress 
                        value={progressEntries.reduce((sum, [, info]) => sum + info.percent, 0) / progressEntries.length} 
                        color="primary"
                        style={{ height: '8px' }}
                    />
                </div>
            )}

            {/* Upload Tips */}
            {isUploading && (
                <Alert color="light" className="mt-3 mb-0">
                    <div className="small">
                        <i className="oi oi-lightbulb"></i> <strong>Upload Tips:</strong>
                        <ul className="mb-0 mt-1">
                            <li>Keep this page open until upload completes</li>
                            <li>Large files may take longer to process</li>
                            <li>You can continue working while files upload</li>
                        </ul>
                    </div>
                </Alert>
            )}
        </div>
    );
};

export default React.memo(UploadProgress);
