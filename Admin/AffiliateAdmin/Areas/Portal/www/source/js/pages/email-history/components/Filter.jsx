import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import { searchByList, globalResource, minDate, all } from '../modules/constants';
import { getDateArrange } from '../../../modules/utils';
import { setParam } from '../actions';

export class Filter extends PureComponent {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const now = new Date(today);

        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;
        const yesterday = new Date(today);
        yesterday.setDate(yesterday.getDate() - 1);

        this.state = {
            searchBy: all,
            searchText: '',
            hasGetByDate: false,
            fromDate: `${yesterday.getMonth() + 1}/${yesterday.getDate()}/${yesterday.getFullYear()}`,
            toDate: this.nowString,
        }
    }

    componentDidMount() {
        this.handleSetParam();
    }

    handleSetParam() {
        const { searchBy, searchText, fromDate, toDate, hasGetByDate } = this.state;

        const param = Object.assign({}, {
            SearchBy: searchBy,
            SearchText: searchText,
            HasGetByDate: hasGetByDate,
            FromDate: fromDate,
            ToDate: toDate
        });

        this.props.setParam(param);
    }

    handleChange = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? !target.checked : target.value;

        this.setState({ [target.name]: value });
        target.name === "searchBy" && this.setState({ searchText: '' });
    }

    handleChangeFromDate = (momentDate) => {
        this.setState(getDateArrange(momentDate, this.state.toDate));
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ toDate: momentDate.format("MM/DD/YYYY") });
    }

    handlePressEnter = (event) => {
        if (event.charCode === 13) {
            event.preventDefault()
            this.handleSetParam();
        }
    }

    handleSearch = () => {
        this.handleSetParam();
    }

    render() {
        const { searchBy, fromDate, toDate, hasGetByDate, searchText } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-3 mb-sm-0 pr-4">
                        <Label className="mr-2">{globalResource.SearchBy}</Label>
                        <Input type="select" name="searchBy" value={searchBy} onChange={this.handleChange}>
                            {
                                Object.keys(searchByList).map(key => {
                                    return (<option key={'searchBy-' + key} value={key}>{searchByList[key]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    {searchBy >= 0 && (
                        <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                            <Input type="text" name="searchText" value={searchText} onKeyPress={this.handlePressEnter} onChange={this.handleChange} />
                        </FormGroup>
                    )}
                    <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                        <FormGroup check>
                            <Label check>
                                <Input name="hasGetByDate" className="ml-1 mr-2" type="checkbox" checked={hasGetByDate} onChange={this.handleChange} />
                            </Label>
                        </FormGroup>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate} isEnable={!hasGetByDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-3 mb-sm-0">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate} isEnable={!hasGetByDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, param }) {
    return Object.assign({}, { today, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);