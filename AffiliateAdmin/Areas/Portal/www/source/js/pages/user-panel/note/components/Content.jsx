import React, { Component } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';

import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { headerText } from '../modules/constants';
import { globalResource } from '../../../../modules/globalResource';

import { post } from '../../../../modules/ajaxRequest';
import { setNoteList, setShowPopup, setParam, setPagingOption } from '../actions';
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
        const { param, pagingOption } = this.props;
        const newParam = Object.assign({}, param, { PageIndex: pagingOption.PageIndex, PageSize: pagingOption.PageSize });
        this.fetchData(newParam);
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
                    <Button color="link" onClick={() => this.handleShowPopup(dataItem.NoteId, dataItem.NoteContent)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    handleShowPopup = (noteId, noteContent) => {
        const { param } = this.props;
        const newParam = Object.assign({}, param, { NoteId: noteId, NoteContent: noteContent })

        this.props.setShowPopup(true);
        this.props.setParam(newParam);
    }

    handlePaging = (index, size) => {
        const newPaging = {
            PageIndex: index,
            PageSize: size
        };

        this.props.setPagingOption(newPaging);

        const newParam = Object.assign({}, this.props.param, newPaging);
        this.fetchData(newParam);
    };

    render() {
        const { shouldResetScrollPosition, pagingOption } = this.props;
        const { gridData } = this.state;

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


function mapStateToProps({ pagingOption, noteList, param }) {
    return Object.assign({}, { pagingOption, noteList, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setNoteList, setShowPopup, setParam, setPagingOption, toggleLoader }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);