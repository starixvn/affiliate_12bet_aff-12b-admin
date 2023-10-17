import React, { PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource, displayDateFormat } from '../libs/constants';
import { setParam, setSelectedId, setCheckedIds, setShouldResetScrollPosition, fetchData } from '../actions';
import { claimResource } from '../../../../modules/claim';
import { formatDateTime } from '../../../../modules/utils';

export class Content extends PureComponent {
    constructor(props) {
        super(props)
    }

    componentDidUpdate(prevProps) {
        const { param } = this.props;

        if (!!param && param !== prevProps.param) {
            this.handleFetchData();
        }
    };

    handleFetchData = async () => {
        await this.props.fetchData(this.props.param);
    }

    onCheckItem = (isChecked, affId) => {
        const newCheckedIds = Object.assign({}, this.props.checkedIds, { [affId]: isChecked });

        this.props.setShouldResetScrollPosition(false);
        this.props.setCheckedIds(newCheckedIds);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                {this.props.isModifiable && <Cell header={true} colWidth={20}></Cell>}
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={150} title={headerText.Affiliate}>{headerText.Affiliate}</Cell>
                <Cell header={true} colWidth={250} title={headerText.OfferPageHtmlFile}>{headerText.OfferPageHtmlFile}</Cell>
                <Cell header={true} colWidth={150} title={headerText.FromDate}>{headerText.FromDate}</Cell>
                <Cell header={true} colWidth={150} title={headerText.ToDate}>{headerText.ToDate}</Cell>
                <Cell header={true} colWidth={50} title={headerText.Preview}>{headerText.Preview}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(this.buildDataRow(dataItem, i + 1));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan="7">{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    buildDataRow(dataItem, index) {
        const fileName = htmlDecode(dataItem.FileName);
        const effectiveFromDate = formatDateTime(dataItem.EffectiveFromDate, displayDateFormat);
        const effectiveToDate = formatDateTime(dataItem.EffectiveToDate, displayDateFormat);

        return (
            <Row key={`dataRow-${index}`}>
                { this.props.isModifiable && <Cell className="text-center">
                        <input type="checkbox" checked={!!this.props.checkedIds[dataItem.AffId]}
                            onChange={(e) => this.onCheckItem(e.target.checked, dataItem.AffId)} />
                    </Cell>
                }
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell title={dataItem.UserName}>{dataItem.UserName}</Cell>
                <Cell title={fileName}>{fileName}</Cell>
                <Cell className="text-center" title={effectiveFromDate}>{effectiveFromDate}</Cell>
                <Cell className="text-center" title={effectiveToDate}>{effectiveToDate}</Cell>
                <Cell className="text-center" title={headerText.Preview}>
                    <Button color="link" onClick={() => this.handlePreview(dataItem.PageId)}>
                        <span className="oi oi-eye text-primary"></span>
                    </Button>
                </Cell>
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

function mapStateToProps({ param, previewLink, pagingOption, checkedIds, shouldResetScrollPosition, gridData, claimsPrincipal }) {
    const isModifiable = claimsPrincipal.hasWriteClaim(claimResource.MediaGalleryMenu);
    return Object.assign({}, { param, previewLink, pagingOption, checkedIds, shouldResetScrollPosition, gridData, isModifiable });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setParam, setSelectedId, setCheckedIds, setShouldResetScrollPosition, fetchData }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);