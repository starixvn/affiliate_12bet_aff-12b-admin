import React, { useEffect, useCallback, useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import AppLayout from '../../../../layouts/AppLayout';
import Loader from '../../../../components/Loader';
import ModuleSelector from './ModuleSelector';
import FileDropZone from './FileDropZone';
import MediaGallery from './MediaGallery';
import UploadProgress from './UploadProgress';
import MediaModal from './MediaModal';
import BulkActions from './BulkActions';
import FileFilters from './FileFilters';
import MediaStats from './MediaStats';

import {
    fetchMediaListAsync,
    uploadFilesAsync,
    deleteMediaAsync,
    setSelectedModuleId,
    setFilters,
    setPagination,
    setBulkSelectedFiles,
    showModal,
    hideModal,
    fetchMediaRootUrlAsync,
    fetchMediaStatsAsync,
    fetchModuleListAsync,
    setError
} from '../actions/assetActions';

export const AssetUploaderContainer = (props) => {
    const {
        isLoading, error, mediaList, totalRecords, uploadQueue,
        selectedModuleId, filters, pagination, bulkSelectedFileIds,
        isShowModal, modalType, modalContent, mediaRootUrl, mediaStats, moduleList,
        fetchMediaListAsync, uploadFilesAsync, deleteMediaAsync,
        setSelectedModuleId, setFilters, setPagination, setBulkSelectedFiles,
        showModal, hideModal, fetchMediaRootUrlAsync, fetchMediaStatsAsync, fetchModuleListAsync, setError
    } = props;

    const [dragOver, setDragOver] = useState(false);

    useEffect(() => {
        fetchModuleListAsync();
        fetchMediaRootUrlAsync();
    }, [fetchModuleListAsync, fetchMediaRootUrlAsync]);

    useEffect(() => {
        if (selectedModuleId > 0) {
            fetchMediaListAsync(selectedModuleId, filters, pagination);
            fetchMediaStatsAsync(selectedModuleId);
        }
    }, [selectedModuleId, filters, pagination, fetchMediaListAsync, fetchMediaStatsAsync]);

    const handleModuleChange = useCallback((moduleId) => {
        setSelectedModuleId(moduleId);
        setFilters({ searchTerm: '', fileType: '', sizeFilter: '' });
        setPagination({ pageIndex: 1, pageSize: 20 });
        setBulkSelectedFiles([]);
    }, [setSelectedModuleId, setFilters, setPagination, setBulkSelectedFiles]);

    const handleFilesDrop = useCallback((files) => {
        if (!selectedModuleId) {
            setError('Please select a module first');
            return;
        }
        uploadFilesAsync(files, selectedModuleId, 0); // ModuleType 0 for generic validation
    }, [uploadFilesAsync, selectedModuleId, setError]);

    const handleFilterChange = useCallback((newFilters) => {
        setFilters(newFilters);
        setPagination({ pageIndex: 1, pageSize: pagination.pageSize }); // Reset to first page
    }, [setFilters, setPagination, pagination.pageSize]);

    const handlePageChange = useCallback((newPageIndex) => {
        setPagination({ pageIndex: newPageIndex, pageSize: pagination.pageSize });
    }, [setPagination, pagination.pageSize]);

    const handleBulkSelectFile = useCallback((mediaId) => {
        setBulkSelectedFiles(prev =>
            prev.includes(mediaId) ? prev.filter(id => id !== mediaId) : [...prev, mediaId]
        );
    }, [setBulkSelectedFiles]);

    const handleSelectAll = useCallback(() => {
        if (bulkSelectedFileIds.length === mediaList.length) {
            setBulkSelectedFiles([]);
        } else {
            setBulkSelectedFiles(mediaList.map(media => media.MediaId));
        }
    }, [bulkSelectedFileIds, mediaList, setBulkSelectedFiles]);

    const handleBulkDelete = useCallback(() => {
        if (window.confirm(`Are you sure you want to delete ${bulkSelectedFileIds.length} selected files?`)) {
            deleteMediaAsync(bulkSelectedFileIds, true, 'Bulk delete from Asset Uploader');
        }
    }, [bulkSelectedFileIds, deleteMediaAsync]);

    const handleViewMedia = useCallback((media) => {
        showModal('view', media);
    }, [showModal]);

    const handleDragOver = (e) => {
        e.preventDefault();
        setDragOver(true);
    };

    const handleDragLeave = (e) => {
        e.preventDefault();
        setDragOver(false);
    };

    const handleDrop = (e) => {
        e.preventDefault();
        setDragOver(false);
        const files = Array.from(e.dataTransfer.files);
        handleFilesDrop(files);
    };

    return (
        <AppLayout>
            <Loader show={isLoading} />
            
            <div 
                className="asset-uploader-container"
                onDragOver={handleDragOver}
                onDragLeave={handleDragLeave}
                onDrop={handleDrop}
            >
                <div className="page-header">
                    <h2>Landing Page Asset Uploader</h2>
                    <p>Manage media assets for landing page builder</p>
                </div>

                {error && <div className="alert alert-danger">{error}</div>}

                <div className="row">
                    <div className="col-md-3">
                        <ModuleSelector
                            modules={moduleList}
                            selectedModuleId={selectedModuleId}
                            onModuleChange={handleModuleChange}
                        />
                        <MediaStats stats={mediaStats} />
                    </div>
                    <div className="col-md-9">
                        <FileDropZone
                            onFilesSelected={handleFilesDrop}
                            selectedModule={moduleList.find(m => m.ModuleId === selectedModuleId)}
                            isUploading={isLoading}
                            dragOver={dragOver}
                        />
                        
                        <UploadProgress uploadQueue={uploadQueue} />

                        <hr />

                        <FileFilters 
                            filters={filters} 
                            onFilterChange={handleFilterChange} 
                        />
                        
                        <BulkActions
                            selectedCount={bulkSelectedFileIds.length}
                            totalCount={mediaList.length}
                            onSelectAll={handleSelectAll}
                            onDeleteSelected={handleBulkDelete}
                            onClearSelection={() => setBulkSelectedFiles([])}
                            isDeleting={isLoading}
                        />
                        
                        <MediaGallery
                            mediaList={mediaList}
                            bulkSelected={bulkSelectedFileIds.reduce((acc, id) => ({ ...acc, [id]: true }), {})}
                            onMediaClick={handleViewMedia}
                            onBulkSelect={handleBulkSelectFile}
                            isLoading={isLoading}
                            pagination={{
                                pageIndex: pagination.pageIndex - 1, // Convert to 0-based
                                pageSize: pagination.pageSize,
                                totalItems: totalRecords,
                                totalPages: Math.ceil(totalRecords / pagination.pageSize)
                            }}
                            onPageChange={(pageIndex) => handlePageChange(pageIndex + 1)} // Convert back to 1-based
                        />
                    </div>
                </div>
            </div>

            <MediaModal
                show={isShowModal && modalType === 'view'}
                media={modalContent}
                onHide={hideModal}
                onUpdate={(mediaId, file, updateData) => {
                    // Handle media update
                    console.log('Update media:', mediaId, file, updateData);
                }}
                onDelete={(mediaId) => {
                    deleteMediaAsync([mediaId], true, 'Delete from modal');
                }}
            />
        </AppLayout>
    );
};

function mapStateToProps(state) {
    return {
        isLoading: state.assetUploader?.isLoading || false,
        error: state.assetUploader?.error || null,
        mediaList: state.assetUploader?.mediaList || [],
        totalRecords: state.assetUploader?.totalRecords || 0,
        uploadQueue: state.assetUploader?.uploadQueue || [],
        selectedModuleId: state.assetUploader?.selectedModuleId || 0,
        filters: state.assetUploader?.filters || { searchTerm: '', fileType: '', sizeFilter: '' },
        pagination: state.assetUploader?.pagination || { pageIndex: 1, pageSize: 20 },
        bulkSelectedFileIds: state.assetUploader?.bulkSelectedFileIds || [],
        isShowModal: state.assetUploader?.isShowModal || false,
        modalType: state.assetUploader?.modalType || null,
        modalContent: state.assetUploader?.modalContent || null,
        mediaRootUrl: state.assetUploader?.mediaRootUrl || '',
        mediaStats: state.assetUploader?.mediaStats || null,
        moduleList: state.assetUploader?.moduleList || [],
    };
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({
        fetchMediaListAsync,
        uploadFilesAsync,
        deleteMediaAsync,
        setSelectedModuleId,
        setFilters,
        setPagination,
        setBulkSelectedFiles,
        showModal,
        hideModal,
        fetchMediaRootUrlAsync,
        fetchMediaStatsAsync,
        fetchModuleListAsync,
        setError
    }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(AssetUploaderContainer));