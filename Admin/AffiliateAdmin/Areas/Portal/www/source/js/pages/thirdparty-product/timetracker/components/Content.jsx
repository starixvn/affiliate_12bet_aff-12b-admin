import React, { PureComponent } from 'react';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

export default class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    _renderHeader(){
        return (
            <Row>
                <Cell header={true} colWidth={50}>Product</Cell>
                <Cell header={true} colWidth={100}>Currency</Cell>
                <Cell header={true} colWidth={100}>CallTime</Cell>
            </Row>
        )
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem));
        }

        return dataRows;
    }

    _buildDataRow(dataItem) {
        return (
            <Row>
                <Cell className="text-center">{this.props.productName}</Cell>
                <Cell className="text-center">{dataItem.Currency !== 0 ? dataItem.Currency : null}</Cell>
                <Cell className="text-center">{dataItem.CallTimeText}</Cell>
            </Row>
        )
    }

    render() {
        const { data } = this.props;

        return data && (
            <div className="table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={false}
                    width={700}
                    bodyHeight={300}
                    header={this._renderHeader()}
                    body={this._renderDataSection(data)}
                    containerPadding={15}
                />
            </div>
        );
    }
}