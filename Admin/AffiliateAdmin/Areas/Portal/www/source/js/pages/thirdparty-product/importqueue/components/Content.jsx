import React, { PureComponent } from 'react';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { resolveClientUrl } from '../../../../modules/utils';

export default class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    _renderHeader(){
        return (
            <Row>
                <Cell header={true} colWidth={50}>QueueId</Cell>
                <Cell header={true} colWidth={100}>CheckInTime</Cell>
                <Cell header={true} colWidth={100}>LogDesc</Cell>
                <Cell header={true} colWidth={50}></Cell>
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
        const detailUrl = resolveClientUrl('ThirdPartyProduct/ImportQueue/XmlData?queueId=' + dataItem.QueueId);
        return (
            <Row>
                <Cell className="text-center">{dataItem.QueueId}</Cell>
                <Cell className="text-center">{dataItem.CheckInTimeText}</Cell>
                <Cell className="text-center">{dataItem.LogDesc}</Cell>
                <Cell className="text-center"><a href={detailUrl} target='_blank'>View Data</a></Cell>
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