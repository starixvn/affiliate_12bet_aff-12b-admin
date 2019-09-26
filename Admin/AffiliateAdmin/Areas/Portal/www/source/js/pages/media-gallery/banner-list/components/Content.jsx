import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';
import { Button } from 'reactstrap';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { setSelectedBannerId, setPagingOption, fetchData, setCheckedBanners, setShouldResetScrollPosition, setParam } from '../actions';

export const Content = (props) => {

    useEffect(() => {
        const { param } = props;
        !!param && props.fetchData(param);
    }, [props.param]);

    const _onCheckItem = (isChecked, BannerId) => {
        const newCheckedBanners = Object.assign({}, props.checkedBanners, { [BannerId]: isChecked });

        props.setShouldResetScrollPosition(false);
        props.setCheckedBanners(newCheckedBanners);
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

    function _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={30}></Cell>
                <Cell header={true} colWidth={30} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={300} title={headerText.Images}>{headerText.Images}</Cell>
                <Cell header={true} colWidth={150} title={headerText.FileName}>{headerText.FileName}</Cell>
                <Cell header={true} colWidth={80} title={headerText.Category}>{headerText.Category}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Language}>{headerText.Language}</Cell>
                <Cell header={true} colWidth={70} title={headerText.Currency}>{headerText.Currency}</Cell>
                <Cell header={true} colWidth={70} title={headerText.Device}>{headerText.Device}</Cell>
                <Cell header={true} colWidth={70} title={headerText.ImageType}>{headerText.ImageType}</Cell>
                <Cell header={true} colWidth={70} title={headerText.Size}>{headerText.Size}</Cell>
                <Cell header={true} colWidth={30} title={globalResource.Edit}>{globalResource.Edit}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList && dataList.length) {
            const { pagingOption } = props;
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + startIndex));
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
        const content = htmlDecode(dataItem.BannerContent);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center">
                    <input type="checkbox" checked={props.checkedBanners[dataItem.BannerId]}
                        onChange={(e) => _onCheckItem(e.target.checked, dataItem.BannerId)} />
                </Cell>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={content}>{content}</Cell>
                <Cell className="text-center" title={dataItem.Category}>{dataItem.Category}</Cell>
                <Cell className="text-center" title={dataItem.Language}>{dataItem.Language}</Cell>
                <Cell className="text-center" title={dataItem.Currency}>{dataItem.Currency}</Cell>
                <Cell className="text-center" title={dataItem.DeviceType}>{dataItem.DeviceType}</Cell>
                <Cell className="text-center" title={dataItem.ImageType}>{dataItem.ImageType}</Cell>
                <Cell className="text-center" title={dataItem.Size}>{dataItem.Size}</Cell>
                <Cell className="text-center" title="Edit Banner">
                    <Button color="link" onClick={() => props.setSelectedBannerId(dataItem.BannerId)}>
                        <span className="oi oi-pencil text-secondary"></span>
                    </Button>
                </Cell>
            </Row>
        )
    }

    return props.gridData && (
        <div className="data-content">
            <div className="banner-list-content table-responsive table-striped my-2">
                <Table
                    minWidth={1200}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(props.gridData)}
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

function mapStateToProps({ pagingOption, param, checkedBanners, shouldResetScrollPosition, gridData }) {
    return Object.assign({}, { pagingOption, param, checkedBanners, shouldResetScrollPosition, gridData });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ setSelectedBannerId, fetchData, setPagingOption, setCheckedBanners, setShouldResetScrollPosition, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(React.memo(Content));