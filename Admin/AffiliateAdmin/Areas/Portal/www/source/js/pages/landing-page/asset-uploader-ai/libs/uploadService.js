import { getAntiForgeryToken } from '../../../../modules/utils';

// Constants for file upload
export const UPLOAD_CONSTANTS = {
    MAX_FILE_SIZE: 5 * 1024 * 1024, // 5MB
    ALLOWED_EXTENSIONS: ['.jpg', '.jpeg', '.png', '.gif'],
    MODULE_TYPES: {
        IMAGE: 1,
        CONTACT: 2,
        FORM: 3,
        LINK: 4
    },
    CHUNK_SIZE: 1024 * 1024 // 1MB chunks for large files
};

class UploadService {
    constructor() {
        this.baseUrl = '/LandingPage/AssetUploader';
        this.uploadProgress = new Map();
    }

    /**
     * Validate file before upload
     * @param {File} file - File to validate
     * @param {number} moduleId - Module ID
     * @param {number} moduleType - Module type
     * @returns {Promise<Object>} Validation result
     */
    async validateFile(file, moduleId, moduleType) {
        const formData = new FormData();
        formData.append('file', file);
        formData.append('moduleId', moduleId);
        formData.append('moduleType', moduleType);

        try {
            const response = await fetch(`${this.baseUrl}/ValidateMediaFile`, {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': getAntiForgeryToken()
                }
            });

            return await response.json();
        } catch (error) {
            return {
                IsValid: false,
                ValidationMessage: `Validation failed: ${error.message}`
            };
        }
    }

    /**
     * Upload multiple files with progress tracking
     * @param {FileList|Array} files - Files to upload
     * @param {Object} params - Upload parameters
     * @param {Function} onProgress - Progress callback
     * @returns {Promise<Object>} Upload result
     */
    async uploadFiles(files, params, onProgress = null) {
        const formData = new FormData();
        
        // Add files to form data
        Array.from(files).forEach(file => {
            formData.append('files', file);
        });

        // Add parameters
        formData.append('ModuleId', params.moduleId);
        formData.append('ModuleType', params.moduleType);
        formData.append('MediaName', params.mediaName || '');
        formData.append('OverwriteExisting', params.overwriteExisting || false);
        formData.append('Category', params.category || '');
        formData.append('Tags', params.tags || '');

        try {
            const xhr = new XMLHttpRequest();
            const uploadId = this.generateUploadId();

            // Track upload progress
            if (onProgress) {
                xhr.upload.addEventListener('progress', (event) => {
                    if (event.lengthComputable) {
                        const percentComplete = (event.loaded / event.total) * 100;
                        this.uploadProgress.set(uploadId, {
                            loaded: event.loaded,
                            total: event.total,
                            percent: percentComplete
                        });
                        onProgress(percentComplete, uploadId);
                    }
                });
            }

            return new Promise((resolve, reject) => {
                xhr.onreadystatechange = () => {
                    if (xhr.readyState === XMLHttpRequest.DONE) {
                        this.uploadProgress.delete(uploadId);
                        
                        if (xhr.status === 200) {
                            try {
                                const result = JSON.parse(xhr.responseText);
                                resolve(result);
                            } catch (e) {
                                reject(new Error('Invalid response format'));
                            }
                        } else {
                            reject(new Error(`Upload failed with status: ${xhr.status}`));
                        }
                    }
                };

                xhr.open('POST', `${this.baseUrl}/UploadModuleMedia`);
                xhr.setRequestHeader('RequestVerificationToken', getAntiForgeryToken());
                xhr.send(formData);
            });

        } catch (error) {
            throw new Error(`Upload failed: ${error.message}`);
        }
    }

    /**
     * Get media list for a module
     * @param {Object} params - Search parameters
     * @returns {Promise<Object>} Media list result
     */
    async getMediaList(params) {
        const formData = new FormData();
        formData.append('ModuleId', params.moduleId);
        formData.append('PageIndex', params.pageIndex || 0);
        formData.append('PageSize', params.pageSize || 20);
        formData.append('SearchTerm', params.searchTerm || '');
        formData.append('FileType', params.fileType || '');
        formData.append('SizeFilter', params.sizeFilter || '');
        formData.append('IncludeDeleted', params.includeDeleted || false);

        try {
            const response = await fetch(`${this.baseUrl}/GetModuleMediaList`, {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': getAntiForgeryToken()
                }
            });

            return await response.json();
        } catch (error) {
            throw new Error(`Failed to fetch media list: ${error.message}`);
        }
    }

    /**
     * Get media by ID
     * @param {number} mediaId - Media ID
     * @returns {Promise<Object>} Media details
     */
    async getMediaById(mediaId) {
        const formData = new FormData();
        formData.append('mediaId', mediaId);

        try {
            const response = await fetch(`${this.baseUrl}/GetMediaById`, {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': getAntiForgeryToken()
                }
            });

            return await response.json();
        } catch (error) {
            throw new Error(`Failed to fetch media: ${error.message}`);
        }
    }

    /**
     * Delete media files
     * @param {Array} mediaIds - Array of media IDs
     * @param {boolean} permanentDelete - Whether to permanently delete
     * @param {string} deleteReason - Reason for deletion
     * @returns {Promise<Object>} Delete result
     */
    async deleteMedia(mediaIds, permanentDelete = false, deleteReason = '') {
        const formData = new FormData();
        formData.append('MediaIds', mediaIds.join(','));
        formData.append('PermanentDelete', permanentDelete);
        formData.append('DeleteReason', deleteReason);

        try {
            const response = await fetch(`${this.baseUrl}/DeleteModuleMedia`, {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': getAntiForgeryToken()
                }
            });

            return await response.json();
        } catch (error) {
            throw new Error(`Failed to delete media: ${error.message}`);
        }
    }

    /**
     * Update media file
     * @param {number} mediaId - Media ID
     * @param {File} file - New file (optional)
     * @param {Object} params - Update parameters
     * @returns {Promise<Object>} Update result
     */
    async updateMedia(mediaId, file, params) {
        const formData = new FormData();
        formData.append('MediaId', mediaId);
        formData.append('MediaName', params.mediaName);
        formData.append('Size', params.size || '');
        formData.append('Category', params.category || '');
        formData.append('Tags', params.tags || '');
        formData.append('IsActive', params.isActive !== false);

        if (file) {
            formData.append('file', file);
        }

        try {
            const response = await fetch(`${this.baseUrl}/UpdateModuleMedia`, {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': getAntiForgeryToken()
                }
            });

            return await response.json();
        } catch (error) {
            throw new Error(`Failed to update media: ${error.message}`);
        }
    }

    /**
     * Get media statistics for a module
     * @param {number} moduleId - Module ID
     * @returns {Promise<Object>} Statistics
     */
    async getMediaStats(moduleId) {
        const formData = new FormData();
        formData.append('moduleId', moduleId);

        try {
            const response = await fetch(`${this.baseUrl}/GetModuleMediaStats`, {
                method: 'POST',
                body: formData,
                headers: {
                    'RequestVerificationToken': getAntiForgeryToken()
                }
            });

            return await response.json();
        } catch (error) {
            throw new Error(`Failed to fetch statistics: ${error.message}`);
        }
    }

    /**
     * Get media root URL configuration
     * @returns {Promise<string>} Root URL
     */
    async getMediaRootUrl() {
        try {
            const response = await fetch(`${this.baseUrl}/GetMediaRootUrl`, {
                method: 'POST',
                headers: {
                    'RequestVerificationToken': getAntiForgeryToken()
                }
            });

            const result = await response.json();
            return result.Data || result;
        } catch (error) {
            throw new Error(`Failed to fetch root URL: ${error.message}`);
        }
    }

    /**
     * Client-side file validation
     * @param {File} file - File to validate
     * @param {number} moduleType - Module type
     * @returns {Object} Validation result
     */
    validateFileClient(file, moduleType) {
        const errors = [];

        // Check file size
        if (file.size > UPLOAD_CONSTANTS.MAX_FILE_SIZE) {
            errors.push(`File size (${this.formatFileSize(file.size)}) exceeds 5MB limit`);
        }

        // Check file extension
        const extension = this.getFileExtension(file.name);
        if (!UPLOAD_CONSTANTS.ALLOWED_EXTENSIONS.includes(extension)) {
            errors.push(`File type ${extension} is not allowed. Allowed types: ${UPLOAD_CONSTANTS.ALLOWED_EXTENSIONS.join(', ')}`);
        }

        // Check if link modules allow uploads
        if (moduleType === UPLOAD_CONSTANTS.MODULE_TYPES.LINK) {
            errors.push('Link modules do not support file uploads');
        }

        return {
            isValid: errors.length === 0,
            errors: errors
        };
    }

    /**
     * Generate unique upload ID
     * @returns {string} Upload ID
     */
    generateUploadId() {
        return `upload_${Date.now()}_${Math.random().toString(36).substr(2, 9)}`;
    }

    /**
     * Get file extension
     * @param {string} filename - File name
     * @returns {string} Extension
     */
    getFileExtension(filename) {
        return filename.toLowerCase().substring(filename.lastIndexOf('.'));
    }

    /**
     * Format file size for display
     * @param {number} bytes - File size in bytes
     * @returns {string} Formatted size
     */
    formatFileSize(bytes) {
        const sizes = ['B', 'KB', 'MB', 'GB'];
        if (bytes === 0) return '0 B';
        const i = Math.floor(Math.log(bytes) / Math.log(1024));
        return Math.round(bytes / Math.pow(1024, i) * 100) / 100 + ' ' + sizes[i];
    }

    /**
     * Get upload progress
     * @param {string} uploadId - Upload ID
     * @returns {Object} Progress info
     */
    getUploadProgress(uploadId) {
        return this.uploadProgress.get(uploadId);
    }

    /**
     * Cancel upload
     * @param {string} uploadId - Upload ID
     */
    cancelUpload(uploadId) {
        this.uploadProgress.delete(uploadId);
        // Note: XMLHttpRequest cancellation would need to be implemented
        // if we store the xhr objects
    }
}

