import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import { pageList, deviceTypes, headerText } from '../modules/constants';
import { setSelectedPage, setParam } from '../actions';
import { globalResource } from '../../../../modules/globalResource';

const minDate = new Date(2009, 11, 1);

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const now = new Date(this.props.today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = {
            today: now,
            page: '',
            language: '',
            fromDate: `${now.getMonth() + 1}/01/${now.getFullYear()}`,
            toDate: `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`,
            deviceType: 0
        };
    }

    handleChangeDevice = (event) => {
        this.props.setDeviceType(event.target.value);
    }

    handleChangeFromDate = (momentDate) => {
        const selectedDateString = momentDate.format("MM/DD/YYYY");
        const fromDate = new Date(selectedDateString);
        const toDate = new Date(this.state.toDate);

        const isOutsideRange = fromDate.getTime() > toDate.getTime();
        const newToDate = isOutsideRange ? selectedDateString : this.state.toDate;

        this.setState({ fromDate: selectedDateString, toDate: newToDate });
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ toDate: momentDate.format("MM/DD/YYYY") });
    }

    handleSearch = () => {
        const newParam = Object.assign({}, this.props.param, this.state);
        this.props.setParam(newParam);
        this.props.setSelectedPage('');
    }

    render() {
        const { page, language, deviceType, fromDate, toDate } = this.state;
        const { languageList } = this.props;
        console.log(languageList);
        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="page" className="mr-sm-2">{headerText.Device}</Label>
                        <Input type="select" name="page" id="page" value={page} onChange={this.handleChangeDevice}>
                            <option value="">{globalResource.All}</option>
                            {
                                pageList.map(index => {
                                    return (<option key={`page-${index}`} value={index}>{index}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="language" className="mr-sm-2">{headerText.Device}</Label>
                        <Input type="select" name="language" id="language" value={language} onChange={this.handleChangeDevice}>
                            <option value="">{globalResource.All}</option>
                            {
                                Object.keys(languageList).map(index => {
                                    return (<option key={`language-${index}`} value={index}>{languageList[index]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="deviceType" className="mr-sm-2">{headerText.Device}</Label>
                        <Input type="select" name="deviceType" id="deviceType" value={deviceType} onChange={this.handleChangeDevice}>
                            {
                                deviceTypes.map(index => {
                                    return (<option key={`deviceType-${index}`} value={index}>{index}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, param, languageList }) {
    return Object.assign({}, { today, param, languageList });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedPage, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);