import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource, rateType, exchangeUs$Id } from '../libs/constants';
import { setTogglePopup, setSelectedExchangeRate, setShouldResetScrollPosition } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            isToday: true
        }

    }

    componentDidUpdate(prevProps) {
        const { date } = this.props;
        if (date !== prevProps.date) {
            const selectedDate = new Date(date);
            const now = new Date(this.props.today);
            const isToday = selectedDate.getMonth() === now.getMonth() && selectedDate.getDate() === now.getDate() && selectedDate.getFullYear() === now.getFullYear()

            this.setState({ isToday });
        }
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={350} title={headerText.Currency}>{headerText.Currency}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Selling}>{headerText.Selling}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Buying}>{headerText.Buying}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="4">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        const { isToday } = this.state;
        const { hasPermission } = this.props;
        const actualRate = dataItem.ActualRate;
        const buyingRate = dataItem.BuyingRate;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={dataItem.Currency}>{dataItem.Currency}</Cell>
                <Cell className="text-center" title={dataItem.ActualRate}>
                    {
                        hasPermission && isToday && dataItem.ExchangeId !== exchangeUs$Id ? <Button color="link" className="p-0"
                            onClick={() => this._handleSelectedExchangeRate({
                                ExchangeId: dataItem.ExchangeId, Currency: dataItem.Currency, Rate: actualRate, RateType: rateType.ActualRate
                            })}>
                            {actualRate}
                        </Button> : actualRate
                    }

                </Cell>
                <Cell className="text-center" title={dataItem.BuyingRate}>
                    {
                        hasPermission && isToday && dataItem.ExchangeId !== exchangeUs$Id ? <Button color="link" className="p-0"
                            onClick={() => this._handleSelectedExchangeRate({
                                ExchangeId: dataItem.ExchangeId, Currency: dataItem.Currency, Rate: buyingRate, RateType: rateType.BuyingRate
                            })}>
                            {buyingRate}
                        </Button> : buyingRate
                    }
                </Cell>
            </Row>
        )
    }

    _handleSelectedExchangeRate = (selectedExchangeRate) => {
        this.props.setTogglePopup(true);
        this.props.setSelectedExchangeRate(selectedExchangeRate);
        this.props.setShouldResetScrollPosition(false);
    }

    render() {
        const { gridData } = this.props;

        return gridData && (
            <div className="data-content">
                <div className="table-responsive table-striped my-2">
                    <Table
                        maxWidth={1000}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        shouldResetScrollPosition={this.props.shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}

function mapStateToProps({ today, date, shouldResetScrollPosition, gridData, hasPermission }) {
    return Object.assign({}, { today, date, shouldResetScrollPosition, gridData, hasPermission });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedExchangeRate, setTogglePopup, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);