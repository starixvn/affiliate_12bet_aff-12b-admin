import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText, globalResource } from '../lib/constants';
import { fetchData, setTogglePopup, setSelectedCurrency, setShouldResetScrollPosition } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

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
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Currency}>{dataItem.Currency}</Cell>
                <Cell title={dataItem.ActualRate}>
                    <Button color="link" className="p-0"
                        onClick={() => this._handleSelectedCurrency({
                            ExchangeId: dataItem.ExchangeId, Currency: dataItem.Currency, Rate: dataItem.ActualRate, isActualRate: true
                        })}>
                        {dataItem.ActualRate}
                    </Button>
                </Cell>
                <Cell title={dataItem.BuyingRate}>
                    <Button color="link" className="p-0"
                        onClick={() => this._handleSelectedCurrency({
                            ExchangeId: dataItem.ExchangeId, Currency: dataItem.Currency, Rate: dataItem.BuyingRate, isActualRate: false
                        })}>
                        {dataItem.BuyingRate}
                    </Button>
                </Cell>
            </Row>
        )
    }

    _handleSelectedCurrency = (param) => {
        this.props.setTogglePopup(true);
        this.props.setSelectedCurrency(param);
    }

    render() {
        const { gridData } = this.props;

        return gridData && (
            <div className="data-content">
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
    return bindActionCreators({ fetchData, setSelectedCurrency, setTogglePopup, setShouldResetScrollPosition }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);