// Main Asset Uploader Components
export { default as AssetUploaderContainer } from './AssetUploaderContainer';
export { default as ModuleSelector } from './ModuleSelector';
export { default as FileDropZone } from './FileDropZone';
export { default as MediaGallery } from './MediaGallery';
export { default as FileFilters } from './FileFilters';
export { default as BulkActions } from './BulkActions';
export { default as UploadProgress } from './UploadProgress';
export { default as MediaStats } from './MediaStats';
export { default as MediaModal } from './MediaModal';

// Page Builder Integration Components
export { default as MediaPicker } from './MediaPicker';
export { default as MediaAssignment } from './MediaAssignment';

// Component Groups for easier imports
export const AssetUploaderComponents = {
    AssetUploaderContainer: require('./AssetUploaderContainer').default,
    ModuleSelector: require('./ModuleSelector').default,
    FileDropZone: require('./FileDropZone').default,
    MediaGallery: require('./MediaGallery').default,
    FileFilters: require('./FileFilters').default,
    BulkActions: require('./BulkActions').default,
    UploadProgress: require('./UploadProgress').default,
    MediaStats: require('./MediaStats').default,
    MediaModal: require('./MediaModal').default
};

export const PageBuilderComponents = {
    MediaPicker: require('./MediaPicker').default,
    MediaAssignment: require('./MediaAssignment').default
};
