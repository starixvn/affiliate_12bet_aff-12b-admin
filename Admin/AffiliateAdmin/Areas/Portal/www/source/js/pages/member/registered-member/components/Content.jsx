import React, { useEffect } from 'react';
import { connect } from 'react-redux';
import { bindActionCreators } from 'redux';
import { Table, Row, Cell } from '@9softstudio/react-fixed-table-header';

import { htmlDecode } from '../../../../modules/htmlHelper';
import { headerText, globalResource } from '../libs/constants';
import { fetchData, setParam } from '../actions';
import { claimResource } from '../../../../modules/claim';

export const Content = (props) => {
    const { gridData, pagingOption, param, isShowMemberName } = props;

    useEffect(() => {
        if (param) {
            props.fetchData(param);
        }
    }, [param]);


    const _handlePaging = (pageIndex, pageSize) => {
        const newPaging = {
            PageIndex: pageIndex,
            PageSize: pageSize
        };
        const newParam = Object.assign({}, props.param, newPaging);

        props.setParam(newParam);
    };

    function _renderHeader() {
        return (
            <Row key="thead">
                <Cell header={true} colWidth={20} title={headerText.Index}>{headerText.Index}</Cell>
                <Cell header={true} colWidth={120} title={headerText.UserName}>{headerText.UserName}</Cell>
                { isShowMemberName && <Cell header={true} colWidth={100} title={headerText.FirstName}>{headerText.FirstName}</Cell>}
                { isShowMemberName && <Cell header={true} colWidth={100} title={headerText.LastName}>{headerText.LastName}</Cell>}
                <Cell header={true} colWidth={100} title={headerText.Currency}>{headerText.Currency}</Cell>
                <Cell header={true} colWidth={100} title={headerText.RegisteredDate}>{headerText.RegisteredDate}</Cell>
                <Cell header={true} colWidth={100} title={headerText.Affiliate}>{headerText.Affiliate}</Cell>
            </Row>
        );
    }

    function _renderDataSection(dataList) {
        const dataRows = [];

        if (dataList.length) {
            const startIndex = (pagingOption.PageIndex - 1) * pagingOption.PageSize + 1;

            for (let i = 0; i < dataList.length; i++) {
                const dataItem = dataList[i];
                dataRows.push(_buildDataRow(dataItem, i + startIndex));
            }
        }
        else {
            dataRows.push(<Row>
                <Cell className="text-center" colSpan={isShowMemberName ? "7" : "5"}>{globalResource.NoInfo}</Cell>
            </Row>);
        }

        return dataRows;
    }

    function _buildDataRow(dataItem, index) {
        const userName = htmlDecode(dataItem.UserName);
        const firstName = htmlDecode(dataItem.FirstName);
        const lastName = htmlDecode(dataItem.LastName);

        return (
            <Row key={`dataRow-${index}`}>
                <Cell className="text-center" title={index}>{index}</Cell>
                <Cell className="text-center" title={userName}>{userName}</Cell>
                { isShowMemberName && <Cell className="text-center" title={firstName}>{firstName}</Cell>}
                { isShowMemberName && <Cell className="text-center" title={lastName}>{lastName}</Cell>}
                <Cell className="text-center" title={dataItem.Currency}>{dataItem.Currency}</Cell>
                <Cell className="text-center" title={dataItem.CreatedDateText}>{dataItem.CreatedDateText}</Cell>
                <Cell className="text-center" title={dataItem.AffName}>{dataItem.AffName}</Cell>
            </Row>
        )
    }

    return gridData && (
        <div className="data-content">
            <div className="member-list-content table-responsive table-striped my-2">
                <Table
                    minWidth={1000}
                    autoWidth={true}
                    bodyHeight={300}
                    header={_renderHeader()}
                    body={_renderDataSection(gridData)}
                    containerPadding={20}
                    isPaging={true}
                    pageOption={pagingOption}
                    onPaging={_handlePaging}
                />
            </div>
        </div>
    );
}

function mapStateToProps({ pagingOption, param, gridData, claimsPrincipal }) {
    const isShowMemberName = claimsPrincipal.hasClaim(claimResource.MemberName);

    return Object.assign({}, { pagingOption, param, gridData, isShowMemberName });
}

function mapDispatchToProps(dispatch) {
    return bindActionCreators({ fetchData, setParam }, dispatch);
}

export default connect(mapStateToProps, mapDispatchToProps)(Content);