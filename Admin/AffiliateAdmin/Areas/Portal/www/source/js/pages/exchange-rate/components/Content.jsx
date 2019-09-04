import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource, rateType } from '../lib/constants';
import { fetchData, setTogglePopup, setSelectedExchangeRate, setShouldResetScrollPosition } from '../actions';
import { format } from '../../../modules/numberFormat';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
        
        this.state = {
            isUpdated: true
        }

    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;
        const date = new Date(param.date);
        const now = new Date();
        const isUpdated = date.getMonth() == now.getMonth() && date.getDate() == now.getDate() && date.getFullYear() == now.getFullYear()

        this.setState({ isUpdated });

        if (param && param !== prevProps.param) {
            this.props.fetchData(param);
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
        const { isUpdated } = this.state;
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Currency}>{dataItem.Currency}</Cell>
                <Cell title={dataItem.ActualRate}>
                    {
                        isUpdated && dataItem.ExchangeId !== 3 ? <Button color="link" className="p-0"
                            onClick={() => this._handleSelectedExchangeRate({
                                ExchangeId: dataItem.ExchangeId, Currency: dataItem.Currency, Rate: dataItem.ActualRate, RateType: rateType.ActualRate
                            })}>
                            {format(dataItem.ActualRate, 5)}
                        </Button> : format(dataItem.ActualRate, 5)
                    }

                </Cell>
                <Cell title={dataItem.BuyingRate}>
                    {
                        isUpdated && dataItem.ExchangeId !== 3 ? <Button color="link" className="p-0"
                            onClick={() => this._handleSelectedExchangeRate({
                                ExchangeId: dataItem.ExchangeId, Currency: dataItem.Currency, Rate: dataItem.BuyingRate, RateType: rateType.BuyingRate
                            })}>
                            {format(dataItem.BuyingRate, 5)}
                        </Button> : format(dataItem.BuyingRate, 5)
                    }
                </Cell>
            </Row>
        )
    }

    _handleSelectedExchangeRate = (param) => {
        this.props.setTogglePopup(true);
        this.props.setSelectedExchangeRate(param);
    }

    render() {
        const { gridData } = this.props;

        return gridData && (
            <div className="data-content mx-1 mt-2">
                <div className="table-responsive table-striped">
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

function mapStateToProps({ param, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { param, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setSelectedExchangeRate, setTogglePopup, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);