import React,  { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { setPagingOption, setParam, setSelectedNewsLetterPageId, setCheckedNewsLetterPages, setShouldResetScrollPosition, fetchData } from '../actions';
import { claimResource } from '../../../../modules/claim';
import{formatDateTime} from '../../../../modules/utils';

export const Content = (props) => {
    
    const _onCheckItem = (isChecked, newsLetterId) => {
        const newCheckedNewsLetterPages = Object.assign({}, props.checkedNewsLetterPages, { [newsLetterId]: isChecked });

        props.setShouldResetScrollPosition(false);
        props.setCheckedNewsLetterPages(newCheckedNewsLetterPages);
    }

    function _renderHeader() {
        const { isModifiable } = props;
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20}></Cell>
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={250} title={headerText.FileName}>{headerText.FileName}</Cell>
                <Cell header={true} colWidth={50} title={headerText.FileSize}>{headerText.FileSize}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={150} title={headerText.UploadDate}>{headerText.UploadDate}</Cell>
                <Cell header={true} colWidth={150} title={headerText.FromDate}>{headerText.FromDate}</Cell>
                <Cell header={true} colWidth={150} title={headerText.ToDate}>{headerText.ToDate}</Cell>
                <Cell header={true} colWidth={30} title={headerText.Preview}>{headerText.Preview}</Cell>
                { isModifiable && <Cell header={true} colWidth={30} title={globalResource.Edit}>{globalResource.Edit}</Cell>}
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="11">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const displayDateFormat = "MM/dd/yyyy hh:mm:ss"
        const { isModifiable } = props;
        const fileName = htmlDecode(dataItem.FileName);
        const displayFileSize = Math.round((dataItem.FileSize/1024)*100)/100;
        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={!!props.checkedNewsLetterPages[dataItem.NewsLetterId]}
                        onChange={(e) => _onCheckItem(e.target.checked, dataItem.NewsLetterId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-left" title={fileName}>{fileName}</Cell>
                <Cell className="text-center" title={dataItem.FileSize}>{displayFileSize}</Cell>
                <Cell className="text-center" title={dataItem.Language}>{dataItem.Language}</Cell>
                <Cell className="text-center" title={dataItem.UploadDate}>{formatDateTime(dataItem.UploadDate, displayDateFormat) }</Cell>
                <Cell className="text-center" title={dataItem.FromDate}>{formatDateTime(dataItem.FromDate, displayDateFormat)}</Cell>
                <Cell className="text-center" title={dataItem.ToDate}>{formatDateTime(dataItem.ToDate, displayDateFormat)}</Cell>
                <Cell className="text-center" title={headerText.Preview}>
                    <Button color="link" onClick={() => _handlePreview(dataItem.NewsLetterId)}>
                        <span className="oi oi-eye text-primary"></span>
                    </Button>
                </Cell>
                { isModifiable &&
                    <Cell className="text-center" title="Edit Landing Page">
                        <Button color="link" onClick={() => props.setSelectedNewsLetterPageId(dataItem.NewsLetterId)}>
                            <span className="oi oi-pencil text-secondary"></span>
                        </Button>
                    </Cell>
                }
            </Row>
        )
    }

    const _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, props.param, newPaging);

        props.setPagingOption(newPaging);
        props.setParam(newParam);
    };

    const _handlePreview = (id) => {
        const {previewLink} = props;
        const url = `${previewLink}?id=${id}`;
        window.open(url);
    }

    return props.gridData && (
        <div className="data-content">
            <div className="landing-page-content table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(props.gridData.MainData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={props.pagingOption}
                    onPaging={_handlePaging}
                    shouldResetScrollPosition={props.shouldResetScrollPosition}
                />
            </div>
        </div>
    );
}

function mapStateToProps({param, previewLink, pagingOption ,checkedNewsLetterPages, shouldResetScrollPosition, gridData, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGallery);
    return Object.assign({}, {param, previewLink, pagingOption, checkedNewsLetterPages, shouldResetScrollPosition, gridData, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({setPagingOption, setParam, setSelectedNewsLetterPageId, setCheckedNewsLetterPages, setShouldResetScrollPosition, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));