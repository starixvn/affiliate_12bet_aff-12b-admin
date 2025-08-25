import React, { useState, useEffect } from 'react';
import { 
    Modal, 
    ModalHeader, 
    ModalBody, 
    ModalFooter, 
    Button, 
    Form, 
    FormGroup, 
    Label, 
    Input,
    Alert,
    ButtonGroup,
    Badge
} from 'reactstrap';
import { post } from '../../../../modules/ajaxRequest';
import { globalResource } from '../../../../modules/globalResource';

const MediaPicker = ({ 
    show, 
    onHide, 
    onSelectMedia, 
    moduleId, 
    moduleType,
    allowMultiple = false,
    selectedMediaIds = [],
    title = "Select Media"
}) => {
    const [mediaList, setMediaList] = useState([]);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);
    const [selectedItems, setSelectedItems] = useState(new Set(selectedMediaIds));
    const [filters, setFilters] = useState({
        searchTerm: '',
        fileType: '',
        pageIndex: 1,
        pageSize: 20
    });
    const [pagination, setPagination] = useState({
        totalRecords: 0,
        totalPages: 0,
        currentPage: 1
    });

    useEffect(() => {
        if (show && moduleId) {
            loadMediaList();
        }
    }, [show, moduleId, filters]);

    useEffect(() => {
        setSelectedItems(new Set(selectedMediaIds));
    }, [selectedMediaIds]);

    const loadMediaList = async () => {
        setLoading(true);
        setError(null);
        
        try {
            const param = {
                moduleId: moduleId,
                searchTerm: filters.searchTerm,
                fileType: filters.fileType,
                pageIndex: filters.pageIndex,
                pageSize: filters.pageSize
            };

            const response = await post('/LandingPage/PageBuilder/GetMediaPickerList', param);
            
            if (response && response.status === 200 && response.data) {
                setMediaList(response.data.DataList || []);
                setPagination({
                    totalRecords: response.data.TotalRecords || 0,
                    totalPages: Math.ceil((response.data.TotalRecords || 0) / filters.pageSize),
                    currentPage: filters.pageIndex
                });
            } else {
                setError('Failed to load media list');
            }
        } catch (err) {
            console.error('Error loading media:', err);
            setError('Error loading media: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    const handleFilterChange = (newFilters) => {
        setFilters(prev => ({
            ...prev,
            ...newFilters,
            pageIndex: 1 // Reset to first page when filtering
        }));
    };

    const handlePageChange = (newPage) => {
        setFilters(prev => ({
            ...prev,
            pageIndex: newPage
        }));
    };

    const handleMediaSelect = (mediaId, media) => {
        if (allowMultiple) {
            const newSelected = new Set(selectedItems);
            if (newSelected.has(mediaId)) {
                newSelected.delete(mediaId);
            } else {
                newSelected.add(mediaId);
            }
            setSelectedItems(newSelected);
        } else {
            setSelectedItems(new Set([mediaId]));
        }
    };

    const handleConfirmSelection = () => {
        const selectedMedia = mediaList.filter(media => selectedItems.has(media.MediaId));
        onSelectMedia(allowMultiple ? selectedMedia : selectedMedia[0]);
        onHide();
    };

    const formatFileSize = (bytes) => {
        if (!bytes) return 'Unknown';
        const sizes = ['B', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(1024));
        return Math.round(bytes / Math.pow(1024, i) * 100) / 100 + ' ' + sizes[i];
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

    const renderMediaGrid = () => {
        if (loading) {
            return (
                <div className="text-center py-5">
                    <div className="spinner-border" role="status">
                        <span className="sr-only">Loading...</span>
                    </div>
                    <p className="mt-2 text-muted">Loading media files...</p>
                </div>
            );
        }

        if (mediaList.length === 0) {
            return (
                <div className="text-center py-5">
                    <i className="fas fa-images fa-3x text-muted mb-3"></i>
                    <h5 className="text-muted">No Media Files</h5>
                    <p className="text-muted">No media files found for this module</p>
                </div>
            );
        }

        return (
            <div className="media-picker-grid">
                <div className="row">
                    {mediaList.map((media) => {
                        const isSelected = selectedItems.has(media.MediaId);
                        return (
                            <div key={media.MediaId} className="col-lg-3 col-md-4 col-sm-6 mb-3">
                                <div 
                                    className={`card media-picker-card ${isSelected ? 'selected' : ''}`}
                                    onClick={() => handleMediaSelect(media.MediaId, media)}
                                    style={{ cursor: 'pointer', position: 'relative' }}
                                >
                                    {/* Selection Indicator */}
                                    {isSelected && (
                                        <div className="selection-indicator">
                                            <i className="oi oi-check text-white"></i>
                                        </div>
                                    )}

                                    <div className="card-img-top-container position-relative">
                                        {media.IsImage ? (
                                            <img 
                                                src={media.ThumbnailPath || media.FullPath} 
                                                alt={media.MediaName}
                                                className="card-img-top"
                                                style={{ height: '120px', objectFit: 'cover' }}
                                                onError={(e) => {
                                                    e.target.src = media.FullPath;
                                                }}
                                            />
                                        ) : (
                                            <div 
                                                className="card-img-top d-flex align-items-center justify-content-center bg-light"
                                                style={{ height: '120px' }}
                                            >
                                                <i className={`${getFileTypeIcon(media.FileName)} fa-2x text-muted`}></i>
                                            </div>
                                        )}
                                        
                                        {/* File size badge */}
                                        <div className="position-absolute" style={{ top: '5px', right: '5px' }}>
                                            <Badge color="dark" className="badge-sm">
                                                {media.FormattedFileSize}
                                            </Badge>
                                        </div>
                                    </div>
                                    
                                    <div className="card-body p-2">
                                        <h6 className="card-title mb-1 text-truncate small" title={media.MediaName}>
                                            {media.MediaName}
                                        </h6>
                                        <p className="card-text small text-muted mb-0">
                                            {media.Size} • {media.FileExtension?.toUpperCase()}
                                        </p>
                                    </div>
                                </div>
                            </div>
                        );
                    })}
                </div>
            </div>
        );
    };

    const renderPagination = () => {
        if (pagination.totalPages <= 1) return null;

        const pages = [];
        const currentPage = pagination.currentPage;
        const totalPages = pagination.totalPages;
        
        // Previous button
        pages.push(
            <Button
                key="prev"
                size="sm"
                color="outline-secondary"
                disabled={currentPage === 1}
                onClick={() => handlePageChange(currentPage - 1)}
            >
                <i className="oi oi-chevron-left"></i>
            </Button>
        );

        // Page numbers
        for (let i = Math.max(1, currentPage - 2); i <= Math.min(totalPages, currentPage + 2); i++) {
            pages.push(
                <Button
                    key={i}
                    size="sm"
                    color={i === currentPage ? "primary" : "outline-secondary"}
                    onClick={() => handlePageChange(i)}
                >
                    {i}
                </Button>
            );
        }

        // Next button
        pages.push(
            <Button
                key="next"
                size="sm"
                color="outline-secondary"
                disabled={currentPage === totalPages}
                onClick={() => handlePageChange(currentPage + 1)}
            >
                <i className="oi oi-chevron-right"></i>
            </Button>
        );

        return (
            <div className="d-flex justify-content-center mt-3">
                <ButtonGroup>
                    {pages}
                </ButtonGroup>
            </div>
        );
    };

    return (
        <Modal isOpen={show} toggle={onHide} size="xl" backdrop="static">
            <ModalHeader toggle={onHide}>
                <div className="d-flex align-items-center">
                    <i className="oi oi-image mr-2"></i>
                    {title}
                    {selectedItems.size > 0 && (
                        <Badge color="primary" className="ml-2">
                            {selectedItems.size} selected
                        </Badge>
                    )}
                </div>
            </ModalHeader>
            
            <ModalBody style={{ minHeight: '500px' }}>
                {/* Error Display */}
                {error && (
                    <Alert color="danger" className="mb-3">
                        {error}
                        <Button 
                            size="sm" 
                            color="outline-danger" 
                            className="float-right"
                            onClick={loadMediaList}
                        >
                            Retry
                        </Button>
                    </Alert>
                )}

                {/* Filters */}
                <div className="media-picker-filters mb-3">
                    <Form inline>
                        <FormGroup className="mr-3">
                            <Label for="searchTerm" className="mr-2 small">Search:</Label>
                            <Input
                                type="text"
                                name="searchTerm"
                                id="searchTerm"
                                placeholder="Search media..."
                                value={filters.searchTerm}
                                onChange={(e) => handleFilterChange({ searchTerm: e.target.value })}
                                size="sm"
                                style={{ width: '200px' }}
                            />
                        </FormGroup>
                        
                        <FormGroup className="mr-3">
                            <Label for="fileType" className="mr-2 small">Type:</Label>
                            <Input
                                type="select"
                                name="fileType"
                                id="fileType"
                                value={filters.fileType}
                                onChange={(e) => handleFilterChange({ fileType: e.target.value })}
                                size="sm"
                                style={{ width: '120px' }}
                            >
                                <option value="">{globalResource.All || 'All'}</option>
                                <option value="image">Images</option>
                                <option value="document">Documents</option>
                            </Input>
                        </FormGroup>

                        <Button 
                            size="sm" 
                            color="outline-primary"
                            onClick={loadMediaList}
                            disabled={loading}
                        >
                            <i className="oi oi-magnifying-glass"></i> Search
                        </Button>
                    </Form>
                </div>

                {/* Media Grid */}
                {renderMediaGrid()}

                {/* Pagination */}
                {renderPagination()}

                {/* Selection Summary */}
                {pagination.totalRecords > 0 && (
                    <div className="selection-summary mt-3 p-2 bg-light rounded">
                        <div className="d-flex justify-content-between align-items-center">
                            <span className="small text-muted">
                                Showing {mediaList.length} of {pagination.totalRecords} files
                            </span>
                            {selectedItems.size > 0 && (
                                <span className="small font-weight-medium">
                                    {selectedItems.size} file{selectedItems.size !== 1 ? 's' : ''} selected
                                </span>
                            )}
                        </div>
                    </div>
                )}
            </ModalBody>

            <ModalFooter>
                <div className="d-flex justify-content-between w-100">
                    <div>
                        {selectedItems.size > 0 && (
                            <Button 
                                color="outline-secondary" 
                                size="sm"
                                onClick={() => setSelectedItems(new Set())}
                            >
                                Clear Selection
                            </Button>
                        )}
                    </div>
                    
                    <ButtonGroup>
                        <Button color="outline-secondary" onClick={onHide}>
                            Cancel
                        </Button>
                        <Button 
                            color="primary" 
                            onClick={handleConfirmSelection}
                            disabled={selectedItems.size === 0}
                        >
                            <i className="oi oi-check"></i> 
                            Select {selectedItems.size > 0 ? `(${selectedItems.size})` : ''}
                        </Button>
                    </ButtonGroup>
                </div>
            </ModalFooter>

            <style jsx>{`
                .media-picker-card {
                    transition: all 0.2s ease;
                }
                
                .media-picker-card:hover {
                    transform: translateY(-2px);
                    box-shadow: 0 4px 8px rgba(0,0,0,0.15);
                }
                
                .media-picker-card.selected {
                    border-color: #007bff;
                    box-shadow: 0 0 0 2px rgba(0,123,255,0.25);
                }
                
                .selection-indicator {
                    position: absolute;
                    top: 5px;
                    left: 5px;
                    background-color: #007bff;
                    border-radius: 50%;
                    width: 24px;
                    height: 24px;
                    display: flex;
                    align-items: center;
                    justify-content: center;
                    z-index: 10;
                }
            `}</style>
        </Modal>
    );
};

export default React.memo(MediaPicker);
