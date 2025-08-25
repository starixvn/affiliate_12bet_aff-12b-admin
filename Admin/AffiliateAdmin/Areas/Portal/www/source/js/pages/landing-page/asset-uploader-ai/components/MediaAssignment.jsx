import React, { useState, useEffect } from 'react';
import { 
    Card, 
    CardHeader, 
    CardBody, 
    Button, 
    Badge, 
    Alert,
    ButtonGroup,
    UncontrolledTooltip
} from 'reactstrap';
import MediaPicker from './MediaPicker';
import { pageBuilderService, mediaHelpers } from '../libs/pageBuilderService';

/**
 * MediaAssignment Component
 * Manages media assignments for modules in the page builder
 */
const MediaAssignment = ({ 
    moduleId, 
    moduleType, 
    pageId, 
    moduleName,
    onMediaAssigned,
    onMediaRemoved,
    allowMultiple = false,
    maxAssignments = 1
}) => {
    const [assignedMedia, setAssignedMedia] = useState([]);
    const [showMediaPicker, setShowMediaPicker] = useState(false);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    useEffect(() => {
        if (moduleId && pageId) {
            loadAssignedMedia();
        }
    }, [moduleId, pageId]);

    const loadAssignedMedia = async () => {
        setLoading(true);
        setError(null);
        
        try {
            const result = await pageBuilderService.getModuleAssignedMedia(moduleId, pageId);
            if (result && result.IsSuccessful) {
                setAssignedMedia(result.Data || []);
            } else {
                setError(result?.Message || 'Failed to load assigned media');
            }
        } catch (err) {
            console.error('Error loading assigned media:', err);
            setError('Error loading assigned media: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    const handleMediaSelect = async (selectedMedia) => {
        if (!selectedMedia) return;

        const mediaArray = Array.isArray(selectedMedia) ? selectedMedia : [selectedMedia];
        
        try {
            setLoading(true);
            setError(null);

            for (const media of mediaArray) {
                // Validate media for this module type
                const validation = mediaHelpers.validateMediaForModule(media, moduleType);
                if (!validation.isValid) {
                    setError(`Invalid media "${media.MediaName}": ${validation.errors.join(', ')}`);
                    continue;
                }

                const param = {
                    pageId: pageId,
                    moduleId: moduleId,
                    mediaId: media.MediaId,
                    mediaRole: 'primary', // Default role
                    displayOrder: assignedMedia.length + 1,
                    isActive: true
                };

                const result = await pageBuilderService.assignMediaToModule(param);
                if (result && result.IsSuccessful) {
                    // Reload assigned media to get updated list
                    await loadAssignedMedia();
                    
                    if (onMediaAssigned) {
                        onMediaAssigned(media, moduleId);
                    }
                } else {
                    setError(result?.Message || 'Failed to assign media');
                }
            }
        } catch (err) {
            console.error('Error assigning media:', err);
            setError('Error assigning media: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    const handleMediaRemove = async (mediaId) => {
        if (!confirm('Are you sure you want to remove this media assignment?')) {
            return;
        }

        try {
            setLoading(true);
            setError(null);

            const param = {
                pageId: pageId,
                moduleId: moduleId,
                mediaId: mediaId
            };

            const result = await pageBuilderService.removeMediaFromModule(param);
            if (result && result.IsSuccessful) {
                // Reload assigned media to get updated list
                await loadAssignedMedia();
                
                if (onMediaRemoved) {
                    onMediaRemoved(mediaId, moduleId);
                }
            } else {
                setError(result?.Message || 'Failed to remove media assignment');
            }
        } catch (err) {
            console.error('Error removing media:', err);
            setError('Error removing media: ' + err.message);
        } finally {
            setLoading(false);
        }
    };

    const canAddMore = allowMultiple && assignedMedia.length < maxAssignments;
    const canAddAny = !allowMultiple && assignedMedia.length === 0;

    const renderAssignedMedia = () => {
        if (loading) {
            return (
                <div className="text-center py-3">
                    <div className="spinner-border spinner-border-sm" role="status">
                        <span className="sr-only">Loading...</span>
                    </div>
                    <div className="small text-muted mt-1">Loading assigned media...</div>
                </div>
            );
        }

        if (assignedMedia.length === 0) {
            return (
                <div className="text-center py-3">
                    <i className="oi oi-image fa-2x text-muted mb-2"></i>
                    <div className="small text-muted">No media assigned</div>
                    <div className="small text-muted">Click "Add Media" to assign media to this module</div>
                </div>
            );
        }

        return (
            <div className="assigned-media-list">
                {assignedMedia.map((assignment, index) => (
                    <div key={assignment.MediaId} className="assigned-media-item mb-2">
                        <div className="d-flex align-items-center">
                            {/* Media Thumbnail */}
                            <div className="media-thumbnail mr-3">
                                {assignment.IsImage ? (
                                    <img
                                        src={assignment.ThumbnailPath || assignment.FullPath}
                                        alt={assignment.MediaName}
                                        className="img-thumbnail"
                                        style={{ width: '60px', height: '60px', objectFit: 'cover' }}
                                        onError={(e) => {
                                            e.target.src = assignment.FullPath;
                                        }}
                                    />
                                ) : (
                                    <div 
                                        className="d-flex align-items-center justify-content-center bg-light border rounded"
                                        style={{ width: '60px', height: '60px' }}
                                    >
                                        <i className={`${mediaHelpers.getFileTypeIcon(assignment.FileName)} fa-lg text-muted`}></i>
                                    </div>
                                )}
                            </div>

                            {/* Media Info */}
                            <div className="media-info flex-grow-1">
                                <div className="font-weight-medium">{assignment.MediaName}</div>
                                <div className="small text-muted">
                                    {assignment.FormattedFileSize} • {assignment.FileExtension?.toUpperCase()}
                                    {assignment.Size && (
                                        <> • {assignment.Size}</>
                                    )}
                                </div>
                                <div className="small">
                                    <Badge color="info" className="mr-1">
                                        {mediaHelpers.getMediaRoleDisplayName(assignment.MediaRole)}
                                    </Badge>
                                    {assignment.DisplayOrder > 1 && (
                                        <Badge color="secondary" className="mr-1">
                                            Order: {assignment.DisplayOrder}
                                        </Badge>
                                    )}
                                    {!assignment.IsActive && (
                                        <Badge color="warning">Inactive</Badge>
                                    )}
                                </div>
                            </div>

                            {/* Actions */}
                            <div className="media-actions">
                                <ButtonGroup size="sm">
                                    <Button
                                        color="outline-primary"
                                        onClick={() => window.open(assignment.FullPath, '_blank')}
                                        title="View Full Size"
                                        id={`view-${assignment.MediaId}`}
                                    >
                                        <i className="oi oi-eye"></i>
                                    </Button>
                                    <UncontrolledTooltip target={`view-${assignment.MediaId}`}>
                                        View Full Size
                                    </UncontrolledTooltip>
                                    
                                    <Button
                                        color="outline-danger"
                                        onClick={() => handleMediaRemove(assignment.MediaId)}
                                        title="Remove Assignment"
                                        id={`remove-${assignment.MediaId}`}
                                        disabled={loading}
                                    >
                                        <i className="oi oi-trash"></i>
                                    </Button>
                                    <UncontrolledTooltip target={`remove-${assignment.MediaId}`}>
                                        Remove Assignment
                                    </UncontrolledTooltip>
                                </ButtonGroup>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        );
    };

    return (
        <Card className="media-assignment-card">
            <CardHeader className="py-2">
                <div className="d-flex justify-content-between align-items-center">
                    <div>
                        <i className="oi oi-image mr-1"></i>
                        <strong>Media Assignment</strong>
                        {moduleName && (
                            <span className="text-muted ml-2">for {moduleName}</span>
                        )}
                    </div>
                    <div>
                        {assignedMedia.length > 0 && (
                            <Badge color="primary" className="mr-2">
                                {assignedMedia.length} assigned
                            </Badge>
                        )}
                        {(canAddMore || canAddAny) && (
                            <Button
                                size="sm"
                                color="outline-primary"
                                onClick={() => setShowMediaPicker(true)}
                                disabled={loading}
                            >
                                <i className="oi oi-plus"></i> Add Media
                            </Button>
                        )}
                    </div>
                </div>
            </CardHeader>

            <CardBody>
                {/* Error Display */}
                {error && (
                    <Alert color="danger" className="mb-3">
                        {error}
                        <Button 
                            size="sm" 
                            color="outline-danger" 
                            className="float-right"
                            onClick={loadAssignedMedia}
                        >
                            Retry
                        </Button>
                    </Alert>
                )}

                {/* Assigned Media List */}
                {renderAssignedMedia()}

                {/* Assignment Limits Info */}
                {allowMultiple && maxAssignments > 1 && (
                    <div className="assignment-limits mt-3 p-2 bg-light rounded">
                        <div className="small text-muted">
                            <i className="oi oi-info"></i>
                            {assignedMedia.length} of {maxAssignments} media assignments used
                            {assignedMedia.length >= maxAssignments && (
                                <span className="text-warning ml-1">(Limit reached)</span>
                            )}
                        </div>
                    </div>
                )}
            </CardBody>

            {/* Media Picker Modal */}
            <MediaPicker
                show={showMediaPicker}
                onHide={() => setShowMediaPicker(false)}
                onSelectMedia={handleMediaSelect}
                moduleId={moduleId}
                moduleType={moduleType}
                allowMultiple={allowMultiple && canAddMore}
                selectedMediaIds={assignedMedia.map(m => m.MediaId)}
                title={`Select Media for ${moduleName || 'Module'}`}
            />
        </Card>
    );
};

export default React.memo(MediaAssignment);
