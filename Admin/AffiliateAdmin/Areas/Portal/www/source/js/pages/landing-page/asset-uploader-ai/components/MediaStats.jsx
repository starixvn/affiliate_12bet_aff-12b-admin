import React from 'react';
import { Progress } from 'reactstrap';

const MediaStats = ({ stats }) => {
    if (!stats) {
        return (
            <div className="text-center text-muted small">
                <i className="oi oi-bar-chart"></i>
                <div>No statistics available</div>
            </div>
        );
    }

    const formatFileSize = (bytes) => {
        if (!bytes) return '0 B';
        const sizes = ['B', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(1024));
        return Math.round(bytes / Math.pow(1024, i) * 100) / 100 + ' ' + sizes[i];
    };

    const getFileTypeIcon = (extension) => {
        switch (extension?.toLowerCase()) {
            case '.jpg':
            case '.jpeg':
            case '.png':
            case '.gif':
            case '.webp':
                return 'oi oi-image';
            case '.pdf':
                return 'oi oi-document';
            case '.mp4':
            case '.avi':
            case '.mov':
                return 'oi oi-video';
            default:
                return 'oi oi-file';
        }
    };

    const getFileTypeColor = (extension) => {
        switch (extension?.toLowerCase()) {
            case '.jpg':
            case '.jpeg':
            case '.png':
            case '.gif':
            case '.webp':
                return 'primary';
            case '.pdf':
                return 'danger';
            case '.mp4':
            case '.avi':
            case '.mov':
                return 'warning';
            default:
                return 'secondary';
        }
    };

    return (
        <div className="media-stats">
            {/* Overview Stats */}
            <div className="stats-overview mb-3">
                <div className="row text-center">
                    <div className="col-6">
                        <div className="stat-item">
                            <div className="stat-value h5 mb-0 text-primary">
                                {stats.TotalFiles || 0}
                            </div>
                            <div className="stat-label small text-muted">
                                Total Files
                            </div>
                        </div>
                    </div>
                    <div className="col-6">
                        <div className="stat-item">
                            <div className="stat-value h5 mb-0 text-info">
                                {stats.TotalSizeFormatted || formatFileSize(stats.TotalSizeBytes)}
                            </div>
                            <div className="stat-label small text-muted">
                                Total Size
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            {/* File Type Breakdown */}
            {stats.FileTypeBreakdown && stats.FileTypeBreakdown.length > 0 && (
                <div className="file-types mb-3">
                    <h6 className="small font-weight-bold mb-2">
                        <i className="oi oi-pie-chart"></i> File Types
                    </h6>
                    
                    {stats.FileTypeBreakdown.map((fileType, index) => (
                        <div key={index} className="file-type-item mb-2">
                            <div className="d-flex justify-content-between align-items-center mb-1">
                                <div className="file-type-info">
                                    <i className={`${getFileTypeIcon(fileType.Extension)} mr-1`}></i>
                                    <span className="small font-weight-medium">
                                        {fileType.Extension?.toUpperCase() || 'Unknown'}
                                    </span>
                                    <span className="text-muted small ml-1">
                                        ({fileType.Count} files)
                                    </span>
                                </div>
                                <div className="file-type-size small text-muted">
                                    {fileType.TotalSizeFormatted || formatFileSize(fileType.TotalSize)}
                                </div>
                            </div>
                            <Progress 
                                value={fileType.Percentage || 0} 
                                color={getFileTypeColor(fileType.Extension)}
                                className="progress-sm"
                                style={{ height: '4px' }}
                            />
                        </div>
                    ))}
                </div>
            )}

            {/* Category Breakdown */}
            {(stats.ImageFiles > 0 || stats.VideoFiles > 0 || stats.DocumentFiles > 0 || stats.OtherFiles > 0) && (
                <div className="categories mb-3">
                    <h6 className="small font-weight-bold mb-2">
                        <i className="oi oi-tags"></i> Categories
                    </h6>
                    
                    <div className="category-grid">
                        {stats.ImageFiles > 0 && (
                            <div className="category-item d-flex justify-content-between align-items-center mb-1">
                                <div className="small">
                                    <i className="oi oi-image text-primary mr-1"></i>
                                    Images
                                </div>
                                <span className="badge badge-primary badge-sm">
                                    {stats.ImageFiles}
                                </span>
                            </div>
                        )}
                        
                        {stats.VideoFiles > 0 && (
                            <div className="category-item d-flex justify-content-between align-items-center mb-1">
                                <div className="small">
                                    <i className="oi oi-video text-warning mr-1"></i>
                                    Videos
                                </div>
                                <span className="badge badge-warning badge-sm">
                                    {stats.VideoFiles}
                                </span>
                            </div>
                        )}
                        
                        {stats.DocumentFiles > 0 && (
                            <div className="category-item d-flex justify-content-between align-items-center mb-1">
                                <div className="small">
                                    <i className="oi oi-document text-danger mr-1"></i>
                                    Documents
                                </div>
                                <span className="badge badge-danger badge-sm">
                                    {stats.DocumentFiles}
                                </span>
                            </div>
                        )}
                        
                        {stats.OtherFiles > 0 && (
                            <div className="category-item d-flex justify-content-between align-items-center mb-1">
                                <div className="small">
                                    <i className="oi oi-file text-secondary mr-1"></i>
                                    Others
                                </div>
                                <span className="badge badge-secondary badge-sm">
                                    {stats.OtherFiles}
                                </span>
                            </div>
                        )}
                    </div>
                </div>
            )}

            {/* Additional Info */}
            {(stats.MostUsedFileType || stats.LargestFile) && (
                <div className="additional-info">
                    <h6 className="small font-weight-bold mb-2">
                        <i className="oi oi-info"></i> Details
                    </h6>
                    
                    {stats.MostUsedFileType && (
                        <div className="info-item d-flex justify-content-between align-items-center mb-1">
                            <span className="small text-muted">Most Used Type:</span>
                            <span className="small font-weight-medium">
                                {stats.MostUsedFileType.toUpperCase()}
                            </span>
                        </div>
                    )}
                    
                    {stats.LargestFile && (
                        <div className="info-item d-flex justify-content-between align-items-center mb-1">
                            <span className="small text-muted">Largest File:</span>
                            <span className="small font-weight-medium" title={stats.LargestFile}>
                                {stats.LargestFile.length > 15 
                                    ? stats.LargestFile.substring(0, 15) + '...' 
                                    : stats.LargestFile
                                }
                            </span>
                        </div>
                    )}
                    
                    {stats.LargestFileSize && (
                        <div className="info-item d-flex justify-content-between align-items-center mb-1">
                            <span className="small text-muted">Size:</span>
                            <span className="small font-weight-medium">
                                {formatFileSize(stats.LargestFileSize)}
                            </span>
                        </div>
                    )}
                </div>
            )}

            {/* Empty State */}
            {stats.TotalFiles === 0 && (
                <div className="text-center text-muted py-3">
                    <i className="oi oi-cloud-upload fa-2x mb-2"></i>
                    <div className="small">No files uploaded yet</div>
                    <div className="small">Upload some files to see statistics</div>
                </div>
            )}
        </div>
    );
};

export default React.memo(MediaStats);
