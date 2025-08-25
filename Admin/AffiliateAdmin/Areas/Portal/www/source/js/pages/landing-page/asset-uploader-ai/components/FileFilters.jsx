import React, { useState } from 'react';
import { Button, Form, FormGroup, Label, Input, InputGroup, InputGroupAddon } from 'reactstrap';
import { globalResource } from '../../../../modules/globalResource';

const FileFilters = ({ filters, onFilterChange }) => {
    const [localFilters, setLocalFilters] = useState(filters);

    const handleInputChange = (e) => {
        const { name, value, type, checked } = e.target;
        const newValue = type === 'checkbox' ? checked : value;
        
        const updatedFilters = {
            ...localFilters,
            [name]: newValue
        };
        
        setLocalFilters(updatedFilters);
        
        // Auto-apply for checkboxes and dropdowns
        if (type === 'checkbox' || type === 'select-one') {
            onFilterChange(updatedFilters);
        }
    };

    const handleSearch = () => {
        onFilterChange(localFilters);
    };

    const handleClear = () => {
        const clearedFilters = {
            searchTerm: '',
            fileType: '',
            sizeFilter: '',
            includeDeleted: false
        };
        setLocalFilters(clearedFilters);
        onFilterChange(clearedFilters);
    };

    const handleKeyPress = (e) => {
        if (e.key === 'Enter') {
            e.preventDefault();
            handleSearch();
        }
    };

    return (
        <div className="file-filters">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3 mb-2">
                    <Label htmlFor="searchTerm" className="mr-sm-2 small">
                        <i className="oi oi-magnifying-glass"></i> Search
                    </Label>
                    <InputGroup size="sm">
                        <Input
                            type="text"
                            name="searchTerm"
                            id="searchTerm"
                            placeholder="Search by name..."
                            value={localFilters.searchTerm}
                            onChange={handleInputChange}
                            onKeyPress={handleKeyPress}
                            style={{ width: '200px' }}
                        />
                        <InputGroupAddon addonType="append">
                            <Button 
                                color="outline-secondary" 
                                size="sm"
                                onClick={handleSearch}
                                title="Search"
                            >
                                <i className="oi oi-magnifying-glass"></i>
                            </Button>
                        </InputGroupAddon>
                    </InputGroup>
                </FormGroup>

                <FormGroup className="mr-sm-3 mb-2">
                    <Label htmlFor="fileType" className="mr-sm-2 small">
                        <i className="oi oi-document"></i> Type
                    </Label>
                    <Input
                        type="select"
                        name="fileType"
                        id="fileType"
                        value={localFilters.fileType}
                        onChange={handleInputChange}
                        bsSize="sm"
                        style={{ width: '120px' }}
                    >
                        <option value="">{globalResource.All || 'All'}</option>
                        <option value="image">Images</option>
                        <option value="document">Documents</option>
                        <option value="video">Videos</option>
                    </Input>
                </FormGroup>

                <FormGroup className="mr-sm-3 mb-2">
                    <Label htmlFor="sizeFilter" className="mr-sm-2 small">
                        <i className="oi oi-resize-both"></i> Size
                    </Label>
                    <Input
                        type="select"
                        name="sizeFilter"
                        id="sizeFilter"
                        value={localFilters.sizeFilter}
                        onChange={handleInputChange}
                        bsSize="sm"
                        style={{ width: '140px' }}
                    >
                        <option value="">{globalResource.All || 'All'}</option>
                        <option value="small">Small (&lt; 1MB)</option>
                        <option value="medium">Medium (1-5MB)</option>
                        <option value="large">Large (&gt; 5MB)</option>
                    </Input>
                </FormGroup>

                <FormGroup className="mr-sm-3 mb-2">
                    <div className="form-check form-check-inline">
                        <Input
                            type="checkbox"
                            name="includeDeleted"
                            id="includeDeleted"
                            checked={localFilters.includeDeleted}
                            onChange={handleInputChange}
                            className="form-check-input"
                        />
                        <Label htmlFor="includeDeleted" className="form-check-label small">
                            <i className="oi oi-trash"></i> Include Deleted
                        </Label>
                    </div>
                </FormGroup>

                <FormGroup className="mb-2">
                    <Button
                        color="outline-secondary"
                        size="sm"
                        onClick={handleClear}
                        title="Clear all filters"
                    >
                        <i className="oi oi-x"></i> Clear
                    </Button>
                </FormGroup>
            </Form>

            {/* Active Filters Display */}
            {(localFilters.searchTerm || localFilters.fileType || localFilters.sizeFilter || localFilters.includeDeleted) && (
                <div className="active-filters mt-2">
                    <div className="d-flex flex-wrap align-items-center">
                        <span className="small text-muted mr-2">Active filters:</span>
                        
                        {localFilters.searchTerm && (
                            <span className="badge badge-primary mr-1 mb-1">
                                Search: "{localFilters.searchTerm}"
                                <button 
                                    className="btn btn-sm p-0 ml-1"
                                    onClick={() => handleInputChange({ target: { name: 'searchTerm', value: '' } })}
                                    style={{ background: 'none', border: 'none', color: 'white' }}
                                >
                                    ×
                                </button>
                            </span>
                        )}
                        
                        {localFilters.fileType && (
                            <span className="badge badge-info mr-1 mb-1">
                                Type: {localFilters.fileType}
                                <button 
                                    className="btn btn-sm p-0 ml-1"
                                    onClick={() => handleInputChange({ target: { name: 'fileType', value: '' } })}
                                    style={{ background: 'none', border: 'none', color: 'white' }}
                                >
                                    ×
                                </button>
                            </span>
                        )}
                        
                        {localFilters.sizeFilter && (
                            <span className="badge badge-warning mr-1 mb-1">
                                Size: {localFilters.sizeFilter}
                                <button 
                                    className="btn btn-sm p-0 ml-1"
                                    onClick={() => handleInputChange({ target: { name: 'sizeFilter', value: '' } })}
                                    style={{ background: 'none', border: 'none', color: 'white' }}
                                >
                                    ×
                                </button>
                            </span>
                        )}
                        
                        {localFilters.includeDeleted && (
                            <span className="badge badge-danger mr-1 mb-1">
                                Include Deleted
                                <button 
                                    className="btn btn-sm p-0 ml-1"
                                    onClick={() => handleInputChange({ target: { name: 'includeDeleted', checked: false, type: 'checkbox' } })}
                                    style={{ background: 'none', border: 'none', color: 'white' }}
                                >
                                    ×
                                </button>
                            </span>
                        )}
                    </div>
                </div>
            )}
        </div>
    );
};

export default React.memo(FileFilters);