// Create singleton instance
const uploadService = new UploadService();

/**
 * Client-side file validation
 */
uploadService.validateFileClient = (file, moduleType) => {
    const errors = [];
    const maxSize = 5 * 1024 * 1024; // 5MB
    const allowedImageTypes = ['.jpg', '.jpeg', '.png', '.gif'];
    
    if (!file) {
        errors.push('No file provided');
        return { isValid: false, errors };
    }

    // File size validation
    if (file.size > maxSize) {
        errors.push(`File size (${uploadService.formatFileSize(file.size)}) exceeds 5MB limit`);
    }

    // File type validation based on module type
    const extension = file.name.toLowerCase().substring(file.name.lastIndexOf('.'));
    
    switch (moduleType) {
        case 1: // Image modules
            if (!allowedImageTypes.includes(extension)) {
                errors.push(`Invalid file type. Allowed: ${allowedImageTypes.join(', ')}`);
            }
            break;
        case 2: // Contact modules
        case 3: // Form modules
            if (!allowedImageTypes.includes(extension)) {
                errors.push(`Invalid file type. Allowed: ${allowedImageTypes.join(', ')}`);
            }
            break;
        case 4: // Link modules
            errors.push('Link modules do not support file uploads');
            break;
    }

    return {
        isValid: errors.length === 0,
        errors
    };
};

// Export singleton instance
export default uploadService;
