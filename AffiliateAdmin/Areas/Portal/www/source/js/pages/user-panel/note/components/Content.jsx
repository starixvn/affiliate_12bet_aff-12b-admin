import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { setShowPopup, setNoteInfo, setPagingOption } from '../actions';
import { toggleLoader } from '../../../../actions/loaderAction';

export class Content extends Component {
    constructor(props) {
        super(props);

        this.state = {
            gridData: props.noteList,
            shouldResetScrollPosition: true
        }
    }

    componentDidMount() {
        this.updateData()
    }

    componentDidUpdate(prevProps) {
        const { noteList } = this.props;

        if (noteList !== prevProps.noteList) {
            this.updateData()
        }
    }

    updateData() {
        const { noteInfo, pagingOption } = this.props;
        const param = Object.assign({}, { AffId: noteInfo.AffId, PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize });
        this.fetchData(param);
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        const url = 'Portal/UserPanel/Note/GetNoteList';
        const response = await post(url, param);

        if (response && response.status === 200) {
            const { MainData, TotalItem } = response.data;

            this.setState({ gridData: MainData, shouldResetScrollPosition: true });

            this.props.setPagingOption({ TotalItem, PageIndex: param.PageIndex, PageSize: param.PageSize });
        }

        this.props.toggleLoader(false);
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
                    <Button color="link" onClick={() => this.handleShowPopup(dataItem.NoteId)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    handleShowPopup = (noteId) => {
        const { noteInfo } = this.props;
        const param = Object.assign({}, noteInfo, { NoteId: noteId });

        this.getNoteContentById(param);

        this.props.setShowPopup(true);
    }

    async getNoteContentById(param) {
        const url = 'Portal/UserPanel/Note/GetNoteById';
        const response = await post(url, param);

        if (response && response.status === 200) {
            const newNoteInfo = Object.assign({}, param, { NoteContent: response.NoteContent });
            this.props.setNoteInfo(newNoteInfo);
        }
    }

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };

        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, this.props.param, newPaging);
        this.fetchData(newParam);
    };

    render() {
        const { gridData, shouldResetScrollPosition } = this.state;

        return gridData && (
            <div className="data-content">
                <div className="my-2">
                    <Button outline type="button" onClick={() => this.handleShowPopup(0, '')}>{globalResource.Add}</Button>
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


function mapStateToProps({ pagingOption, noteList, noteInfo }) {
    return Object.assign({}, { pagingOption, noteList, noteInfo });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopup, setNoteInfo, setPagingOption, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);