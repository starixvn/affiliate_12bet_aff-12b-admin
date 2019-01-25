import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Modal, ModalHeader, ModalBody, Button } from 'reactstrap';
import { Pager } from '@9softstudio/react-fixed-table-header';
import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { setNoteList, setShowPopupEdit, setParam } from '../actions';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: props.noteList,
            shouldResetScrollPosition: true
        }
    }

    _renderHeader() {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={500} title={headerText.Content}>{headerText.Content}</Cell>
                <Cell header={true} colWidth={150} title={headerText.CreatedBy}>{headerText.CreatedBy}</Cell>
                <Cell header={true} colWidth={200} title={headerText.LastModify}>{headerText.LastModify}</Cell>
                <Cell header={true} colWidth={30} title={globalResource.Edit}>{globalResource.Edit}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, i));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.Content}>{dataItem.Content}</Cell>
                <Cell title={dataItem.CreatedBy}>{dataItem.CreatedBy}</Cell>
                <Cell title={dataItem.LastModifyText} className="text-center">{dataItem.LastModifyText}</Cell>
                <Cell title="Edit Note" className="text-center">
                    <Button color="link" onClick={() => this.handleEditNote(dataItem.NoteId, dataItem.Content)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    handleEditNote = (noteId, content) => {
        const { param } = this.props;
        const newParam = Object.assign({}, param, { Content: content })

        this.props.setShowPopupEdit(true);
        this.props.setParam(newParam);
    }

    handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        }
    };

    render() {
        const { shouldResetScrollPosition } = this.props;
        const { gridData } = this.state;

        return gridData && (
            <div className="data-content">
                <div className="my-2">
                    <Button outline type="button">{globalResource.Add}</Button>
                </div>
                <div className="table-responsive table-striped">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={20}
                        isPaging={true}
                        pageOption={this.props.pagingOption}
                        onPaging={this.handlePaging}
                        shouldResetScrollPosition={shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, noteList, param }) {
    return Object.assign({}, { pagingOption, noteList, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setNoteList, setShowPopupEdit, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);