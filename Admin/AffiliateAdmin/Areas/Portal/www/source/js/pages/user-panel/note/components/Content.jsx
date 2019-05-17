import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';
import { maxWidth } from '../../../../modules/constants';

import { setShowPopup, setNoteInfo, getNoteList, getNoteContentById, toggleLoader } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);
    }

    componentDidMount() {
        const { pagingOption, affId } = this.props;
        const param = Object.assign({}, { affId, pageIndex: 1, pageSize: pagingOption.PageSize });

        this.props.getNoteList(param);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={35} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={580} title={headerText.Content}>{headerText.Content}</Cell>
                <Cell header={true} colWidth={150} title={headerText.CreatedBy}>{headerText.CreatedBy}</Cell>
                <Cell header={true} colWidth={200} title={headerText.LastModify}>{headerText.LastModify}</Cell>
                <Cell header={true} colWidth={35} title={globalResource.Edit}>{globalResource.Edit}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="5">{globalResource.NoInfo}</Cell>
            </Row>);
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
        if (noteId === 0) {
            this.props.setNoteInfo({ noteId, noteContent: '' });
        }
        else {
            this.getNoteContentById(noteId);
        }

        this.props.setShowPopup(true);
    }

    async getNoteContentById(noteId) {
        this.props.toggleLoader(true);

        const { affId } = this.props;
        const data = await this.props.getNoteContentById({ affId, noteId });

        if (data) {
            const { NoteId, NoteContent } = data;
            const noteItem = { noteId: NoteId, noteContent: NoteContent };
            this.props.setNoteInfo(noteItem);
        }

        this.props.toggleLoader(false);
    }

    handlePaging = (pageIndex, pageSize) => {
        const newParam = { affId: this.props.affId, pageIndex, pageSize };
        this.props.getNoteList(newParam);
    };

    render() {
        const { noteList, pagingOption } = this.props;

        return noteList && pagingOption && (
            <div className="data-content">
                <div className="my-2">
                    <Button outline type="button" onClick={() => this.handleShowPopup(0)}>{globalResource.Add}</Button>
                </div>
                <div className="table-responsive table-striped">
                    <Table
                        minWidth={1000}
                        maxWidth={maxWidth}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(noteList)}
                        containerPadding={10}
                        isPaging={true}
                        pageOption={this.props.pagingOption}
                        onPaging={this.handlePaging}
                    />
                </div>
            </div>
        );
    }
}


function mapStateToProps({ affCommonInfo, userPanel }) {
    const { pagingOption, noteList, noteInfo } = userPanel;
    const { affId } = affCommonInfo;

    return Object.assign({}, {
        affId,
        pagingOption: pagingOption ? pagingOption : null,
        noteList: noteList ? noteList : null,
        noteInfo
    });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopup, setNoteInfo, toggleLoader, getNoteList, getNoteContentById }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);