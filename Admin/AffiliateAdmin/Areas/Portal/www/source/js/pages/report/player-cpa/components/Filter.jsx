import React, { useState } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { setReportTitle, toggleSubmit } from '../actions';
import { Button, Form, FormGroup, Label } from 'reactstrap';
import { setParams } from '../actions';
import Field from '../../../../components/Field';
import { globalResource } from '../../../../modules/globalResource';
import { minDate } from '../../../../modules/constants';
import { DatePicker } from '@9softstudio/react-datepicker';
import { statusOptions, viewByOptions, MAX_MONTH_DIFF, DATE_FORMAT, defaultParams } from '../libs/constants';
import moment from 'moment';

const Filter = ({ params, setParams, toggleSubmit }) => {
    const [maxToDate, setMaxToDate] = useState(moment(defaultParams.toDate));
    const handleChange = (e) => {
        setParams({
            [e.target.name]: e.target.value
        })
    }

    const onChangeDate = (date, key) => {
        const selectedDate = moment(date);
        if (key === 'fromDate') {
            const fromDate = selectedDate.clone();
            const maxRange = fromDate.clone().add(MAX_MONTH_DIFF, 'month');
            const currentToDate = moment(params.toDate);

            if(currentToDate.isAfter(fromDate) && currentToDate.isBefore(maxRange)) {
                setParams({
                    ...params,
                    fromDate: fromDate.format(DATE_FORMAT),
                })
    
            }
            else {
                setParams({
                    ...params,
                    fromDate: fromDate.format(DATE_FORMAT),
                    toDate: fromDate.add(MAX_MONTH_DIFF, 'month').format(DATE_FORMAT),
                })
            }

            setMaxToDate(maxRange.endOf('month'));
        } else {
            setParams({
                ...params,
                toDate: selectedDate.endOf('month').format(DATE_FORMAT)
            });
        }
    }

    const handleClickSearch = () => {
        toggleSubmit()
    }

    return (
        <div className="filter-container">
            <Form inline tag="div">
                <Field className="my-sm-1 mr-sm-2" name="userName" value={params.userName} onChange={handleChange} label="Affiliate"/>
                <Field className="my-sm-1 mr-sm-2" name="custName" value={params.custName} onChange={handleChange} label="Player"/>
                <Field className="my-sm-1 mr-sm-2" name="dealName" value={params.dealName} onChange={handleChange} label="Deal Name"/>
                <Field className="my-sm-1 mr-sm-2" name="statusId" value={params.status} onChange={handleChange} label="Status" selectOptions={statusOptions} type='select'/>
                <Field className="my-sm-1 mr-sm-2" name="viewBy" value={params.viewBy} onChange={handleChange} label="View By" selectOptions={viewByOptions} type='select'/>
                <FormGroup className="my-sm-1 mr-sm-2">
                    <Label className="mr-sm-2">{globalResource.From}</Label>
                    <DatePicker 
                        minDate={minDate} 
                        maxDate={moment().format(DATE_FORMAT)} 
                        date={params.fromDate}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                        onChange={(date) => onChangeDate(date, 'fromDate')} />
                </FormGroup>
                <FormGroup className="my-sm-1 mr-sm-2">
                    <Label className="mr-sm-2">{globalResource.To}</Label>
                    <DatePicker 
                        minDate={params.fromDate} 
                        maxDate={maxToDate.format(DATE_FORMAT)} 
                        date={params.toDate}
                        iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                        onChange={(date) => onChangeDate(date, 'toDate')} />
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
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setReportTitle, setParams, toggleSubmit }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);