import React, { useEffect, useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import { setParams, toggleSubmit } from '../actions';
import { globalResource } from '../../../../modules/globalResource';
import { viewByOptions } from '../libs/constants';
import { formatUnit, handleAfterChangeFromDate, handleAfterChangeToDate } from '../../../../modules/utils';
import { minDate, sixMonth } from '../../../../modules/constants';

const Filter = ({ setParams, params, toggleSubmit, today }) => {
    const [nowString, setNowString] = useState('');

    useEffect(() => {
        const now = new Date(today);
        const currentMonth = now.getMonth() + 1;
        const currentMonthString = formatUnit(currentMonth);
        const currentYear = now.getFullYear();

        const nowString = `${currentMonthString}/${formatUnit(now.getDate())}/${currentYear}`;
        setNowString(nowString);
        setParams({
            fromDate: `${currentMonthString}/01/${currentYear}`,
            toDate: nowString
        });
    }, [today]);


    const onEnterPress = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();
            onSearch();
        }
    }

    const onChange = (event) => {
        const { name, value } = event.target;
        setParams({ [name]: value });
    }

    const handleChangeFromDate = (date) => {
        setParams({ ...handleAfterChangeFromDate(date, params.toDate, sixMonth) });
    }
    const handleChangeToDate = (date) => {
        setParams({ ...handleAfterChangeToDate(date, params.fromDate, sixMonth) });
    }

    const onSearch = () => {
        toggleSubmit();
    }

    return (
        <div className="filter-container">
            <Form inline tag="div">
                <FormGroup className="my-sm-1 mr-sm-2">
                    <Label htmlFor="Affiliate" className="mr-sm-2">Affiliate</Label>
                    <Input type="text" name="username" id="Affiliate" maxLength="30"
                        onKeyPress={onEnterPress}
                        onChange={onChange}
                        value={params.username} />
                </FormGroup>
                <FormGroup className="my-sm-1 mr-sm-2">
                    <Label htmlFor="Deal" className="mr-sm-2">Deal Name</Label>
                    <Input type="text" name="dealName" id="Deal" maxLength="30"
                        onKeyPress={onEnterPress}
                        onChange={onChange}
                        value={params.dealName} />
                </FormGroup>
                <FormGroup className="my-sm-1 mr-sm-2">
                    <Label htmlFor="ViewBy" className="mr-sm-2">View By</Label>
                    <Input type="select" name="viewBy" id="ViewBy"
                        onKeyPress={onEnterPress}
                        onChange={onChange}
                        value={params.viewBy} >
                        {
                            viewByOptions.map(v => <option key={v.value} value={v.value}>{v.label}</option>)
                        }
                    </Input>
                </FormGroup>

                <FormGroup className="my-sm-1 mr-sm-2">
                    <Label className="mr-sm-2">{globalResource.From}</Label>
                    <DatePicker
                        minDate={minDate}
                        maxDate={nowString}
                        date={params.fromDate}
                        onChange={handleChangeFromDate}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />

                </FormGroup>
                <FormGroup className="my-sm-1 mr-sm-2"
                    key={`${params.fromDate}-${params.toDate}`}>
                    <Label className="mr-sm-2">{globalResource.To}</Label>
                    <DatePicker
                        minDate={params.fromDate}
                        maxDate={nowString}
                        date={params.toDate}
                        onChange={handleChangeToDate}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)} />

                </FormGroup>
                <Button outline color="primary" type="button" onClick={onSearch}>
                    <span className="oi oi-magnifying-glass"></span> Search
                </Button>
            </Form>
        </div>
    )
}

function mapStateToProps(state) {
    return Object.assign({}, {
        pagingOption: state.pagingOption,
        isProduction: state.isProduction,
        today: state.today,
        params: state.params,
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParams, toggleSubmit }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);