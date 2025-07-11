import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { setReportTitle, setParams, toggleSubmit } from '../actions';
import { globalResource } from '../../../../modules/globalResource';
import { viewByOptions } from '../libs/constants';
import { minMonthDate } from '../../affiliate-report/libs/constants';
import MonthPicker from '@9softstudio/react-monthpicker';
import moment from 'moment';

const Filter = ({ setParams, params, toggleSubmit }) => {
    const onEnterPress = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();
            onSearch();
        }
    }

    const onChange = (event) => {
        const { name, value } = event.target;
        setParams({ ...params, [name]: value });
    }

    const onMonthChange = (month, year, field) => {
        const firstDay = 1;
        const lastDay = moment(`${year}-${month}-01`).endOf('month').date();

        const day = field === 'fromDate' ? firstDay : lastDay;
        const date = moment(`${year}-${month}-${day}`);

        if(field === 'fromDate' && date.isAfter(moment(params.toDate))) {
            setParams({ 
                ...params, 
                [field]: moment(`${year}-${month}-${firstDay}`).format('MM/DD/YYYY'),
                toDate: moment(`${year}-${month}-${lastDay}`).format('MM/DD/YYYY')
            });
        }
        else {
            setParams({ ...params, [field]: date.format('MM/DD/YYYY') });
        }

    }

    const onSearch = () => {
        if(moment(params.fromDate).isAfter(moment(params.toDate))) {
            alert('From date must be before to date');
            return;
        }
        toggleSubmit();
    }

    const getMonth = (date) => {
        return moment(date).month() + 1;
    }

    const getYear = (date) => {
        return moment(date).year();
    }

    return (
        <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="my-sm-1 mr-sm-5">
                        <Label htmlFor="Affiliate" className="mr-sm-2">Affiliate</Label>
                        <Input type="text" name="username" id="Affiliate" maxLength="30"
                            onKeyPress={onEnterPress}
                            onChange={onChange}
                            value={params.username} />
                    </FormGroup>
                    <FormGroup className="my-sm-1 mr-sm-5">
                        <Label htmlFor="Deal" className="mr-sm-2">Deal Name</Label>
                        <Input type="text" name="dealName" id="Deal" maxLength="30"
                            onKeyPress={onEnterPress}
                            onChange={onChange}
                            value={params.dealName} />
                    </FormGroup>
                    <FormGroup className="my-sm-1 mr-sm-5">
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
                   
                    <FormGroup className="my-sm-1 mr-sm-5">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <MonthPicker 
                            minMonth={minMonthDate.month} 
                            minYear={minMonthDate.year}
                            selectedDropdownYear={getYear(params.fromDate)}
                            selectedMonth={getMonth(params.fromDate)}
                            onSelect={(month, year) => onMonthChange(month, year, 'fromDate')}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}/>
                        
                    </FormGroup>
                    <FormGroup className="my-sm-1 mr-sm-5" 
                        key={`${params.fromDate}-${params.toDate}`}>
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <MonthPicker 
                            minMonth={getMonth(params.fromDate)} 
                            minYear={getYear(params.fromDate)}
                            selectedDropdownYear={getYear(params.toDate)}
                            selectedMonth={getMonth(params.toDate)}
                            onSelect={(month, year) => onMonthChange(month, year, 'toDate')}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}/>
                        
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
    return bindActionCreators({ setReportTitle, setParams, toggleSubmit }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);