import uploadService from '../libs/uploadService';
import * as types from './types';
import { toggleLoader } from '../../../../actions/loaderAction';

// Action Creators
export const uploadFilesStart = (payload) => ({
    type: types.UPLOAD_FILES_START,
    payload
});

export const uploadFilesSuccess = (payload) => ({
    type: types.UPLOAD_FILES_SUCCESS,
    payload
});

export const uploadFilesFailure = (payload) => ({
    type: types.UPLOAD_FILES_FAILURE,
    payload
});

export const uploadProgress = (payload) => ({
    type: types.UPLOAD_PROGRESS,
    payload
});

export const loadMediaStart = (payload) => ({
    type: types.LOAD_MEDIA_START,
    payload
});

export const loadMediaSuccess = (payload) => ({
    type: types.LOAD_MEDIA_SUCCESS,
    payload
});

export const loadMediaFailure = (payload) => ({
    type: types.LOAD_MEDIA_FAILURE,
    payload
});

export const deleteMediaStart = (payload) => ({
    type: types.DELETE_MEDIA_START,
    payload
});

export const deleteMediaSuccess = (payload) => ({
    type: types.DELETE_MEDIA_SUCCESS,
    payload
});

export const deleteMediaFailure = (payload) => ({
    type: types.DELETE_MEDIA_FAILURE,
    payload
});

export const updateMediaStart = (payload) => ({
    type: types.UPDATE_MEDIA_START,
    payload
});

export const updateMediaSuccess = (payload) => ({
    type: types.UPDATE_MEDIA_SUCCESS,
    payload
});

export const updateMediaFailure = (payload) => ({
    type: types.UPDATE_MEDIA_FAILURE,
    payload
});

export const validateFileStart = (payload) => ({
    type: types.VALIDATE_FILE_START,
    payload
});

export const validateFileSuccess = (payload) => ({
    type: types.VALIDATE_FILE_SUCCESS,
    payload
});

export const validateFileFailure = (payload) => ({
    type: types.VALIDATE_FILE_FAILURE,
    payload
});

export const setSelectedModule = (payload) => ({
    type: types.SET_SELECTED_MODULE,
    payload
});

export const setFilterOptions = (payload) => ({
    type: types.SET_FILTER_OPTIONS,
    payload
});

export const setBulkSelected = (payload) => ({
    type: types.SET_BULK_SELECTED,
    payload
});

export const setUploadQueue = (payload) => ({
    type: types.SET_UPLOAD_QUEUE,
    payload
});

export const removeFromQueue = (payload) => ({
    type: types.REMOVE_FROM_QUEUE,
    payload
});

export const clearUploadQueue = () => ({
    type: types.CLEAR_UPLOAD_QUEUE
});

export const showMediaModal = (payload) => ({
    type: types.SHOW_MEDIA_MODAL,
    payload
});

export const hideMediaModal = () => ({
    type: types.HIDE_MEDIA_MODAL
});

export const showUploadModal = (payload) => ({
    type: types.SHOW_UPLOAD_MODAL,
    payload
});

export const hideUploadModal = () => ({
    type: types.HIDE_UPLOAD_MODAL
});

export const loadStatsStart = (payload) => ({
    type: types.LOAD_STATS_START,
    payload
});

export const loadStatsSuccess = (payload) => ({
    type: types.LOAD_STATS_SUCCESS,
    payload
});

export const loadStatsFailure = (payload) => ({
    type: types.LOAD_STATS_FAILURE,
    payload
});

export const clearErrors = () => ({
    type: types.CLEAR_ERRORS
});

export const setError = (payload) => ({
    type: types.SET_ERROR,
    payload
});

export const setLoading = (payload) => ({
    type: types.SET_LOADING,
    payload
});

export const clearLoading = () => ({
    type: types.CLEAR_LOADING
});

// Async Action Creators (Thunks)

/**
 * Upload files with progress tracking
 */
