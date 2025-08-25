import * as types from '../actions/types';

const initialState = {
    // Loading states
    isLoading: false,
    isUploading: false,
    isDeleting: false,
    isValidating: false,

    // Data
    mediaList: [],
    selectedModule: null,
    mediaStats: null,
    uploadQueue: [],
    
    // UI state
    filters: {
        searchTerm: '',
        fileType: '',
        sizeFilter: '',
        includeDeleted: false
    },
    bulkSelected: {},
    
    // Modal states
    showMediaModal: false,
    showUploadModal: false,
    selectedMedia: null,
    
    // Upload progress
    uploadProgress: {},
    
    // Pagination
    pagination: {
        pageIndex: 0,
        pageSize: 20,
        totalItems: 0,
        totalPages: 0
    },
    
    // Error handling
    errors: {
        upload: null,
        delete: null,
        load: null,
        validation: null,
        general: null
    },
    
    // Success messages
    successMessage: null,
    
    // Configuration
    mediaRootUrl: '',
    moduleInfo: null
};

const assetUploaderReducer = (state = initialState, action) => {
    switch (action.type) {
        // Upload actions
        case types.UPLOAD_FILES_START:
            return {
                ...state,
                isUploading: true,
                errors: { ...state.errors, upload: null },
                successMessage: null
            };

        case types.UPLOAD_FILES_SUCCESS:
            return {
                ...state,
                isUploading: false,
                successMessage: `Successfully uploaded ${action.payload.files.length} file(s)`,
                uploadProgress: {}
            };

        case types.UPLOAD_FILES_FAILURE:
            return {
                ...state,
                isUploading: false,
                errors: { ...state.errors, upload: action.payload.error },
                uploadProgress: {}
            };

        case types.UPLOAD_PROGRESS:
            return {
                ...state,
                uploadProgress: {
                    ...state.uploadProgress,
                    [action.payload.uploadId]: {
                        percent: action.payload.percent,
                        files: action.payload.files
                    }
                }
            };

        // Media list actions
        case types.LOAD_MEDIA_START:
            return {
                ...state,
                isLoading: true,
                errors: { ...state.errors, load: null }
            };

        case types.LOAD_MEDIA_SUCCESS:
            const { result, params } = action.payload;
            return {
                ...state,
                isLoading: false,
                mediaList: result.Data || result.Items || [],
                pagination: {
                    ...state.pagination,
                    pageIndex: params.pageIndex || 0,
                    pageSize: params.pageSize || 20,
                    totalItems: result.TotalItems || result.Total || 0,
                    totalPages: result.TotalPages || Math.ceil((result.Total || 0) / (params.pageSize || 20))
                }
            };

        case types.LOAD_MEDIA_FAILURE:
            return {
                ...state,
                isLoading: false,
                errors: { ...state.errors, load: action.payload.error },
                mediaList: []
            };

        // Delete actions
        case types.DELETE_MEDIA_START:
            return {
                ...state,
                isDeleting: true,
                errors: { ...state.errors, delete: null }
            };

        case types.DELETE_MEDIA_SUCCESS:
            return {
                ...state,
                isDeleting: false,
                successMessage: `Successfully deleted ${action.payload.mediaIds.length} item(s)`,
                bulkSelected: {} // Clear selections after delete
            };

        case types.DELETE_MEDIA_FAILURE:
            return {
                ...state,
                isDeleting: false,
                errors: { ...state.errors, delete: action.payload.error }
            };

        // Update actions
        case types.UPDATE_MEDIA_START:
            return {
                ...state,
                isLoading: true,
                errors: { ...state.errors, update: null }
            };

        case types.UPDATE_MEDIA_SUCCESS:
            return {
                ...state,
                isLoading: false,
                successMessage: 'Media updated successfully'
            };

        case types.UPDATE_MEDIA_FAILURE:
            return {
                ...state,
                isLoading: false,
                errors: { ...state.errors, update: action.payload.error }
            };

        // Validation actions
        case types.VALIDATE_FILE_START:
            return {
                ...state,
                isValidating: true,
                errors: { ...state.errors, validation: null }
            };

        case types.VALIDATE_FILE_SUCCESS:
            return {
                ...state,
                isValidating: false
            };

        case types.VALIDATE_FILE_FAILURE:
            return {
                ...state,
                isValidating: false,
                errors: { ...state.errors, validation: action.payload.error }
            };

        // UI state actions
        case types.SET_SELECTED_MODULE:
            return {
                ...state,
                selectedModule: action.payload,
                mediaList: [], // Clear media list when module changes
                bulkSelected: {},
                filters: initialState.filters // Reset filters
            };

        case types.SET_FILTER_OPTIONS:
            return {
                ...state,
                filters: { ...state.filters, ...action.payload }
            };

        case types.SET_BULK_SELECTED:
            return {
                ...state,
                bulkSelected: action.payload
            };

        case types.SET_UPLOAD_QUEUE:
            return {
                ...state,
                uploadQueue: action.payload
            };

        case types.REMOVE_FROM_QUEUE:
            return {
                ...state,
                uploadQueue: state.uploadQueue.filter(item => item.id !== action.payload.id)
            };

        case types.CLEAR_UPLOAD_QUEUE:
            return {
                ...state,
                uploadQueue: []
            };

        // Modal actions
        case types.SHOW_MEDIA_MODAL:
            return {
                ...state,
                showMediaModal: true,
                selectedMedia: action.payload || null
            };

        case types.HIDE_MEDIA_MODAL:
            return {
                ...state,
                showMediaModal: false,
                selectedMedia: null
            };

        case types.SHOW_UPLOAD_MODAL:
            return {
                ...state,
                showUploadModal: true
            };

        case types.HIDE_UPLOAD_MODAL:
            return {
                ...state,
                showUploadModal: false
            };

        // Statistics actions
        case types.LOAD_STATS_START:
            return {
                ...state,
                isLoading: true
            };

        case types.LOAD_STATS_SUCCESS:
            return {
                ...state,
                isLoading: false,
                mediaStats: action.payload.result
            };

        case types.LOAD_STATS_FAILURE:
            return {
                ...state,
                isLoading: false,
                errors: { ...state.errors, stats: action.payload.error }
            };

        // Error handling
        case types.CLEAR_ERRORS:
            return {
                ...state,
                errors: initialState.errors,
                successMessage: null
            };

        case types.SET_ERROR:
            return {
                ...state,
                errors: { ...state.errors, general: action.payload.message }
            };

        // Loading states
        case types.SET_LOADING:
            return {
                ...state,
                isLoading: action.payload
            };

        case types.CLEAR_LOADING:
            return {
                ...state,
                isLoading: false,
                isUploading: false,
                isDeleting: false,
                isValidating: false
            };

        default:
            return state;
    }
};

export default assetUploaderReducer;
