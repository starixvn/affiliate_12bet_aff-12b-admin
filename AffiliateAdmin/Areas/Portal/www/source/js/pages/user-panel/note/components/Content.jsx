import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { setShowPopup, setNoteInfo, setPagingOption, getNoteList } from '../actions';
import { toggleLoader } from '../../../../actions/loaderAction';

export class Content extends Component {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { noteInfo, pagingOption } = this.props;
        const param = Object.assign({}, { AffId: noteInfo.AffId, PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize });
        this.props.getNoteList(param);
    }

    _renderHeader() {
        return (
            <Row key="theard">
                <Cell header={true} colWidth={40} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={1000} title={headerText.Content}>{headerText.Content}</Cell>
                <Cell header={true} colWidth={200} title={headerText.CreatedBy}>{headerText.CreatedBy}</Cell>
                <Cell header={true} colWidth={250} title={headerText.LastModify}>{headerText.LastModify}</Cell>
                <Cell header={true} colWidth={60} title={globalResource.Edit}>{globalResource.Edit}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];
        const { pagingOption } = this.props;
        const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

        for (let i = 0; i < dataList.length; i++) {
            const dataItem = dataList[i];
            dataRows.push(this._buildDataRow(dataItem, i + startIndex));
        }

        return dataRows;
    }

    _buildDataRow(dataItem, index) {
        return (
            <Row key={`dataRow-${index}`}>
                <Cell title={index} className="text-center">{index}</Cell>
                <Cell title={dataItem.NoteContent}>{dataItem.NoteContent}</Cell>
                <Cell title={dataItem.CreatedBy}>{dataItem.CreatedBy}</Cell>
                <Cell title={dataItem.LastModifyText} className="text-center">{dataItem.LastModifyText}</Cell>
                <Cell title="Edit Note" className="text-center">
                    <Button className="p-0" color="link" onClick={() => this.handleShowPopup(dataItem.NoteId)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    handleShowPopup = (noteId) => {
        const newNoteInfo = Object.assign({}, this.props.noteInfo, { NoteId: noteId, NoteContent: '' });

        noteId === 0 ? this.props.setNoteInfo(newNoteInfo) : this.getNoteContentById(newNoteInfo);

        this.props.setShowPopup(true);
    }

    async getNoteContentById(noteInfo) {
        const url = 'Portal/UserPanel/Note/GetNoteById';
        const response = await post(url, noteInfo);

        if (response && response.status === 200) {
            const newNoteInfo = Object.assign({}, noteInfo, { NoteContent: response.data.NoteContent });
            this.props.setNoteInfo(newNoteInfo);
        }
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };

        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, newPaging, { AffId: this.props.noteInfo.AffId });
        this.props.getNoteList(newParam);
    };

    render() {
        const { noteList } = this.props;

        return noteList && (
            <div className="data-content">
                <div className="my-2">
                    <Button outline type="button" onClick={() => this.handleShowPopup(0)}>{globalResource.Add}</Button>
                </div>
                <div className="table-responsive table-striped">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(noteList)}
                        containerPadding={20}
                        isPaging={true}
                        pageOption={this.props.pagingOption}
                        onPaging={this.handlePaging}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ pagingOption, noteList, noteInfo }) {
    return Object.assign({}, { pagingOption, noteList, noteInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopup, setNoteInfo, setPagingOption, toggleLoader, getNoteList }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);