import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource, displayDateFormat } from '../libs/constants';
import { setParam, setSelectedNewsLetterPageId, setCheckedNewsLetterPages, setShouldResetScrollPosition, fetchData } from '../actions';
import { claimResource } from '../../../../modules/claim';
import { formatDateTime } from '../../../../modules/utils';

export class Content extends PureComponent {
    constructor(props) {
        super(props)
    }

    onCheckItem = (isChecked, newsLetterId) => {
        const newCheckedNewsLetterPages = Object.assign({}, this.props.checkedNewsLetterPages, { [newsLetterId]: isChecked });

        this.props.setShouldResetScrollPosition(false);
        this.props.setCheckedNewsLetterPages(newCheckedNewsLetterPages);
    }

    _renderHeader() {
        const { isModifiable } = this.props;
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20}></Cell>
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={250} title={headerText.FileName}>{headerText.FileName}</Cell>
                <Cell header={true} colWidth={50} title={headerText.FileSize}>{headerText.FileSize}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={150} title={headerText.UploadDate}>{headerText.UploadDate}</Cell>
                <Cell header={true} colWidth={150} title={headerText.FromDate}>{headerText.FromDate}</Cell>
                <Cell header={true} colWidth={150} title={headerText.ToDate}>{headerText.ToDate}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Preview}>{headerText.Preview}</Cell>
                { isModifiable && <Cell header={true} colWidth={30} title={globalResource.Edit}>{globalResource.Edit}</Cell>}
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this.buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="11">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    buildDataRow(dataItem, index) {
        const fileName = htmlDecode(dataItem.FileName);
        const displayFileSize = Math.round((dataItem.FileSize / 1024) * 100) / 100;

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={!!this.props.checkedNewsLetterPages[dataItem.NewsLetterId]}
                        onChange={(e) => this.onCheckItem(e.target.checked, dataItem.NewsLetterId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-left" title={fileName}>{fileName}</Cell>
                <Cell className="text-center" title={dataItem.FileSize}>{displayFileSize}</Cell>
                <Cell className="text-center" title={dataItem.LanguageName}>{dataItem.LanguageName}</Cell>
                <Cell className="text-center" title={dataItem.UploadDate}>{formatDateTime(dataItem.UploadDate, displayDateFormat)}</Cell>
                <Cell className="text-center" title={dataItem.FromDate}>{formatDateTime(dataItem.FromDate, displayDateFormat)}</Cell>
                <Cell className="text-center" title={dataItem.ToDate}>{formatDateTime(dataItem.ToDate, displayDateFormat)}</Cell>
                <Cell className="text-center" title={headerText.Preview}>
                    <Button color="link" onClick={() => this.handlePreview(dataItem.NewsLetterId)}>
                        <span className="oi oi-eye text-primary"></span>
                    </Button>
                </Cell>
                { this.props.isModifiable &&
                    <Cell className="text-center" title="Edit Newsletter">
                        <Button color="link" onClick={() => this.props.setSelectedNewsLetterPageId(dataItem.NewsLetterId)}>
                            <span className="oi oi-pencil text-secondary"></span>
                        </Button>
                    </Cell>
                }
            </Row>
        )
    }

    _handlePaging = (pageIndex, pageSize) => {
        const newParam = Object.assign({}, this.props.param, { pageIndex, pageSize });

        this.props.setParam(newParam);
    };

    handlePreview = (id) => {
        const { previewLink } = this.props;
        const url = `${previewLink}?id=${id}`;
        window.open(url);
    }

    render() {
        const { gridData, pagingOption } = this.props;

        return gridData && (
            <div className="data-content">
                <div className="landing-page-content table-responsive table-striped my-2">
                    <Table
                        minWidth={1000}
                        autoWidth={true}
                        bodyHeight={300}
                        header={this._renderHeader()}
                        body={this._renderDataSection(gridData)}
                        containerPadding={20}
                        isPaging={true}
                        pageOption={pagingOption}
                        onPaging={this._handlePaging}
                        shouldResetScrollPosition={this.props.shouldResetScrollPosition}
                    />
                </div>
            </div>
        );
    }
}

function mapStateToProps({ param, previewLink, pagingOption, checkedNewsLetterPages, shouldResetScrollPosition, gridData, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGalleryMenu);
    return Object.assign({}, { param, previewLink, pagingOption, checkedNewsLetterPages, shouldResetScrollPosition, gridData, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setSelectedNewsLetterPageId, setCheckedNewsLetterPages, setShouldResetScrollPosition, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);