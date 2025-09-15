import React, { useState, useCallback } from 'react';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { CAMPAIGN_STATUS } from '../../../constants/status';

const FilterForm = ({ onSearch }) => {
    const [filters, setFilters] = useState({
        campaignName: '',
        description: '',
        status: 'All'
    });

    const handleChange = useCallback((event) => {
        const { name, value } = event.target;
        setFilters(prev => ({ ...prev, [name]: value }));
    }, []);

    const handleSearch = useCallback(() => {
        onSearch && onSearch(filters);
    }, [filters, onSearch]);

    return (
        <div className="filter-container my-2 pb-0">
            <Form inline tag="div">
                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="campaignName" className="mr-sm-2">Campaign Name</Label>
                    <Input 
                        type="text" 
                        name="campaignName" 
                        id="campaignName" 
                        value={filters.campaignName}
                        onChange={handleChange}
                        placeholder="All"
                    />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="description" className="mr-sm-2">Description</Label>
                    <Input 
                        type="text" 
                        name="description" 
                        id="description" 
                        value={filters.description}
                        onChange={handleChange}
                        placeholder="Search by description"
                    />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="status" className="mr-sm-2">Status</Label>
                    <Input 
                        type="select" 
                        name="status" 
                        id="status" 
                        value={filters.status}
                        onChange={handleChange}
                    >
                        <option value="All">All</option>
                        {CAMPAIGN_STATUS.map(statusOption => (
                            <option key={statusOption.id} value={statusOption.status}>
                                {statusOption.status}
                            </option>
                        ))}
                    </Input>
                </FormGroup>

                <Button outline color="primary" type="button" className="mb-2" onClick={handleSearch}>
                    <span className="oi oi-magnifying-glass"></span> Search
                </Button>
            </Form>
        </div>
    );
};

export default FilterForm;
