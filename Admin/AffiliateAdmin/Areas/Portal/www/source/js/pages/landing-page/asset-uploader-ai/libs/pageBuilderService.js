import { post } from '../../../../modules/ajaxRequest';

const API_BASE_PATH = '/LandingPage/PageBuilder';

/**
 * Page Builder API Service for Media Integration
 */
export const pageBuilderService = {
    /**
     * Get media list for picker modal
     */
    getMediaPickerList: async (param) => {
        const response = await post(`${API_BASE_PATH}/GetMediaPickerList`, param);
        return response.data;
    },

    /**
     * Assign media to module in page builder
     */
    assignMediaToModule: async (param) => {
        const response = await post(`${API_BASE_PATH}/AssignMediaToModule`, param);
        return response.data;
    },

    /**
     * Remove media assignment from module
     */
    removeMediaFromModule: async (param) => {
        const response = await post(`${API_BASE_PATH}/RemoveMediaFromModule`, param);
        return response.data;
    },

    /**
     * Get assigned media for a specific module
     */
    getModuleAssignedMedia: async (moduleId, pageId) => {
        const response = await post(`${API_BASE_PATH}/GetModuleAssignedMedia`, { moduleId, pageId });
        return response.data;
    },

    /**
     * Get live preview of landing page
     */
    getLivePreview: async (param) => {
        const response = await post(`${API_BASE_PATH}/GetLivePreview`, param);
        return response.data;
    },

    /**
     * Update module configuration
     */
    updateModuleConfiguration: async (param) => {
        const response = await post(`${API_BASE_PATH}/UpdateModuleConfiguration`, param);
        return response.data;
    },

    /**
     * Add module to page
     */
    addModuleToPage: async (param) => {
        const response = await post(`${API_BASE_PATH}/AddModuleToPage`, param);
        return response.data;
    },

    /**
     * Remove module from page
     */
    removeModuleFromPage: async (param) => {
        const response = await post(`${API_BASE_PATH}/RemoveModuleFromPage`, param);
        return response.data;
    },

    /**
     * Reorder modules on page
     */
    reorderModules: async (param) => {
        const response = await post(`${API_BASE_PATH}/ReorderModules`, param);
        return response.data;
    },

    /**
     * Save landing page template
     */
    saveLandingPageTemplate: async (param) => {
        const response = await post(`${API_BASE_PATH}/SaveLandingPageTemplate`, param);
        return response.data;
    }
};

/**
 * Helper functions for media management
 */
export const mediaHelpers = {
    /**
     * Format file size to human readable format
     */
    formatFileSize: (bytes) => {
        if (!bytes) return '0 B';
        const sizes = ['B', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(1024));
        return Math.round(bytes / Math.pow(1024, i) * 100) / 100 + ' ' + sizes[i];
    },

    /**
     * Get file type icon class
     */
    getFileTypeIcon: (fileName) => {
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
            case 'mp4':
            case 'avi':
            case 'mov':
                return 'oi oi-video';
            default:
                return 'oi oi-file';
        }
    },

    /**
     * Check if file is an image
     */
    isImageFile: (fileName) => {
        if (!fileName) return false;
        const extension = fileName.toLowerCase().split('.').pop();
        return ['jpg', 'jpeg', 'png', 'gif', 'webp', 'bmp', 'svg'].includes(extension);
    },

    /**
     * Get media role display name
     */
    getMediaRoleDisplayName: (role) => {
        switch (role?.toLowerCase()) {
            case 'primary':
                return 'Primary Image';
            case 'secondary':
                return 'Secondary Image';
            case 'background':
                return 'Background';
            case 'logo':
                return 'Logo';
            case 'icon':
                return 'Icon';
            default:
                return role || 'Media';
        }
    },

    /**
     * Validate media for module type
     */
    validateMediaForModule: (media, moduleType) => {
        const errors = [];

        if (!media) {
            errors.push('No media selected');
            return { isValid: false, errors };
        }

        // Module type specific validation
        switch (moduleType) {
            case 1: // Image modules
                if (!mediaHelpers.isImageFile(media.FileName)) {
                    errors.push('Only image files are allowed for image modules');
                }
                break;
            case 2: // Contact modules
                // Contact modules can accept images and documents
                break;
            case 3: // Form modules
                // Form modules can accept images and documents
                break;
            case 4: // Link modules
                // Link modules typically don't need file uploads
                break;
        }

        return {
            isValid: errors.length === 0,
            errors
        };
    }
};

export default pageBuilderService;