export const uploadFiles = (files, params) => async (dispatch) => {
    dispatch(uploadFilesStart({ files: Array.from(files), params }));
    
    try {
        // Client-side validation first
        const clientValidation = Array.from(files).map(file => ({
            file,
            validation: uploadService.validateFileClient(file, params.moduleType)
        }));

        const invalidFiles = clientValidation.filter(item => !item.validation.isValid);
        if (invalidFiles.length > 0) {
            dispatch(uploadFilesFailure({
                error: 'Client-side validation failed',
                validationErrors: invalidFiles.map(item => ({
                    fileName: item.file.name,
                    errors: item.validation.errors
                }))
            }));
            return;
        }

        // Server-side validation
        const validationPromises = Array.from(files).map(file => 
            uploadService.validateFile(file, params.moduleId, params.moduleType)
        );
        
        const validationResults = await Promise.all(validationPromises);
        const serverInvalidFiles = validationResults.filter(result => !result.IsValid);
        
        if (serverInvalidFiles.length > 0) {
            dispatch(uploadFilesFailure({
                error: 'Server-side validation failed',
                validationErrors: serverInvalidFiles
            }));
            return;
        }

        // Upload files with progress tracking
        const result = await uploadService.uploadFiles(files, params, (percent, uploadId) => {
            dispatch(uploadProgress({ percent, uploadId, files: Array.from(files) }));
        });

        dispatch(uploadFilesSuccess({ result, files: Array.from(files) }));
        
        // Reload media list to show new uploads
        dispatch(loadMediaList({ moduleId: params.moduleId }));
        
    } catch (error) {
        dispatch(uploadFilesFailure({ 
            error: error.message,
            files: Array.from(files)
        }));
    }
};

/**
 * Load media list for a module
 */
export const loadMediaList = (params = {}) => async (dispatch) => {
    dispatch(loadMediaStart(params));
    
    try {
        const result = await uploadService.getMediaList(params);
        dispatch(loadMediaSuccess({ result, params }));
    } catch (error) {
        dispatch(loadMediaFailure({ error: error.message, params }));
    }
};

/**
 * Delete media files
 */
export const deleteMedia = (mediaIds, permanentDelete = false, deleteReason = '') => async (dispatch) => {
    dispatch(deleteMediaStart({ mediaIds, permanentDelete }));
    
    try {
        const result = await uploadService.deleteMedia(mediaIds, permanentDelete, deleteReason);
        dispatch(deleteMediaSuccess({ result, mediaIds }));
        
        // Reload media list to reflect deletions
        dispatch(loadMediaList());
        
    } catch (error) {
        dispatch(deleteMediaFailure({ error: error.message, mediaIds }));
    }
};

/**
 * Update media file
 */
export const updateMedia = (mediaId, file, params) => async (dispatch) => {
    dispatch(updateMediaStart({ mediaId, params }));
    
    try {
        const result = await uploadService.updateMedia(mediaId, file, params);
        dispatch(updateMediaSuccess({ result, mediaId, params }));
        
        // Reload media list to show updates
        dispatch(loadMediaList());
        
    } catch (error) {
        dispatch(updateMediaFailure({ error: error.message, mediaId }));
    }
};

/**
 * Validate file before upload
 */
export const validateFile = (file, moduleId, moduleType) => async (dispatch) => {
    dispatch(validateFileStart({ file, moduleId, moduleType }));
    
    try {
        const result = await uploadService.validateFile(file, moduleId, moduleType);
        dispatch(validateFileSuccess({ result, file }));
        return result;
    } catch (error) {
        dispatch(validateFileFailure({ error: error.message, file }));
        throw error;
    }
};

/**
 * Load media statistics
 */
export const loadMediaStats = (moduleId) => async (dispatch) => {
    dispatch(loadStatsStart({ moduleId }));
    
    try {
        const result = await uploadService.getMediaStats(moduleId);
        dispatch(loadStatsSuccess({ result, moduleId }));
    } catch (error) {
        dispatch(loadStatsFailure({ error: error.message, moduleId }));
    }
};

