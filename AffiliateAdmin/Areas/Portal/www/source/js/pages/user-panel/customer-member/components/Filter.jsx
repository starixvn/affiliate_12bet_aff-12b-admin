import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';
import { memberTypes, resource } from '../modules/constants';
import { setParam } from '../actions';

const minDate = new Date(2009, 11, 1);
export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const now = new Date(this.props.today);
        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        this.state = {
            MemberType: '0',
            FromDate: `${now.getMonth() + 1}/01/${now.getFullYear()}`,
            ToDate: this.nowString,
            IsGetAll: false
        }
    }

    handleChangeInput = (event) => {
        const target = event.target;
        const value = target.type === 'checkbox' ? target.checked : target.value;
        const name = target.name;

        this.setState({
            [name]: value
        });
    }

    handleChangeFromDate = (momentDate) => {
        const { ToDate } = this.state;
        const selectedDateString = momentDate.format("MM/DD/YYYY");
        const fromDate = new Date(selectedDateString);
        const toDate = new Date(ToDate);

        const isOutsideRange = fromDate.getTime() > toDate.getTime();
        const newState = isOutsideRange ? { FromDate: selectedDateString, ToDate: selectedDateString } : { FromDate: selectedDateString }

        this.setState(newState);
    }

    handleChangeToDate = (momentDate) => {
        this.setState({ ToDate: momentDate.format("MM/DD/YYYY") });
    }

    handleSearch = () => {
        const param = Object.assign({}, this.state, { PageIndex: 1, AffId: this.props.affId });
        this.props.setParam(param);
    }

    render() {
        const { MemberType, FromDate, ToDate, IsGetAll } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label for="Affiliate" className="mr-sm-2">{resource.Member}</Label>
                        <Input type="select" name="MemberType" id="MemberType" value={MemberType} onChange={this.handleChangeInput}>
                            <option key="memberType-0" value="0">Select</option>
                            {
                                Object.keys(memberTypes).map(k => {
                                    return (<option key={`memberType-${k}`} value={k}>{memberTypes[k]}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{resource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={FromDate} isEnable={!IsGetAll}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <Label className="mr-sm-2">{resource.To}</Label>
                        <DatePicker minDate={FromDate} maxDate={this.nowString} date={ToDate} isEnable={!IsGetAll}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this.handleChangeToDate} />
                    </FormGroup>
                    <FormGroup className="mb-2 mr-sm-2 mb-sm-0">
                        <FormGroup check>
                            <Label check>
                                {resource.GetAll}
                                <Input name="IsGetAll" className="ml-1 mr-2" type="checkbox" checked={IsGetAll} onChange={this.handleChangeInput} />
                            </Label>
                        </FormGroup>
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this.handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {resource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, affId }) {
    return Object.assign({}, { today, affId });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);