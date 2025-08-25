import React from 'react';
import { Button, ButtonGroup, Badge } from 'reactstrap';
import { globalResource } from '../../../../modules/globalResource';

const BulkActions = ({ 
    selectedCount, 
    totalCount, 
    onSelectAll, 
    onDeleteSelected, 
    onClearSelection,
    isDeleting 
}) => {
    const isAllSelected = selectedCount === totalCount && totalCount > 0;
    const hasSelection = selectedCount > 0;

    return (
        <div className="bulk-actions">
            <div className="d-flex justify-content-between align-items-center">
                <div className="selection-info">
                    <span className="text-muted small">
                        {selectedCount > 0 ? (
                            <>
                                <Badge color="primary" className="mr-1">{selectedCount}</Badge>
                                of {totalCount} selected
                            </>
                        ) : (
                            `${totalCount} file(s) total`
                        )}
                    </span>
                </div>
                
                <ButtonGroup size="sm">
                    {totalCount > 0 && (
                        <Button
                            color={isAllSelected ? "primary" : "outline-primary"}
                            onClick={onSelectAll}
                            title={isAllSelected ? "Deselect All" : "Select All"}
                            disabled={isDeleting}
                        >
                            <i className={`oi ${isAllSelected ? 'oi-circle-x' : 'oi-circle-check'}`}></i>
                            {isAllSelected ? 'Deselect All' : 'Select All'}
                        </Button>
                    )}
                    
                    {hasSelection && (
                        <>
                            <Button
                                color="outline-secondary"
                                onClick={onClearSelection}
                                title="Clear Selection"
                                disabled={isDeleting}
                            >
                                <i className="oi oi-x"></i>
                                Clear
                            </Button>
                            
                            <Button
                                color="outline-danger"
                                onClick={onDeleteSelected}
                                title={`Delete ${selectedCount} selected item(s)`}
                                disabled={isDeleting}
                            >
                                {isDeleting ? (
                                    <>
                                        <span className="spinner-border spinner-border-sm mr-1" role="status">
                                            <span className="sr-only">Deleting...</span>
                                        </span>
                                        Deleting...
                                    </>
                                ) : (
                                    <>
                                        <i className="oi oi-trash"></i>
                                        Delete ({selectedCount})
                                    </>
                                )}
                            </Button>
                        </>
                    )}
                </ButtonGroup>
            </div>
            
            {/* Selection Summary */}
            {hasSelection && (
                <div className="selection-summary mt-2">
                    <div className="small text-muted">
                        <i className="oi oi-info"></i>
                        {selectedCount === 1 
                            ? '1 file selected for bulk operations' 
                            : `${selectedCount} files selected for bulk operations`
                        }
                    </div>
                </div>
            )}
            
            {/* No Selection State */}
            {!hasSelection && totalCount > 0 && (
                <div className="no-selection-state mt-2">
                    <div className="small text-muted">
                        <i className="oi oi-target"></i>
                        Select files to perform bulk operations
                    </div>
                </div>
            )}
        </div>
    );
};

export default React.memo(BulkActions);
