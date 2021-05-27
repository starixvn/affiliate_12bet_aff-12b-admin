import React, { Fragment, PureComponent } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Breadcrumb, BreadcrumbItem } from 'reactstrap';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import RowDetail from './RowDetail';
import MyLink from '../../../common/components/MyLink';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { setPagingOption, toggleLoader, fetchDetailData, setSelectedAff } from '../actions';

export class DetailContent extends PureComponent {
    constructor(props) {
        super(props);

        this.state = {
            gridData: null,
            shouldResetScrollPosition: true
        }
    }

    componentDidMount() {
        const { pagingOption, detailViewParam } = this.props;

        if (detailViewParam) {
            const newParam = Object.assign({}, detailViewParam, { pageIndex: 1, pageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    componentDidUpdate(prevProps) {
        const { pagingOption, detailViewParam } = this.props;

        if (detailViewParam && detailViewParam !== prevProps.detailViewParam) {
            const newParam = Object.assign({}, detailViewParam, { pageIndex: 1, pageSize: pagingOption.PageSize });
            this.fetchData(newParam);
        }
    }

    async fetchData(param) {
        this.props.toggleLoader(true);

        const data = await this.props.fetchDetailData(param);

        this.setState({ gridData: data ? data.MainData : null, shouldResetScrollPosition: true });
        this.props.setPagingOption({ TotalItem: data ? data.TotalItem : 0, PageIndex: param.pageIndex, PageSize: param.pageSize });

        this.props.toggleLoader(false);
    }

    _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={50} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={90} title={headerText.Id}>{headerText.Id}</Cell>
                <Cell header={true} colWidth={200} title={headerText.Datetime}>{headerText.Datetime}</Cell>
                <Cell header={true} colWidth={150} title={headerText.UpdatedBy}>{headerText.UpdatedBy}</Cell>
                <Cell header={true} colWidth={360} title={headerText.Action}>{headerText.Action}</Cell>
                <Cell header={true} colWidth={150} title={headerText.Ip}>{headerText.Ip}</Cell>
            </Row>
        );
    }

    _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const { pagingOption } = this.props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                dataRows.push(this._buildDataRow(dataList[i], i + startIndex));
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
        const logId = dataItem.LogId;
        const ipAddress = dataItem.Ip;
        const url = `http://anonym.to/?http://legacytools.dnsstuff.com/tools/ipall.ch?tool_id=67&token=&toolhandler_redirect=0&ip=${ipAddress}&j=1`;

        const updatedBy = htmlDecode(dataItem.UpdatedName);
        const action = htmlDecode(dataItem.Action);
        const oldData = htmlDecode(dataItem.OldData);
        const newData = htmlDecode(dataItem.NewData);

        return (
            <Fragment key={`dataRow-${index}`}>
                <Row className="row-data">
                    <Cell title={index} className="text-center">{index}</Cell>
                    <Cell title={logId} className="text-center">{logId}</Cell>
                    <Cell title={dataItem.DateText} className="text-center">{dataItem.DateText}</Cell>
                    <Cell title={updatedBy} className="text-center">{updatedBy}</Cell>
                    <Cell title={action} className="text-center">{action}</Cell>
                    <Cell title={ipAddress} className="text-center">
                        <a target="_blank" href={url}>{ipAddress}</a>
                    </Cell>
                </Row>
                {
                    (oldData || newData) && (
                        <Row className="row-detail-group">
                            <Cell colSpan="6" className="p-0">
                                <RowDetail oldData={oldData} newData={newData} />
                            </Cell>
                        </Row>
                    )
                }
            </Fragment>
        )
    }

    handlePaging = (pageIndex, pageSize) => {
        const newParam = { ...this.props.detailViewParam, pageIndex, pageSize };

        this.fetchData(newParam);
    };

    render() {
        const { gridData, shouldResetScrollPosition } = this.state;

        return gridData && (
            <Fragment>
                <Breadcrumb className="mt-2">
                    (<BreadcrumbItem>
                        <MyLink onClick={() => this.props.setSelectedAff("")}>
                            Aff List
                        </MyLink>
                    </BreadcrumbItem>
                    <BreadcrumbItem>{this.props.selectedAff}</BreadcrumbItem>)
                </Breadcrumb>
                <div className="data-content new-view-log">
                    <div className="table-responsive table-striped new-view-log-table my-2">
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
            </Fragment>
        );
    }
}


function mapStateToProps({ selectedAff, detailViewParam, pagingOption }) {
    return Object.assign({}, { selectedAff, detailViewParam, pagingOption });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setPagingOption, toggleLoader, fetchDetailData, setSelectedAff }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(DetailContent);