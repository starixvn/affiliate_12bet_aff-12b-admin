import React, { Component } from 'react';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

export default class List extends Component {
    constructor(props) {
        super(props);

        this.state = {
        }
    }

    _renderHeader() {
        return (
            <Row>
                <Cell header={true} colWidth={20}></Cell>
                <Cell header={true} colWidth={30}>#</Cell>
                <Cell header={true} colWidth={200} title="Landing Page Name (Shown in dropdown)">Landing Page Name (Shown in dropdown)</Cell>
                <Cell header={true} colWidth={200} title="Landing Page Code (Contained in links)">Landing Page Code (Contained in links)</Cell>
                <Cell header={true} colWidth={50} title="Edit">Edit</Cell>
            </Row>
        )
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, i + 1));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, rowNumber) {
        return (
            <Row>
                <Cell className="text-center">
                    <input type="checkbox" checked={this.props.selectedPages[dataItem.PageId]} 
                        onChange={(e) => this.props.onCheckItem(e.target.checked, dataItem.PageId)} />
                </Cell>
                <Cell className="text-center" title={rowNumber}>{rowNumber}</Cell>
                <Cell className="text-left" title={dataItem.Name}>{dataItem.PageName}</Cell>
                <Cell className="text-left" title={dataItem.Code}>{dataItem.PageCode}</Cell>
                <Cell className="text-center">
                    <button className="btn btn-link" onClick={() => this.props.onEdit(dataItem.PageId)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </button>
                </Cell>
            </Row>
        )
    }

    render() {
        const { dataList } = this.props;

        return dataList && (
            <div>
                <div className="selection">
                    <span>Select:</span>
                    <Button type="button" color="link" className="text-dark font-weight-bold" onClick={() => this.props.onToggleAll(true)}>
                        <u>All</u>
                    </Button>
                    <Button type="button" color="link" className="text-dark font-weight-bold" onClick={() => this.props.onToggleAll(false)}>
                        <u>None</u>
                    </Button>
                </div>

                <div className="main-content my-2">
                    <div className="table-responsive table-striped">
                        <Table
                            minWidth={1000}
                            autoWidth={true}
                            bodyHeight={300}
                            header={this._renderHeader()}
                            body={this._renderDataSection(dataList)}
                            containerPadding={20}
                        />
                    </div>
                </div>
            </div>
        )
    }
}