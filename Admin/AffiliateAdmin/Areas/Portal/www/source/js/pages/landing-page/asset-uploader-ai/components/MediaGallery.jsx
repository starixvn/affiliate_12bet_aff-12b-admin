import React, { useState } from 'react';
import { Button, ButtonGroup, Alert } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { globalResource } from '../../../../modules/globalResource';

const MediaGallery = ({ 
    mediaList, 
    bulkSelected, 
    onMediaClick, 
    onBulkSelect, 
    isLoading,
    pagination,
    onPageChange
}) => {
    const [viewMode, setViewMode] = useState('grid'); // 'grid' or 'table'

    const formatFileSize = (bytes) => {
        if (!bytes) return 'Unknown';
        const sizes = ['B', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(1024));
        return Math.round(bytes / Math.pow(1024, i) * 100) / 100 + ' ' + sizes[i];
    };

    const formatDate = (dateString) => {
        if (!dateString) return 'Unknown';
        const date = new Date(dateString);
        return date.toLocaleDateString() + ' ' + date.toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' });
    };

    const getFileTypeIcon = (fileName) => {
        if (!fileName) return 'oi oi-file';
        const extension = fileName.toLowerCase().split('.').pop();
        switch (extension) {
            case 'jpg':
            case 'jpeg':
            case 'png':
            case 'gif':
            case 'webp':
                return 'oi oi-image';
            case 'pdf':
                return 'oi oi-document';
            default:
                return 'oi oi-file';
        }
    };

    const renderGridView = () => {
        if (!mediaList || mediaList.length === 0) {
            return (
                <div className="text-center py-5">
                    <i className="fas fa-images fa-3x text-muted mb-3"></i>
                    <h5 className="text-muted">No Media Files</h5>
                    <p className="text-muted">Upload some files to see them here</p>
                </div>
            );
        }

        return (
            <div className="media-grid">
                <div className="row">
                    {mediaList.map((media) => (
                        <div key={media.MediaId} className="col-lg-3 col-md-4 col-sm-6 mb-4">
                            <div className="card media-card h-100">
                                <div className="card-img-top-container position-relative">
                                    {media.IsImage ? (
                                        <img 
                                            src={media.ThumbnailPath || media.FullPath} 
                                            alt={media.MediaName}
                                            className="card-img-top media-thumbnail"
                                            style={{ height: '150px', objectFit: 'cover', cursor: 'pointer' }}
                                            onClick={() => onMediaClick(media)}
                                            onError={(e) => {
                                                e.target.src = media.FullPath;
                                            }}
                                        />
                                    ) : (
                                        <div 
                                            className="card-img-top d-flex align-items-center justify-content-center bg-light"
                                            style={{ height: '150px', cursor: 'pointer' }}
                                            onClick={() => onMediaClick(media)}
                                        >
                                            <i className={`${getFileTypeIcon(media.FileName)} fa-3x text-muted`}></i>
                                        </div>
                                    )}
                                    
                                    {/* Selection checkbox */}
                                    <div className="position-absolute" style={{ top: '10px', left: '10px' }}>
                                        <input
                                            type="checkbox"
                                            checked={bulkSelected[media.MediaId] || false}
                                            onChange={(e) => onBulkSelect(media.MediaId)}
                                            className="form-check-input"
                                            style={{ transform: 'scale(1.2)' }}
                                        />
                                    </div>

                                    {/* File size badge */}
                                    <div className="position-absolute" style={{ top: '10px', right: '10px' }}>
                                        <span className="badge badge-dark badge-sm">
                                            {media.FormattedFileSize}
                                        </span>
                                    </div>
                                </div>
                                
                                <div className="card-body p-2">
                                    <h6 className="card-title mb-1 text-truncate" title={media.MediaName}>
                                        {media.MediaName}
                                    </h6>
                                    <p className="card-text small text-muted mb-1">
                                        {media.Size} • {media.FileExtension?.toUpperCase()}
                                    </p>
                                    <p className="card-text small text-muted mb-0">
                                        {formatDate(media.CreatedDate)}
                                    </p>
                                </div>
                                
                                <div className="card-footer p-2 bg-transparent">
                                    <div className="btn-group btn-group-sm w-100" role="group">
                                        <Button 
                                            size="sm" 
                                            color="outline-primary"
                                            onClick={() => onMediaClick(media)}
                                            title="View Details"
                                        >
                                            <i className="oi oi-eye"></i>
                                        </Button>
                                        <Button 
                                            size="sm" 
                                            color="outline-secondary"
                                            onClick={() => window.open(media.FullPath, '_blank')}
                                            title="Download"
                                        >
                                            <i className="oi oi-cloud-download"></i>
                                        </Button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    ))}
                </div>
            </div>
        );
    };

    const renderTableHeader = () => {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={40}></Cell>
                <Cell header={true} colWidth={60}>Preview</Cell>
                <Cell header={true} colWidth={200}>Name</Cell>
                <Cell header={true} colWidth={100}>Type</Cell>
                <Cell header={true} colWidth={80}>Size</Cell>
                <Cell header={true} colWidth={100}>Dimensions</Cell>
                <Cell header={true} colWidth={120}>Created</Cell>
                <Cell header={true} colWidth={100}>Created By</Cell>
                <Cell header={true} colWidth={80}>Actions</Cell>
            </Row>
        );
    };

    const renderTableRows = () => {
        if (!mediaList || mediaList.length === 0) {
            return [
                <Row key="no-data">
                    <Cell className="text-center py-4" colSpan="9">
                        <i className="fas fa-images fa-2x text-muted mb-2"></i>
                        <div>{globalResource.NoInfo || 'No media files found'}</div>
                    </Cell>
                </Row>
            ];
        }

        return mediaList.map((media, index) => (
            <Row key={media.MediaId}>
                <Cell className="text-center">
                    <input
                        type="checkbox"
                        checked={bulkSelected[media.MediaId] || false}
                        onChange={(e) => onBulkSelect(media.MediaId)}
                    />
                </Cell>
                <Cell className="text-center">
                    {media.IsImage ? (
                        <img
                            src={media.ThumbnailPath || media.FullPath}
                            alt={media.MediaName}
                            style={{ width: '40px', height: '40px', objectFit: 'cover', cursor: 'pointer' }}
                            onClick={() => onMediaClick(media)}
                            onError={(e) => {
                                e.target.src = media.FullPath;
                            }}
                        />
                    ) : (
                        <i 
                            className={`${getFileTypeIcon(media.FileName)} fa-2x text-muted`}
                            style={{ cursor: 'pointer' }}
                            onClick={() => onMediaClick(media)}
                        ></i>
                    )}
                </Cell>
                <Cell className="text-truncate" title={media.MediaName}>
                    {media.MediaName}
                </Cell>
                <Cell className="text-center">
                    {media.FileExtension?.toUpperCase()}
                </Cell>
                <Cell className="text-center">
                    {media.FormattedFileSize}
                </Cell>
                <Cell className="text-center">
                    {media.Size}
                </Cell>
                <Cell className="text-center small">
                    {formatDate(media.CreatedDate)}
                </Cell>
                <Cell className="text-center">
                    {media.CreatorName}
                </Cell>
                <Cell className="text-center">
                    <ButtonGroup size="sm">
                        <Button 
                            color="outline-primary" 
                            size="sm"
                            onClick={() => onMediaClick(media)}
                            title="View Details"
                        >
                            <i className="oi oi-eye"></i>
                        </Button>
                        <Button 
                            color="outline-secondary" 
                            size="sm"
                            onClick={() => window.open(media.FullPath, '_blank')}
                            title="Download"
                        >
                            <i className="oi oi-cloud-download"></i>
                        </Button>
                    </ButtonGroup>
                </Cell>
            </Row>
        ));
    };

    const renderTableView = () => {
        return (
            <div className="media-table">
                <Table
                    minWidth={920}
                    autoWidth={true}
                    bodyHeight={400}
                    header={renderTableHeader()}
                    body={renderTableRows()}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={{
                        PageIndex: pagination.pageIndex + 1, // Table component uses 1-based indexing
                        PageSize: pagination.pageSize,
                        TotalItems: pagination.totalItems
                    }}
                    onPaging={(pageIndex, pageSize) => {
                        onPageChange(pageIndex - 1); // Convert back to 0-based indexing
                    }}
                />
            </div>
        );
    };

    if (isLoading) {
        return (
            <div className="text-center py-5">
                <div className="spinner-border" role="status">
                    <span className="sr-only">Loading...</span>
                </div>
                <p className="mt-2 text-muted">Loading media files...</p>
            </div>
        );
    }

    return (
        <div className="media-gallery">
            {/* View Mode Toggle */}
            <div className="d-flex justify-content-between align-items-center mb-3">
                <div>
                    <span className="text-muted small">
                        {mediaList?.length || 0} file(s) • Page {pagination.pageIndex + 1} of {pagination.totalPages}
                    </span>
                </div>
                <ButtonGroup size="sm">
                    <Button 
                        color={viewMode === 'grid' ? 'primary' : 'outline-primary'}
                        onClick={() => setViewMode('grid')}
                        title="Grid View"
                    >
                        <i className="oi oi-grid-three-up"></i>
                    </Button>
                    <Button 
                        color={viewMode === 'table' ? 'primary' : 'outline-primary'}
                        onClick={() => setViewMode('table')}
                        title="Table View"
                    >
                        <i className="oi oi-list"></i>
                    </Button>
                </ButtonGroup>
            </div>

            {/* Content */}
            {viewMode === 'grid' ? renderGridView() : renderTableView()}
        </div>
    );
};

export default React.memo(MediaGallery);
