import React, { useEffect } from 'react';
import { useSelector, useDispatch, batch } from 'react-redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import MultipleSelect from '@/components/multiple-select';
import { handleChangeMultipleSelected } from '@/modules/utils';
import { ActionCreators } from '../actions';


const FilterForm = () => {
    const {
        setSelectedStatus,
        setStatusList,
        setSearchCampaign,
        setSearchDescription,
        setParam,
        fetchFilter } = ActionCreators;

    const [
        statusList,
        selectedStatus,
        searchCampaign,
        searchDescription,
        isShowLoader,
        pagingOption
    ] = useSelector(state => [
        state.statusList,
        state.selectedStatus,
        state.searchCampaign,
        state.searchDescription,
        state.isShowLoader,
        state.pagingOption]);


    const dispatch = useDispatch();
    const handleChangeStatus = (item, selectedStatusList) => {
        dispatch(setSelectedStatus(selectedStatusList));
        dispatch(setStatusList(handleChangeMultipleSelected(statusList, selectedStatusList)));
    }

    const handleChangeCampaignName = (event) => {
        dispatch(setSearchCampaign(event.target.value));
    }

    const handleChangeDescription = (event) => {
        dispatch(setSearchDescription(event.target.value));
    }

    const handleSearch = () => {
        const params = {
            StatusIds: selectedStatus,
            Search: searchCampaign,
            Description: searchDescription,
            PageIndex: 1,
            PageSize: pagingOption.PageSize
        }
        dispatch(setParam(params));
    }

    const handleAddNew = () => {
        if (!isShowLoader) {
            console.log('Add new campaign clicked');
            // TODO: Implement navigation to create campaign page or open modal
            // Example: history.push('/campaign-management/create');
            // Or: setShowCreateModal(true);
        }
    }

    useEffect(() => {
        dispatch(fetchFilter());
    }, []);

    return (
        <div className="filter-container my-2 pb-0">
            <Form inline tag="div">
                <FormGroup className="mr-sm-2 pb-2">
                    <Label htmlFor="campaignName" className="mr-sm-2">Campaign Name</Label>
                    <Input
                        type="text"
                        name="campaignName"
                        id="campaignName"
                        value={searchCampaign}
                        onChange={handleChangeCampaignName}
                        placeholder="Search campaign name/code"
                        className="wide-input"
                    />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2">
                    <Label htmlFor="description" className="mr-sm-2">Description</Label>
                    <Input
                        type="text"
                        name="description"
                        id="description"
                        value={searchDescription}
                        onChange={handleChangeDescription}
                        placeholder="Search by description"
                        className="wide-input"
                    />
                </FormGroup>

                <FormGroup className="mr-sm-3 pb-2 multiple-status-filter">
                    <Label htmlFor="status" className="mr-sm-2">Status</Label>
                    <MultipleSelect
                        id="status"
                        dataSource={statusList || []}
                        keyField="key"
                        valueField="name"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        hasInputOption={true}
                        noneSelectedLabel="No Selection"
                        selectedAllLabel="All"
                        onChange={handleChangeStatus} />
                </FormGroup>

                <div className="button-group">
                    <Button outline color="primary" type="button" className="mb-2 mr-2" onClick={handleSearch} disabled={!selectedStatus}>
                        <span className="oi oi-magnifying-glass"></span> Search
                    </Button>

                    <Button color="success" type="button" className="mb-2" onClick={handleAddNew}>
                        <span className="oi oi-plus"></span> Add Campaign
                    </Button>
                </div>
            </Form>
        </div>
    );
};

export default FilterForm;
