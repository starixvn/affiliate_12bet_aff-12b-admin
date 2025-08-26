import React, { useState, useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Button, Form, FormGroup, Label } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import Field from '../../../../components/Field';
import MultipleSelect from '../../../../components/multiple-select';

import { setReportTitle, toggleSubmit, setParams } from '../actions';
import { statusOptions, viewByOptions } from '../libs/constants';
import { formatUnit, handleAfterChangeFromDate, handleAfterChangeToDate, handleChangeMultipleSelected } from '../../../../modules/utils';
import { globalResource } from '../../../../modules/globalResource';
import { minDate, sixMonth } from '../../../../modules/constants';

const Filter = ({ params, setParams, toggleSubmit, today, deals }) => {

    const [nowString, setNowString] = useState('');
    const [dealList, setDealList] = useState(deals);

    useEffect(() => {
        const now = new Date(today);
        const currentMonth = now.getMonth() + 1;
        const currentMonthString = formatUnit(currentMonth);
        const currentYear = now.getFullYear();

        const nowString = `${currentMonthString}/${formatUnit(now.getDate())}/${currentYear}`;
        setNowString(nowString);

        // Set initial date params
        setParams({
            fromDate: `${currentMonthString}/01/${currentYear}`,
            toDate: nowString
        });
    }, [today]);

    useEffect(() => {
        if (deals && deals.length > 0) {
            const dealIds = deals.map(deal => deal.key).join(',');
            setParams({
                dealName: dealIds
            });
        }
    }, [deals]);

    const handleChange = (e) => {
        setParams({
            [e.target.name]: e.target.value
        })
    }

    const handleChangeFromDate = (date) => {
        setParams({ ...handleAfterChangeFromDate(date, params.toDate, sixMonth) });
    }
    const handleChangeToDate = (date) => {
        setParams({ ...handleAfterChangeToDate(date, params.fromDate, sixMonth) });
    }

    const handleChangeDeals = (item, selectedDeals) => {
        setDealList(handleChangeMultipleSelected(dealList, selectedDeals));
        setParams({
            dealName: selectedDeals
        });
    }

    const handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();

            handleClickSearch();
        }
    }

    const handleClickSearch = () => {
        toggleSubmit()
    }

    return (
        <div className="filter-container">
            <Form inline tag="div">
                <Field className="my-sm-1 mr-sm-2" name="userName" value={params.userName} onChange={handleChange} label="Affiliate" onPressEnter={handlePressEnter} />
                <Field className="my-sm-1 mr-sm-2" name="custName" value={params.custName} onChange={handleChange} label="Player" onPressEnter={handlePressEnter} />
                <FormGroup className="my-sm-2 mr-sm-4 comm-group">
                    <MultipleSelect
                        dataSource={dealList}
                        keyField="key"
                        valueField="name"
                        statusField="checked"
                        maxDisplayItemCount={1}
                        noneSelectedLabel="None Select"
                        selectedAllLabel="All Deals"
                        hasInputOption={true}
                        onChange={handleChangeDeals} />
                </FormGroup>
                <Field className="my-sm-1 mr-sm-2" name="statusId" value={params.status} onChange={handleChange} label="Status" selectOptions={statusOptions} type='select' />
                <Field className="my-sm-1 mr-sm-2" name="viewBy" value={params.viewBy} onChange={handleChange} label="View By" selectOptions={viewByOptions} type='select' />
                <FormGroup className="my-sm-1 mr-sm-2">
                    <Label className="mr-sm-2">{globalResource.From}</Label>
                    <DatePicker
                        minDate={minDate}
                        maxDate={nowString}
                        date={params.fromDate}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                        onChange={handleChangeFromDate} />
                </FormGroup>
                <FormGroup className="my-sm-1 mr-sm-2">
                    <Label className="mr-sm-2">{globalResource.To}</Label>
                    <DatePicker
                        minDate={params.fromDate}
                        maxDate={nowString}
                        date={params.toDate}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                        onChange={handleChangeToDate} />
                </FormGroup>
                <Button outline color="primary" type="button" onClick={handleClickSearch}>
                    <span className="oi oi-magnifying-glass"></span> Search
                </Button>
            </Form>
        </div>
    )
}
function mapStateToProps(state) {
    return Object.assign({}, {
        pagingOption: state.pagingOption,
        params: state.params,
        today: state.today,
        deals: state.deals
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParams, toggleSubmit }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Filter));