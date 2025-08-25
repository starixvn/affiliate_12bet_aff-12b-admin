import React, { useState } from 'react';
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

const MediaModal = ({ show, media, onHide, onUpdate, onDelete }) => {
    const [editMode, setEditMode] = useState(false);
    const [formData, setFormData] = useState({
        mediaName: '',
        category: '',
        tags: '',
        isActive: true
    });
    const [newFile, setNewFile] = useState(null);
    const [errors, setErrors] = useState([]);

    React.useEffect(() => {
        if (media) {
            setFormData({
                mediaName: media.MediaName || '',
                category: media.Category || '',
                tags: media.Tags || '',
                isActive: media.IsActive !== false
            });
        }
        setEditMode(false);
        setNewFile(null);
        setErrors([]);
    }, [media]);

    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        setFormData(prev => ({
            ...prev,
            [name]: type === 'checkbox' ? checked : value
        }));
    };

    const handleFileChange = (e) => {
        const file = e.target.files[0];
        if (file) {
            // Basic validation
            const maxSize = 5 * 1024 * 1024; // 5MB
            const allowedTypes = ['.jpg', '.jpeg', '.png', '.gif'];
            const extension = file.name.toLowerCase().substring(file.name.lastIndexOf('.'));
            
            const validationErrors = [];
            if (file.size > maxSize) {
                validationErrors.push('File size exceeds 5MB limit');
            }
            if (!allowedTypes.includes(extension)) {
                validationErrors.push('Invalid file type. Allowed: JPG, PNG, GIF');
            }
            
            if (validationErrors.length > 0) {
                setErrors(validationErrors);
                setNewFile(null);
            } else {
                setErrors([]);
                setNewFile(file);
            }
        }
    };

    const handleSave = () => {
        if (!formData.mediaName.trim()) {
            setErrors(['Media name is required']);
            return;
        }

        const updateData = {
            mediaName: formData.mediaName.trim(),
            category: formData.category.trim(),
            tags: formData.tags.trim(),
            isActive: formData.isActive
        };

        onUpdate(media.MediaId, newFile, updateData);
        setEditMode(false);
        setNewFile(null);
        setErrors([]);
    };

    const handleDelete = () => {
        if (confirm(`Are you sure you want to delete "${media.MediaName}"?`)) {
            onDelete(media.MediaId);
        }
    };

    const formatDate = (dateString) => {
        if (!dateString) return 'Unknown';
        return new Date(dateString).toLocaleString();
    };

    const formatFileSize = (bytes) => {
        if (!bytes) return 'Unknown';
        const sizes = ['B', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(1024));
        return Math.round(bytes / Math.pow(1024, i) * 100) / 100 + ' ' + sizes[i];
    };

    if (!media) return null;

    return (
        <Modal isOpen={show} toggle={onHide} size="lg" backdrop="static">
            <ModalHeader toggle={onHide}>
                <div className="d-flex align-items-center">
                    <i className="oi oi-image mr-2"></i>
                    Media Details
                    {media.IsDeleted && (
                        <Badge color="danger" className="ml-2">Deleted</Badge>
                    )}
                </div>
            </ModalHeader>
            
            <ModalBody>
                {/* Error Messages */}
                {errors.length > 0 && (
                    <Alert color="danger">
                        <ul className="mb-0">
                            {errors.map((error, index) => (
                                <li key={index}>{error}</li>
                            ))}
                        </ul>
                    </Alert>
                )}

                <div className="row">
                    {/* Media Preview */}
                    <div className="col-md-6">
                        <div className="media-preview text-center mb-3">
                            {media.IsImage ? (
                                <div>
                                    <img
                                        src={media.FullPath}
                                        alt={media.MediaName}
                                        className="img-fluid rounded shadow"
                                        style={{ maxHeight: '300px', maxWidth: '100%' }}
                                        onError={(e) => {
                                            e.target.src = '/images/no-image.png';
                                        }}
                                    />
                                    <div className="mt-2">
                                        <Button
                                            size="sm"
                                            color="outline-primary"
                                            onClick={() => window.open(media.FullPath, '_blank')}
                                        >
                                            <i className="oi oi-external-link"></i> View Full Size
                                        </Button>
                                    </div>
                                </div>
                            ) : (
                                <div className="non-image-preview p-4 bg-light rounded">
                                    <i className="oi oi-document fa-4x text-muted mb-3"></i>
                                    <div className="h5">{media.MediaName}</div>
                                    <div className="text-muted">{media.FileExtension?.toUpperCase()} File</div>
                                    <Button
                                        size="sm"
                                        color="outline-primary"
                                        className="mt-2"
                                        onClick={() => window.open(media.FullPath, '_blank')}
                                    >
                                        <i className="oi oi-cloud-download"></i> Download
                                    </Button>
                                </div>
                            )}
                        </div>
                    </div>

                    {/* Media Information */}
                    <div className="col-md-6">
                        {editMode ? (
                            /* Edit Form */
                            <Form>
                                <FormGroup>
                                    <Label for="mediaName">Media Name <span className="text-danger">*</span></Label>
                                    <Input
                                        type="text"
                                        name="mediaName"
                                        id="mediaName"
                                        value={formData.mediaName}
                                        onChange={handleInputChange}
                                        placeholder="Enter media name"
                                    />
                                </FormGroup>

                                <FormGroup>
                                    <Label for="category">Category</Label>
                                    <Input
                                        type="text"
                                        name="category"
                                        id="category"
                                        value={formData.category}
                                        onChange={handleInputChange}
                                        placeholder="Enter category"
                                    />
                                </FormGroup>

                                <FormGroup>
                                    <Label for="tags">Tags</Label>
                                    <Input
                                        type="text"
                                        name="tags"
                                        id="tags"
                                        value={formData.tags}
                                        onChange={handleInputChange}
                                        placeholder="Enter tags (comma separated)"
                                    />
                                </FormGroup>

                                <FormGroup>
                                    <Label for="replaceFile">Replace File</Label>
                                    <Input
                                        type="file"
                                        name="replaceFile"
                                        id="replaceFile"
                                        accept=".jpg,.jpeg,.png,.gif"
                                        onChange={handleFileChange}
                                    />
                                    {newFile && (
                                        <div className="small text-success mt-1">
                                            <i className="oi oi-check"></i> New file selected: {newFile.name}
                                        </div>
                                    )}
                                </FormGroup>

                                <FormGroup check>
                                    <Label check>
                                        <Input
                                            type="checkbox"
                                            name="isActive"
                                            checked={formData.isActive}
                                            onChange={handleInputChange}
                                        />
                                        Active
                                    </Label>
                                </FormGroup>
                            </Form>
                        ) : (
                            /* View Mode */
                            <div className="media-info">
                                <div className="info-group mb-3">
                                    <label className="small font-weight-bold text-muted">MEDIA NAME</label>
                                    <div className="h6">{media.MediaName}</div>
                                </div>

                                <div className="info-group mb-3">
                                    <label className="small font-weight-bold text-muted">FILE INFO</label>
                                    <div>
                                        <div><strong>File Name:</strong> {media.FileName}</div>
                                        <div><strong>Type:</strong> {media.FileExtension?.toUpperCase()}</div>
                                        <div><strong>Size:</strong> {media.FormattedFileSize}</div>
                                        {media.Size && media.Size.includes('x') && (
                                            <div><strong>Dimensions:</strong> {media.Size}</div>
                                        )}
                                    </div>
                                </div>

                                <div className="info-group mb-3">
                                    <label className="small font-weight-bold text-muted">MODULE INFO</label>
                                    <div>
                                        <div><strong>Module:</strong> {media.ModuleName}</div>
                                        <div><strong>Module ID:</strong> {media.ModuleId}</div>
                                    </div>
                                </div>

                                <div className="info-group mb-3">
                                    <label className="small font-weight-bold text-muted">TIMESTAMPS</label>
                                    <div>
                                        <div><strong>Created:</strong> {formatDate(media.CreatedDate)}</div>
                                        <div><strong>Created By:</strong> {media.CreatorName}</div>
                                        {media.UpdatedDate && (
                                            <>
                                                <div><strong>Updated:</strong> {formatDate(media.UpdatedDate)}</div>
                                                <div><strong>Updated By:</strong> {media.UpdatedByName}</div>
                                            </>
                                        )}
                                    </div>
                                </div>

                                {(media.Category || media.Tags) && (
                                    <div className="info-group mb-3">
                                        <label className="small font-weight-bold text-muted">METADATA</label>
                                        <div>
                                            {media.Category && (
                                                <div><strong>Category:</strong> {media.Category}</div>
                                            )}
                                            {media.Tags && (
                                                <div><strong>Tags:</strong> {media.Tags}</div>
                                            )}
                                        </div>
                                    </div>
                                )}
                            </div>
                        )}
                    </div>
                </div>
            </ModalBody>

            <ModalFooter>
                <div className="d-flex justify-content-between w-100">
                    <div>
                        {!media.IsDeleted && (
                            <Button color="outline-danger" onClick={handleDelete}>
                                <i className="oi oi-trash"></i> Delete
                            </Button>
                        )}
                    </div>
                    
                    <ButtonGroup>
                        {editMode ? (
                            <>
                                <Button color="outline-secondary" onClick={() => setEditMode(false)}>
                                    Cancel
                                </Button>
                                <Button color="primary" onClick={handleSave}>
                                    <i className="oi oi-check"></i> Save Changes
                                </Button>
                            </>
                        ) : (
                            <>
                                <Button color="outline-secondary" onClick={onHide}>
                                    Close
                                </Button>
                                {!media.IsDeleted && (
                                    <Button color="primary" onClick={() => setEditMode(true)}>
                                        <i className="oi oi-pencil"></i> Edit
                                    </Button>
                                )}
                            </>
                        )}
                    </ButtonGroup>
                </div>
            </ModalFooter>
        </Modal>
    );
};

export default React.memo(MediaModal);