/**
 * Add files to upload queue
 */
export const addToUploadQueue = (files, params) => (dispatch, getState) => {
    const { uploadQueue } = getState().assetUploader;
    const newItems = Array.from(files).map(file => ({
        id: `${Date.now()}_${Math.random().toString(36).substr(2, 9)}`,
        file,
        params,
        status: 'pending', // pending, uploading, completed, failed
        progress: 0,
        error: null,
        validationResult: null
    }));
    
    dispatch(setUploadQueue([...uploadQueue, ...newItems]));
};

/**
 * Process upload queue
 */
export const processUploadQueue = () => async (dispatch, getState) => {
    const { uploadQueue } = getState().assetUploader;
    const pendingUploads = uploadQueue.filter(item => item.status === 'pending');
    
    for (const item of pendingUploads) {
        try {
            // Update status to uploading
            dispatch(setUploadQueue(
                uploadQueue.map(queueItem => 
                    queueItem.id === item.id 
                        ? { ...queueItem, status: 'uploading' }
                        : queueItem
                )
            ));
            
            // Upload the file
            await dispatch(uploadFiles([item.file], item.params));
            
            // Update status to completed
            dispatch(setUploadQueue(
                uploadQueue.map(queueItem => 
                    queueItem.id === item.id 
                        ? { ...queueItem, status: 'completed', progress: 100 }
                        : queueItem
                )
            ));
            
        } catch (error) {
            // Update status to failed
            dispatch(setUploadQueue(
                uploadQueue.map(queueItem => 
                    queueItem.id === item.id 
                        ? { ...queueItem, status: 'failed', error: error.message }
                        : queueItem
                )
            ));
        }
    }
};

/**
 * Clear completed uploads from queue
 */
export const clearCompletedUploads = () => (dispatch, getState) => {
    const { uploadQueue } = getState().assetUploader;
    const remainingUploads = uploadQueue.filter(item => 
        item.status !== 'completed' && item.status !== 'failed'
    );
    dispatch(setUploadQueue(remainingUploads));
};

/**
 * Remove specific item from queue
 */
export const removeQueueItem = (itemId) => (dispatch, getState) => {
    const { uploadQueue } = getState().assetUploader;
    const updatedQueue = uploadQueue.filter(item => item.id !== itemId);
    dispatch(setUploadQueue(updatedQueue));
};

/**
 * Toggle bulk selection
 */
export const toggleBulkSelection = (mediaId) => (dispatch, getState) => {
    const { bulkSelected } = getState().assetUploader;
    const newSelection = { ...bulkSelected };
    
    if (newSelection[mediaId]) {
        delete newSelection[mediaId];
    } else {
        newSelection[mediaId] = true;
    }
    
    dispatch(setBulkSelected(newSelection));
};

/**
 * Select all media items
 */
export const selectAllMedia = (mediaList) => (dispatch) => {
    const allSelected = {};
    mediaList.forEach(media => {
        allSelected[media.MediaId] = true;
    });
    dispatch(setBulkSelected(allSelected));
};

/**
 * Clear all selections
 */
export const clearAllSelections = () => (dispatch) => {
    dispatch(setBulkSelected({}));
};

/**
 * Delete selected media items
 */
export const deleteSelectedMedia = (permanentDelete = false, deleteReason = '') => async (dispatch, getState) => {
    const { bulkSelected } = getState().assetUploader;
    const selectedIds = Object.keys(bulkSelected).filter(id => bulkSelected[id]);
    
    if (selectedIds.length === 0) {
        dispatch(setError({ message: 'No media items selected for deletion' }));
        return;
    }
    
    await dispatch(deleteMedia(selectedIds, permanentDelete, deleteReason));
    dispatch(clearAllSelections());
};
