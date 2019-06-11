import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { resource, headerText, globalResource } from '../modules/constants';
import { setShowPopup, setPagingOption, fetchData, toggleLoader } from '../actions';

export class Content extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true
        }
    }

    componentDidMount() {
        const { pagingOption, param } = this.props;
        const newParam = Object.assign({}, param, { pageIndex: 1, pageSize: pagingOption.PageSize });

        this.props.fetchData(newParam);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={35}></Cell>
                <Cell header={true} colWidth={35} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Sender}>{headerText.Sender}</Cell>
                <Cell header={true} colWidth={150} title={headerText.Affiliate}>{headerText.Affiliate}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Subject}>{headerText.Subject}</Cell>
                <Cell header={true} colWidth={300} title={headerText.EmailAddress}>{headerText.EmailAddress}</Cell>
                <Cell header={true} colWidth={100} title={headerText.SentDate}>{headerText.SentDate}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this._buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
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

    async fetchData(param) {
        this.props.toggleLoader(true);

        const data = await this.props.fetchData(param);

        this.setState({ gridData: data ? data.MainData : null, shouldResetScrollPosition: true });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.PageIndex, PageSize: param.PageSize });

        this.props.toggleLoader(false);
}

    handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
    PageIndex: pageIndex,
    PageSize: pageSize
};
        const newParam = Object.assign({}, this.props.param, newPaging);

        this.props.setPagingOption(newPaging);
        this.fetchData(newParam);
};

    render() {
        const { gridData, shouldResetScrollPosition } = this.state;

        return (
            <div className="data-content">
                <div className="my-2">
                    <Button outline type="button" color="danger" className="mr-2">{resource.Delete}<span className="oi oi-x ml-2"></span></Button>
                </div>
                <div className="selection">
                    <span>{resource.Select}</span>
                    <Button type="button" color="link" className="text-dark font-weight-bold">
                        <u>All</u>
                    </Button>
                    <Button type="button" color="link" className="text-dark font-weight-bold">
                        <u>None</u>
                    </Button>
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


function mapStateToProps({ pagingOption, param,  }) {
    return Object.assign({}, { pagingOption, param });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setShowPopup, toggleLoader, fetchData, setPagingOption }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);