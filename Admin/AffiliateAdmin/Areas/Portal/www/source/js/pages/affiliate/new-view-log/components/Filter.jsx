import React from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Button, Form, FormGroup, Label, Input } from 'reactstrap';
import { DatePicker } from '@9softstudio/react-datepicker';

import { headerText, globalResource } from '../libs/constants';
import { setMainViewParam, setDetailViewParam, getLogActionList } from '../actions';
import { sixMonth } from '../../../../modules/constants';
import { handleAfterChangeFromDate, handleAfterChangeToDate } from '../../../../modules/utils';

const minDate = '3/25/2021';

export class Filter extends React.Component {
    constructor(props) {
        super(props);

        const { today } = this.props;
        const now = new Date(today);

        this.nowString = `${now.getMonth() + 1}/${now.getDate()}/${now.getFullYear()}`;

        const sixMonthAgo = new Date(today);
        sixMonthAgo.setMonth(sixMonthAgo.getMonth() - sixMonth);

        this.state = {
            affName: '',
            logActionIds: '',
            fromDate: sixMonthAgo < new Date(minDate) ? minDate : `${sixMonthAgo.getMonth() + 1}/${sixMonthAgo.getDate()}/${sixMonthAgo.getFullYear()}`,
            toDate: this.nowString,
            logActionList: null
        }
    }

    componentDidMount() {
        this.handleGetLogActionList();
    }

    componentDidUpdate(prevProps) {
        const { selectedAff } = this.props;

        if (!selectedAff && selectedAff !== prevProps.selectedAff) {
            const { mainViewParam } = this.props;

            this.setState({ ...mainViewParam });
        }

        if (!!selectedAff && selectedAff !== prevProps.selectedAff) {
            const { detailViewParam } = this.props;

            this.setState({ logActionIds: detailViewParam.logActionIds, fromDate: detailViewParam.fromDate, toDate: detailViewParam.toDate });
        }
    }

    handleGetLogActionList = async () => {
        var logActionList = await this.props.getLogActionList();

        this.setState({ logActionList });
    }

    _handleChange = (event) => {
        this.setState({ [event.target.name]: event.target.value });
    }

    _handleChangeFromDate = (momentDate) => {
        this.setState(handleAfterChangeFromDate(momentDate, this.state.toDate, sixMonth));
    }

    _handleChangeToDate = (momentDate) => {
        this.setState(handleAfterChangeToDate(momentDate, this.state.fromDate, sixMonth));
    }

    _handleSearch = () => {
        const { selectedAff } = this.props;
        const { affName, logActionIds, fromDate, toDate } = this.state;

        !selectedAff ? this.props.setMainViewParam({ affName, logActionIds, fromDate, toDate }) : this.props.setDetailViewParam({ logActionIds, fromDate, toDate });
    }

    _handleKeyPress = (event) => {
        if (event.charCode === 13) {
            event.preventDefault();
            this._handleSearch();
        }
    }

    render() {
        const { selectedAff } = this.props;
        const { affName, logActionIds, fromDate, toDate, logActionList } = this.state;

        return (
            <div className="filter-container">
                <Form inline tag="div">
                    {
                        !selectedAff && (
                            <FormGroup className="mr-sm-2 my-2">
                                <Label for="affName" className="mr-sm-2">{headerText.Affiliate}</Label>
                                <Input type="text" name="affName" id="affName" value={affName} onChange={this._handleChange} onKeyPress={this._handleKeyPress} />
                            </FormGroup>
                            )
                    }
                    <FormGroup className="mr-sm-2 my-2">
                        <Label for="logActionIds" className="mr-sm-2">{headerText.Action}</Label>
                        <Input type="select" name="logActionIds" id="logActionIds" value={logActionIds} onChange={this._handleChange}>
                            <option value="">{globalResource.All}</option>
                            {
                                logActionList && logActionList.map(item => {
                                    return (<option key={`logAction-${item.ActionIds}`} value={item.ActionIds}>{item.ActionName}</option>)
                                })
                            }
                        </Input>
                    </FormGroup>
                    <FormGroup className="mr-sm-2 my-2">
                        <Label className="mr-sm-2">{globalResource.From}</Label>
                        <DatePicker minDate={minDate} maxDate={this.nowString} date={fromDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this._handleChangeFromDate} />
                    </FormGroup>
                    <FormGroup className="mr-sm-2 my-2">
                        <Label className="mr-sm-2">{globalResource.To}</Label>
                        <DatePicker minDate={fromDate} maxDate={this.nowString} date={toDate}
                            iconElement={(<span className="oi oi-calendar icon-calendar ml-1"></span>)}
                            onChange={this._handleChangeToDate} />
                    </FormGroup>
                    <Button outline color="primary" type="button" onClick={this._handleSearch}>
                        <span className="oi oi-magnifying-glass"></span> {globalResource.Search}
                    </Button>
                </Form>
            </div>
        )
    }
}

function mapStateToProps({ today, selectedAff, mainViewParam, detailViewParam }) {
    return Object.assign({}, { today, selectedAff, mainViewParam, detailViewParam });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setMainViewParam, setDetailViewParam, getLogActionList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Filter);